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
    public class AdminOwnerProfileDA
    {
        PropertyEntities objPropertyEntities = null;
        public AdminOwnerProfileDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = PropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public AdminOwnerProfileDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = PropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = PropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }       
        public OwnerProfile GetByUserName(string serial)
        {
            OwnerProfile objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.OwnerProfile
                               where b.Serial == serial
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }

        public OwnerProfile GetBySerial(string serial)
        {
            OwnerProfile objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.OwnerProfile
                               where b.Serial == serial
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }

        public OwnerProfile GetUserByUserID(int userID)
        {
            OwnerProfile objUser = null;
            try
            {         
                var empQuery = from b in objPropertyEntities.OwnerProfile
                               where b.Id == userID
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }

            return objUser;
        }
    
        public bool Insert(OwnerProfile objOwner)
        {
            try
            {
                objPropertyEntities.OwnerProfile.Add(objOwner);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(OwnerProfile obj)
        {
            try
            {
                OwnerProfile existing = objPropertyEntities.OwnerProfile.Find(obj.Id);
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
                objPropertyEntities.OwnerProfile.Remove(this.GetUserByUserID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }

        public OwnerProfile GetUserBySQL(string sql)
        {
            OwnerProfile objUser = null;
            try
            {
               
                var empQuery = objPropertyEntities.OwnerProfile.SqlQuery(sql).ToList<OwnerProfile>().FirstOrDefault();
                objUser = empQuery;
                return objUser;
            }
            catch (Exception ex)
            {
                
            }

            return objUser;
        }   
       
        public List<OwnerProfile> GetAllOwnersInfo()
        {
            List<OwnerProfile> users = null;

            var empQuery = from b in objPropertyEntities.OwnerProfile
                           where b.Id > 0 && b.IsDelete == false && b.Serial != string.Empty
                           select b;
            users = empQuery.ToList();
            return users;
        }
        public List<OwnerProfile> GetBySearch(string sSearchQuery)
        {
            List<OwnerProfile> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  OwnerProfile where IsActive = 1 and IsDelete = 0 ";

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

                var empQuery = objPropertyEntities.OwnerProfile.SqlQuery(sSQL).ToList<OwnerProfile>();
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
                objPropertyEntities = PropertyEntity.GetFreshEntity();
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
