using MyWebApiApp.Models;

namespace MyWebApiApp.Services.Interfaces
{
    public interface IProductServices
    {
        IEnumerable<ProductModel> GetAllProducts();
        bool AddProduct(ProductModel product);
        bool EditProduct(ProductModel product);
        bool DeleteProduct(int ProductID); 
    }
}