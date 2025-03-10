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
        return View(_mapper.Map<IEnumerable<UserViewModel>>(users));
    }

    public IActionResult Details(int id)
    {
        UserDto user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(_mapper.Map<UserViewModel>(user));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(UserViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            UserDto userDto = _mapper.Map<UserDto>(userViewModel);
            _userService.CreateUser(userDto);
            return RedirectToAction(nameof(Index));
        }
        return View(userViewModel);
    }

    public IActionResult Edit(int id)
    {
        UserDto user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(_mapper.Map<UserViewModel>(user));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, UserViewModel userViewModel)
    {
        if (id != userViewModel.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            UserDto userDto = _mapper.Map<UserDto>(userViewModel);
            _userService.UpdateUser(userDto);
            return RedirectToAction(nameof(Index));
        }
        return View(userViewModel);
    }

    public IActionResult Delete(int id)
    {
        UserDto user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(_mapper.Map<UserViewModel>(user));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _userService.DeleteUser(id);
        return RedirectToAction(nameof(Index));
    }
}