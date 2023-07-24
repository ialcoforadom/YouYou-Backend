namespace YouYou.Business.Models.Pagination
{
    public class PersonFilter : PaginationFilterBase
    {
        public string? General { get; set; }
        public Guid? RoleId { get; set; }
    }
}
