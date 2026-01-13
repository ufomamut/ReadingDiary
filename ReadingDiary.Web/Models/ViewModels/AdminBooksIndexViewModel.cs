namespace ReadingDiary.Web.Models.ViewModels
{

    /// <summary>
    /// View model for the admin books overview page.
    /// Contains a paged list of books and related filtering state.
    /// </summary>
    public class AdminBooksIndexViewModel
    {
        public List<BookListItemViewModel> Books { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Search { get; set; }
        public string? Sort { get; set; }
    }
}
