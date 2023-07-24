using YouYou.Business.Models;
using YouYou.Business.Models.Pagination;

namespace YouYou.Business.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<IEnumerable<Person>> GetPersonsToSelect(); 
        Task<IEnumerable<Person>> GetAllPersons(PersonFilter filter);
        Task<int> GetTotalRecords(PersonFilter filter);
        Task<Person> GetByIdWithIncludes(int id);
        Task<Person> GetPersonByIdWithIncludesTracked(int id);
        Task<Person> GetPersonById(int id);

    }
}
