using System.ComponentModel.DataAnnotations;

namespace YouYou.Api.ViewModels
{
    public class CreateDeviceViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Code { get; set; }
        public int? PersonId { get; set; }
    }
}
