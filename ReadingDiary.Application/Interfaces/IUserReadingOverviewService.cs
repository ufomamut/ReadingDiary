using ReadingDiary.Application.DTOs.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IUserReadingOverviewService
    {
        Task<UserReadingOverviewDto> GetOverviewAsync(string userId);
    }
}
