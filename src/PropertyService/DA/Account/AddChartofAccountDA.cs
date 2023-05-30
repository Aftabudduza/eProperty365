using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using PropertyService.BO;
using PropertyService.ViewModel;

namespace PropertyService.DA.Account
{
    public class AddChartofAccountDA
    {
        private EPropertyEntities _objPropertyEntities;
        private PropertyEntities _propertyEntities;
        public AddChartofAccountDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _propertyEntities = PropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public AddChartofAccountDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public bool Insert(AccountChart obj)
        {
            try
            {
                _objPropertyEntities.AccountChart.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(AccountChart obj)
        {
            try
            {
                var existing = _objPropertyEntities.AccountChart.Find(obj.id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<AccountType> GetAccountType()
        {
            var AccountType = _propertyEntities.AccountType.Where(x => x.isActive == true).ToList();
            return AccountType;
        }
        public AccountChart GetAccountTypeByLedgerCode(string sCode)
        {
            var AccountChart = _objPropertyEntities.AccountChart.Where(x => x.accountCode == sCode).FirstOrDefault();
            return AccountChart;
        }

        public AccountType GetAccountTypeByCode(string sCode)
        {
            var AccountType = _objPropertyEntities.AccountType.Where(x => x.type == sCode).FirstOrDefault();
            return AccountType;
        }

        public List<AccountModel> GetAccountChart(string OwnerId)
        {
            List<AccountModel> listObj = new List<AccountModel>();
            List<AccountChart> lstAccountCharts = _objPropertyEntities.AccountChart.Where(x => (x.OwnerId == "Admin" || x.OwnerId == OwnerId)).OrderBy(x=> x.accountCode).ToList();
            List<AccountType> lstAccountTypes = _propertyEntities.AccountType.Where(x => x.isActive == true).ToList();
            listObj = (from objAccountChart in lstAccountCharts
                       join objAccountTypes in lstAccountTypes on objAccountChart.accountTypeId equals objAccountTypes.type.ToString()
                       select new AccountModel()
                       {
                           Id = objAccountChart.id,
                           AccountCode = objAccountChart.accountCode,
                           AccountName = objAccountChart.accountName,
                           AccountType = objAccountTypes.type,
                           AccountTypeId = objAccountTypes.id,
                           AccountDescription = objAccountTypes.description,
                           IsActive = Convert.ToBoolean(objAccountChart.isActive),
                           EditAble = Convert.ToBoolean(objAccountChart.editAble),
                           CreateDate = Convert.ToDateTime(objAccountChart.CreateDate).ToString("dd/MMM/yyyy")
                       }).OrderBy(x => x.AccountCode).ToList();
            return listObj;
        }
    }

   
}
