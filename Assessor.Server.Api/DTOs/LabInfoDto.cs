using Microsoft.AspNetCore.Mvc;

namespace Assessor.Server.Api.DTOs;

public record LabInfoDto(
    [FromForm] IFormFile LabFile
);