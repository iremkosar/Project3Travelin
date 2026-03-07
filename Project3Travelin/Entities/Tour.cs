using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities
{
    [BsonIgnoreExtraElements]
    public class Tour
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]      
        public string TourId { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DayNight { get; set; }
        public string ImageUrl { get; set; }
        public string SecondImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public decimal Price { get; set; }
        public List<TourStop> Stops { get; set; }
    }
}
