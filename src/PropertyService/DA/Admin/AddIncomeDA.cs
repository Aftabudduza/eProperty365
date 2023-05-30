using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;

namespace PropertyService.DA.Admin.ResidentialTenent
{
   public class AddIncomeDA
    {
        private EPropertyEntities _objPropertyEntities;
        private String error = String.Empty;
        private String Mass = String.Empty;
        //private ADONetDataConnection _objAdoNetDataConnection;
        private PropertyService.CommonDA _commonDa = new PropertyService.CommonDA();

        public AddIncomeDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public AddIncomeDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }


        public bool Insert(AddIncome obj)
        {
            try
            {
                _objPropertyEntities.AddIncome.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Showing_Database obj)
        {
            try
            {
                var existing = _objPropertyEntities.Showing_Database.Find(obj.Id);
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
        public Residential_Tenant_Add_Step4_Owner_Signature GetSignatureById_Owner(int Id)
        {
            var obj = new Residential_Tenant_Add_Step4_Owner_Signature();
            try
            {
                obj =
                    _objPropertyEntities.Residential_Tenant_Add_Step4_Owner_Signature.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }
        public bool DeleteById_Owner(int id)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step4_Owner_Signature.Remove(GetSignatureById_Owner(id));
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataSet GetInitialData(string ownerId)
        {
            DataSet ds =  null;
            ADONetDataConnection _objAdoNetDataConnection = new ADONetDataConnection();
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@OwnerId", ownerId));
                ds = _objAdoNetDataConnection.GetDataByDataSet("Get_AddIncomeInitial_Data", paramList, AllDBName.EPropertyDB_Owner1);
                
                return ds;
            }
            catch (Exception exception)
            {
                return ds;
            }
        }
    }
}
