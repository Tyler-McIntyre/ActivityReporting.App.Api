using ActivityReporting.App.Api.Models;
using ActivityReporting.App.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;

namespace ActivityReporting.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public ReportController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpPost]
        [Route("/activity/{key}")]
        public ActionResult NewActivityEntry([FromBody] ActivityDto activity, [FromRoute] string key)
        {
            Log.Logger.Information($"{key}, {activity.Value}");

            if (!_cache.TryGetValue(key, out _))
            {
                //create and add the entry
                _cache.CreateEntry(key);

                List<IActivityDto> keyList = new() {
                    activity
                };

                _cache.Set(key, keyList, Factory.CreateNewCacheOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddHours(12)));
            }
            else
            {
                //add to the key cache
                List<IActivityDto> cachedList = _cache.Get<List<IActivityDto>>(key);
                cachedList.Add(activity);
                _cache.Set(key, cachedList);
            }

            return Ok();
        }

        [HttpGet]
        [Route("activity/{key}/total")]
        public ActionResult<IActivityDto> GetKeyValue(string key)
        {
            Log.Logger.Information($"Get Total: {key}");

            if (!_cache.TryGetValue(key, out _))
            {
                return NotFound("Key not found");
            }

            return Ok(Factory.CreateNewActivity().Value = 
                _cache.Get<List<IActivityDto>>(key)
                .Sum(x => x.Value));
        }
    }
}
