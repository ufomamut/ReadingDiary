namespace ReadingDiary.Web.Models.ViewModels
{
    public class RatingViewModel
    {
        public int BookId { get; set; }
        public double AverageRating { get; set; }
        public int RatingsCount { get; set; }
        public int? UserRating { get; set; }
        public bool IsReadOnly { get; set; } = false;
    }
}
