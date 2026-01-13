using ReadingDiary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IDiaryRepository : IRepository<Diary>
    {
        Task<Diary?> GetByBookAndUserAsync(int bookId, string userId);
        Task<IEnumerable<Diary>> GetAllByUserAsync(string userId);
    }
}
