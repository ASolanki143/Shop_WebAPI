using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
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
        private readonly MongoLogService _logService = new MongoLogService();
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
            if (users == null)
            {
                response = new ApiResponse("Users not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(users, "Users fetch successfully", 200);
            return Ok(response);
        }
        #endregion

        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginRequestDto? request)
        {
            try
            {
                ApiResponse response;
                var user = _userService?.Login(request.UserName, request.Password);
                if (user == null)
                {
                    response = new ApiResponse("User Not found", 404);
                    return NotFound(response);
                }
                string? userId = user.UserID.ToString();
                string role = user.Role;
                // _logService.InsertLog("Login", $"{role} logged in", userId);
                Console.WriteLine("user is not null");
                response = new ApiResponse(user, "User login successfully", 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exceofihg");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            LogoutResult user = _userService.Logout();
            string? userId = user.UserID;
            string? role = user.Role;
            // _logService.InsertLog("Logout", $"{role} logged out", userId);
            ApiResponse response = new ApiResponse("Logged out successfully.", 200);
            return Ok(response);
        }
    }
}