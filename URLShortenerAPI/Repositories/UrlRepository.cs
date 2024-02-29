using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Data;

namespace URLShortenerAPI.Repositories
{
    public class UrlRepository
    {
        private readonly ApiContext _context;
        public UrlRepository(ApiContext context)
        {
            _context = context;
        }
    }
}
