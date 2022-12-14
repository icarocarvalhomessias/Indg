using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Trader.Api.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        //private readonly ILogger _logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate Next)
        {
            try
            {
                await Next(context);

            }
            catch(Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    //Detail = "An internal server error has ocurred"
                    Detail  = ex.Message
                };

                var json = JsonSerializer.Serialize(problem);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);

            }
        }
    }
}
