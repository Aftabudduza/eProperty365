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
    
    public partial class XMLOperationStatus
    {
        public int Id { get; set; }
        public string UnitId { get; set; }
        public string TenantId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
