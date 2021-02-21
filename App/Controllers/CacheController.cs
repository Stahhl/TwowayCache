using App.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        public CacheController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IDistributedCache _cache { get; set; }

        [HttpGet]
        public async Task<JsonResult> Get(string type, string key)
        {
            var result = await _cache.AddAndOrGetKeyAsync(type.ToUpper(), key);

            return new JsonResult(result);
        }
    }
}
