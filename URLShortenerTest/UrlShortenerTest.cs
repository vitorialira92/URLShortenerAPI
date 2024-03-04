using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using URLShortenerAPI.Exceptions;
using URLShortenerAPI.Model;
using URLShortenerAPI.Repositories;
using URLShortenerAPI.Services;

namespace URLShortenerTest
{
    public class UrlShortenerTest
    {
        [Theory]
        [MemberData(nameof(UrlsList))]
        public void must_return_a_shortened_url(string url)
        {
            var mockedRepository = Substitute.For<IUrlRepository>();
            var sut = new UrlService(mockedRepository);

            Url mustBeUrl = new Url { Id = 12 };
            mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(true);

            mockedRepository.Save((Arg.Any<Url>())).Returns(mustBeUrl);

            var shortUrl = sut.ShortUrl(url);

            shortUrl.ShortenedURL.Should().NotBeNull();

            shortUrl.Should().BeOfType<Url>();
        }
        
        
        [Theory]
        [MemberData(nameof(UrlsList))]
        public void short_url_must_be_7_characters_long(string url)
        {
            var mockedRepository = Substitute.For<IUrlRepository>();
            var sut = new UrlService(mockedRepository);

            Url mustBeUrl = new Url
            {
                Id = 12,
            };
            mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(true);

            mockedRepository.Save((Arg.Any<Url>())).Returns(mustBeUrl);

            var shortUrl = sut.ShortUrl(url);

            shortUrl.ShortenedURL.Should().HaveLength(7);

        }
        
        
        [Theory]
        [MemberData(nameof(UrlsList))]
        public void short_url_must_have_only_binary_numbers(string url)
        {
            var mockedRepository = Substitute.For<IUrlRepository>();
            var sut = new UrlService(mockedRepository);

            Url mustBeUrl = new Url
            {
                Id = 12,
            };
            mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(true);

            mockedRepository.Save((Arg.Any<Url>())).Returns(mustBeUrl);

            var shortUrl = sut.ShortUrl(url);

            shortUrl.ShortenedURL.Should().NotContainAll(new List<string>()
            {
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9"
            });

        }
        

        [Theory]
        [MemberData(nameof(UrlAndShortUrl))]
        public void short_url_must_be_correct(string url, string shortUrl)
        {
            var mockedRepository = Substitute.For<IUrlRepository>();
            var sut = new UrlService(mockedRepository);

            Url mustBeUrl = new Url
            {
                Id = 12,
            };
            mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(true);
            mockedRepository.Save((Arg.Any<Url>())).Returns(mustBeUrl);

            var ret = sut.ShortUrl(url);

            ret.ShortenedURL.Should().Be(shortUrl);
        }


        [Theory]
        [MemberData(nameof(UrlAndShortUrl))]
        public void must_return_original_url_by_short_url(string originalUrl, string shortUrl)
        {

            var mockedRepository = Substitute.For<IUrlRepository>();
            var sut = new UrlService(mockedRepository);

            Url mustBeUrl = new Url
            {
                Id = 12,
            };
            mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(true);
            mockedRepository.Save((Arg.Any<Url>())).Returns(mustBeUrl);

            var ret = sut.ShortUrl(originalUrl);

            mockedRepository.GetOriginalByShort((Arg.Any<string>())).Returns(new Url { OriginalURL = originalUrl, 
                ExpiresWhen = DateTime.Today.AddDays(2) }); ;


            sut.GetOriginalByShort(shortUrl).Should().Be(originalUrl);
        }

        [Theory]
        [MemberData(nameof(SameBinaryNumberUrls))]
        public void must_include_a_letter_when_binary_number_already_saved(string url, string expectedShortUrl, bool first)
        {
            var mockedRepository = Substitute.For<IUrlRepository>();
            Url mustBeUrl = new Url
            {
                Id = 145,
            };
            mockedRepository.Save((Arg.Any<Url>())).Returns(mustBeUrl);

            if(first)
                mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(true);
            else
                mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(false, true);

            var sut = new UrlService(mockedRepository);

            var ret = sut.ShortUrl(url);

            ret.ShortenedURL.Should().Be(expectedShortUrl);
        }
        
        
        [Theory]
        [MemberData(nameof(SameUrls))]
        public void must_not_generate_again_when_there_is_a_valid_short_url(string url, string expectedShortUrl)
        {
            var mockedRepository = Substitute.For<IUrlRepository>();
            Url mustBeUrl = new Url
            {
                Id = 145,
            };
            mockedRepository.Save((Arg.Any<Url>())).Returns(mustBeUrl);

            mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(true);

            var sut = new UrlService(mockedRepository);

            var ret = sut.ShortUrl(url);

            mockedRepository.GetUrlByOriginal((Arg.Any<string>())).Returns(ret);

            var secondTry = sut.ShortUrl(url);

            secondTry.ShortenedURL.Should().Be(expectedShortUrl);
        }

        
        [Fact]
        public async Task must_throw_exception_when_time_is_over()
        {
            string url = @"https://www.youtube.com/watch?v=zV1qLYukTH8";
            var mockedRepository = Substitute.For<IUrlRepository>();

            Url mustBeUrl = new Url
            {
                Id = 12,
            };
            mockedRepository.IsShortUrlAvailable((Arg.Any<string>())).Returns(true);
            mockedRepository.Save((Arg.Any<Url>())).Returns(mustBeUrl);
            


            var sut = new UrlService(mockedRepository);

            var ret = sut.ShortUrl(url);

            mockedRepository.GetOriginalByShort((Arg.Any<string>())).Returns(ret);

            await Task.Delay(TimeSpan.FromSeconds(180)); //3 min

            sut.Invoking(x => x.GetOriginalByShort(ret.ShortenedURL))
                   .Should().Throw<ResourceNotFoundException>();

        }



        public static IEnumerable<object[]> UrlAndShortUrl()
        {
            return new[]
            {
                new object []{ @"https://www.netflix.com/browse", "0010100"},
                new object []{ 
                    @"https://www.facebook.com/login/identify/?ctx=recover&ars=facebook_login&from_login_screen=0",
                    "0011100"
                },
                
            };
        }
        
        public static IEnumerable<object[]> UrlsList()
        {
            return new[]
            {
                new object []{ @"https://www.netflix.com/browse" },
                new object []{ @"https://www.youtube.com/watch?v=zV1qLYukTH8" },
            };
        } 
        
        public static IEnumerable<object[]> SameBinaryNumberUrls()
        {
            return new[]
            {
                new object []{ @"https://www.netflix.com/browse", "0010100", true},
                new object []{ @"https://www.netflix.com/broswe", "010100A", false},
            };
        } 
        
        public static IEnumerable<object[]> SameUrls()
        {
            return new[]
            {
                new object []{ @"https://www.figma.com/community/", "0001110"},
            };
        }
    }
}