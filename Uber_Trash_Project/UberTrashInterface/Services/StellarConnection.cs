using Newtonsoft.Json;
using System.Net.Http;
using UberTrashInterface.Entities;

namespace UberTrashInterface.Services
{
    public class StellarConnection : IStellarConnection
    {
        //Note if the code do not work it will be due to the HttpContext.
        private readonly HttpClient _httpClient;
        public StellarConnection(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<string> CreateWallet(KeyPairModel keyPairModel)
        {
            var response = await _httpClient.PostAsJsonAsync<KeyPairModel>("api/Stellar/CreateWallet", keyPairModel);// Check if the response indicates success

            // Read the content as a string
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return "Wallet created successfully";
            }
            else
            {
                Console.WriteLine(($"Error: {response.ReasonPhrase}. Details: {responseContent}"));
                return "Unable tot create wallet";
            }
        }
    }
}
