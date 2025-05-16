using MailWarehouse.Domain.Entities;
using MailWarehouse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace MailWarehouse.Controllers;

[AllowAnonymous]
public class AuthController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly IStringLocalizer<AuthController> _localizer;

    public AuthController(SignInManager<User> signInManager, IStringLocalizer<AuthController> localizer)
    {
        _signInManager = signInManager;
        _localizer = localizer;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }
            else
            {
                ModelState.AddModelError(string.Empty, _localizer["Неправильний логін або пароль."]);
                return View(model);
            }
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}