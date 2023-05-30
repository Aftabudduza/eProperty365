using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;
using PropertyService.ViewModel;

namespace PropertyService.DA.Admin.ResidentialTenent
{
    public class TenantDashboardDA
    {
        readonly ADONetDataConnection _objDataAccessManager = new ADONetDataConnection();
        private EPropertyEntities _objPropertyEntities;
        private String error = String.Empty;
        private String Mass = String.Empty;
        private PropertyService.CommonDA _commonDa = new PropertyService.CommonDA();
        public TenantDashboardDA(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        public TenantDashboardDA(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }

        //public List<usp_GetTenantProfileInfo_Result>  GetTenantProfileList(int Id)
        //{
        //    var res = new List<usp_GetTenantProfileInfo_Result>();
        //    try
        //    {
        //         res = _objPropertyEntities.usp_GetTenantProfileInfo(Id).ToList();
        //    }
        //    catch (Exception)
        //    {

        //        
        //    }
        //    return res;
        //}
        public DataSet GetTenantProfileList(int Id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            return _objDataAccessManager.GetDataByDataSet("usp_GetTenantProfileInfo", sqlParameters, AllDBName.EPropertyDB_Owner1);
        }
        public bool Insert(Showing_Database objResidentialUnit)
        {
            try
            {
                _objPropertyEntities.Showing_Database.Add(objResidentialUnit);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Insert_Residential_Tenant_App_Step2_AgreementNameOf(Residential_Tenant_App_Step2_AgreementNameOf obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_App_Step2_AgreementNameOf.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update_aggrementNameOf_Save(Residential_Tenant_App_Step2_AgreementNameOf obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_App_Step2_AgreementNameOf.Find(obj.Id);
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
        public Residential_Tenant_App_Step2_AgreementNameOf GetResidential_Tenant_AgreementNameOf(string UnitId, string TenantId)
        {
            var obj = new Residential_Tenant_App_Step2_AgreementNameOf();
            try
            {
                obj = _objPropertyEntities.Residential_Tenant_App_Step2_AgreementNameOf.Where(x=>x.ResidentialUnitSerialId == UnitId && x.Serial == TenantId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return obj;
        }
        public bool Insert_Vehicle(List<Residential_Tenant_Add_Step2_Page2_Vehicles> obj)
        {
            try
            {
                foreach (var aObj in obj)
                {
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_Vehicles.Add(aObj);
                    _objPropertyEntities.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update_Vehicle(List<Residential_Tenant_Add_Step2_Page2_Vehicles> obj)
        {
            try
            {
                foreach (var aObj in obj)
                {
                    var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_Vehicles.Find(aObj.Id);
                    ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                    _objPropertyEntities.Entry(aObj).State = EntityState.Modified;
                    _objPropertyEntities.SaveChanges();
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Insert_People(List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit> obj)
        {
            try
            {
                foreach (var aObj in obj)
                {
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit.Add(aObj);
                    _objPropertyEntities.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update_People(List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit> obj)
        {
            try
            {
                foreach (var aObj in obj)
                {
                    var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit.Find(aObj.Id);
                    ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                    _objPropertyEntities.Entry(aObj).State = EntityState.Modified;
                    _objPropertyEntities.SaveChanges();
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public usp_GetOwnerPropertyManagerAndLocationInfo_UnitId_Result GetOwnerPropertyAndLocationInfo(string UnitId)
        {
            var obj = new usp_GetOwnerPropertyManagerAndLocationInfo_UnitId_Result();
            try
            {
                obj = _objPropertyEntities.usp_GetOwnerPropertyManagerAndLocationInfo_UnitId(UnitId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public bool InsertTenant_OnlineFee(TenantOnlineFee obj)
        {
            try
            {
                _objPropertyEntities.TenantOnlineFee.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Location> GetLocation(string ownerId)
        {
            var location = _objPropertyEntities.Location.Where(x => x.OwnerId == ownerId).ToList();
            return location;

        }
        public DataTable GetUnitId(int Id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            return _objDataAccessManager.GetDataByDataTable("usp_GetUnitId", sqlParameters, AllDBName.EPropertyDB_Owner1);
        }
        //public bool DeleteById(int id)
        //{
        //    try
        //    {
        //        _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_Signature.Remove(GetSignatureById(id));
        //        _objPropertyEntities.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public usp_GetSerialId_UnitId_Result GetSerialAndUnitId(int loginuserId)
        {
            var res = new usp_GetSerialId_UnitId_Result();
            res = _objPropertyEntities.usp_GetSerialId_UnitId(loginuserId).FirstOrDefault();
            return res;
        }
        public bool InsertComminicationPanelData(communicationPanel obj)
        {
            try
            {
                _objPropertyEntities.communicationPanel.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<usp_GetMessage_Result> GetMessageData(string fromuser, string toUser, string unidId, string MonthName, string RequestType)
        {
            var result = new List<usp_GetMessage_Result>();
            try
            {
                result = _objPropertyEntities.usp_GetMessage(fromuser, toUser, unidId, MonthName, RequestType).ToList();
            }
            catch (Exception ex)
            {


            }
            return result;
        }
        public List<TenantPaymentHistory> GetPaymentHistory(string TenantId)
        {
            List<TenantPaymentHistory> result = new List<TenantPaymentHistory>();
            try
            {
                result = _objPropertyEntities.TenantPaymentHistory.Where(x => x.FromUser == TenantId).ToList();
            }
            catch (Exception ex)
            {


            }
            return result;
        }

        public List<TenantPaymentHistory> GetUnApprovedPaymentHistory(string OwnerId, string UnitId, string status)
        {
            var result = new List<TenantPaymentHistory>();
            try
            {
                result = _objPropertyEntities.TenantPaymentHistory.OrderBy(x => x.Serial).ToList();

                if(OwnerId != "" && OwnerId != "-1")
                {
                    result = result.Where(x => (x.ToUser == OwnerId || x.FromUser == OwnerId)).OrderBy(x => x.Serial).ToList();
                }
                if (UnitId != "" && UnitId != "-1")
                {
                    result = result.Where(x => x.UnitNo == UnitId).OrderBy(x => x.Serial).ToList();
                }
                if (status != "")
                {
                    result = result.Where(x => x.Status == status).OrderBy(x => x.Serial).ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

    }

}
