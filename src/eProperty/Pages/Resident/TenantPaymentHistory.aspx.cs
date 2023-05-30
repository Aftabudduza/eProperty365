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
    public partial class TenantPaymentHistory : System.Web.UI.Page
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
        public static string GetPaymentHistory()
        {
            var res = new List<PropertyService.BO.TenantPaymentHistory>();
            if (HttpContext.Current.Session["TenentId"] !=null)
            {
                var FromUser = HttpContext.Current.Session["TenentId"].ToString();
                 res = new TenantDashboardDA().GetPaymentHistory(FromUser).ToList();
            }
            
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
    }
}