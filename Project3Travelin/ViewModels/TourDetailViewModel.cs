using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Dtos.TourDtos;

namespace Project3Travelin.ViewModels
{
    public class TourDetailViewModel
    {
        public GetTourByIdDto Tour { get; set; }
        public List<ResultCommentListByTourIdDto> Comments { get; set; }
    }
}
