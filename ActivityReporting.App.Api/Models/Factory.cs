using ActivityReporting.App.Api.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ActivityReporting.App.Api.Models
{
    public class Factory
    {
        public static IActivityDto CreateNewActivity() {

            return new ActivityDto();
        }
        public static MemoryCacheEntryOptions CreateNewCacheOptions()
        {
            return new MemoryCacheEntryOptions();
        }
    }
}
