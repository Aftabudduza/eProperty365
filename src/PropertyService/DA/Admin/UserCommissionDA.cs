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
    public class AdminUserCommissionDA
    {
        PropertyEntities objPropertyEntities = null;
        public AdminUserCommissionDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = PropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public AdminUserCommissionDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = PropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = PropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }       
        public UserCommission GetBySerial(string serial)
        {
            UserCommission objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.UserCommission
                               where b.RefId == serial
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }      

        public UserCommission GetByID(int ID)
        {
            UserCommission objUser = null;
            try
            {         
                var empQuery = from b in objPropertyEntities.UserCommission
                               where b.Id == ID
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }

            return objUser;
        }
    
        public bool Insert(UserCommission objUser)
        {
            try
            {
                objPropertyEntities.UserCommission.Add(objUser);
                objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
            
        }

        public bool Update(UserCommission obj)
        {
            try
            {
                UserCommission existing = objPropertyEntities.UserCommission.Find(obj.Id);
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
                objPropertyEntities.UserCommission.Remove(this.GetByID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }

        public UserCommission GetUserBySQL(string sql)
        {
            UserCommission objUser = null;
            try
            {
               
                var empQuery = objPropertyEntities.UserCommission.SqlQuery(sql).ToList<UserCommission>().FirstOrDefault();
                objUser = empQuery;
                return objUser;
            }
            catch (Exception ex)
            {
                
            }

            return objUser;
        }   
       
        public List<UserCommission> GetAllInfo()
        {
            List<UserCommission> users = null;

            var empQuery = from b in objPropertyEntities.UserCommission
                           where b.Id > 0  && b.RefId != string.Empty
                           select b;
            users = empQuery.ToList();
            return users;
        }

        public List<UserCommission> GetByUserId(string sCode)
        {
            List<UserCommission> users = null;

            var empQuery = from b in objPropertyEntities.UserCommission
                           where b.Id > 0 && b.RefId == sCode
                           select b;
            users = empQuery.OrderBy(x=>x.CreateDate).ToList();
            return users;
        }

        public List<UserCommission> GetByUserIdAndLedgerCode(string sUserCode, string sLedgerCode)
        {
            List<UserCommission> users = null;

            var empQuery = from b in objPropertyEntities.UserCommission
                           where b.Id > 0 && b.RefId == sUserCode && b.LedgerCode == sLedgerCode
                           select b;
            users = empQuery.OrderBy(x => x.CreateDate).ToList();
            return users;
        }

        public List<UserCommission> GetBySearch(string sSearchQuery)
        {
            List<UserCommission> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  UserCommission where Id > 0 ";

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

                var empQuery = objPropertyEntities.UserCommission.SqlQuery(sSQL).ToList<UserCommission>();
                contacts = empQuery.OrderByDescending(x => x.CreateDate).ToList();

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
