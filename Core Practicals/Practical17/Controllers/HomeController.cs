using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practical17.Interfaces;
using Practical17.Models;
using System.Diagnostics;

namespace Practical17.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IStudentRepository studentRepository;
        private readonly IUserRepository userRepository;

        public HomeController(IStudentRepository studentRepo, IUserRepository userRepo)
        {
            //_logger = logger;
            studentRepository = studentRepo;
            userRepository = userRepo;
        }


        [Authorize(Roles = "User, Admin")]
        public IActionResult GetAll()
        {
            var studentList = studentRepository.GetAll(); 
            return View(studentList);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddStudent(Student student)
        {
            studentRepository.AddStudent(student);
            return RedirectToAction("GetAll", "Home");
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            userRepository.AddUser(user);
            return RedirectToAction("GetAll", "Home");
        }
        [Authorize]
        public IActionResult GetStudentDetails(int id)
        {
            return View(studentRepository.GetDetails(id));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteStudent(int id)
        {
            var std = studentRepository.GetDetails(id);

            return View(std);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteStudent(Student student)
        {
            studentRepository.Delete(student.StudentId); 
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditStudent(int id)
        {
            var std = studentRepository.GetDetails(id);

            return View(std);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.Edit(student);
                return RedirectToAction("GetAll");
            }
            return View();
        }
        [Authorize]
        public IActionResult AuthorizedPage()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}