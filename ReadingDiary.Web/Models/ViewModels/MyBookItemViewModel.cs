namespace ReadingDiary.Web.Models.ViewModels
{
    public class MyBookItemViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string? CoverImagePath { get; set; }

        public string StatusText { get; set; } = string.Empty;

        public string UpdatedAtFormatted { get; set; } = string.Empty;
    }
}
