using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ErrorLab.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "GlobalExceptionFilter caught an exception");

        context.ExceptionHandled = true;

        context.Result = new RedirectToActionResult(
            "Error",
            "Error",
            new
            {
                statusCode = 500,
                message = context.Exception.Message
            });
    }
}