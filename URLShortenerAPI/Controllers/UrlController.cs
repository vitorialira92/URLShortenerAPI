using Microsoft.AspNetCore.Mvc;
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

            return Ok(url);
        }
    }
}
