using Codecool.CodecoolShop.Services.Interfaces;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Threading.Tasks;
//using System.Web.Http;

namespace Codecool.CodecoolShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMailService _mailServices;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IMailService mailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailServices = mailServices;

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
                        new { userId = user.Id, token = token}, Request.Scheme);
                    
                    SendgridAccountConfirmationModel emailModel = new(user, confirmationLink);
                    await _mailServices.SendAccountRegisterConfirmation(emailModel);
                   
                    ViewBag.Message = @"Registration succesful!<br>Before you can Login, please confirm your
                                            email, by clicking on the confirmation link sent to your email.";
                   
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
                    ModelState.AddModelError(string.Empty, "Confirm your email first");
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
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
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
                await _signInManager.SignInAsync(user, isPersistent: false);
                return View();
            }

            Log.Warning($"the user Id {userId} cannot confirme the account");
            ViewBag.Error = "Email cannot be confirmed";
            return View("ConfirmEmail");
        }
    }
}
