using Microsoft.AspNetCore.Mvc;
using ReadingDiary.Application.DTOs.Books;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Application.Services;
using ReadingDiary.Web.Models;
using ReadingDiary.Web.Models.ViewModels;
using System.Diagnostics;

namespace ReadingDiary.Web.Controllers
{

    /// <summary>
    /// Handles public application entry points such as the book catalog homepage
    /// and auxiliary informational pages.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IBookService _bookService;

        public HomeController(IGenreRepository genreRepository, IBookService bookService)
        {
            _genreRepository = genreRepository;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(int page = 1, int? genreId = null, string? search = null, string? sort = null)
        {
            int pageSize = 8;

            var filterDto = new BookFilterDto
            {
                SearchTerm = search,
                GenreId = genreId,
                RatingMin = null,
                Sort = sort
            };

            var books = await _bookService.GetFilteredAsync(filterDto);

            var totalCount = books.Count;

            var pagedBooks = books
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var booksModel = pagedBooks.Select(b => new BookListItemViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                CoverImagePath = b.CoverImagePath,
                Genres = b.Genres.ToList(),
                AverageRating = b.AverageRating,
                RatingsCount = b.RatingsCount,
                Isbn = b.Isbn
            }).ToList();

            var model = new BooksFeedViewModel
            {
                Books = booksModel,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                SelectedGenreId = genreId,
                Search = search,
                Sort = sort,
                Genres = (await _genreRepository.GetAllAsync()).ToList(),
                NoResults = !booksModel.Any()
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
