using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;

namespace PropertyService.DA.Report
{
    public class ReportDA
    {
        private PropertyEntities _objPropertyEntities;
        private EPropertyEntities _objEPropertyEntities;
        private DataTable _dt ;
        readonly ADONetDataConnection _objDataAccessManager = new ADONetDataConnection();

        public ReportDA(bool isLazyLoadingEnable = true)
        {
            _objEPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities = PropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
            _objEPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public ReportDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
            {
                _objPropertyEntities = PropertyEntity.GetFreshEntity();
                _objEPropertyEntities = EPropertyEntity.GetFreshEntity();
            }
                
            else
            {
                _objPropertyEntities = PropertyEntity.GetEntity();
                _objEPropertyEntities = EPropertyEntity.GetEntity();
            }
               

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
            _objEPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public DataTable GetAllSalesPartnerDealer()
        {
            _objPropertyEntities = new PropertyEntities();
            List<Dealer_SalesPartner> listDealerSalesPartner = _objPropertyEntities.Dealer_SalesPartner.ToList();
            _dt = ToDataTable(listDealerSalesPartner);
            return _dt;
        }
        public DataSet GetBalanceSheet(string date, string ownerId)
        {
            _objEPropertyEntities = new EPropertyEntities();
            List<SqlParameter> _lstparam = new List<SqlParameter>();
            _lstparam.Add(new SqlParameter("@createdate", date));
            _lstparam.Add(new SqlParameter("@OwnerId", ownerId));
            DataSet ds = _objDataAccessManager.GetDataByDataSet("usp_GetBalanceSheetData", _lstparam,
                AllDBName.EPropertyDB_Owner1);
           // List<usp_GetBalanceSheetData_Result> listDealerSalesPartner = _objEPropertyEntities.usp_GetBalanceSheetData(Convert.ToDateTime(date)).ToList();
            //_dt = ToDataTable(listDealerSalesPartner);
            return ds;
        }
        private DataTable ToDataTable<T>(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);

                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
