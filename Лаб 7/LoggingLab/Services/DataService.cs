namespace LoggingLab.Services;

public class DataService
{
    private readonly ILogger<DataService> _logger;

    public DataService(ILogger<DataService> logger)
    {
        _logger = logger;
    }

    public void ProcessData()
    {
        _logger.LogDebug("Начало обработки данных...");
        // Логика обработки...
        _logger.LogInformation("Данные успешно обработаны");
    }
}