using AutoMapper;
using MailWarehouse.ViewModels;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MailWarehouse.Domain.Entities;

namespace MailWarehouse.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserViewModel>> GetAllUsers()
    {
        IEnumerable<UserDto> users = _userService.GetAllUsers();
        return Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
    }

    [HttpGet("{id:int}")]
    public ActionResult<UserViewModel> GetUserById(int id)
    {
        UserDto user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<UserViewModel>(user));
    }

    [HttpPost]
    public ActionResult<UserViewModel> CreateUser([FromBody] UserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        UserDto userDto = _mapper.Map<UserDto>(userViewModel);
        _userService.CreateUser(userDto);
        return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userViewModel);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateUser(int id, [FromBody] UserViewModel userViewModel)
    {
        if (id != userViewModel.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        UserDto userDto = _mapper.Map<UserDto>(userViewModel);
        _userService.UpdateUser(userDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        UserDto user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        _userService.DeleteUser(id);
        return NoContent();
    }
}