using AutoMapper;
using MailWarehouse.Application.Models;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Domain.Entities;
using MailWarehouse.Domain.Interfaces;

namespace MailWarehouse.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public void CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = HashPassword(userDto.Password);
            _userRepository.Add(user);
        }

        public void UpdateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public User Authenticate(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user == null)
            {
                return null;
            }

            if (VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}