using Assessor.Server.Api.DTOs;
using Assessor.Server.Domain.Extensions;
using FluentValidation;

namespace Assessor.Server.Api.Validators;

public class LabInfoDtoValidator  : AbstractValidator<LabInfoDto>
{
    public LabInfoDtoValidator()
    {
        RuleFor(li => li.LabFile)
            .NotNull().WithMessage("Lab file is required.")
            .Must(file => file.IsDocFile() || file.IsPdfFile())
            .WithMessage("File must be either a .doc, .docx, or .pdf file.");
    }
}