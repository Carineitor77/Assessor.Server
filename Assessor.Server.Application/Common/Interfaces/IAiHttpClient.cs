using Assessor.Server.Domain.Models;

namespace Assessor.Server.Application.Common.Interfaces;

public interface IAiHttpClient
{
    Task<Result<Content, Error>> GetCompletion(string message);
}