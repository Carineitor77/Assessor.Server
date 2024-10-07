using Assessor.Server.Domain.Constants;
using Assessor.Server.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Assessor.Server.Domain.Extensions;

public static class FormFileExtensions
{
    public static bool IsDocFile(this IFormFile? file)
    {
        if (file is null || string.IsNullOrEmpty(file.FileName))
        {
            return false;
        }

        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        var isDocFile = fileExtension is FileExtensionConstants.Doc or FileExtensionConstants.Docx;

        return isDocFile;
    }
    
    public static bool IsPdfFile(this IFormFile? file)
    {
        if (file is null || string.IsNullOrEmpty(file.FileName))
        {
            return false;
        }

        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        var isPdfFile = fileExtension is FileExtensionConstants.Pdf;

        return isPdfFile;
    }

    public static FileReaderTypes GetFileReaderType(this IFormFile? file)
    {
        if (file.IsDocFile())
        {
            return FileReaderTypes.Doc;
        }

        if (file.IsPdfFile())
        {
            return FileReaderTypes.Pdf;
        }

        return FileReaderTypes.Unknown;
    }
}