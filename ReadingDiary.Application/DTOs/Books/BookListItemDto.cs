using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.DTOs.Books
{
    public class BookListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public List<string> Genres { get; set; } = new();
        public string? CoverImagePath { get; set; }
        public string? Isbn { get; set; }
        public int RatingsCount { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
