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

namespace eProperty.Pages.Resident
{
    public partial class TenantProfile_DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["TenentId"] = null;
                //Session["ResidentialUnitSerial"] = null;
                if (Session["UserObject"] != null)
                {

                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
        }
        [WebMethod]
        public static string GetProfileInfoList()
        {
            var Profile = new VmForTenantProfile();
            if (HttpContext.Current.Session["UserObject"] != null)
            {
               
                var ress = new TenantDashboardDA().GetTenantProfileList(Convert.ToInt32(((UserProfile)HttpContext.Current.Session["UserObject"]).Id));

                // aggrementNameOf obj= (((DataTable)(ress.Tables[0])).Rows[0]).GetItem<aggrementNameOf>();//.DataTableToList<aggrementNameOf>();
                if(ress != null)
                {
                    if(ress.Tables[0] != null && ress.Tables[0].Rows.Count > 0)
                    {
                        aggrementNameOf item = GetItemSingleObject<aggrementNameOf>((((DataTable)(ress.Tables[0])).Rows[0]));
                        Profile.aggrementNameOf = item;
                    }
                    if (ress.Tables[1] != null && ress.Tables[1].Rows.Count > 0)
                    {
                        Profile.Emergency = ress.Tables[1].DataTableToList<Emergency>();
                    }
                    if (ress.Tables[2] != null && ress.Tables[2].Rows.Count > 0)
                    {
                        Profile.Vehicle = ress.Tables[2].DataTableToList<Vehicle>();
                    }
                    if (ress.Tables[3] != null && ress.Tables[3].Rows.Count > 0)
                    {
                        Profile.People = ress.Tables[3].DataTableToList<People>();
                    }
                   
                   
                   
                    //HttpContext.Current.Session["TenentId"] = item.SerialId;
                    //HttpContext.Current.Session["ResidentialUnitSerial"] = item.UnitId;
                }

            }
            else
            {
                HttpContext.Current.Response.Redirect("../Login.aspx");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(Profile);
            return json;
        }
        [WebMethod]
        public static string SaveEmergencyContactInformation(Residential_Tenant_Add_Step2_Page2_EmergencyContacts Obj)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null && HttpContext.Current.Session["TenentId"] == null)
                return "";

            var insertedOrUpdatedRecord = new List<Residential_Tenant_Add_Step2_Page2_EmergencyContacts>();
            if (HttpContext.Current.Session["UserObject"] != null)
            {
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
            else
            {
                HttpContext.Current.Response.Redirect("../Login.aspx");
            }
            
            
            }
            //  var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;

        }
        [WebMethod]
        public static string SaveTetantCreditVehicleAndUnitInformation(TenantSaveProfile Obj)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null && HttpContext.Current.Session["TenentId"] == null)
                return "";
            var myCon = ConfigurationSettings.AppSettings["ConnectionStringNew"].ToString();
            bool nextStep = true;
            var aggrementNameOf_Save = new Residential_Tenant_App_Step2_AgreementNameOf();
            Residential_Tenant_Add_Step2_Page2_CreditHistory_New credit = new Residential_Tenant_Add_Step2_Page2_CreditHistory_New();
            var Vehicles = new List<Residential_Tenant_Add_Step2_Page2_Vehicles>();
            var PeopleStayingUnit = new List<Residential_Tenant_Add_Step2_Page2_PeopleStayingUnit>();
            CreditInfoEmergencyVehiclePeopleObject ObjCreditVehicle = new CreditInfoEmergencyVehiclePeopleObject();

            var BaseObject = new TenantCommonModel();
            BaseObject.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
            BaseObject.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString();

            aggrementNameOf_Save = Obj.aggrementNameOf;
            aggrementNameOf_Save.Serial = HttpContext.Current.Session["TenentId"].ToString();
            aggrementNameOf_Save.ResidentialUnitSerialId =HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            Vehicles = Obj.Vehicles;
            Vehicles.ForEach(x=>x.Serial = HttpContext.Current.Session["TenentId"].ToString());
            Vehicles.ForEach(x => x.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            PeopleStayingUnit = Obj.PeopleStayingUnit;
            PeopleStayingUnit.ForEach(x=>x.Serial= HttpContext.Current.Session["TenentId"].ToString());
            PeopleStayingUnit.ForEach(x => x.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString());

            ObjCreditVehicle.Credit = credit;
            ObjCreditVehicle.Vehicles = Vehicles;
            ObjCreditVehicle.PeopleStayingUnit = PeopleStayingUnit;

            if(aggrementNameOf_Save.Id > 0)
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

                    //if (new TenantDashboardDA(false).Update_Vehicle(Vehicles))
                    //{
                    //    if (new TenantDashboardDA(false).Update_People(PeopleStayingUnit))
                    //    {
                    //        nextStep = true;
                    //    }else
                    //    {
                    //        nextStep = false;
                    //    }
                    //}
                    //else
                    //{
                    //    nextStep = false;
                    //}
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
            

            //var BaseObject = new TenantCommonModel();
            //BaseObject.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
            //BaseObject.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString();
            //Obj.Credit.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
            //Obj.Credit.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString();
            //Obj.PeopleStayingUnit.ForEach(x => x.Serial = (HttpContext.Current.Session["TenentId"]).ToString());
            //Obj.PeopleStayingUnit.ForEach(x => x.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());


            //Obj.Vehicles.ForEach(x => x.Serial = (HttpContext.Current.Session["TenentId"]).ToString());
            //Obj.Vehicles.ForEach(x => x.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            //var result = new ResidentialAddResponceTemplateDa().InsertCreditVehicleUnitData(BaseObject, Obj, myCon);
           

            // var TenantBasicData = new ResidentialAddResponceTemplateDa().GetCreditInformationList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            //if (result == "true")
            //{
            //    nextStep = new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");
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
    }
}