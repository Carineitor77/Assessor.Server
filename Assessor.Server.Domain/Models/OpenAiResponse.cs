namespace Assessor.Server.Domain.Models;

public class OpenAiResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public long Created { get; set; }
    public string Model { get; set; }
    public List<Choice> Choices { get; set; }
    public Usage Usage { get; set; }
    public string SystemFingerprint { get; set; }
}
