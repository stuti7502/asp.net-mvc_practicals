using Practical12.Models;
using Practical12.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical12.Controllers
{
    public class EmployeeController : Controller
    {
        
        // GET: Employee
        public ActionResult GetAllEmployees()
        {
            Emprepo emc = new Emprepo();
            return View(emc.GetEmployees());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Emprepo empRepo = new Emprepo();

                    empRepo.InsertRecord(emp);

                }

                return RedirectToAction("GetAllEmployees");
            }
            catch
            {
                return RedirectToAction("GetAllEmployees");
            }


        }
        public ActionResult TestRecords(Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand($"insert into Employee values('Stuti', 'N', 'Vithlani', '2002-05-07', 77777777, 'Jamnagar'), ('Prisha', 'P', 'Amlani', '2004-05-03', 4654777, 'Ahmedabad'), ('Likita', 'S', 'Rai', '2001-11-01', 89577777, 'Vadodara')", conn);
                com.ExecuteNonQuery();
            }
            return RedirectToAction("GetAllEmployees");
        }

        public ActionResult firstPersonUpdate(Employee emp)
        {
            if (ModelState.IsValid)
            {
                Emprepo empRepo = new Emprepo();

                empRepo.changeFirstPerson(emp);

            }
            return RedirectToAction("GetAllEmployees");
        }

        public ActionResult MiddleNameUpdate(Employee emp)
        {
            if (ModelState.IsValid)
            {
                Emprepo empRepo = new Emprepo();

                empRepo.changeMiddleName(emp);

            }
            return RedirectToAction("GetAllEmployees");
        }
        public ActionResult DeleteRecord(Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand($"delete from employee where Id < 2", conn);
                com.ExecuteNonQuery();
            }
            return RedirectToAction("GetAllEmployees");
        }

        public ActionResult DeleteAllRecord(Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand($"truncate table employee", conn);
                com.ExecuteNonQuery();
            }
            return RedirectToAction("GetAllEmployees");
        }
    }
    
}