using AutoMapper;
using YouYou.Api.ViewModels;
using YouYou.Business.Models;
using YouYou.Business.Models.Pagination;
using YouYou.Business.Utils;

namespace YouYou.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            #region PaginacaoFiltros
            CreateMap<PaginationFilterBase, PaginationFilterBase>()
                .ForMember(dest => dest.PageNumber, src => src.MapFrom(src => src.PageNumber < 1 ? 1 : src.PageNumber))
                .ForMember(dest => dest.PageSize, src => src.MapFrom(src => src.PageSize > 50 ? 50 : src.PageSize));

            CreateMap<DeviceFilter, DeviceFilter>()
               .ForMember(dest => dest.PageNumber, src => src.MapFrom(src => src.PageNumber < 1 ? 1 : src.PageNumber))
               .ForMember(dest => dest.PageSize, src => src.MapFrom(src => src.PageSize > 50 ? 50 : src.PageSize));

            CreateMap<PersonFilter, PersonFilter>()
               .ForMember(dest => dest.PageNumber, src => src.MapFrom(src => src.PageNumber < 1 ? 1 : src.PageNumber))
               .ForMember(dest => dest.PageSize, src => src.MapFrom(src => src.PageSize > 50 ? 50 : src.PageSize));
            #endregion

            #region Addresses
            CreateMap<AddressViewModel, Address>()
                    .ForMember(dest => dest.CEP, src => src.MapFrom(c =>
                        UsefulFunctions.RemoveNonNumeric(c.CEP)));
            CreateMap<Address, AddressViewModel>()
                .ForMember(dest => dest.StateId, src => src.MapFrom(c => c.City.StateId));
            #endregion

            #region Person
            CreateMap<CreatePersonViewModel, Person>();

            CreateMap<CreatePersonViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(c => c.UserName));

            CreateMap<CreatePersonViewModel, PhysicalPerson>()
                .ForMember(dest => dest.CPF, src => src.MapFrom(c =>
                    UsefulFunctions.RemoveNonNumeric(c.CPF)));

            CreateMap<CreatePersonViewModel, JuridicalPerson>()
                .ForMember(dest => dest.CNPJ, src => src.MapFrom(c =>
                    UsefulFunctions.RemoveNonNumeric(c.CNPJ)));

            CreateMap<Person, ListPersonToSelectViewModel>()
                .ForMember(dest => dest.Name, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.Name : c.JuridicalPerson.CompanyName));

            CreateMap<Person, ListPersonViewModel>()
                .ForMember(dest => dest.Name, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.Name : c.JuridicalPerson.CompanyName))
                .ForMember(dest => dest.Email, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.Email : c.JuridicalPerson.Email))
                .ForMember(dest => dest.Phone, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.FirstNumber : c.JuridicalPerson.FirstNumber))
                .ForMember(dest => dest.CpfOrCnpj, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.CPF : c.JuridicalPerson.CNPJ))
                .ForMember(dest => dest.CityName, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.Address.City.Name : c.JuridicalPerson.Address.City.Name))
                .ForMember(dest => dest.GenderName, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.Gender.Name : null))
                .ForMember(dest => dest.BirthdayDate, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.BirthdayDate : null))
                .ForMember(dest => dest.IsActived, src => src.MapFrom(c => c.PhysicalPersonId != null ? !c.PhysicalPerson.User.Disabled : !c.JuridicalPerson.User.Disabled));


            CreateMap<UpdatePersonViewModel, Person>().ReverseMap()
                .ForMember(dest => dest.Email, src => src.MapFrom(c => c.PhysicalPersonId!= null ? c.PhysicalPerson.User.Email : c.JuridicalPerson.User.Email))
                .ForMember(dest => dest.IsCompany, src => src.MapFrom(c => c.PhysicalPersonId!= null ? c.PhysicalPerson.User.IsCompany : c.JuridicalPerson.User.IsCompany))
                .ForMember(dest => dest.Name, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.Name : null))
                .ForMember(dest => dest.CompanyName, src => src.MapFrom(c => c.JuridicalPersonId != null ? c.JuridicalPerson.CompanyName : null))
                .ForMember(dest => dest.TradingName, src => src.MapFrom(c => c.JuridicalPersonId != null ? c.JuridicalPerson.TradingName : null))
                .ForMember(dest => dest.UserName, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.User.UserName : c.JuridicalPerson.User.UserName))
                .ForMember(dest => dest.FirstNumber, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.FirstNumber : c.JuridicalPerson.FirstNumber))
                .ForMember(dest => dest.SecondNumber, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.SecondNumber : c.JuridicalPerson.SecondNumber))
                .ForMember(dest => dest.CPF, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.CPF : null))
                .ForMember(dest => dest.CNPJ, src => src.MapFrom(c => c.JuridicalPersonId != null ? c.JuridicalPerson.CNPJ : null))
                .ForMember(dest => dest.BirthdayDate, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.BirthdayDate : null))
                .ForMember(dest => dest.GenderId, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.GenderId : null))
                .ForMember(dest => dest.Address, src => src.MapFrom(c => c.PhysicalPersonId != null ? c.PhysicalPerson.Address : c.JuridicalPerson.Address));

            CreateMap<UpdatePersonViewModel, ApplicationUser>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdatePersonViewModel, PhysicalPerson>()
                .ForMember(dest => dest.CPF, src => src.MapFrom(c =>
                    UsefulFunctions.RemoveNonNumeric(c.CPF)))
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdatePersonViewModel, JuridicalPerson>()
                .ForMember(dest => dest.CNPJ, src => src.MapFrom(c =>
                    UsefulFunctions.RemoveNonNumeric(c.CNPJ)))
                .ForMember(x => x.Id, opt => opt.Ignore());
            #endregion

            #region Dispositivos
            CreateMap<CreateDeviceViewModel, Device>().ReverseMap();
            CreateMap<Device, ListDeviceViewModel>()
                .ForMember(dest => dest.PersonName, src => src.MapFrom(c => c.Person.PhysicalPersonId != null ? c.Person.PhysicalPerson.Name : c.Person.JuridicalPerson.CompanyName));
            CreateMap<UpdateDeviceViewModel, Device>().ReverseMap();
            #endregion
        }
    }
}
