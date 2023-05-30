using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.DA.Account;
using PropertyService.Enums;
using PropertyService.Admin.DA;

namespace eProperty.Pages.Resident
{
    public partial class PayACHFee : System.Web.UI.Page
    {
        string strLoginID = System.Configuration.ConfigurationManager.AppSettings.Get("LoginID");
        string strSecureKey = System.Configuration.ConfigurationManager.AppSettings.Get("ProcessingKey");
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                    if (Session["OwnerId"] != null)
                    {
                        List<BillPayment> obj = null;
                        obj = new BillPaymentDA().GetByUserId(Session["OwnerId"].ToString());
                        gvLocation.DataSource = obj;
                        gvLocation.DataBind();
                    }

                    string sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? ((UserProfile)Session["UserObject"]).OwnerId : "";
                    //OwnerProfile objOwner = new AdminOwnerProfileDA().GetByUserName(sOwnerId);
                    //int nOwnerId = objOwner != null ? objOwner.Id : 0;
                    //string sNumber = nOwnerId.ToString().PadLeft(4, '0');

                    int sPackage = sOwnerId.Length > 1 ? Convert.ToInt32(sOwnerId.Substring(1)) : 1;
                    string sNumber = sPackage.ToString().PadLeft(4, '0').ToString();
                    string sPrefix = "I" + sNumber;

                    string sInvoice = new ResidentialAddResponceTemplateDa().MakeAutoGenSerialForPaymentHistory(sPrefix, "Invoice");

                    //Set utcTime, store on page in hidden field
                    page_pg_utc_time.Value = DateTime.UtcNow.Ticks.ToString();

                    page_pg_transaction_order_number.Value = sInvoice;


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

                        if (Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3")
                        {
                            if (Session["HasCompletedFullProfile"] != null)
                            {
                                if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == true)
                                {
                                    lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                                       "/Pages/DashboardAdmin.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                                }
                                else
                                {
                                    lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                                       "/Pages/DashboardOwner.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                                }
                            }
                        }

                        else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16)EnumUserType.Resident)
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
                        else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16)EnumUserType.SalesPartner)
                        {
                            lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                              "/Pages/Account/SalesPartnerDealerDashboard.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                        }
                        else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16)EnumUserType.Dealer)
                        {
                            lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                              "/Pages/Account/SalesPartnerDealerDashboard.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
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
        }

        #region Events Support
        protected void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["OwnerId"] != null)
                {
                    var objPaymentInfo = new PaymentInformationDA().GetCheckingAccountByOwner(Session["OwnerId"].ToString());
                    var objOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());

                    if(objOwner != null && objPaymentInfo != null)
                    {
                        var paymentHistory = new PropertyService.BO.TenantPaymentHistory()
                        {
                            FromUser = Session["OwnerId"].ToString(),
                            FromUserType = 2,
                            ToUser = "EProperty",
                            ToUserType = 1,
                            UnitNo = "",
                            Amount = Convert.ToDecimal(txtPaid.Text.ToString()),
                            AccountName = objPaymentInfo.AccountName,
                            Address = objOwner.Address,
                            Address1 = objOwner.Address1,
                            City = objOwner.City,
                            State = objOwner.State,
                            Zip = objOwner.Zip,
                            CardNumber = "",
                            CVS = "",
                            Month = DateTime.Now.Month.ToString(),
                            Year = DateTime.Now.Year.ToString(),
                            LastFourDigitCard = "",
                            IsCheckingAccount = true,
                            RoutingNo = objPaymentInfo.RoutingNo,
                            AccountNo = objPaymentInfo.AccountNo.Length > 4 ? objPaymentInfo.AccountNo.Substring(objPaymentInfo.AccountNo.Length - 4, 4) : objPaymentInfo.AccountNo,
                            CheckNo = "",
                            AccountType = "Check",
                            AuthorizationCode = "",
                            TransactionCode = "",
                            TransactionDescription = "",
                            Getway = ddlType.SelectedItem.Text,
                            DebitAmount = 0,
                            CreditAmount = Convert.ToDecimal(txtPaid.Text.ToString()),
                            CreateDate = DateTime.Now,
                            Status = "pending",
                            Serial = page_pg_transaction_order_number.Value,
                            TransactionType = ddlType.SelectedItem.Text,
                            LedgerCode = ddlType.SelectedValue,
                            Remarks = ddlType.SelectedValue == "5040" ? "ach" : "exp"
                        };

                        Session["paymentHistory"] = paymentHistory;

                        ProcessTransaction(objOwner, objPaymentInfo);

                        //int paymentHistoryId = 0;

                        //paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);

                        //if (paymentHistoryId > 0)
                        //{
                        //    ProcessTransaction(objOwner, objPaymentInfo);
                        //}
                    }
                    else
                    {
                        Utility.DisplayMsg("Payment Information Not Found !!!", this);
                    }
                    
                }
            }
            catch (Exception ex)
            { }
        }
        protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocation.PageIndex = e.NewPageIndex;

            string sOwnerId = "";
            string sLedger = "";
            sOwnerId = Session["OwnerId"] != null ? Session["OwnerId"].ToString() : "";
            sLedger = ddlType.SelectedValue != "-1" ? ddlType.SelectedValue : "";

            FillTransactionList(sOwnerId, sLedger);

        }
        protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sOwnerId = "";
            string sLedger = "";
            sOwnerId = Session["OwnerId"] != null ? Session["OwnerId"].ToString() : "";
            sLedger = ddlType.SelectedValue != "-1" ? ddlType.SelectedValue : "";

            FillTransactionList(sOwnerId, sLedger);
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string sOwnerId = "";
                string sLedger = "";
                sOwnerId = Session["OwnerId"] != null ? Session["OwnerId"].ToString() : "";
                sLedger = ddlType.SelectedValue != "-1" ? ddlType.SelectedValue : "";

                FillTransactionList(sOwnerId, sLedger);

            }
            catch (Exception)
            {

            }
        }
       
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            string sOwnerId = "";
            string sLedger = "";
            sOwnerId = Session["OwnerId"] != null ? Session["OwnerId"].ToString() : "";
            sLedger = ddlType.SelectedValue != "-1" ? ddlType.SelectedValue : "";

            FillTransactionList(sOwnerId, sLedger);
        }
        #endregion

        #region Method Support
        public void FillTransactionList(string owner, string ledger)
        {
            try
            {
                List<BillPayment> obj = null;
                obj = new BillPaymentDA().GetByLedgerCode(owner, ledger);
                gvLocation.DataSource = obj;
                gvLocation.DataBind();


                decimal nDebit = 0, nCredit = 0, nBalance = 0;

                if (obj != null && obj.Count > 0)
                {
                    foreach (BillPayment bill in obj)
                    {
                        nDebit = nDebit + (bill.Debit != null ? Convert.ToDecimal(bill.Debit) : 0);
                        nCredit = nCredit + (bill.Credit != null ? Convert.ToDecimal(bill.Credit) : 0);
                    }
                }

                nBalance = nDebit - nCredit;

                txtDue.Text = nBalance.ToString("#.00");

            }
            catch (Exception ex)
            {
            }

        }

        public void ProcessTransaction(OwnerProfile objOwner, PaymentInformation objPaymentInfo)
        {
            try
            {
                string apiLoginId;
                string returnURL;
                string continueURL;
                string secTranKey;
                string versionNumber;

                //Enter your values here:
                //apiLoginId = "U0NOp0o43e";  //Login ID from PaymentsGateway SWP admin
                //secTranKey = "74p6B5QhUgI"; //SecureTransKey in ClearText

                returnURL = Utility.WebUrl + "/Pages/Resident/ThankYouOrder.aspx?o=1"; //Optional: must match URL set in PaymentsGateway.net admin settings, page should have script to read http stream
                continueURL = Utility.WebUrl + "/Pages/Resident/ThankYouOrder.aspx?o=1"; //Optional: used only if returnURL is not set. Page to return to from SWP confirmation screen

                versionNumber = "1.0"; //SWP version number, per documentation currently 1.0

                apiLoginId = strLoginID;
                secTranKey = strSecureKey;

                //Begin building page stream to post to SWP
                RemotePost myremotepost = new RemotePost();
                myremotepost.Url = "https://swp.paymentsgateway.net/co/default.aspx";
                //myremotepost.Url = "https://sandbox.paymentsgateway.net/swp/co/default.aspx";
                myremotepost.Add("pg_api_login_id", apiLoginId);
                myremotepost.Add("pg_return_url", returnURL);
                myremotepost.Add("pg_utc_time", page_pg_utc_time.Value);
                myremotepost.Add("pg_transaction_order_number", page_pg_transaction_order_number.Value);
                myremotepost.Add("pg_continue_url", continueURL);

                //Generate Hash Value
                string hash;
                hash = generateHash(secTranKey, apiLoginId, "20", versionNumber, txtPaid.Text, page_pg_utc_time.Value, page_pg_transaction_order_number.Value);
                myremotepost.Add("pg_ts_hash", hash);

                string email = "", phone = "";
                if (Session["UserObject"] != null)
                {
                    email = ((UserProfile)Session["UserObject"]).Email != null ? ((UserProfile)Session["UserObject"]).Email : "";
                    phone = ((UserProfile)Session["UserObject"]).Phone != null ? ((UserProfile)Session["UserObject"]).Phone : "";
                }
                //Required Values-- from aspx page fields
                //Note- transaction type options are Credit Card Sale or Check Sale only
                //Other types (Credits & Auths) could be added using a control that stores the appropriate values, as listed in the SWP Integration Guide
                //Valid values as of 2009-02-26: 10=CC Sale, 13=CC Credit, 20=eCheck Sale, 23=eCheck Credit, 11=CC Auth, 21=eChech Auth
                myremotepost.Add("pg_transaction_type", "20");
                myremotepost.Add("pg_version_number", versionNumber);
                myremotepost.Add("pg_total_amount", txtPaid.Text);
                myremotepost.Add("ecom_payment_check_account_type", "C");

                myremotepost.Add("ecom_payment_check_trn", objPaymentInfo.RoutingNo);
                myremotepost.Add("ecom_payment_check_account", objPaymentInfo.AccountNo);

                //live 
                myremotepost.Add("pg_billto_postal_name_company", objOwner.CompanyName);
                myremotepost.Add("pg_billto_postal_name_first", objOwner.FirstName);
                myremotepost.Add("pg_billto_postal_name_last", objOwner.LastName);
                myremotepost.Add("pg_billto_postal_street_line1", objOwner.Address);
                myremotepost.Add("pg_billto_postal_street_line2", objOwner.Address1);
                myremotepost.Add("pg_billto_postal_city", objOwner.City);
                myremotepost.Add("pg_billto_postal_stateprov", objOwner.State);
                myremotepost.Add("pg_billto_postal_postalcode", objOwner.Zip);

                if (!string.IsNullOrEmpty(email))
                {
                    myremotepost.Add("pg_billto_online_email", email);
                }
                if (!string.IsNullOrEmpty(phone))
                {
                    myremotepost.Add("pg_billto_telecom_phone_number", phone);
                }               

                myremotepost.Add("pg_shipto_postal_name", objPaymentInfo.AccountName);
                myremotepost.Add("pg_shipto_postal_street_line1", objOwner.Address);
                myremotepost.Add("pg_shipto_postal_street_line2", objOwner.Address1);
                myremotepost.Add("pg_shipto_postal_city", objOwner.City);
                myremotepost.Add("pg_shipto_postal_stateprov", objOwner.State);
                myremotepost.Add("pg_shipto_postal_postalcode", objOwner.Zip);

                myremotepost.Post();
            }
            catch(Exception ex3)
            {

            }
        }
        private string generateHash(string secTranKey, string apiLoginID, string transactionType, string versionNumber, string totalAmount, string utcTime, string transactionOrderNumber)
        {
            //Build Send String
            string send;
            send = apiLoginID + "|" + transactionType + "|" + versionNumber + "|" + totalAmount + "|" + utcTime + "|" + transactionOrderNumber;

            //Generate and return the hash value
            GenerateHashCode.HashCode objHashkey = new GenerateHashCode.HashCode();
            return objHashkey.GenerateHash(send, secTranKey).ToString();

        }

        public class RemotePost
        {
            //Thanks to Jigar Desai's article at C-Sharp Corner for this class 
            //http://www.c-sharpcorner.com/UploadFile/desaijm/ASP.NetPostURL11282005005516AM/ASP.NetPostURL.aspx
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();

            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public void Post()
            {
                System.Web.HttpContext.Current.Response.Clear();

                System.Web.HttpContext.Current.Response.Write("<html><head>");

                System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                System.Web.HttpContext.Current.Response.Write("</form>");
                System.Web.HttpContext.Current.Response.Write("</body></html>");
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
            }
        }

        #endregion


    }
}