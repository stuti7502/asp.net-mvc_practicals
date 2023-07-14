using Microsoft.EntityFrameworkCore;
using Practical17.Interfaces;
using Practical17.Models;

namespace Practical17.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<User> GetAll()
        {
            return _context.Users.Include(u => u.Roles).ToList();
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
