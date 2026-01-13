using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.DTOs.Books
{
    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public string? Description { get; set; }
        public string? Review { get; set; }
        public List<int> GenreIds { get; set; } = new();
        public string? CoverImagePath { get; set; }
        public string? Isbn { get; set; }
    }
}
