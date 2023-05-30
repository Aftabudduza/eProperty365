using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;

namespace PropertyService.ViewModel
{
   public class VmRentalEquipment
    {
        public List<ResidentialUnitEquipmentImage> EqIage { get; set; }
        public ResidentialUnitEquipment RentalUnit { get; set; }
    }
}
