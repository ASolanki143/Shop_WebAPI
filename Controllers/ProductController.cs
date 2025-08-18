using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Implementations;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
        private readonly MongoLogService _logService = new MongoLogService();

        public ProductController(IProductServices productServices)
        {
            _productService = productServices;
        }

        #region Get All Products
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            ApiResponse response;
            if (products == null || !products.Any())
            {
                response = new ApiResponse("Products Not Found", 404);
                return NotFound("Products not found");
            }
            response = new ApiResponse(products, "Products fetch successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Add Product
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductModel product)
        {
            ApiResponse response;

            if (product == null)
            {
                response = new ApiResponse("Product is null", 400);
                return BadRequest(response);
            }

            bool isInserted = _productService.AddProduct(product);

            if (!isInserted)
            {
                throw new Exception("Error while inserting Product");
            }

            // for log
            int? userIdValue = HttpContext.Session.GetInt32("UserID");
            string? userId = userIdValue?.ToString();
            string? username = HttpContext.Session.GetString("UserName");
            _logService.InsertLog("Insert", $"Product Inserted by {username}", userId);

            response = new ApiResponse("Inserted Successfully",200);
            return Ok(response);
        }
        #endregion

        #region Update Product
        [HttpPut("{ProductID}")]
        public IActionResult UpdateProduct(int productId, ProductModel product)
        {
            ApiResponse response;

            if (productId <= 0)
            {
                response = new ApiResponse("ProductID is required", 400);
                return BadRequest(response);
            }

            // add productid to product model
            product.ProductID = productId;

            bool isUpdated = _productService.EditProduct(product);

            if (!isUpdated)
            {
                throw new Exception("Error while updating product");
            }

            // for log
            int? userIdValue = HttpContext.Session.GetInt32("UserID");
            string? userId = userIdValue?.ToString();
            string? username = HttpContext.Session.GetString("UserName");
            _logService.InsertLog("Update", $"Product Updated by {username}", userId);

            response = new ApiResponse("Product Updated Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Delete Product
        [HttpPatch("Delete/{ProductID}")]
        public IActionResult DeleteProduct(int productId)
        {
            ApiResponse response;

            if (productId <= 0)
            {
                response = new ApiResponse("ProductID is required", 400);
                return BadRequest(response);
            }

            bool isUpdated = _productService.DeleteProduct(productId);
            if (!isUpdated)
            {
                throw new Exception("Error while deleting product");
            }

            // for log
            int? userIdValue = HttpContext.Session.GetInt32("UserID");
            string? userId = userIdValue?.ToString();
            string? username = HttpContext.Session.GetString("UserName");
            _logService.InsertLog("Delete", $"Product Deleted by {username}", userId);

            response = new ApiResponse("Product Delete Successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}