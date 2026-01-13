using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ReadingDiary.Web.Controllers
{

    /// <summary>
    /// Base controller providing common functionality
    /// for controllers that require authenticated user context.
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
