using Microsoft.AspNetCore.Mvc;
using Uber_Trash.Logic;
using Uber_Trash.Model;
using Uber_Trash.Stellar;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Uber_Trash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StellarController : ControllerBase
    {
        public ApplicationDbContext _applicationDbContext { get; set; }
        // GET: api/<StellarController>
        public StellarController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }
        [HttpPost("SendFunds")]
        public async Task<IActionResult> SendFunds([FromBody] StellarSendFunds stellarSendFunds)
        {
            var result = await StellarRespository.SendFunds(stellarSendFunds);
            if (result == "Successfull")
            {
                return Ok("Sent funds successfully");
            }
            return BadRequest("Unable to send fund");
        }
        [HttpPost("Successfull Transaction")]
        public async Task<IActionResult> GetPayment(StellarAccountDetails stellarAccountDetails)
        {
             StellarRespository.LoadServer(stellarAccountDetails);
            //if (result.IsCompleted && result != null)
            //{
                return Ok("Sent funds successfully");
            //}
            //return BadRequest("Unable to send fund");
        }
        [HttpPost("CreateWallet")]
        public async Task<IActionResult> CreateWallet(KeyPairModel keyPairModel)
        {
            var result = await _applicationDbContext.KeyPairModel.AddAsync(keyPairModel);
            await _applicationDbContext.SaveChangesAsync();

            return Ok("Sent funds successfully");
        }
    }
}
