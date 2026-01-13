using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Domain.Entities;
using ReadingDiary.Infrastructure.Data;
using ReadingDiary.Infrastructure.Repositories;
using ReadingDiary.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingDiary.Web.Controllers
{

    /// <summary>
    /// Provides administrative CRUD operations for book genres.
    /// 
    /// NOTE:
    /// This controller currently uses repositories directly.
    /// In a future version, repositories will be replaced by
    /// dedicated application services.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenresController(IRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepository.GetAllAsync();

            var model = genres.Select(g => new GenreViewModel
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            }).ToList();

            return View(model);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreViewModel genreViewModel)
        {
            if (!ModelState.IsValid)
                return View(genreViewModel);

            var genre = new Genre
            {
                Name = genreViewModel.Name,
                Description = genreViewModel.Description
            };

            await _genreRepository.AddAsync(genre);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var genre = await _genreRepository.GetByIdAsync(id.Value);
            if (genre == null)
                return NotFound();

            var model = new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GenreViewModel genreViewModel)
        {
            if (id != genreViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(genreViewModel);

            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                return NotFound();
            
            genre.Name = genreViewModel.Name;
            genre.Description = genreViewModel.Description;

            try
            {
                await _genreRepository.UpdateAsync(genre);
            }
            catch
            {
                if (!await GenreExistsAsync(genreViewModel.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _genreRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GenreExistsAsync(int id)
        {
            return await _genreRepository.ExistsAsync(id);
        }
    }
}
