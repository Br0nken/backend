using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CachingLab.Controllers
{
    public class MemoryCacheController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            DateTime currentTime;

            // Попытка получить значение из кеша
            if (!_memoryCache.TryGetValue("CachedTime", out currentTime))
            {
                // Если значения нет в кеше, получаем его и сохраняем
                currentTime = DateTime.Now;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10)) // Обновляется при каждом запросе
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30)); // Максимальное время жизни

                _memoryCache.Set("CachedTime", currentTime, cacheEntryOptions);

                ViewBag.Message = "Значение получено и сохранено в кеше.";
            }
            else
            {
                ViewBag.Message = "Значение получено из кеша.";
            }

            ViewBag.CurrentTime = currentTime;
            return View();
        }
    }
}