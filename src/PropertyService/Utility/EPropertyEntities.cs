using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService;
using PropertyService.BO;

namespace PropertyService
{
    internal class EPropertyEntity
    {
        private EPropertyEntity()
        {
            // Prevent outside instantiation
        }

        private static EPropertyEntities _singleton;

        public static EPropertyEntities GetEntity()
        {           
            if (_singleton == null)
            {
                _singleton = new EPropertyEntities();
            }
            return _singleton;
        }

        public static EPropertyEntities GetFreshEntity()
        {
            _singleton = new EPropertyEntities();
            return _singleton;
        }
    }


}
