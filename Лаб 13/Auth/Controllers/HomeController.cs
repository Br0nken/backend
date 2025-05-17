using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize] // Изменено с AllowAnonymous
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Editor,Admin")]
        public IActionResult ContentManagement()
        {
            return View();
        }
    }
}