using YouYou.Business.Models.Pagination;

namespace YouYou.Business.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilterBase filter, string route);
    }
}
