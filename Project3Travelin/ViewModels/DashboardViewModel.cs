using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Dtos.TourDtos;

namespace Project3Travelin.ViewModels
{
    public class DashboardViewModel
    {
        public List<ResultTourDto> RecentTours { get; set; }
        public List<ResultBookingDto> RecentBookings { get; set; }
        public List<ResultCommentDto> RecentComments { get; set; }
        public int TotalTours { get; set; }
        public int TotalBookings { get; set; }
        public int TotalComments { get; set; }
        public int PendingBookings { get; set; }
    }
}
