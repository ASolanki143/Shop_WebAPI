using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Filters;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Implementations;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService? _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region Get All User
        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            ApiResponse response;
            var users = _userService?.GetAllUser();
            if (users == null || !users.Any())
            {
                response = new ApiResponse("Users not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(users, "Users fetch successfully", 200);
            return Ok(response);
        }
        #endregion

        [LogAction("Login")]
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginRequest? request)
        {
            ApiResponse response;
            var user = _userService?.Login(request.UserName, request.Password);
            if (user == null)
            {
                response = new ApiResponse("User Not found", 404);
                return NotFound(response);
            }
            Console.WriteLine("User Login ...........");
            response = new ApiResponse(user, "User login successfully", 200);
            return Ok(response);
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            LogoutResult user = _userService.Logout();
            ApiResponse response = new ApiResponse("Logged out successfully.", 200);
            return Ok(response);
        }
    }
}