using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Reflection;
using UberTrashInterface.Entities;
using UberTrashInterface.Models;
using UberTrashInterface.Services;

namespace UberTrashInterface.Controllers
{
    public class AgentController : Controller
    {
        public IUberTrashServices _UberTrashServices;
        public AgentController(IUberTrashServices UberTrashServices)
        {
            _UberTrashServices = UberTrashServices;
        }
        // GET: AgentController1
        [HttpGet]
        public ActionResult UserSellers()
        {
            return View();
        }
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserSellers(UserSellers userSellers)
        {
            if (userSellers == null)
            {
                return RedirectToAction(nameof(Post));
            }
            if (ModelState.IsValid)
            {
                var newSeller = new UserSellers{
                    PublicKey = HttpContext.Session.GetString("publicKey_ForNew_USers"),
                    Address = userSellers.Address,
                    Phone_Number = userSellers.Phone_Number,
                    NumberOfItemsToBeOrdered = userSellers.NumberOfItemsToBeOrdered
                };
                var UrlCollection = await _UberTrashServices.UserSellers(newSeller);

                //Write a code that will get all the propertys of the particular Url
                //var FinalUrlCollection = await UrlServices.GetUrlId(UrlCollection);
                if (UrlCollection != null)
                {
                    ViewBag.Message = "Details has been loggedIn";
                    HttpContext.Session.SetString("phoneNumber", userSellers.Phone_Number);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Message = "Unable to place an order, public key exist in database";
                return View(UrlCollection);
            }
            return View(userSellers);
        }

        [HttpGet]
        public ActionResult Post()
        {
            return View();
        }
        // POST: AgentController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(User user)
        {
            if (user == null)
            {
                return RedirectToAction(nameof(Post));
            }
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Email = user.Email,
                    Address = user.Address,
                    Username = user.Username,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    AgentORUser = "Agent",
                    PublicKey = HttpContext.Session.GetString("publicKey_ForNew_USers")
                };
                var UrlCollection = await _UberTrashServices.InputDetails(newUser);

                //Write a code that will get all the propertys of the particular Url
                //var FinalUrlCollection = await UrlServices.GetUrlId(UrlCollection);
                if (UrlCollection != null)
                {
                    HttpContext.Session.SetString("username", user.Username);
                    // Assuming UrlCollection contains Email and Code properties
                    var emailConfirmation = new EmailConfirmation
                    {
                        Email = UrlCollection.Email,
                        UserName = UrlCollection.Username
                    };

                    return RedirectToAction("EmailConfrimation", "Agent", new { email = emailConfirmation.Email, username = emailConfirmation.UserName });
                }
                else
                {
                    ViewBag.ErrorMessages = "The username is in use or Invalid mail";
                }
                return View(UrlCollection);
            }
            // Collect all error messages
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            ViewBag.ErrorMessages = errorMessages;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> SendFund(UserSellers userSellers)
        {
            var agentName = HttpContext.Session.GetString("publicKey_ForNew_USers");
            var AgentTransaction = new AgentTransaction()
            {
                RecieverPublicKey = userSellers.PublicKey,
                amount = userSellers.NumberOfItemsToBeOrdered.ToString(),
                SenderSecretKey = userSellers.AgentSecertKey,
                SenderPublicKey = agentName
            };
            var result = await _UberTrashServices.SendFund(AgentTransaction);
            if (result)
            {
                userSellers.Received = true;
                var resultx = await _UberTrashServices.UpdateUSerSellerAgent(userSellers);
                ViewBag.Message = "Payment was successull";
            }
            else
            {
                ViewBag.Message = "Transaction failed maybe due to wrong secret key";

            }
            return RedirectToAction("Index", "Home");

        }

        // GET: AgentController1
        [HttpGet("EmailConfrimation")]
        public IActionResult EmailConfrimation(string email, string username)
        {

            if (string.IsNullOrEmpty(email))
            {
                // Handle the case where email is null or empty
                return RedirectToAction("Error");
            }// Assuming UrlCollection contains Email and Code properties
            var emailConfirmation = new EmailConfirmation
            {
                Email = email,
                UserName = username
            };
            return View(emailConfirmation);
        }
        [HttpPost("EmailConfrimation")]
        public async Task<IActionResult> EmailConfrimation(EmailConfirmation emailConfirmation)
        {
            if (ModelState.IsValid)
            {
                var UrlCollection = await _UberTrashServices.ConfirmEmail(emailConfirmation);
                ViewBag.EmailConfirmation = UrlCollection;
                return View(emailConfirmation);

            }
            return BadRequest("Enter value");
        }

        [HttpGet]
        public async Task<IActionResult> LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogIn logIn)
        {
            var account = await _UberTrashServices.LogIn(logIn);
            if (account != null)
            {
                HttpContext.Session.SetString("username", logIn.Username);
                HttpContext.Session.SetString("publicKey_ForNew_USers", account.PublicKey!);
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Login, Check your username or email";
                return View();
            }
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("publicKey_ForNew_USers");
            return RedirectToAction("Index", "Home");
        }
    }
}
