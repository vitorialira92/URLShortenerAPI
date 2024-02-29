using URLShortenerAPI.Repositories;

namespace URLShortenerAPI.Services
{
    public class UrlService
    {
        public UrlRepository _repository;
        public UrlService(UrlRepository repository) { _repository = repository; }
    }
}
