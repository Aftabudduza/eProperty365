using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.DA.Account;
using PropertyService.Enums;

namespace eProperty.Pages.Resident
{
    public partial class ApproveEPropertyFinancialList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                    FillUsers(2);
                    List<PaymentHistory> obj = null;
                    obj = new AdminPaymentHistoryDA().GetUnApprovedPaymentHistory("pending");
                    gvLocation.DataSource = obj;
                    gvLocation.DataBind();
                }
            }
        }       

        #region Events Support
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sUserId = "";
                string sLedger = "";
                sUserId = ddlUser.SelectedValue != "-1" ? ddlUser.SelectedValue : "";
                sLedger = ddlType.SelectedValue != null ? ddlType.SelectedValue : "";

                FillTransactionList(sUserId, sLedger, "pending");
            }
            catch (Exception ex)
            { }
        }
        protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocation.PageIndex = e.NewPageIndex;

            string sUserId = "";
            string sLedger = "";
            sUserId = ddlUser.SelectedValue != "-1" ? ddlUser.SelectedValue : "";
            sLedger = ddlType.SelectedValue != null ? ddlType.SelectedValue : "";

            FillTransactionList(sUserId, sLedger, "pending");

        }
        protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sUserId = "";
            string sLedger = "";
            sUserId = ddlUser.SelectedValue != "-1" ? ddlUser.SelectedValue : "";
            sLedger = ddlType.SelectedValue != null ? ddlType.SelectedValue : "";

            FillTransactionList(sUserId, sLedger, "pending");
        }
        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string sUserId = "";
                string sLedger = "";
                sUserId = ddlUser.SelectedValue != "-1" ? ddlUser.SelectedValue : "";

                FillTransactionList(sUserId,  sLedger, "pending");

            }
            catch (Exception)
            {

            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string sUserId = "";
                string sLedger = "";
                sUserId = ddlUser.SelectedValue != "-1" ? ddlUser.SelectedValue : "";
                sLedger = ddlType.SelectedValue != null ? ddlType.SelectedValue : "";

                FillTransactionList(sUserId, sLedger, "pending");

            }
            catch (Exception)
            {

            }
        }

        protected void rdoUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoUserType.SelectedItem.Value != null)
            {
                FillUsers(Convert.ToInt32(rdoUserType.SelectedItem.Value));

                ddlType.Items.Clear();
                ddlType.AppendDataBoundItems = true;

                if (rdoUserType.SelectedItem.Value == "9")
                {
                    ddlType.Items.Add(new ListItem("Sales Commission (4010)", "4010"));
                }
                else if (rdoUserType.SelectedItem.Value == "10")
                {
                    ddlType.Items.Add(new ListItem("Sales Commission (4010)", "4010"));
                }
                else if (rdoUserType.SelectedItem.Value == "2")
                {
                    ddlType.Items.Add(new ListItem("Security Fee (1040)", "1040"));
                    ddlType.Items.Add(new ListItem("Application Fee (4060)", "4060"));
                    ddlType.Items.Add(new ListItem("Sales Commission (4010)", "4010"));
                }

                ddlType.DataBind();
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            if (row != null)
            {
                Label hdApplicationId = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblAppId");
                if (!string.IsNullOrEmpty(hdApplicationId.Text))
                {
                    hdnAppId.Value = hdApplicationId.Text.ToString();
                }

                var ExistingHistory = new AdminPaymentHistoryDA().GetBySerial(hdApplicationId.Text.ToString());

                if (ExistingHistory != null)
                {
                    ExistingHistory.Status = "complete";
                    if(new AdminPaymentHistoryDA().Update(ExistingHistory))
                    {
                        ApproveTransaction(ExistingHistory);
                        string sUserId = "";
                        string sLedger = "";
                        sUserId = ddlUser.SelectedValue != "-1" ? ddlUser.SelectedValue : "";
                        sLedger = ddlType.SelectedValue != null ? ddlType.SelectedValue : "";

                        FillTransactionList(sUserId, sLedger, "pending");
                        Utility.DisplayMsg("Transaction successful !!", this);
                    }

                }

            }
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            if (row != null)
            {
                Label hdApplicationId = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblAppId");
                if (!string.IsNullOrEmpty(hdApplicationId.Text))
                {
                    hdnAppId.Value = hdApplicationId.Text.ToString();
                }              

                var ExistingHistory = new AdminPaymentHistoryDA().GetBySerial(hdApplicationId.Text.ToString());

                if (ExistingHistory != null)
                {
                    ExistingHistory.Status = "decline";
                    if (new AdminPaymentHistoryDA().Update(ExistingHistory))
                    {
                        string sUserId = "";
                        string sLedger = "";
                        sUserId = ddlUser.SelectedValue != "-1" ? ddlUser.SelectedValue : "";
                        sLedger = ddlType.SelectedValue != null ? ddlType.SelectedValue : "";
                        FillTransactionList(sUserId, sLedger, "pending");

                        Utility.DisplayMsg("Transaction Rejected !!", this);
                    }
                    
                }

            }
        }
        #endregion

        #region Method Support
        private void FillUsers(int nType)
        {
            try
            {
                ddlUser.Items.Clear();
                ddlUser.AppendDataBoundItems = true;
                ddlUser.Items.Add(new ListItem("Select User", "-1"));

                if (nType == 9 || nType == 10)
                {
                    List<Dealer_SalesPartner> objDealers = new SalesPartnerDealerDashboardDA().GetAllSalesPartnerDealer(nType).ToList();
                    if (objDealers != null && objDealers.Count > 0)
                    {
                        foreach (Dealer_SalesPartner obj in objDealers)
                        {
                            string sName = obj.firstName.ToString() + " " + obj.lastName.ToString() + " (" + obj.serialCode.ToString() + ")";
                            ddlUser.Items.Add(new ListItem(sName, obj.serialCode.ToString()));
                        }
                    }
                }
                else
                {
                    List<OwnerProfile> objOwners = new AdminOwnerProfileDA().GetAllOwnersInfo();
                    if (objOwners != null && objOwners.Count > 0)
                    {
                        foreach (OwnerProfile obj in objOwners)
                        {
                           // string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                            string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString() + " (" + obj.Serial.ToString() + ")";
                            ddlUser.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                        }
                    }
                }

                ddlUser.DataBind();
            }
            catch (Exception ex)
            {

            }

        }
        public void FillTransactionList(string userId, string ledgerCode, string status)
        {
            try
            {
                List<PaymentHistory> obj = null;
                obj = new AdminPaymentHistoryDA().GetUnApprovedPaymentHistory(userId, ledgerCode, status);
                gvLocation.DataSource = obj;
                gvLocation.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        private void ApproveTransaction(PaymentHistory objPaymentHistory)
        {
            try
            {
                decimal nAmount = 0;
                string sLedgerCode = objPaymentHistory.LedgerCode != null? objPaymentHistory.LedgerCode : "";
                string sNote = objPaymentHistory.Remarks != null ? objPaymentHistory.Remarks : "";
                string sUnit = objPaymentHistory.UnitNo != null ? objPaymentHistory.UnitNo : "";              
                string sInvoice = objPaymentHistory.Serial != null ? objPaymentHistory.Serial : "";
                string sUserType = objPaymentHistory.ToUserType != null ? objPaymentHistory.ToUserType.ToString() : "";
                string sPayorId = objPaymentHistory.ToUser != null ? objPaymentHistory.ToUser : "EProperty";

                try
                {
                    string sDelFinSQL = "delete from FinancialTransaction where InvoiceNo = '" + sInvoice + "'";
                    string sDelUserSQL = "delete from UserCommission where InvoiceNo = '" + sInvoice + "'";

                    Utility.RunCMDMain(sDelUserSQL);

                    Utility.RunCMDMain(sDelFinSQL);  
                                     
                    Utility.RunCMD(sDelFinSQL);
                }
                catch(Exception ex)
                {

                }

                nAmount = objPaymentHistory.Amount != null ? Convert.ToDecimal(objPaymentHistory.Amount) : 0;

                string sTransactionNo = new AdminFinancialTransactionDA().MakeAutoGenSerial("F", "Account");

                string sOwnerTransactionNo = new FinancialTransactionDA().MakeAutoGenSerial("F", "Account");

                AccountChart objAccount = new AddChartofAccountDA().GetAccountTypeByLedgerCode(sLedgerCode);
              
               

                if (sLedgerCode != string.Empty)
                {
                    if (nAmount > 0)
                    {
                        var GlobalFinTranDebit = new FinancialTransaction()
                        {
                            Serial = sTransactionNo,
                            AccountType = "Liab",
                            LedgerCode = sLedgerCode,
                            InvoiceNo = sInvoice,
                            RefId = sPayorId,
                            Amount = nAmount,
                            Debit = nAmount,
                            Credit = 0,
                            CreateDate = DateTime.Now,
                            EntryType = "Debit",
                            Remarks = objPaymentHistory.TransactionType
                        };

                        var GlobalFinTranCredit = new FinancialTransaction()
                        {
                            Serial = sTransactionNo,
                            AccountType = "Asset",
                            LedgerCode = "1010",
                            InvoiceNo = sInvoice,
                            RefId = sPayorId,
                            Amount = nAmount,
                            Debit = 0,
                            Credit = nAmount,
                            CreateDate = DateTime.Now,
                            EntryType = "Credit",
                            Remarks = objPaymentHistory.TransactionType
                        };

                        if (new AdminFinancialTransactionDA(true, false).Insert(GlobalFinTranDebit))
                        {
                            if (new AdminFinancialTransactionDA(true, false).Insert(GlobalFinTranCredit))
                            {

                            }
                        }

                    }
                    

                    if (sUserType == "2")  //OwnerDB Transaction
                    {
                        if(nAmount > 0)
                        {
                            var FinTranDebit = new FinancialTransaction()
                            {
                                Serial = sOwnerTransactionNo,
                                AccountType = "Asset",
                                LedgerCode = "1010",
                                InvoiceNo = sInvoice,
                                RefId = sPayorId,
                                Amount = nAmount,
                                Debit = nAmount,
                                Credit = 0,
                                CreateDate = DateTime.Now,
                                EntryType = "Debit",
                                Remarks = objPaymentHistory.TransactionType
                            };
                            var FinTranCredit = new FinancialTransaction()
                            {
                                Serial = sOwnerTransactionNo,
                                AccountType = "Inc",
                                LedgerCode = sLedgerCode,
                                InvoiceNo = sInvoice,
                                RefId = sPayorId,
                                Amount = nAmount,
                                Debit = 0,
                                Credit = nAmount,
                                CreateDate = DateTime.Now,
                                EntryType = "Credit",
                                Remarks = objPaymentHistory.TransactionType
                            };

                            if (new FinancialTransactionDA(true, false).Insert(FinTranDebit))
                            {
                                if (new FinancialTransactionDA(true, false).Insert(FinTranCredit))
                                {

                                }
                            }

                        }                      

                    }

                    if (sLedgerCode == "1040" || sLedgerCode == "4060" || sLedgerCode == "4010")  //admin user commission
                    {
                        string sTransactionType = "";
                        if(sUserType == "9")
                        {
                            sTransactionType = "Sales Partner Commission";
                        }
                        else if (sUserType == "10")
                        {
                            sTransactionType = "Dealer Commission";
                        }
                        else if (sUserType == "2")
                        {
                            if(sLedgerCode == "4060")
                            {
                                sTransactionType = "ApplicationFee";
                            }
                            else if (sLedgerCode == "1040")
                            {
                                sTransactionType = "Security Fee";
                            }
                            else
                            {
                                sTransactionType = "Owner Commission";
                            }
                        }

                        var userCommission = new UserCommission()
                        {
                            RefId = sPayorId,
                            InvoiceNo = sInvoice,
                            TransactionType = sTransactionType,
                            AccountType = sLedgerCode != "1040" ? "Inc" : "Liab",
                            Amount = nAmount,
                            LedgerCode = sLedgerCode,
                            Debit = nAmount,
                            Credit = 0,
                            CreateDate = DateTime.Now,
                            Remarks = sNote,
                            Status = objPaymentHistory.Status,
                            UnitId = sUnit,
                            UserType = objPaymentHistory.ToUserType.ToString(),
                            OwnerId = objPaymentHistory.ToUser,
                            Month = objPaymentHistory.Month,
                            Year = objPaymentHistory.Year
                        };
                        if (new AdminUserCommissionDA(true, false).Insert(userCommission))
                        {

                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

    }
}