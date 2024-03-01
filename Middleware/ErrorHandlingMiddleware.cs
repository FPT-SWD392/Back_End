using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Middleware
{
    public class ErrorHandlingMiddleware
    {
        public readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }   
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = JsonSerializer.Serialize(new
            {
                error=ex.Message
            });
            context.Response.ContentType= "application/json";
            var header = new KeyValuePair<string, StringValues>("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add(header);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(result);
        }
    }
}
