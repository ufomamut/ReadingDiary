using ReadingDiary.Application.DTOs.Books;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ReadingDiary.Application.Services
{

    /// <summary>
    /// Provides operations for managing books,
    /// including creation, update, deletion and catalog querying.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IBookRatingRepository _ratingRepository;
        private readonly IImageService _imageService;

        public BookService(
            IBookRepository bookRepository,
            IGenreRepository genreRepository,
            IBookRatingRepository ratingRepository,
            IImageService imageService)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _ratingRepository = ratingRepository;
            _imageService = imageService;
        }

        public async Task<int> CreateAsync(BookCreateDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Year = dto.Year,
                Description = dto.Description,
                Review = dto.Review,
                CoverImagePath = dto.CoverImagePath,
                CreatedAt = DateTime.UtcNow,
                Isbn = dto.Isbn
            };
            
            if (dto.GenreIds.Any())
            {
                var allGenres = await _genreRepository.GetAllAsync();
                var selectedGenres = allGenres
                    .Where(g => dto.GenreIds.Contains(g.Id))
                    .ToList();

                foreach (var genre in selectedGenres)
                {
                    book.Genres.Add(genre);
                }
            }
            
            await _bookRepository.AddAsync(book);

            return book.Id;
        }



        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetBookWithGenresAndRatingsAsync(id);

            if (book == null)
                throw new Exception("Book not found.");

            if (!string.IsNullOrEmpty(book.CoverImagePath))
            {
                try
                {
                    await _imageService.DeleteAsync(book.CoverImagePath);
                }
                catch
                {
                    // Image deletion failure should not block book deletion
                }
            }

            if (book.Ratings != null && book.Ratings.Any())
            {
                foreach (var rating in book.Ratings.ToList())
                    await _ratingRepository.DeleteAsync(rating.Id);
            }

            book.Genres.Clear();

            await _bookRepository.DeleteAsync(book.Id);
        }

        public async Task<List<BookListItemDto>> GetAdminListAsync(string? search, string? sort)
        {
            var books = await _bookRepository.GetAllAsync(); 

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.ToLower();
                books = books.Where(b =>
                    b.Title.ToLower().Contains(term) ||
                    b.Author.ToLower().Contains(term));
            }

            books = sort switch
            {
                "title" => books.OrderBy(b => b.Title),
                "author" => books.OrderBy(b => b.Author),
                _ => books.OrderByDescending(b => b.CreatedAt)
            };

            return books.Select(b => new BookListItemDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                CoverImagePath = b.CoverImagePath,
                Isbn = b.Isbn,
                CreatedAt = b.CreatedAt
            }).ToList();
        }


        public async Task<BookDetailDto?> GetDetailAsync(int id)
        {
            var book = await _bookRepository.GetBookWithGenresAndRatingsAsync(id);

            if (book == null)
                return null;

            return new BookDetailDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Description = book.Description,
                Review = book.Review,
                Genres = book.Genres.Select(g => g.Name).ToList(),
                CoverImagePath = book.CoverImagePath,
                AverageRating = CalculateAverageRating(book),
                RatingsCount = book.Ratings?.Count ?? 0,
                Isbn = book.Isbn,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            };
        }

        public async Task<List<BookListItemDto>> GetFilteredAsync(BookFilterDto filter)
        {
            var books = await _bookRepository.GetAllWithGenresAndRatingsAsync();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var term = filter.SearchTerm.ToLower();

                books = books.Where(b =>
                    b.Title.ToLower().Contains(term) ||
                    b.Author.ToLower().Contains(term)
                );
            }

            if (filter.GenreId.HasValue)
            {
                books = books.Where(b =>
                    b.Genres.Any(g => g.Id == filter.GenreId.Value)
                );
            }

            if (filter.RatingMin.HasValue)
            {
                books = books.Where(b =>
                    CalculateAverageRating(b) >= filter.RatingMin.Value
                );
            }

            books = filter.Sort switch
            {
                "title" =>
                    books.OrderBy(b => b.Title),

                "author" =>
                    books.OrderBy(b => b.Author),

                "rating" =>
                    books.OrderByDescending(b => CalculateAverageRating(b)),

                null or "" =>
                    books.OrderByDescending(b => b.CreatedAt),

                _ =>
                    books.OrderByDescending(b => b.CreatedAt)
            };

            return books.Select(book => new BookListItemDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                AverageRating = CalculateAverageRating(book),
                Genres = book.Genres.Select(g => g.Name).ToList(),
                CoverImagePath = book.CoverImagePath,
                RatingsCount = book.Ratings?.Count ?? 0
            }).ToList();
        }



        public async Task UpdateAsync(BookUpdateDto dto)
        {
            var book = await _bookRepository.GetBookWithGenresAndRatingsAsync(dto.Id);

            if (book == null)
                throw new Exception("Book not found.");

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Year = dto.Year;
            book.Description = dto.Description;
            book.Review = dto.Review;
            book.Isbn = dto.Isbn;
            book.UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(dto.NewCoverImagePath))
            {
                book.CoverImagePath = dto.NewCoverImagePath;
            }

            var allGenres = await _genreRepository.GetAllAsync();

            book.Genres.Clear();

            var selectedGenres = allGenres
                .Where(g => dto.GenreIds.Contains(g.Id))
                .ToList();

            foreach (var genre in selectedGenres)
            {
                book.Genres.Add(genre);
            }

            await _bookRepository.UpdateAsync(book);
        }


        private double CalculateAverageRating(Book book)
        {
            if (book.Ratings == null || !book.Ratings.Any())
                return 0;

            return book.Ratings.Average(r => r.RatingValue);
        }

    }
}
