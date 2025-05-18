using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpPost]
    public IActionResult SetTheme(string theme)
    {
        theme = theme == "dark" ? "dark" : "light"; // Принудительная валидация

        Response.Cookies.Append("Theme", theme, new CookieOptions
        {
            Expires = DateTime.Now.AddDays(30),
            HttpOnly = true,
            IsEssential = true
        });

        HttpContext.Session.SetString("Theme", theme);
        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        // Синхронизируем localStorage с сервером
        var theme = HttpContext.Session.GetString("Theme")
                   ?? Request.Cookies["Theme"]
                   ?? "light";

        // Обновляем куки при каждом запросе для актуальности
        Response.Cookies.Append("Theme", theme, new CookieOptions
        {
            Expires = DateTime.Now.AddDays(30),
            HttpOnly = true,
            IsEssential = true
        });

        ViewBag.CurrentTheme = theme;
        return View();
    }
}