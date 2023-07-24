using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Business.Services;
using Microsoft.AspNetCore.Identity;

namespace YouYou.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IUser _appUser;

        public UserService(IErrorNotifier errorNotifier,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, 
            IUserRepository userRepository, IUser appUser) : base(errorNotifier)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _appUser = appUser;
        }

        public async Task<ApplicationUser> GetByIdWithPerson(Guid id)
        {
            return await _userRepository.GetByIdWithPerson(id);
        }

        public async Task<bool> Add(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return true;
            }

            foreach (var erro in result.Errors)
            {
                NotifyError(erro.Description);
            }

            return false;
        }

        public async Task<bool> Update(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            foreach (var erro in result.Errors)
            {
                NotifyError(erro.Description);
            }

            return false;
        }

        public async Task<bool> Update(ApplicationUser user, string password)
        {
            bool updatePasswordSucceeded = await UpdatePassword(user, password);
            if (updatePasswordSucceeded)
            {
                return await Update(user);
            }

            return false;
        }

        private async Task<bool> UpdatePassword(ApplicationUser user, string senha)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPasswordResult = await _userManager
                .ResetPasswordAsync(user, token, senha);

            if (resetPasswordResult.Succeeded)
            {
                return true;
            }

            foreach (var error in resetPasswordResult.Errors)
            {
                NotifyError(error.Description);
            }

            return false;
        }

        public async Task<bool> AddRole(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);

            if (result.Succeeded)
            {
                return true;
            }

            foreach (var erro in result.Errors)
            {
                NotifyError(erro.Description);
            }

            return false;
        }

        public async Task<bool> AddRole(ApplicationUser user, Guid roleId)
        {
            var roleName = _roleManager.Roles.Where(c => c.Id == roleId).FirstOrDefault().Name;

            return await AddRole(user, roleName);
        }
    }
}
