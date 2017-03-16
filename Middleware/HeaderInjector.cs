using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace dotnetapiapp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HeaderInjector
    {
        private readonly RequestDelegate _next;

        public HeaderInjector(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            IHeaderDictionary responseHeaders = httpContext.Response.Headers;
            responseHeaders["x-correlation"] = httpContext.TraceIdentifier;
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HeaderInjectorExtensions
    {
        public static IApplicationBuilder UseHeaderInjector(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderInjector>();
        }
    }
}
