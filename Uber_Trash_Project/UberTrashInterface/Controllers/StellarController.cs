using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using StellarDotnetSdk.Accounts;
using UberTrashInterface.Entities;
using UberTrashInterface.Services;

namespace UberTrashInterface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StellarController : ControllerBase
    {
        public IStellarConnection _stellarConnection { get; set; }
        public StellarController(IStellarConnection stellarConnection)
        {
            _stellarConnection = stellarConnection;
        }

            [HttpPost("Connect")]
        public async Task<IActionResult> Connect([FromBody] ConnectWallet connectWallet)
        {
            HttpContext.Session.Remove("publicKey_ForNew_USers");
            var result = !string.IsNullOrEmpty(connectWallet.PublicKey) || connectWallet.PublicKey != null && connectWallet.Error == null;
            if (result)
            {
                HttpContext.Session.SetString("publicKey_ForNew_USers", connectWallet.PublicKey);
                return RedirectToAction("Index", "Home");
            }
            return BadRequest("The public key is not valid");

        }

            [HttpPost("SaveKeys")]
        public async Task<IActionResult> SaveKeys([FromBody] KeyPairModel keyPair)
        {

            HttpContext.Session.Remove("publicKey_ForNew_USers");
            HttpContext.Session.SetString("publicKey_ForNew_USers", keyPair.PublicKey);
            // Process the keys (e.g., save to database)
            Console.WriteLine($"Public Key: {keyPair.PublicKey}");
            Console.WriteLine($"Secret Key: {keyPair.SecretKey}");
            Console.WriteLine($"Secret Key: {keyPair.Balance}");
            var stellar = await _stellarConnection.CreateWallet(keyPair); 
            
            if (stellar == "Wallet created successfully")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return BadRequest("Service was not successful: " + stellar);
            }

        }
    }
}
