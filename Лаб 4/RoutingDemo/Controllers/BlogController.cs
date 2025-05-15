using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Archive(int year, int month, int? day)
        {
            if (day.HasValue)
                return Content($"Blog Archive for {year}/{month}/{day}");
            else
                return Content($"Blog Archive for {year}/{month}");
        }
    }
}