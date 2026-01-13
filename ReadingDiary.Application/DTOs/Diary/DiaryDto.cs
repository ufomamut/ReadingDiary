using ReadingDiary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.DTOs.Diary
{
    public class DiaryDto
    {
        public int? Id { get; set; }
        public string DiaryNotes { get; set; } = string.Empty;
        public ReadingDiaryState Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; } = default!;
    }
}
