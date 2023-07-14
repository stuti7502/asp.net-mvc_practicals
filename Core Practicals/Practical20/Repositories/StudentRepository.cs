using Practical20.context;
using Practical20.Interfaces;
using Practical20.Models;

namespace Practical20.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DbContextClass dbContextClass) : base(dbContextClass)
        {
        }
    }
}
