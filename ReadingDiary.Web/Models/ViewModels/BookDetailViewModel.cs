using Microsoft.AspNetCore.Http.HttpResults;
using ReadingDiary.Domain.Entities;
using ReadingDiary.Domain.Enums;

namespace ReadingDiary.Web.Models.ViewModels
{
    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public string? Description { get; set; }
        public string? Review { get; set; }
        public string? CoverImagePath { get; set; }
        public List<string> Genres { get; set; } = new();
        public string? Isbn { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? CreatedAtFormatted => CreatedAt?.ToLocalTime().ToString("d.M.yyyy HH:mm");
        public string? UpdatedAtFormatted => UpdatedAt?.ToLocalTime().ToString("d.M.yyyy HH:mm");

        public double AverageRating { get; set; }
        public int RatingsCount { get; set; }

        public int? DiaryId { get; set; }
        public ReadingDiaryState? DiaryStatus { get; set; }
        public string? DiaryNotes { get; set; }

        public DateTime? DiaryCreatedAt { get; set; }
        public DateTime? DiaryUpdatedAt { get; set; }

        public string DiaryUpdatedAtFormatted => DiaryUpdatedAt.HasValue ? DiaryUpdatedAt.Value.ToLocalTime().ToString("d.M.yyyy HH:mm") : "Zatím neupraveno";
        public bool HasDiary => DiaryId.HasValue;
    }
}
