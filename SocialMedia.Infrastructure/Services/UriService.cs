using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string baseUri;

        public UriService(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public Uri GetPostPaginationUri(PostQueryFilter filters, string actionUrl)
        {
            string baseUrl = $"{baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
