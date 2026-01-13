using ReadingDiary.Domain.Entities;

namespace ReadingDiary.Web.Models.ViewModels
{

    /// <summary>
    /// View model for the public book catalog page.
    /// Combines paged book results with filtering,
    /// sorting and genre selection state.
    /// </summary>
    public class BooksFeedViewModel
    {
        public List<BookListItemViewModel> Books { get; set; } = new();
        public int? SelectedGenreId { get; set; }
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<Genre> Genres { get; set; } = new();
        public bool NoResults { get; set; }
    }
}
