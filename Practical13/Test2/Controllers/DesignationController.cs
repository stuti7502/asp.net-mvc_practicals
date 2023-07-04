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
    public class DesignationController : Controller
    {
        // GET: Designation
        EmployeeContext db = new EmployeeContext();
        // GET: Employee
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Designation des)
        {

            db.Designations.Add(des);
            db.SaveChanges();
            return RedirectToAction("DesignationData", "Home");
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.DesignationID = new SelectList(db.Designations, "Id", "DesignationName");
            var emp = db.Designations.Where(s => s.Id == Id).FirstOrDefault();

            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Designation des)
        {
            db.Designations.AddOrUpdate(des);
            db.SaveChanges();
            return RedirectToAction("DesignationData", "Home");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var std = db.Designations.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }
        [HttpPost]
        public ActionResult Delete(Designation des)
        {
            var employee1 = db.Designations.Where(s => s.Id == des.Id).FirstOrDefault();
            db.Designations.Remove(employee1);
            db.SaveChanges();
            return RedirectToAction("DesignationData", "Home");
        }
    }
}