using System;
using System.Collections.Generic;

namespace PropertyService.ViewModel
{
    public class DealerSalesPartnerModel
    {
        public int Id { get; set; }
        public string SerialCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string CountryId { get; set; }
        public string Region { get; set; }
        public string ZipCode { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string MobilePhoneNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileName { get; set; }
        public string UserType { get; set; }
        public string JoinDate { get; set; }
        public string RoutingNo { get; set; }
        public string AccountNo { get; set; }
        public decimal? CommissionRate { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsAdmin { get; set; }
        public int userProfileId { get; set; }
        public List<DealerSalesPartnerDetailsZipCodeCoverageModel> ListDetails { get; set; }
        public DealerSalesPartnerAccounts ObjAccounts { get; set; }
    }

    public class DealerSalesPartnerDetailsZipCodeCoverageModel
    {
        public int Id { get; set; }
        public string DealerSalesPartnerId { get; set; }
        public string ZipCode { get; set; }
        public decimal? CommissionRate { get; set; }
    }

    public class DealerSalesPartnerAccounts
    {
        public string PartnerName { get; set; }
        public string LocationId { get; set; }
        public string OwnerId { get; set; }
        public List<Dropdown> Location { get; set; }
        public List<Dropdown> Owner { get; set; }
    }
    
    public class Dropdown
    {
        public string Id2 { get; set; }
        public string Data { get; set; }
    }
}
