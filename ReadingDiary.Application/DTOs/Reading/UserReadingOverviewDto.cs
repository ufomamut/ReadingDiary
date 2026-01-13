using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.DTOs.Reading
{
    public class UserReadingOverviewDto
    {
        public IReadOnlyList<MyBookItemDto> ToRead { get; init; } = [];
        public IReadOnlyList<MyBookItemDto> Reading { get; init; } = [];
        public IReadOnlyList<MyBookItemDto> Postponed { get; init; } = [];
        public IReadOnlyList<MyBookItemDto> Finished { get; init; } = [];
    }
}
