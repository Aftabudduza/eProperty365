using System;
using System.Collections.Generic;
using System.Linq;
using PropertyService.BO;
using PropertyService.ViewModel;

namespace PropertyService.DA.Account
{
    public class MakeBillPaymentDA
    {
        private EPropertyEntities _objPropertyEntities;
        private PropertyEntities _propertyEntities;
        public MakeBillPaymentDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _propertyEntities = PropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public MakeBillPaymentDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public bool Insert(List<MakeBillPayment> obj)
        {
            try
            {
                _objPropertyEntities.MakeBillPayment.AddRange(obj);
                _objPropertyEntities.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<usp_GetRecordABillByDate_Result> GetRecord(string fromDate, string toDate)
        {
            var result = _objPropertyEntities.usp_GetRecordABillByDate(fromDate, toDate).ToList();
            return result;
        }

        public List<usp_GetRecordABillByDate_Result> GetRecordBySearch(string owner, string fromDate, string toDate)
        {
            var result = _objPropertyEntities.usp_GetRecordABillByDate(fromDate, toDate).ToList();
            return result;
        }

        public List<RecordABillDetails> GetRecordDetails(List<RecordABillDetails> details)
        {
            MakeBillPaymentModel recordBill = new MakeBillPaymentModel();
            foreach (var obj in details)
            {
                _objPropertyEntities = new EPropertyEntities();
                List<RecordABillDetails> objDtlList = new List<RecordABillDetails>();
                objDtlList = _objPropertyEntities.RecordABillDetails.Where(x => x.RecordABillId == obj.RecordABillId)
                    .ToList();
                foreach (var objDtl in objDtlList)
                {
                    objDtl.CreditAccountName = _objPropertyEntities.AccountChart
                        .Where(x => x.id.ToString() == objDtl.CreditAccountName).FirstOrDefault().accountName;

                    objDtl.DebitAccountName = _objPropertyEntities.AccountChart
                        .Where(x => x.id.ToString() == objDtl.DebitAccountName).FirstOrDefault().accountName;
                    
                    recordBill.lstDetails.Add(objDtl);
                }
            }
            return recordBill.lstDetails;
        }
    }
}
