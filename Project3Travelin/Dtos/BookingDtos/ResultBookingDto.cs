namespace Project3Travelin.Dtos.BookingDtos
{
    public class ResultBookingDto
    {      
            public string BookingId { get; set; }
            public string TourId { get; set; }
            public string TourTitle { get; set; }
            public string TourImageUrl { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int PersonCount { get; set; }
            public decimal TotalPrice { get; set; }
         
            public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        }
    }
