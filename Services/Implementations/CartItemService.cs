using MyWebApiApp.Data;
using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Services.Implementations
{
    public class CartItemService : ICartItemServices
    {
        private readonly CartItemRepository _cartItemRepository;
        public CartItemService(CartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public IEnumerable<CartItemModel> GetCartItemsByCart(int CartID)
        {
            var cartItems = _cartItemRepository.GetCartItemsByCart(CartID);
            return cartItems;
        }

        public bool AddCartItem(CartItemDto cartItem)
        {
            bool isInserted = _cartItemRepository.AddCartItem(cartItem);
            return isInserted;
        }

        public bool UpdateCartItem(int cartItemId, int Quantity)
        {
            bool isUpdated = _cartItemRepository.UpdateCartItem(cartItemId, Quantity);
            return isUpdated;
        }

        public bool DeleteCartItem(int cartItemId)
        {
            bool isDeleted = _cartItemRepository.DeleteCartItem(cartItemId);
            return isDeleted;
        }
    }
}