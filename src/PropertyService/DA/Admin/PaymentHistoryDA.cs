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
    public class AdminPaymentHistoryDA
    {
        PropertyEntities objPropertyEntities = null;
        public AdminPaymentHistoryDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = PropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public AdminPaymentHistoryDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = PropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = PropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }       
        public PaymentHistory GetBySerial(string serial)
        {
            PaymentHistory objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.PaymentHistory
                               where b.Serial == serial
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }      

        public PaymentHistory GetByID(int ID)
        {
            PaymentHistory objUser = null;
            try
            {         
                var empQuery = from b in objPropertyEntities.PaymentHistory
                               where b.Id == ID
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }

            return objUser;
        }
        public int Insert(PaymentHistory obj)
        {
            try
            {
                objPropertyEntities.PaymentHistory.Add(obj);
                objPropertyEntities.SaveChanges();
                return obj.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        //public bool Insert(PaymentHistory objUser)
        //{
        //    objPropertyEntities.PaymentHistory.Add(objUser);
        //    objPropertyEntities.SaveChanges();

        //    return true;
        //}

        public bool Update(PaymentHistory obj)
        {
            try
            {
                PaymentHistory existing = objPropertyEntities.PaymentHistory.Find(obj.Id);
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
                objPropertyEntities.PaymentHistory.Remove(this.GetByID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }

        public PaymentHistory GetUserBySQL(string sql)
        {
            PaymentHistory objUser = null;
            try
            {
               
                var empQuery = objPropertyEntities.PaymentHistory.SqlQuery(sql).ToList<PaymentHistory>().FirstOrDefault();
                objUser = empQuery;
                return objUser;
            }
            catch (Exception ex)
            {
                
            }

            return objUser;
        }   
       
        public List<PaymentHistory> GetAllInfo()
        {
            List<PaymentHistory> users = null;

            var empQuery = from b in objPropertyEntities.PaymentHistory
                           where b.Id > 0  && b.Serial != string.Empty
                           select b;
            users = empQuery.ToList();
            return users;
        }

        public List<PaymentHistory> GetUnApprovedPaymentHistory(string status)
        {
            List<PaymentHistory> users = null;

            var empQuery = from b in objPropertyEntities.PaymentHistory
                           where b.Id > 0 && b.Status == status
                           select b;
            users = empQuery.ToList();
            return users;
        }

        public List<PaymentHistory> GetBySearch(string sSearchQuery)
        {
            List<PaymentHistory> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  PaymentHistory where IsActive = 1 and IsDelete = 0 ";

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

                var empQuery = objPropertyEntities.PaymentHistory.SqlQuery(sSQL).ToList<PaymentHistory>();
                contacts = empQuery.OrderByDescending(x => x.CreateDate).ToList();

            }
            catch (Exception ex)
            {

            }

            return contacts;
        }
        public List<PaymentHistory> GetUnApprovedPaymentHistory(string userId, string ledgerCode, string status)
        {
            var result = new List<PaymentHistory>();
            try
            {
                result = objPropertyEntities.PaymentHistory.OrderBy(x => x.Serial).ToList();

                if (userId != "" && userId != "-1")
                {
                    result = result.Where(x => (x.ToUser == userId || x.FromUser == userId)).OrderBy(x => x.Serial).ToList();
                }
                if (ledgerCode != "")
                {
                    result = result.Where(x => (x.LedgerCode == ledgerCode)).OrderBy(x => x.Serial).ToList();
                }
                if (status != "")
                {
                    result = result.Where(x => x.Status == status).OrderBy(x => x.Serial).ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return result;
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
