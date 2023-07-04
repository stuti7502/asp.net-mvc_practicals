using Practical13.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical13.Controllers
{
    public class HomeController : Controller
    {
        EmployeeContext db = new EmployeeContext();
        public ActionResult Index()
        {
            var data = db.Employees.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            
            db.Employees.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var emp = db.Employees.Where(s => s.Id == Id).FirstOrDefault();

            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            db.Employees.AddOrUpdate(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var std = db.Employees.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }
        [HttpPost]
        public ActionResult Delete(Employee emp)
        {
            var student1 = db.Employees.Where(s => s.Id == emp.Id).FirstOrDefault();
            db.Employees.Remove(student1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var emp = db.Employees.FirstOrDefault(s => s.Id == id);
            return View(emp);
        }
    }
}