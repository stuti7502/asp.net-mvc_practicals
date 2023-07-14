using Microsoft.EntityFrameworkCore;
using Practical20.context;
using Practical20.Interfaces;

namespace Practical20.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContextClass _dbContext;
        public GenericRepository(DbContextClass dbContextClass)
        {
            _dbContext = dbContextClass;
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByID(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
