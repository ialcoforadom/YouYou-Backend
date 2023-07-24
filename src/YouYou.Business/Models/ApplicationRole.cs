using Microsoft.AspNetCore.Identity;

namespace YouYou.Business.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ApplicationRole(string name) : base(name) { }
    }
}
