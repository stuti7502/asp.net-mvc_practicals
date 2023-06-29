using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical10.Controllers
{
    public class ActionResultTypeController : Controller
    {
        // GET: ActionResult
        public ContentResult ContentResult()
        {
            return Content("<h2>hey there! This is sample for content result</h2>");
        }

        public FileResult FileContentResult()
        {
            return File("C:\\Users\\stuti\\source\\repos\\Practical10\\Practical10\\Views\\Home\\Index.cshtml", "text");
        }

        public EmptyResult EmptyResult()
        {
            return new EmptyResult();
        }

        public JavaScriptResult JavaScriptResult()
        {
            var msg = "alert('Hey there! Just a demo alert! Click OK to continue!')";
            return new JavaScriptResult() { Script = msg};
        }

        public JsonResult JSONResult()
        {
            return Json(new {Name = "Stuti Vithlani", Id = 7, gender = "Female"},
                JsonRequestBehavior.AllowGet);
        }
    }
}