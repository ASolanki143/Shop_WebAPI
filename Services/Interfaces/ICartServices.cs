using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;

namespace MyWebApiApp.Services.Interfaces
{
    public interface ICartServices
    {
        IEnumerable<CartModel> GetAllCart(int? UserID);
        // IEnumerable<CartModel> GetAllCartForUser(int UserID);
        bool AddCart(int UserID, List<CartItemDto> cartItems);
        bool DeleteCart(int CartID);
        bool UpdateTotal(int CartID);
    }
}