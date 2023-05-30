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
    public class StateDA
    {
        EPropertyEntities objPropertyEntities = null;
        public StateDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public StateDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public RefStates GetByCode(string sCode)
        {
            RefStates objRefStates = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.RefStates
                               where b.STATE == sCode
                               select b;

                objRefStates = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objRefStates;
        }     
        public bool Insert(RefStates objRefStates)
        {
            try
            {                
                objPropertyEntities.RefStates.Add(objRefStates);
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
        public bool Update(RefStates obj)
        {
            try
            {
                RefStates existing = objPropertyEntities.RefStates.Find(obj.STATE);
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
                objPropertyEntities.RefStates.Remove(this.GetByCode(code));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<RefStates> GetAllRefStates()
        {
            List<RefStates> listRefStates = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.RefStates                             
                               select b;

                listRefStates = empQuery.OrderBy(x => x.STATENAME).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listRefStates;
        }
    }
}
