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
    
    public partial class ResidentialTenantSignIn
    {
        public int Id { get; set; }
        public string SerialId { get; set; }
        public string UnitId { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ApprovalCode { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ApplicationCode { get; set; }
        public string ApproveStatus { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
