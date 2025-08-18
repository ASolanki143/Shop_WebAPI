using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Filters;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Implementations;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceService;
        public InvoiceController(IInvoiceServices invoiceServices)
        {
            _invoiceService = invoiceServices;
        }

        #region Create Invoice from Cart
        [LogAction("Invoice Insert")]
        [HttpPost("CreateFromCart/{cartId}")]
        public IActionResult CreateInvoiceFromCart(int cartId)
        {
            ApiResponse response;
            if (cartId <= 0)
            {
                response = new ApiResponse("Invalid card id",400);
                return BadRequest(response);
            }

            bool isCreated = _invoiceService.InsertInvoice(cartId);
            if (!isCreated)
            {
                throw new Exception("Error while creating invoice from cart");
            }

            response = new ApiResponse("Invoice created successfully", 200);
            return Ok(response);
        }
        #endregion

        #region List All Invoices
        [HttpGet]
        public IActionResult GetAllInvoices()
        {
            ApiResponse response;
            int? userId = null;
            string? role = HttpContext.Session.GetString("Role");
            if (role == "User")
            {
                userId = HttpContext.Session.GetInt32("UserID");
            }
            var invoices = _invoiceService.GetAllInvices(userId);
            if (invoices == null || !invoices.Any())
            {
                response = new ApiResponse("No invoices found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(invoices, "Invoices Fetch Successfully", 200);
            return Ok(response);
        }
        #endregion

        
    }
}
