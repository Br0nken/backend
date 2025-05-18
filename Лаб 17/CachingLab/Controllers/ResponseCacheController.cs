using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CachingLab.Controllers
{
    public class ResponseCacheController : Controller
    {
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            ViewBag.CurrentTime = DateTime.Now.ToString();
            return View();
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult ClientSideCache()
        {
            ViewBag.CurrentTime = DateTime.Now.ToString();
            return View();
        }
    }
}