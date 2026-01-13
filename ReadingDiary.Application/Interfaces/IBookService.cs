using ReadingDiary.Application.DTOs.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookListItemDto>> GetFilteredAsync(BookFilterDto filter);
        Task<BookDetailDto?> GetDetailAsync(int id);
        Task<int> CreateAsync(BookCreateDto dto);
        Task UpdateAsync(BookUpdateDto dto);
        Task DeleteAsync(int id);
        Task<List<BookListItemDto>> GetAdminListAsync( string? search, string? sort);
    }
}
