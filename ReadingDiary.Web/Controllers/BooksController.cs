using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReadingDiary.Application.Configuration;
using ReadingDiary.Application.DTOs.Books;
using ReadingDiary.Application.Exceptions;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Application.Services;
using ReadingDiary.Domain.Entities;
using ReadingDiary.Infrastructure.Data;
using ReadingDiary.Infrastructure.Repositories;
using ReadingDiary.Infrastructure.Services;
using ReadingDiary.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadingDiary.Web.Controllers
{

    /// <summary>
    /// Handles book-related pages including public book details
    /// and administrative CRUD operations.
    /// </summary>
    public class BooksController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IImageService _imageService;
        private readonly IDiaryService _diaryService;

        public BooksController(
            IBookService bookService,
            IRepository<Genre> genreRepository,
            IImageService imageService,
            IDiaryService diaryService)
        {
            _bookService = bookService;
            _genreRepository = genreRepository;
            _imageService = imageService;
            _diaryService = diaryService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int page = 1, string? search = null,string? sort = null)
        {
            int pageSize = 10;

            var books = await _bookService.GetAdminListAsync(search, sort);

            var totalCount = books.Count;

            var pagedBooks = books
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var bookItems = pagedBooks.Select(b => new BookListItemViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                CoverImagePath = b.CoverImagePath,
                Isbn = b.Isbn,
                CreatedAt = b.CreatedAt
            }).ToList();

            var model = new AdminBooksIndexViewModel
            {
                Books = bookItems,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Search = search,
                Sort = sort
            };

            return View(model);
        }


        public async Task<IActionResult> Details(int id)
        {
            var dto = await _bookService.GetDetailAsync(id);

            if (dto == null)
                return NotFound();

            var viewModel = new BookDetailViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Author = dto.Author,
                Year = dto.Year,
                Description = dto.Description,
                Review = dto.Review,
                CoverImagePath = dto.CoverImagePath,
                AverageRating = dto.AverageRating,
                RatingsCount = dto.RatingsCount,
                Genres = dto.Genres,
                Isbn = dto.Isbn,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };

            if (User.Identity?.IsAuthenticated == true)
            {
                var diary = await _diaryService
                    .GetDiaryForBookAsync(id, CurrentUserId);

                if (diary != null)
                {
                    viewModel.DiaryId = diary.Id;
                    viewModel.DiaryStatus = diary.Status;
                    viewModel.DiaryNotes = diary.DiaryNotes;
                    viewModel.DiaryCreatedAt = diary.CreatedAt;
                    viewModel.DiaryUpdatedAt = diary.UpdatedAt;
                }
            }

            return View(viewModel);
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            // future change -> genre service
            var genres = await _genreRepository.GetAllAsync();

            var model = new BookViewModel
            {
                AvailableGenres = genres.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await FillGenresAsync(model);
                return View(model);
            }

            string? imagePath = null;

            try
            {
                imagePath = await _imageService.SaveAsync(model.CoverImageFile);
            }
            catch (ImageValidationException ex)
            {
                ModelState.AddModelError("CoverImageFile", ex.Message);
                await FillGenresAsync(model);
                return View(model);
            }

            var dto = new BookCreateDto
            {
                Title = model.Title,
                Author = model.Author,
                Year = model.Year,
                Description = model.Description,
                Review = model.Review,
                GenreIds = model.SelectedGenreIds.ToList(),
                CoverImagePath = imagePath,
                Isbn = model.Isbn
            };

            await _bookService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _bookService.GetDetailAsync(id);
            if (dto == null)
                return NotFound();

            var genres = await _genreRepository.GetAllAsync();

            var model = new BookViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Author = dto.Author,
                Year = dto.Year,
                Description = dto.Description,
                Review = dto.Review,
                CoverImagePath = dto.CoverImagePath,
                Isbn = dto.Isbn,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                SelectedGenreIds = genres
                                        .Where(g => dto.Genres.Contains(g.Name))
                                        .Select(g => g.Id)
                                        .ToList(),
                AvailableGenres = genres.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = dto.Genres.Contains(g.Name)
                })
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, BookViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await FillGenresAsync(model);
                return View(model);
            }

            string? imagePath = model.CoverImagePath;

            if (model.CoverImageFile != null)
            {
                try
                {
                    imagePath = await _imageService.ReplaceAsync(
                        model.CoverImagePath,
                        model.CoverImageFile);
                }
                catch (ImageValidationException ex)
                {
                    ModelState.AddModelError("CoverImageFile", ex.Message);
                    await FillGenresAsync(model);
                    return View(model);
                }
            }

            var dto = new BookUpdateDto
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Year = model.Year,
                Description = model.Description,
                Review = model.Review,
                GenreIds = model.SelectedGenreIds,
                NewCoverImagePath = imagePath,
                Isbn = model.Isbn
            };

            await _bookService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// Populates AvailableGenres collection for book create/edit views.
        /// </summary>
        private async Task FillGenresAsync(BookViewModel model)
        {
            var genres = await _genreRepository.GetAllAsync();

            model.AvailableGenres = genres.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name,
                Selected = model.SelectedGenreIds.Contains(g.Id)
            });
        }
    }
}
