using System.ComponentModel.DataAnnotations;
using YouYou.Api.Extensions;

namespace YouYou.Api.ViewModels
{
    public class AddressViewModel
    {
        [CEPValidationAttribute(ErrorMessage = "O campo {0} está em formato inválido")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(256, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(256, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Neighborhood { get; set; }

        [StringLength(256, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string? Complement { get; set; }

        public int StateId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int CityId { get; set; }
    }
}
