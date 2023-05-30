using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyService.ViewModel
{
    public class MonthlyBatchRentalInvoiceModel
    {
        public string OwnerId { get; set; }
        public string PropertyManagerSerialId { get; set; }
        public string LocationSerialId { get; set; }
        public string OwnerName { get; set; }
        public string PropertyManagerName { get; set; }
        public string LocationName { get; set; }
        public List<MonthlyBatchRentalInvoiceTenantMail> lst { get; set; }

        public MonthlyBatchRentalInvoiceModel()
        {
            lst=new List<MonthlyBatchRentalInvoiceTenantMail>();
        }
    }
    public class MonthlyBatchRentalInvoiceTenantMail
    {
        public string UnitSerialId { get; set; }
        public string UnitName { get; set; }
        public string TenantName { get; set; }
        public string TenantSerialId { get; set; }
        public string Amount { get; set; }
        public string EmailId { get; set; }
        public string SendDate { get; set; }
        public string InvoiceID  { get; set; }

    }
    public class MonthlyBatchRentalInvoiceTenantSave
    {
        public string RefId { get; set; }
        public string PropertyManagerId { get; set; }
        public string LocationId { get; set; }
        public string InvoiceNo { get; set; }
        public string TransactionType { get; set; }
        public string AccountType { get; set; }
        public string LedgerCode { get; set; }
        public string Amount { get; set; }

    }
}
