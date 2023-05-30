using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using eProperty.Models;
using PropertyService.BO;
using PropertyService.DA.Account;
using PropertyService.ViewModel;

namespace eProperty.Pages.Account
{
    public partial class AddChartofAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                    string username = ((UserProfile)Session["UserObject"]).Username;
                    string SQL = " update UserProfile set HasAccountSystem = 1  where Username = '" + username + "' ";
                    Utility.RunCMD(SQL);
                    Utility.RunCMDMain(SQL);
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
        }
        [WebMethod]
        public static string GetAccountType()
        {
            List<ComboData> lstComboData = new List<ComboData>();
            List<AccountType> listAccountType = new AddChartofAccountDA().GetAccountType();
            foreach (var obj in listAccountType)
            {
                ComboData cmb = new ComboData();
                cmb.Id = obj.id;
                cmb.Id2 = obj.type;
                cmb.Data = obj.description;
                lstComboData.Add(cmb);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod]
        public static string GetAccountChart()
        {
            string OwnerId = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId;
            List<AccountModel> listAccountChart = new AddChartofAccountDA().GetAccountChart(OwnerId);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(listAccountChart);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string Save(AccountChart Obj)
        {
            var res = false;
            if (Obj != null)
            {
                Obj.OwnerId = ((UserProfile) HttpContext.Current.Session["UserObject"]).OwnerId;
                Obj.CreateDate = DateTime.Now;
                Obj.editAble = true;
                if (Obj.id>0)
                {
                    if (new AddChartofAccountDA().Update(Obj))
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }
                }
                else
                {
                    if (new AddChartofAccountDA().Insert(Obj))
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }
                }
                
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
    }
}