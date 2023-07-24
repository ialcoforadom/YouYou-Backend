namespace YouYou.Api.ViewModels
{
    public class ListDeviceViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string PersonName { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
