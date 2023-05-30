using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyService.ViewModel
{
   public class EmployeementXML
    {
        public string OrganizationName { get; set; }
        public string Title { get; set; }
        public string JobLevelInfo { get; set; }
        public string StartDat { get; set; }
        public string EndDate { get; set; }
        public decimal Compensation { get; set; }
        public string FormattedName { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string Municipality { get; set; }
        public string AddressLine { get; set; }
        public string Telephone { get; set; }
    }
}
