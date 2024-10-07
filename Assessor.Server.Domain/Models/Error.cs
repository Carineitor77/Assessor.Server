using System.Net;

namespace Assessor.Server.Domain.Models;

public record Error(HttpStatusCode StatusCode, string Message = default);