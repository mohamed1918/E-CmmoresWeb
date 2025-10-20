using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_CmmoresWeb.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly Logger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next, Logger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Somethig Went Wrong");
                await HandleExceptionAsync(httpContext, ex);

            }

        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            //httpContext.Response.ContentType = "application/json";

            var response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            //var responseJson = JsonSerializer.Serialize(response);

            //await httpContext.Response.WriteAsync(responseJson);

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var respose = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point ${httpContext.Request.Path} is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(respose);

            }
        }
    }
}
