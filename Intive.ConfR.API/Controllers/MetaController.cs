using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intive.ConfR.API.Controllers
{
    public class MetaController : BaseController
    {
        private readonly IHostingEnvironment _environment;

        public MetaController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Returns environment variable
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("Environment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetEnvironmentVariable()
        {
            return Ok(_environment.EnvironmentName);
        }
    }
}
