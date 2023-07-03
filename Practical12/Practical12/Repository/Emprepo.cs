using Practical12.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Practical12.Repository
{
    public class Emprepo
    {

        public List<Employee> GetEmployees()
        {
           
            List<Employee> EmpList = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand("select * from Employee", conn);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                conn.Open();
                da.Fill(dt);
                conn.Close();
                    
                foreach (DataRow dr in dt.Rows)
                {

                    EmpList.Add(

                            new Employee
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                FirstName = Convert.ToString(dr["First Name"]),
                                MiddleName = Convert.ToString(dr["Middle Name"]),
                                LastName = Convert.ToString(dr["Last Name"]),
                                DOB = (DateTime)dr["DOB"],
                                MobileNumber = Convert.ToInt32(dr["Mobile Number"]),
                                address = Convert.ToString(dr["Address"])
                            }
                            );
                }

                return EmpList;
            }

        }

        public void InsertRecord(Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand($"insert into Employee values('{emp.FirstName}', '{emp.MiddleName}', '{emp.LastName}', '{emp.DOB}', '{emp.MobileNumber}', '{emp.address}')", conn);
                com.ExecuteNonQuery();
            }

        }

        public List<Employee> changeFirstPerson(Employee emp)
        {
            List<Employee> EmpList = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand($"update Employee set [First Name] = 'SQLPerson' where Id in (select top(1) id from Employee)", conn);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                
                da.Fill(dt);
                conn.Close();
                     
                foreach (DataRow dr in dt.Rows)
                {

                    EmpList.Add(

                            new Employee
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                FirstName = Convert.ToString(dr["First Name"]),
                                MiddleName = Convert.ToString(dr["Middle Name"]),
                                LastName = Convert.ToString(dr["Last Name"]),
                                DOB = (DateTime)dr["DOB"],
                                MobileNumber = Convert.ToInt32(dr["Mobile Number"]),
                                address = Convert.ToString(dr["Address"])
                            }
                            );
                }

                return EmpList;
            }
        }

        public List<Employee> changeMiddleName(Employee emp)
        {
            List<Employee> EmpList = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand($"update Employee set [Middle Name] = 'I' ", conn);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();


                da.Fill(dt);
                conn.Close();
                    
                foreach (DataRow dr in dt.Rows)
                {

                    EmpList.Add(

                            new Employee
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                FirstName = Convert.ToString(dr["First Name"]),
                                MiddleName = Convert.ToString(dr["Middle Name"]),
                                LastName = Convert.ToString(dr["Last Name"]),
                                DOB = (DateTime)dr["DOB"],
                                MobileNumber = Convert.ToInt32(dr["Mobile Number"]),
                                address = Convert.ToString(dr["Address"])
                            }
                            );
                }

                return EmpList;
            }
        }

        
    }
}