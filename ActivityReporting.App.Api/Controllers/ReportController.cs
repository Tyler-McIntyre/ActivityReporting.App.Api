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
        private readonly IDatabase iDatabase = new InMemDatabase();

        [HttpPost]
        [Route("/activity/{key}")]
        public async Task<ActionResult> Post([FromBody] ActivityLogReq activity, [FromRoute] string key)
        {
            activity.SetKey(key);

            await Task
                .Run(() => iDatabase
                .Log(activity));

            return Ok();
        }

        [HttpGet]
        [Route("activity/{key}/total")]
        public async Task<ActionResult<ActivityResp>> Get(string key)
        {
            return Ok(await Task
                .Run(() => new ActivityResp(iDatabase
                .Sum(key
                .ToUpper()))));
        }
    }
}
