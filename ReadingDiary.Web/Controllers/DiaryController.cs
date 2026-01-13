using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReadingDiary.Application.Configuration;
using ReadingDiary.Application.DTOs.Diary;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Domain.Enums;
using ReadingDiary.Web.Models.ViewModels;
using System.Security.Claims;

namespace ReadingDiary.Web.Controllers
{

    /// <summary>
    /// Handles creation, editing and management of user reading diaries.
    /// Requires authenticated user.
    /// </summary>
    [Authorize]
    public class DiaryController : BaseController
    {
        private readonly IDiaryService _diaryService;
        private readonly IOptions<TinyMceSettings> _tinyMceSettings;

        public DiaryController(IDiaryService diaryService, IOptions<TinyMceSettings> tinyMceSettings)
        {
            _diaryService = diaryService;
            _tinyMceSettings = tinyMceSettings;
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int bookId)
        {
            ViewBag.TinyMceApiKey = _tinyMceSettings.Value.ApiKey;

            var diaryDto = await _diaryService
                .GetDiaryForBookAsync(bookId, CurrentUserId);

            DiaryViewModel model;

            if (diaryDto == null)
            {
                model = new DiaryViewModel
                {
                    BookId = bookId
                };
            }
            else
            {
                model = new DiaryViewModel
                {
                    Id = diaryDto.Id,
                    BookId = diaryDto.BookId,
                    DiaryNotes = diaryDto.DiaryNotes,
                    Status = diaryDto.Status
                };
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DiaryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new DiaryDto
            {
                Id = model.Id,                
                BookId = model.BookId,
                UserId = CurrentUserId,
                DiaryNotes = model.DiaryNotes,
                Status = model.Status
            };

            await _diaryService.SaveDiaryAsync(dto);

            return RedirectToAction("Details", "Books", new { id = model.BookId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int bookId)
        {
            await _diaryService.DeleteDiaryAsync(id, CurrentUserId);

            return RedirectToAction( "Details", "Books", new { id = bookId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int diaryId, ReadingDiaryState status, int bookId)
        {
            await _diaryService.ChangeStatusAsync(diaryId, status, CurrentUserId);

            return RedirectToAction("Details", "Books", new { id = bookId });
        }
    }
}
