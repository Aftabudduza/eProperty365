using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyService.ViewModel
{
   public class TenantCommonModel
    {
        public string Serial { get; set; }
        public string ResidentialUnitSerialId { get; set; }
    }

    public class TenantSignInModel
    {
        public string Serial { get; set; }
        public bool isFirstSignIn { get; set; }
        public string ResidentialUnitSerialId { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
    public class TenantPaymentModel
    {
        public string AuthCode { get; set; }
        public bool IsSuccess { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionDetails { get; set; }

    }

    public class TenantImportModel
    {
        public string SerialId { get; set; }
        public string LocationId { get; set; }
        public string UnitId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string SecurityDeposit { get; set; }
        public string MonthlyRentHeld { get; set; }
        public string OtherAmountHeld { get; set; }

        public string LeaseSignDate { get; set; }
        public string MonthlyPayDueDate { get; set; }

    }
}
