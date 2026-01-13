using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IRatingService
    {
        Task RateAsync(int bookId, string userId, int value);
        Task<int?> GetUserRatingAsync(int bookId, string userId);
        Task<double> GetAverageRatingAsync(int bookId);
        Task DeleteRatingsForBookAsync(int bookId);
    }
}
