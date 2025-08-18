using MyWebApiApp.Models.DTOs;

namespace MyWebApiApp.Services.Interfaces
{
    public interface IInvoiceServices
    {
        bool InsertInvoice(int cartId);
        IEnumerable<InvoiceResponse> GetAllInvices(int? userId);        
    }
}