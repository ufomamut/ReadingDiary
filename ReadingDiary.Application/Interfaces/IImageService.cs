using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Application.Interfaces
{
    public interface IImageService
    {
        Task<string?> SaveAsync(IFormFile image);
        Task DeleteAsync(string? imageName);
        Task<string?> ReplaceAsync(string? oldImageName, IFormFile newImage);
    }
}
