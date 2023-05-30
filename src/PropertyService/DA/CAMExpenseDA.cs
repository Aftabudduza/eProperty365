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
    public class CAMExpenseDA
    {
        EPropertyEntities objPropertyEntities = null;
        public CAMExpenseDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public CAMExpenseDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public CAMExpense GetbyID(int id)
        {
            CAMExpense objContactInformation = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.CAMExpense
                               where b.Id == id
                               select b;

                objContactInformation = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objContactInformation;
        }      
        public bool Insert(CAMExpense objContactInformation)
        {
            try
            {                
                objPropertyEntities.CAMExpense.Add(objContactInformation);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }      
        public bool Update(CAMExpense obj)
        {
            try
            {
                CAMExpense existing = objPropertyEntities.CAMExpense.Find(obj.Id);
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
                objPropertyEntities.CAMExpense.Remove(this.GetbyID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }       
        public List<CAMExpense> GetAllContactInformation()
        {
            List<CAMExpense> listContactInformation = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.CAMExpense
                               where b.Id > 0
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.Name).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listContactInformation;
        }
        public List<CAMExpense> GetByOwner(string serial)
        {
            List<CAMExpense> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.CAMExpense
                               where b.OwnerId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.Name).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
        public List<CAMExpense> GetByPropertyLocation(string serial)
        {
            List<CAMExpense> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.CAMExpense
                               where b.PropertyLocationId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.Name).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
        public List<CAMExpense> GetBySearch(DateTime from, DateTime to)
        {
            List<CAMExpense> contacts = null;
            try
            {
                string sSQL = string.Empty;
                sSQL = " select * from  CAMExpense where Id <> -1 ";

                try
                {
                    if (from != DateTime.MinValue)
                    {
                        sSQL += " and (PaidDate between '" + from + "' and '" + to + "')";

                    }


                }
                catch (Exception ex)
                {

                }

                var empQuery = objPropertyEntities.CAMExpense.SqlQuery(sSQL).ToList<CAMExpense>();
                contacts = empQuery.OrderBy(x => x.Name).ToList();

            }
            catch (Exception ex)
            {

            }

            return contacts;
        }
    }
}
