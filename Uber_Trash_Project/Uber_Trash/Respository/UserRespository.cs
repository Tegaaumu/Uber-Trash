
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Uber_Trash.DTO;
using Uber_Trash.Interface;
using Uber_Trash.Logic;
using Uber_Trash.Model;
using Uber_Trash.SMTP;

namespace UberTrash.Respository
{
    public class UserRespository : IUserRespository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public IEmailSender _emailSender { get; set; }
        public UserRespository(ApplicationDbContext applicationDbContext, IEmailSender emailSender)
        {
            this.applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
        }
        public async Task<User> Authenticate(string userName, string passwordText)
        {
            var user = await applicationDbContext.Users.FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null || user.PasswordKey == null) return null;

            if (!MacthPasswordHash(passwordText, user.Password, user.PasswordKey)) return null;

            return user;

        }

        public bool MacthPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {

            using (var hmac = new HMACSHA512(passwordKey))
            {
               var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i]) return false;
                }
                return true;
            }
        }

        public async void Register(string userName, string email, string address, string password, string phoneNumber, string code, string publicKey, string agentOrUser = "nutfixed")
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            User user = new User()
            {
                Username = userName,
                Password = passwordHash,
                PasswordKey = passwordKey,
                Email = email,
                Address = address,
                Phone_Number = phoneNumber,
                Email_Code = code,
                PublicKey = publicKey,
                AgentORUser = agentOrUser
            };
            await applicationDbContext.Users.AddAsync(user);
            _emailSender.SendEmailAsync(email, "Confirmation", "Completed", code);
            applicationDbContext.SaveChanges();

            //UnComment this code later.

        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await applicationDbContext.Users.AnyAsync(x => x.Username == userName);
        }
    }
}
