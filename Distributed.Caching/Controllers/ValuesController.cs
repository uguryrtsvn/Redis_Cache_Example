using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Distributed.Caching.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary> !!!!!!!!!!!!!!!!!!!******************
        /// UYGULAMAYI ÇALIŞTIRMADAN ÖNCE REDİS SUNUCUNUZU AYAĞA KALDIRMAYI VE PROGRAM.CS de
        /// Yapılandırmayı unutmayın !.
        /// </summary> 
        private readonly IDistributedCache _distributedCache;
        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        [HttpGet]
        public async Task<IActionResult> GetRedisCache(string key)
        { 
            //**** Binary formatta çalışılmak istenirse kullanılacak şekli.******// 
            //var byteResult = await _distributedCache.GetAsync(key);
            //var result = Encoding.UTF8.GetString(byteResult);
            //return Ok(result);
            //----------------------------------------////

            return Ok(await _distributedCache.GetStringAsync(key));
        }
        [HttpGet]
        public async Task SetRedisCache(string key, string val)
        {
            await _distributedCache.SetStringAsync(key, val);
        }

        [HttpGet]
        public async Task SetRedisCacheWithExpiration(string key, string val)
        {
            await _distributedCache.SetStringAsync(key, val, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(10)
            });

            //**** Binary formatta çalışılmak istenirse kullanılacak şekli.******// 

            //_distributedCache.Set(key,Encoding.UTF8.GetBytes(val), options: new()
            //{
            //    AbsoluteExpiration = DateTime.Now.AddMinutes(1),
            //    SlidingExpiration = TimeSpan.FromSeconds(10)
            //});
        }
    }
}
