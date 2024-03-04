using Microsoft.EntityFrameworkCore;
using System.Net;
using URLShortenerAPI.Data;
using URLShortenerAPI.Model;

namespace URLShortenerAPI.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ApiContext _context;
        public UrlRepository(ApiContext context)
        {
            _context = context;
        }

        public void Delete(Url url)
        {
            _context.Remove(url);
            _context.SaveChanges();

        }

        public Url GetOriginalByShort(string v)
        {
            var original = _context.Urls.FirstOrDefault(x => x.ShortenedURL == v);
            return original;
        }

        public bool IsShortUrlAvailable(string v)
        {
            return _context.Urls.FirstOrDefault(x => x.ShortenedURL == v ) == null;
        }

        public Url Save(Url url)
        {
            url.OriginalURL = WebUtility.UrlDecode(url.OriginalURL);
            _context.Urls.Add(url);
            _context.SaveChanges();

            return url;
        }
    }
}
