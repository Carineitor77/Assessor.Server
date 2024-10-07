namespace Assessor.Server.Domain.Extensions;

public static class StringExtensions
{
    public static string RemoveJsonBlockMarkers(this string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return default;
        }

        var result = content
            .Replace("```json", string.Empty)
            .Replace("```", string.Empty)
            .Trim();

        return result;
    }
}