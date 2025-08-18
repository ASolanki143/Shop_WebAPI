using System.Data;
using Microsoft.Data.SqlClient;
using MyWebApiApp.Models.DTOs;
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
        public IEnumerable<InvoiceResponse> GetAllInvoices(int? userId)
        {
            var invoices = new List<InvoiceResponse>();
            var dt = _dBHelper.ExecuteDataTable(
                "PR_Invoice_ListAll",
                new SqlParameter("@UserID", userId)
            );

            foreach (DataRow row in dt.Rows)
            {
                invoices.Add(new InvoiceResponse()
                {
                    TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                    UserName = row["UserName"].ToString(),
                    Email = row["Email"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    InvoiceItemCount = Convert.ToInt32(row["InvoiceItemCount"])
                });
            }
            return invoices;
        }
        #endregion

    }
}
