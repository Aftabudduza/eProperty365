using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;
using PropertyService.Admin.DA;
using PropertyService.Enums;

namespace eProperty.Pages.Resident
{
    public partial class AddNewTenantProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["TenentId"] = null;
                Session["ResidentialUnitSerial"] = null;
                string residentialId, tenantId;
                try
                {
                    residentialId = Request.QueryString["ResidentialUnitSerial"];
                }
                catch (Exception ex)
                {
                    residentialId = "";
                }
                try
                {
                    tenantId = Request.QueryString["TenentId"];
                }
                catch (Exception ex)
                {
                    tenantId = "";
                }
                if (residentialId != "" && tenantId != "")
                {
                    Session["TenentId"] = tenantId;
                    Session["ResidentialUnitSerial"] = residentialId;
                }

            }
        }
        [WebMethod]
        public static string GetProfileInfoList()
        {
          
            TenantImportModel objTenantModel = new TenantImportModel();

            if (HttpContext.Current.Session["TenentId"] != null)
            {
                var objTenant = new ResidentialAddResponceTemplateDa().GetTenantbyId(HttpContext.Current.Session["TenentId"].ToString());

                if (objTenant != null)
                {
                    objTenantModel = new TenantImportModel();
                    objTenantModel.SerialId = objTenant.SerialId;
                    objTenantModel.FirstName = objTenant.FirstName;
                    objTenantModel.LastName = objTenant.LastName;
                    objTenantModel.EmailAddress = objTenant.EmailId;
                    objTenantModel.UnitId = objTenant.UnitId;
                   

                    var objResidentialMasterTable = new ResidentialAddResponceTemplateDa().GetOwnerPropertyAndLocationInfo(objTenant.UnitId);
                    if (objResidentialMasterTable != null)
                    {
                        objTenantModel.LocationId = objResidentialMasterTable.LocationSerialId;
                    }

                    TenantRentalFee objTenantFee = new ResidentialAddResponceTemplateDa().GetTenantRentalFeeById(objTenant.SerialId);
                    if (objTenantFee != null && objTenantFee.Id > 0)
                    {
                        objTenantModel.SecurityDeposit = objTenantFee.SecurityDeposit != null ? Convert.ToDecimal(objTenantFee.SecurityDeposit).ToString("#.00") : "";
                        objTenantModel.MonthlyRentHeld = objTenantFee.MonthlyRent != null ? Convert.ToDecimal(objTenantFee.MonthlyRent).ToString("#.00") : ""; 
                        objTenantModel.OtherAmountHeld = objTenantFee.FirstMonthRent != null ? Convert.ToDecimal(objTenantFee.FirstMonthRent).ToString("#.00") : ""; 
                        objTenantModel.LeaseSignDate = objTenantFee.LeaseSignedDate != null ? Convert.ToDateTime(objTenantFee.LeaseSignedDate).ToString("MM-dd-yyyy") : "";
                        objTenantModel.MonthlyPayDueDate = objTenantFee.MonthlyPaymentDueDate != null ? objTenantFee.MonthlyPaymentDueDate : "";

                    }

                }

            }
          

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(objTenantModel);
            return json;
        }
        [WebMethod]
        public static string SaveEmergencyContactInformation(Residential_Tenant_Add_Step2_Page2_EmergencyContacts Obj)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null && HttpContext.Current.Session["TenentId"] == null)
                return "";

            var insertedOrUpdatedRecord = new List<Residential_Tenant_Add_Step2_Page2_EmergencyContacts>();
            if (Obj != null)
            {
                if (Obj.Id > 0)
                {
                    var NewRentalTenantResidenceHistory = new ResidentialAddResponceTemplateDa().GetTenantResidenceEmergencyContactById(Obj.Id);

                    Obj.Serial = NewRentalTenantResidenceHistory.Serial;
                    Obj.ResidentialUnitSerialId = NewRentalTenantResidenceHistory.ResidentialUnitSerialId;
                    //obj.IsViewedOrDownloaded = NewRentalDocumentObject.IsViewedOrDownloaded;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantEmergencyContact(Obj))
                    {
                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantEmergencyContactList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

                    }
                }
                else
                {
                    Obj.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
                    Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

                    if (new ResidentialAddResponceTemplateDa().InsertResidentialEmergencyContact(Obj))
                    {
                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantEmergencyContactList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
            }

            //  var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;

        }
        [WebMethod]
        public static string SaveTetantCreditVehicleAndUnitInformation(TenantSaveProfileNew Obj)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null && HttpContext.Current.Session["TenentId"] == null)
                return "";
            bool nextStep = true;
            var myCon = ConfigurationSettings.AppSettings["ConnectionStringNew"].ToString();
            CreditInfoEmergencyVehiclePeopleObject ObjCreditVehicle = new CreditInfoEmergencyVehiclePeopleObject();
            Residential_Tenant_Add_Step2_Page2_CreditHistory_New credit = new Residential_Tenant_Add_Step2_Page2_CreditHistory_New();

            var BaseObject = new TenantCommonModel();
            BaseObject.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
            BaseObject.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString();

            var aggrementNameOf_Save = new Residential_Tenant_App_Step2_AgreementNameOf();
            var Vehicles = new List<Residential_Tenant_Add_Step2_Page2_Vehicles>();
            var PeopleStayingUnit = new List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit>();
            aggrementNameOf_Save = Obj.aggrementNameOf;
            aggrementNameOf_Save.Serial = HttpContext.Current.Session["TenentId"].ToString();
            aggrementNameOf_Save.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            Vehicles = Obj.Vehicles;
            Vehicles.ForEach(x => x.Serial = HttpContext.Current.Session["TenentId"].ToString());
            Vehicles.ForEach(x => x.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            PeopleStayingUnit = Obj.PeopleStayingUnit;
            PeopleStayingUnit.ForEach(x => x.Serial = HttpContext.Current.Session["TenentId"].ToString());
            PeopleStayingUnit.ForEach(x => x.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString());

            ObjCreditVehicle.Credit = credit;
            ObjCreditVehicle.Vehicles = Vehicles;
            ObjCreditVehicle.PeopleStayingUnit = PeopleStayingUnit;

            var exist_aggrementNameOf = new TenantDashboardDA().GetResidential_Tenant_AgreementNameOf(aggrementNameOf_Save.ResidentialUnitSerialId, aggrementNameOf_Save.Serial);
            if(exist_aggrementNameOf != null)
            {
                aggrementNameOf_Save.Id = exist_aggrementNameOf.Id;
            }

            ResidentialTenantSignIn objResidentialTenant = new ResidentialAddResponceTemplateDa().GetTenantbyId(HttpContext.Current.Session["TenentId"].ToString());
            if (objResidentialTenant != null)
            {
                Random generator = new Random();
                int r = generator.Next(1, 1000000);
                string sApproveCode = r.ToString().PadLeft(6, '0');

                objResidentialTenant.UpdatedDate = DateTime.Now;
                objResidentialTenant.EmailId = objResidentialTenant.EmailId;
                objResidentialTenant.Password = Obj.Password;
                objResidentialTenant.ApprovalCode = sApproveCode;
                objResidentialTenant.ApproveStatus = "Approved";
                objResidentialTenant.ApplicationCode = objResidentialTenant.SerialId;

                if (new ResidentialAddResponceTemplateDa().UpdateTenant(objResidentialTenant))
                {
                    try
                    {
                        UserProfile objTenantUser = new UserProfile();

                        UserProfile objOldTenantUser = new AdminUserProfileDA().GetUserByEmail(objResidentialTenant.EmailId);

                        ResidentialUnit objUnit = new ResidentialUnitDa().GetbySerial(objResidentialTenant.UnitId);

                        objTenantUser.Email = objResidentialTenant.EmailId;
                        objTenantUser.Password = Utility.base64Encode(objResidentialTenant.Password);
                        objTenantUser.IsAdmin = false;
                        objTenantUser.UserType = Convert.ToInt32(EnumUserType.Resident).ToString();
                        string tempEmail = objTenantUser.Email;
                        char ch = '@';
                        int idx = tempEmail.IndexOf(ch);
                        string username = tempEmail.Substring(0, idx);

                        objTenantUser.Username = username;
                        objTenantUser.Title = objTenantUser.Username;
                        objTenantUser.Phone = "";
                        objTenantUser.LocationId = objUnit != null ? objUnit.LocationSerialId : "";
                        objTenantUser.Location = "";
                        objTenantUser.DatabaseName = "";
                        objTenantUser.DatabaseLocation = "";
                        objTenantUser.CanLogin = true;
                        objTenantUser.IsDeleted = false;
                        objTenantUser.SecurityLevel = "2 and Up";
                        objTenantUser.IsActive = true;
                        objTenantUser.OwnerId = objUnit != null ? objUnit.OwnerId : "";
                        objTenantUser.Remarks = "";
                        objTenantUser.HasCompletedFullProfile = false;
                        objTenantUser.HasContactProfile = false;
                        objTenantUser.HasLedgerCode = false;
                        objTenantUser.HasOwnerProfile = false;
                        objTenantUser.HasPropertyLocation = false;
                        objTenantUser.HasPropertyManagerProfile = false;
                        objTenantUser.HasPropertyUnit = false;
                        objTenantUser.HasSystemInfo = false;
                        objTenantUser.HasUserProfile = true;
                        objTenantUser.HasVendorProfile = false;
                        objTenantUser.HasAccountSystem = false;
                        objTenantUser.HasDocuments = false;
                        objTenantUser.HasFinishedTenantImport = false;
                        objTenantUser.CreatedDate = DateTime.Now;

                        PropertyEntities mEntity = new PropertyEntities();
                        if (objOldTenantUser != null)
                        {
                            objTenantUser.Id = objOldTenantUser.Id;
                            if (new AdminUserProfileDA().Update(objTenantUser))
                            {
                                HttpContext.Current.Session["UserObject"] = objTenantUser;
                                HttpContext.Current.Session["UserType"] = objTenantUser.UserType;
                                HttpContext.Current.Session["Username"] = objTenantUser.Username;
                                HttpContext.Current.Session["bIsLogin"] = true;
                                HttpContext.Current.Session["OwnerId"] = objTenantUser.OwnerId;
                                HttpContext.Current.Session["UserId"] = objTenantUser.Id;

                                Utility.LoginUser = objTenantUser.Username;
                            
                                UserProfile objOldTenantUser1 = new UserProfileDA().GetUserByEmail(objResidentialTenant.EmailId);
                                if (objOldTenantUser1 != null)
                                {
                                    objTenantUser.Id = objOldTenantUser1.Id;
                                    if (new UserProfileDA().Update(objTenantUser))
                                    {

                                    }
                                }
                                else
                                {
                                    if (new UserProfileDA(true, false).Insert(objTenantUser))
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            if (new AdminUserProfileDA(true, false).Insert(objTenantUser))
                            {
                                HttpContext.Current.Session["UserObject"] = objTenantUser;
                                HttpContext.Current.Session["UserType"] = objTenantUser.UserType;
                                HttpContext.Current.Session["Username"] = objTenantUser.Username;
                                HttpContext.Current.Session["bIsLogin"] = true;
                                HttpContext.Current.Session["OwnerId"] = objTenantUser.OwnerId;
                                HttpContext.Current.Session["UserId"] = objTenantUser.Id;

                                Utility.LoginUser = objTenantUser.Username;
                             

                                UserProfile objOldTenant1 = new UserProfileDA().GetUserByEmail(objResidentialTenant.EmailId);
                                if (objOldTenant1 != null)
                                {
                                    objTenantUser.Id = objOldTenant1.Id;
                                    if (new UserProfileDA().Update(objTenantUser))
                                    {

                                    }
                                }
                                else
                                {
                                    if (new UserProfileDA(true, false).Insert(objTenantUser))
                                    {

                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }
             }

            if (aggrementNameOf_Save.Id > 0)
            {
                //-------- Update aggrementnameof --------//
                if (new TenantDashboardDA().Update_aggrementNameOf_Save(aggrementNameOf_Save))
                {
                    var result = new ResidentialAddResponceTemplateDa().InsertCreditVehicleUnitData(BaseObject, ObjCreditVehicle, myCon);
                    if (result == "true")
                    {
                        nextStep = true;
                    }
                    else
                    {
                        nextStep = false;
                    }
                    
                }
                else
                {
                    nextStep = false;
                }
            }
            else
            {
                //-------- Update aggrementnameof --------//
                if (new TenantDashboardDA().Insert_Residential_Tenant_App_Step2_AgreementNameOf(aggrementNameOf_Save))
                {
                    var result = new ResidentialAddResponceTemplateDa().InsertCreditVehicleUnitData(BaseObject, ObjCreditVehicle, myCon);
                    if (result == "true")
                    {
                        nextStep = true;
                    }
                    else
                    {
                        nextStep = false;
                    }

                }
                else
                {
                    nextStep = false;
                }
            }


            //-------- Update aggrementnameof --------//

            //if (new TenantDashboardDA().Insert_Residential_Tenant_App_Step2_AgreementNameOf(aggrementNameOf_Save))
            //{
            //    if (new TenantDashboardDA(false).Insert_Vehicle(Vehicles))
            //    {
            //        if (new TenantDashboardDA(false).Insert_People(PeopleStayingUnit))
            //        {
            //            nextStep = true;
            //        }
            //    }

            //    nextStep = true;
            //}
            //else
            //{
            //    nextStep = false;
            //}


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(nextStep);
            return json;
        }
        public static T GetItemSingleObject<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                    {
                        if (pro.PropertyType == typeof(DateTime?))
                        {
                            pro.SetValue(obj, Convert.ToDateTime(dr[column.ColumnName]), null);
                        }
                        else
                        {
                            if (pro.PropertyType == typeof(Int64))
                            {
                                pro.SetValue(obj, Convert.ToInt64(dr[column.ColumnName]), null);
                            }
                            else
                            {
                                if (pro.PropertyType == typeof(Boolean))
                                {
                                    pro.SetValue(obj, Convert.ToBoolean(dr[column.ColumnName]), null);
                                }
                                else
                                {
                                    pro.SetValue(obj, dr[column.ColumnName], null);
                                }

                            }

                        }
                    }


                    else
                    {
                        continue;
                    }

                }
            }
            return obj;
        }

        [WebMethod]
        public static string ExistTenant()
        {
            var resultobj = new TenantSignInModel();
            try
            {
                if (HttpContext.Current.Session["ResidentialUnitSerial"] != null)
                {
                    resultobj = new ResidentialAddResponceTemplateDa().CheckTenantAlreadyExist(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
                    //CheckFirstTimeSignIn
                }
            }
            catch (Exception ex)
            {

            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(resultobj);
            return json;
        }

    }
}