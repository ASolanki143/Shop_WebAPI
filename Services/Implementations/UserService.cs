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
        private readonly ILogServices _logServices = new LogService();
        public UserService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public LoginResponse? Login(string userName = "", string password = "")
        {
            Console.WriteLine("Login Services");
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username or password are required");
            }

            LoginResponse? user = _userRepository?.Login(userName, password);
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
            string? role = session.GetString("Role");
            int? userIdValue = session.GetInt32("UserID");
            string? userName = session.GetString("UserName");
            string? userId = userIdValue?.ToString();

            _logServices.InsertLog("Logout", $"Logout by {userName}.", userId);
            session.Clear();
            return new LogoutResult
            {
                Role = role,
                UserID = userId
            };
        }
    }
}