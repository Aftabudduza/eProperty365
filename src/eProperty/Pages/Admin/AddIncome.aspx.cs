using System;
using System.Collections.Generic;
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
using PropertyService.Enums;

namespace eProperty.Pages.Resident
{
    public partial class AddIncome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserObject"] != null)
            {

            }
        }
        [WebMethod]
        public static string GetInitialData()
        {
            List<List<ComboData>> lstComboData = new List<List<ComboData>>();
            string OwnerId = HttpContext.Current.Session["OwnerId"].ToString();
            var dataSet = new AddIncomeDA().GetInitialData(OwnerId);
            if (dataSet.Tables[0].Rows.Count>0)
            {
                var cmbList = new List<ComboData>();
                cmbList = dataSet.Tables[0].DataTableToList<ComboData>();
                cmbList[0].Id2 = OwnerId;
                lstComboData.Add(cmbList);
            }
            if (dataSet.Tables[1].Rows.Count > 0)
            {
                var cmbList = new List<ComboData>();
                cmbList = dataSet.Tables[1].DataTableToList<ComboData>();
                lstComboData.Add(cmbList);
            }
            if (dataSet.Tables[2].Rows.Count > 0)
            {
                var cmbList = new List<ComboData>();
                cmbList = dataSet.Tables[2].DataTableToList<ComboData>();
                lstComboData.Add(cmbList);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string Save(PropertyService.BO.AddIncome Obj)
        {
            var res = false;
            if (Obj != null)
            {
                Obj.OwnerId = (HttpContext.Current.Session["OwnerId"]).ToString();
                string toUser = String.Empty;
                int ToUserType = 0;
                decimal debit = Decimal.Zero;
                decimal Credit = Decimal.Zero;
                if (Obj.TransactionType.Equals("Payment"))
                {
                    toUser = Obj.PropertyManagerId;
                    ToUserType = Convert.ToInt32(EnumUserType.Manager);
                    debit= Convert.ToDecimal(Obj.EnterAmountBeingPaid);
                }
                else
                {
                    toUser = Obj.ResidentTenantName;
                    ToUserType = Convert.ToInt32(EnumUserType.Resident);
                    Credit = Convert.ToDecimal(Obj.EnterAmountBeingPaid);
                }
                Obj.CreateDate = DateTime.Now;
                if (Obj.AccountNo != string.Empty && Obj.AccountNo.Length > 4)
                {
                    Obj.AccountNo = Obj.AccountNo.Substring(Obj.AccountNo.Length - 4, 4);
                }
                //List<Get_TenantInformation_Result> tenObjInformation = new ResidentialAddResponceTemplateDa().GetTenantInformation(Obj.OwnerId);
                var paymentHistory = new PropertyService.BO.TenantPaymentHistory()
                {
                    FromUser = Obj.OwnerId,
                    FromUserType = Convert.ToInt32(HttpContext.Current.Session["UserType"].ToString()),
                    ToUser = toUser,
                    ToUserType = ToUserType,
                    UnitNo = Obj.UnitId,
                    Amount = Obj.EnterAmountBeingPaid,
                    AccountName = Obj.AccountName,
                    Address = Obj.Address,
                    Address1 = Obj.Address,
                    City = Obj.City,
                    State = Obj.State,
                    Zip = Obj.ZipCode,
                    CardNumber = Obj.AccountNo,
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    LastFourDigitCard = Obj.AccountNo,
                    IsCheckingAccount = Obj.AccountType.Equals("Check")? true:false,
                    RoutingNo = Obj.RoutingNo,
                    AccountNo = Obj.AccountNo,
                    CheckNo = Obj.AccountNo,
                    AccountType = Obj.AccountType,
                    Getway = "Add Income",
                    DebitAmount = Obj.TransactionType.Equals("Payment") ? 0 : Convert.ToDecimal(Obj.EnterAmountBeingPaid),
                    CreditAmount = Obj.TransactionType.Equals("Payment") ? Convert.ToDecimal(Obj.EnterAmountBeingPaid) : 0,
                    CreateDate = DateTime.Now
                };

                int paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);
                if (paymentHistoryId > 0)
                {
                    Obj.PaymentHistoryId = paymentHistoryId;
                    if (new AddIncomeDA().Insert(Obj))
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