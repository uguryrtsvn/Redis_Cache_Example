using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoryExampController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryExampController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult GetCache(string key)
        {
            return Ok(_memoryCache.Get(key)); 
        }

        [HttpGet]
        public IActionResult SetCache(string key, string val)
        {
            var item = _memoryCache.Set(key, val);

            return item != null ? Ok(item) : BadRequest(item);
        }
        [HttpGet]
        public void SetDateWithOptions()
        {
            _memoryCache.Set("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            }); 
        }
        [HttpGet]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
