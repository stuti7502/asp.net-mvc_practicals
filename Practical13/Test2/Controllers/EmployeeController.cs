using Practical13.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test2.Models;

namespace Test2.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeContext db = new EmployeeContext();
        // GET: Employee
        public ActionResult Create()
        {
            ViewBag.DesignationID = new SelectList(db.Designations, "Id", "DesignationName"); 
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee2 emp)
        {

            db.Employees.Add(emp);
            db.SaveChanges();
            return RedirectToAction("EmployeeData", "Home");
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.DesignationID = new SelectList(db.Designations, "Id", "DesignationName");
            var emp = db.Employees.Where(s => s.Id == Id).FirstOrDefault();

            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee2 emp)
        {
            db.Employees.AddOrUpdate(emp);
            db.SaveChanges();
            return RedirectToAction("EmployeeData", "Home");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var std = db.Employees.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }
        [HttpPost]
        public ActionResult Delete(Employee2 emp)
        {
            var student1 = db.Employees.Where(s => s.Id == emp.Id).FirstOrDefault();
            db.Employees.Remove(student1);
            db.SaveChanges();
            return RedirectToAction("EmployeeData", "Home");
        }
    }
}