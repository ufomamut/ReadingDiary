using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingDiary.Application.Interfaces;
using ReadingDiary.Web.Controllers;
using System.Security.Claims;

namespace ReadingDiary.Web.Controllers
{
    /// <summary>
    /// Handles user book ratings.
    /// Allows authenticated users to rate books and updates
    /// existing ratings if already present.
    /// </summary>
    public class RatingController : BaseController
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rate(int bookId, int value)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction(
                    "Login",
                    "Account",
                    new
                    {
                        returnUrl = Url.Action(
                            "Details",
                            "Books",
                            new { id = bookId }
                        )
                    });
            }

            await _ratingService.RateAsync(bookId, CurrentUserId, value);

            return RedirectToAction("Details", "Books", new { id = bookId });
        }
    }
}

