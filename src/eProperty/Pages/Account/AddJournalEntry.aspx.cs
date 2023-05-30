using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Account;
using PropertyService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Account
{
    public partial class AddJournalEntry : System.Web.UI.Page
    {
        #region Events Support
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                    FillLedger();

                    DateTime now = DateTime.Now;
                    DateTime fromdate = new DateTime(now.Year, now.Month, 1);
                    txtFromDate.Text = fromdate.ToString("MM-dd-yyyy");
                    txtToDate.Text = DateTime.Today.ToString("MM-dd-yyyy");

                    string sFrom = Convert.ToDateTime(DateTime.Today).ToString("MM-dd-yyyy");
                    string sTo = Convert.ToDateTime(DateTime.Today).ToString("MM-dd-yyyy");
                    string sLedger = "";
                    string sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
                    FillTransactionList(sLedger, sFrom, sTo);

                }
                else
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/Login.aspx", false);
                }
            }
        }
        protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocation.PageIndex = e.NewPageIndex;

            string sFrom = Convert.ToDateTime(txtFromDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sTo = Convert.ToDateTime(txtToDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sLedger = ddlType.SelectedValue != "-1" ? ddlType.SelectedValue : "";

            FillTransactionList(sLedger, sFrom, sTo);

        }
        protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sFrom = Convert.ToDateTime(txtFromDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sTo = Convert.ToDateTime(txtToDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sLedger = ddlType.SelectedValue != "-1" ? ddlType.SelectedValue : "";

            FillTransactionList(sLedger, sFrom, sTo);
        }
       
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string sFrom = Convert.ToDateTime(txtFromDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sTo = Convert.ToDateTime(txtToDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sLedger = ddlType.SelectedValue != "-1" ? ddlType.SelectedValue : "";

            FillTransactionList(sLedger, sFrom, sTo);
        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearcontrols();
            Response.Redirect(Utility.WebUrl + "/Pages/Account/DashboardAccount.aspx", false);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string sOwnerTransactionNo = new FinancialTransactionDA().MakeAutoGenSerial("F", "Account");

                AccountChart objDebitType = new AddChartofAccountDA().GetAccountTypeByLedgerCode(ddlDebitAccount.SelectedValue);
                AccountChart objCreditType = new AddChartofAccountDA().GetAccountTypeByLedgerCode(ddlCreditAccount.SelectedValue);

                decimal nAmount = txtPaid.Text != string.Empty ? Convert.ToDecimal(txtPaid.Text.ToString()) : 0;

                if (nAmount > 0)
                {
                        var FinTranDebit = new FinancialTransaction()
                        {
                            Serial = sOwnerTransactionNo,
                            AccountType = objDebitType.accountTypeId,
                            LedgerCode = ddlDebitAccount.SelectedValue,
                            InvoiceNo = "",
                            RefId = Session["OwnerId"] != null ? Session["OwnerId"].ToString() : "",
                            Amount = nAmount,
                            Debit = nAmount,
                            Credit = 0,
                            CreateDate = DateTime.Now,
                            EntryType = "Debit",
                            Remarks = txtRemarks.Text.ToString().Trim()
                        };

                        var FinTranCredit = new FinancialTransaction()
                        {
                            Serial = sOwnerTransactionNo,
                            AccountType = objCreditType.accountTypeId,
                            LedgerCode = ddlCreditAccount.SelectedValue,
                            InvoiceNo = "",
                            RefId = Session["OwnerId"] != null ? Session["OwnerId"].ToString() : "",
                            Amount = nAmount,
                            Debit = 0,
                            Credit = nAmount,
                            CreateDate = DateTime.Now,
                            EntryType = "Credit",
                            Remarks = txtRemarks.Text.ToString().Trim()
                        };
                   

                        if (new FinancialTransactionDA(true, false).Insert(FinTranDebit))
                        {
                            if (new FinancialTransactionDA(true, false).Insert(FinTranCredit))
                            {
                                clearcontrols();
                                string sFrom = Convert.ToDateTime(DateTime.Today).ToString("MM/dd/yyyy");
                                string sTo = Convert.ToDateTime(DateTime.Today).ToString("MM/dd/yyyy");
                                string sLedger = "";
                                FillTransactionList(sLedger, sFrom, sTo);
                                Utility.DisplayMsg("Journal Added Successfully !!", this);
                            }
                        }
                    }

                }
            catch(Exception ex)
            {

            }
        }

        #endregion

        #region Method Support
        private void clearcontrols()
        {
            txtPaid.Text = "";
            txtRemarks.Text = "";
            ddlDebitAccount.SelectedValue = "-1";
            ddlCreditAccount.SelectedValue = "-1";
        }
        private void FillLedger()
        {
            string OwnerId = Session["OwnerId"] != null ? Session["OwnerId"].ToString() : "";
            List<AccountModel> listAccountChart = new AddChartofAccountDA().GetAccountChart(OwnerId);


            try
            {
                ddlType.Items.Clear();
                ddlType.AppendDataBoundItems = true;
                ddlType.Items.Add(new ListItem("Select Ledger Code", "-1"));              

                if (listAccountChart != null && listAccountChart.Count > 0)
                {
                    foreach (AccountModel obj in listAccountChart)
                    {
                        string sName = obj.AccountName.ToString() + " (" + obj.AccountCode.ToString() + ")";
                        ddlType.Items.Add(new ListItem(sName, obj.AccountCode.ToString()));
                    }
                }

                ddlType.DataBind();
            }
            catch (Exception ex)
            {

            }

            try
            {
                ddlDebitAccount.Items.Clear();
                ddlDebitAccount.AppendDataBoundItems = true;
                ddlDebitAccount.Items.Add(new ListItem("Select Ledger Code", "-1"));

                if (listAccountChart != null && listAccountChart.Count > 0)
                {
                    foreach (AccountModel obj in listAccountChart)
                    {
                        string sName = obj.AccountName.ToString() + " (" + obj.AccountCode.ToString() + ")";
                        ddlDebitAccount.Items.Add(new ListItem(sName, obj.AccountCode.ToString()));
                    }
                }

                ddlDebitAccount.DataBind();
            }
            catch (Exception ex)
            {

            }

            try
            {
                ddlCreditAccount.Items.Clear();
                ddlCreditAccount.AppendDataBoundItems = true;
                ddlCreditAccount.Items.Add(new ListItem("Select Ledger Code", "-1"));

                if (listAccountChart != null && listAccountChart.Count > 0)
                {
                    foreach (AccountModel obj in listAccountChart)
                    {
                        string sName = obj.AccountName.ToString() + " (" + obj.AccountCode.ToString() + ")";
                        ddlCreditAccount.Items.Add(new ListItem(sName, obj.AccountCode.ToString()));
                    }
                }

                ddlCreditAccount.DataBind();
            }
            catch (Exception ex)
            {

            }


        }

        public void FillTransactionList(string ledger, string FromDate, string ToDate)
        {
            try
            {
                List<FinancialTransaction> obj = null;
                string sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
                if (FromDate != "" && ToDate != "")
                {
                    obj = new FinancialTransactionDA().GetBySearch(sOwnerId, ledger, FromDate, ToDate);
                }
                else
                {
                    obj = new FinancialTransactionDA().GetBySearch(sOwnerId, ledger, "", "");
                }

                gvLocation.DataSource = obj;
                gvLocation.DataBind();


               
            }
            catch (Exception ex)
            {
            }

        }

       
        #endregion
    }
}