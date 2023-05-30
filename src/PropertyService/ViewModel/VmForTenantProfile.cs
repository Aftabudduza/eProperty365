using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;

namespace PropertyService.ViewModel
{
  public class VmForTenantProfile
    {
        public aggrementNameOf aggrementNameOf { get; set; }
        public List<Emergency>  Emergency { get; set; }
        public List<Vehicle> Vehicle { get; set; }
        public List<People> People { get; set; }
    }

    public class aggrementNameOf
    {
        public string SerialId { get; set; }
        public string userEmail { get; set; }
        public string UnitId { get; set; }
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AliasName { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string RelationShip { get; set; }
        public string Other { get; set; }
        public string Birthday { get; set; }
        public decimal SecurityDeposit { get; set; }
        public decimal MonthlyRent { get; set; }

        public decimal OtherAmountHeld { get; set; }
        public DateTime LeaseSignDate { get; set; }
        public string MonthlyPayDueDate { get; set; }
    }
    public class aggrementNameOf_Save
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string ResidentialUnitSerialId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AliasName { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string RelationShip { get; set; }
        public string Other { get; set; }
        public string Birthday { get; set; }
        public string DriversLicenseNo { get; set; }
        public string LicenceState { get; set; }
        public string SocialSecurityNo { get; set; }
        public int NoOfPeopleLivingUnit { get; set; }
        public int NoOfPeopleLiving18 { get; set; }


    }

    public class Emergency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
        public string Relationship { get; set; }
        public string RelationshipName { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonPhoneNo { get; set; }
        public string EmailAddress { get; set; }
       
    }

    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string Year { get; set; }

    }

    public class People
    {
        public int Id { get;set; }
        public string Name { get;set; }
        public int Relationship { get; set; }
        public int Age { get; set; }

    }

    public class TenantSaveProfile
    {
        public Residential_Tenant_App_Step2_AgreementNameOf aggrementNameOf { get; set; }
        public List<Residential_Tenant_Add_Step2_Page2_Vehicles> Vehicles { get; set; }
        public List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit> PeopleStayingUnit { get; set; }
    }
    public class TenantSaveProfileNew
    {
        public Residential_Tenant_App_Step2_AgreementNameOf aggrementNameOf { get; set; }
        public List<Residential_Tenant_Add_Step2_Page2_Vehicles> Vehicles { get; set; }
        public List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit> PeopleStayingUnit { get; set; }
        public string Password { get; set; }
    }
}
