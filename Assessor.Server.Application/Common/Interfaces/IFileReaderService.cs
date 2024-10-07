using Assessor.Server.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Assessor.Server.Application.Common.Interfaces;

public interface IFileReaderService
{
    Task<Result<string, Error>> ReadTextFromFile(IFormFile file);
}