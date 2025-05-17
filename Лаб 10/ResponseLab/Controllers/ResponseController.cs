using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ResponseLab.Controllers
{
    public class ResponseController : Controller
    {
        // 1. Возврат HTML (View)
        public IActionResult HtmlResponse()
        {
            return View(); // Будет искать Views/Response/HtmlResponse.cshtml
        }

        // 2. Возврат JSON
        public IActionResult JsonResponse()
        {
            var data = new { Message = "Hello, ASP.NET Core!", Status = 200 };
            return Json(data);
        }

        // 3. Возврат файла (пример)
        public IActionResult FileResponse()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sample.txt");
            System.IO.File.WriteAllText(filePath, "Это тестовый файл для скачивания.");
            return PhysicalFile(filePath, "text/plain", "sample.txt");
        }

        // 4. Возврат простого текста
        public IActionResult TextResponse()
        {
            return Content("Это простой текстовый ответ", "text/plain");
        }

        // 5. Возврат ошибки 404
        public IActionResult NotFoundResponse()
        {
            return NotFound("Страница не найдена");
        }

        // 6. Перенаправление
        public IActionResult RedirectResponse()
        {
            return Redirect("https://google.com");
        }
    }
}