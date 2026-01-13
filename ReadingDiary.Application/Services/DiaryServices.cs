using ReadingDiary.Application.DTOs.Diary;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Domain.Entities;
using ReadingDiary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Services
{

    /// <summary>
    /// Provides operations for managing user reading diaries,
    /// including creation, update, deletion and status changes.
    /// </summary>
    public class DiaryService : IDiaryService
    {
        private readonly IDiaryRepository _diaryRepository;

        public DiaryService(IDiaryRepository diaryRepository)
        {
            _diaryRepository = diaryRepository;
        }

        public async Task<DiaryDto?> GetDiaryForBookAsync(int bookId, string userId)
        {
            var diary = await _diaryRepository.GetByBookAndUserAsync(bookId, userId);

            if (diary == null)
                return null;

            return new DiaryDto
            {
                Id = diary.Id,
                BookId = diary.BookId,
                UserId = diary.UserId,
                DiaryNotes = diary.DiaryNotes,
                Status = diary.Status,
                CreatedAt = diary.CreatedAt,
                UpdatedAt = diary.UpdatedAt,
                FinishedAt = diary.FinishedAt
            };
        }

        public async Task SaveDiaryAsync(DiaryDto dto)
        {
            Diary diary;

            if (dto.Id.HasValue)
            {
                diary = await _diaryRepository.GetByIdAsync(dto.Id.Value)
                    ?? throw new Exception("Diary entry not found.");

                diary.DiaryNotes = dto.DiaryNotes;
                diary.Status = dto.Status;
                diary.UpdatedAt = DateTime.UtcNow;

                if (dto.Status == ReadingDiaryState.Finished)
                    diary.FinishedAt = DateTime.UtcNow;

                await _diaryRepository.UpdateAsync(diary);
            }
            else
            {
                diary = new Diary
                {
                    BookId = dto.BookId,
                    UserId = dto.UserId,
                    DiaryNotes = dto.DiaryNotes,
                    Status = dto.Status,
                    CreatedAt = DateTime.UtcNow,
                    FinishedAt = dto.Status == ReadingDiaryState.Finished
                        ? DateTime.UtcNow
                        : null
                };

                await _diaryRepository.AddAsync(diary);
            }
        }

        public async Task<IEnumerable<DiaryDto>> GetDiariesForUserAsync(string userId)
        {
            var diaries = await _diaryRepository.GetAllByUserAsync(userId);

            return diaries.Select(d => new DiaryDto
            {
                Id = d.Id,
                BookId = d.BookId,
                UserId = d.UserId,
                DiaryNotes = d.DiaryNotes,
                Status = d.Status,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt,
                FinishedAt = d.FinishedAt
            });
        }

        public async Task DeleteDiaryAsync(int diaryId, string userId)
        {
            var diary = await _diaryRepository.GetByIdAsync(diaryId);

            if (diary == null)
                throw new Exception("Diary not found.");

            // User is allowed to delete only their own diary entries
            if (diary.UserId != userId)
                throw new UnauthorizedAccessException("You cannot delete another user's diary.");

            await _diaryRepository.DeleteAsync(diaryId);
        }

        public async Task ChangeStatusAsync(int diaryId, ReadingDiaryState newStatus, string userId)
        {
            var diary = await _diaryRepository.GetByIdAsync(diaryId);

            if (diary == null)
                throw new InvalidOperationException("Diary not found.");

            if (diary.UserId != userId)
                throw new UnauthorizedAccessException();

            diary.Status = newStatus;
            diary.UpdatedAt = DateTime.UtcNow;

            if (newStatus == ReadingDiaryState.Finished)
            {
                diary.FinishedAt = DateTime.UtcNow;
            }

            await _diaryRepository.UpdateAsync(diary);
        }

    }
}
