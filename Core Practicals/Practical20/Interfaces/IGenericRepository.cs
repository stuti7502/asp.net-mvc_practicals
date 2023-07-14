namespace Practical20.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByID(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
