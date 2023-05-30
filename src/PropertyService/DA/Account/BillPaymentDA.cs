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
    public class BillPaymentDA
    {
        EPropertyEntities objPropertyEntities = null;
        public BillPaymentDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public BillPaymentDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }       
        public BillPayment GetBySerial(string serial)
        {
            BillPayment objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.BillPayment
                               where b.RefId == serial
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }      

        public BillPayment GetByID(int ID)
        {
            BillPayment objUser = null;
            try
            {         
                var empQuery = from b in objPropertyEntities.BillPayment
                               where b.Id == ID
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }

            return objUser;
        }

        public BillPayment GetByInvoiceAndLegderCode(string sLedger, string sInvoice)
        {
            BillPayment objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.BillPayment
                               where b.InvoiceNo  == sInvoice && b.LedgerCode == sLedger
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }
        public bool Insert(BillPayment objUser)
        {
            try
            {
                objPropertyEntities.BillPayment.Add(objUser);
                objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
            
        }

        public bool Update(BillPayment obj)
        {
            try
            {
                BillPayment existing = objPropertyEntities.BillPayment.Find(obj.Id);
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
                objPropertyEntities.BillPayment.Remove(this.GetByID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }
        public bool DeleteByLedgerCode(string sLedger, string sInvoice)
        {
            try
            {
                objPropertyEntities.BillPayment.Remove(this.GetByInvoiceAndLegderCode(sLedger, sInvoice));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }
        public BillPayment GetUserBySQL(string sql)
        {
            BillPayment objUser = null;
            try
            {
               
                var empQuery = objPropertyEntities.BillPayment.SqlQuery(sql).ToList<BillPayment>().FirstOrDefault();
                objUser = empQuery;
                return objUser;
            }
            catch (Exception ex)
            {
                
            }

            return objUser;
        }   
       
        public List<BillPayment> GetAllInfo()
        {
            List<BillPayment> users = null;

            var empQuery = from b in objPropertyEntities.BillPayment
                           where b.Id > 0  && b.RefId != string.Empty
                           select b;
            users = empQuery.ToList();
            return users;
        }

        public List<BillPayment> GetByUserId(string sCode)
        {
            List<BillPayment> users = null;

            var empQuery = from b in objPropertyEntities.BillPayment
                           where b.Id > 0 && b.RefId == sCode
                           select b;
            users = empQuery.OrderBy(x=>x.CreateDate).ToList();
            return users;
        }

        public List<BillPayment> GetByLedgerCode(string sOwner, string sLedgerCode)
        {
            List<BillPayment> users = null;

            var empQuery = from b in objPropertyEntities.BillPayment
                           where b.Id > 0 && b.LedgerCode == sLedgerCode && b.OwnerId == sOwner
                           select b;
            users = empQuery.OrderBy(x => x.CreateDate).ToList();
            return users;
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
