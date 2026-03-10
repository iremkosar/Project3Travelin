using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;
using System.Text.Json;
using System.Text;

namespace Project3Travelin.Services.TourServices
{
    public class TourService : ITourService
    {
        
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IConfiguration _configuration;

        public TourService(IMapper mapper, IDatabaseSettings _databaseSettings, IConfiguration configuration)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task CreateTourAsync(CreateTourDto createTourDto)
        {
            var values = _mapper.Map<Tour>(createTourDto);
            await _tourCollection.InsertOneAsync(values);
        }

        public async Task DeleteTourAsync(string id)
        {
            await _tourCollection.DeleteOneAsync(x => x.TourId == id);
        }

        public async Task<List<ResultTourDto>> GetAllTourAsync()
        {
            var values = await _tourCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public async Task<GetTourByIdDto> GetTourByIdAsync(string id)
        {
            var value = await _tourCollection.Find(x => x.TourId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTourByIdDto>(value);
        }

        public async Task UpdateTourAsync(UpdateTourDto updateTourDto)
        {
            var values = _mapper.Map<Tour>(updateTourDto);
            await _tourCollection.FindOneAndReplaceAsync(x => x.TourId == updateTourDto.TourId, values);
        }

        public async Task<string> GenerateRouteAsync(string tourName, string city, string country)
        {
            var apiKey = _configuration["OpenAI:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("API key bulunamadı");

            var prompt = $@"Sen bir seyahat uzmanısın. '{tourName}' ({city}, {country}) için 4 günlük detaylı bir gezi rotası oluştur.
            Her gün için:
            - Sabah, öğle ve akşam aktiviteleri
            - Gezilecek yerler ve öneriler
            - Yemek önerileri
            Türkçe olarak yaz. HTML formatında, her gün için <div class='route-day'><h5>Gün X</h5><ul><li>...</li></ul></div> şeklinde döndür.";

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] { new { role = "user", content = prompt } },
                max_tokens = 1500
            };

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"OpenAI hatası: {responseBody}");

            using var doc = JsonDocument.Parse(responseBody);
            var route = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            // ```html ve ``` işaretlerini temizle
            route = route?.Trim();
            if (route != null && route.StartsWith("```html"))
                route = route.Substring(7);
            if (route != null && route.StartsWith("```"))
                route = route.Substring(3);
            if (route != null && route.EndsWith("```"))
                route = route.Substring(0, route.Length - 3);

            return route?.Trim() ?? string.Empty;
        }
    }
}
