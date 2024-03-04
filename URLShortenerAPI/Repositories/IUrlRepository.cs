using URLShortenerAPI.Model;

namespace URLShortenerAPI.Repositories
{
    public interface IUrlRepository
    {
        void Delete(Url url);
        Url GetOriginalByShort(string v);
        Url GetUrlByOriginal(string url);
        bool IsShortUrlAvailable(string v);
        Url Save(Url url);
    }
}
