using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practical18.Models;
using Practical18_2.Models;
using System.Diagnostics;

namespace Practical18_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<StudentViewModel> students = new List<StudentViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7126");
            HttpResponseMessage response = await client.GetAsync("api/StudentController");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<StudentViewModel>>(res);
            }
            return View(students);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            StudentViewModel student = await GetStudentById(id);
            return View(student);
        }

        private static async Task<StudentViewModel> GetStudentById(Guid id)
        {
            StudentViewModel student = new StudentViewModel();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7126/");
            HttpResponseMessage response = await client.GetAsync($"api/StudentController/{id}");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentViewModel>(res);
            }

            return student;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel student)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7126/");
                var response = await client.PostAsJsonAsync<StudentViewModel>("api/StudentController", student);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            StudentViewModel student = new StudentViewModel();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7126/");
            HttpResponseMessage response = await client.DeleteAsync($"api/StudentController/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            StudentViewModel student = await GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel student, Student stu)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7126/");
            var response = await client.PutAsJsonAsync<StudentViewModel>($"api/StudentController/{stu.Id}", student);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Privacy()
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