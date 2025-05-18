using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Config.Models;

namespace Config.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        ViewBag.TestSetting = _config["TestSetting"];

        ViewBag.AppName = _config["AppSettings:AppName"];
        ViewBag.Environment = _config["AppSettings:Environment"];
        ViewBag.DbConnection = _config.GetConnectionString("DefaultConnection");
        ViewBag.DebugMode = _config.GetValue<bool>("AppSettings:DebugMode");

        return View();
    }
}