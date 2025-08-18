using MyWebApiApp.Data;
using MyWebApiApp.Models;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Services.Implementations
{
    public class InvoiceService : IInvoiceServices
    {
        private readonly InvoiceRepository _invoiceRepository;
        public InvoiceService(InvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public bool InsertInvoice(int cartId)
        {
            bool inInserted = _invoiceRepository.CreateInvoiceFromCart(cartId);
            return inInserted;
        }

        public IEnumerable<InvoiceResponse> GetAllInvices(int? userId)
        {
            var invoices = _invoiceRepository.GetAllInvoices(userId);
            return invoices;
        }
    }
}