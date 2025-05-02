using MailWarehouse.Application.Interfaces;
using MailWarehouse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly IStringLocalizer<AuthController> _localizer;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IStringLocalizer<AuthController> localizer, IConfiguration configuration)
    {
        _userService = userService;
        _localizer = localizer;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            var hardcodedUsername = _configuration.GetSection("Authentication")["Username"];
            var hardcodedPassword = _configuration.GetSection("Authentication")["Password"];

            if (model.Username == hardcodedUsername && model.Password == hardcodedPassword)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = _localizer["Невірне ім'я користувача або пароль"];
                return View(model);
            }
        }

        return View(model);
    }
}