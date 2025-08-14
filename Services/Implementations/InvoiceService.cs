using MyWebApiApp.Data;
using MyWebApiApp.Models;
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

        public bool InsertInvoice(int CartID)
        {
            bool inInserted = _invoiceRepository.CreateInvoiceFromCart(CartID);
            return inInserted;
        }

        public IEnumerable<InvoiceModel> GetAllInvices(int? UserID)
        {
            var invoices = _invoiceRepository.GetAllInvoices(UserID);
            return invoices;
        }

        public IEnumerable<InvoiceModel> GetAllInvoicesForUser(int UserID)
        {
            var invoices = _invoiceRepository.GetAllInvoiceByUser(UserID);
            return invoices;
        }
    }
}