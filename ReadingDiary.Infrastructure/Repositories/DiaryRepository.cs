using Microsoft.EntityFrameworkCore;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Domain.Entities;
using ReadingDiary.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Infrastructure.Repositories
{
    public class DiaryRepository : Repository<Diary>, IDiaryRepository
    {
        public DiaryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<Diary?> GetByBookAndUserAsync(int bookId, string userId)
        {
            return await _context.Diaries
                .Include(d => d.Book)
                .FirstOrDefaultAsync(d => d.BookId == bookId && d.UserId == userId);
        }

        public async Task<IEnumerable<Diary>> GetAllByUserAsync(string userId)
        {
            return await _context.Diaries
                .Include(d => d.Book)
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }
    }
}
