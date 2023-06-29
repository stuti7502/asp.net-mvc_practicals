using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical10.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Test2()
        {
            
            return View();
        }

        [OutputCache(Duration = 300)]
        public ActionResult Test3()
        {
            ViewBag.displayDate = DateTime.Now;

            return View();
        }

        [HandleError(ExceptionType = typeof(DivideByZeroException), View = "~/Views/Error/Error1.cshtml")]
        public ActionResult Test4()
        {
            int x = 7;
            int y = 0;
            var z = x/y;
            ViewBag.error = z;
            return View();
        }
    }
}