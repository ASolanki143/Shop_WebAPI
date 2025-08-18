using MyWebApiApp.Data;
using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserRepository? _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public LoginResponseDto? Login(string? UserName, string? Password)
        {
            Console.WriteLine("Login Services");
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("Username or password are required");
            }

            LoginResponseDto? user = _userRepository?.Login(UserName: UserName, Password: Password);
            if (user != null)
            {
                var session = _httpContextAccessor.HttpContext!.Session;
                session.SetInt32("UserID", user.UserID);
                session.SetString("UserName", user.UserName);
                session.SetString("Role", user.Role);
            }
            return user;
        }

        public IEnumerable<UserModel>? GetAllUser()
        {
            var users = _userRepository?.SelectAll();
            return users;
        }

        public LogoutResult Logout()
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            string role = session.GetString("Role");
            int? userIdValue = session.GetInt32("UserID");

            string? userId = userIdValue?.ToString();
            session.Clear();
            return new LogoutResult
            {
                Role = role,
                UserID = userId
            };
        }
    }
}