using AutoMapper;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailWarehouse.Domain.Entities;

namespace MailWarehouse.Controllers;

//[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<UserController> _localizer;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;

    public UserController(IUserService userService, IMapper mapper, IStringLocalizer<UserController> localizer,
                            UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
                            SignInManager<User> signInManager)
    {
        _userService = userService;
        _mapper = mapper;
        _localizer = localizer;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = _localizer["Користувачі"];
        var users = await _userService.GetAllUsersAsync();
        var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);
        return View("Index", userViewModels);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(string id)
    {
        ViewData["Title"] = _localizer["Деталі"];
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        var userViewModel = _mapper.Map<UserViewModel>(user);
        return View("Details", userViewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = _localizer["Створити"];
        return View("Create");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(CreateUserViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = new User { UserName = userViewModel.Email, Email = userViewModel.Email, FirstName = userViewModel.FirstName, LastName = userViewModel.LastName };
            var result = await _userManager.CreateAsync(user, userViewModel.Password);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = _localizer["Користувача успішно створено."].Value;
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View("Create", userViewModel);
    }


    [HttpGet("EditUser")]
    public async Task<IActionResult> EditUser(string email)
    {
        ViewData["Title"] = _localizer["Редагувати"];
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return NotFound();
        var editUserViewModel = _mapper.Map<EditUserViewModel>(user);
        return View("Edit", editUserViewModel);
    }

    [HttpPost("EditUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
    {
        ViewData["Title"] = _localizer["Редагувати"];
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(editUserViewModel.Email);
            if (user == null) return NotFound();

            user.FirstName = editUserViewModel.FirstName;
            user.LastName = editUserViewModel.LastName;
            user.PhoneNumber = editUserViewModel.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = _localizer["Користувача успішно відредаговано."].Value;
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View("Edit", editUserViewModel);
    }

    [HttpGet("Edit/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(string id)
    {
        ViewData["Title"] = _localizer["Редагувати ролі"];
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
        var allRoles = await _roleManager.Roles.ToListAsync();
        var model = new EditUserRolesViewModel
        {
            UserId = user.Id.ToString(),
            UserName = user.UserName,
            UserRoles = userRoles,
            AllRoles = allRoles.Select(role => new RoleViewModel
            {
                RoleName = role.Name,
                IsSelected = userRoles.Contains(role.Name)
            }).ToList()
        };
        return View("EditRoles", model);
    }

    [HttpPost("EditRoles/{userId}")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRoles(string userId, EditUserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();
        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = model.AllRoles.Where(r => r.IsSelected && !currentRoles.Contains(r.RoleName)).Select(r => r.RoleName).ToList();
        var rolesToRemove = model.AllRoles.Where(r => !r.IsSelected && currentRoles.Contains(r.RoleName)).Select(r => r.RoleName).ToList();

        var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
        var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        if (addResult.Succeeded && removeResult.Succeeded)
        {
            TempData["SuccessMessage"] = _localizer["Ролі успішно оновлено."].Value;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            ModelState.AddModelError("", _localizer["Помилка оновлення ролей."].Value);
            return View("EditRoles", model);
        }
    }

    [HttpGet("DeleteUser")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        ViewData["Title"] = _localizer["Видалення"];
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return NotFound();
        var userViewModel = _mapper.Map<UserViewModel>(user);
        return View("Delete", userViewModel);
    }

    [HttpPost("DeleteUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserConfirmed(UserViewModel userViewModel)
    {
        var userToDelete = await _userManager.FindByIdAsync(userViewModel.Id.ToString());
        if (userToDelete == null) return NotFound();

        var result = await _userManager.DeleteAsync(userToDelete);
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = _localizer["Користувача успішно видалено"].Value;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Delete", userViewModel);
        }
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UserList()
    {
        ViewData["Title"] = _localizer["Список користувачів"];
        var users = await _userManager.Users.ToListAsync();
        return View("UserList", users);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RoleList()
    {
        ViewData["Title"] = _localizer["Список ролей"];
        var roles = await _roleManager.Roles.ToListAsync();
        return View("RoleList", roles);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult CreateRole()
    {
        ViewData["Title"] = _localizer["Створити роль"];
        return View("CreateRole");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (!string.IsNullOrWhiteSpace(model.RoleName))
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.RoleName);
                if (!roleExist)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                    if (roleResult.Succeeded)
                    {
                        TempData["SuccessMessage"] = _localizer["Роль створено."].Value;
                        return RedirectToAction(nameof(RoleList));
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("RoleName", _localizer["Роль з такою назвою вже існує."].Value);
                }
            }
        }
        return View("CreateRole", model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRole(string id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var roleResult = await _roleManager.DeleteAsync(role);
                if (roleResult.Succeeded)
                {
                    TempData["SuccessMessage"] = _localizer["Роль видалено."].Value;
                    return RedirectToAction(nameof(RoleList));
                }
                else
                {
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
        }
        return RedirectToAction(nameof(RoleList));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult CreateUserByAdmin()
    {
        ViewData["Title"] = _localizer["Створити користувача"];
        return View("CreateUserByAdmin");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUserByAdmin(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = _localizer["Користувача успішно створено."].Value;
                return RedirectToAction(nameof(UserList));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View("CreateUserByAdmin", model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserByAdmin(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = _localizer["Користувача успішно видалено."].Value;
            return RedirectToAction(nameof(UserList));
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return RedirectToAction(nameof(UserList));
    }

    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        ViewData["Title"] = _localizer["Редагувати профіль"];
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound(_localizer["Не вдалося завантажити користувача."].Value);

        var model = new EditProfileViewModel
        {
            Email = user.Email
        };
        return View("EditProfile", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
    {
        ViewData["Title"] = _localizer["Редагувати профіль"];
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound(_localizer["Не вдалося завантажити користувача."].Value);

            user.Email = model.Email;
            user.UserName = model.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["SuccessMessage"] = _localizer["Профіль успішно оновлено."].Value;
                return RedirectToAction(nameof(EditProfile));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View("EditProfile", model);
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        ViewData["Title"] = _localizer["Змінити пароль"];
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound(_localizer["Не вдалося завантажити користувача."].Value);

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
        {
            return RedirectToAction(nameof(SetPassword));
        }

        return View("ChangePassword");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        ViewData["Title"] = _localizer["Змінити пароль"];
        if (!ModelState.IsValid) return View("ChangePassword", model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound(_localizer["Не вдалося завантажити користувача."].Value);

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (changePasswordResult.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = _localizer["Пароль успішно змінено."].Value;
            return RedirectToAction(nameof(ChangePassword));
        }
        foreach (var error in changePasswordResult.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View("ChangePassword", model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        ViewData["Title"] = _localizer["Забули пароль?"];
        return View("ForgotPassword");
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        ViewData["Title"] = _localizer["Забули пароль?"];
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                TempData["ForgotPasswordMessage"] = _localizer["Інструкції для відновлення пароля відправлено на вашу електронну пошту."].Value;
                return View("ForgotPassword");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(
                nameof(ResetPassword),
                "User",
                new { userId = user.Id, code },
                protocol: HttpContext.Request.Scheme);

            TempData["ForgotPasswordMessage"] = _localizer["Інструкції для відновлення пароля відправлено на вашу електронну пошту."].Value;
            return View("ForgotPassword");
        }
        return View("ForgotPassword", model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPassword(string code = null)
    {
        ViewData["Title"] = _localizer["Відновлення пароля"];
        return code == null ? View("Error")
            : View("ResetPassword", new ResetPasswordViewModel { Code = code });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        ViewData["Title"] = _localizer["Відновлення пароля"];
        if (!ModelState.IsValid)
        {
            return View("ResetPassword", model);
        }
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View("ResetPassword", model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
        ViewData["Title"] = _localizer["Підтвердження відновлення пароля"];
        return View("ResetPasswordConfirmation");
    }

    [HttpGet]
    public async Task<IActionResult> SetPassword()
    {
        ViewData["Title"] = _localizer["Створити пароль"];
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound(_localizer["Не вдалося завантажити користувача."].Value);
        }
        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (hasPassword)
        {
            return RedirectToAction(nameof(ChangePassword));
        }
        return View("SetPassword");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
    {
        ViewData["Title"] = _localizer["Створити пароль"];
        if (!ModelState.IsValid)
        {
            return View("SetPassword", model);
        }
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound(_localizer["Не вдалося завантажити користувача."].Value);
        }
        var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
        if (addPasswordResult.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = _localizer["Пароль успішно встановлено."].Value;
            return RedirectToAction(nameof(SetPassword));
        }
        foreach (var error in addPasswordResult.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View("SetPassword", model);
    }
}