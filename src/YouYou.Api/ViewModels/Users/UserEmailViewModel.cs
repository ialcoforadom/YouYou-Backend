using System.ComponentModel.DataAnnotations;

namespace YouYou.Api.ViewModels
{
    public class UserEmailViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
    }
}
