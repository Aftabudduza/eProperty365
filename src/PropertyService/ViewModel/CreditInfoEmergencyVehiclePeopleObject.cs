using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;

namespace PropertyService.ViewModel
{
   public class CreditInfoEmergencyVehiclePeopleObject
    {
        public Residential_Tenant_Add_Step2_Page2_CreditHistory_New Credit { get; set; }
        //public List<Residential_Tenant_Add_Step2_Page2_EmergencyContacts> EmergencyContactses { get; set; } 
        public List<Residential_Tenant_Add_Step2_Page2_Vehicles>  Vehicles { get; set; }
        public List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit>  PeopleStayingUnit { get; set; }

    }
}
