using ReadingDiary.Domain.Entities;
using ReadingDiary.Infrastructure.Data;
using ReadingDiary.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
	{
		public GenreRepository(ApplicationDbContext context)
			: base(context)
		{
		}
	}
}
