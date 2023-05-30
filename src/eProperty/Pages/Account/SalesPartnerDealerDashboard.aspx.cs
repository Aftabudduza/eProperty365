using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using PropertyService.BO;
using PropertyService.DA.Account;
using PropertyService.ViewModel;

namespace eProperty.Pages.Account
{
    public partial class SalesPartnerDealerDashboard : System.Web.UI.Page
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
        public static string GetData()
        {
            List<DealerSalesPartnerModel> objList = new List<DealerSalesPartnerModel>();
            if (HttpContext.Current.Session["UserObject"] != null)
            {
                DealerSalesPartnerModel objModel = new DealerSalesPartnerModel();
                PropertyService.BO.UserProfile userProfile = (HttpContext.Current.Session["UserObject"]) as PropertyService.BO.UserProfile;
                /*if (userProfile.UserType != "1")
                {
                    objModel = new SalesPartnerDealerDashboardDA().GetDealerSalesData(userProfile);
                    objModel.UserType = userProfile.UserType;
                    if (objModel.SerialCode == null && objModel.UserType != "1")
                    {
                        string objName = userProfile.UserType == "9" ? "SalesPartnerProfile" : userProfile.UserType == "10" ? "DealerProfile" : "";
                        objModel.SerialCode = new SalesPartnerDealerDashboardDA().MakeAutoGenNumber("1", objName);
                    }
                    objList.Add(objModel);
                }
                else
                {
                    objList = new SalesPartnerDealerDashboardDA().GetDealerSalesData(userProfile);
                }*/
                objList = new SalesPartnerDealerDashboardDA().GetDealerSalesData(userProfile);


            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(objList);
            return json;
        }
        [WebMethod]
        public static string GetDataById(DealerSalesPartnerModel Obj)
        {
            DealerSalesPartnerModel obj = new SalesPartnerDealerDashboardDA().GetDataById(Obj);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
        [WebMethod]
        public static string GetOwnerByLocationId(DealerSalesPartnerAccounts Obj)
        {
            DealerSalesPartnerAccounts objModel = new DealerSalesPartnerAccounts();
            if (HttpContext.Current.Session["UserObject"] != null)
            {
                
                PropertyService.BO.UserProfile userProfile =
                    (HttpContext.Current.Session["UserObject"]) as PropertyService.BO.UserProfile;
                objModel = new SalesPartnerDealerDashboardDA().GetOwnerByLocationId(userProfile,Obj.LocationId);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(objModel);
            return json;
        }
        [WebMethod]
        public static string SearchData(DealerSalesPartnerAccounts Obj)
        {
            List<GetDealerSalesAccounts_Result> obj = new SalesPartnerDealerDashboardDA().GetSearchData(Obj);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
        [WebMethod]
        public static string AlreadyExistMail(DealerSalesPartnerModel Obj)
        {
            bool obj = new SalesPartnerDealerDashboardDA().AlreadyExistMail(Obj);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
        [WebMethod]
        public static string GetAutoGenNumber()
        {
            List<DealerSalesPartnerModel> objList = new List<DealerSalesPartnerModel>();
            DealerSalesPartnerModel objModel1 = new DealerSalesPartnerModel();
            objModel1.SerialCode = new SalesPartnerDealerDashboardDA().MakeAutoGenNumber("1", "SalesPartnerProfile");
            objModel1.UserType = "9";
            objList.Add(objModel1);
            DealerSalesPartnerModel objModel2 = new DealerSalesPartnerModel();
            objModel2.SerialCode = new SalesPartnerDealerDashboardDA().MakeAutoGenNumber("1", "DealerProfile");
            objModel2.UserType = "10";
            objList.Add(objModel2);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(objList);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string Save(DealerSalesPartnerModel Obj)
        {
            var res = false;
            if (Obj != null)
            {
                Obj.CreateDate = DateTime.Now;
                if (new SalesPartnerDealerDashboardDA(true, false).Insert(Obj))
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string Update(DealerSalesPartnerModel Obj)
        {
            var res = false;
            if (Obj != null)
            {
                Obj.CreateDate = DateTime.Now;
                if (new SalesPartnerDealerDashboardDA().Update(Obj))
                {
                    PropertyService.BO.UserProfile userProfile = (HttpContext.Current.Session["UserObject"]) as PropertyService.BO.UserProfile;
                    if (userProfile.UserType != "1")
                    {
                        UserProfile objUser = new SalesPartnerDealerDashboardDA().GetUpdateUser(userProfile.Email);
                        HttpContext.Current.Session["UserObject"] = objUser;
                        HttpContext.Current.Session["Username"] = objUser.Username;
                    }
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
        [WebMethod]
        public static string DeleteData(Dealer_SalesPartner_DetailsZipCodeCoverage Obj)
        {
            var res = false;
            if (Obj != null)
            {
                if (new SalesPartnerDealerDashboardDA().Delete(Obj))
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
        
    }
}