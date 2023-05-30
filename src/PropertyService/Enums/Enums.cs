using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyService.Enums
{
    public enum EnumUserType
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Owner")]
        Owner = 2,
        [Description("Property Manager")]
        Manager = 3,
        [Description("Maintenance Manager")]
        Maintenance = 4,
        [Description("Resident Tenant")]
        Resident = 5,
        [Description("Commercial Tenant")]
        Commercial = 6,
        [Description("Condo Tenant")]
        Condo = 7,
        [Description("Normal")]
        Normal = 8,
        [Description("Sales Partner")]
        SalesPartner = 9,
        [Description("Dealer")]
        Dealer = 10
    }

    public enum EnumOp
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }

    public enum EnumBasicData
    {
        [Description("Country")]
        Country = 1,
        [Description("Currency")]
        Currency = 2,
        [Description("State")]
        State = 3,
        [Description("Contact Type")]
        ContactType = 4,
        [Description("No Order Reason")]
        NoOrderReason = 5,
        [Description("Month")]
        Month = 6
    }
    public enum EnumGlobalData
    {
        [Description("Contact Type")]
        ContactType = 4,
        [Description("Ledger Code")]
        Ledger = 7,
        [Description("Payment Mode")]
        Payment = 8
    }

    public enum ItemStaus : short
    {
        NoFilter = 0,
        Active = 1,
        Inactive = 2
    }

}
