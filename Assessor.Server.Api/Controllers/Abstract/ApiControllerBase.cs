using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assessor.Server.Api.Controllers.Abstract;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMediator Mediator;

    protected ApiControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }
}