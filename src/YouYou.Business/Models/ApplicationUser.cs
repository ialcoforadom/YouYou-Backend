using Microsoft.AspNetCore.Identity;

namespace YouYou.Business.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string NickName { get; set; }

        public bool IsCompany { get; set; }

        public bool Disabled { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public PhysicalPerson? PhysicalPerson { get; set; }

        public JuridicalPerson? JuridicalPerson { get; set; }
    }
}
