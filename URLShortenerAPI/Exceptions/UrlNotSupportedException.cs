namespace URLShortenerAPI.Exceptions
{
    public class UrlNotSupportedException : Exception
    {
        public UrlNotSupportedException(int statusCode) : base($"Url not supported.")
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}
