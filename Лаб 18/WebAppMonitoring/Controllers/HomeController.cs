using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["TotalRequests"] = AppMetrics.TotalRequests;
        ViewData["TotalErrors"] = AppMetrics.TotalErrors;
        return View();
    }
}