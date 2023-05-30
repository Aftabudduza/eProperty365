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
    public class FinancialTransactionDA
    {
        EPropertyEntities objPropertyEntities = null;
        public FinancialTransactionDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public FinancialTransactionDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }       
        public FinancialTransaction GetBySerial(string serial)
        {
            FinancialTransaction objUser = null;
            try
            {
                var empQuery = from b in objPropertyEntities.FinancialTransaction
                               where b.Serial == serial
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objUser;
        }      

        public FinancialTransaction GetByID(int ID)
        {
            FinancialTransaction objUser = null;
            try
            {         
                var empQuery = from b in objPropertyEntities.FinancialTransaction
                               where b.Id == ID
                               select b;

                objUser = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }

            return objUser;
        }
    
        public bool Insert(FinancialTransaction objUser)
        {
            objPropertyEntities.FinancialTransaction.Add(objUser);
            objPropertyEntities.SaveChanges();

            return true;
        }

        public bool Update(FinancialTransaction obj)
        {
            try
            {
                FinancialTransaction existing = objPropertyEntities.FinancialTransaction.Find(obj.Id);
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
                objPropertyEntities.FinancialTransaction.Remove(this.GetByID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }

        public FinancialTransaction GetUserBySQL(string sql)
        {
            FinancialTransaction objUser = null;
            try
            {
               
                var empQuery = objPropertyEntities.FinancialTransaction.SqlQuery(sql).ToList<FinancialTransaction>().FirstOrDefault();
                objUser = empQuery;
                return objUser;
            }
            catch (Exception ex)
            {
                
            }

            return objUser;
        }   
       
        public List<FinancialTransaction> GetAllInfo()
        {
            List<FinancialTransaction> users = null;

            var empQuery = from b in objPropertyEntities.FinancialTransaction
                           where b.Id > 0  
                           select b;
            users = empQuery.ToList();
            return users;
        }

        public List<FinancialTransaction> GetByLedgerCode(string sOwnerSerial, string sLedgerCode, string sFrom, string sTo)
        {         

            var result = new List<FinancialTransaction>();
            try
            {
                result = objPropertyEntities.FinancialTransaction.Where(x=>x.RefId == sOwnerSerial || x.Remarks == "ApplicationFee").OrderBy(x => x.CreateDate).ToList();

                if (sLedgerCode != "" && sLedgerCode != "-1")
                {
                    result = result.Where(x => x.LedgerCode == sLedgerCode).OrderBy(x => x.CreateDate).ToList();
                }
                if (sFrom != "" && sTo != "")
                {
                    result = result.Where(x => x.CreateDate >= Convert.ToDateTime(sFrom) && x.CreateDate <= Convert.ToDateTime(sTo)).OrderBy(x => x.CreateDate).ToList();
                }
               
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public List<FinancialTransaction> GetBySearch(string sOwnerSerial, string sLedgerCode, string sFrom, string sTo)
        {
            List<FinancialTransaction> ObjFinTransactions = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  FinancialTransaction where Amount > 0 ";

                try
                {
                    if (!string.IsNullOrEmpty(sOwnerSerial))
                    {
                        sSQL += "and (RefId = '" + sOwnerSerial + "' or RefId in (select FromUser from TenantPaymentHistory where ToUser = '" + sOwnerSerial + "'))";
                    }
                    if (!string.IsNullOrEmpty(sLedgerCode))
                    {
                        sSQL += " and LedgerCode = '" + sLedgerCode + "'";
                    }
                    if (!string.IsNullOrEmpty(sFrom) && !string.IsNullOrEmpty(sTo))
                    {                        
                         sSQL += " and CONVERT(DATE, CreateDate) between '" + sFrom + "' and  '" + sTo + "'";
                        //sSQL += " and convert(varchar, CreateDate, 110) between '" + sFrom + "' and  '" + sTo + "'";
                    }
                }
                catch (Exception ex)
                {

                }

                var empQuery = objPropertyEntities.FinancialTransaction.SqlQuery(sSQL).ToList<FinancialTransaction>();
                ObjFinTransactions = empQuery.OrderBy(x => x.CreateDate).ToList();

            }
            catch (Exception ex)
            {

            }

            return ObjFinTransactions;
        }

        public List<FinancialTransaction> GetIncomeBySearch(string sOwnerSerial, string sFrom, string sTo)
        {
            List<FinancialTransaction> ObjFinTransactions = null;
            try
            {
                string sSQL = string.Empty;

                sSQL = " select * from  FinancialTransaction where Amount > 0 and (AccountType = 'Inc' or AccountType = 'Exp' or AccountType = 'COG') ";

                try
                {
                    if (!string.IsNullOrEmpty(sOwnerSerial))
                    {
                        sSQL += "and (RefId = '" + sOwnerSerial + "' or RefId in (select FromUser from TenantPaymentHistory where ToUser = '" + sOwnerSerial + "'))";
                       // sSQL += " and (RefId = '" + sOwnerSerial + "' or Remarks = 'ApplicationFee') ";
                    }
                    if (!string.IsNullOrEmpty(sFrom) && !string.IsNullOrEmpty(sTo))
                    {
                        sSQL += " and CONVERT(DATE, CreateDate) between '" + sFrom + "' and  '" + sTo + "'";

                    }
                }
                catch (Exception ex)
                {

                }

                var empQuery = objPropertyEntities.FinancialTransaction.SqlQuery(sSQL).ToList<FinancialTransaction>();
                ObjFinTransactions = empQuery.OrderBy(x => x.CreateDate).ToList();

            }
            catch (Exception ex)
            {

            }

            return ObjFinTransactions;
        }

        public List<FinancialTransaction> GetTransactionBySearch(string sOwnerSerial, string sFrom, string sTo)
        {
            List<FinancialTransaction> ObjFinTransactions = null;
            try
            {
                string sSQL = string.Empty;

                sSQL = " select * from  FinancialTransaction where Amount > 0  ";

                try
                {
                    if (!string.IsNullOrEmpty(sFrom) && !string.IsNullOrEmpty(sTo))
                    {
                        sSQL += " and CONVERT(DATE, CreateDate) between '" + sFrom + "' and  '" + sTo + "'";
                    }
                    if (!string.IsNullOrEmpty(sOwnerSerial))
                    {
                        sSQL += "and (RefId = '" + sOwnerSerial + "' or RefId in (select FromUser from TenantPaymentHistory where ToUser = '" + sOwnerSerial + "'))";
                        // sSQL += " and RefId = '" + sOwnerSerial + "'";
                    }
                }
                catch (Exception ex)
                {

                }

                var empQuery = objPropertyEntities.FinancialTransaction.SqlQuery(sSQL).ToList<FinancialTransaction>();
                ObjFinTransactions = empQuery.OrderBy(x => x.CreateDate).ToList();

            }
            catch (Exception ex)
            {

            }

            return ObjFinTransactions;
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
