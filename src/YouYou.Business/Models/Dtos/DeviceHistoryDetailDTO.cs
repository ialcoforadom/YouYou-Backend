namespace YouYou.Business.Models.Dtos
{
    public class DeviceHistoryDetailDTO
    {
        public string PersonName { get; set; }
        public string Phone { get; set; }
        public DateTime BindingDate { get; set; }
        public DateTime? UnbindingDate { get; set; }
        public string CityName { get; set; }
        public bool Status { get; set; }
        public DeviceHistoryDetailDTO(DeviceHistory history)
        {
            PersonName = history.Person.PhysicalPersonId != null ? history.Person.PhysicalPerson.Name : history.Person.JuridicalPerson.CompanyName;
            Phone = history.Person.PhysicalPersonId != null ? history.Person.PhysicalPerson.FirstNumber : history.Person.JuridicalPerson.FirstNumber;
            CityName = history.Person.PhysicalPersonId != null ? history.Person.PhysicalPerson.Address.City.Name : history.Person.JuridicalPerson.Address.City.Name;
            UnbindingDate = history.UnbindingDate;
            BindingDate = history.BindingDate;
            Status = history.Status;
        }
    }
}
