using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Intive.ConfR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET api/app/version
        [HttpGet("version")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetAppVersion()
        {
            var version = _configuration.GetSection("Application:Version").Value;

            return version;
        }
    }
}