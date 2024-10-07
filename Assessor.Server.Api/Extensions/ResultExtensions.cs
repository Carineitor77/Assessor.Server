using Assessor.Server.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assessor.Server.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult GetActionResult<TValue, TError>(this Result<TValue, TError> result) where TError : Error
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }
            
        var objectResult = new ObjectResult(result.Error.Message)
        {
            StatusCode = (int)result.Error.StatusCode
        };

        return objectResult;
    }
}