using Microsoft.AspNetCore.Mvc;
using Practical17.Models;

namespace Practical17.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        public Student GetDetails(int id);
        public void AddStudent(Student std);
        public void Delete(int id);
        public void Edit(Student std);
    }
}
