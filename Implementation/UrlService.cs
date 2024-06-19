using Microsoft.EntityFrameworkCore;
using System;
using UrlShortener.Data;
using UrlShortener.Dtos.Request;
using UrlShortener.Dtos.Response;
using UrlShortener.Interface;
using UrlShortener.Models;
using UrlShortener.Utilities;

namespace UrlShortener.Implementation
{
    public class UrlService : IUrlService
    {
        private readonly UrlShortenerContext _dbContext;

        public UrlService(UrlShortenerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UrlResponse> GetActualUrl(string url)
        {
            var urlMapping = await _dbContext.UrlMappings.FirstOrDefaultAsync(u => u.ShortUrl == url);

            if (urlMapping == null)
            {
                return new UrlResponse
                {
                    Message = "Short Url does not exist",
                    Status = false
                };
            }
            return new UrlResponse
            {
                Url = urlMapping.OriginalUrl,
                Message = "Successful",
                Status = true
            };
        }

        public async  Task<UrlResponse> ShortenUrl(UrlRequest request)
        {
            try
            {
                if (!Utils.IsValidUrl(request.Url))
                {
                    return new UrlResponse
                    {
                        Status = false,
                        Message = "Invalid Url"
                    };
                }

                var shortUrl = Utils.GenerateShortUrl(30);

                var urlMapping = new UrlMapping
                {
                    OriginalUrl = request.Url,
                    ShortUrl = shortUrl
                };

                _dbContext.UrlMappings.Add(urlMapping);
                await _dbContext.SaveChangesAsync();
                return new UrlResponse
                {
                    Status = true,
                    Message = "Successful",
                    Url = shortUrl
                };
            }
            catch (Exception ex)
            {
                return new UrlResponse
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }
    }


}
