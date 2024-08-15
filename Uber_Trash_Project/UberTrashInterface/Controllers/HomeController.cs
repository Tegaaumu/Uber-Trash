using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UberTrashInterface.Entities;
using UberTrashInterface.Models;
using UberTrashInterface.Services;

namespace UberTrashInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IUberTrashServices _UberTrashServices;
        public UserSellers? _logInUser1 = new UserSellers();

        public HomeController(ILogger<HomeController> logger, IUberTrashServices UberTrashServices, UserSellers? logInUser1)
        {
            _logger = logger;
            _UberTrashServices = UberTrashServices;
            _logInUser1 = logInUser1;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userSeller = await _UberTrashServices.GetAllSellersAsync();
                ViewBag.UserSellers = userSeller;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {ex.Message}");
            }
            ViewBag.SessionName = HttpContext.Session.GetString("username");
            ViewBag.PublicKey = HttpContext.Session.GetString("publicKey_ForNew_USers");
            if(HttpContext.Session.GetString("phoneNumber") != null)
            {
                var logInUser = new LogInUser
                {
                    Phone_Number = HttpContext.Session.GetString("phoneNumber")
                };
                var UserDetails = await _UberTrashServices.LogInUser(logInUser);
                ViewBag.UserDetails = UserDetails;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LogInUser()
        {
            return View();
        }

            [HttpPost]
        public async Task<IActionResult> LogInUser(LogInUser logInUser)
        {
            var userSeller = await _UberTrashServices.LogInUser(logInUser);
            if (userSeller != null)
            {
                HttpContext.Session.SetString("publicKey_ForNew_USers", userSeller.PublicKey);
                HttpContext.Session.SetString("phoneNumber", userSeller.Phone_Number);
                _logInUser1 = userSeller;
                ViewBag.Message = "Logged In successfully";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "The information entered is incorrect";
            return View(userSeller);
        }
            [HttpPost]
        public async Task<IActionResult> Index(UserSellers userSellers)
        {
            var agentName = HttpContext.Session.GetString("username") == null ? "Tega" : HttpContext.Session.GetString("username");
            userSellers.Agent = agentName;
            var result = await _UberTrashServices.UpdateUSerSellerAgent(userSellers);
            if (result)
            {
                ViewBag.Message = "Successful";
            }
            else
            {
                ViewBag.Message = "Not Successful";
            }
            return RedirectToAction("Index", "Home");
        }

            public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Connect()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
