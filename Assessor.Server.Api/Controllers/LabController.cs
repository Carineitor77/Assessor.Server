using Assessor.Server.Api.Controllers.Abstract;
using Assessor.Server.Api.DTOs;
using Assessor.Server.Api.Extensions;
using Assessor.Server.Application.Lab.Commands.MarkLab;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assessor.Server.Api.Controllers;

public class LabController : ApiControllerBase
{
    public LabController(IMediator mediator) : base(mediator) { }
    
    [HttpPost]
    public async Task<IActionResult> MarkLab(/*LabInfoDto labInfoDto*/ IFormFile file, CancellationToken cancellationToken)
    {
        //var markLabCommand = labInfoDto.MapToMarkLabCommand();
        var markLabCommand = new MarkLabCommand(file);
        var result = await Mediator.Send(markLabCommand, cancellationToken);
        return result.GetActionResult();
    }
}