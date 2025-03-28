using AutoMapper;
using MailWarehouse.ViewModels;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MailWarehouse.Presentation.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        IEnumerable<UserDto> users = _userService.GetAllUsers();
        var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);
        return View(userViewModels);
    }

    public IActionResult Details(int id)
    {
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
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
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
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
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
        return RedirectToAction(nameof(Index));
    }
}