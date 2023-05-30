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
    public class MasterDA
    {
        EPropertyEntities objPropertyEntities = null;
        public MasterDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public MasterDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public Master GetParentbyID(int id)
        {
            Master objMaster = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.Master
                               where b.Id == id
                               select b;

                objMaster = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objMaster;
        }

        public Master GetParentbyUserDefinedID(int userdefinedId)
        {
            Master objMaster = null;
            try
            {
                var empQuery = from b in objPropertyEntities.Master
                               where b.UserDefinedId == userdefinedId
                               select b;

                objMaster = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objMaster;
        }

        public bool Insert(Master objMaster)
        {
            try
            {                
                objPropertyEntities.Master.Add(objMaster);
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

        public bool Update(Master obj)
        {
            try
            {
                Master existing = objPropertyEntities.Master.Find(obj.Id);
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

        public bool DeleteByID(int id)
        {
            try
            {
                objPropertyEntities.Master.Remove(this.GetParentbyID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public UserProfile GetUserBySQL(string sql)
        //{
        //    UserProfile objUser = null;
        //    try
        //    {
        //        var empQuery = objPropertyEntities.UserProfile.SqlQuery(sql).ToList<UserProfile>().FirstOrDefault();
        //        objUser = empQuery;
               
        //    }
        //    catch (Exception ex)
        //    {
               
        //    }
        //    return objUser;
        //}

        public List<Master> GetAllMaster()
        {
            List<Master> listMaster = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.Master
                               where b.Id > 0
                               select b;

                listMaster = empQuery.OrderBy(x => x.UserDefinedId).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listMaster;
        }
    }
}
