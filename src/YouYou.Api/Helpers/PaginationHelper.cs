using YouYou.Business.Interfaces;
using YouYou.Business.Models.Pagination;

namespace YouYou.Api.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilterBase validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilterBase(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilterBase(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilterBase(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilterBase(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
