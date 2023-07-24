namespace YouYou.Business.Models
{
    public class Device : Entity
    {
        public string Model { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public Person Person { get; set; }
        public int? PersonId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DeviceHistory> Histories { get; set; }
    }
}
