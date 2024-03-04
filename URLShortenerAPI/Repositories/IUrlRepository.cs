using URLShortenerAPI.Model;

namespace URLShortenerAPI.Repositories
{
    public interface IUrlRepository
    {
        void Delete(Url url);
        Url GetOriginalByShort(string v);
        bool IsShortUrlAvailable(string v);
        Url Save(Url url);
    }
}
