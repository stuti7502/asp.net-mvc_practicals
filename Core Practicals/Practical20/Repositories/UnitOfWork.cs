using Microsoft.EntityFrameworkCore;
using Practical20.context;
using Practical20.Interfaces;

namespace Practical20.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass dbContext;
        public IStudentRepository students { get; }

        public UnitOfWork(DbContextClass dbContext, IStudentRepository student)
        {
            this.dbContext = dbContext;
            this.students = student;
        }
        public async Task save()
        {
            await dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }

       

    }
}
