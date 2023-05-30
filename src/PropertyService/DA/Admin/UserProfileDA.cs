using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;
using PropertyService;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace PropertyService.Admin.DA
{
    public class AdminUserProfileDA
    {

        //PropertyEntities objPropertyEntities = PropertyEntities.Create(Utility.CONNECTIONSTRING);
        PropertyEntities objPropertyEntities = null;
        public AdminUserProfileDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = PropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public AdminUserProfileDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = PropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = PropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public UserProfile GetUserByEmailPassword(string userName, string passWord)
        {
            UserProfile objUser = null;
            try
            {
              
                passWord = Utility.base64Encode(passWord);
                var empQuery = from b in objPropertyEntities.UserProfile
                               where (b.Username == userName || b.Email == userName) && b.Password == passWord && b.IsActive == true
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();

            }
            catch (Exception ex)
            {

            }

            return objUser;
        }
        public UserProfile GetUserByIDPassword(string userName, string passWord, int nAccountType)
        {
            UserProfile objUser = null;
            try
            {
                passWord = Utility.base64Encode(passWord);
                var empQuery = from b in objPropertyEntities.UserProfile
                                where (b.Username == userName || b.Email == userName) && b.Password == passWord && b.IsActive == true && b.UserType == nAccountType.ToString()
                                select b;

                objUser = empQuery.ToList().FirstOrDefault();               
              
            }
            catch (Exception ex)
            {
               
            }

            return objUser;
        }

        public UserProfile GetUserBySearch(string userName, string passWord, string Serial, int nAccountType)
        {
            UserProfile objUser = null;
            try
            {
                passWord = Utility.base64Encode(passWord);
                var empQuery = from b in objPropertyEntities.UserProfile
                               where (b.Username == userName || b.Email == userName) && b.Password == passWord && b.OwnerId == Serial && b.UserType == nAccountType.ToString() && b.IsActive == true
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();

            }
            catch (Exception ex)
            {

            }

            return objUser;
        }
        public UserProfile GetUserByEmail(string Email)
        {
            UserProfile objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.UserProfile
                               where b.Email == Email && b.IsActive == true
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }
        public List<UserProfile> GetUsersByUserName2(string userName)
        {
            List<UserProfile> objUsers = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.UserProfile
                               where b.Username == userName 
                               select b;

                objUsers = empQuery.ToList();

                
            }
            catch (Exception ex)
            {
               
            }

            return objUsers;
        }

        public List<UserProfile> GetUsersByAccountType(int nAccountType)
        {
            List<UserProfile> objUsers = null;
            try
            {
                var empQuery = from b in objPropertyEntities.UserProfile
                               where b.UserType == nAccountType.ToString()
                               select b;

                objUsers = empQuery.ToList();


            }
            catch (Exception ex)
            {

            }

            return objUsers;
        }
        public UserProfile GetUserByUserID(int userID)
        {
            UserProfile objUser = null;
            try
            {         
                var empQuery = from b in objPropertyEntities.UserProfile
                               where b.Id == userID
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }

            return objUser;
        }

        public UserProfile GetUserByOwnerSerial(string serial)
        {
            UserProfile objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.UserProfile
                               where b.OwnerId == serial
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }


        public bool Insert(UserProfile objUser)
        {
            try
            {
                objPropertyEntities.UserProfile.Add(objUser);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(UserProfile obj)
        {
            try
            {
                UserProfile existing = objPropertyEntities.UserProfile.Find(obj.Id);
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
                objPropertyEntities.UserProfile.Remove(this.GetUserByUserID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }

        public UserProfile GetUserBySQL(string sql)
        {
            UserProfile objUser = null;
            try
            {
               
                var empQuery = objPropertyEntities.UserProfile.SqlQuery(sql).ToList<UserProfile>().FirstOrDefault();
                objUser = empQuery;
                return objUser;
            }
            catch (Exception ex)
            {
                
            }

            return objUser;
        }
        public string MakeAutoGenLocation(string yourPrefix, string objName)
        {
            string serial = "";
            try
            {
                objPropertyEntities = PropertyEntity.GetFreshEntity();
                string prefix = "";
                prefix = DateTime.Now.Year.ToString();
                ObjectParameter oupParam = new ObjectParameter("NewID", 0);
                oupParam.Value = DBNull.Value;
                objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                string sNumber = oupParam.Value.ToString().PadLeft(6, '0');
                serial = string.Concat(yourPrefix, sNumber);

                return serial;
            }
            catch (Exception ex)
            {

            }

            return null;

        }
        public List<UserProfile> GetAllUsers()
        {
            List<UserProfile> users = null;

            var empQuery = from b in objPropertyEntities.UserProfile
                           where b.Id > 0 
                           select b;
            users = empQuery.ToList();
            return users;
        }

        public List<UserProfile> GetAllUsersByCreatorAndStatus(int nCreatedBy, bool bStatus)
        {
            List<UserProfile> users = null;

            var empQuery = from b in objPropertyEntities.UserProfile
                           where (b.Id != nCreatedBy && b.CreatedBy == nCreatedBy && b.IsActive == bStatus)
                           select b;
            users = empQuery.ToList();
            return users;
        }

        public List<UserProfile> GetAllUsersInfo(int nAccountType)
        {
            List<UserProfile> users = null;

            var empQuery = from b in objPropertyEntities.UserProfile
                           where b.Id > 0 && b.IsActive == true && b.CanLogin == false && b.UserType == nAccountType.ToString()
                           select b;
            users = empQuery.ToList();
            return users;
        }
        public List<UserProfile> GetBySearch(string sSearchQuery)
        {
            List<UserProfile> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  UserProfile where IsActive = 1 and CanLogin = 0 and UserType = '8'  ";

                try
                {
                    if (!string.IsNullOrEmpty(sSearchQuery))
                    {
                        sSQL += " and (Username like '%" + sSearchQuery + "%' or Title like '%" + sSearchQuery + "%'  or Email like '%" + sSearchQuery + "%'  or Phone like '%" + sSearchQuery + "%')";

                    }
                }
                catch (Exception ex)
                {

                }

                var empQuery = objPropertyEntities.UserProfile.SqlQuery(sSQL).ToList<UserProfile>();
                contacts = empQuery.OrderBy(x => x.Username).ToList();

            }
            catch (Exception ex)
            {

            }

            return contacts;
        }

    }
}
