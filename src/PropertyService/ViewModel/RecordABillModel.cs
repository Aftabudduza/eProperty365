using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;

namespace PropertyService.ViewModel
{
    public class RecordABillModel
    {
        public RecordABill Master { get; set; }

        public List<RecordBillDetails> Details { get; set; }
        //public TYPE Type { get; set; }
    }

    public class RecordBillDetails : RecordABillDetails
    {
        public List<AccountChart> LstCharts { get; set; }

        public RecordBillDetails()
        {
            LstCharts = new List<AccountChart>();
        }
    }
}
