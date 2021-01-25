using System;
using System.Net;
using System.Threading.Tasks;
using Advertisement.Domain.Shared.Exceptions;
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
            catch (DomainException domainException)
            {
                context.Response.StatusCode = (int)ObtainStatusCode(domainException);
                await context.Response.WriteAsJsonAsync(new
                {
                    TraceId = context.TraceIdentifier,
                    Error = domainException.Message,
                }, context.RequestAborted);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = _options.DefaultErrorStatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    TraceId = context.TraceIdentifier,
                    Error = "Произошла ошибка"
                }, context.RequestAborted);
            }
        }

        private HttpStatusCode ObtainStatusCode(DomainException domainException)
        {
            return domainException switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ConflictException => HttpStatusCode.Conflict,
                NoRightsException => HttpStatusCode.Forbidden,
                _ => throw new ArgumentOutOfRangeException(nameof(domainException), domainException, null)
            };
        }
    }

    public class ApplicationExceptionOptions
    {
        public int DefaultErrorStatusCode { get; set; } = StatusCodes.Status500InternalServerError;
    }
}