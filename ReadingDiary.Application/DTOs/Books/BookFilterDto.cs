using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.DTOs.Books
{
    public class BookFilterDto
    {
        public string? SearchTerm { get; set; }
        public int? GenreId { get; set; }
        public int? RatingMin { get; set; }
        public string? Sort { get; set; }
    }
}
