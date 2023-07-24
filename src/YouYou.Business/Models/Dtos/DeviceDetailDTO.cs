namespace YouYou.Business.Models.Dtos
{
    public class DeviceDetailDTO
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public int? PersonId { get; set; }
        public IEnumerable<DeviceHistoryDetailDTO> History { get; set; }
    }
}
