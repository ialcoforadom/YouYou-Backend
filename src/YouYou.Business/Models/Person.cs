namespace YouYou.Business.Models
{
    public class Person : Entity
    {
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public JuridicalPerson? JuridicalPerson { get; set; }
        public int? JuridicalPersonId { get; set; }
        public PhysicalPerson? PhysicalPerson { get; set; }
        public int? PhysicalPersonId { get; set; }
    }
}
