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
    public class BookRatingRepository : Repository<BookRating>, IBookRatingRepository
    {

        public BookRatingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<BookRating?> GetUserRatingAsync(int bookId, string userId)
        {
            return await _context.BookRatings
                                 .FirstOrDefaultAsync(r => r.BookId == bookId && r.UserId == userId);
        }

        public async Task<IEnumerable<BookRating>> GetRatingsForBookAsync(int bookId)
        {
            return await _context.BookRatings
                                 .Where(r => r.BookId == bookId)
                                 .ToListAsync();
        }

        public async Task DeleteByBookIdAsync(int bookId)
        {
            var ratings = await _context.BookRatings
                                        .Where(r => r.BookId == bookId)
                                        .ToListAsync();

            if (ratings.Any())
            {
                _context.BookRatings.RemoveRange(ratings);
                await _context.SaveChangesAsync();
            }
        }
    }
}
