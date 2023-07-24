using YouYou.Business.Interfaces;
using YouYou.Business.Models.Pagination;
using Microsoft.AspNetCore.WebUtilities;

namespace YouYou.Business.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(PaginationFilterBase filter, string route)
        {
            var _endpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(
                _endpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());

            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
