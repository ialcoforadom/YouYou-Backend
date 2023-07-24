namespace YouYou.Business.Models.Pagination
{
    public class DeviceFilter : PaginationFilterBase
    {
        public bool? Status { get; set; }
        public int? PersonId { get; set; }
        public string? General { get; set; }
    }
}
