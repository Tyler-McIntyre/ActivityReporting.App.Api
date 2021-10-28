using ActivityReporting.App.Api.Interfaces;
using ActivityReporting.App.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;

namespace ActivityReporting.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        [HttpPost]
        [Route("/activity/{key}")]
        public async Task<ActionResult> Post([FromBody] ActivityLog activityLog, [FromRoute] string key)
        {
            Log.Logger.Information($"{key}, {activityLog.Value}");

            activityLog.SetKey(key);
            
            await Task
                .Run(() => InMemDatabase
                .Log(activityLog));

            return Ok();
        }

        [HttpGet]
        [Route("activity/{key}/total")]
        public async Task<ActionResult<ActivityResponse>> Get(string key)
        {
            Log.Logger.Information($"Get Total: {key}");

            return Ok(await Task
                .Run(() => Factory
                .CreateNewActivityResponse(InMemDatabase
                .Sum(key))));
        }
    }
}
