using System.ComponentModel.DataAnnotations;

namespace URLShortenerAPI.DTO
{
    public class UrlShortenResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ShortenedUrl { get; set; }
        [Required]
        public int ValidityDurationInSeconds { get; set; }
    }
}
