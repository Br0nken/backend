using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StaticFilesLab.Models;

namespace StaticFilesLab.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ImageTest()
    {
        return View();
    }
}