using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using eProperty.Models;
using PropertyService.BO;
using PropertyService.DA.Account;
using PropertyService.DA.Report;
using PropertyService.ViewModel;

namespace eProperty.Pages.ReportPage
{
    public partial class BasicReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        [WebMethod]
        public static string GetReportList()
        {
            List<ComboData> lstComboData = new List<ComboData>();
            List<PropertyService.BO.Report> list = new BasicReportDA().GetReportList();
            foreach (var obj in list)
            {
                ComboData cmb = new ComboData();
                cmb.Id = obj.id;
                cmb.Data = obj.name;
                lstComboData.Add(cmb);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod]
        public static string SetReportSessionData(ReportParamModel Obj)
        {
            Obj.CompanyName = new BasicReportDA().GetCompanyName(HttpContext.Current.Session["OwnerId"].ToString());
            HttpContext.Current.Session.Remove("reportObj");
            string content = "/Pages/ReportPage/CommonReportViewer.aspx";
            HttpContext.Current.Session["reportObj"] = Obj;

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(content);
            return json;

        }
        [WebMethod]
        public static string SetReportSessionDataIncome(ReportParamModel Obj)
        {
            UserProfile objUser = new UserProfile();
            objUser = (UserProfile)HttpContext.Current.Session["UserObject"];
            Obj.PrintBy = objUser != null ? objUser.Username : "";
            Obj.CompanyName = new BasicReportDA().GetCompanyName(HttpContext.Current.Session["OwnerId"].ToString());

            HttpContext.Current.Session.Remove("reportObj");
            string content = "/Pages/ReportPage/CommonReportViewer.aspx";
            HttpContext.Current.Session["reportObj"] = Obj;

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(content);
            return json;

        }
    }
}