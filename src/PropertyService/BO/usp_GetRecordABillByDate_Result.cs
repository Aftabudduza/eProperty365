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
    
    public partial class usp_GetRecordABillByDate_Result
    {
        public int Id { get; set; }
        public string BillNumber { get; set; }
        public string Date { get; set; }
        public string PersonCompany { get; set; }
        public string ContactName { get; set; }
        public string Address1 { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
    }
}
