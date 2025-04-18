using AutoMapper;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Models;
using MailWarehouse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MailWarehouse.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<UserController> _localizer;

    public UserController(IUserService userService, IMapper mapper, IStringLocalizer<UserController> localizer)
    {
        _userService = userService;
        _mapper = mapper;
        _localizer = localizer;
    }

    // GET: User/Index
    public IActionResult Index()
    {
        ViewData["Title"] = _localizer["UserIndexTitle"];
        try
        {
            IEnumerable<UserDto> users = _userService.GetAllUsers();
            var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return View("Index", userViewModels);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View("Index", new List<UserViewModel>());
        }
    }

    // GET: User/Details/5
    [HttpGet("Details/{id:int}")]
    public IActionResult Details(int id)
    {
        ViewData["Title"] = _localizer["UserDetailsTitle"];
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
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: User/Create
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = _localizer["UserCreateTitle"];
        return View("Create");
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateUser(UserViewModel userViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View("Create", userViewModel);
            }
            UserDto userDto = _mapper.Map<UserDto>(userViewModel);
            _userService.CreateUser(userDto);
            TempData["SuccessMessage"] = _localizer["UserCreateSuccess"];
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View("Create", userViewModel);
        }
    }

    // GET: User/Edit/5
    [HttpGet("Edit/{id:int}")]
    public IActionResult Edit(int id)
    {
        ViewData["Title"] = _localizer["UserEditTitle"];
        try
        {
            UserDto user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return View("Edit", userViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: User/Edit/5
    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult EditUser(int id, UserViewModel userViewModel)
    {
        try
        {
            if (id != userViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", userViewModel);
            }

            UserDto userDto = _mapper.Map<UserDto>(userViewModel);
            _userService.UpdateUser(userDto);
            TempData["SuccessMessage"] = _localizer["UserUpdateSuccess"]; // Assuming you have this key
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View("Edit", userViewModel);
        }
    }

    // GET: User/Delete/5
    [HttpGet("Delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        ViewData["Title"] = _localizer["UserDeleteTitle"];
        try
        {
            UserDto user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return View("Delete", userViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: User/Delete/5
    [HttpPost("Delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteUserConfirmed(int id)
    {
        try
        {
            _userService.DeleteUser(id);
            TempData["SuccessMessage"] = _localizer["UserDeleteSuccess"];
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }
}