using YouYou.Business.Models;
using YouYou.Business.Models.Dtos;
using YouYou.Business.Models.Pagination;

namespace YouYou.Business.Interfaces
{
    public interface IPersonService
    {
        Task CreatePerson(PersonDto personDto);
        Task<IEnumerable<Person>> GetPersonsToSelect();
        Task<IEnumerable<Person>> GetAllPersons(PersonFilter filter);
        Task<int> GetTotalRecords(PersonFilter filter);
        Task<PersonDto> GetPersonByIdWithIncludes(int id);
        Task<Person> GetPersonByIdWithIncludesTracked(int id);
        Task Update(PersonDto personDto);
        Task<Person> GetPersonById(int id); 
        Task Disable(int id);
        Task Enable(int id);
        Task DeletePerson(Person person);
    }
}
