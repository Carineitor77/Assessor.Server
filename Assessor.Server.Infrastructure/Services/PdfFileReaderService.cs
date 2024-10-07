using System.Net;
using System.Text;
using Assessor.Server.Application.Common.Interfaces;
using Assessor.Server.Domain.Extensions;
using Assessor.Server.Domain.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Http;

namespace Assessor.Server.Infrastructure.Services;

public class PdfFileReaderService : IFileReaderService
{
    public async Task<Result<string, Error>> ReadTextFromFile(IFormFile file)
    {
        if (!file.IsPdfFile())
        {
            return new Error(HttpStatusCode.BadRequest, "Invalid file extension");
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var stringBuilder = new StringBuilder();
        using var pdfReader = new PdfReader(memoryStream);
        using var pdfDocument = new PdfDocument(pdfReader);
        
        for (var i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
            var text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
            stringBuilder.Append(text);
        }

        return stringBuilder.ToString();
    }
}