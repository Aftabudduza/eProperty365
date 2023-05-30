using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA.Admin.ResidentialTenent;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialTenantRental_App_Page_3 : System.Web.UI.Page
    {
        public bool isVisible = false;
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
        public static string GetGeneralInformationList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetGeneralInformationList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }
        [WebMethod]
        public static string SaveGeneralInfo(Residential_Tenant_Add_Step2_Page3_GeneralInformation Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_Add_Step2_Page3_GeneralInformation>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            bool nextStep = true;
            bool result = true;
            if (Obj != null)
            {
                if (Obj.Id > 0)
                {
                    var NewRentalTenantResidenceHistory = new ResidentialAddResponceTemplateDa().GetGeneralInfoById(Obj.Id);

                    Obj.Serial = NewRentalTenantResidenceHistory.Serial;
                    Obj.ResidentialUnitSerialId = NewRentalTenantResidenceHistory.ResidentialUnitSerialId;
                    result = new ResidentialAddResponceTemplateDa().UpdateGeneralInfo(Obj);
                    //obj.IsViewedOrDownloaded = NewRentalDocumentObject.IsViewedOrDownloaded;

                }
                else
                {
                    Obj.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
                    Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                    result = new ResidentialAddResponceTemplateDa().InsertGeneralInfo(Obj);
                    if (result == true)
                    {
                        nextStep = new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");
                    }
                }
            }
            //  var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

           
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(nextStep);
            return json;

        }
    }
}