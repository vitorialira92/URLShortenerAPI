using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using URLShortenerAPI.DTO;
using URLShortenerAPI.Services;

namespace URLShortenerAPI.Controllers
{
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlService _service;

        public UrlController(UrlService service)
        {
            _service = service;
        }

        [HttpGet("{shortUrl}")]
        public IActionResult RedirectToOriginalUrl(string shortUrl)
        {
            var url = _service.GetOriginalByShort(shortUrl);

            return Redirect(url);
        }

        [HttpPost("{originalUrl}")]
        public IActionResult ShortUrl(string originalUrl)
        {
            var url = _service.ShortUrl(originalUrl);

            UrlShortenResponse response = new UrlShortenResponse
            {
                Id = url.Id,
                ShortenedUrl = url.ShortenedURL,
                ValidityDurationInSeconds = 120
            };

            return Ok(response);
        }
    }
}
