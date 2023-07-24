namespace YouYou.Business.Models
{
    public class Address : Entity
    {
        public string? CEP { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string? Complement { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

    }
}
