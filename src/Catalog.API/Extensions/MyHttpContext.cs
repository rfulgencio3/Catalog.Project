using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Catalog.API.Extensions
{
    public class MyHttpContext
    {
        private static IHttpContextAccessor m_httpContextAccessor;

        public static HttpContext Current => m_httpContextAccessor.HttpContext;

        public static string AppBaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";

        public static string AppFullUrl()
        {

            var displayUrl = UriHelper.GetDisplayUrl(Current.Request);
            var urlBuilder =
            new UriBuilder(displayUrl)
            {
                Query = null,
                Fragment = null
            };

            return urlBuilder.ToString();

        }

        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            m_httpContextAccessor = contextAccessor;
        }

        public static int StatusCode()
        {
            return Current.Response.StatusCode;
        }

    }

    public static class HttpContextExtensions
    {
        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            MyHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            return app;
        }
    }
}
