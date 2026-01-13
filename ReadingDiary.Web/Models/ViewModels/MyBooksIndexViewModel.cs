namespace ReadingDiary.Web.Models.ViewModels
{

    /// <summary>
    /// View model for the user's reading overview page.
    /// Groups user's books by their current reading status.
    /// </summary>
    public class MyBooksIndexViewModel
    {
        public IReadOnlyList<MyBookItemViewModel> ToRead { get; init; } = [];
        public IReadOnlyList<MyBookItemViewModel> Reading { get; init; } = [];
        public IReadOnlyList<MyBookItemViewModel> Postponed { get; init; } = [];
        public IReadOnlyList<MyBookItemViewModel> Finished { get; init; } = [];
    }
}
