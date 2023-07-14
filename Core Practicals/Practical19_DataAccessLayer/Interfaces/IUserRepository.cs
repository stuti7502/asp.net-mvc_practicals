using Practical19_DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical19_DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
        Task<UserManagerResponse> LogoutUserAsync(Logout model);
        Task<IEnumerable<RegisteredUser>> GetUsers();
    }
}
