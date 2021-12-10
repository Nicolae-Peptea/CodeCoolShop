using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Threading.Tasks;
//using System.Web.Http;

namespace Codecool.CodecoolShop.Controllers
{
    public class AccountController : Controller
    {
        private const string SUCCESSFUL_REGISTRATION_MESSAGE =
            @"Registration succesful!<br>Before you can Login, please confirm your
             email, by clicking on the confirmation link sent to your email.";
        private const string CONFIRM_YOUR_EMAIL = "Confirm your email first";
        private const string INVALID_LOGIN = "Invalid Login Attempt";
        private const string EMAIL_NOT_CONFIRMED = "Email cannot be confirmed";

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMailService _mailServices;
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IMailService mailServices, IConfiguration configuration, ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailServices = mailServices;
            _configuration = configuration;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new()
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    string sendgridTemplateId = _configuration.GetValue<string>("Sendgrid:AccountConfirmationTemplateId");
                    SendgridAccountConfirmationModel emailModel = new(user, confirmationLink, sendgridTemplateId);
                    await _mailServices.SendEmail(emailModel);

                    ViewBag.Message = SUCCESSFUL_REGISTRATION_MESSAGE;
                    return View("ConfirmEmail");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed &&
                    await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ModelState.AddModelError(string.Empty, CONFIRM_YOUR_EMAIL);
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "HomePage");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, INVALID_LOGIN);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "HomePage");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "HomePage");
            }

            IdentityUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                Log.Warning($"the user Id {userId} is invalid");
                return RedirectToAction("Index", "HomePage");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {   
                Log.Information($"the user Id {userId} successful confirmed the account");
                _customerService.UpdateCustomerUserId(user.Email, userId);
                await _signInManager.SignInAsync(user, isPersistent: false);
              
                return View();
            }

            Log.Warning($"the user Id {userId} cannot confirme the account");
            ViewBag.Error = EMAIL_NOT_CONFIRMED;

            return View("ConfirmEmail");
        }
    }
}
