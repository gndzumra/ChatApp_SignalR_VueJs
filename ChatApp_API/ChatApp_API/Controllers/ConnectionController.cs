using Microsoft.AspNetCore.Mvc;

namespace ChatApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        [HttpGet, Route("test")]
        public string Test()
        {
            return "test";
        }
    }
}
