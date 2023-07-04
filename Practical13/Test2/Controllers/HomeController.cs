using Practical13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Test2.Models;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        EmployeeContext db = new EmployeeContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EmployeeData()
        {
            var data = db.Employees.ToList();
            return View(data);
        }
        public ActionResult DesignationData()
        {
            var data = db.Designations.ToList();
            return View(data);
        }
        public ActionResult LinqQuery()
        {
            var QuerySyntax = (from emp in db.Employees
                               join des in db.Designations
                               on emp.DesignationID equals des.Id
                               select new ViewModel
                               {
                                  employee2= emp,
                                  designation = des
                               }).ToList();
            return View(QuerySyntax);
        }
        public ActionResult CountEmployee()
        {
            var QuerySyntax = (from des in db.Employees
                               group des by des.Designation.DesignationName into designame
                               select new ViewModel
                               {
                                   desg = designame.Key,
                                   count = designame.Count()
                               }).ToList();
            return View(QuerySyntax);
        }
    }
}