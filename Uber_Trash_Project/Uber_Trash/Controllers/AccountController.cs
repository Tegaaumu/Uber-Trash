using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uber_Trash.DTO;
using Uber_Trash.Interface;
using Uber_Trash.Logic;
using Uber_Trash.Model;
using Uber_Trash.Session;
using Uber_Trash.SMTP;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Uber_Trash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IUserRespository _userRespository { get; set; }
        public ILogInSession _LogInSession { get; set; } 
        public ApplicationDbContext applicationDbContext { get; set; }
        public AccountController(IUserRespository userRespository, ApplicationDbContext applicationDbContext, ILogInSession LogInSession)
        {
            _userRespository = userRespository;
            this.applicationDbContext = applicationDbContext;
            _LogInSession = _LogInSession;
        }

        [HttpPost("UpdateUSerSellerAgent")]
        public async Task<IActionResult> UpdateUSerSellerAgent(UserSellers userSellers)
        {// Find the existing record
            var existingSeller = await applicationDbContext.Sellers.FirstOrDefaultAsync(s => s.PublicKey == userSellers.PublicKey && s.NumberOfItemsToBeOrdered == userSellers.NumberOfItemsToBeOrdered);

            if (existingSeller == null)
            {
                return NotFound();
            }

            existingSeller.NumberOfItemsToBeOrdered = userSellers.NumberOfItemsToBeOrdered;
            existingSeller.Phone_Number = userSellers.Phone_Number;
            existingSeller.Agent = userSellers.Agent;
            existingSeller.Address = userSellers.Address;
            existingSeller.Received = userSellers.Received;

            applicationDbContext.Sellers.Update(existingSeller);
            try
            {
                await applicationDbContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine($"Status exception {ex.Message}");
            }

            return Ok();
        }

        [HttpGet("GetAllUSers")]
        public async Task<IActionResult> GetAllUSers()
        {
            var result = await applicationDbContext.Sellers.ToListAsync();
            //.Where(s => s.IsActive) // Filter active sellers
            //.OrderBy(s => s.Name)   // Order by name
            //.ToList();

            return Ok(result);
        }
        [HttpPost("UserSellers")]
        public async Task<IActionResult> Register([FromBody] UserSellers userSellers)
        {
            var findKey = await applicationDbContext.Sellers.FirstOrDefaultAsync(s =>s.PublicKey == userSellers.PublicKey);
            if (findKey != null) return BadRequest("Public Key exist already");
            var result = await applicationDbContext.Sellers.AddAsync(userSellers); 
            
            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // Optionally, rethrow the exception or handle it as needed
                throw;
            }

            return Ok("Successfull");
        }

            [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDTO)
        {
            if (registrationDTO.Username.ToLower() == "email")
            {
                await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                    new AuthenticationProperties
                    {
                        RedirectUri = Url.Action("GoogleResponse")
                    });
                return Ok("SignedIn Sucessfully");
            }
            if (registrationDTO.PublicKey != null || !String.IsNullOrEmpty(HttpContext.Session.GetString("publicKey_ForNew_USers")))
            {
                registrationDTO.PublicKey = registrationDTO.PublicKey;
            }
            if (await _userRespository.UserAlreadyExists(registrationDTO.Username)) return BadRequest("This user has already been registered. Thank You, So please login");


            var code = GenerateLogInCode.GenerateCode();

            _userRespository.Register(registrationDTO.Username, registrationDTO.Email, registrationDTO.Address!, registrationDTO.Password, registrationDTO.Phone_Number, code, registrationDTO.PublicKey, registrationDTO.AgentORUser);
            return Ok("Registered Sucessfully");
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInDTO logIn)
        {
            var user = await _userRespository.Authenticate(logIn.Username, logIn.Password);
            if (user == null) return BadRequest("User is not registered yet.");

            return Ok(user);
        }

        [HttpPost("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromBody] EmailConfirmationDTO emailConfirmation)
        {
            var confirmcode = applicationDbContext.Users.FirstOrDefault(s => s.Email_Code == emailConfirmation.Code && s.Email == emailConfirmation.Email && s.Username == emailConfirmation.UserName);
            if (confirmcode != null)
            {
                confirmcode.EmailConfirmation = true;
                await applicationDbContext.SaveChangesAsync();

                return Ok("Email has been confirmed");
            }
            return BadRequest("You either entered a wrong code or email");
        }

        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claim = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value,
            });
            var name = result.Principal.Identities.FirstOrDefault().Name;
            return Ok(claim);
        }
        [HttpPost("LogInUser")]
        public async Task<IActionResult> LogInUser(LogInUser logInUser)
        {
            var logInUsers = await applicationDbContext.Sellers.FirstOrDefaultAsync(s => s.Phone_Number == logInUser.Phone_Number);
            if (logInUser != null)
            {
                return Ok(logInUsers);
            }
            return BadRequest("You entered the wrong number");
        }

            [HttpGet("GetUrlId/{Username}")]
        //public async Task<IActionResult> ShortenUrl(string code, ApplicationDbContext applicationDbContext)
        public async Task<ActionResult<User>> GetUrlId(string Username)
        {
            var details = applicationDbContext.Users.FirstOrDefault(s => s.Username == Username);
            if (details == null)
            {
                return BadRequest("USer does not exist");
            }
            return Ok(details);
        }

            [HttpGet("GoogleLocation")]
        public async Task<IActionResult> GoogleLocation()
        {
            RootObject rootObject = getAddress.getAddressInfo(13.5270797, 77.2548046);
            Console.WriteLine("Full Address " + rootObject.display_name);
            return Ok($" Here is your address {rootObject.address}");
        }
    }
}
