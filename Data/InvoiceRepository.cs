using System.Data;
using Microsoft.Data.SqlClient;
using MyWebApiApp.Models;
using MyWebApiApp.Utilities;

namespace MyWebApiApp.Data
{
    public class InvoiceRepository
    {
        private readonly DBHelper _dBHelper;

        public InvoiceRepository(DBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        #region Create Invoice from Cart
        public bool CreateInvoiceFromCart(int cartId)
        {
            int rowAffected = _dBHelper.ExecuteNonQuery(
                "PR_Invoice_CreateFromCart",
                new SqlParameter("@CartID", cartId)
            );
            return rowAffected > 0;
        }
        #endregion

        #region List All Invoices
        public IEnumerable<InvoiceModel> GetAllInvoices(int? UserID)
        {
            var invoices = new List<InvoiceModel>();
            var dt = _dBHelper.ExecuteDataTable(
                "PR_Invoice_ListAll",
                new SqlParameter("@UserID",UserID)
            );

            foreach (DataRow row in dt.Rows)
            {
                invoices.Add(new InvoiceModel()
                {
                    InvoiceID = Convert.ToInt32(row["InvoiceID"]),
                    UserID = Convert.ToInt32(row["UserID"]),
                    TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                    UserName = row["UserName"].ToString(),
                    Email = row["Email"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    CartItemCount = Convert.ToInt32(row["CartItemCount"])
                    });
            }
            return invoices;
        }
        #endregion

        #region Get All Invoices By User
        public IEnumerable<InvoiceModel> GetAllInvoiceByUser(int UserID)
        {
            var invoices = new List<InvoiceModel>();
            var dt = _dBHelper.ExecuteDataTable(
                "PR_Invoice_SelectByUser",
                new SqlParameter("@UserID",UserID)
            );

            foreach (DataRow row in dt.Rows)
            {
                invoices.Add(new InvoiceModel()
                {
                    InvoiceID = Convert.ToInt32(row["InvoiceID"]),
                    UserID = Convert.ToInt32(row["UserID"]),
                    TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                    UserName = row["UserName"].ToString(),
                    Email = row["Email"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                        
                    });
            }
            return invoices;
        }
        #endregion
    }
}
