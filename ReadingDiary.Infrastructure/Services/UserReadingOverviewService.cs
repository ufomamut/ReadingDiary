using Microsoft.EntityFrameworkCore;
using ReadingDiary.Application.DTOs.Reading;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Domain.Enums;
using ReadingDiary.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Infrastructure.Services
{
    public class UserReadingOverviewService : IUserReadingOverviewService
    {
        private readonly ApplicationDbContext _context;

        public UserReadingOverviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserReadingOverviewDto> GetOverviewAsync(string userId)
        {
            // 1️ Načteme všechny deníky uživatele + související knihy
            var items = await _context.Diaries
                .AsNoTracking()
                .Where(d => d.UserId == userId)
                .Select(d => new MyBookItemDto
                {
                    BookId = d.Book.Id,
                    Title = d.Book.Title,
                    Author = d.Book.Author,
                    CoverImagePath = d.Book.CoverImagePath,
                    Status = d.Status,
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt
                })
                .ToListAsync();

            // 2️ Pomocná funkce pro řazení
            static IEnumerable<MyBookItemDto> Order(IEnumerable<MyBookItemDto> source) =>
                source.OrderByDescending(i => i.UpdatedAt ?? i.CreatedAt);

            // 3️ Rozdělení do sekcí podle stavu
            return new UserReadingOverviewDto
            {
                ToRead = Order(items.Where(i => i.Status == ReadingDiaryState.ToRead)).ToList(),
                Reading = Order(items.Where(i => i.Status == ReadingDiaryState.Reading)).ToList(),
                Postponed = Order(items.Where(i => i.Status == ReadingDiaryState.Postponed)).ToList(),
                Finished = Order(items.Where(i => i.Status == ReadingDiaryState.Finished)).ToList()
            };
        }
    }
}
