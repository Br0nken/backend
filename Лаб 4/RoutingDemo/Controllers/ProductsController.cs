using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
    public class ProductsController : Controller
    {
        // GET /products
        public IActionResult Index()
        {
            return Content("Products Index Page");
        }

        // GET /products/details/5
        public IActionResult Details(int id)
        {
            return Content($"Product Details for ID: {id}");
        }
    }
}