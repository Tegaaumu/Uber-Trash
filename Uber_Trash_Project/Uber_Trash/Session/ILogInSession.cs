
using Uber_Trash.DTO;
using Uber_Trash.Model;

namespace Uber_Trash.Session
{
    public interface ILogInSession
    {
        public Task<User> LogIn(string username, string password);
    }
}
