namespace Practical20.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository students { get; }
        Task save();
    }
}
