using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouYou.Api.Helpers;
using YouYou.Api.ViewModels;
using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Business.Models.Dtos;
using YouYou.Business.Models.Pagination;

namespace YouYou.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/api/[controller]")]
    [ApiController]
    public class PersonController : MainController<PersonController>
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public PersonController(IPersonService personService, IUser appUser, IUriService uriService,
            IMapper mapper, IErrorNotifier errorNotifier) : base(errorNotifier, appUser)
        {
            _personService = personService;
            _mapper = mapper;
            _uriService = uriService;
        }
        
        [HttpPost]
        public async Task<ActionResult<CreatePersonViewModel>> CreatePerson(CreatePersonViewModel createPersonViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            PersonDto personDto = MappingPerson(createPersonViewModel);

            await _personService.CreatePerson(personDto);

            return CustomResponse(createPersonViewModel);
        }
        private PersonDto MappingPerson(CreatePersonViewModel createPersonViewModel)
        {
            var person = _mapper.Map<Person>(createPersonViewModel);
            if (!createPersonViewModel.IsCompany)
            {
                person.PhysicalPerson = _mapper.Map<PhysicalPerson>(createPersonViewModel);
                person.PhysicalPerson.User = _mapper.Map<ApplicationUser>(createPersonViewModel);
            }
            else if (createPersonViewModel.IsCompany)
            {
                person.JuridicalPerson = _mapper.Map<JuridicalPerson>(createPersonViewModel);
                person.JuridicalPerson.User = _mapper.Map<ApplicationUser>(createPersonViewModel);
            }

            return new PersonDto(person, createPersonViewModel.Password, createPersonViewModel.RoleId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons([FromQuery] PersonFilter filter)
        {
            string route = Request.Path.Value;

            var validFilter = _mapper.Map<PersonFilter>(filter);
            var persons = _mapper.Map<IEnumerable<ListPersonViewModel>>(await _personService.GetAllPersons(validFilter));

            int totalRecords = await _personService.GetTotalRecords(validFilter);
            var pagedReponse = PaginationHelper.CreatePagedReponse<ListPersonViewModel>(persons.ToList(), validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }


        [Authorize(Roles = "Admin, Operador")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UpdatePersonViewModel>> GetPersonById(int id)
        {
            var personDto = await _personService.GetPersonByIdWithIncludes(id);
            if (personDto == null) return NotFound();

            var result = _mapper.Map<UpdatePersonViewModel>(personDto.Person);

            return result;
        }

        [Authorize(Roles = "Admin, Operador")]
        [HttpPut]
        public async Task<ActionResult<UpdatePersonViewModel>> Update(UpdatePersonViewModel userViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var person = await _personService.GetPersonByIdWithIncludesTracked(userViewModel.Id);
            MappingUpdate(userViewModel, person);

            var personDto = new PersonDto(person, userViewModel.Password);
            await _personService.Update(personDto);

            return CustomResponse(userViewModel);
        }

        private void MappingUpdate(UpdatePersonViewModel userViewModel, Person person)
        {
            if (userViewModel.IsCompany)
            {
                _mapper.Map<UpdatePersonViewModel, JuridicalPerson>(userViewModel, person.JuridicalPerson);
                _mapper.Map<UpdatePersonViewModel, ApplicationUser>(userViewModel, person.JuridicalPerson.User);
            }
            else if (!userViewModel.IsCompany)
            {
                _mapper.Map<UpdatePersonViewModel, PhysicalPerson>(userViewModel, person.PhysicalPerson);
                _mapper.Map<UpdatePersonViewModel, ApplicationUser>(userViewModel, person.PhysicalPerson.User);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = await _personService.GetPersonByIdWithIncludesTracked(id);
            if (person == null) return NotFound();
            else
                await _personService.DeletePerson(person);
            return CustomResponse();
        }
        [HttpPut("Disable/{id:int}")]
        public async Task<ActionResult> Disable(int id)
        {
            var person = await _personService.GetPersonById(id);
            if (person == null) return NotFound();

            await _personService.Disable(id);
            return CustomResponse();
        }
        [HttpPut("Enable/{id:int}")]
        public async Task<ActionResult> Enable(int id)
        {
            var person = await _personService.GetPersonById(id);
            if (person == null) return NotFound();

            await _personService.Enable(id);
            return CustomResponse();
        }

        [HttpGet("GetPersonsToSelect")]
        public async Task<IActionResult> GetPersonsToSelect()
        {
            var persons = _mapper.Map<IEnumerable<ListPersonToSelectViewModel>>(await _personService.GetPersonsToSelect());
            return Ok(persons);
        }
    }
}
