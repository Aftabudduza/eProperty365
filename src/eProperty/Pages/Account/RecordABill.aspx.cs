using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using eProperty.Models;
using PropertyService.BO;
using PropertyService.DA.Account;
using PropertyService.ViewModel;

namespace eProperty.Pages.Account
{
    public partial class RecordABill : System.Web.UI.Page
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
        public static string GetAutoGenNumber()
        {
            string BillNumber = new RecordABillDA().MakeAutoGenNumber("R", "RecordABill");
            
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(BillNumber);
            return json;
        }
        [WebMethod]
        public static string GetBillNumber()
        {
            List<ComboData> lstComboData = new List<ComboData>();
            var list = new RecordABillDA().GetBillNumber();
            foreach (var obj in list)
            {
                ComboData cmb = new ComboData();
                cmb.Id = obj.Id;
                cmb.Data = obj.BillNumber;
                lstComboData.Add(cmb);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }

        [WebMethod]
        public static string GetBillNumberWiseData(PropertyService.BO.RecordABill Obj)
        {
            var list = new RecordABillDA().GetRecordABillData(Obj.Id);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list);
            return json;
        }

        [WebMethod]
        public static string GetAccountType()
        {
            List<ComboData> lstComboData = new List<ComboData>();
            List<AccountType> listAccountType = new RecordABillDA().GetAccountType();
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
        public static string GetAccountName(RecordABillDetails Obj)
        {
            List<ComboData> lstComboData = new List<ComboData>();
            List<AccountChart> listAccountName = new RecordABillDA().GetAccountChart(Obj.Type);
            foreach (var obj in listAccountName)
            {
                ComboData cmb = new ComboData();
                cmb.Id = obj.id;
                cmb.Id2 = obj.accountCode;
                cmb.Id3 = obj.accountTypeId;
                cmb.Data = obj.accountName;
                lstComboData.Add(cmb);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod]
        public static string DeleteData(RecordABillDetails Obj)
        {
            var res = false;
            if (Obj != null) 
            {
                if (new RecordABillDA().Delete(Obj.Id))
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
        public static string Save(RecordABillModel Obj)
        {
            var res = false;
            if (Obj != null)
            {
                if (Obj.Master.Id > 0)
                {
                    if (new RecordABillDA().Update(Obj))
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
                    if (new RecordABillDA().Insert(Obj))
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