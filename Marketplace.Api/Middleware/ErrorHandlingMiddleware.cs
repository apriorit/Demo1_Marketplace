using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Marketplace.Api.Helpers;
using Marketplace.Infrastructure.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Marketplace.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private readonly ILogger _logger;
        private static readonly ActionDescriptor _emptyActionDescriptor = new ActionDescriptor();

        public ErrorHandlingMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _executor = executor;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BaseAppException ex)
            {
                _logger.LogInformation(ex, ex.Message);

                var result = new ObjectResult(new ErrorResponse(ex.Message, ex.ErrorData))
                {
                    StatusCode = (int)ex.StatusCode
                };
                await SendErrorResponse(httpContext, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unhandled exception has occurred while executing the request. Url: {httpContext.Request.GetDisplayUrl()}");

                var responseData = new ErrorDataObject(ex.Message, ex.GetType().Name);
                var result = new ObjectResult(new ErrorResponse("Error processing request. Server error.", responseData))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                await SendErrorResponse(httpContext, result);
            }
        }

        private async Task SendErrorResponse(HttpContext context, ObjectResult result)
        {
            var routeData = context.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(context, routeData, _emptyActionDescriptor);
            await _executor.ExecuteAsync(actionContext, result);
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
