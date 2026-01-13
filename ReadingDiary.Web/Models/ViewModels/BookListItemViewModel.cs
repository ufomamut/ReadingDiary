using ReadingDiary.Domain.Entities;

namespace ReadingDiary.Web.Models.ViewModels
{
    public class BookListItemViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int Year { get; set; } 

        public DateTime? CreatedAt { get; set; }

        public string? CreatedAtFormatted => CreatedAt?.ToLocalTime().ToString("d.M.yyyy - HH:mm");

        public double AverageRating { get; set; }

        public string? CoverImagePath { get; set; }

        public List<string> Genres { get; set; } = new();

        public string? Isbn { get; set; }

        public int RatingsCount { get; set; }

        public int? UserRating { get; set; }
    }
}
