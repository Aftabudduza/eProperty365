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
    public class AdminFinancialTransactionDA
    {
        PropertyEntities objPropertyEntities = null;
        public AdminFinancialTransactionDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = PropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public AdminFinancialTransactionDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = PropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = PropertyEntity.GetEntity();
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
                           where b.Id > 0  && b.Serial != string.Empty
                           select b;
            users = empQuery.ToList();
            return users;
        }
        public List<FinancialTransaction> GetBySearch(string sSearchQuery)
        {
            List<FinancialTransaction> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  FinancialTransaction where IsActive = 1 and IsDelete = 0 ";

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

                var empQuery = objPropertyEntities.FinancialTransaction.SqlQuery(sSQL).ToList<FinancialTransaction>();
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
