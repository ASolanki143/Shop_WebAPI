using MyWebApiApp.Models;

namespace MyWebApiApp.Services.Interfaces
{
    public interface IInvoiceServices
    {
        bool InsertInvoice(int CartID);
        IEnumerable<InvoiceModel> GetAllInvices(int? UserID);
        IEnumerable<InvoiceModel> GetAllInvoicesForUser(int UserID);
        
    }
}