using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;
using PropertyService.ViewModel;

namespace PropertyService.DA.Account
{
    public class RecordABillDA
    {
        private EPropertyEntities _objPropertyEntities;
        private PropertyEntities _propertyEntities;
        public RecordABillDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _propertyEntities = PropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public RecordABillDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public bool Insert(RecordABillModel obj)
        {
            try
            {
                _objPropertyEntities = new EPropertyEntities();
                RecordABill objMaster = new RecordABill();
                objMaster = obj.Master;
                objMaster.CreateDate = DateTime.Now;
                _objPropertyEntities.RecordABill.Add(objMaster);
                _objPropertyEntities.SaveChanges();
                int id = objMaster.Id;
                List<RecordABillDetails> details = new List<RecordABillDetails>();
                foreach (var dtl in obj.Details)
                {
                    RecordABillDetails obj2 = new RecordABillDetails()
                    {
                        Id = dtl.Id,
                        Amount = dtl.Amount,
                        RecordABillId = id,
                        CreditAccountName = dtl.CreditAccountName,
                        DebitAccountName = dtl.DebitAccountName,
                        Type = dtl.Type,
                        BillId = dtl.BillId,
                        CreditLedgerCode = dtl.CreditLedgerCode,
                        DebitLedgerCode = dtl.DebitLedgerCode,
                        Description = dtl.Description,
                        DueDate = dtl.DueDate
                    };
                    details.Add(obj2);
                }
                _objPropertyEntities.RecordABillDetails.AddRange(details);
                _objPropertyEntities.SaveChanges();
                //foreach (var dtl in obj.Details)
                //{
                //    RecordABillDetails objDtl = new RecordABillDetails();
                //    objDtl = dtl;
                //    objDtl.RecordABillId = id;
                //    _objPropertyEntities.RecordABillDetails.Add(objDtl);
                //    _objPropertyEntities.SaveChanges();
                //}
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(RecordABillModel obj)
        {
            try
            {
                _objPropertyEntities = new EPropertyEntities();
                RecordABill objMaster = new RecordABill();
                objMaster = obj.Master;

                var existing = _objPropertyEntities.RecordABill.Find(objMaster.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(objMaster).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();
                
                int id = objMaster.Id;
                foreach (var dtl in obj.Details)
                {
                    RecordABillDetails objDtl = new RecordABillDetails()
                    {
                        Id = dtl.Id,
                        Amount = dtl.Amount,
                        RecordABillId = id,
                        CreditAccountName = dtl.CreditAccountName,
                        DebitAccountName = dtl.DebitAccountName,
                        Type = dtl.Type,
                        BillId = dtl.BillId,
                        CreditLedgerCode = dtl.CreditLedgerCode,
                        DebitLedgerCode = dtl.DebitLedgerCode,
                        Description = dtl.Description,
                        DueDate = dtl.DueDate
                    };
                    objDtl.RecordABillId = id;
                    if (objDtl.Id > 0)
                    {
                        var existing2 = _objPropertyEntities.RecordABillDetails.Find(objDtl.Id);
                        ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing2);
                        _objPropertyEntities.Entry(objDtl).State = EntityState.Modified;
                        _objPropertyEntities.SaveChanges();
                    }
                    else
                    {

                        _objPropertyEntities.RecordABillDetails.Add(objDtl);
                        _objPropertyEntities.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string MakeAutoGenNumber(string yourPrefix, string objName)
        {
            try
            {
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
                var oupParam = new ObjectParameter("NewID", 0) { Value = DBNull.Value };
                _objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
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
        public List<AccountType> GetAccountType()
        {
            var AccountTypeData = _propertyEntities.AccountType.Where(x => x.isActive == true && (x.type == "Exp" || x.type == "Inc")).ToList();
            return AccountTypeData;
        }

        public List<AccountChart> GetAccountChart(string type)
        {
            var AccountNameData = _objPropertyEntities.AccountChart.Where(x => x.isActive == true && 
                                                                      ((x.accountTypeId == type || x.accountTypeId == "Asset" ||
                                                                                                x.accountTypeId == "Lib"))).ToList();
           
            return AccountNameData;
        }
        public List<RecordABill> GetBillNumber()
        {
            var BillNumberData = _objPropertyEntities.RecordABill.ToList();
            return BillNumberData;
        }

        public RecordABillModel GetRecordABillData(int billnumber)
        {
            RecordABillModel obj = new RecordABillModel();
            RecordABill objMaster = new RecordABill();
            objMaster = _objPropertyEntities.RecordABill.Where(x => x.Id == billnumber).FirstOrDefault();
            List<RecordABillDetails> objDetails = new List<RecordABillDetails>();
            objDetails = _objPropertyEntities.RecordABillDetails.Where(x => x.RecordABillId == objMaster.Id).ToList();
            obj.Master = objMaster;
            //obj.Details = objDetails;
            obj.Details = new List<RecordBillDetails>();
            foreach (var dtl in objDetails)
            {
                List<AccountChart> lstCharts = _objPropertyEntities.AccountChart.Where(x => x.isActive == true && (x.accountTypeId == dtl.Type)).ToList();
                RecordBillDetails obj2 = new RecordBillDetails()
                {
                    Id = dtl.Id,
                    Amount = dtl.Amount,
                    RecordABillId = dtl.RecordABillId,
                    CreditAccountName = dtl.CreditAccountName,
                    DebitAccountName = dtl.DebitAccountName,
                    LstCharts = lstCharts,
                    Type = dtl.Type,
                    BillId = dtl.BillId,
                    CreditLedgerCode = dtl.CreditLedgerCode,
                    DebitLedgerCode = dtl.DebitLedgerCode,
                    Description = dtl.Description,
                    DueDate = dtl.DueDate
                };
                obj.Details.Add(obj2);
            }
            return obj;
        }
        public bool Delete(int id)
        {
            try
            {
                RecordABillDetails existing = new RecordABillDetails();
                existing = _objPropertyEntities.RecordABillDetails.Find(id);
                _objPropertyEntities.RecordABillDetails.Remove(existing);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
