using System;
using System.Collections.Generic;
using System.Data;
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
using PropertyService.Admin.DA;
using PropertyService.Enums;
using PropertyService;
using PropertyService.DA.Account;

namespace eProperty.Pages.Resident
{
    public partial class TenantOnlinefee : System.Web.UI.Page
    {
        public bool isVisible = false;
        public bool isView = true;
        public string api_loginID;
        public string utc_time = DateTime.Now.Ticks.ToString();
        public string pay_now_single_return_string;
        Gateway.Signature cliSignature = new Gateway.Signature();     // This is the worker object that takes the values and performs functions.
        public string return_string = "Initial";
        public string source = "";
        string strLoginID = System.Configuration.ConfigurationManager.AppSettings.Get("LoginID");
        string strSecureKey = System.Configuration.ConfigurationManager.AppSettings.Get("ProcessingKey");
        public string sTotal = "1";
        public string sOrderNo = "A1234";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["UserObject"] == null)
                //{
                //    Response.Redirect("~/Pages/Logout.aspx", false);
                //}

                string invoiceId, tenantId;
               
                try
                {
                    tenantId = Request.QueryString["TenentId"];
                }
                catch (Exception ex)
                {
                    tenantId = "";
                }

                try
                {
                    invoiceId = Request.QueryString["InvoiceId"];
                }
                catch (Exception ex)
                {
                    invoiceId = "";
                }

                if (tenantId != null && tenantId != "")
                {
                    Session["TenentId"] = tenantId;
                    var objTenant = new ResidentialAddResponceTemplateDa().GetTenantbyId(tenantId);

                    if (objTenant != null)
                    {
                        UserProfile objTenantUser = new AdminUserProfileDA().GetUserByEmail(objTenant.EmailId);
                        if(objTenantUser != null)
                        {
                            HttpContext.Current.Session["UserObject"] = objTenantUser;
                            HttpContext.Current.Session["UserType"] = objTenantUser.UserType;
                            HttpContext.Current.Session["Username"] = objTenantUser.Username;
                            HttpContext.Current.Session["bIsLogin"] = true;
                            HttpContext.Current.Session["OwnerId"] = objTenantUser.OwnerId;
                            HttpContext.Current.Session["UserId"] = objTenantUser.Id;
                            Utility.LoginUser = objTenantUser.Username;
                        }
                    }
                }
                api_loginID = strLoginID;

                if (invoiceId != null && invoiceId != "")
                {                   
                    var objInvoice = new MonthlyBatchRentalInvoiceDA().GetMonthlyBatchRentalInvoiceByInvoiceNo(invoiceId, tenantId);

                    if (objInvoice != null)
                    {
                        var get_app = new ResidentialAddResponceTemplateDa().GetApplicationFree(objInvoice.UnitId);
                        var typeChck = get_app.FeeTypeCheck;
                        var percentOrFlatVal = 0.0;
                        var subtotal = objInvoice.Amount != null ? Convert.ToDouble(objInvoice.Amount) : 0;
                        var nTotal = 0.0;

                        if (get_app.TanentPayFees != null && Convert.ToBoolean(get_app.TanentPayFees) == true)
                        {
                            if (typeChck == 1)
                            {
                                percentOrFlatVal = get_app.FeePercentageCheck != null ? Convert.ToDouble(get_app.FeePercentageCheck) : 0;
                                var nCharge = subtotal * percentOrFlatVal / 100;
                                nTotal = subtotal + nCharge;
                               // txtACHFee.Value = nCharge.ToString();
                            }
                            else
                            {
                                percentOrFlatVal = get_app.FeeFlatAmountCheck != null ? Convert.ToDouble(get_app.FeeFlatAmountCheck) : 0;
                                nTotal = subtotal + percentOrFlatVal;
                             //   txtACHFee.Value = percentOrFlatVal.ToString();
                            }
                        }
                        else
                        {
                            nTotal = subtotal;
                        }

                        sTotal = nTotal.ToString();

                        txtRentAmount.InnerText = subtotal.ToString();

                    }
                }

                if (Session["UserObject"] != null)
                {
                    if (Session["UserId"] != null)
                    {
                        spanAccount.InnerHtml = "<a style='color:#fff;' href='" + Utility.WebUrl + "/Pages/Admin/AddUser.aspx?IsTopMenu=1&AddUserId= " + Session["UserId"].ToString() + "'>Account Profile </a>";
                        spanReset.InnerHtml = "<a class='btn btn-default btn-flat' href='" + Utility.WebUrl + "/Pages/Admin/ResetPassword.aspx?AddUserId= " + Session["UserId"].ToString() + "'>Reset Password </a>";
                        spanSignOut.InnerHtml = "<a class='btn btn-default btn-flat' href='" + Utility.WebUrl + "/Pages/Logout.aspx'>Sign out</a>";

                        UserProfile obj = new AdminUserProfileDA().GetUserByUserID(Convert.ToInt32(Session["UserId"].ToString()));
                        if (obj != null)
                        {
                            if (!string.IsNullOrEmpty(obj.Location))
                            {
                                imgTopLogo.ImageUrl = Utility.WebUrl + "/Uploads/Files/User/" + obj.Location;
                                imgTopIcon.ImageUrl = Utility.WebUrl + "/Uploads/Files/User/" + obj.Location;
                            }
                        }
                    }

                    if (Session["UserType"] != null)
                    {
                        if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16)EnumUserType.Resident)
                        {
                            lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                               "/Pages/Resident/ResidentTenantDashboard.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                        }
                        else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16)EnumUserType.Condo)
                        {
                            lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                               "/Pages/DashboardCondo.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                        }
                        else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16)EnumUserType.Commercial)
                        {
                            lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                               "/Pages/DashboardCommercial.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                        }

                        liHeader.InnerHtml = "Users =: " + Enum.GetName(typeof(EnumUserType), Convert.ToInt16(Session["UserType"].ToString()));
                        spanDash.InnerHtml = Enum.GetName(typeof(EnumUserType), Convert.ToInt16(Session["UserType"].ToString())) + " Dashboard";
                    }
                    else
                    {
                        Response.Redirect(Utility.WebUrl + "/Pages/Login.aspx", false);
                    }

                }
                else
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/Login.aspx", false);
                }
            }

            Random generator = new Random();
            int r = generator.Next(1, 1000000);
            sOrderNo = r.ToString().PadLeft(6, '0');

            cliSignature.api_login_id = strLoginID;            //Add your API login ID.
            cliSignature.secure_trans_key = strSecureKey;    //Add your secure transaction key.    

            cliSignature.pay_now_single_payment = cliSignature.api_login_id + "|sale|1.0|" + sTotal + "|" + utc_time + "|" + sOrderNo + "||";
            api_loginID = cliSignature.api_login_id;
            pay_now_single_return_string = Gateway.EndPoint(cliSignature, "CREATESIGNATUREPAYSINGLEAMOUNT");

        }

        [WebMethod]
        public static string GetLocation()
        {
            var lstOfComboData = new List<ComboData>();
            if (HttpContext.Current.Session["UserObject"] != null)
            {
               
                //var ownerId = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId;
               // var lstOfLocation = new TenantDashboardDA().GetLocation(ownerId);
                var lstOfLocation = new List<Location>();
               // var TenantAndUnitId = new TenantDashboardDA().GetSerialAndUnitId(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                string sUnitId = string.Empty;
                string sLocationId = string.Empty;
                sLocationId = ((UserProfile)HttpContext.Current.Session["UserObject"]).LocationId;
                if (sLocationId != string.Empty)
                {
                    var location = new LocationDA().GetbySerial(sLocationId);
                    if (location != null)
                    {
                        lstOfLocation.Add(location);
                    }
                }

                foreach (Location loc in lstOfLocation)
                {
                    ComboData c = new ComboData();
                    c.Data = loc.LocationName;
                    c.Id = loc.Id;
                    lstOfComboData.Add(c);

                }
               
            }
            else
            {
              HttpContext.Current. Response.Redirect("../Login.aspx");
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstOfComboData);
            return json;

        }

        [WebMethod]
        public static string GetUnitId()
        {
            DataTable dt = new DataTable() ;
            string sUnitId = "";
            if (HttpContext.Current.Session["UserObject"] != null)
            {

                dt = new TenantDashboardDA().GetUnitId(Convert.ToInt32(((UserProfile) HttpContext.Current.Session["UserObject"]).Id));
                if(dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0] != null)
                    {
                        sUnitId = dt.Rows[0][0].ToString();
                    }
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("../Login.aspx");
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(sUnitId);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string Save(TenantOnlineFee Obj)
        {
            var res = true;

            if (Obj != null)
            {
                Obj.TenantId = (HttpContext.Current.Session["TenentId"]).ToString();
                Obj.CreateDate = DateTime.Now;
                if (Obj.AccountNo != string.Empty && Obj.AccountNo.Length > 4)
                {
                    Obj.AccountNo = Obj.AccountNo.Substring(Obj.AccountNo.Length - 4, 4);
                }
                List<Get_TenantInformation_Result> tenObjInformation = new ResidentialAddResponceTemplateDa().GetTenantInformation(Obj.TenantId);

                string sOwnerId = tenObjInformation[0] != null ? tenObjInformation[0].OwnerId : "";
                //OwnerProfile objOwner = new AdminOwnerProfileDA().GetByUserName(sOwnerId);
                //int nOwnerId = objOwner != null ? objOwner.Id : 0;
                //string sNumber = nOwnerId.ToString().PadLeft(4, '0');

                int sPackage = sOwnerId.Length > 1 ? Convert.ToInt32(sOwnerId.Substring(1)) : 1;
                string sNumber = sPackage.ToString().PadLeft(4, '0').ToString();
                string sPrefix = "I" + sNumber;

                var paymentHistory = new PropertyService.BO.TenantPaymentHistory()
                {
                    FromUser = Obj.TenantId,
                    //FromUserType = Convert.ToInt32(tenObjInformation[0].FromUserType),
                    ToUser = tenObjInformation[0].OwnerId,
                    // ToUserType = Convert.ToInt32(tenObjInformation[0].ToUserType),
                    FromUserType = 5,
                    ToUserType = 2,
                    UnitNo = Obj.UnitId,
                    Amount = Obj.PaidAmount,
                    AccountName = Obj.AccountName,
                    Address = Obj.Address,
                    Address1 = Obj.Address,
                    City = Obj.City,
                    State = Obj.State,
                    Zip = Obj.Zip,
                    CardNumber = Obj.AccountNo,
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    LastFourDigitCard = Obj.AccountNo,
                    IsCheckingAccount = true,
                    RoutingNo = Obj.RoutingNo,
                    AccountNo = Obj.AccountNo.Length > 4 ? Obj.AccountNo.Substring(Obj.AccountNo.Length - 4, 4) : Obj.AccountNo,
                    CheckNo = "",
                    AccountType = "Check",
                    AuthorizationCode = "",
                    TransactionCode = "",
                    TransactionDescription = "",
                    Getway = "Tenant Online Fee",
                    DebitAmount = 0,
                    CreditAmount = Obj.PaidAmount,
                    CreateDate = DateTime.Now,
                    Status = "pending",
                    Serial = new ResidentialAddResponceTemplateDa().MakeAutoGenSerialForPaymentHistory(sPrefix, "Invoice"),
                    TransactionType = "Rent",
                    LedgerCode = "4010",
                    Remarks = ""

                };

                //  int paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);
                int paymentHistoryId = 0;

                var ExistingHistory = new ResidentialAddResponceTemplateDa().GetTenantPaymentHistory(Obj.UnitId, Obj.TenantId, "Tenant Online Fee");

                if (ExistingHistory != null)
                {
                    if (new ResidentialAddResponceTemplateDa().DeletePaymentHistoryById(ExistingHistory.Id))
                    {
                        paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);
                    }
                }
                else
                {
                    paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);
                }

               
                var get_app = new ResidentialAddResponceTemplateDa().GetApplicationFree(Obj.UnitId.ToString());
                var typeChck = get_app.FeeTypeCheck != null ? get_app.FeeTypeCheck : 0;
                var percentOrFlatVal = 0.0;
                var subtotal = Obj.PaidAmount != null ? Convert.ToDouble(Obj.PaidAmount) : 0;
                var nTotal = 0.0;

                if (get_app.IncludeProcessFees != null && Convert.ToBoolean(get_app.IncludeProcessFees) == true && Convert.ToBoolean(get_app.TanentPayFees) == false)
                {
                    if (typeChck == 1)
                    {
                        percentOrFlatVal = get_app.FeePercentageCheck != null ? Convert.ToDouble(get_app.FeePercentageCheck) : 0;
                        var nCharge = subtotal * percentOrFlatVal / 100;
                        nTotal = nCharge;
                    }
                    else
                    {
                        percentOrFlatVal = get_app.FeeFlatAmountCheck != null ? Convert.ToDouble(get_app.FeeFlatAmountCheck) : 0;
                        nTotal = percentOrFlatVal;
                    }
                }

                if (paymentHistoryId > 0)
                {
                    Obj.PaymentHistoryId = paymentHistoryId;
                    if (new TenantDashboardDA().InsertTenant_OnlineFee(Obj))
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }

                    if (nTotal > 0)
                    {
                        string sDelBillSQL = "delete from BillPayment where InvoiceNo = '" + paymentHistory.Serial + "'";
                        Utility.RunCMD(sDelBillSQL);
                        var achFee = new BillPayment()
                        {
                            RefId = paymentHistory.ToUser,
                            InvoiceNo = paymentHistory.Serial,
                            TransactionType = "ACH Process Fee",
                            AccountType = "COG",
                            Amount = Convert.ToDecimal(nTotal),
                            LedgerCode = "5040",
                            Debit = Convert.ToDecimal(nTotal),
                            Credit = 0,
                            CreateDate = DateTime.Now,
                            Remarks = "ach",
                            Status = paymentHistory.Status,
                            UnitId = paymentHistory.UnitNo,
                            UserType = paymentHistory.ToUserType.ToString(),
                            OwnerId = paymentHistory.ToUser,
                            Month = paymentHistory.Month,
                            Year = paymentHistory.Year
                        };
                        if (new BillPaymentDA(true, false).Insert(achFee))
                        {

                        }
                    }

                }
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string GetProcessFee(TenantOnlineFee obj)
        {
            var get_app = new ResidentialAddResponceTemplateDa().GetApplicationFree(obj.UnitId);
            var typeChck = get_app.FeeTypeCheck;
            var percentOrFlatVal = 0.0;
            var subtotal = obj.PaidAmount != null ? Convert.ToDouble(obj.PaidAmount) : 0;
            var nTotal = 0.0;
            
            if (get_app.TanentPayFees != null && Convert.ToBoolean(get_app.TanentPayFees) == true)
            {
                if (typeChck == 1)
                {
                    percentOrFlatVal = get_app.FeePercentageCheck != null ? Convert.ToDouble(get_app.FeePercentageCheck) : 0;
                    var nCharge = subtotal * percentOrFlatVal / 100;
                    nTotal = nCharge;
                }
                else
                {
                    percentOrFlatVal = get_app.FeeFlatAmountCheck != null ? Convert.ToDouble(get_app.FeeFlatAmountCheck) : 0;
                    nTotal = percentOrFlatVal;
                }
            }
           

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(nTotal);
            return json;
        }

    }
}