namespace Project3Travelin.Models
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private const string OpenAiUrl = "https://api.openai.com/v1/chat/completions";
        private const string apiKey = "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";
        public OpenAiService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }
    }
}
