using Practical11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical11.Controllers
{
    public class StudentController : Controller
    {
        static List<Student> studentList = new List<Student>()
        {
            new Student() { StudentId = 1, StudentName = "John", DOB = "07-05-2002", Address = "UK" } ,
            new Student() { StudentId = 2, StudentName = "Steve", DOB = "08-09-2001", Address = "US" } ,
            new Student() { StudentId = 3, StudentName = "Bill",  DOB = "11-09-2002", Address = "UK" } ,
            new Student() { StudentId = 4, StudentName = "Ram" , DOB = "01-11-2000" , Address = "India"} ,
        };
        // GET: Student
        public ActionResult Index()
        {
            return View(studentList);
        }
        public ActionResult Edit(int Id)
        {
            var std = studentList.Where(s => s.StudentId == Id).FirstOrDefault();

            return View(std);
        }
        [HttpPost]
        public ActionResult Edit(Student std)
        {

            var student = studentList.Where(s => s.StudentId == std.StudentId).FirstOrDefault();
            //student.StudentId= std.StudentId;
            student.StudentName = std.StudentName;
            student.DOB = std.DOB;
            student.Address = std.Address;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var std = studentList.Find(s => s.StudentId == Id);

            return View(std);
        }
        [HttpPost]
        public ActionResult Delete(Student student)
        {
            var student1 = studentList.Find(s => s.StudentId == student.StudentId);
            studentList.Remove(student1);

            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            var std = studentList.Where(s => s.StudentId == Id).FirstOrDefault();

            return View(std);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            int id = studentList.Count + 1;
            var std = new Student()
            {
                StudentId = id,
                StudentName = student.StudentName,
                DOB = student.DOB,
                Address = student.Address
            };
            studentList.Add(std);
            return RedirectToAction("Index");
        }
    }
}