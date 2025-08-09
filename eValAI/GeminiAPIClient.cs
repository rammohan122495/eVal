using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eValAI
{
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string ApiUrlBase =
            "https://generativelanguage.googleapis.com/v1beta/models/";

        public GeminiApiClient(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateContentAsync(string model, object requestBody)
        {
            string apiUrl = $"{ApiUrlBase}{model}:generateContent?key={_apiKey}";
            string jsonBody = JsonSerializer.Serialize(requestBody);
            StringContent content = new StringContent(
                jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return ExtractTextFromGeminiResponse(jsonResponse);
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                return $"Error: {response.StatusCode} - {response.ReasonPhrase}\n" +
                       $"{errorContent}";
            }
        }

        public string ExtractTextFromGeminiResponse(string jsonResponse)
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
            {
                if (doc.RootElement.TryGetProperty("candidates", out JsonElement candidates) &&
                    candidates.EnumerateArray().Any())
                {
                    JsonElement firstCandidate = candidates.EnumerateArray().First();
                    if (firstCandidate.TryGetProperty("content", out JsonElement content) &&
                        content.TryGetProperty("parts", out JsonElement parts) &&
                        parts.EnumerateArray().Any())
                    {
                        JsonElement firstPart = parts.EnumerateArray().First();
                        if (firstPart.TryGetProperty("text", out JsonElement textElement))
                        {
                            return textElement.GetString() ?? string.Empty;
                        }
                    }
                }
            }
            return "No text found in Gemini response.";
        }
    }
}
