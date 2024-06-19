using Microsoft.AspNetCore.Mvc;
using UrlShortener.Dtos.Request;
using UrlShortener.Dtos.Response;
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
        [ProducesResponseType(typeof(UrlResponse), 200)]
        [ProducesResponseType(400)]
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
        [ProducesResponseType(typeof(UrlResponse), 200)]
        [ProducesResponseType(404)]
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
