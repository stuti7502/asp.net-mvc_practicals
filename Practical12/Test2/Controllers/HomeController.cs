using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Practical12.Models;
using System.Reflection;
using System.Xml.Linq;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult GetAllEmployees()
        {
            List<Employee> empList = new List<Employee>();
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from employee2", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    empList.Add(
                        new Employee
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            FirstName = Convert.ToString(dr["First Name"]),
                            MiddleName = Convert.ToString(dr["Middle Name"]),
                            LastName = Convert.ToString(dr["Last Name"]),
                            DOB = (DateTime)dr["DOB"],
                            MobileNumber = Convert.ToInt32(dr["Mobile Number"]),
                            address = Convert.ToString(dr["Address"]),
                            salary = Convert.ToInt32(dr["Salary"])
                        }

                    );
                }
            }
            return View(empList);
        }
        public ActionResult TestRecord()
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd= new SqlCommand($"insert into Employee2 values('Stuti', 'N', 'Vithlani', '2002-05-07', 77777777, 'Jamnagar', 50000), ('Prisha', 'P', 'Amlani', '2004-05-03', 4654777, 'Ahmedabad', 45000), ('Likita',null , 'Rai', '2001-11-01', 89577777, 'Vadodara', 40000), ('Hetvi', null,'Vithlani', '2002-08-09', 4658468, 'Jamnagar', 35000)", con);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("GetAllEmployees");
        }
        public ActionResult TotalAmount()
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select sum(salary) from Employee2", con);
                var total = cmd.ExecuteScalar();
                TempData["Total"] = "Total Salary" + total;
            }
            return RedirectToAction("GetAllEmployees");
        }
        
        public ActionResult DOB()
        {
            List<Employee> empList = new List<Employee>();
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Employee2 where DOB < '2000-01-01'", con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    empList.Add(
                        new Employee
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            FirstName = Convert.ToString(dr["First Name"]),
                            MiddleName = Convert.ToString(dr["Middle Name"]),
                            LastName = Convert.ToString(dr["Last Name"]),
                            DOB = (DateTime)dr["DOB"],
                            MobileNumber = Convert.ToInt32(dr["Mobile Number"]),
                            address = Convert.ToString(dr["Address"]),
                            salary = Convert.ToInt32(dr["Salary"])

                        });
                }
                return View(empList);
            }
        }
        public ActionResult ContMiddleName()
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from Employee2 where [Middle Name] is Null", con);
                var midname = cmd.ExecuteScalar();
                TempData["MiddleName"] = "Count " + midname;
            }
            return RedirectToAction("GetAllEmployees");
        }
       
    }
}