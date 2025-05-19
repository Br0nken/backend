using Microsoft.AspNetCore.Mvc;

namespace YourProjectName.Controllers;

public class TestErrorController : Controller
{
    public IActionResult Generate404() => NotFound();
    public IActionResult Generate401() => Unauthorized();
    public IActionResult Generate403()
    {
        return RedirectToAction("Error", "Error", new { statusCode = 403 });
    }

    public IActionResult Generate500()
    {
        throw new Exception("Тестовое исключение для проверки обработки ошибок 500");
    }

    public IActionResult GenerateDbError()
    {
        var ex = new Exception("Тестовая ошибка базы данных");
        ex.Data.Add("IsDatabaseError", true);
        throw ex;
    }

    public IActionResult GenerateIoError()
    {
        throw new System.IO.IOException("Тестовая ошибка ввода-вывода");
    }
}