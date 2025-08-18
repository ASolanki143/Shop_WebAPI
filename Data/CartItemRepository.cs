using System.Data;
using MyWebApiApp.Models;
using Microsoft.Data.SqlClient;
using MyWebApiApp.Utilities;
using MyWebApiApp.Models.DTOs;

namespace MyWebApiApp.Data
{
    public class CartItemRepository
    {
        private readonly DBHelper _dBHelper;

        public CartItemRepository(DBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        #region Add Cart Item
        public bool AddCartItem(CartItemDto cartItemModel)
        {
            int rowAffected = _dBHelper.ExecuteNonQuery(
                "PR_CartItem_Add",
                new SqlParameter("@CartID", cartItemModel.CartID),
                new SqlParameter("@ProductID", cartItemModel.ProductID),
                new SqlParameter("@Quantity", cartItemModel.Quantity)
            );
            return rowAffected > 0;
        }
        #endregion

        #region Update Cart Item
        public bool UpdateCartItem(int cartItemId, int quantity)
        {
            int rowAffected = _dBHelper.ExecuteNonQuery(
                "PR_CartItem_Update",
                new SqlParameter("@CartItemID", cartItemId),
                new SqlParameter("@Quantity", quantity)
            );
            return rowAffected > 0;
        }
        #endregion

        #region Delete Cart Item
        public bool DeleteCartItem(int cartItemId)
        {
            int rowAffected = _dBHelper.ExecuteNonQuery(
                "PR_CartItem_Delete",
                new SqlParameter("@CartItemID", cartItemId)
            );
            return rowAffected > 0;
        }
        #endregion

        #region Get Cart Items by Cart
        public IEnumerable<CartItemModel> GetCartItemsByCartOrInvoice(int? cartId, int? invoiceId)
        {
            var items = new List<CartItemModel>();
            var dt = _dBHelper.ExecuteDataTable(
                "PR_CartItem_ListByCart",
                new SqlParameter("@CartID", cartId),
                new SqlParameter("@InvoiceID", invoiceId)
            );

            foreach (DataRow row in dt.Rows)
            {
                items.Add(new CartItemModel()
                {
                    CartItemID = Convert.ToInt32(row["CartItemID"]),
                    ProductID = Convert.ToInt32(row["ProductID"]),
                    ProductName = row["ProductName"].ToString(),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                    Price = Convert.ToDecimal(row["Price"])
                    });
            }
            return items;
        }
        #endregion
    }
}
