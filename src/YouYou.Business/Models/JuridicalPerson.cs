namespace YouYou.Business.Models
{
    public class JuridicalPerson : Entity
    {
        public string? CNPJ { get; set; }
        public string CompanyName { get; set; }
        public string? TradingName { get; set; }
        public string FirstNumber { get; set; }
        public string? SecondNumber { get; set; }
        public string? Email { get; set; }
        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
