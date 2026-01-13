using ReadingDiary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IBookRatingRepository : IRepository<BookRating>
    {
        Task<BookRating?> GetUserRatingAsync(int bookId, string userId);
        Task<IEnumerable<BookRating>> GetRatingsForBookAsync(int bookId);
        Task DeleteByBookIdAsync(int bookId);
    }
}
