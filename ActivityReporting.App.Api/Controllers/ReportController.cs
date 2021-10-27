using ActivityReporting.App.Api.Interfaces;
using ActivityReporting.App.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ActivityReporting.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IDatabase iDatabase = Factory.CreateNewInMemDb();

        [HttpPost]
        [Route("/activity/{key}")]
        public async Task<ActionResult> Post([FromBody] ActivityLog activityLog, [FromRoute] string key)
        {
            activityLog.SetKey(key);
            
            await Task
                .Run(() => iDatabase
                .Log(activityLog));

            return Ok();
        }

        [HttpGet]
        [Route("activity/{key}/total")]
        public async Task<ActionResult<ActivityResponse>> Get(string key)
        {
            return Ok(await Task
                .Run(() => Factory.CreateNewActivityResponse(iDatabase
                .Sum(key
                .ToUpper()))));
        }
    }
}
