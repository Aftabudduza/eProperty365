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
    
    public partial class Child
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int UserDefinedId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<short> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<short> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string OwnerId { get; set; }
    
        public virtual Master Master { get; set; }
    }
}
