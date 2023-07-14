using Practical17.Interfaces;
using Practical17.Models;

namespace Practical17.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
                _context = context;
        }
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        
        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }
        public void Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.StudentId == id);
            _context.Students.Remove(student); 
            _context.SaveChanges();
        }

        public void Edit(Student std)
        {
            var student = _context.Students.Where(s => s.StudentId == std.StudentId).FirstOrDefault();
            
            student.FirstName = std.FirstName;
            student.LastName = std.LastName;
            student.Address = std.Address;
            student.Age= std.Age;
            _context.SaveChanges();
        }
        
        public Student GetDetails(int id)
        {
            return _context.Students.FirstOrDefault(x=>x.StudentId==id);
        } 
    }
}
