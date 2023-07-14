using Microsoft.AspNetCore.Mvc;
using Practical18.Models;

namespace Practical18.Controllers
{
    [ApiController]
    [Route("api/StudentController")]
    public class StudentController : Controller
    {
        private readonly StudentDbContext context;
        public StudentController(StudentDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(context.Students.ToList());
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetStudent([FromRoute] Guid id)
        {
            var student = context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public IActionResult AddStudent(AddContactRequest addContactRequest)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Address = addContactRequest.Address,
                Age = addContactRequest.Age
            };
            context.Students.Add(student);
            context.SaveChanges();
            return Ok(student);
            
            
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateStudent([FromRoute] Guid id, UpdateStudentRequest updateStudentRequest)
        {
            var student = context.Students.Find(id);
            if(student != null)
            {
                student.Name = updateStudentRequest.Name;
                student.Address = updateStudentRequest.Address;
                student.Age = updateStudentRequest.Age;

                context.SaveChanges();

                return Ok(student);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteStudent([FromRoute] Guid id)
        {
            var student = context.Students.Find(id);
            if(student != null)
            {
                context.Remove(student);
                context.SaveChanges();
                return Ok(student);
            }
            return NotFound();
        }
    }
}
