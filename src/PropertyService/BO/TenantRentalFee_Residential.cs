//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PropertyService.BO
{
    using System;
    using System.Collections.Generic;
    
    public partial class TenantRentalFee_Residential
    {
        public int Id { get; set; }
        public string ApplicationId { get; set; }
        public string Serial { get; set; }
        public string ResidentialUnitSerialId { get; set; }
        public string OwnerId { get; set; }
        public string PropertyManagerId { get; set; }
        public string LocationId { get; set; }
        public Nullable<decimal> SecurityDeposit { get; set; }
        public Nullable<decimal> MonthlyRent { get; set; }
        public Nullable<decimal> ProrateAmount { get; set; }
        public Nullable<decimal> FirstMonthRent { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CountryName { get; set; }
        public string CountryId { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string StateId { get; set; }
        public string ZipCode { get; set; }
        public string PaymentHeadType { get; set; }
        public string RoutingNo { get; set; }
        public string AccountNumber { get; set; }
        public Nullable<bool> RentalFeeAgrement { get; set; }
        public Nullable<decimal> SubTotalCharge { get; set; }
        public Nullable<decimal> CheckingAccountProcessingFee { get; set; }
        public Nullable<decimal> TotalAmountCharge { get; set; }
        public string PersonName { get; set; }
        public string PersonLast4CreditCardNumber { get; set; }
        public string CompanyName { get; set; }
        public string CashAmountPayDate { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public string Location { get; set; }
        public string CashPaymentDate { get; set; }
        public string Signature { get; set; }
        public string AuthorizationCode { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionDescription { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string bankname { get; set; }
        public string echecknumber { get; set; }
    }
}
