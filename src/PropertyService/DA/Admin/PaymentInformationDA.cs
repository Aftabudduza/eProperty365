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
    public class AdminPaymentInformationDA
    {
        PropertyEntities objPropertyEntities = null;
        public AdminPaymentInformationDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = PropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public AdminPaymentInformationDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = PropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = PropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }             

        public PaymentInformation GetByOwner(string ownerId)
        {
            PaymentInformation obj = null;
            try
            {
                var empQuery = from b in objPropertyEntities.PaymentInformation
                               where b.OwnerId == ownerId
                               select b;

                obj = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return obj;
        }

        public PaymentInformation GetByID(int nID)
        {
            PaymentInformation obj = null;
            try
            {         
                var empQuery = from b in objPropertyEntities.PaymentInformation
                               where b.Id == nID
                               select b;

                obj = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }

            return obj;
        }
    
        public bool Insert(PaymentInformation obj)
        {
            objPropertyEntities.PaymentInformation.Add(obj);
            objPropertyEntities.SaveChanges();

            return true;
        }

        public bool Update(PaymentInformation obj)
        {
            try
            {
                PaymentInformation existing = objPropertyEntities.PaymentInformation.Find(obj.Id);
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
                objPropertyEntities.PaymentInformation.Remove(this.GetByID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }
        public bool DeleteByOwnerCardAndCheckingAccount(string ownerId, string card, string account)
        {
            try
            {
                objPropertyEntities.PaymentInformation.Remove(this.GetByOwnerCardAndCheckingAccount(ownerId, card, account));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }
        public PaymentInformation GetByOwnerCardAndCheckingAccount(string ownerId, string card, string account)
        {
            PaymentInformation obj = null;
            try
            {
                var empQuery = from b in objPropertyEntities.PaymentInformation
                               where b.OwnerId == ownerId && b.CardNumber == card && b.AccountNo == account
                               select b;

                obj = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return obj;
        }
        public PaymentInformation GetBySQL(string sql)
        {
            PaymentInformation obj = null;
            try
            {
               
                var empQuery = objPropertyEntities.PaymentInformation.SqlQuery(sql).ToList<PaymentInformation>().FirstOrDefault();
                obj = empQuery;
                return obj;
            }
            catch (Exception ex)
            {
                
            }

            return obj;
        }
        public List<PaymentInformation> GetByOwnerId(string ownerId)
        {
            List<PaymentInformation> listPaymentInformations = null;

            var empQuery = from b in objPropertyEntities.PaymentInformation
                           where b.OwnerId == ownerId
                           select b;
            listPaymentInformations = empQuery.ToList();
            return listPaymentInformations;
        }
        public List<PaymentInformation> GetByUsername(string username)
        {
            List<PaymentInformation> listPaymentInformations = null;

            var empQuery = from b in objPropertyEntities.PaymentInformation
                           where b.Username == username
                           select b;
            listPaymentInformations = empQuery.ToList();
            return listPaymentInformations;
        }
        public List<PaymentInformation> GetAllInfo()
        {
            List<PaymentInformation> listPaymentInformations = null;

            var empQuery = from b in objPropertyEntities.PaymentInformation
                           where b.Id > 0
                           select b;
            listPaymentInformations = empQuery.ToList();
            return listPaymentInformations;
        }       
        public string MakeAutoGenSerial(string yourPrefix, string objName)
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
                string sNumber = oupParam.Value.ToString().PadLeft(9, '0');
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
