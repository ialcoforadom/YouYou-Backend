using YouYou.Api.Extensions;
using System.ComponentModel.DataAnnotations;

namespace YouYou.Api.ViewModels
{
    public class CreatePersonViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool IsCompany { get; set; }

        [StringLength(256, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(256, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? CompanyName { get; set; }

        [StringLength(256, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? TradingName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(13, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string FirstNumber { get; set; }

        [StringLength(13, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string? SecondNumber { get; set; }

        [CPFValidation(ErrorMessage = "O campo {0} está em formato inválido")]
        public string? CPF { get; set; }

        [CNPJValidation(ErrorMessage = "O campo {0} está em formato inválido")]
        public string? CNPJ { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public AddressViewModel Address { get; set; }

        public DateTime? BirthdayDate { get; set; }

        public int? GenderId { get; set; }

        public Guid RoleId { get; set; }
    }
}
