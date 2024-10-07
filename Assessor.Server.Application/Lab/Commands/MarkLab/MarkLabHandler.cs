using System.Net;
using Assessor.Server.Application.Common.Interfaces;
using Assessor.Server.Domain.Enums;
using Assessor.Server.Domain.Extensions;
using Assessor.Server.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Assessor.Server.Application.Lab.Commands.MarkLab;

public class MarkLabHandler : IRequestHandler<MarkLabCommand, Result<Content, Error>>
{
    private readonly IAiHttpClient _aiHttpClient;
    private readonly IServiceProvider _serviceProvider;

    public MarkLabHandler(IAiHttpClient aiHttpClient, IServiceProvider serviceProvider)
    {
        _aiHttpClient = aiHttpClient;
        _serviceProvider = serviceProvider;
    }
    
    public async Task<Result<Content, Error>> Handle(MarkLabCommand request, CancellationToken cancellationToken)
    {
        var fileReaderType = request.LabFile.GetFileReaderType();

        if (fileReaderType is FileReaderTypes.Unknown)
        {
            return new Error(HttpStatusCode.BadRequest, "Invalid file type");
        }

        var scope = _serviceProvider.CreateScope();
        var fileReaderService = scope.ServiceProvider.GetRequiredKeyedService<IFileReaderService>(fileReaderType);
        
        var readTextResult = await fileReaderService.ReadTextFromFile(request.LabFile);

        if (!readTextResult.IsSuccess)
        {
            return readTextResult.Error;
        }

        var result = await _aiHttpClient.GetCompletion(readTextResult.Value);
        return result;
    }
}