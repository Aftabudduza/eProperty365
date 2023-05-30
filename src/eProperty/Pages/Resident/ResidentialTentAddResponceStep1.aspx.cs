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
using System.Configuration;
using System.Security.Authentication;
using System.Net;
using PropertyService;
using PropertyService.ViewModel;
using System.Security.Cryptography;
using PropertyService.DA.Account;

namespace eProperty.Pages.Resident
{  
    public partial class ResidentialTentAddResponceStep1 : System.Web.UI.Page
    {
        public bool isView = true;
        public string api_loginID;
        public string utc_time = DateTime.Now.Ticks.ToString();
        public string pay_now_single_return_string;
        Gateway.Signature cliSignature = new Gateway.Signature();     // This is the worker object that takes the values and performs functions.
        public string return_string = "Initial";
        public string source = "";
        string strLoginID = System.Configuration.ConfigurationManager.AppSettings.Get("LoginID");
        string strSecureKey = System.Configuration.ConfigurationManager.AppSettings.Get("ProcessingKey");
        public string sTotal= "0";
        public string sApplicationFee = "0";
        public string sScreeningFee = "0";

        public string sScreeningType = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["TenentId"] = null;
                Session["ResidentialUnitSerial"] = null;
               
                string residentialId, tenantId;
                try
                {
                    residentialId = Request.QueryString["ResidentialUnitSerial"];
                }
                catch (Exception ex)
                {
                    residentialId = "";
                }
                try
                {
                    tenantId = Request.QueryString["TenentId"];
                }
                catch (Exception ex)
                {
                    tenantId = "";
                }
                if (Session["TenentId"] != null)
                {
                    tenantId = Session["TenentId"].ToString();
                }
                if (Session["ResidentialUnitSerial"] != null)
                {
                    residentialId = Session["ResidentialUnitSerial"].ToString();
                }

                if (residentialId != "" && tenantId != "")
                {
                    Session["TenentId"] = tenantId;
                    Session["ResidentialUnitSerial"] = residentialId;
                }

                if (residentialId != "" && tenantId != "" && residentialId != null && tenantId != null)
                {
                    var stepcount = new ResidentialAddResponceTemplateDa().GetStepCount((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    if (stepcount != null)
                    {
                        if (stepcount.Step == 1)
                        {
                            CheckBox1.Checked = true;
                        }
                        else if (stepcount.Step == 2)
                        {
                            CheckBox1.Checked = true;
                            CheckBox2.Checked = true;
                        }
                        else if (stepcount.Step == 3)
                        {
                            CheckBox1.Checked = true;
                            CheckBox2.Checked = true;
                            CheckBox3.Checked = true;
                        }
                        else if (stepcount.Step == 4)
                        {
                            CheckBox1.Checked = true;
                            CheckBox2.Checked = true;
                            CheckBox3.Checked = true;
                            CheckBox4.Checked = true;
                        }
                    }

                    var objResidentialUnit = new ResidentialUnitDa().GetbySerial((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    if (objResidentialUnit != null)
                    {
                        if (objResidentialUnit.IsBackgroundCheck != null)
                        {
                            sScreeningType = Convert.ToBoolean(objResidentialUnit.IsBackgroundCheck) == true ? "1" : "0";
                        }
                    }

                    var get_app = new ResidentialAddResponceTemplateDa().GetApplicationFree((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    decimal TotalFee = 0;
                    if(get_app != null)
                    {
                       // TotalFee = get_app.ApplicationFee != null ? Convert.ToDecimal(get_app.ApplicationFee) : 0;
                        sApplicationFee = get_app.ApplicationFee != null ? Convert.ToString(get_app.ApplicationFee) : "0";
                        sScreeningFee = get_app.ScreeningFee != null ? Convert.ToString(get_app.ScreeningFee) : "0";

                        if (objResidentialUnit != null)
                        {
                            if (objResidentialUnit.IsBackgroundCheck != null)
                            {
                                TotalFee = Convert.ToBoolean(objResidentialUnit.IsBackgroundCheck) == true ? (Convert.ToDecimal(sApplicationFee) + Convert.ToDecimal(sScreeningFee)) : Convert.ToDecimal(sApplicationFee);
                            }
                            else
                            {
                                TotalFee = get_app.ApplicationFee != null ? Convert.ToDecimal(get_app.ApplicationFee) : 0;
                            }
                        }
                        else
                        {
                            TotalFee = get_app.ApplicationFee != null ? Convert.ToDecimal(get_app.ApplicationFee) : 0;
                        }
                    }
                        
                    sTotal = Convert.ToDecimal(TotalFee).ToString();

                }               

                api_loginID = strLoginID;

                var isAdmin = false;
                if (Session["UserObject"] != null)
                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                        ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                        : false;

                if (Session["UserType"] != null)
                {
                    if (isAdmin == true || Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3")
                    {                      
                        isView = false;
                    }
                }


            }

         
            cliSignature.api_login_id = strLoginID;            //Add your API login ID.
            cliSignature.secure_trans_key = strSecureKey;    //Add your secure transaction key.

            //cliSignature.api_login_id = "3nmC763bPB";            //Add your API login ID.
            //cliSignature.secure_trans_key = "WsC8h0p813JuQ";    //Add your secure transaction key.

            //if (!string.IsNullOrEmpty(hdTotalAmount.Value))
            //{
            //    sTotal = hdTotalAmount.Value.Trim();
            //}

            cliSignature.pay_now_single_payment = cliSignature.api_login_id + "|sale|1.0|" + sTotal + "|" + utc_time + "|A1234||";
            api_loginID = cliSignature.api_login_id;
            pay_now_single_return_string = Gateway.EndPoint(cliSignature, "CREATESIGNATUREPAYSINGLEAMOUNT");
         
        }      
        [WebMethod(EnableSession = true)]      
        public static string Save(Residential_Tenant_Application_Step1 obj)
        {
            bool nextStep = true;

            if (obj != null)
            {
                try
                {                   
                    obj.Serial = HttpContext.Current.Session["TenentId"].ToString();
                    obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

                    if(obj.AccountType == "Check")
                    {
                        if (obj.CheckingAccountNumber != string.Empty && obj.CheckingAccountNumber.Length > 4)
                        {
                            obj.CheckingAccountNumber = obj.CheckingAccountNumber.Substring(obj.CheckingAccountNumber.Length - 4, 4);
                        }
                    }
                    else if(obj.AccountType == "Credit")
                    {
                        if (obj.CreditCardNumber != string.Empty && obj.CreditCardNumber.Length > 4)
                        {
                            obj.CreditCardNumber = obj.CreditCardNumber.Substring(obj.CreditCardNumber.Length - 4, 4);
                        }
                    }

                    List<Get_TenantInformation_Result> tenObjInformation = new ResidentialAddResponceTemplateDa().GetTenantInformation(obj.Serial);

                    string sOwnerId = tenObjInformation[0] != null ? tenObjInformation[0].OwnerId : "";
                    
                    //OwnerProfile objOwner = new AdminOwnerProfileDA().GetByUserName(sOwnerId);
                    //int nOwnerId = objOwner != null ? objOwner.Id : 0;

                    int sPackage = sOwnerId.Length > 1 ? Convert.ToInt32(sOwnerId.Substring(1)) : 1;

                    string sNumber =  sPackage.ToString().PadLeft(4, '0').ToString();

                    string sPrefix = "I" + sNumber;

                    var paymentHistory = new PropertyService.BO.TenantPaymentHistory()
                    {
                        FromUser = obj.Serial,
                        // FromUserType = tenObjInformation[0] != null? Convert.ToInt32(tenObjInformation[0].FromUserType) : 5,                       
                        ToUser = tenObjInformation[0] != null ? tenObjInformation[0].OwnerId : "",
                        //ToUserType = tenObjInformation[0] != null ? Convert.ToInt32(tenObjInformation[0].ToUserType) : 2,
                        FromUserType = 5,
                        ToUserType = 2,
                        UnitNo = obj.ResidentialUnitSerialId,
                        Amount = Convert.ToDecimal(obj.TotalApplicationFree),
                        AccountName = obj.NameOfAccount,
                        Address = obj.Address,
                        Address1 = obj.Address,
                        City = obj.City,
                        State = obj.State,
                        Zip = obj.ZipCode,
                        CardNumber = obj.CreditCardNumber,
                        CVS = obj.CVSNumber,
                        Month = DateTime.Now.Month.ToString(),
                        Year = DateTime.Now.Year.ToString(),
                        LastFourDigitCard = obj.CreditCardNumber,
                        IsCheckingAccount = obj.AccountType.Equals("Check") ? true : false,
                        RoutingNo = obj.RoutingNumber,
                        AccountNo = obj.CheckingAccountNumber,
                        CheckNo = "",
                        AccountType = obj.AccountType,
                        AuthorizationCode = obj.AuthorizationCode,
                        TransactionCode = obj.TransactionCode,
                        TransactionDescription = obj.TransactionDescription,
                        Getway = "Residential Tenant And Responce Step1",
                        DebitAmount = 0,
                        CreditAmount = Convert.ToDecimal(obj.TotalApplicationFree),
                        CreateDate = DateTime.Now,
                        Status = "complete",
                        Serial = new ResidentialAddResponceTemplateDa().MakeAutoGenSerialForPaymentHistory(sPrefix, "Invoice"),
                        TransactionType = "ApplicationFee",
                        LedgerCode = "4060",
                        Remarks = ""
                    };

                    int paymentHistoryId = 0;
                    var get_app = new ResidentialAddResponceTemplateDa().GetApplicationFree(obj.ResidentialUnitSerialId.ToString());
                    var typeChck = get_app.FeeTypeCheck != null ? get_app.FeeTypeCheck : 0;
                    var percentOrFlatVal = 0.0;
                    var subtotal = obj.TotalApplicationFree != null ? Convert.ToDouble(obj.TotalApplicationFree) : 0;
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

                    try
                    {
                        if (obj.Id > 0)
                        {
                            var oldObj = new ResidentialAddResponceTemplateDa().GetTenantApp1ById(Convert.ToInt32(obj.Id));
                            obj.CreatedDate = oldObj.CreatedDate;
                            obj.Updateddate = DateTime.Now;

                            var ExistingHistory = new ResidentialAddResponceTemplateDa().GetTenantPaymentHistory(obj.ResidentialUnitSerialId, obj.Serial, "Residential Tenant And Responce Step1");
                            if (ExistingHistory != null)
                            {
                                if (new ResidentialAddResponceTemplateDa().DeletePaymentHistoryById(ExistingHistory.Id))
                                {
                                    paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);
                                }

                                if (paymentHistoryId > 0)
                                {
                                    obj.PaymentHistoryId = paymentHistoryId;
                                    if (new ResidentialAddResponceTemplateDa().Update_step1(obj))
                                    {
                                        if (new ResidentialAddResponceTemplateDa().UpdateTenantStatus(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), HttpContext.Current.Session["TenentId"].ToString(), "Started"))
                                        {
                                        }
                                    }
                                }

                            }
                            else
                            {
                                paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);
                                if (paymentHistoryId > 0)
                                {
                                    obj.PaymentHistoryId = paymentHistoryId;
                                    if (new ResidentialAddResponceTemplateDa().Update_step1(obj))
                                    {
                                        if (new ResidentialAddResponceTemplateDa().UpdateTenantStatus(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), HttpContext.Current.Session["TenentId"].ToString(), "Started"))
                                        {
                                        }
                                    }

                                }
                            }

                        }
                        else
                        {
                            obj.CreatedDate = DateTime.Now;

                            paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);

                            if (paymentHistoryId > 0)
                            {
                                obj.PaymentHistoryId = paymentHistoryId;
                                if (new ResidentialAddResponceTemplateDa().Insert_Step1(obj))
                                {
                                    if (new ResidentialAddResponceTemplateDa().UpdateTenantStatus(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), HttpContext.Current.Session["TenentId"].ToString(), "Started"))
                                    {
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }                  


                    try
                    {
                        if (paymentHistoryId > 0)
                        {
                            string sOwnerTransactionNo = new FinancialTransactionDA().MakeAutoGenSerial("F", "Account");

                            AccountChart objAccount = new AddChartofAccountDA().GetAccountTypeByLedgerCode(paymentHistory.LedgerCode);
                            decimal nAmount = 0;

                            nAmount = paymentHistory.Amount != null ? Convert.ToDecimal(paymentHistory.Amount) : 0;

                            if (nAmount > 0)
                            {
                                var FinTranDebit = new FinancialTransaction()
                                {
                                    Serial = sOwnerTransactionNo,
                                    AccountType = "Asset",
                                    LedgerCode = "1010",
                                    InvoiceNo = paymentHistory.Serial,
                                    RefId = paymentHistory.FromUser,
                                    Amount = nAmount,
                                    Debit = nAmount,
                                    Credit = 0,
                                    CreateDate = DateTime.Now,
                                    EntryType = "Debit",
                                    Remarks = paymentHistory.TransactionType
                                };
                                var FinTranCredit = new FinancialTransaction()
                                {
                                    Serial = sOwnerTransactionNo,
                                    AccountType = "Inc",
                                    LedgerCode = paymentHistory.LedgerCode,
                                    InvoiceNo = paymentHistory.Serial,
                                    RefId = paymentHistory.FromUser,
                                    Amount = nAmount,
                                    Debit = 0,
                                    Credit = nAmount,
                                    CreateDate = DateTime.Now,
                                    EntryType = "Credit",
                                    Remarks = paymentHistory.TransactionType
                                };

                                if (new FinancialTransactionDA(true, false).Insert(FinTranDebit))
                                {
                                    if (new FinancialTransactionDA(true, false).Insert(FinTranCredit))
                                    {

                                    }
                                }

                            }
                        }
                    }
                    catch(Exception ex)
                    {

                    }                    
                    

                    if (nTotal > 0)
                    {
                        string sDelBillSQL = "delete from BillPayment where InvoiceNo = '" + paymentHistory.Serial + "'";                       
                        Utility.RunCMD(sDelBillSQL);

                        try
                        {
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
                        catch (Exception ex)
                        {
                            
                        }                       
                    }

                }
                catch (Exception ex)
                {
                    nextStep = false;
                }
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(nextStep);
            return json;
        }        
        [WebMethod(EnableSession = true)]
        public static string Continue(Residential_Tenant_Application_Step1 obj)
        {
            var nextStep = true;
            if (obj.Id ==0)
            {
                 nextStep = new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");
            }
          
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(nextStep);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string Load(Residential_Tenant_Application_Step1 Obj)
        {
            var getobj =
                new ResidentialAddResponceTemplateDa().GetTenantApp1ByUnitAndTenantId(
                    (HttpContext.Current.Session["TenentId"]).ToString(),
                    (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var get_app = new ResidentialAddResponceTemplateDa().GetApplicationFree((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
            // var TotalFee = get_app.ApplicationFee + get_app.ScreeningFee;
            //sTotal = TotalFee != null ? Convert.ToDecimal(TotalFee).ToString() : "1";
            var objResidentialUnit = new ResidentialUnitDa().GetbySerial((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            decimal TotalFee = 0;
            if (get_app != null)
            {
               string sApplicationFee = get_app.ApplicationFee != null ? Convert.ToString(get_app.ApplicationFee) : "0";
               string sScreeningFee = get_app.ScreeningFee != null ? Convert.ToString(get_app.ScreeningFee) : "0";

                if (objResidentialUnit != null)
                {
                    if (objResidentialUnit.IsBackgroundCheck != null)
                    {
                        TotalFee = Convert.ToBoolean(objResidentialUnit.IsBackgroundCheck) == true ? (Convert.ToDecimal(sApplicationFee) + Convert.ToDecimal(sScreeningFee)) : Convert.ToDecimal(sApplicationFee);
                    }
                    else
                    {
                        TotalFee = get_app.ApplicationFee != null ? Convert.ToDecimal(get_app.ApplicationFee) : 0;
                    }
                }
                else
                {
                    TotalFee = get_app.ApplicationFee != null ? Convert.ToDecimal(get_app.ApplicationFee) : 0;
                }
                // TotalFee = (get_app.ApplicationFee != null ? Convert.ToDecimal(get_app.ApplicationFee) : 0) + (get_app.ScreeningFee != null ? Convert.ToDecimal(get_app.ScreeningFee) : 0);
            }           

            List<dynamic> dyOldAllObject = new List<dynamic>();
            dyOldAllObject.Add(new { TotalFee = TotalFee, getobj= getobj }); 
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(dyOldAllObject);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string LoadApplicationFree(Residential_Tenant_Application_Step1 Obj)
        {
            //GetApplicationFree

            var getobj =
               new ResidentialAddResponceTemplateDa().GetApplicationFree((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
            var objResidentialUnit = new ResidentialUnitDa().GetbySerial((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
            // var TotalFee = getobj.ApplicationFee + getobj.ScreeningFee;
            decimal TotalFee = 0;
            if (getobj != null)
            {
                string sApplicationFee = getobj.ApplicationFee != null ? Convert.ToString(getobj.ApplicationFee) : "0";
                string sScreeningFee = getobj.ScreeningFee != null ? Convert.ToString(getobj.ScreeningFee) : "0";

                if (objResidentialUnit != null)
                {
                    if (objResidentialUnit.IsBackgroundCheck != null)
                    {
                        TotalFee = Convert.ToBoolean(objResidentialUnit.IsBackgroundCheck) == true ? (Convert.ToDecimal(sApplicationFee) + Convert.ToDecimal(sScreeningFee)) : Convert.ToDecimal(sApplicationFee);
                    }
                    else
                    {
                        TotalFee = getobj.ApplicationFee != null ? Convert.ToDecimal(getobj.ApplicationFee) : 0;
                    }
                }
                else
                {
                    TotalFee = getobj.ApplicationFee != null ? Convert.ToDecimal(getobj.ApplicationFee) : 0;
                }

                //TotalFee = (getobj.ApplicationFee != null ? Convert.ToDecimal(getobj.ApplicationFee) : 0) + (getobj.ScreeningFee != null ? Convert.ToDecimal(getobj.ScreeningFee) : 0);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TotalFee);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string GetApprovalCode()
        {
            var TenantList = new ResidentialAddResponceTemplateDa().GetApprovalCode_Step1(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), (HttpContext.Current.Session["TenentId"]).ToString());
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantList);
            return json;
        }
        
    }

}

