namespace YouYou.Business.Models
{
    public class PhysicalPerson : Entity
    {
        public string? CPF { get; set; }
        public string Name { get; set; }
        public string FirstNumber { get; set; }
        public string? SecondNumber { get; set; }
        public string? Email { get; set; }
        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public DateTime? BirthdayDate { get; set; }
        public int? GenderId { get; set; }
        public Gender? Gender { get; set; }
    }
}
