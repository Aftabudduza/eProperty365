using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService;
using PropertyService.BO;

namespace PropertyService
{
    internal class PropertyEntity
    {
        private PropertyEntity()
        {
            // Prevent outside instantiation
        }

        //private static PropertyEntities _singleton = PropertyEntities.Create(Utility.CONNECTIONSTRING);

        private static PropertyEntities _singleton;
        public static PropertyEntities GetEntity()
        {           
            if (_singleton == null)
            {
                _singleton = new PropertyEntities();
            }
            return _singleton;
        }

        public static PropertyEntities GetFreshEntity()
        {
            _singleton = new PropertyEntities();
            return _singleton;
        }
    }


}
