using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingDiary.Application.DTOs.Reading;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Web.Extensions;
using ReadingDiary.Web.Models.ViewModels;

namespace ReadingDiary.Web.Controllers
{

    /// <summary>
    /// Displays an overview of the authenticated user's reading activity,
    /// grouped by reading status.
    /// </summary>
    [Authorize]
    public class MyBooksController : BaseController
    {
        private readonly IUserReadingOverviewService _overviewService;

        public MyBooksController(IUserReadingOverviewService overviewService)
        {
            _overviewService = overviewService;
        }

     
        public async Task<IActionResult> Index()
        {
            var dto = await _overviewService.GetOverviewAsync(CurrentUserId);

            var model = new MyBooksIndexViewModel
            {
                ToRead = dto.ToRead.Select(MapItem).ToList(),
                Reading = dto.Reading.Select(MapItem).ToList(),
                Postponed = dto.Postponed.Select(MapItem).ToList(),
                Finished = dto.Finished.Select(MapItem).ToList()
            };

            return View(model);
        }


        private static MyBookItemViewModel MapItem(MyBookItemDto item)
        {
            return new MyBookItemViewModel
            {
                BookId = item.BookId,
                Title = item.Title,
                Author = item.Author,
                CoverImagePath = item.CoverImagePath,
                StatusText = item.Status.ToCzech(),
                UpdatedAtFormatted =
                                    (item.UpdatedAt ?? item.CreatedAt)
                                    .ToLocalTime()
                                    .ToString("d.M.yyyy HH:mm")
            };
        }
    }
}
