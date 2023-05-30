using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;

namespace PropertyService.DA
{
   public class CommonDA
    {

        private EPropertyEntities _objPropertyEntities;

        public CommonDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public CommonDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public string MakeAutoGenLocation(string yourPrefix, string objName)
        {
            try
            {
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
                var oupParam = new ObjectParameter("NewID", 0) { Value = DBNull.Value };
                _objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                var sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                var serial = string.Concat(yourPrefix, sNumber);

                return serial;
            }
            catch (Exception ex)
            {
                // ignored
            }

            return null;
        }
        public List<RefCountries> GetCountrlList()
        {
            var lstOfCountry = new List<RefCountries>();
            try
            {
                lstOfCountry = _objPropertyEntities.RefCountries.ToList();
            }
            catch (Exception ex)
            {


            }
            return lstOfCountry;
        }
        public List<RefStates> GetStateList()
        {
            var lstOfStates = new List<RefStates>();
            try
            {
                lstOfStates = _objPropertyEntities.RefStates.ToList().Distinct().ToList();
            }
            catch (Exception ex)
            {


            }
            return lstOfStates;
        }
        public List<Cities> GetCityList()
        {
            var lstOfCities = new List<Cities>();
            try
            {
                lstOfCities = _objPropertyEntities.Cities.ToList().Distinct().ToList();
            }
            catch (Exception ex)
            {


            }
            return lstOfCities;
        }

    }
}
