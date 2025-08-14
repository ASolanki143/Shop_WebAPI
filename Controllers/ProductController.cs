using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
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
                response = new ApiResponse("Error while inserting product", 400);
                return BadRequest(response);
            }
            response = new ApiResponse("Inserted Successfully",200);
            return Ok(response);
        }
        #endregion

        #region Update Product
        [HttpPut("{ProductID}")]
        public IActionResult UpdateProduct(int ProductID, ProductModel product)
        {
            if (ProductID <= 0)
            {
                return BadRequest("ProductID is required");
            }
            product.ProductID = ProductID;
            bool isUpdated = _productService.EditProduct(product);
            if (!isUpdated)
            {
                return BadRequest("Error while updating product");
            }
            return Ok("Product Updated Successfully");
        }
        #endregion

        #region Delete Product
        [HttpPatch("Delete{ProductID}")]
        public IActionResult DeleteProduct(int ProductID)
        {
            if (ProductID <= 0)
            {
                return BadRequest("ProductID is required");
            }
            bool isUpdated = _productService.DeleteProduct(ProductID);
            if (!isUpdated)
            {
                return BadRequest("Error while deleting product");
            }
            return Ok("Product Delete Successfully");
        }
        #endregion
    }
}