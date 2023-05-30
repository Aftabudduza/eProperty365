using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyService.BO;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Web;
using PropertyService.ViewModel;
using PropertyService;

namespace PropertyService.DA.Admin.ResidentialTenent
{
    public class ResidentialAddResponceTemplateDa
    {
        readonly ADONetDataConnection _objDataAccessManager = new ADONetDataConnection();
       
        private EPropertyEntities _objPropertyEntities;
        private String error = String.Empty;
        private String Mass = String.Empty;
        private PropertyService.CommonDA _commonDa = new PropertyService.CommonDA();
        public ResidentialAddResponceTemplateDa(bool isLazyLoadingEnable = true)
        {
            _objPropertyEntities = EPropertyEntity.GetEntity();
            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public ResidentialAddResponceTemplateDa(bool isNewContext, bool isLazyLoadingEnable = true)
        {
            if (isNewContext)
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
            else
                _objPropertyEntities = EPropertyEntity.GetEntity();

            _objPropertyEntities.Configuration.LazyLoadingEnabled = isLazyLoadingEnable;
        }
        public Showing_Database GetbyId(int id)
        {
            Showing_Database objResidentialUnit = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.Showing_Database
                               where b.Id == id
                               select b;

                objResidentialUnit = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialUnit;
        }
        public ResidentialTenantSignIn GetTenantbyId(string serial)
        {
            ResidentialTenantSignIn objTenant = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialTenantSignIn
                               where b.SerialId == serial
                               select b;

                objTenant = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            return objTenant;
        }
        public ResidentialTenantSignIn GetTenantbyUnitAndEmail(string UnitId, string Email)
        {
            ResidentialTenantSignIn objTenant = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialTenantSignIn
                               where b.UnitId == UnitId && b.EmailId == Email
                               select b;

                objTenant = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            return objTenant;
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
        public Residential_Tenant_Application_Step1 GetTenantApp1ById(int Id)
        {
            var obj = new Residential_Tenant_Application_Step1();
            try
            {
                obj = _objPropertyEntities.Residential_Tenant_Application_Step1.FirstOrDefault(x => x.Id == Id);


            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        public Residential_Tenant_Application_Step1 GetTenantApp1ByUnitAndTenantId(string TenantId, string UnitId)
        {
            var obj = new Residential_Tenant_Application_Step1();
            try
            {
                obj = _objPropertyEntities.Residential_Tenant_Application_Step1.FirstOrDefault(x => x.Serial == TenantId && x.ResidentialUnitSerialId == UnitId);


            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        public bool Insert_Step1(Residential_Tenant_Application_Step1 objResidentialUnit)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Application_Step1.Add(objResidentialUnit);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update_step1(Residential_Tenant_Application_Step1 obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Application_Step1.Find(obj.Id);
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
        public bool SetUpStep(string TenantId, string UnitId, string ResientialType)
        {
            try
            {
                _objPropertyEntities.usp_SetStepValue(TenantId, UnitId, ResientialType);
                _objPropertyEntities.SaveChanges();
            }
            catch (Exception ex)
            {


                return false;
            }
            return true;
        }
        #region Residential Step 2 Page 1
        public Residential_Tenant_App_Step2_AgreementNameOf GetTenantAgrementNameOf(string id, string unitid)
        {
            Residential_Tenant_App_Step2_AgreementNameOf objResidentialTenantBasic = null;
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_App_Step2_AgreementNameOf.FirstOrDefault(x => x.Serial == id && x.ResidentialUnitSerialId == unitid);


                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }

        public Residential_Tenant_Add_Step2_ResidenceHistory GetTenantResidenceHistoryById(int id)
        {
            Residential_Tenant_Add_Step2_ResidenceHistory objResidentialTenantBasic = new Residential_Tenant_Add_Step2_ResidenceHistory();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_ResidenceHistory.FirstOrDefault(x => x.Id == id && x.DeletedBy == null);

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public bool UpdateResidentialTenantResidenceHistory(Residential_Tenant_Add_Step2_ResidenceHistory obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_ResidenceHistory.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertResidentialTenantResidenceHistory(Residential_Tenant_Add_Step2_ResidenceHistory obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_ResidenceHistory.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public List<Residential_Tenant_Add_Step2_ResidenceHistory> GetTenantResidentHistoryList(string id, string unitid)
        {
            List<Residential_Tenant_Add_Step2_ResidenceHistory> objResidentialTenantBasic = new List<Residential_Tenant_Add_Step2_ResidenceHistory>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_ResidenceHistory.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid && x.DeletedBy == null).ToList();

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public List<Residential_Tenant_App_Step2_EmployeeInfo> GetTenantResidentEmployeeInformationList(string id, string unitid)
        {
            List<Residential_Tenant_App_Step2_EmployeeInfo> objResidentialTenantBasic = new List<Residential_Tenant_App_Step2_EmployeeInfo>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_App_Step2_EmployeeInfo.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid && x.DeletedBy == null).ToList();

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public List<Residential_Tenant_App_Step2_Reference> GetTenantResidentReferenceList(string id, string unitid)
        {
            List<Residential_Tenant_App_Step2_Reference> objResidentialTenantBasic = new List<Residential_Tenant_App_Step2_Reference>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_App_Step2_Reference.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid && x.DeletedBy == null).ToList();

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }


        public List<RelationShip> GetAllRelationshipData()
        {
            var lstOfRelationShip = new List<RelationShip>();
            lstOfRelationShip = _objPropertyEntities.RelationShip.ToList();
            return lstOfRelationShip;
        }







        public Residential_Tenant_App_Step2_EmployeeInfo GetTenantResidenceEmployeeById(int id)
        {
            Residential_Tenant_App_Step2_EmployeeInfo objResidentialTenantBasic = new Residential_Tenant_App_Step2_EmployeeInfo();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_App_Step2_EmployeeInfo.FirstOrDefault(x => x.Id == id && x.DeletedBy == null);//

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public bool UpdateResidentialTenantEmployee(Residential_Tenant_App_Step2_EmployeeInfo obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_App_Step2_EmployeeInfo.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertResidentialEmployee(Residential_Tenant_App_Step2_EmployeeInfo obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_App_Step2_EmployeeInfo.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public List<Residential_Tenant_App_Step2_EmployeeInfo> GetTenantEmployeeList(string id, string unitid)
        {
            List<Residential_Tenant_App_Step2_EmployeeInfo> objResidentialTenantBasic = new List<Residential_Tenant_App_Step2_EmployeeInfo>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_App_Step2_EmployeeInfo.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid && x.DeletedBy == null).ToList();//&& x.DeletedBy == null

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }





        public Residential_Tenant_App_Step2_Reference GetTenantReferenceById(int id)
        {
            Residential_Tenant_App_Step2_Reference objResidentialTenantBasic = new Residential_Tenant_App_Step2_Reference();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_App_Step2_Reference.FirstOrDefault(x => x.Id == id && x.DeletedBy == null);//

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public bool UpdateResidentialTenantReference(Residential_Tenant_App_Step2_Reference obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_App_Step2_Reference.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertResidentialReference(Residential_Tenant_App_Step2_Reference obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_App_Step2_Reference.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public List<Residential_Tenant_App_Step2_Reference> GetTenantRefenceList(string id, string unitid)
        {
            List<Residential_Tenant_App_Step2_Reference> objResidentialTenantBasic = new List<Residential_Tenant_App_Step2_Reference>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_App_Step2_Reference.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid && x.DeletedBy == null).ToList();//

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }


        public Residential_Tenant_App_Step2_AgreementNameOf GetTenantBasicInfoById(int id)
        {
            Residential_Tenant_App_Step2_AgreementNameOf objResidentialTenantBasic = new Residential_Tenant_App_Step2_AgreementNameOf();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_App_Step2_AgreementNameOf.FirstOrDefault(x => x.Id == id);

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }

        public bool InsertResidentialBasic(Residential_Tenant_App_Step2_AgreementNameOf obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_App_Step2_AgreementNameOf.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool UpdateResidentialTenantBasicInfo(Residential_Tenant_App_Step2_AgreementNameOf obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_App_Step2_AgreementNameOf.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region Residential Step 2 Page 2
        public Residential_Tenant_Add_Step2_Page2_EmergencyContacts GetTenantResidenceEmergencyContactById(int id)
        {
            Residential_Tenant_Add_Step2_Page2_EmergencyContacts objResidentialTenantBasic = new Residential_Tenant_Add_Step2_Page2_EmergencyContacts();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_EmergencyContacts.FirstOrDefault(x => x.Id == id && x.DeletedBy == null);//

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public bool UpdateResidentialTenantEmergencyContact(Residential_Tenant_Add_Step2_Page2_EmergencyContacts obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_EmergencyContacts.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();


                return true;
            }
            // return true;
            // }


            // return true;
            //}
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertResidentialEmergencyContact(Residential_Tenant_Add_Step2_Page2_EmergencyContacts obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_EmergencyContacts.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public List<Residential_Tenant_Add_Step2_Page2_EmergencyContacts> GetTenantEmergencyContactList(string id, string unitid)
        {
            List<Residential_Tenant_Add_Step2_Page2_EmergencyContacts> objResidentialTenantBasic = new List<Residential_Tenant_Add_Step2_Page2_EmergencyContacts>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_EmergencyContacts.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid && x.DeletedBy == null).ToList();//&& x.DeletedBy == null

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }


        //-------------------------------- Vehicle Information---------------------
        public List<Residential_Tenant_Add_Step2_Page2_Vehicles> GetVehicleInformationList(string id, string unitid)
        {
            List<Residential_Tenant_Add_Step2_Page2_Vehicles> objResidentialTenantBasic = new List<Residential_Tenant_Add_Step2_Page2_Vehicles>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_Vehicles.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid).ToList();//&& x.DeletedBy == null

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }

        public Residential_Tenant_Add_Step2_Page2_Vehicles GetVehicleInfoById(int id)
        {
            Residential_Tenant_Add_Step2_Page2_Vehicles objResidentialTenantBasic = new Residential_Tenant_Add_Step2_Page2_Vehicles();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_Vehicles.FirstOrDefault(x => x.Id == id);//

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public bool UpdateVehicleInfo(Residential_Tenant_Add_Step2_Page2_Vehicles obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_Vehicles.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertVehicleInfo(Residential_Tenant_Add_Step2_Page2_Vehicles obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_Vehicles.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //-------------------------------- Credit Information---------------------
        public List<Residential_Tenant_Add_Step2_Page2_CreditHistory_New> GetCreditInformationList(string id, string unitid)
        {
            List<Residential_Tenant_Add_Step2_Page2_CreditHistory_New> objResidentialTenantBasic = new List<Residential_Tenant_Add_Step2_Page2_CreditHistory_New>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_CreditHistory_New.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid).ToList();//&& x.DeletedBy == null

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }

        public Residential_Tenant_Add_Step2_Page2_CreditHistory_New GetCreditInfoById(int id)
        {
            Residential_Tenant_Add_Step2_Page2_CreditHistory_New objResidentialTenantBasic = new Residential_Tenant_Add_Step2_Page2_CreditHistory_New();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_CreditHistory_New.FirstOrDefault(x => x.Id == id);//

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public bool UpdateCreditInfo(Residential_Tenant_Add_Step2_Page2_CreditHistory_New obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_CreditHistory_New.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertCreditInfo(Residential_Tenant_Add_Step2_Page2_CreditHistory_New obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_CreditHistory_New.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //-------------------------------- People Information---------------------
        public List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit> GetPeopleInformationList(string id, string unitid)
        {
            List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit> objResidentialTenantBasic = new List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid).ToList();//&& x.DeletedBy == null

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }

        public Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit GetPeopleInfoById(int id)
        {
            Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit objResidentialTenantBasic = new Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit.FirstOrDefault(x => x.Id == id);//

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public bool UpdateeopleInfo(Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertPeopleInfo(Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //--------------------------------- Credit Vehicle Unit ------------------//
        public string InsertCreditVehicleUnitData(TenantCommonModel baseobj, CreditInfoEmergencyVehiclePeopleObject Obj, string connectionstringName)
        {

            List<Residential_Tenant_Add_Step2_Page2_CreditHistory_New> credit = new List<Residential_Tenant_Add_Step2_Page2_CreditHistory_New>();
            List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit> people = new List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit>();
            List<Residential_Tenant_Add_Step2_Page2_Vehicles> vehicle = new List<Residential_Tenant_Add_Step2_Page2_Vehicles>();

            if (Obj.Credit != null)
            {
                credit.Add(Obj.Credit);
            }
            if (Obj.PeopleStayingUnit.Count > 0)
            {
                people = Obj.PeopleStayingUnit;
            }
            if (Obj.Vehicles.Count > 0)
            {
                vehicle = Obj.Vehicles;
            }

            DataTable newdt = new DataTable();
            DataTable dtPeople = people.ToDataTable<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit>();
            DataTable dtCredit = credit.ToDataTable<Residential_Tenant_Add_Step2_Page2_CreditHistory_New>();
            DataTable dtVehicle = vehicle.ToDataTable<Residential_Tenant_Add_Step2_Page2_Vehicles>();


            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@Credit", dtCredit));
            paramList.Add(new SqlParameter("@Vehicle", dtVehicle));
            paramList.Add(new SqlParameter("@People", dtPeople));
            string mass = "";

            mass = _commonDa.ExecProcedureWithTable("usp_SaveCredit_Vehicle_PeopleUnit", paramList, connectionstringName, out error, out Mass);
            return mass;

            //try
            //{
            //    //_objPropertyEntities.Residential_Tenant_Add_Step2_ResidenceHistory.Add(obj);
            //    //_objPropertyEntities.SaveChanges();
            //    return true;
            //}
            //catch (Exception ex)
            //{

            //    return false;
            //}
        }
        #endregion
        #region Reisdential Tenant Info Step 2 Page 3
        //-------------------------------- General Information---------------------
        public List<Residential_Tenant_Add_Step2_Page3_GeneralInformation> GetGeneralInformationList(string id, string unitid)
        {
            List<Residential_Tenant_Add_Step2_Page3_GeneralInformation> objResidentialTenantBasic = new List<Residential_Tenant_Add_Step2_Page3_GeneralInformation>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page3_GeneralInformation.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid).ToList();//&& x.DeletedBy == null

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }

        public Residential_Tenant_Add_Step2_Page3_GeneralInformation GetGeneralInfoById(int id)
        {
            Residential_Tenant_Add_Step2_Page3_GeneralInformation objResidentialTenantBasic = new Residential_Tenant_Add_Step2_Page3_GeneralInformation();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page3_GeneralInformation.FirstOrDefault(x => x.Id == id);//

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;
        }
        public bool UpdateGeneralInfo(Residential_Tenant_Add_Step2_Page3_GeneralInformation obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page3_GeneralInformation.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertGeneralInfo(Residential_Tenant_Add_Step2_Page3_GeneralInformation obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page3_GeneralInformation.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion
        #region Reisdential Tenant Info Step 2 Page 4
        public List<Residential_Tenant_Add_Step2_Page4_VerityIncome> GetVerityIncomeList(string id, string unitid)
        {
            List<Residential_Tenant_Add_Step2_Page4_VerityIncome> objResidentialTenantBasic = new List<Residential_Tenant_Add_Step2_Page4_VerityIncome>();
            try
            {
                var empQuery =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_VerityIncome.Where(x => x.Serial == id && x.ResidentialUnitSerialId == unitid).ToList();//&& x.DeletedBy == null

                objResidentialTenantBasic = empQuery;
            }
            catch (Exception)
            {
                // ignored
            }
            return objResidentialTenantBasic;



        }
        // -----------  Residential Document ------------------//
        public List<usp_GetTenantListOfDocumentByUnitId_new_Result> GetRentalDocList(string unitId, string tenantId)
        {
            var objTetantDocumentList = new List<usp_GetTenantListOfDocumentByUnitId_new_Result>();
            try
            {
                objTetantDocumentList = _objPropertyEntities.usp_GetTenantListOfDocumentByUnitId_new(unitId, tenantId).ToList();//&& x.DeletedBy == null
            }
            catch (Exception ex)
            {
                // ignored
            }
            return objTetantDocumentList;
        }

        public bool InsertOrUpdate(ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant obj)
        {

            var checkexits =
                _objPropertyEntities.ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant.FirstOrDefault(
                    x =>
                        x.ResidentTenantDocId == obj.ResidentTenantDocId && x.Serial == obj.Serial &&
                        x.ResidentialUnitSerialId == obj.ResidentialUnitSerialId);
            try
            {
                if (checkexits != null)
                {
                    UpdateResidentialDocInfo(obj);
                }
                else
                {
                    InsertResidentialDocInfo(obj);
                }
            }
            catch (Exception)
            {

                return false;
            }


            return true;
        }

        public bool UpdateResidentialDocInfo(ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertResidentialDocInfo(ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant obj)
        {
            try
            {
                _objPropertyEntities.ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        //----------------------------- verity Income ------------------------------//
        public bool InsertOrUpdateVerityOfIncome(Residential_Tenant_Add_Step2_Page4_VerityIncome obj)
        {

            var checkexits =
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_VerityIncome.FirstOrDefault(
                    x =>
                        x.FileName == obj.FileName && x.Serial == obj.Serial && x.FilePath == obj.FilePath &&
                        x.ResidentialUnitSerialId == obj.ResidentialUnitSerialId);
            try
            {
                if (checkexits != null)
                {
                    UpdateVerityOfIncome(obj);
                }
                else
                {
                    InsertVerityOfIncome(obj);
                }
            }
            catch (Exception)
            {

                return false;
            }


            return true;
        }

        public bool UpdateVerityOfIncome(Residential_Tenant_Add_Step2_Page4_VerityIncome obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_VerityIncome.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertVerityOfIncome(Residential_Tenant_Add_Step2_Page4_VerityIncome obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_VerityIncome.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        #endregion
        #region First Time Sign In Button Click
        public TenantSignInModel CheckTenantAlreadyExist(string unitId)
        {
            var resultobj = new ResidentialTenantSignIn();
            var tenantModel = new TenantSignInModel();
            var isFirstSignIn = false;
            var status = "";
            try
            {
                resultobj =
                    _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(
                        x => (x.ApproveStatus == "Review" || x.ApproveStatus == "Approved") && x.UnitId == unitId);
                if (resultobj != null)
                {
                    isFirstSignIn = true;
                    status = resultobj.ApproveStatus;
                    tenantModel.Email = resultobj.EmailId;
                    tenantModel.Status = resultobj.ApproveStatus != null ? resultobj.ApproveStatus : "";
                    tenantModel.Serial = resultobj.SerialId;
                    tenantModel.isFirstSignIn = isFirstSignIn;
                    tenantModel.ResidentialUnitSerialId = resultobj.UnitId;
                }
            }
            catch (Exception ex)
            {
                // tenantModel.isFirstSignIn = isFirstSignIn;
            }

            return tenantModel;
        }
        public TenantSignInModel CheckFirstTimeSignIn(ResidentialTenantSignIn obj, string unitId)
        {
            var resultobj = new ResidentialTenantSignIn();
            var tenantModel = new TenantSignInModel();
            var isFirstSignIn = true;
            var serialid = "";
            var applicationCode = "";
            var email = obj.EmailId;

            try
            {
                resultobj =
                    _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(
                        x =>
                            x.EmailId == obj.EmailId && x.UnitId == unitId);
                if (resultobj != null)
                {
                    isFirstSignIn = false;
                    tenantModel.Serial = resultobj.SerialId;
                    tenantModel.isFirstSignIn = isFirstSignIn;
                    tenantModel.ResidentialUnitSerialId = resultobj.UnitId;
                    tenantModel.Email = resultobj.EmailId;
                    //var stepcount = _objPropertyEntities.ResidentialAddTemplate_StepCount.FirstOrDefault();
                    var stepcount = new ResidentialAddResponceTemplateDa().GetStepCount(resultobj.SerialId, resultobj.UnitId);
                    if (stepcount == null)
                    {
                        new ResidentialAddResponceTemplateDa().SetUpStep(obj.SerialId, obj.UnitId, "Residential");
                    }
                    else if (stepcount.Step == 0 && stepcount.Sub_Step == 0)
                    {
                        new ResidentialAddResponceTemplateDa().SetUpStep(obj.SerialId, obj.UnitId, "Residential");
                    }
                }
                else
                {
                    serialid = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialTenantSerialFromThirdParty");
                    applicationCode = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialTenantApproveCode");
                    obj.IsActive = true;
                    obj.SerialId = serialid;
                    obj.CreateDate = DateTime.Now;
                    obj.ApplicationCode = applicationCode;
                    obj.UnitId = unitId;
                    if (InsertTenant(obj))
                    {
                        new ResidentialAddResponceTemplateDa().SetUpStep(obj.SerialId, obj.UnitId, "Residential");
                        tenantModel.Serial = serialid;
                        tenantModel.isFirstSignIn = isFirstSignIn;
                        tenantModel.ResidentialUnitSerialId = unitId;
                        tenantModel.Email = obj.EmailId;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tenantModel;
        }
        public TenantSignInModel CheckSecondTimeSignIn(ResidentialTenantSignIn obj, string unitId)
        {
            var resultobj = new ResidentialTenantSignIn();
            var tenantModel = new TenantSignInModel();
            var isSecondSignIn = true;
            var serialid = "";
            var ApproveCode = "";
            var email = "";

            try
            {
                resultobj =
                    _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(
                        x =>
                            x.EmailId == obj.EmailId && x.UnitId == unitId);
                if (resultobj != null)
                {

                    tenantModel.Serial = resultobj.SerialId;
                    tenantModel.isFirstSignIn = isSecondSignIn;
                    tenantModel.ResidentialUnitSerialId = resultobj.UnitId;
                    tenantModel.Email = resultobj.EmailId;
                    //var stepcount = _objPropertyEntities.ResidentialAddTemplate_StepCount.FirstOrDefault();
                    var stepcount = new ResidentialAddResponceTemplateDa().GetStepCount(resultobj.SerialId, resultobj.UnitId);
                    if (stepcount == null)
                    {
                        new ResidentialAddResponceTemplateDa().SetUpStep(obj.SerialId, obj.UnitId, "Residential");
                    }
                    else if (stepcount.Step == 0 && stepcount.Sub_Step == 0)
                    {
                        new ResidentialAddResponceTemplateDa().SetUpStep(obj.SerialId, obj.UnitId, "Residential");
                    }
                }
                else
                {
                    isSecondSignIn = false;
                    tenantModel.Serial = serialid;
                    tenantModel.isFirstSignIn = isSecondSignIn;
                    tenantModel.ResidentialUnitSerialId = unitId;
                    tenantModel.Email = email;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tenantModel;
        }
        public bool InsertTenant(ResidentialTenantSignIn obj)
        {
            try
            {
                _objPropertyEntities.ResidentialTenantSignIn.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTenant(ResidentialTenantSignIn obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialTenantSignIn.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

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

        public ResidentialUnitSpecs GetResidentialUnitSpecs(string UnitId)
        {
            var obj = new ResidentialUnitSpecs();
            try
            {
                obj = _objPropertyEntities.ResidentialUnitSpecs.FirstOrDefault(x => x.ResidentialUnitSerialId == UnitId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public ResidentialAddTemplate_StepCount GetStepCount(string tenantId, string UnitId)
        {
            var step = new ResidentialAddTemplate_StepCount();
            try
            {
                step = _objPropertyEntities.ResidentialAddTemplate_StepCount.FirstOrDefault(x => x.UnitId == UnitId && x.TenantId == tenantId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return step;
        }

        public SystemInformation GetApplicationFree(string UnitId)
        {
            var obj = new SystemInformation();
            try
            {
                string OwnerId =
                    _objPropertyEntities.ResidentialUnit.FirstOrDefault(x => x.Serial == UnitId).OwnerId.ToString();
                obj = _objPropertyEntities.SystemInformation.FirstOrDefault(x => x.OwnerId == OwnerId);


            }
            catch (Exception ex)
            {

            }
            return obj;
        }

        public List<Residential_Tenant_Add_Step2_Page4_Signature> GetAllSignature(string TenantId, string UnitId)
        {
            var obj = new List<Residential_Tenant_Add_Step2_Page4_Signature>();
            try
            {
                obj =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_Signature.Where(
                        x => x.Serial == TenantId && x.ResidentialUnitSerialId == UnitId).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }
        public Residential_Tenant_Add_Step2_Page4_Signature GetSignatureById(int Id)
        {
            var obj = new Residential_Tenant_Add_Step2_Page4_Signature();
            try
            {
                obj =
                    _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_Signature.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public bool UpdateSignature(Residential_Tenant_Add_Step2_Page4_Signature obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_Signature.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertSignature(Residential_Tenant_Add_Step2_Page4_Signature obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_Signature.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool DeleteById(int id)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step2_Page4_Signature.Remove(GetSignatureById(id));
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetTenantEmailAddress(string TenantId)
        {
            var Email = "";
            try
            {
                Email = _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(x => x.SerialId == TenantId).EmailId;
            }
            catch (Exception)
            {
                return Email;
            }
            return Email;
        }
        #endregion
        public List<usp_GetTenantApplication_Result> GetResidentTenantsBySearch(string ownerId, string propertyManagerId, string locationId, string unitId, string tenantId)
        {
            List<usp_GetTenantApplication_Result> listTenants = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.usp_GetTenantApplication(ownerId, propertyManagerId, locationId, unitId, tenantId)

                               select b;

                listTenants = empQuery.ToList();

            }
            catch (Exception ex)
            {

            }

            return listTenants;
        }

        public List<GetOwnerDashboardData_Result> GetOwnerDashboardData(string ownerId)
        {
            List<GetOwnerDashboardData_Result> listDatas = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.GetOwnerDashboardData(ownerId)

                               select b;

                listDatas = empQuery.ToList();

            }
            catch (Exception ex)
            {

            }

            return listDatas;
        }

        public TenantRentalFee_Residential GetByApplicationId(string serial)
        {
            TenantRentalFee_Residential objTenantFee = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.TenantRentalFee_Residential
                               where b.ApplicationId == serial
                               select b;

                objTenantFee = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            return objTenantFee;
        }
        public TenantRentalFee GetTenantRentalFeeById(string serial)
        {
            TenantRentalFee objTenantFee = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.TenantRentalFee
                               where b.ApplicationId == serial
                               select b;

                objTenantFee = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            return objTenantFee;
        }
        public bool InsertTenantRentalFee(TenantRentalFee obj)
        {
            try
            {
                _objPropertyEntities.TenantRentalFee.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateTenantRentalFee(TenantRentalFee obj)
        {
            try
            {
                var existing = _objPropertyEntities.TenantRentalFee.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Residential Step 4
        public List<usp_GetTenantListOfDocumentByUnitId_SignDeposit_New_Result> GetRentalDocList_Step4(string unitId, string tenantId)
        {
            var objTetantDocumentList = new List<usp_GetTenantListOfDocumentByUnitId_SignDeposit_New_Result>();
            try
            {
                objTetantDocumentList = _objPropertyEntities.usp_GetTenantListOfDocumentByUnitId_SignDeposit_New(unitId, tenantId).ToList();//&& x.DeletedBy == null
            }
            catch (Exception ex)
            {
                // ignored
            }
            return objTetantDocumentList;
        }
        //public List<usp_GetTenantListOfDocumentByUnitId_SignDeposit_Result> GetRentalDocList_Step4(string unitId, string tenantId)
        //{
        //    var objTetantDocumentList = new List<usp_GetTenantListOfDocumentByUnitId_SignDeposit_Result>();
        //    try
        //    {
        //        objTetantDocumentList = _objPropertyEntities.usp_GetTenantListOfDocumentByUnitId_SignDeposit(unitId, tenantId).ToList();//&& x.DeletedBy == null
        //    }
        //    catch (Exception ex)
        //    {
        //        // ignored
        //    }
        //    return objTetantDocumentList;
        //}
        public TenantRentalFee GetDepositeAmount(string unitId, string tenantId)
        {
            var objTenantDeposite = new TenantRentalFee();
            try
            {
                objTenantDeposite = _objPropertyEntities.TenantRentalFee.FirstOrDefault(x => x.ApplicationId == tenantId && x.UnitId == unitId);//&& x.DeletedBy == null
            }
            catch (Exception ex)
            {
                // ignored
            }
            return objTenantDeposite;
        }
        public bool InsertResidentialDocInfo(Residential_Tenant_Add_Step4_DocumentList obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step4_DocumentList.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool UpdateSignature_SignIn(Residential_Tenant_Add_Step4_Signature obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step4_Signature.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Residential_Tenant_Add_Step4_Owner_Signature> GetAllOwnerInfo(string TenantId, string UnitId)
        {
            var obj = new List<Residential_Tenant_Add_Step4_Owner_Signature>();
            try
            {
                obj =
                    _objPropertyEntities.Residential_Tenant_Add_Step4_Owner_Signature.Where(
                        x => x.Serial == TenantId && x.ResidentialUnitSerialId == UnitId).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }
        public bool UpdateSignature_Owner(Residential_Tenant_Add_Step4_Owner_Signature obj)
        {
            try
            {
                var existing = _objPropertyEntities.Residential_Tenant_Add_Step4_Owner_Signature.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertSignature_Owner(Residential_Tenant_Add_Step4_Owner_Signature obj)
        {
            try
            {
                _objPropertyEntities.Residential_Tenant_Add_Step4_Owner_Signature.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
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

        #endregion

        #region Login Check

        public ResidentialTenantSignIn CheckValideTenant(ResidentialTenantSignIn obj)
        {
            var returnTenant = new ResidentialTenantSignIn();
            returnTenant =
               _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(
                   x => x.ApprovalCode == obj.ApprovalCode && x.EmailId == obj.EmailId && x.IsActive == true && x.ApproveStatus == "Approved");
            return returnTenant;
        }

        public bool UpdateTenantPassword(ResidentialTenantSignIn obj)
        {
            try
            {
                var existing = _objPropertyEntities.ResidentialTenantSignIn.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateTenantRentalFee_Residential(TenantRentalFee_Residential obj)
        {
            try
            {
                var existing = _objPropertyEntities.TenantRentalFee_Residential.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertTenant_Resident(TenantRentalFee_Residential obj)
        {
            try
            {
                _objPropertyEntities.TenantRentalFee_Residential.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public ResidentialUnit GetResidentialUnit(string unitId)
        {
            var unitInfo = new ResidentialUnit();
            try
            {
                unitInfo = _objPropertyEntities.ResidentialUnit.FirstOrDefault(x => x.Serial == unitId);
            }
            catch (Exception)
            {


            }
            return unitInfo;
        }
        public ResidentialTenantSignIn GetApprovalCode(string unitId, string TenantId, string pass)
        {
            var appCode = new ResidentialTenantSignIn();
            try
            {
                appCode = _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(x => x.UnitId == unitId && x.SerialId == TenantId && x.Password == pass);
            }
            catch (Exception)
            {


            }
            return appCode;
        }
        public ResidentialTenantSignIn GetApprovalCode_Step1(string unitId, string TenantId)
        {
            var appCode = new ResidentialTenantSignIn();
            try
            {
                appCode = _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(x => x.UnitId == unitId && x.SerialId == TenantId);
            }
            catch (Exception)
            {


            }
            return appCode;
        }
        public bool UpdateTenantApproveStatus(string UnitId, string TenantId)
        {
            try
            {
                var obj =
                    _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(
                        x => x.SerialId == TenantId && x.UnitId == UnitId);
                obj.ApproveStatus = "Review";
                obj.UpdatedDate = DateTime.Now;
                var existing = _objPropertyEntities.ResidentialTenantSignIn.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateTenantStatus(string UnitId, string TenantId, string Status)
        {
            try
            {
                var obj =
                    _objPropertyEntities.ResidentialTenantSignIn.FirstOrDefault(
                        x => x.SerialId == TenantId && x.UnitId == UnitId);
                obj.ApproveStatus = Status;
                obj.UpdatedDate = DateTime.Now;
                var existing = _objPropertyEntities.ResidentialTenantSignIn.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public TenantPaymentHistory GetTenantPaymentHistory(string unitId, string TenantId, string sPaymentFor)
        {
            var objTenantPaymentHistory = new TenantPaymentHistory();
            try
            {
                objTenantPaymentHistory = _objPropertyEntities.TenantPaymentHistory.FirstOrDefault(x => x.UnitNo == unitId && x.FromUser == TenantId && x.Getway == sPaymentFor);
            }
            catch (Exception)
            {
            }
            return objTenantPaymentHistory;
        }
        public TenantPaymentHistory GetTenantPaymentHistoryById(int Id)
        {
            var objTenantPaymentHistory = new TenantPaymentHistory();
            try
            {
                objTenantPaymentHistory = _objPropertyEntities.TenantPaymentHistory.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {


            }
            return objTenantPaymentHistory;
        }
        public TenantPaymentHistory GetTenantPaymentHistoryBySerial(string serial)
        {
            var objTenantPaymentHistory = new TenantPaymentHistory();
            try
            {
                objTenantPaymentHistory = _objPropertyEntities.TenantPaymentHistory.FirstOrDefault(x => x.Serial == serial);
            }
            catch (Exception)
            {


            }
            return objTenantPaymentHistory;
        }
        public List<TenantPaymentHistory> GetTenantPaymentHistoryByStatus(string status)
        {
            List<TenantPaymentHistory> objPayments = new List<TenantPaymentHistory>();
            try
            {
                objPayments = _objPropertyEntities.TenantPaymentHistory.Where(x => x.Status == status).ToList();
                return objPayments;
            }
            catch (Exception)
            {
                return objPayments;
            }
        }
        public bool UpdateTenantPaymentHistory(string unitId, string TenantId, string sPaymentFor)
        {
            try
            {
                var obj = _objPropertyEntities.TenantPaymentHistory.FirstOrDefault(x => x.UnitNo == unitId && x.FromUser == TenantId && x.Getway == sPaymentFor);

                var existing = _objPropertyEntities.TenantPaymentHistory.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public GetSalesPartnerCommissionRateByUnitId_Result GetSalesPartnerCommissionRateByUnitId(string unitId)
        {
            var objSalesPartnerCommission = new GetSalesPartnerCommissionRateByUnitId_Result();
            try
            {
                objSalesPartnerCommission = _objPropertyEntities.GetSalesPartnerCommissionRateByUnitId(unitId).FirstOrDefault();//&& x.DeletedBy == null
            }
            catch (Exception ex)
            {
                // ignored
            }
            return objSalesPartnerCommission;
        }
        public GetDealerCommissionRateByUnitId_Result GetDealerCommissionRateByUnitId(string unitId)
        {
            var objDealerCommission = new GetDealerCommissionRateByUnitId_Result();
            try
            {
                objDealerCommission = _objPropertyEntities.GetDealerCommissionRateByUnitId(unitId).FirstOrDefault();//&& x.DeletedBy == null
            }
            catch (Exception ex)
            {
                // ignored
            }
            return objDealerCommission;
        }
        public string MakeAutoGenSerialForPaymentHistory(string yourPrefix, string objName)
        {
            string serial = "";
            try
            {
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
                string prefix = "";
                //  prefix = DateTime.Now.Year.ToString().Substring(2, 2); 
                ObjectParameter oupParam = new ObjectParameter("NewID", 0);
                oupParam.Value = DBNull.Value;
                _objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                string sNumber = oupParam.Value.ToString().PadLeft(7, '0');
                serial = string.Concat(yourPrefix + prefix, sNumber);

                return serial;
            }
            catch (Exception ex)
            {

            }


            return null;

        }
        public bool UpdateTenantPaymentHistory(TenantPaymentHistory obj)
        {
            try
            {
                var existing = _objPropertyEntities.TenantPaymentHistory.Find(obj.Id);
                ((IObjectContextAdapter)_objPropertyEntities).ObjectContext.Detach(existing);
                _objPropertyEntities.Entry(obj).State = EntityState.Modified;
                _objPropertyEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int Insert(TenantPaymentHistory objSave)
        {
            try
            {
                _objPropertyEntities.TenantPaymentHistory.Add(objSave);
                _objPropertyEntities.SaveChanges();
                //int id = objSave.Id;
                return objSave.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool DeletePaymentHistoryById(int id)
        {
            try
            {
                _objPropertyEntities.TenantPaymentHistory.Remove(GetTenantPaymentHistoryById(id));
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Get_TenantInformation_Result> GetTenantInformation(string obj)
        {
            try
            {
                var tenantInformation = _objPropertyEntities.Get_TenantInformation(obj).ToList();
                // _objPropertyEntities.SaveChanges();
                //int id = objSave.Id;
                return tenantInformation;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string MakeAutoGenSerial(string yourPrefix, string objName)
        {
            string serial = "";
            try
            {
                _objPropertyEntities = EPropertyEntity.GetFreshEntity();
                string prefix = "";
                // prefix = DateTime.Now.Year.ToString();
                ObjectParameter oupParam = new ObjectParameter("NewID", 0);
                oupParam.Value = DBNull.Value;
                _objPropertyEntities.SP_GetID(objName, "Id", DateTime.Now.Year, null, null, oupParam);
                string sNumber = oupParam.Value.ToString().PadLeft(11, '0');
                serial = string.Concat(yourPrefix + prefix, sNumber);

                return serial;
            }
            catch (Exception ex)
            {

            }


            return null;

        }


        public DataSet uspGetTenantList(string ResidentialId, string TenantId)
        {
            DataSet ds = new DataSet();
            //usp_GetAllTenantDataByTenanrId_ResidentialId_Result obj = new usp_GetAllTenantDataByTenanrId_ResidentialId_Result();
            //try
            //{
            //    _objPropertyEntities.usp_GetAllTenantDataByTenanrId_ResidentialId(ResidentialId, TenantId);
            //    _objPropertyEntities.SaveChanges();
            //}
            //catch (Exception ex)
            //{


            //    return obj;
            //}
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            try
            {
                sqlParameters.Add(new SqlParameter("@ResidentialId", ResidentialId));
                sqlParameters.Add(new SqlParameter("@TenantId", TenantId));
                ds = _objDataAccessManager.GetDataByDataSet("usp_GetAllTenantDataByTenanrId_ResidentialId", sqlParameters, AllDBName.EPropertyDB_Owner1);
            }
            catch (Exception ex)
            {

                // throw;
            }

            return ds;
            // return obj;
        }

        public bool SaveXMLStatus(XMLOperationStatus obj)
        {
            try
            {
                _objPropertyEntities.XMLOperationStatus.Add(obj);
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public XMLOperationStatus GetXMLOperationStatus(string TenantSerial)
        {
            var obj = new XMLOperationStatus();
            try
            {
                obj =  _objPropertyEntities.XMLOperationStatus.FirstOrDefault(x => x.TenantId == TenantSerial);
            }
            catch (Exception ex)
            {

            }

            return obj;
        }

        public bool DeleteXMLOperationStatus(string TenantSerial)
        {
            try
            {
                _objPropertyEntities.XMLOperationStatus.Remove(GetXMLOperationStatus(TenantSerial));
                _objPropertyEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        public int GetOrderId(string TenantId, string UnitId)
        {
            var OrderId = 0;
            try
            {
                if (!string.IsNullOrEmpty(TenantId) && !string.IsNullOrEmpty(UnitId))
                {
                    OrderId = Convert.ToInt32(
                        _objPropertyEntities.XMLOperationStatus.Where(x => x.TenantId == TenantId && x.UnitId == UnitId && x.Status == "pending")
                            .FirstOrDefault()
                            .OrderId);
                }
            }
            catch(Exception ex)
            {

            }
           
            return OrderId;
        }

        public XMLOperationStatus GetTenantBackgroundStatus(string TenantId, string UnitId)
        {
            XMLOperationStatus objXMLOperationStatus = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.XMLOperationStatus
                               where b.TenantId == TenantId && b.UnitId == UnitId
                               select b;

                objXMLOperationStatus = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }

            return objXMLOperationStatus;
        }

        public ResidentialTenantSignIn GetResidentialTenantInfoByEmail(string Email)
        {
            ResidentialTenantSignIn objResidentialTenantSignIn = null;
            try
            {
                var empQuery = from b in _objPropertyEntities.ResidentialTenantSignIn
                               where b.EmailId == Email
                               select b;

                objResidentialTenantSignIn = empQuery.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }

            return objResidentialTenantSignIn;
        }
    }


}
