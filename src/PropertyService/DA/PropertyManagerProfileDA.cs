using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;
using PropertyService;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace PropertyService.DA
{
    public class PropertyManagerProfileDA
    {
        EPropertyEntities objPropertyEntities = null;
        public PropertyManagerProfileDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public PropertyManagerProfileDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }       
        public PropertyManagerProfile GetBySerial(string serial)
        {
            PropertyManagerProfile objPropertyManager = null;
            try
            {
                var empQuery = from b in objPropertyEntities.PropertyManagerProfile
                               where b.Serial == serial
                               select b;

                objPropertyManager = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objPropertyManager;
        }
        public PropertyManagerProfile GetByOwner(string ownerId)
        {
            PropertyManagerProfile objPropertyManager = null;
            try
            {
                var empQuery = from b in objPropertyEntities.PropertyManagerProfile
                               where b.OwnerId == ownerId
                               select b;

                objPropertyManager = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objPropertyManager;
        }
        public PropertyManagerProfile GetByID(int nID)
        {
            PropertyManagerProfile objOwner = null;
            try
            {         
                var empQuery = from b in objPropertyEntities.PropertyManagerProfile
                               where b.Id == nID
                               select b;

                objOwner = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }

            return objOwner;
        }    
        public bool Insert(PropertyManagerProfile objPropertyManager)
        {
            objPropertyEntities.PropertyManagerProfile.Add(objPropertyManager);
            objPropertyEntities.SaveChanges();

            return true;
        }
        public bool Update(PropertyManagerProfile obj)
        {
            try
            {
                PropertyManagerProfile existing = objPropertyEntities.PropertyManagerProfile.Find(obj.Id);
                ((IObjectContextAdapter)objPropertyEntities).ObjectContext.Detach(existing);
                objPropertyEntities.Entry(obj).State = EntityState.Modified;               
                objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }
        public bool DeleteByID(int id)
        {
            try
            {
                objPropertyEntities.PropertyManagerProfile.Remove(this.GetByID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }
        public PropertyManagerProfile GetUserBySQL(string sql)
        {
            PropertyManagerProfile objUser = null;
            try
            {
               
                var empQuery = objPropertyEntities.PropertyManagerProfile.SqlQuery(sql).ToList<PropertyManagerProfile>().FirstOrDefault();
                objUser = empQuery;
                return objUser;
            }
            catch (Exception ex)
            {
                
            }

            return objUser;
        }          
        public List<PropertyManagerProfile> GetAllInfo()
        {
            List<PropertyManagerProfile> users = null;

            var empQuery = from b in objPropertyEntities.PropertyManagerProfile
                           where b.Id > 0 && b.IsDelete == false && b.Serial != string.Empty
                           select b;
            users = empQuery.ToList();
            return users;
        }
        public List<PropertyManagerProfile> GetByOwnerId(string ownerId)
        {
            List<PropertyManagerProfile> users = null;

            var empQuery = from b in objPropertyEntities.PropertyManagerProfile
                           where b.Id > 0 && b.IsDelete == false && b.OwnerId == ownerId
                           select b;
            users = empQuery.ToList();
            return users;
        }
        public List<PropertyManagerProfile> GetBySearch(string sSearchQuery)
        {
            List<PropertyManagerProfile> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  PropertyManagerProfile where IsActive = 1 and IsDelete = 0 ";

                try
                {
                    if (!string.IsNullOrEmpty(sSearchQuery))
                    {
                        sSQL += " and (FirstName like '%" + sSearchQuery + "%' or CompanyName like '%" + sSearchQuery + "%'  or LastName like '%" + sSearchQuery + "%'  or LastName like '%" + sSearchQuery + "%')";

                    }
                }
                catch (Exception ex)
                {

                }

                var empQuery = objPropertyEntities.PropertyManagerProfile.SqlQuery(sSQL).ToList<PropertyManagerProfile>();
                contacts = empQuery.OrderBy(x => x.FirstName).ToList();

            }
            catch (Exception ex)
            {

            }

            return contacts;
        }
        public string MakeAutoGenSerial(string yourPrefix, string objName)
        {
            string serial = "";
            try
            {
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
                string prefix = "";
               // prefix = DateTime.Now.Year.ToString();
                ObjectParameter oupParam = new ObjectParameter("NewID", 0);
                oupParam.Value = DBNull.Value;
                objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                string sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                serial = string.Concat(yourPrefix + prefix , sNumber);

                return serial;
            }
            catch (Exception ex)
            {

            }


            return null;

        }

    }
}
