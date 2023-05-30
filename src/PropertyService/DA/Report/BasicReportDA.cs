using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;

namespace PropertyService.DA.Report
{
    public class BasicReportDA
    {
        private EPropertyEntities _objPropertyEntities;
        private PropertyEntities _propertyEntities;
        public BasicReportDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _propertyEntities = PropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public BasicReportDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public List<BO.Report> GetReportList()
        {
            var report = _propertyEntities.Report.Where(x => x.isActive == true).ToList();
            return report;
        }

        public string GetCompanyName(string serial)
        {
            _propertyEntities = PropertyEntity.GetEntity();
            var companyName = _propertyEntities.OwnerProfile.Where(x => x.Serial == serial).FirstOrDefault().CompanyName;
            return companyName;
        }
    }
}
