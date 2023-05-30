using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;
using PropertyService.ViewModel;

namespace PropertyService.DA.Account
{
    public class MonthlyBatchRentalInvoiceDA
    {
        private EPropertyEntities _objPropertyEntities;
        private PropertyEntities _propertyEntities;
        public MonthlyBatchRentalInvoiceDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _propertyEntities = PropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public MonthlyBatchRentalInvoiceDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public bool Insert(List<MonthlyBatchRentalInvoice> obj, BillPayment item)
        {
            try
            {
                var existBillPayment = _objPropertyEntities.BillPayment.Where(x => x.RefId == item.RefId && x.Month == item.Month && x.Year == item.Year && x.LedgerCode == item.LedgerCode)
                        .ToList();
                if (existBillPayment != null && existBillPayment.Count > 0)
                {
                    item.Id = existBillPayment[0].Id;
                    var exist = _objPropertyEntities.BillPayment.Find(existBillPayment[0].Id);
                    ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(exist);
                    _objPropertyEntities.Entry(item).State = EntityState.Modified;
                    _objPropertyEntities.SaveChanges();
                }
                else
                {
                    _objPropertyEntities.BillPayment.Add(item);
                    _objPropertyEntities.SaveChanges();
                }
                //_objPropertyEntities.SaveChanges();
                if (obj != null)
                {
                    _objPropertyEntities.MonthlyBatchRentalInvoice.AddRange(obj);
                    _objPropertyEntities.SaveChanges();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public MonthlyBatchRentalInvoice GetById(long Id)
        {
            var Data = _objPropertyEntities.MonthlyBatchRentalInvoice.Where(x => x.Id == Id).FirstOrDefault();
            return Data;
        }
        public bool DeleteById(long id)
        {
            try
            {
                _objPropertyEntities.MonthlyBatchRentalInvoice.Remove(GetById(id));
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<OwnerProfile> GetOwner()
        {
            var OwnerData = _propertyEntities.OwnerProfile.Where(x => x.IsDelete == false).ToList();
            return OwnerData;
        }
        public List<PropertyManagerProfile> GetPropertyManager(string owner)
        {
            var Data = _objPropertyEntities.PropertyManagerProfile.Where(x => x.IsDelete == false && x.OwnerId == owner).ToList();
            return Data;
        }

        public MonthlyBatchRentalInvoice GetMonthlyBatchRentalInvoiceByUnit(string ownerId, string unit, string year, string month)
        {
            var Data = _objPropertyEntities.MonthlyBatchRentalInvoice.Where(x =>x.OwnerId == ownerId && x.UnitId == unit && x.Year == year && x.Month == month).FirstOrDefault();
            return Data;
        }

        public MonthlyBatchRentalInvoice GetMonthlyBatchRentalInvoiceByInvoiceNo(string sInvoice, string sTenant)
        {
            var Data = _objPropertyEntities.MonthlyBatchRentalInvoice.Where(x => x.InvoiceNo == sInvoice && x.RefId == sTenant).FirstOrDefault();
            return Data;
        }

        public List<Location> GetLocation(string owner)
        {
            var Data = _objPropertyEntities.Location.Where(x => x.IsDelete == false && x.OwnerId == owner).ToList();
            return Data;
        }
        public List<MonthlyBatchRentalInvoice> GetMonthlyBatchRentalInvoiceByOwner(string sOwnerSerial, string sFrom, string sTo)
        {

            var result = new List<MonthlyBatchRentalInvoice>();
            try
            {
                string sSQL = string.Empty;

                sSQL = " select * from  MonthlyBatchRentalInvoice Amount > 0  ";

                try
                {
                    if (!string.IsNullOrEmpty(sFrom) && !string.IsNullOrEmpty(sTo))
                    {
                        sSQL += " and convert(varchar, CreateDate, 101) between '" + sFrom + "' and  '" + sTo + "'";
                    }
                    if (!string.IsNullOrEmpty(sOwnerSerial))
                    {
                        sSQL += " and OwnerId = '" + sOwnerSerial + "'";
                    }
                }
                catch (Exception ex)
                {

                }

                var empQuery = _objPropertyEntities.MonthlyBatchRentalInvoice.SqlQuery(sSQL).ToList<MonthlyBatchRentalInvoice>();
                result = empQuery.OrderBy(x => x.CreateDate).ToList();

            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public List<MonthlyBatchRentalInvoiceTenantMail> GetMonthlyBatchRentalInvoiceTenantMail(MonthlyBatchRentalInvoiceModel obj)
        {
            var list = new List<MonthlyBatchRentalInvoiceTenantMail>();

            //List<ResidentialUnit> lstUnit = _objPropertyEntities.ResidentialUnit.Where(x => x.OwnerId == obj.OwnerId &&
            //                                                    x.PropertyManagerSerialId == obj.PropertyManagerSerialId &&
            //                                                    x.LocationSerialId == obj.LocationSerialId).ToList();

            List<ResidentialUnit> listUnit = _objPropertyEntities.ResidentialUnit.Where(x => x.OwnerId == obj.OwnerId).ToList();

            if (string.IsNullOrEmpty(obj.PropertyManagerSerialId) && obj.PropertyManagerSerialId != null && obj.PropertyManagerSerialId != "-1")
            {
                listUnit = listUnit.Where(x => x.PropertyManagerSerialId == obj.PropertyManagerSerialId).ToList();
            }
            if (string.IsNullOrEmpty(obj.LocationSerialId) && obj.LocationSerialId != null && obj.LocationSerialId != "-1")
            {
                listUnit = listUnit.Where(x => x.LocationSerialId == obj.LocationSerialId).ToList();
            }

            foreach (var item in listUnit)
            {
                MonthlyBatchRentalInvoiceTenantMail objMail = new MonthlyBatchRentalInvoiceTenantMail();
                objMail.UnitSerialId = item.Serial;
                objMail.UnitName = item.UnitName;
                ResidentialTenantSignIn objTenantSignIn = _objPropertyEntities.ResidentialTenantSignIn
                    .Where(x => x.UnitId == item.Serial && (x.ApproveStatus == "Approved" || x.ApproveStatus == "Completed")).FirstOrDefault();

                if (objTenantSignIn != null)
                {
                    objMail.EmailId = objTenantSignIn.EmailId;
                    objMail.TenantName = objTenantSignIn.FirstName + " " + objTenantSignIn.LastName;
                    objMail.TenantSerialId = objTenantSignIn.SerialId;

                    //TenantRentalFee objRentalFee = _objPropertyEntities.TenantRentalFee
                    //    .Where(x => x.ApplicationId == objTenantSignIn.ApplicationCode)
                    //    .FirstOrDefault();

                    TenantRentalFee_Residential objRentalFee = _objPropertyEntities.TenantRentalFee_Residential
                       .Where(x => x.ApplicationId == objTenantSignIn.ApplicationCode)
                       .FirstOrDefault();

                    if (objRentalFee != null)
                    {
                        objMail.Amount = string.IsNullOrEmpty(objRentalFee.MonthlyRent.ToString()) ? "0" : Convert.ToDecimal(objRentalFee.MonthlyRent).ToString("#.00");
                    }
                }

                objMail.InvoiceID = new FinancialTransactionDA().MakeAutoGenSerial("M", "MonthlyBatchRentalInvoice");
                objMail.SendDate = DateTime.Today.ToString("MM/dd/yy");
                if(Convert.ToDecimal(objMail.Amount) > 0)
                {
                    list.Add(objMail);
                }
               
            }
            return list;
        }

        public decimal GetUnitPrice(string objOwnerId)
        {
            var amount = _objPropertyEntities.SystemInformation.Where(x => x.OwnerId == objOwnerId).FirstOrDefault()
                .UnitPrice;
            return Convert.ToDecimal(amount);
        }

    }
}
