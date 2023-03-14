using Microsoft.AspNetCore.Mvc;

namespace APIServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var port = Request.Host.Port;

            return new string[] { "value1", "value2", port.Value.ToString() };
        }
    }
}
