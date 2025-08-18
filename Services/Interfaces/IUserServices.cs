using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;

namespace MyWebApiApp.Services.Interfaces
{
    public interface IUserService
    {
        LoginResponseDto? Login(string UserName, string Password);
        IEnumerable<UserModel>? GetAllUser();
        LogoutResult Logout();
    }
}