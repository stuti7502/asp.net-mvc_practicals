using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Practical16.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HelloWorldController : Controller
    {
        private readonly ILogger<HelloWorldController> _logger;
        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
                _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Hey");
            return "Hello World";
        }
    }
}
