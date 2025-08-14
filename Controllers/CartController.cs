using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Implementations;
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

        // #region Get All Carts by User
        // [HttpGet("User")]
        // public IActionResult GetCartsByUser()
        // {
        //     int userId = (int)HttpContext.Session.GetInt32("UserID");
        //     var carts = _cartService.GetAllCartForUser(userId);
        //     if (carts == null || !carts.Any())
        //     {
        //         return NotFound("No carts found for this user");
        //     }
        //     return Ok(carts);
        // }
        // #endregion

        #region Add Cart
        [HttpPost]
        public IActionResult AddCart([FromBody] List<CartItemDto> cartItems)
        {
            Console.WriteLine("Add cart");
            ApiResponse response;
            int UserID = (int)HttpContext.Session.GetInt32("UserID");
            bool isInserted = _cartService.AddCart(UserID,cartItems);
            if (!isInserted)
            {
                response = new ApiResponse("Error while adding cart", 404);
                return BadRequest(response);
            }
            response = new ApiResponse("Cart added successfully", 200);
            return Ok(response);
        }
        #endregion  

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
                response = new ApiResponse("Error while deleting cart", 400);
                return BadRequest(response);
            }
            response = new ApiResponse("Cart deleted successfully", 200);
            return Ok(response);
        }
        #endregion

        // #region Move Cart to Invoice
        // [HttpPost("MoveToInvoice/{cartId}")]
        // public IActionResult MoveToInvoice(int cartId)
        // {
        //     if (cartId <= 0)
        //     {
        //         return BadRequest("Invalid CartID");
        //     }
        //     bool isMoved = _cartRepository.MoveCartToInvoice(cartId);
        //     if (!isMoved)
        //     {
        //         return BadRequest("Error while moving cart to invoice");
        //     }
        //     return Ok("Cart moved to invoice successfully");
        // }
        // #endregion

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
                response = new ApiResponse("Error while updating total amount", 400);
                return BadRequest(response);
            }
            response = new ApiResponse("Cart Total updated successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
