using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace URLShortenerAPI.Model
{
    public class Url
    {
        [Key]
        public int Id { get; set; }
        public string OriginalURL { get; set; }
        public string ShortenedURL { get; set; }
        public DateTime ExpiresWhen { get; set; }
    }
}
