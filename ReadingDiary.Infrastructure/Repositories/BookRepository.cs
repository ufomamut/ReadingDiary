using Microsoft.EntityFrameworkCore;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Domain.Entities;
using ReadingDiary.Infrastructure.Data;
using ReadingDiary.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Book>> GetAllWithGenresAndRatingsAsync()
        {
            return await context.Books
                                .Include(b => b.Genres)
                                .Include(b => b.Ratings)
                                .ToListAsync();
        }


        public async Task<Book?> GetBookWithGenresAndRatingsAsync(int id)
        {
            return await context.Books
                                .Include(b => b.Genres)
                                .Include(b => b.Ratings)
                                .FirstOrDefaultAsync(b => b.Id == id);
        }




        /// <summary>
        /// Returns a paged list of books with optional filtering, searching and sorting.
        /// Intended for catalog-like views.
        /// </summary>
        public async Task<(IEnumerable<Book> Books, int TotalCount)> GetPagedWithFiltersAsync(int page, int pageSize, int? genreId, string? search, string? sort)
        {
            var query = context.Books
                               .Include(b => b.Genres)
                               .AsQueryable();

            // Filter by genre
            if (genreId.HasValue)
                query = query.Where(b => b.Genres.Any(g => g.Id == genreId.Value));

            // Simple text search
            if (!string.IsNullOrEmpty(search))
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));

            // Sorting
            query = sort switch
            {
                "title" => query.OrderBy(b => b.Title),
                "author" => query.OrderBy(b => b.Author),
                "newest" => query.OrderByDescending(b => b.CreatedAt),
                _ => query.OrderByDescending(b => b.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, totalCount);
        }
    }
}
