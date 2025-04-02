using AutoMapper;
using MailWarehouse.ViewModels;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

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

    public IActionResult Index()
    {
        ViewData["Title"] = _localizer["UserIndexTitle"];
        IEnumerable<UserDto> users = _userService.GetAllUsers();
        var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);
        return View(userViewModels);
    }

    public IActionResult Details(int id)
    {
        ViewData["Title"] = _localizer["UserDetailsTitle"];
        UserDto user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        var userViewModel = _mapper.Map<UserViewModel>(user);
        return View(userViewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = _localizer["UserCreateTitle"];
        return View();
    }

    [HttpPost]
    public IActionResult CreateUser(UserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }
        UserDto userDto = _mapper.Map<UserDto>(userViewModel);
        _userService.CreateUser(userDto);
        TempData["SuccessMessage"] = _localizer["UserCreateSuccess"];
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        ViewData["Title"] = _localizer["UserEditTitle"];
        UserDto user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        var userViewModel = _mapper.Map<UserViewModel>(user);
        return View(userViewModel);
    }

    [HttpPost]
    public IActionResult EditUser(int id, UserViewModel userViewModel)
    {
        if (id != userViewModel.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        UserDto userDto = _mapper.Map<UserDto>(userViewModel);
        _userService.UpdateUser(userDto);
        TempData["SuccessMessage"] = _localizer["UserUpdateSuccess"];
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        ViewData["Title"] = _localizer["UserDeleteTitle"];
        UserDto user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        var userViewModel = _mapper.Map<UserViewModel>(user);
        return View(userViewModel);
    }

    [HttpPost]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        TempData["SuccessMessage"] = _localizer["UserDeleteSuccess"];
        return RedirectToAction(nameof(Index));
    }
}