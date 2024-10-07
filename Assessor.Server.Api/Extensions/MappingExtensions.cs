using Assessor.Server.Api.DTOs;
using Assessor.Server.Application.Lab.Commands.MarkLab;

namespace Assessor.Server.Api.Extensions;

public static class MappingExtensions
{
    public static MarkLabCommand MapToMarkLabCommand(this LabInfoDto labInfoDto)
    {
        var markLabCommand = new MarkLabCommand(
            LabFile: labInfoDto.LabFile
        );

        return markLabCommand;
    }
}