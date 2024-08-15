using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using UberTrashInterface.Entities;

namespace UberTrashInterface.Services
{
    public class UberTrashServices : IUberTrashServices
    {
        //Note if the code do not work it will be due to the HttpContext.
        private readonly HttpClient _httpClient;

        public UberTrashServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> ConfirmEmail(EmailConfirmation emailConfirmation)
        {
            if (_httpClient == null)
            {
                throw new InvalidOperationException("httpClient is not initialized.");
            }
            var queryParams = new Dictionary<string, string>
            {
                { "email", emailConfirmation.Email },
                { "code", emailConfirmation.Code },
                { "username", emailConfirmation.UserName }
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync<EmailConfirmation>("api/Account/EmailConfirmation", emailConfirmation);

                if (response.IsSuccessStatusCode)
                {
                    // If the response is successful (status code 2xx)
                    var next = "Email has been confirmed";
                    return next;
                }
                //return await _httpClient.GetFromJsonAsync<EmailConfirmation>($"api/Account/EmailConfirmation/{emailConfirmation}");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"EmailCorfirmation error: {httpRequestException.Message}");
            }
            return "Email has not yet been Confrimed";
        }
        public async Task<User> LogIn(LogIn logIn)
        {
           var response = await _httpClient.PostAsJsonAsync<LogIn>("api/Account/LogIn", logIn);// Check if the response indicates success

            // Read the content as a string
            var responseContent = await response.Content.ReadAsStringAsync(); 
            if (response.IsSuccessStatusCode)
            {

                // Deserialize the content into a LogIn object
                var result = JsonConvert.DeserializeObject<User>(responseContent);

                // Return the deserialized LogIn object
                return result;
            }
            else
            {
                // Handle the error (e.g., log it, throw an exception, return null, etc.)
                // You could throw an exception or return a default value like null
                Console.WriteLine(($"Error: {response.ReasonPhrase}. Details: {responseContent}"));
                return null;
            }
        }


        public async Task<User> InputDetails(User registrationDTO)
        {
            try
            {
                if (_httpClient == null)
                {
                    throw new InvalidOperationException("httpClient is not initialized.");
                }

                var response = await _httpClient.PostAsJsonAsync<User>("api/Account", registrationDTO);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    //var Code = responseContent.Split("/").Last();
                    var Username = registrationDTO.Username;
                    return registrationDTO;
                }
                else
                {
                    // Log the error response for further analysis
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response: {response.StatusCode}, Content: {errorContent}");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"Request error: {httpRequestException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            return null!;
        }

        public async Task<UserSellers> UserSellers(UserSellers userSellers)
        {
            try
            {
                if (_httpClient == null)
                {
                    throw new InvalidOperationException("httpClient is not initialized.");
                }

                var response = await _httpClient.PostAsJsonAsync<UserSellers>("api/Account/UserSellers", userSellers);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return userSellers;
                }
                else
                {
                    // Log the error response for further analysis
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response: {response.StatusCode}, Content: {errorContent}");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"Request error: {httpRequestException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            return null!;
        }
        public async Task<bool> UpdateUSerSellerAgent(UserSellers userSellers)
        {
            var response = await _httpClient.PostAsJsonAsync<UserSellers>("api/Account/UpdateUSerSellerAgent", userSellers);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> SendFund(AgentTransaction agentTransaction)
        {
            var response = await _httpClient.PostAsJsonAsync<AgentTransaction>("api/Stellar/SendFunds", agentTransaction);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return true;
            }
            return false;

        }
            public async Task<List<UserSellers>> GetAllSellersAsync()
        {
            var response = await _httpClient.GetAsync("api/Account/GetAllUsers").ConfigureAwait(false);
            if (response.ReasonPhrase != "No Content")
            {
                response.EnsureSuccessStatusCode();

                var sellers = await response.Content.ReadFromJsonAsync<List<UserSellers>>().ConfigureAwait(false);
                return sellers;
            }
            return null;
        }

        public async Task<UserSellers> LogInUser(LogInUser logInUser)
        {
            var response = await _httpClient.PostAsJsonAsync<LogInUser>("api/Account/LogInUser", logInUser);
            if (response.ReasonPhrase != "No Content")
            {
                response.EnsureSuccessStatusCode();

                var sellers = await response.Content.ReadFromJsonAsync<UserSellers>().ConfigureAwait(false);
                return sellers;
            }
            return null;
        }
    }
}
