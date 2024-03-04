using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using URLShortenerAPI.Exceptions;
using URLShortenerAPI.DTO;

namespace URLShortenerAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ResourceNotFoundException ex)
            {
                var errorResult = new ErrorResponse
                {
                    Title = "Url Shortener System Error",
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                };

                context.Result = new JsonResult(errorResult)
                {
                    StatusCode = errorResult.StatusCode
                };
            }
            else if (context.Exception is UrlNotSupportedException exc)
            {
                var errorResult = new ErrorResponse
                {
                    Title = "Url Shortener System Error: url operation error",
                    Message = exc.Message,
                    StatusCode = exc.StatusCode
                };

                context.Result = new JsonResult(errorResult)
                {
                    StatusCode = errorResult.StatusCode
                };
            }
            else
            {
                var errorResult = new ErrorResponse
                {
                    Title = "Url Shortener  System Error",
                    Message = "Internal server error",
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.Result = new JsonResult(errorResult)
                {
                    StatusCode = errorResult.StatusCode
                };
            }

        }

    }

}
