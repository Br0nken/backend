using LoggingLab.Services;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataService _dataService;

    public HomeController(
        ILogger<HomeController> logger,
        DataService dataService)
    {
        _logger = logger;
        _dataService = dataService;
    }

    public IActionResult Index()
    {
        _dataService.ProcessData(); // Вызов сервиса
        _logger.LogInformation("Страница загружена");
        _logger.LogWarning("Это тестовое предупреждение!");


        try
        {
            throw new Exception("Исключение для теста логирования");
        }
        catch (Exception ex)
        {
            _logger.LogError("Ошибка при обработке запроса {Url} пользователем {UserId}",
    Request.Path, User.Identity?.Name ?? "Anonymous");
        }

        return View();
    }
}