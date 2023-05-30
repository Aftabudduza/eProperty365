using System.Collections.Generic;
using PropertyService.BO;

namespace PropertyService.ViewModel
{
    public class MakeBillPaymentModel
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public List<RecordABillDetails> lstDetails { get; set; }
        public List<MakeBillPayment> lstSaveBillPayments { get; set; }

        public MakeBillPaymentModel()
        {
            lstDetails = new List<RecordABillDetails>();
            lstSaveBillPayments = new List<MakeBillPayment>();
        }
    }
    
}
