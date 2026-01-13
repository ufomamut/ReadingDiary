using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ReadingDiary.Application.Configuration;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDiary.Infrastructure.Services
{

    /// <summary>
    /// Handles local storage of book cover images.
    /// 
    /// Images are stored outside of the application directory on the local file system.
    /// This implementation is intended for development and demo purposes
    /// until a cloud-based storage solution is introduced.
    /// </summary>
    public class ImageService : IImageService
    {
        private readonly string _rootPath;

        public ImageService(IOptions<StorageSettings> settings)
        {
            _rootPath = settings.Value.BookCoversPath;

            // Ensure target directory exists
            if (!Directory.Exists(_rootPath))
                Directory.CreateDirectory(_rootPath);
        }



        public async Task DeleteAsync(string? imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return;

            string path = Path.Combine(_rootPath, imageName);

            if (File.Exists(path))
                File.Delete(path);
        }



        public async Task<string?> ReplaceAsync(string? oldImageName, IFormFile newImage)
        {
            await DeleteAsync(oldImageName);
            return await SaveAsync(newImage);
        }



        public async Task<string?> SaveAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            ValidateImageFile(image);

            string fileName = GenerateImageName(image.FileName);
            string fullPath = Path.Combine(_rootPath, fileName);

            using var stream = File.Create(fullPath);
            await image.CopyToAsync(stream);

            return fileName;
        }




        // Validation helpers
        private void ValidateImageFile(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName).ToLower();
            string[] permitted = { ".jpg", ".jpeg", ".png" };
            long maxSize = 2 * 1024 * 1024; // 2MB

            if (!permitted.Contains(extension))
                throw new ImageValidationException("Povolené formáty: JPG, JPEG, PNG.");

            if (file.Length > maxSize)
                throw new ImageValidationException("Maximální velikost obrázku je 2MB.");

        }

        private string GenerateImageName(string originalName)
        {
            string ext = Path.GetExtension(originalName);
            return $"{Guid.NewGuid()}{ext}";
        }
    }
}
