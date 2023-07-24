namespace YouYou.Api.ViewModels
{
    public class ListPersonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthdayDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CpfOrCnpj { get; set; }
        public string CityName { get; set; }
        public string GenderName { get; set; }
        public bool IsActived { get; set; }
    }
}
