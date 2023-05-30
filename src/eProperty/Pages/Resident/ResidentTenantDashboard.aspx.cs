using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using eProperty.Models;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;

namespace eProperty.Pages.Resident
{
    public partial class ResidentTenantDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
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
        public static string GetTenantName()
        {
            var TenantName = ((UserProfile) HttpContext.Current.Session["UserObject"]).Title.ToString();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantName);
            return json;
        }

        [WebMethod]
        public static string LoadMessage(MessageSearch Obj)
        {
            var FromUser = HttpContext.Current.Session["TenentId"] != null ? HttpContext.Current.Session["TenentId"].ToString() : "";
            var UnitId = HttpContext.Current.Session["ResidentialUnitSerial"] != null ? HttpContext.Current.Session["ResidentialUnitSerial"].ToString() : "";
            var ToUser = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId;
            var Result = new TenantDashboardDA().GetMessageData(FromUser, ToUser, UnitId,Obj.MonthName,Obj.RequestType);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(Result);
            return json;
        }

        [WebMethod]
        public static string SendMessage(communicationPanel Obj)
        {
            var result = new List<usp_GetMessage_Result>();
            if (Obj !=null)
            {
               // Obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialCommunication");
                Obj.FromUser = HttpContext.Current.Session["TenentId"].ToString();
                Obj.ToUser = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId;
                Obj.QuestionId = 0;
                Obj.UnitId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                Obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                Obj.CreatedDate = DateTime.Now;
                if (new TenantDashboardDA().InsertComminicationPanelData(Obj))
                {
                    var FromUser = HttpContext.Current.Session["TenentId"].ToString();
                    var UnitId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                    var ToUser = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId;
                    result = new TenantDashboardDA().GetMessageData(FromUser, ToUser, UnitId, DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture),"");
                }
                else
                {
                   
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(result);
            return json;

        }
    }
}