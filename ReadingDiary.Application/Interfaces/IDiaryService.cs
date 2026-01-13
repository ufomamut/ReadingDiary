using ReadingDiary.Application.DTOs.Diary;
using ReadingDiary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IDiaryService
    {
        Task<DiaryDto?> GetDiaryForBookAsync(int bookId, string userId);
        Task SaveDiaryAsync(DiaryDto dto);
        Task<IEnumerable<DiaryDto>> GetDiariesForUserAsync(string userId);
        Task DeleteDiaryAsync(int diaryId, string userId);
        Task ChangeStatusAsync(int diaryId, ReadingDiaryState newStatus, string userId);
    }
}
