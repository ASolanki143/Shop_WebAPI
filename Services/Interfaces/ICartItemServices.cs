using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;

namespace MyWebApiApp.Services.Interfaces
{
    public interface ICartItemServices
    {
        IEnumerable<CartItemModel> GetCartItemsByCart(int? CartID,int? InvoiceID);
        bool AddCartItem(CartItemDto cartItem);
        bool UpdateCartItem(int cartItemId, int Quantity);
        bool DeleteCartItem(int cartItemId);
    }
}