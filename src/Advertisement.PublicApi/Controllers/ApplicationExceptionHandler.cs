using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Advertisement.PublicApi.Controllers
{
    public class ApplicationExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationExceptionOptions _options;
        
        public ApplicationExceptionHandler(RequestDelegate next,
            IOptions<ApplicationExceptionOptions> options)
        {
            _next = next;
            _options = options.Value;
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = _options.DefaultStatusCode;
                await context.Response.WriteAsync("Во врем выполнения приложения произошла ошибка");
            }
        }
    }

    public class ApplicationExceptionOptions
    {
        public int DefaultStatusCode { get; set; } = StatusCodes.Status500InternalServerError;
    }
}