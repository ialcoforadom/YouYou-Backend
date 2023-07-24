using System.ComponentModel.DataAnnotations;

namespace YouYou.Api.ViewModels
{
    public class RefreshTokenDataViewModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
