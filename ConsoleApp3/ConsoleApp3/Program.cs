using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

// Настройка хоста с DI
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<ILoggerService, LoggerService>(); // Регистрация сервиса
        services.AddTransient<App>(); // Главный класс приложения
    })
    .Build();

// Запуск
var app = host.Services.GetRequiredService<App>();
app.Run();

// Класс приложения
public class App
{
    private readonly ILoggerService _logger;

    // Внедрение зависимости через конструктор
    public App(ILoggerService logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        _logger.Log("Приложение запущено!");
        Console.WriteLine("Hello from ConsoleDIApp!");
        _logger.Log("Приложение завершает работу.");
    }
}