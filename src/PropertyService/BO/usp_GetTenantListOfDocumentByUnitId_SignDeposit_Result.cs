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
    
    public partial class usp_GetTenantListOfDocumentByUnitId_SignDeposit_Result
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string ResidentialUnitSerialId { get; set; }
        public string DocumentDescription { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string IsViewedOrDownloaded { get; set; }
        public Nullable<int> TenentAddiId { get; set; }
        public string CurrentStatus { get; set; }
        public string IsChecked { get; set; }
    }
}
