using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ErrorLab.Controllers;

public class ErrorController : Controller
{
    [Route("/Error/{statusCode?}")]
    public IActionResult Error(int? statusCode, [FromQuery] string message)
    {
        statusCode ??= 500;
        Response.StatusCode = statusCode.Value;

        ViewBag.ErrorCode = statusCode;
        ViewBag.ErrorMessage = message ?? statusCode switch
        {
            403 => "Доступ запрещен",
            404 => "Страница не найдена",
            500 => "Внутренняя ошибка сервера",
            _ => "Произошла ошибка"
        };

        if (User.IsInRole("Admin"))
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ErrorDetail = exceptionHandler?.Error?.StackTrace;
        }

        return View();
    }

    private string GetDefaultMessage(int code) => code switch
    {
        400 => "Некорректный запрос",
        401 => "Требуется авторизация",
        403 => "Доступ запрещен",
        404 => "Страница не найдена",
        500 => "Внутренняя ошибка сервера",
        _ => $"Ошибка {code}"
    };
}