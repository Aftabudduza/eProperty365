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
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialAddResponceTemplate_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //spanYear.InnerHtml = DateTime.UtcNow.Year.ToString();
                //Session["ResidentialUnitSerial"] = null;
                Session["TenentId"] = null;
               // if (Session["UserObject"] != null)
                //{
                    string cId;
                    try
                    {
                        cId = Request.QueryString["ResidentialUnitSerial"];
                    }
                    catch (Exception ex)
                    {
                        cId = "";
                    }
                    if (cId != "" && cId !=null)
                    {

                        Session["ResidentialUnitSerial"] = cId;
                    }
                    else
                    {
                        Response.Redirect("ResidentialUnitListing.aspx");
                    }
                //}
            }
        }
        [WebMethod(EnableSession = true)]
        public static string GetAllIamge()
        {

            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
                string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var imageobj = new List<ResidentialUnitWebImage>();

            try
            {
                var residentialUnitWebImage = new ResidentialUnitDa().GetAllWebpagesListbyIsTrue(
                    HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
                if (residentialUnitWebImage.Count > 0)
                {
                    foreach (ResidentialUnitWebImage aObj in residentialUnitWebImage)
                    {
                        ResidentialUnitWebImage obj = new ResidentialUnitWebImage();
                        obj.ImagePath = "../../Uploads/Images/" + aObj.ImageName;
                        obj.ShortDescription = aObj.ShortDescription;
                        imageobj.Add(obj);
                    }
                }

                //"..\..\Uploads\Images\images(2).jpg"
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(imageobj);
                return json;
            }
            catch (Exception)
            {

                return "";
            }
        }
        [WebMethod]
        public static string GetResidentialQuickFeaturesView()
        {
            List<usp_GetResidentialFeatureSpecs_Result> accObj = new List<usp_GetResidentialFeatureSpecs_Result>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] != null)
            {
                accObj = new ResidentialUnitDa().GetAllFeatureList(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(accObj);
            return json;

        }
        [WebMethod]
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
        [WebMethod]
        public static string GetOtherData()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var specData = new ResidentialUnitDa().GetAllResidentialUnitSpecsList(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(specData);
            return json;
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

        [WebMethod]
        public static string FirstTimeSignIn(ResidentialTenantSignIn Obj)
        {
            var resultobj = new TenantSignInModel();
            try
            {
                if (Obj != null)
                {
                    resultobj = new ResidentialAddResponceTemplateDa().CheckFirstTimeSignIn(Obj, HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
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
        [WebMethod]
        public static string SecondTimeSignIn(ResidentialTenantSignIn Obj)
        {
            var resultobj = new TenantSignInModel();
            try
            {
                if (Obj != null)
                {
                    resultobj = new ResidentialAddResponceTemplateDa().CheckSecondTimeSignIn(Obj, HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
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