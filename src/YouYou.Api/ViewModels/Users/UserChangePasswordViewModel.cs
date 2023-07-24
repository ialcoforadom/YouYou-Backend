using System.ComponentModel.DataAnnotations;

namespace YouYou.Api.ViewModels
{
    public class UserChangePasswordViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}
