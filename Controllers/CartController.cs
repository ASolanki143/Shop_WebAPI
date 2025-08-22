using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartService;

        public CartController(ICartServices cartService)
        {
            _cartService = cartService;
        }

        #region Get All Carts
        [HttpGet]
        public IActionResult GetAllCarts()
        {
            ApiResponse response;
            string Role = HttpContext.Session.GetString("Role");
            int? UserID = null;
            if (!string.IsNullOrEmpty(Role) && Role.Equals("User"))
            {
                UserID = HttpContext.Session.GetInt32("UserID");
            }
            var carts = _cartService.GetAllCart(UserID);
            if (carts == null || !carts.Any())
            {
                response = new ApiResponse("Carts not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(carts, "Carts Fetch Successfully", 200);
            return Ok(response);
        }
        #endregion

        // #region Add Cart
        // [HttpPost]
        // public IActionResult AddCart([FromBody] List<CartItemDto> cartItems)
        // {
        //     ApiResponse response;
        //     int userId = (int)HttpContext.Session.GetInt32("UserID");
        //     bool isInserted = _cartService.AddCart(userId,cartItems);
        //     if (!isInserted)
        //     {
        //         throw new Exception("Error while inserting cart");
        //     }
        //     response = new ApiResponse("Cart added successfully", 200);
        //     return Ok(response);
        // }
        // #endregion  

        #region Delete Cart
        [HttpPatch("Delete/{cartId}")]
        public IActionResult DeleteCart(int cartId)
        {
            ApiResponse response;
            if (cartId <= 0)
            {
                response = new ApiResponse("Invalid CartID", 400);
                return BadRequest(response);
            }

            bool isDeleted = _cartService.DeleteCart(cartId);
            if (!isDeleted)
            {
                throw new Exception("Error while deleting cart");
            }
            response = new ApiResponse("Cart deleted successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Total Amount
        [HttpPatch("UpdateTotal/{cartId}")]
        public IActionResult UpdateCartTotal(int cartId)
        {
            ApiResponse response;
            if (cartId <= 0)
            {
                response = new ApiResponse("Invalid CartID", 400);
                return BadRequest(response);
            }
            bool isUpdated = _cartService.UpdateTotal(cartId);
            if (!isUpdated)
            {
                throw new Exception("Error while updating cart");
            }
            response = new ApiResponse("Cart Total updated successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
