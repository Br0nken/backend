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
        _dataService.ProcessData(); // ����� �������
        _logger.LogInformation("�������� ���������");
        _logger.LogWarning("��� �������� ��������������!");


        try
        {
            throw new Exception("���������� ��� ����� �����������");
        }
        catch (Exception ex)
        {
            _logger.LogError("������ ��� ��������� ������� {Url} ������������� {UserId}",
    Request.Path, User.Identity?.Name ?? "Anonymous");
        }

        return View();
    }
}