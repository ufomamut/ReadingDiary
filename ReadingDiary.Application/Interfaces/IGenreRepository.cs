using ReadingDiary.Domain.Entities;
using ReadingDiary.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
    }
}
