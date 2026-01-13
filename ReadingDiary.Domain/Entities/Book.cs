using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public string? Isbn { get; set; }
        public string? Description { get; set; }
        public string? Review { get; set; }
        public string? CoverImagePath { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedByUserId { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<BookRating> Ratings { get; set; } = new List<BookRating>();
    }
}
