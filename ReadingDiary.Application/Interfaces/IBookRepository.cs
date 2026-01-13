using ReadingDiary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        

        Task<Book?> GetBookWithGenresAndRatingsAsync(int id);
        Task<IEnumerable<Book>> GetAllWithGenresAndRatingsAsync();
        Task<(IEnumerable<Book> Books, int TotalCount)> GetPagedWithFiltersAsync(
            int page,
            int pageSize,
            int? genreId,
            string? search,
            string? sort);
    }
}
