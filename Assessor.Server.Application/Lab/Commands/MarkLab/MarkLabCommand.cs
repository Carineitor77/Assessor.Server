using Assessor.Server.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assessor.Server.Application.Lab.Commands.MarkLab;

public record MarkLabCommand(
    IFormFile LabFile
) : IRequest<Result<Content, Error>>;