namespace Project3Travelin.Dtos.BookingDtos
{
    public class UpdateBookingDto
    {
        public string TourId { get; set; }
        public string TourTitle { get; set; }
        public string TourImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int PersonCount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
