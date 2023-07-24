using YouYou.Api.Extensions;
using YouYou.Api.ViewModels;
using YouYou.Business.Interfaces;
using YouYou.Business.Interfaces.Emails;
using YouYou.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace YouYou.Api.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController : MainController<AuthController>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly IDistributedCache _cache;
        private readonly IEmailService _emailService;

        public AuthController(IErrorNotifier errorNotifier,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, 
            IOptions<AppSettings> appSettings,
            IUser user, 
            IUserService userService,
            IEmailService emailService, 
            IDistributedCache cache) : base(errorNotifier, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _userService = userService;
            _emailService = emailService;
            _cache = cache;
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return CustomResponse();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginUser.UserName);
                if (user.Disabled || user.IsDeleted)
                {
                    NotifyError("Usuário ou Senha incorretos");
                    return CustomResponse(loginUser);
                }

                return CustomResponse(await GenerateJwt(user));
            }
            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotifyError("Usuário ou Senha incorretos");
            return CustomResponse(loginUser);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult> RefreshToken(RefreshTokenDataViewModel requestRefreshToken)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            string strTokenArmazenado = _cache.GetString(requestRefreshToken.RefreshToken);

            if (!RefreshTokenIsValid(requestRefreshToken, strTokenArmazenado)) return CustomResponse(requestRefreshToken);

            _cache.Remove(requestRefreshToken.RefreshToken);

            var user = await _userManager.FindByIdAsync(requestRefreshToken.UserId.ToString());
            if (user.Disabled)
            {
                NotifyError("Usuário desabilitado.");
                return CustomResponse(requestRefreshToken);
            }

            return CustomResponse(await GenerateJwt(user));
        }

        [HttpPost("RecoverPassword")]
        public async Task<ActionResult> RecoverPassword([FromBody] UserEmailViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                NotifyError("Se você não recebeu o código no seu e-mail, por favor revise a escrita do seu endereço de e-mail (verifique se não há erros de digitação) e solicite o reenvio das instruções.");
                return CustomResponse();
            }

            var modelEmailHtml = System.IO.File.ReadAllText(@"./Contents/modelEmail.html");

            var randomAlphanumeric = GenerateRandomAlphanumeric(user.Id);

            modelEmailHtml = modelEmailHtml.Replace("{{randomAlphanumeric}}", randomAlphanumeric);

            var message = new EmailMessage(new string[] { model.Email }, "Recuperação de Senha", modelEmailHtml);

            await _emailService.SendEmailAsync(message);
            return CustomResponse("UsuarioId: " + user.Id);
        }

        [HttpPost("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromBody] UserRandomAlphanumericViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            string randomAlphanumeric = _cache.GetString(model.RandomAlphanumeric);

            if (!RandomAlphanumericValid(model, randomAlphanumeric)) return CustomResponse(model);

            _cache.Remove(model.RandomAlphanumeric);

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user.Disabled)
            {
                NotifyError("Usuário inativo.");
                return CustomResponse(model);
            }

            return CustomResponse("UsuarioId: " + user.Id);
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult<EmailConfiguration>> ChangePassword([FromBody] UserChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null) return NotFound();

            await _userService.Update(user, model.NewPassword);
            return CustomResponse("Senha alterada com sucesso!");
        }

        private bool RefreshTokenIsValid(RefreshTokenDataViewModel requestRefreshToken, string strTokenArmazenado)
        {
            if (string.IsNullOrWhiteSpace(strTokenArmazenado))
            {
                NotifyError("Falha ao Autenticar");
                return false;
            }

            var refreshTokenBase = JsonConvert
                .DeserializeObject<RefreshTokenDataViewModel>(strTokenArmazenado);

            if (refreshTokenBase.UserId != requestRefreshToken.UserId ||
               refreshTokenBase.RefreshToken != requestRefreshToken.RefreshToken)
            {
                NotifyError("Falha ao Autenticar");
                return false;
            }

            return true;
        }

        private async Task<LoginResponseViewModel> GenerateJwt(ApplicationUser user)
        {
            var claims = await GetClaims(user);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            UserTokenViewModel userData = await GetUserData(user, claims);

            string token = GenerateRefreshToken(user.Id);

            DateTime expiryHours = DateTime.Now.AddHours(_appSettings.ExpiryHours);
            DateTimeOffset data = (DateTimeOffset)expiryHours;
            return new LoginResponseViewModel
            {
                AccessToken = CreateToken(identityClaims),
                //ExpiraEm = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                ExpiresIn = data.ToUnixTimeMilliseconds(),
                RefreshToken = token,
                UserToken = userData
            };
        }
        private string GenerateRefreshToken(Guid userId)
        {
            string refreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty);

            var refreshTokenData = new RefreshTokenDataViewModel();
            refreshTokenData.RefreshToken = refreshToken;
            refreshTokenData.UserId = userId;

            DistributedCacheEntryOptions optionsCache = new DistributedCacheEntryOptions();
            optionsCache.SetAbsoluteExpiration(TimeSpan.FromHours(_appSettings.FinalExpiration));
            _cache.SetString(refreshToken, JsonConvert.SerializeObject(refreshTokenData), optionsCache);

            return refreshToken;
        }
        private async Task<IList<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email != null ? user.Email : string.Empty));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            return claims;
        }

        private async Task<UserTokenViewModel> GetUserData(ApplicationUser usuario, IList<Claim> claims)
        {
            var notTypeResults = new List<string>(){
                JwtRegisteredClaimNames.Sub,
                JwtRegisteredClaimNames.Email,
                JwtRegisteredClaimNames.Jti,
                JwtRegisteredClaimNames.Nbf,
                JwtRegisteredClaimNames.Iat
            };

            var userWithPerson = await _userService.GetByIdWithPerson(usuario.Id);

            UserTokenViewModel userData = new UserTokenViewModel()
            {
                Id = usuario.Id,
                Name = ((userWithPerson.PhysicalPerson != null || userWithPerson.JuridicalPerson != null) ? (userWithPerson.IsCompany ? userWithPerson.JuridicalPerson.CompanyName : userWithPerson.PhysicalPerson.Name) : usuario.UserName),
                UserName = usuario.UserName,
                Claims = claims.Where(c => !notTypeResults.Contains(c.Type))
                            .Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })

            };

            return userData;
        }

        private string CreateToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emitter,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiryHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }
        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        private string GenerateRandomAlphanumeric(Guid userId)
        {
            string randomAlphanumeric = CreateRandomAlphanumeric(6);

            var alphanumericRandomDate = new UserRandomAlphanumericViewModel();
            alphanumericRandomDate.UserId = userId;
            alphanumericRandomDate.RandomAlphanumeric = randomAlphanumeric;

            DistributedCacheEntryOptions optionsCache = new DistributedCacheEntryOptions();
            optionsCache.SetAbsoluteExpiration(TimeSpan.FromHours(1));
            _cache.SetString(randomAlphanumeric, JsonConvert.SerializeObject(alphanumericRandomDate), optionsCache);

            return randomAlphanumeric;
        }

        private string CreateRandomAlphanumeric(int numero)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            var result = new string(
                Enumerable.Repeat(characters, numero)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        private bool RandomAlphanumericValid(UserRandomAlphanumericViewModel model, string storedAlphanumeric)
        {
            if (string.IsNullOrWhiteSpace(storedAlphanumeric))
            {
                NotifyError("Falha ao Autenticar");
                return false;
            }

            var refreshTokenBase = JsonConvert
                .DeserializeObject<UserRandomAlphanumericViewModel>(storedAlphanumeric);

            if (refreshTokenBase.UserId != model.UserId ||
               refreshTokenBase.RandomAlphanumeric != model.RandomAlphanumeric)
            {
                NotifyError("Falha ao Autenticar");
                return false;
            }

            return true;
        }
    }
}
