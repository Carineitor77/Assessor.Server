namespace Assessor.Server.Domain.Options;

public class OpenAiOptions
{
    public string Model { get; set; }
    public string ApiKey { get; set; }
    public double Temperature { get; set; }
    public int MaxTokens { get; set; }
    public double TopP { get; set; }
    public int? FrequencyPenalty { get; set; }
    public int? PresencePenalty { get; set; }
    public string Schema { get; set; }
}