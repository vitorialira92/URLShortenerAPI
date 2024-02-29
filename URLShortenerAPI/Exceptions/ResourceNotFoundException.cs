namespace URLShortenerAPI.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string resourceName, int statusCode) : base($"{resourceName} não encontrado.")
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }

    }
}
