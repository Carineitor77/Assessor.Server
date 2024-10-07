using System.Net;
using Assessor.Server.Domain.Constants;
using Assessor.Server.Domain.Models;
using Newtonsoft.Json;

namespace Assessor.Server.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var error = new Error(HttpStatusCode.InternalServerError, ex.Message);

            var serializedError = JsonConvert.SerializeObject(error);

            context.Response.ContentType = MediaTypeConstants.JsonMediaType;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(serializedError);
        }
    }
}