using ReadingDiary.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReadingDiary.Web.Models.ViewModels
{
    public class DiaryViewModel
    {
        public int? Id { get; set; }
        public int BookId { get; set; }
        public string DiaryNotes { get; set; } = string.Empty;
        public ReadingDiaryState Status { get; set; }
    }
}
