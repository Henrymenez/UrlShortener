using Microsoft.AspNetCore.Mvc;
using UrlShortener.Dtos.Request;
using UrlShortener.Interface;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlService urlService;

        public UrlShortenerController(IUrlService urlService)
        {
            this.urlService = urlService;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlRequest request)
        {

            var result = await urlService.ShortenUrl(request);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> ShowUrl(string shortUrl)
        {
            var result = await urlService.GetActualUrl(shortUrl);
            if (result.Status)
            {
                return Ok(result);
            }
            return NotFound(result);

        }


    }
}
