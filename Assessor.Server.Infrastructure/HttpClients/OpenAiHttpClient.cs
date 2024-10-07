using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Assessor.Server.Application.Common.Interfaces;
using Assessor.Server.Domain.Constants;
using Assessor.Server.Domain.Extensions;
using Assessor.Server.Domain.Models;
using Assessor.Server.Domain.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Assessor.Server.Infrastructure.HttpClients;

public class OpenAiHttpClient : IAiHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly OpenAiOptions _openAiOptions;

    public OpenAiHttpClient(HttpClient httpClient, IOptions<OpenAiOptions> options)
    {
        _httpClient = httpClient;
        _openAiOptions = options.Value;
    }
    
    public async Task<Result<Content, Error>> GetCompletion(string message)
    {
        var requestUrl = "https://api.openai.com/v1/chat/completions";
        var requestContent = new
        {
            model = _openAiOptions.Model,
            messages = new[]
            {
                // Please provide feedback and a grade for the following student's lab:
                new { role = OpenAiConstants.AssistantRole, content = "Будь ласка, надайте відгук і оцінку за лабораторну роботу наступного студента:" },
                new { role = OpenAiConstants.UserRole, content = message },
                // Please respond with a JSON object that matches this schema:
                new { role = OpenAiConstants.SystemRole, content = $"Використовуй тільки українську мову. У відповідь надішліть об’єкт JSON, який відповідає цій схемі: {_openAiOptions.Schema}" }
            },
            temperature = _openAiOptions.Temperature,
            max_tokens = _openAiOptions.MaxTokens,
            top_p = _openAiOptions.TopP,
            frequency_penalty = _openAiOptions.FrequencyPenalty,
            presence_penalty = _openAiOptions.PresencePenalty
        };

        var jsonContent = JsonConvert.SerializeObject(requestContent);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, MediaTypeConstants.JsonMediaType);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthConstants.BearerHeader, _openAiOptions.ApiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeConstants.JsonMediaType));

        var response = await _httpClient.PostAsync(requestUrl, httpContent);
        
        var responseString = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            return new Error(response.StatusCode, $"Failed to get completion from OpenAI. See Error: {responseString}");
        }

        var result = JsonConvert.DeserializeObject<OpenAiResponse>(responseString);
        
        if (result is null)
        {
            return new Error(HttpStatusCode.InternalServerError, "Failed to deserialize result");
        }
        
        var contentJson = result.Choices.FirstOrDefault()?.Message.Content.RemoveJsonBlockMarkers();

        if (string.IsNullOrWhiteSpace(contentJson))
        {
            return new Error(HttpStatusCode.InternalServerError, "Failed to receive a content");
        }
        
        var content = JsonConvert.DeserializeObject<Content>(contentJson.Trim());

        if (content is null)
        {
            return new Error(HttpStatusCode.InternalServerError, "Failed to deserialize content");
        }
        
        return content;
    }
}