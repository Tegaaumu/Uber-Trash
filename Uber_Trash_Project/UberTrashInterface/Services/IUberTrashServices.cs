using Microsoft.AspNetCore.Mvc;
using UberTrashInterface.Entities;

namespace UberTrashInterface.Services
{
    public interface IUberTrashServices
    {
        Task<User> InputDetails(User code);
        Task<UserSellers> UserSellers(UserSellers userSellers);
        Task<List<UserSellers>> GetAllSellersAsync();
        Task<UserSellers> LogInUser(LogInUser logInUser);
        Task<bool> UpdateUSerSellerAgent(UserSellers userSellers);
        Task<bool> SendFund(AgentTransaction agentTransaction);
        Task<string> ConfirmEmail(EmailConfirmation emailConfirmation);
        Task<User> LogIn(LogIn logIn);
    }
}
