using Practical17.Models;

namespace Practical17.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        public void AddUser(User user);
    }
}
