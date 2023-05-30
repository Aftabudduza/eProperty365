using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialUnitListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["UserObject"] != null)
                {

                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static string LoadResidentialGrid()
        {
            List<ResidentialUnit> obj = new List<ResidentialUnit>();
            List<ResidentialUnit> result = new List<ResidentialUnit>();
            if (HttpContext.Current.Session["OwnerId"] != null)
                obj = new ResidentialUnitDa().GetByOwnerData(HttpContext.Current.Session["OwnerId"].ToString());
            foreach (ResidentialUnit a in obj)
            {
                ResidentialUnit r = new ResidentialUnit();
                r.Serial = a.Serial;
                r.UnitName = a.UnitName;
                r.Id = a.Id;
                result.Add(r);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string LoadResidentialGrid_Approval()
        {
            List<ResidentialTenantSignIn> obj = new List<ResidentialTenantSignIn>();
            if (HttpContext.Current.Session["OwnerId"] != null)
                obj = new ResidentialUnitDa().GetByTenantData(HttpContext.Current.Session["OwnerId"].ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
    }
    
}