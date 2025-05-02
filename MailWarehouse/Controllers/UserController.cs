using AutoMapper;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Models;
using MailWarehouse.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MailWarehouse.Domain.Entities;

namespace MailWarehouse.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<UserController> _localizer;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(IUserService userService, IMapper mapper, IStringLocalizer<UserController> localizer,
                        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userService = userService;
        _mapper = mapper;
        _localizer = localizer;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = _localizer["Користувачі"];
        try
        {
            IEnumerable<UserDto> users = _userService.GetAllUsers();
            var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

            if (userViewModels != null)
            {
                Console.WriteLine($"Кількість користувачів: {userViewModels.Count()}");
                foreach (var vm in userViewModels)
                {
                    Console.WriteLine($"Користувачі - ID: {vm.Id}, Ім'я: {vm.FirstName}, Прізвище: {vm.LastName}, Пошта: {vm.Email}");
                }
            }
            else
            {
                Console.WriteLine("Користувачів немає.");
            }

            return View("Index", userViewModels);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["Помилка", ex.Message].Value;
            return View("Index", new List<UserViewModel>());
        }
    }

    [HttpGet("Details/{id:int}")]
    public IActionResult Details(int id)
    {
        ViewData["Title"] = _localizer["Деталі"];
        try
        {
            UserDto user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return View("Details", userViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["Помилка", ex.Message].Value;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = _localizer["Створити"];
        return View("Create");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(UserViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = userViewModel.FirstName,
                Email = userViewModel.Email,
                PhoneNumber = userViewModel.PhoneNumber,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName
            };
            var result = await _userManager.CreateAsync(user, userViewModel.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = _localizer["Користувача успішно створено."].Value;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Create", userViewModel);
            }
        }
        return View("Create", userViewModel);
    }

    [HttpGet("EditUser")]
    public IActionResult EditUser(string email)
    {
        ViewData["Title"] = _localizer["Редагувати"];
        try
        {
            UserDto userDto = _userService.GetUserByEmail(email);
            if (userDto == null)
            {
                return NotFound();
            }
            var userViewModel = _mapper.Map<UserViewModel>(userDto);
            return View("Edit", userViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ПОмилка", ex.Message].Value;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("EditUser")]
    [ValidateAntiForgeryToken]
    public IActionResult EditUser(UserViewModel userViewModel)
    {
        ViewData["Title"] = _localizer["Редагувати"];
        try
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", userViewModel);
            }
            UserDto userDto = _mapper.Map<UserDto>(userViewModel);
            _userService.UpdateUser(userDto);
            TempData["SuccessMessage"] = _localizer["Користувача успішно відредаговано."].Value;
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["Помилка", ex.Message].Value;
            return View("Edit", userViewModel);
        }
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
        ViewData["Title"] = _localizer["Редагувати"];
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = await _roleManager.Roles.ToListAsync();

        var model = new EditUserRolesViewModel
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = allRoles.Select(role => new UserRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                IsSelected = userRoles.Contains(role.Name)
            }).ToList()
        };

        return View("EditRoles", model);
    }

    [HttpPost("EditRoles/{userId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRoles(string userId, List<UserRoleViewModel> model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var rolesToRemove = await _userManager.GetRolesAsync(user);
        var resultRemove = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        if (!resultRemove.Succeeded)
        {
            ModelState.AddModelError("", _localizer["Помилка видалення ролі."].Value);
            return View("EditRoles", new EditUserRolesViewModel { UserId = user.Id, Email = user.Email, Roles = model });
        }

        var rolesToAdd = model.Where(r => r.IsSelected).Select(r => r.RoleName).ToList();
        var resultAdd = await _userManager.AddToRolesAsync(user, rolesToAdd);

        if (!resultAdd.Succeeded)
        {
            ModelState.AddModelError("", _localizer["Помилка додавання ролі."].Value);
            return View("EditRoles", new EditUserRolesViewModel { UserId = user.Id, Email = user.Email, Roles = model });
        }

        TempData["SuccessMessage"] = _localizer["Ролі успішно оновлено."].Value;
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("DeleteUser")]
    public IActionResult DeleteUser(string email)
    {
        ViewData["Title"] = _localizer["Видалення"];
        try
        {
            UserDto user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return View("Delete", userViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["Помилка", ex.Message].Value;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("DeleteUser")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteUserConfirmed(UserViewModel userViewModel)
    {
        try
        {
            _userService.DeleteUser(userViewModel.Id);
            TempData["SuccessMessage"] = _localizer["Користувача успішно видалено"].Value;
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["Помилка", ex.Message].Value;
            return RedirectToAction(nameof(Index));
        }
    }
}