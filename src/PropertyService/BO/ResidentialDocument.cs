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
    
    public partial class ResidentialDocument
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string PropertyManagerSerialId { get; set; }
        public string DocumrntId { get; set; }
        public string DocumentDescription { get; set; }
        public string TypeOfDocument { get; set; }
        public string DateAdded { get; set; }
        public Nullable<short> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<short> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}