using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
using MyWebApiApp.Models.DTOs;
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
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService?.GetAllUser();
            if (users == null)
            {
                return BadRequest("Users not found");
            }
            return Ok(users);
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
            _userService.Logout();
            return Ok("Logged out successfully.");
        }
    }
}