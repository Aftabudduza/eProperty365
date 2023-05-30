using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;
using System.Data.Entity.Core.Objects;

namespace PropertyService.DA
{
    public class RecordInvoiceDA
    {
        EPropertyEntities objPropertyEntities = null;
        public RecordInvoiceDA(bool isLazyLoadingEnable = true)
        {
            objPropertyEntities = EPropertyEntity.GetEntity();
            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public RecordInvoiceDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
            {
                objPropertyEntities = EPropertyEntity.GetEntity();
            }

            objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public RecordInvoice GetbyID(int id)
        {
            RecordInvoice objRecordInvoice = null;
            try
            {             
                var empQuery = from b in objPropertyEntities.RecordInvoice
                               where b.Id == id
                               select b;

                objRecordInvoice = empQuery.ToList().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
               
            }
            return objRecordInvoice;
        }
        public RecordInvoice GetBySerial(string serial)
        {
            RecordInvoice objRecordInvoice = null;
            try
            {
                var empQuery = from b in objPropertyEntities.RecordInvoice
                               where b.BillNumber == serial
                               select b;

                objRecordInvoice = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }

            return objRecordInvoice;
        }
        public bool Insert(RecordInvoice objRecordInvoice)
        {
            try
            {                
                objPropertyEntities.RecordInvoice.Add(objRecordInvoice);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }      
        public bool Update(RecordInvoice obj)
        {
            try
            {
                RecordInvoice existing = objPropertyEntities.RecordInvoice.Find(obj.Id);
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
                objPropertyEntities.RecordInvoice.Remove(this.GetbyID(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteBySerial(string serial)
        {
            try
            {
                objPropertyEntities.RecordInvoice.Remove(this.GetBySerial(serial));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<RecordInvoice> GetAllInformation()
        {
            List<RecordInvoice> listContactInformation = null;
            try
            {               
                var empQuery = from b in objPropertyEntities.RecordInvoice
                               where b.Id > 0
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.BillNumber).ToList();
               
            }
            catch (Exception ex)
            {
               
            }
            return listContactInformation;
        }
        public List<RecordInvoice> GetByOwner(string serial)
        {
            List<RecordInvoice> listContactInformation = null;
            try
            {
                var empQuery = from b in objPropertyEntities.RecordInvoice
                               where b.OwnerId == serial
                               select b;

                listContactInformation = empQuery.OrderBy(x => x.BillNumber).ToList();

            }
            catch (Exception ex)
            {

            }
            return listContactInformation;
        }
       
        public List<RecordInvoice> GetBySearch(string owner, string manager, string location)
        {
            List<RecordInvoice> listRecordInvoice = null;
            try
            {
                var empQuery = from b in objPropertyEntities.RecordInvoice
                               where b.OwnerId == owner 
                               select b;

                listRecordInvoice = empQuery.OrderBy(x => x.BillNumber).ToList();

                if (manager != string.Empty)
                {
                    listRecordInvoice = listRecordInvoice.Where(x=> x.BillTo == manager).ToList();
                }

               

               
               
            }
            catch (Exception ex)
            {

            }
            return listRecordInvoice;
        }
        public string MakeAutoGenLocation(string yourPrefix, string objName)
        {
            try
            {               
                objPropertyEntities = EPropertyEntity.GetFreshEntity();
                var oupParam = new ObjectParameter("NewID", 0) { Value = DBNull.Value };
                objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                var sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                var serial = string.Concat(yourPrefix, sNumber);

                return serial;
            }
            catch (Exception ex)
            {
                // ignored
            }

            return null;
        }

        #region Part

        public RecordInvoiceDetails GetRecordInvoicePartById(int id)
        {
            RecordInvoiceDetails objRecordInvoiceDetails = null;
            try
            {
                var empQuery = from b in objPropertyEntities.RecordInvoiceDetails
                               where b.Id == id
                               select b;

                objRecordInvoiceDetails = empQuery.ToList().FirstOrDefault();


            }
            catch (Exception ex)
            {

            }
            return objRecordInvoiceDetails;
        }
        public bool InsertPart(RecordInvoiceDetails objRecordInvoiceDetails)
        {
            try
            {
                objPropertyEntities.RecordInvoiceDetails.Add(objRecordInvoiceDetails);
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdatePart(RecordInvoiceDetails obj)
        {
            try
            {
                RecordInvoiceDetails existing = objPropertyEntities.RecordInvoiceDetails.Find(obj.Id);
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
        public bool DeletePartByID(int id)
        {
            try
            {
                objPropertyEntities.RecordInvoiceDetails.Remove(this.GetRecordInvoicePartById(id));
                objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }       
        public List<RecordInvoiceDetails> GetInvoiceDetailRecordByInvoiceNumber(string serial)
        {
            List<RecordInvoiceDetails> listRecordInvoiceDetails = null;
            try
            {
                var empQuery = from b in objPropertyEntities.RecordInvoiceDetails
                               where b.BillNumber == serial
                               select b;

                listRecordInvoiceDetails = empQuery.OrderBy(x => x.Id).ToList();

            }
            catch (Exception ex)
            {

            }
            return listRecordInvoiceDetails;
        }

        public decimal GetTotalAmountByInvoiceNumber(string serial)
        {
            decimal nTotal = 0;
            List<RecordInvoiceDetails> listRecordInvoiceDetails = null;

            try
            {
                var empQuery = from b in objPropertyEntities.RecordInvoiceDetails
                               where b.BillNumber == serial
                               select b;

                listRecordInvoiceDetails = empQuery.OrderBy(x => x.Id).ToList();

                if(listRecordInvoiceDetails != null && listRecordInvoiceDetails.Count > 0)
                {
                    foreach(RecordInvoiceDetails detail in listRecordInvoiceDetails)
                    {
                        if(detail != null && detail.Amount != null)
                        {
                            nTotal += Convert.ToDecimal(detail.Amount);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                nTotal = 0;
            }

            return nTotal;
        }

        #endregion

    }
}
