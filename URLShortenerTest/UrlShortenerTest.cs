namespace URLShortenerTest
{
    public class UrlShortenerTest
    {
        [Theory]
        [MemberData(nameof(UrlsList))]
        public void must_return_a_shortened_url(string url)
        {

        }

        public static IEnumerable<object[]> UrlsList()
        {
            return new[]
            {
                new object []{ @"https://www.youtube.com/watch?v=zV1qLYukTH8" },
                new object []{ @"https://www.youtube.com/watch?v=zV1qLYukTH8" },
                new object []{ @"https://www.youtube.com/watch?v=zV1qLYukTH8" },
                new object []{ @"https://www.youtube.com/watch?v=zV1qLYukTH8" },
                new object []{ @"https://www.youtube.com/watch?v=zV1qLYukTH8" },
                new object []{ @"https://www.youtube.com/watch?v=zV1qLYukTH8" },
                new object []{ @"https://www.youtube.com/watch?v=zV1qLYukTH8" },
            };
        }
    }
}