using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialTenantRental_App_Page_2 : System.Web.UI.Page
    {
        public bool isVisible = false;
        public bool isSubmit = true;
        public bool isView = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Session["ResidentialUnitSerial"] = "100000000004";
                //Session["TenentId"] = "100000000001";
                if (Session["ResidentialUnitSerial"] == null && Session["TenentId"] == null)
                {
                    Response.Redirect("ResidentialAddResponceTemplate_Login.aspx");
                }
                var isAdmin = false;
                if (Session["UserObject"] != null)
                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                        ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                        : false;

                if (Session["UserType"] != null)
                {
                    if (isAdmin == true || Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3")
                    {
                        isVisible = true;
                        isView = false;
                    }
                }
            }



        }
        [WebMethod]
        public static string GetTenantEmergencyContactInformationList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantEmergencyContactList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }
        [WebMethod]
        public static string SaveEmergencyContactInformation(Residential_Tenant_Add_Step2_Page2_EmergencyContacts Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_Add_Step2_Page2_EmergencyContacts>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
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
        [WebMethod(EnableSession = true)]
        public static string DeleteTenantEmergencyInfo(Residential_Tenant_Add_Step2_Page2_EmergencyContacts Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_Add_Step2_Page2_EmergencyContacts>();
            if (Obj.Id > 0)
            {
                try
                {
                    Residential_Tenant_Add_Step2_Page2_EmergencyContacts newObj = new ResidentialAddResponceTemplateDa().GetTenantResidenceEmergencyContactById(Obj.Id);
                    newObj.DeletedBy = (HttpContext.Current.Session["TenentId"]).ToString();
                    newObj.DeletedDate = DateTime.Now;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantEmergencyContact(newObj))
                    {

                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantEmergencyContactList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
                catch (Exception)
                {


                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string EditTenantEmergency(Residential_Tenant_Add_Step2_Page2_EmergencyContacts Obj)
        {
            var insertedOrUpdatedRecord = new Residential_Tenant_Add_Step2_Page2_EmergencyContacts();
            if (Obj.Id > 0)
            {
                try
                {
                    insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantResidenceEmergencyContactById(Obj.Id);

                }
                catch (Exception)
                {


                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;
        }



        //------------------------------------- Credit Section ----------------------------------//
        //[WebMethod]
        //public static string SaveCredit(ResidentialTenantStep2_Page2_CreditHistory Obj)
        //{
        //    var insertedOrUpdatedRecord = new List<ResidentialTenantStep2_Page2_CreditHistory>();
        //    if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
        //      string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
        //        return "";
        //    if (Obj != null)
        //    {
        //        if (Obj.Id > 0)
        //        {
        //            var NewRentalTenantResidenceHistory = new ResidentialAddResponceTemplateDa().GetTenantResidenceEmergencyContactById(Obj.Id);

        //            Obj.Serial = NewRentalTenantResidenceHistory.Serial;
        //            Obj.ResidentialUnitSerialId = NewRentalTenantResidenceHistory.ResidentialUnitSerialId;
        //            //obj.IsViewedOrDownloaded = NewRentalDocumentObject.IsViewedOrDownloaded;
        //            if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantEmergencyContact(Obj))
        //            {
        //                insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantEmergencyContactList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

        //            }
        //        }
        //        else
        //        {
        //            Obj.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
        //            Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

        //            if (new ResidentialAddResponceTemplateDa().InsertResidentialEmergencyContact(Obj))
        //            {
        //                insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantEmergencyContactList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
        //            }
        //        }
        //    }
        //    //  var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

        //    var jsonSerialiser = new JavaScriptSerializer();
        //    var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
        //    return json;

        //}

        [WebMethod]
        public static string GetCreditInformationList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetCreditInformationList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }

        [WebMethod]
        public static string GetVehicleformationList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetVehicleInformationList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }
        [WebMethod]
        public static string GetPeopleUnitformationList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetPeopleInformationList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }
        [WebMethod]
        public static string SaveTetantCreditVehicleAndUnitInformation(CreditInfoEmergencyVehiclePeopleObject Obj)
        {
            if(HttpContext.Current.Session["ResidentialUnitSerial"] == null &&  HttpContext.Current.Session["TenentId"] == null)
                return "";
            var myCon = ConfigurationSettings.AppSettings["ConnectionStringNew"].ToString();

           // var con = ConfigurationManager.ConnectionStrings["EPropertyEntities"].ConnectionString;
            



            //DataTable newdt = new DataTable();
            ////DataTable sizeObj = SizeList.ToDataTable<Size>();
            ////DataTable MasterObj = Master.ToDataTable<FabricBookingMaster>();
            ////DataTable FabricObj = FabricList.ToDataTable<Fabric>();
            ////DataTable SpecialInstructionObj = SpecialInstruction.ToDataTable<SpecialInstruction>();
            ////DataTable BookingInstructionObj = BookingInstruction.ToDataTable<BookingFootNode>();
            ////DataTable WashAndCareLogoObj = WashAndCareLogoList.ToDataTable<Washlogo>();
            ////DataTable RemarksObj = RemarksList.ToDataTable<SampleRemarks>();
            ////DataTable ApprovalObj = ApprovalList.ToDataTable<ApprovalObj>();

            //List<SqlParameter> paramList = new List<SqlParameter>();
            ////  cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            //paramList.Add(new SqlParameter("@Master", MasterObj));
            //paramList.Add(new SqlParameter("@size", sizeObj));
            //paramList.Add(new SqlParameter("@Fabric", FabricObj));
            //paramList.Add(new SqlParameter("@SpecialInstruction", SpecialInstructionObj));
            //paramList.Add(new SqlParameter("@BookingFootNode", BookingInstructionObj));
            //paramList.Add(new SqlParameter("@Wash", WashAndCareLogoObj));
            //paramList.Add(new SqlParameter("@Remarks", RemarksObj));
            //paramList.Add(new SqlParameter("@Approval", ApprovalObj));
            //string autoId = "";
            //FabricAutoId = ExecProcedureWithTable("usp_Insert_FabricBookingSample_1", paramList, AllDBName.MER_DB,
            //    out error, autoId);
            //return FabricAutoId;

            var BaseObject = new TenantCommonModel();
            BaseObject.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
            BaseObject.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString();
            Obj.Credit.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
            Obj.Credit.ResidentialUnitSerialId= (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString();
            Obj.PeopleStayingUnit.ForEach(x=>x.Serial = (HttpContext.Current.Session["TenentId"]).ToString());
            Obj.PeopleStayingUnit.ForEach(x => x.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());


            Obj.Vehicles.ForEach(x => x.Serial = (HttpContext.Current.Session["TenentId"]).ToString());
            Obj.Vehicles.ForEach(x => x.ResidentialUnitSerialId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var result = new ResidentialAddResponceTemplateDa().InsertCreditVehicleUnitData(BaseObject, Obj, myCon);
            bool nextStep = true;

            // var TenantBasicData = new ResidentialAddResponceTemplateDa().GetCreditInformationList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            if (result == "true")
            {
                nextStep = new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(nextStep);
            return json;
        }

    }
}