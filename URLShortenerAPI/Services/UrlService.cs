using URLShortenerAPI.Model;
using URLShortenerAPI.Repositories;
using URLShortenerAPI.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace URLShortenerAPI.Services
{
    public class UrlService
    {
        public IUrlRepository _repository;
        public UrlService(IUrlRepository repository) { _repository = repository; }

        public Url ShortUrl(string url)
        {
            string binary = GenerateShortUrl(url);
            string generatedUrl = "0" + binary;

            int count = 0;

            while (!_repository.IsShortUrlAvailable(generatedUrl))
            {
                generatedUrl = binary + GetAditionalLetterToAvoidColision(count);
                count++;
            }


            Url newUrl = new Url()
            {
                OriginalURL = url,
                ShortenedURL = generatedUrl,
                ExpiresWhen = DateTime.Now.AddMinutes(2),
            };

            var obj = _repository.Save(newUrl);
            if (obj == null) return null;

            newUrl.Id = obj.Id;

            return newUrl;
        }
        private string GenerateShortUrl(string url)
        {
            string txt = GetValidText(url); 

            int sum = 0;
            foreach(char c in txt)
            {
                sum += (int)c;
            }

            int numberToConvert = SumDigits(sum);

            if (numberToConvert > 63)
                throw new UrlNotSupportedException(405);

            return Convert.ToString(numberToConvert, 2).PadLeft(6, '0');
        }

        private string GetValidText(string url)
        {
            url = url.Replace("https", "").Replace("http", "").Replace("www", "");

            string[] charactersToIgnore = { "!", "#", "$", "%", 
                "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", 
                ":", ";", "=", "?", "@", "[", "]", "_", "~", "^", "`", "{", "}", "|" };
            foreach (string c in charactersToIgnore)
                url = url.Replace(c, "");

            return url.ToLower();
        }

        private int SumDigits(int number)
        {
            int sum = 0;
            while (number > 0)
            {
                int dig = number % 10;
                sum += dig;
                number /= 10;
            }

            return sum;
        }

        private string GetAditionalLetterToAvoidColision(int time)
        {
            string[] letters = {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
                "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
            };

            if (time < 0 || time >= letters.Count())
                throw new UrlNotSupportedException(405);

            return letters[time];
        }

        public string GetOriginalByShort(string shortenedURL)
        {
            Url url = _repository.GetOriginalByShort(shortenedURL);

            if(url == null)
                throw new ResourceNotFoundException("Url", 400);

            if (url.ExpiresWhen < DateTime.Now)
            {
                _repository.Delete(url);
                throw new ResourceNotFoundException("Url", 400);
            }

            return url.OriginalURL;
        }
    }
}
