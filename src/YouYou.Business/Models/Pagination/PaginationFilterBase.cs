namespace YouYou.Business.Models.Pagination
{
    public class PaginationFilterBase
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PaginationFilterBase()
        {
            this.PageNumber = 1;
            this.PageSize = 3;
        }

        public PaginationFilterBase(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize;
        }
    }
}
