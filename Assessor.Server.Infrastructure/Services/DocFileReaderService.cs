using System.Net;
using System.Text;
using Assessor.Server.Application.Common.Interfaces;
using Assessor.Server.Domain.Extensions;
using Assessor.Server.Domain.Models;
using Microsoft.AspNetCore.Http;
using NPOI.XWPF.UserModel;

namespace Assessor.Server.Infrastructure.Services;

public class DocFileReaderService : IFileReaderService
{
    public async Task<Result<string, Error>> ReadTextFromFile(IFormFile file)
    {
        if (!file.IsDocFile())
        {
            return new Error(HttpStatusCode.BadRequest, "Invalid file extension");
        }
        
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var stringBuilder = new StringBuilder();
        using var document = new XWPFDocument(memoryStream);
        
        foreach (var paragraph in document.Paragraphs)
        {
            stringBuilder.AppendLine(paragraph.Text);
        }

        return stringBuilder.ToString();
    }
}