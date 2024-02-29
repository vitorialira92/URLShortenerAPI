using System.ComponentModel.DataAnnotations;

namespace URLShortenerAPI.DTO
{
    public class UrlShortenRequest
    {
        [Required]
        public string Url { get; set; }
    }
}
