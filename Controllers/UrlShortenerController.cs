using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly UrlShortenerContext _context;

        public UrlShortenerController(UrlShortenerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return BadRequest("Invalid URL.");
            }

            var shortUrl = GenerateShortUrl();
            var urlMapping = new UrlMapping { OriginalUrl = originalUrl, ShortUrl = shortUrl };

            //   _context.UrlMappings.Add(urlMapping);
            // await _context.SaveChangesAsync();

            return Ok(new { ShortUrl = shortUrl });
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectToUrl(string shortUrl)
        {
            var urlMapping = new
            {
                OriginalUrl = ""
            };
            // await _context.UrlMappings
            // .FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);

            if (urlMapping == null)
            {
                return NotFound();
            }

            return Redirect(urlMapping.OriginalUrl);
        }

        private string GenerateShortUrl()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
