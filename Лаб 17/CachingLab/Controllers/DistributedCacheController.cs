using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace CachingLab.Controllers
{
    public class DistributedCacheController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            string cacheKey = "CachedTime";
            string currentTime;

            var encodedTime = await _distributedCache.GetAsync(cacheKey);
            Console.WriteLine($"encodedTime is null: {encodedTime == null}"); // Лог

            if (encodedTime == null)
            {
                currentTime = DateTime.Now.ToString();
                encodedTime = Encoding.UTF8.GetBytes(currentTime);
                Console.WriteLine($"New value: {currentTime}"); // Лог

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(20))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                await _distributedCache.SetAsync(cacheKey, encodedTime, options);
                Console.WriteLine("Value saved to cache"); // Лог

                ViewBag.Message = "Значение получено и сохранено в распределенном кеше.";
            }
            else
            {
                currentTime = Encoding.UTF8.GetString(encodedTime);
                Console.WriteLine($"Cached value: {currentTime}"); // Лог
                ViewBag.Message = "Значение получено из распределенного кеша.";
            }

            ViewBag.CurrentTime = currentTime;
            return View();
        }
    }
}