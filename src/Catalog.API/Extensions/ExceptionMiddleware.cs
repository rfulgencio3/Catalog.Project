using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.API.Extensions
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private static ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (ExceptionBusinessLog ex)
            {
                HandleExceptionBusinessLog(httpContext, ex);
            }
            catch (Exception ex)
            {
                HandleException(httpContext, ex);
            }

        }

        private static void HandleException(HttpContext context, Exception exception)
        {
            CreateResponse(context, exception);
        }

        private static void HandleExceptionBusinessLog(HttpContext context, ExceptionBusinessLog exceptionBusinessLog)
        {
            CreateResponse(context, exceptionBusinessLog);
        }

        private async static void CreateResponse(HttpContext context, object value)
        {
            try
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json; charset=utf-8";
                string jsonString = JsonConvert.SerializeObject(value);
                var response = JsonConvert.SerializeObject(Controllers.ControllerBaseCustom.CreateErrorResponse(jsonString, context.Response.StatusCode));
                await context.Response.WriteAsync(response, Encoding.UTF8);
                Log.Error(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
            }
        }

    }
}