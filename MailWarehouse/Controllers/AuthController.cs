using MailWarehouse.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly IStringLocalizer<AuthController> _localizer;

    public AuthController(IUserService userService, IStringLocalizer<AuthController> localizer)
    {
        _userService = userService;
        _localizer = localizer;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(MailWarehouse.ViewModels.UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user != null)
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