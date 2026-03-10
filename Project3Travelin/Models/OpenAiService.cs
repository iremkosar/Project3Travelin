namespace Project3Travelin.Models
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private const string OpenAiUrl = "https://api.openai.com/v1/chat/completions";
        private const string apiKey = "";
        public OpenAiService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }
    }
}
