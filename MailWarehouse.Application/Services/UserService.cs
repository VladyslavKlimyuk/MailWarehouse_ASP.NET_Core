using AutoMapper;
using MailWarehouse.Application.Models;
using MailWarehouse.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using MailWarehouse.Domain.Entities;
using MailWarehouse.Domain.Interfaces;

namespace MailWarehouse.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var applicationUsers = _userRepository.GetAllIdentityUsers();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(applicationUsers);

            foreach (var dto in userDtos)
            {
                Console.WriteLine($"Після мапінгу - Id: {dto.Id}, Username: {dto.Username}, Email: {dto.Email}, FirstName: {dto.FirstName}, LastName: {dto.LastName}, PhoneNumber: {dto.PhoneNumber}");
            }

            return userDtos;
        }

        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUserByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            return _mapper.Map<UserDto>(user);
        }

        public async void CreateUser(UserDto userDto)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = userDto.Username ?? userDto.Email,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };

            var result = await _userManager.CreateAsync(applicationUser, userDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Помилка створення користувача: {error.Description}");
                }
                throw new Exception("Не вдалося створити користувача.");
            }
        }

        public void UpdateUser(UserDto userDto)
        {
            var user = _userRepository.GetById(userDto.Id);
            if (user != null)
            {
                user.Username = userDto.Username;
                user.Email = userDto.Email;
                user.PhoneNumber = userDto.PhoneNumber;
                _userRepository.Update(user);
            }
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public User Authenticate(string username, string password)
        {
            return _userRepository.GetByUsernameAndPassword(username, password);
        }

        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }
    }
}