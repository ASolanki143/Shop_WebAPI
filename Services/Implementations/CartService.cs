using MyWebApiApp.Data;
using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Services.Implementations
{
    public class CartService : ICartServices
    {
        private readonly CartRepository _cartRepository;
        private readonly ICartItemServices _cartItemService;
        public CartService(CartRepository cartRepository, ICartItemServices cartItemServices)
        {
            _cartItemService = cartItemServices;
            _cartRepository = cartRepository;
        }

        public IEnumerable<CartModel> GetAllCart(int? UserID)
        {
            var carts = _cartRepository.SelectAllCarts(UserID);
            return carts;
        }

        public IEnumerable<CartModel> GetAllCartForUser(int UserID)
        {
            var carts = _cartRepository.SelectAllCartsByUser(UserID);
            return carts;
        }

        public bool AddCart(int UserID, List<CartItemDto> cartItems)
        {
            int isInserted = _cartRepository.AddCart(UserID);
            int cartID = isInserted;
            foreach (CartItemDto model in cartItems)
            {
                model.CartID = cartID;
                _cartItemService.AddCartItem(model);
            }
            UpdateTotal(cartID);
            return isInserted > 0;
        }

        public bool DeleteCart(int CartID)
        {
            bool isDeleted = _cartRepository.DeleteCart(CartID);
            return isDeleted;
        }

        public bool UpdateTotal(int CartID)
        {
            bool isUpdated = _cartRepository.UpdateCartTotal(CartID);
            return isUpdated;
        }
    }
}