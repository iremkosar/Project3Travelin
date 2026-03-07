using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Booking> _bookingCollection;

        public BookingService(IMapper mapper, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _bookingCollection = database.GetCollection<Booking>(settings.BookingCollectionName);
            _mapper = mapper;
        }

        public async Task CreateBookingAsync(CreateBookingDto dto)
        {
            var booking = _mapper.Map<Booking>(dto);
            await _bookingCollection.InsertOneAsync(booking);
        }
        public async Task<List<ResultBookingDto>> GetAllBookingsAsync()
        {
            var values = await _bookingCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultBookingDto>>(values);
        }

        public async Task<Booking> GetBookingByIdAsync(string id)
        {
            return await _bookingCollection.Find(x => x.BookingId == id).FirstOrDefaultAsync();
        }

        public async Task UpdateBookingStatusAsync(string id, string status)
        {
            var update = Builders<Booking>.Update.Set(x => x.Status, status);
            await _bookingCollection.UpdateOneAsync(x => x.BookingId == id, update);
        }
        public async Task<List<ResultBookingDto>> GetBookingsByTourIdAsync(string tourId)
        {
            var values = await _bookingCollection
                .Find(x => x.TourId == tourId && x.Status == "Onaylandı")
                .ToListAsync();
            return _mapper.Map<List<ResultBookingDto>>(values);
        }
    }
}
