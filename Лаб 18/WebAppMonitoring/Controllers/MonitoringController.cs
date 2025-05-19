using Microsoft.AspNetCore.Mvc;

public class MonitoringController : Controller
{
    private static readonly Random _random = new();

    public IActionResult Test()
    {
        var delay = _random.Next(50, 500);
        Thread.Sleep(delay);
        return View("TestResult", delay); // Возвращаем на специальную страницу
    }

    public IActionResult GenerateError()
    {
        throw new Exception("Тестовая ошибка для мониторинга");
    }
}