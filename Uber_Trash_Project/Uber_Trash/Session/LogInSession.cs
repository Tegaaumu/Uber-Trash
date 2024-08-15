
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uber_Trash.DTO;
using Uber_Trash.Interface;
using Uber_Trash.Logic;
using Uber_Trash.Model;

namespace Uber_Trash.Session
{
    public class LogInSession : ILogInSession
    {
        private ApplicationDbContext _applicationDbContext;
        private IUserRespository _userRespository;
        public LogInSession(ApplicationDbContext applicationDbContext, IUserRespository userRespository)
        {
            _applicationDbContext = applicationDbContext;
            _userRespository= userRespository;
        }
        public async Task<User> LogIn(string username, string password)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(s => s.Username == username);
            if(!_userRespository.MacthPasswordHash(password, user.Password, user.PasswordKey)) return null;
            return user;
            //return OkObjectResult(await _applicationDbContext.Users.FirstOrDefault(s => s.Username == username));
            //return _applicationDbContext.Users.FirstOrDefault(s => s.Username == username && s.Password == password);
        }
    }
}
