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
    
    public partial class MonthlyBatchRentalInvoice
    {
        public long Id { get; set; }
        public string RefId { get; set; }
        public string PropertyManagerId { get; set; }
        public string LocationId { get; set; }
        public string InvoiceNo { get; set; }
        public string TransactionType { get; set; }
        public string AccountType { get; set; }
        public string LedgerCode { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string UserType { get; set; }
        public string UnitId { get; set; }
        public string OwnerId { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}