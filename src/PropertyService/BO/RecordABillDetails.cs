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
    
    public partial class RecordABillDetails
    {
        public int Id { get; set; }
        public Nullable<int> RecordABillId { get; set; }
        public string DueDate { get; set; }
        public string BillId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string CreditAccountName { get; set; }
        public string DebitAccountName { get; set; }
        public string CreditLedgerCode { get; set; }
        public string DebitLedgerCode { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}