using System.Data;
using Microsoft.Data.SqlClient;
using MyWebApiApp.Models;
using MyWebApiApp.Utilities;

namespace MyWebApiApp.Data
{
    public class ProductRepository
    {
        private readonly DBHelper _dBHelper;
        public ProductRepository(DBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        #region Get All Product
        public IEnumerable<ProductModel> SelectAllProducts()
        {
            var products = new List<ProductModel>();
            var dt = _dBHelper.ExecuteDataTable("PR_Product_ListAll");

            foreach(DataRow row in dt.Rows) {
                products.Add(new ProductModel()
                {
                    ProductID = Convert.ToInt32(row["ProductID"]),
                    ProductName = row["ProductName"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }
            return products;
        }
        #endregion

        #region Add Product
        public bool AddProduct(ProductModel product)
        {
            int rowAffected = _dBHelper.ExecuteNonQuery(
                "PR_Product_Add",
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Quantity", product.Quantity)
            );
            
            return rowAffected > 0;
        }
        #endregion

        #region Update Product
        public bool UpdateProduct(ProductModel product)
        {
            int rowAffected = _dBHelper.ExecuteNonQuery(
                "PR_Product_Update",
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Quantity", product.Quantity),
                new SqlParameter("@ProductID", product.ProductID)
            );
            
            return rowAffected > 0;
        }
        #endregion

        #region Delete Product
        public bool DeleteProduct(int productId)
        {
            int rowAffected = _dBHelper.ExecuteNonQuery(
                "PR_Product_Delete",
                new SqlParameter("@ProductID", productId)
            );
            return rowAffected > 0;
        }
        #endregion
    }
}