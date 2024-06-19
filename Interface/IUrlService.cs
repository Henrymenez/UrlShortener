using UrlShortener.Dtos.Request;
using UrlShortener.Dtos.Response;

namespace UrlShortener.Interface
{
    public interface IUrlService
    {
        Task<UrlResponse> ShortenUrl(UrlRequest request);
        Task<UrlResponse> GetActualUrl(string request);
    }
}
