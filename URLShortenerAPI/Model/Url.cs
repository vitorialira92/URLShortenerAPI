using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace URLShortenerAPI.Model
{
    public class Url
    {
        public int Id { get; set; }
        public string OriginalURL { get; set; }
        public string ShortenedURL { get; set; }
        public DateTime ExpiresWhen { get; set; }
    }
}
