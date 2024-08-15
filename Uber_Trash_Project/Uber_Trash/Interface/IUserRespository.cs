using Uber_Trash.Model;

namespace Uber_Trash.Interface
{
    public interface IUserRespository
    {
        Task<User> Authenticate(string userName, string passwordText);
        void Register(string userName, string email, string address, string password, string phoneNumber, string code, string publicKey, string agentOrUser = "nutfixed");
        Task<bool> UserAlreadyExists(string userName);
        bool MacthPasswordHash(string passwordText, byte[] password, byte[] passwordKey);
    }
}