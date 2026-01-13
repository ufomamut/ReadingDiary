using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Domain.Entities
{
    public class BookRating
    {
        public int Id { get; set; }
        public int RatingValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public string? UserId { get; set; }
    }
}
