using YouYou.Business.Models;

namespace YouYou.Business.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdWithPerson(Guid id);
    }
}
