using System.Transactions;
using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Business.Models.Dtos;
using YouYou.Business.Models.Pagination;
using YouYou.Business.Models.Validations;

namespace YouYou.Business.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUserService _userService;
        public PersonService(IErrorNotifier errorNotifier,
            IPersonRepository personRepository, IUserService userService) : base(errorNotifier)
        {
            _personRepository = personRepository;
            _userService = userService;
        }
        public async Task CreatePerson(PersonDto personDto)
        {
            var person = personDto.Person;
            if (person.PhysicalPerson != null)
            {
                var sucessValidation = ValidationPhysicalPerson(person);
                if (sucessValidation)
                {
                    using (TransactionScope tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        person.PhysicalPerson.User.IsCompany = false;
                        person.PhysicalPerson.User.NickName = "";
                        var sucess = await _userService.Add(person.PhysicalPerson.User, personDto.Password);
                        if (sucess)
                        {
                            var createdPerson = await _userService.AddRole(person.PhysicalPerson.User, personDto.RoleId);
                            if (createdPerson)
                            {
                                person.CreationDate = DateTime.Now;
                                person.PhysicalPerson.User.Disabled = false;
                                await _personRepository.Add(person);
                                tr.Complete();
                            }
                        }
                    }
                }
            }
            else
            {
                var sucessValidation = ValidationJuridicalPerson(person);
                if (sucessValidation)
                {
                    using (TransactionScope tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        person.JuridicalPerson.User.IsCompany = true;
                        person.JuridicalPerson.User.NickName = "";
                        var sucess = await _userService.Add(person.JuridicalPerson.User, personDto.Password);
                        if (sucess)
                        {
                            var createdPerson = await _userService.AddRole(person.JuridicalPerson.User, personDto.RoleId);
                            if (createdPerson)
                            {
                                person.CreationDate = DateTime.Now;
                                person.JuridicalPerson.User.Disabled = false;
                                await _personRepository.Add(person);
                                tr.Complete();
                            }
                        }
                    }
                }
            }
        }
        private bool ValidationPhysicalPerson(Person person)
        {
            if (!ExecuteValidation(new AddressValidation(), person.PhysicalPerson.Address) ||
                   !ExecuteValidation(new PhysicalPersonValidation(), person.PhysicalPerson)) return false;
            return true;
        }
        private bool ValidationJuridicalPerson(Person person)
        {
            if (!ExecuteValidation(new AddressValidation(), person.JuridicalPerson.Address) ||
                   !ExecuteValidation(new JuridicalPersonValidation(), person.JuridicalPerson)) return false;
            return true;
        }

        public async Task<IEnumerable<Person>> GetPersonsToSelect()
        {
            return await _personRepository.GetPersonsToSelect();
        }
        public async Task<IEnumerable<Person>> GetAllPersons(PersonFilter filter)
        {
            return await _personRepository.GetAllPersons(filter);
        }
        public async Task<int> GetTotalRecords(PersonFilter filter)
        {
            return await _personRepository.GetTotalRecords(filter);
        }
        public async Task<PersonDto> GetPersonByIdWithIncludes(int id)
        {
            var person = await _personRepository.GetByIdWithIncludes(id);

            return new PersonDto(person);
        }
        public async Task<Person> GetPersonByIdWithIncludesTracked(int id)
        {
            return await _personRepository.GetPersonByIdWithIncludesTracked(id);
        }

        public async Task Update(PersonDto personDto)
        {
            var person = personDto.Person;

            if (person.PhysicalPerson != null)
            {
                var sucessValidation = ValidationPhysicalPerson(person);
                if (sucessValidation)
                {

                    using (TransactionScope tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        bool succeeded;

                        if (string.IsNullOrEmpty(personDto.Password))
                            succeeded = await _userService.Update(person.PhysicalPerson.User);
                        else
                            succeeded = await _userService.Update(person.PhysicalPerson.User, personDto.Password);

                        if (succeeded)
                        {
                            await _personRepository.Update(person);
                            tr.Complete();
                        }
                    }
                }
            }
            else
            {
                var sucessValidation = ValidationJuridicalPerson(person);
                if (sucessValidation)
                {
                    using (TransactionScope tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        bool succeeded;

                        if (string.IsNullOrEmpty(personDto.Password))
                            succeeded = await _userService.Update(person.JuridicalPerson.User);
                        else
                            succeeded = await _userService.Update(person.JuridicalPerson.User, personDto.Password);

                        if (succeeded)
                        {
                            await _personRepository.Update(person);
                            tr.Complete();
                        }
                    }
                }
            }
        }
        public async Task<Person> GetPersonById(int id)
        {
            return await _personRepository.GetPersonById(id);
        }
        public async Task Disable(int id)
        {
            var person = await _personRepository.GetPersonByIdWithIncludesTracked(id);
            if (person.PhysicalPersonId != null)
                person.PhysicalPerson.User.Disabled = true;
            else
                person.JuridicalPerson.User.Disabled = true;
            await _personRepository.Update(person);
        }
        public async Task Enable(int id)
        {
            var person = await _personRepository.GetPersonByIdWithIncludesTracked(id);
            if (person.PhysicalPersonId != null)
                person.PhysicalPerson.User.Disabled = false;
            else
                person.JuridicalPerson.User.Disabled = false;

            await _personRepository.Update(person);
        }
        public async Task DeletePerson(Person person)
        {
            person.IsDeleted = true;
            if (person.PhysicalPersonId != null)
                person.PhysicalPerson.User.IsDeleted = true;
            else
                person.JuridicalPerson.User.IsDeleted = true;
            await _personRepository.Update(person);
        }
        public void Dispose()
        {
            _personRepository?.Dispose();
        }
    }
}
