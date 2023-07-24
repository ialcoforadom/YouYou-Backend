namespace YouYou.Business.Models
{
    public class DeviceHistory : Entity
    {
        public DateTime BindingDate { get; set; }
        public DateTime? UnbindingDate { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public bool Status { get; set; }

        public DeviceHistory() { }
        public DeviceHistory(int deviceId, int personId, Guid userId, bool status)
        {
            BindingDate = DateTime.Now;
            UserId = userId;
            DeviceId = deviceId;
            PersonId = personId;
            Status = status;
        }
    }
}
