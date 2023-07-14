using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Practical20.Models;

namespace Practical20.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{StatusCode}")]
        public IActionResult Index(int StatusCode)
        {
            StatusResult model = new StatusResult();
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (StatusCode)
            {
                case 404:
                    {
                        model.Message = "Sorry, the resource you requested could not be found";
                        model.Path = statusCodeResult.OriginalPath;
                        model.QS = statusCodeResult.OriginalQueryString;
                    }
                    break;
            }
            return View();
        }
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
            return View("Error");
        }
    }
}
