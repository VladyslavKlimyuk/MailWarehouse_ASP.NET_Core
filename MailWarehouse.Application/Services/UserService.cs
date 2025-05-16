using AutoMapper;
using MailWarehouse.Application.Models;
using MailWarehouse.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailWarehouse.Domain.Entities;
using MailWarehouse.Domain.Interfaces;

namespace MailWarehouse.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var applicationUsers = await _userRepository.GetAllIdentityUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(applicationUsers);
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            var applicationUser = _mapper.Map<User>(userDto);
            await _userRepository.AddIdentityUserAsync(applicationUser, userDto.Password);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(userDto.Id);
            if (existingUser != null)
            {
                _mapper.Map(userDto, existingUser);
                await _userRepository.UpdateIdentityUserAsync(existingUser);
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteIdentityUserAsync(user);
            }
        }

        public async Task<UserDto> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Користувача не знайдено." });
            }

            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
    }
}