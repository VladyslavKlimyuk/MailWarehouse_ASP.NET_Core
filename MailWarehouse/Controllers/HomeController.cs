using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MailWarehouse.Controllers;

public class HomeController : Controller
{
    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(IStringLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = _localizer["IndexTitle"];
        return View();
    }

    public IActionResult Privacy()
    {
        ViewData["Title"] = _localizer["PrivacyText"];
        return View();
    }
}