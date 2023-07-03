using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test3.Models;

namespace Test3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
        {
            List<Employee> empList = new List<Employee>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Select * from Employees3", con);
                SqlDataAdapter sd = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sd.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    empList.Add(
                        new Employee
                        {
                            employeeId = Convert.ToInt32(dr["Id"]),
                            FirstName = Convert.ToString(dr["First Name"]),
                            MiddleName = Convert.ToString(dr["Middle Name"]),
                            LastName = Convert.ToString(dr["Last Name"]),
                            DOB = (DateTime)dr["DOB"],
                            MobileNumber = Convert.ToInt32(dr["Mobile Number"]),
                            address = Convert.ToString(dr["Address"]),
                            salary = Convert.ToInt32(dr["Salary"]),
                            DesignationId = Convert.ToInt32(dr["DesignationId"])
                        });
                }
                return View(empList);
            }
        }

        public ActionResult Index2()
        {
            List<Designation> desgList = new List<Designation>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Select * from Designation", con);
                SqlDataAdapter sd = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sd.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    desgList.Add(
                        new Designation
                        {
                            designationId = Convert.ToInt32(dr["Id"]),
                            DesignationName = Convert.ToString(dr["Designation"])
                        });
                }
                return View(desgList);
            }
        }
        public ActionResult CountDesignation()
        {
            List <DesignationCount> designationCounts= new List<DesignationCount>();
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select Designation, count(*) as count_no from Designation d join Employees3 e on d.Id = e.designationid group by d.Designation", con);
                SqlDataReader sdr = com.ExecuteReader();
                while(sdr.Read())
                {
                    designationCounts.Add(new DesignationCount
                    {
                        DesignationNames = (string)sdr["Designation"],
                        count = (int)sdr["count_no"]
                    });
                }
            }
            return View(designationCounts);
        }

        public ActionResult Joins()
        {
            List<Join> joins= new List<Join>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select e.Id, [First Name], [Middle Name], [Last Name], DOB, [Mobile Number], Address, Salary, Designation from Designation d join Employees3 e on d.Id = e.designationid", con);
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    joins.Add(new Join
                    {
                        employeeId = Convert.ToInt32(sdr["Id"]),
                        FirstName = Convert.ToString(sdr["First Name"]),
                        MiddleName = Convert.ToString(sdr["Middle Name"]),
                        LastName = Convert.ToString(sdr["Last Name"]),
                        DOB = (DateTime)sdr["DOB"],
                        MobileNumber = Convert.ToInt32(sdr["Mobile Number"]),
                        address = Convert.ToString(sdr["Address"]),
                        salary = Convert.ToInt32(sdr["Salary"]),
                        DesignationName = Convert.ToString(sdr["Designation"])
                    });
                }
            }
            return View(joins);

        }

        public ActionResult databaseView()
        {
            List<Join> empList= new List<Join>();
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Select * from vwGetDetails", con);
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    empList.Add(new Join
                    {
                        employeeId = Convert.ToInt32(sdr["Id"]),
                        FirstName = Convert.ToString(sdr["First Name"]),
                        MiddleName = Convert.ToString(sdr["Middle Name"]),
                        LastName = Convert.ToString(sdr["Last Name"]),
                        DOB = (DateTime)sdr["DOB"],
                        MobileNumber = Convert.ToInt32(sdr["Mobile Number"]),
                        address = Convert.ToString(sdr["Address"]),
                        salary = Convert.ToInt32(sdr["Salary"]),
                        DesignationName = Convert.ToString(sdr["Designation"])
                    });
                }
            }
            return View(empList);

        }

        public ActionResult CreateDesignation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDesignation(Designation desig)
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_AddtoDesignation", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Designation", desig.DesignationName);
                com.ExecuteNonQuery();
            }
            return RedirectToAction("Index2");
        }

        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee emp)
        {
            List<Employee> list = new List<Employee>();
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_AddtoEmployees3", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fname", emp.FirstName);
                com.Parameters.AddWithValue("@mname", emp.MiddleName);
                com.Parameters.AddWithValue("@lname", emp.LastName);
                com.Parameters.AddWithValue("dob", emp.DOB);
                com.Parameters.AddWithValue("@mobile_no", emp.MobileNumber);
                com.Parameters.AddWithValue("@address", emp.address);
                com.Parameters.AddWithValue("@salary", emp.salary);
                com.Parameters.AddWithValue("@designation_id", emp.DesignationId);
                com.ExecuteNonQuery();
            }
            return RedirectToAction("Index1");
        }

        public ActionResult EmployeeCount()
        {
            List<DesignationCount> list = new List<DesignationCount>();
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select Designation, count(*) as count_no from Designation d join Employees3 e on d.Id = e.designationid group by d.Designation having count(*) > 1", con);
                SqlDataReader sdr = com.ExecuteReader();
                while(sdr.Read())
                {
                    list.Add(new DesignationCount
                    {
                        DesignationNames = (string)sdr["Designation"],
                        count = (int)sdr["count_no"]
                    });
                }
            }
            return View(list);
        }

        public ActionResult OrderByDob()
        {
            List<Join> list = new List<Join>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("spOrderByDob", con);
                SqlDataReader sdr = com.ExecuteReader();
                while(sdr.Read())
                {
                    list.Add(new Join
                    {
                        employeeId = Convert.ToInt32(sdr["Id"]),
                        FirstName = Convert.ToString(sdr["First Name"]),
                        MiddleName = Convert.ToString(sdr["Middle Name"]),
                        LastName = Convert.ToString(sdr["Last Name"]),
                        DOB = (DateTime)sdr["DOB"],
                        MobileNumber = Convert.ToInt32(sdr["Mobile Number"]),
                        address = Convert.ToString(sdr["Address"]),
                        salary = Convert.ToInt32(sdr["Salary"]),
                        DesignationName = Convert.ToString(sdr["Designation"])
                    });
                }
            }
            return View(list);
        }
        public ActionResult OrderByFname()
        {
            List<Employee> empList = new List<Employee>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("spOrderByDesignationId 1", con);
                SqlDataAdapter sd = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sd.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    empList.Add(
                        new Employee
                        {
                            employeeId = Convert.ToInt32(dr["Id"]),
                            FirstName = Convert.ToString(dr["First Name"]),
                            MiddleName = Convert.ToString(dr["Middle Name"]),
                            LastName = Convert.ToString(dr["Last Name"]),
                            DOB = (DateTime)dr["DOB"],
                            MobileNumber = Convert.ToInt32(dr["Mobile Number"]),
                            address = Convert.ToString(dr["Address"]),
                            salary = Convert.ToInt32(dr["Salary"]),
                            DesignationId = Convert.ToInt32(dr["DesignationId"])
                        });
                }
                return View(empList);
            }
        }
        public ActionResult nonClusteredIndex()
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("create NonClustered index IX_Employees3_DesignationId_1 on Employees3 (DesignationId ASC) ", con);
                com.ExecuteNonQuery();
            }
            return View();
        }

        public ActionResult maxSalary()
        {
            List<Employee> empList = new List<Employee>();
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select [First Name] + [Last Name] as FullName, salary from Employees3 where salary in (select max(salary) from Employees3)", con);
                SqlDataReader sdr = com.ExecuteReader();
                while(sdr.Read())
                {
                    empList.Add(new Employee()
                    {
                        FirstName = (string)sdr["FullName"],
                        salary = Convert.ToInt32(sdr["salary"])
                    });
                }
            }
            return View(empList);
        }
    }
}