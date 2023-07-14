using Microsoft.AspNetCore.Mvc;
using Practical20.context;
using Practical20.Interfaces;
using Practical20.Models;
using Practical20.Repositories;
using System;

namespace Practical20.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IUnitOfWork unitOfWork, ILogger<StudentController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IActionResult> Get()
        {

            _logger.LogInformation("Get all");
            var users = await _unitOfWork.students.GetAll();
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (student.Address == null)
            {
                return BadRequest();
            }
            else
            {
                _logger.LogInformation("Student Added");
                await _unitOfWork.students.Add(student);
                await _unitOfWork.save();
                return RedirectToAction("Get");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var users = await _unitOfWork.students.GetByID(id);
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
             _unitOfWork.students.Update(student);
            await _unitOfWork.save();
            return RedirectToAction("Get");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var users = await _unitOfWork.students.GetByID(id);
            return View(users);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            _logger.LogInformation("Data deleted");
            var users = await _unitOfWork.students.GetByID(id);
            _unitOfWork.students.Delete(users);
            await _unitOfWork.save();
            return RedirectToAction("Get");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            throw new Exception("Error");
            var users = await _unitOfWork.students.GetByID(id);
            return View(users);
        }
    }
}
