using ReadingDiary.Application.Interfaces;
using ReadingDiary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Services
{

    /// <summary>
    /// Provides operations for rating books,
    /// including creating, updating and querying user ratings.
    /// </summary>
    public class RatingService : IRatingService
    {
        private readonly IBookRatingRepository _ratingRepository;
        private readonly IBookRepository _bookRepository;

        public RatingService(
            IBookRatingRepository ratingRepository,
            IBookRepository bookRepository)
        {
            _ratingRepository = ratingRepository;
            _bookRepository = bookRepository;
        }

        public async Task RateAsync(int bookId, string userId, int value)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId must not be empty.", nameof(userId));

            if (value < 1 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value), "Rating value must be between 1 and 5.");

            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
                throw new InvalidOperationException("Book does not exist.");

            var existing = await _ratingRepository.GetUserRatingAsync(bookId, userId);

            if (existing != null)
            {
                existing.RatingValue = value;
                await _ratingRepository.UpdateAsync(existing);
            }
            else
            {
                var rating = new BookRating
                {
                    BookId = bookId,
                    UserId = userId,
                    RatingValue = value,
                    CreatedAt = DateTime.UtcNow
                };

                await _ratingRepository.AddAsync(rating);
            }
        }

        public async Task<int?> GetUserRatingAsync(int bookId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            var rating = await _ratingRepository.GetUserRatingAsync(bookId, userId);
            return rating?.RatingValue;
        }

        public async Task<double> GetAverageRatingAsync(int bookId)
        {
            var ratings = await _ratingRepository.GetRatingsForBookAsync(bookId);
            var list = ratings.ToList();

            if (!list.Any())
                return 0;

            return list.Average(r => r.RatingValue);
        }

        public async Task DeleteRatingsForBookAsync(int bookId)
        {
            await _ratingRepository.DeleteByBookIdAsync(bookId);
        }

    }
}
