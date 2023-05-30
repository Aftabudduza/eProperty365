using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;


namespace PropertyService.DA
{
    public class CountryDA
    {
        EPropertyEntities objPropertyEntities = null;
        public CountryDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public CountryDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public RefCountries GetByCode(string sName)
        {
            RefCountries objRefCountries = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.RefCountries
                               where b.COUNTRY == sName
                               select b;

                objRefCountries = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objRefCountries;
        }  
        public bool Insert(RefCountries objRefCountries)
        {
            try
            {                
                objPropertyEntities.RefCountries.Add(objRefCountries);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertChild(Child objChild)
        {
            try
            {
               
                objPropertyEntities.Child.Add(objChild);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(RefCountries obj)
        {
            try
            {
                RefCountries existing = objPropertyEntities.RefCountries.Find(obj.COUNTRY);
                ((IObjectContextAdapter)objPropertyEntities).ObjectContext.Detach(existing);
                objPropertyEntities.Entry(obj).State = EntityState.Modified;
                objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteByID(string code)
        {
            try
            {
                objPropertyEntities.RefCountries.Remove(this.GetByCode(code));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<RefCountries> GetAllRefCountries()
        {
            List<RefCountries> listRefCountries = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.RefCountries                             
                               select b;

                listRefCountries = empQuery.OrderBy(x => x.COUNTRYNAME).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listRefCountries;
        }

    }
}
