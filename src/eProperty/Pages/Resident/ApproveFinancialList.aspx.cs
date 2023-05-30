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
    public partial class ApproveFinancialList : System.Web.UI.Page
    {

        #region Events Support
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                    UserProfile user = (UserProfile)Session["UserObject"];
                    if (user != null)
                    {
                        FillOwner();
                    }
                    var isAdmin = false;
                    if (Session["UserObject"] != null)
                        isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                            ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                            : false;

                    if (isAdmin == false && Session["OwnerId"] != null)
                    {
                        ddlOwner.SelectedValue = Session["OwnerId"].ToString();
                        ddlOwner.Enabled = false;

                        ddlUnit.Items.Clear();
                        ddlUnit.AppendDataBoundItems = true;
                        ddlUnit.Items.Add(new ListItem("Select Unit", "-1"));
                        ddlUnit.DataSource = new ResidentialUnitDa().GetByOwner(ddlOwner.SelectedValue);
                        ddlUnit.DataTextField = "Serial";
                        ddlUnit.DataValueField = "Serial";
                        ddlUnit.DataBind();
                        ddlUnit.SelectedValue = "-1";

                        FillTransactionList(ddlOwner.SelectedValue, "", "pending");

                    }


                }
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sOwnerId = "";
            
                string sUnitId = "";

                sOwnerId = ddlOwner.SelectedValue != String.Empty && ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";             
                sUnitId = ddlUnit.SelectedValue != String.Empty && ddlUnit.SelectedValue != "-1" ? ddlUnit.SelectedValue : "";

                FillTransactionList(sOwnerId, sUnitId, "pending");
            }
            catch (Exception ex)
            { }
        }
        protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocation.PageIndex = e.NewPageIndex;

            string sOwnerId = "";
            string sUnitId = "";
            sOwnerId = ddlOwner.SelectedValue != String.Empty && ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";
            sUnitId = ddlUnit.SelectedValue != String.Empty && ddlUnit.SelectedValue != "-1" ? ddlUnit.SelectedValue : "";

            FillTransactionList(sOwnerId, sUnitId, "pending");

        }
        protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sOwnerId = "";
            string sUnitId = "";
            sOwnerId = ddlOwner.SelectedValue != String.Empty && ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";
            sUnitId = ddlUnit.SelectedValue != String.Empty && ddlUnit.SelectedValue != "-1" ? ddlUnit.SelectedValue : "";
            FillTransactionList(sOwnerId, sUnitId, "pending");
        }
        protected void ddlOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                ddlUnit.Items.Clear();
                ddlUnit.AppendDataBoundItems = true;
                ddlUnit.Items.Add(new ListItem("Select Unit", "-1"));
                ddlUnit.DataSource = new ResidentialUnitDa().GetByOwner(ddlOwner.SelectedValue);
                ddlUnit.DataTextField = "Serial";
                ddlUnit.DataValueField = "Serial";
                ddlUnit.DataBind();
                ddlUnit.SelectedValue = "-1";

                FillTransactionList(ddlOwner.SelectedValue, "", "pending");

            }
            catch (Exception)
            {

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

                int paymentHistoryId = 0;

              //  var paymentHistory = new ResidentialAddResponceTemplateDa().GetTenantPaymentHistoryBySerial(hdApplicationId.Text.ToString());

                var ExistingHistory = new ResidentialAddResponceTemplateDa().GetTenantPaymentHistoryBySerial(hdApplicationId.Text.ToString());

                if (ExistingHistory != null)
                {
                    ExistingHistory.Status = "complete";
                    if(new ResidentialAddResponceTemplateDa().UpdateTenantPaymentHistory(ExistingHistory))
                    {
                        ApproveTenant(ExistingHistory);
                        FillTransactionList(ddlOwner.SelectedValue, "", "pending");
                        Utility.DisplayMsg("Transaction successful !!", this);
                    }

                    //if (new ResidentialAddResponceTemplateDa().DeletePaymentHistoryById(ExistingHistory.Id))
                    //{
                    //   paymentHistory.Status = "complete";
                    //   paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);

                    //    if (paymentHistoryId > 0)
                    //    {

                    //    }
                    //}
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

                int paymentHistoryId = 0;

              //  var paymentHistory = new ResidentialAddResponceTemplateDa().GetTenantPaymentHistoryBySerial(hdApplicationId.Text.ToString());

                var ExistingHistory = new ResidentialAddResponceTemplateDa().GetTenantPaymentHistoryBySerial(hdApplicationId.Text.ToString());

                if (ExistingHistory != null)
                {
                    ExistingHistory.Status = "decline";
                    if (new ResidentialAddResponceTemplateDa().UpdateTenantPaymentHistory(ExistingHistory))
                    {
                        FillTransactionList(ddlOwner.SelectedValue, "", "pending");
                        Utility.DisplayMsg("Transaction Rejected !!", this);
                    }

                    //if (new ResidentialAddResponceTemplateDa().DeletePaymentHistoryById(ExistingHistory.Id))
                    //{
                    //   paymentHistory.Status = "complete";
                    //   paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);

                    //    if (paymentHistoryId > 0)
                    //    {

                    //    }
                    //}
                }

            }
        }
        #endregion

        #region Method Support
        private void FillOwner()
        {
            try
            {
                ddlOwner.Items.Clear();
                ddlOwner.AppendDataBoundItems = true;
                ddlOwner.Items.Add(new ListItem("Select Owner", "-1"));
                List<OwnerProfile> objOwners = new AdminOwnerProfileDA().GetAllOwnersInfo();
                if (objOwners != null && objOwners.Count > 0)
                {
                    foreach (OwnerProfile obj in objOwners)
                    {
                        string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString() + " (" + obj.Serial.ToString() + ")";
                        ddlOwner.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
                ddlOwner.DataBind();
                ddlOwner.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
            }

        }
        public void FillTransactionList(string ownerId, string unitId, string status)
        {
            try
            {
                List<PropertyService.BO.TenantPaymentHistory> obj = null;
                obj = new TenantDashboardDA().GetUnApprovedPaymentHistory(ownerId, unitId, status);
                gvLocation.DataSource = obj;
                gvLocation.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        private void ApproveTenant(PropertyService.BO.TenantPaymentHistory objTenantPaymentHistory)
        {
            try
            {
                decimal nApplicationFee = 0, nSecurityFee = 0, nDealerRate = 0, nDealerCommission = 0, nSalesPartnerRate = 0, nSalesPartnerCommission = 0, nOwnerCommission = 0, nAmount = 0, nRent = 0;
                string sLedgerCode = objTenantPaymentHistory.LedgerCode != null? objTenantPaymentHistory.LedgerCode : "";
                string sNote = objTenantPaymentHistory.Remarks != null ? objTenantPaymentHistory.Remarks : "";
                string sUnit = objTenantPaymentHistory.UnitNo != null ? objTenantPaymentHistory.UnitNo : "";
                string sTenant = objTenantPaymentHistory.FromUser != null ? objTenantPaymentHistory.FromUser : "";
                string sInvoice = objTenantPaymentHistory.Serial != null ? objTenantPaymentHistory.Serial : "";
                string sAccountType = "";
                string sDealerId = "";
                string sSalesPartnerId = "";
                string sPayorId = "";

                try
                {
                    string sDelFinSQL = "delete from FinancialTransaction where InvoiceNo = '" + sInvoice + "'";
                    string sDelUserSQL = "delete from UserCommission where InvoiceNo = '" + sInvoice + "'";
                    string sDelBillSQL = "delete from BillPayment where InvoiceNo = '" + sInvoice + "'";
                    Utility.RunCMDMain(sDelFinSQL);
                    Utility.RunCMDMain(sDelUserSQL);
                    Utility.RunCMD(sDelFinSQL);
                    Utility.RunCMD(sDelBillSQL);
                }
                catch(Exception ex)
                {

                }

                nAmount = objTenantPaymentHistory.Amount != null ? Convert.ToDecimal(objTenantPaymentHistory.Amount) : 0;

                string sTransactionNo = new AdminFinancialTransactionDA().MakeAutoGenSerial("F", "Account");

                string sOwnerTransactionNo = new FinancialTransactionDA().MakeAutoGenSerial("F", "Account");

                AccountChart objAccount = new AddChartofAccountDA().GetAccountTypeByLedgerCode(sLedgerCode);
                var objDealerCommission = new GetDealerCommissionRateByUnitId_Result();
                var objSalesPartnerCommission = new GetSalesPartnerCommissionRateByUnitId_Result(); 
                var get_app = new SystemInformation(); 

                TenantRentalFee_Residential objTenantRentalFee_Residential = null;

                if (sLedgerCode == "1040" || sLedgerCode == "4060" || sLedgerCode == "4010")
                {
                    objDealerCommission = new ResidentialAddResponceTemplateDa().GetDealerCommissionRateByUnitId(sUnit);

                    objSalesPartnerCommission = new ResidentialAddResponceTemplateDa().GetSalesPartnerCommissionRateByUnitId(sUnit);

                    get_app = new ResidentialAddResponceTemplateDa().GetApplicationFree(sUnit);

                    objTenantRentalFee_Residential = new ResidentialAddResponceTemplateDa().GetByApplicationId(sTenant);

                    if (objDealerCommission != null)
                    {
                        sDealerId = objDealerCommission.serialCode;
                        nDealerRate = objDealerCommission.DealerCommission != null ? Convert.ToDecimal(objDealerCommission.DealerCommission) : 0;
                    }


                    if (objSalesPartnerCommission != null)
                    {
                        sSalesPartnerId = objSalesPartnerCommission.serialCode;
                        nSalesPartnerRate = objSalesPartnerCommission.SalesPartnerCommission != null ? Convert.ToDecimal(objSalesPartnerCommission.SalesPartnerCommission) : 0;
                    }
                }

               

                if (sLedgerCode != string.Empty && sNote != "ach")
                {
                    
                    if(objAccount != null)
                    {
                        sAccountType = objAccount.accountTypeId;
                    }

                    if(sLedgerCode == "4060")  //Application fees
                    {
                        sPayorId = objTenantPaymentHistory.ToUser;
                        if (get_app != null)
                        {
                            nApplicationFee = get_app.ApplicationFee != null ? Convert.ToDecimal(get_app.ApplicationFee) : 0;
                            //try
                            //{
                            //    decimal tempScreenFee = get_app.ScreeningFee != null ? Convert.ToDecimal(get_app.ScreeningFee) : 0;
                            //    if (tempScreenFee > 0)
                            //    {
                            //        nApplicationFee = nApplicationFee - tempScreenFee;
                            //    }
                            //}
                            //catch(Exception ex)
                            //{

                            //}
                            
                        }
                    }

                    else if (sLedgerCode == "1040")  //SignUp fees
                    {
                        sPayorId = objTenantPaymentHistory.ToUser;
                        if (objTenantRentalFee_Residential != null)
                        {
                           decimal tempSecurityFee = objTenantRentalFee_Residential.SecurityDeposit != null ? Convert.ToDecimal(objTenantRentalFee_Residential.SecurityDeposit) : 0;
                            if(nAmount >= tempSecurityFee)
                            {
                               nSecurityFee = tempSecurityFee;
                               nRent = nAmount - tempSecurityFee;
                            }
                            else
                            {
                                nSecurityFee = nAmount;
                                nRent = 0;
                            }
                        }

                        nDealerCommission = nRent * nDealerRate / 100;
                        nSalesPartnerCommission = nRent * nSalesPartnerRate / 100;
                        nOwnerCommission = nRent - (nDealerCommission + nSalesPartnerCommission);
                    }
                    else if (sLedgerCode == "4010")  //Rent fees
                    {
                        sPayorId = objTenantPaymentHistory.ToUser;
                        nRent = nAmount;
                        nDealerCommission = nRent * nDealerRate / 100;
                        nSalesPartnerCommission = nRent * nSalesPartnerRate / 100;
                        nOwnerCommission = nRent - (nDealerCommission + nSalesPartnerCommission);
                    }

                    else if (sLedgerCode == "6130" && sNote == "exp")  //License/Unit fees
                    {
                        sPayorId = objTenantPaymentHistory.FromUser;
                    }

                    if (nAmount > 0)
                    {
                        var GlobalFinTranDebit = new FinancialTransaction()
                        {
                            Serial = sTransactionNo,
                            AccountType = "Asset",
                            LedgerCode = "1010",
                            InvoiceNo = sInvoice,
                            RefId = sPayorId,
                            Amount = nAmount,
                            Debit = nAmount,
                            Credit = 0,
                            CreateDate = DateTime.Now,
                            EntryType = "Debit",
                            Remarks = objTenantPaymentHistory.TransactionType
                        };

                        if (sLedgerCode == "1040")
                        {
                            var GlobalFinTranCreditSecurity = new FinancialTransaction()
                            {
                                Serial = sTransactionNo,
                                AccountType = "Liab",
                                LedgerCode = sLedgerCode,
                                InvoiceNo = sInvoice,
                                RefId = sPayorId,
                                Amount = nSecurityFee,
                                Debit = 0,
                                Credit = nSecurityFee,
                                CreateDate = DateTime.Now,
                                EntryType = "Credit",
                                Remarks = objTenantPaymentHistory.TransactionType
                            };

                            var GlobalFinTranCreditRent = new FinancialTransaction()
                            {
                                Serial = sTransactionNo,
                                AccountType = "Liab",
                                LedgerCode = "4010",
                                InvoiceNo = sInvoice,
                                RefId = sPayorId,
                                Amount = nRent,
                                Debit = 0,
                                Credit = nRent,
                                CreateDate = DateTime.Now,
                                EntryType = "Credit",
                                Remarks = objTenantPaymentHistory.TransactionType
                            };

                            if (new AdminFinancialTransactionDA(true, false).Insert(GlobalFinTranDebit))
                            {
                                if (new AdminFinancialTransactionDA(true, false).Insert(GlobalFinTranCreditSecurity))
                                {

                                }
                                if (new AdminFinancialTransactionDA(true, false).Insert(GlobalFinTranCreditRent))
                                {

                                }
                            }
                        }
                        else
                        {
                            var GlobalFinTranCredit = new FinancialTransaction()
                            {
                                Serial = sTransactionNo,
                                AccountType = objAccount != null ? objAccount.accountTypeId : "Inc",
                                LedgerCode = sLedgerCode,
                                InvoiceNo = sInvoice,
                                RefId = sPayorId,
                                Amount = nAmount,
                                Debit = 0,
                                Credit = nAmount,
                                CreateDate = DateTime.Now,
                                EntryType = "Credit",
                                Remarks = objTenantPaymentHistory.TransactionType
                            };

                            if (new AdminFinancialTransactionDA(true, false).Insert(GlobalFinTranDebit))
                            {
                                if (new AdminFinancialTransactionDA(true, false).Insert(GlobalFinTranCredit))
                                {

                                }
                            }
                        }                       
                       
                    }
                    

                    if (sLedgerCode == "6130" && sNote == "exp")  //Unit fees
                    {
                        if(nAmount > 0)
                        {
                            var FinTranCredit = new FinancialTransaction()
                            {
                                Serial = sOwnerTransactionNo,
                                AccountType = "Asset",
                                LedgerCode = "1010",
                                InvoiceNo = sInvoice,
                                RefId = sPayorId,
                                Amount = nAmount,
                                Debit = 0,
                                Credit = nAmount,
                                CreateDate = DateTime.Now,
                                EntryType = "Credit",
                                Remarks = objTenantPaymentHistory.TransactionType
                            };
                            var FinTranDebit = new FinancialTransaction()
                            {
                                Serial = sOwnerTransactionNo,
                                AccountType = "Exp",
                                LedgerCode = sLedgerCode,
                                InvoiceNo = sInvoice,
                                RefId = sPayorId,
                                Amount = nAmount,
                                Debit = nAmount,
                                Credit = 0,
                                CreateDate = DateTime.Now,
                                EntryType = "Debit",
                                Remarks = objTenantPaymentHistory.TransactionType
                            };

                            if (new FinancialTransactionDA(true, false).Insert(FinTranDebit))
                            {
                                if (new FinancialTransactionDA(true, false).Insert(FinTranCredit))
                                {

                                }
                            }

                            
                            var achFee = new BillPayment()
                            {
                                RefId = objTenantPaymentHistory.FromUser,
                                InvoiceNo = sInvoice,
                                TransactionType = "Unit Fee",
                                AccountType = "Exp",
                                Amount = Convert.ToDecimal(nAmount),
                                LedgerCode = "6130",
                                Debit = 0,
                                Credit = Convert.ToDecimal(nAmount),
                                CreateDate = DateTime.Now,
                                Remarks = "exp",
                                Status = objTenantPaymentHistory.Status,
                                UnitId = objTenantPaymentHistory.UnitNo,
                                UserType = objTenantPaymentHistory.FromUserType.ToString(),
                                OwnerId = objTenantPaymentHistory.FromUser,
                                Month = objTenantPaymentHistory.Month,
                                Year = objTenantPaymentHistory.Year
                            };
                            if (new BillPaymentDA(true, false).Insert(achFee))
                            {

                            }

                        }                      

                    }

                    if (sLedgerCode == "1040" || sLedgerCode == "4060" || sLedgerCode == "4010")  //user commission
                    {
                        if(sLedgerCode == "4060")
                        {
                            var appFee = new UserCommission()
                            {
                                RefId = sPayorId,                               
                                InvoiceNo = sInvoice,
                                TransactionType = objTenantPaymentHistory.TransactionType,
                                AccountType = objAccount != null ? objAccount.accountTypeId : "Inc",
                                Amount = nApplicationFee,
                                LedgerCode = sLedgerCode,
                                Debit = 0,
                                Credit = nApplicationFee,
                                CreateDate = DateTime.Now,
                                Remarks = sNote,
                                Status = objTenantPaymentHistory.Status,
                                UnitId = sUnit,
                                UserType = objTenantPaymentHistory.ToUserType.ToString(),
                                OwnerId = objTenantPaymentHistory.ToUser,
                                Month = objTenantPaymentHistory.Month,
                                Year = objTenantPaymentHistory.Year
                            };
                            if (new AdminUserCommissionDA(true, false).Insert(appFee))
                            {

                            }
                        }
                        else
                        {                            
                            try
                            {
                                if(nSecurityFee > 0)
                                {
                                    if (sLedgerCode == "1040")
                                    {
                                        var securityFee = new UserCommission()
                                        {
                                            RefId = sPayorId,
                                            InvoiceNo = sInvoice,
                                            TransactionType = "Security Fee",
                                            AccountType = "Liab",
                                            Amount = nSecurityFee,
                                            LedgerCode = sLedgerCode,
                                            Debit = 0,
                                            Credit = nSecurityFee,
                                            CreateDate = DateTime.Now,
                                            Remarks = objTenantPaymentHistory.TransactionType,
                                            Status = objTenantPaymentHistory.Status,
                                            UnitId = sUnit,
                                            UserType = objTenantPaymentHistory.ToUserType.ToString(),
                                            OwnerId = objTenantPaymentHistory.ToUser,
                                            Month = objTenantPaymentHistory.Month,
                                            Year = objTenantPaymentHistory.Year
                                        };
                                        if (new AdminUserCommissionDA(true, false).Insert(securityFee))
                                        {

                                        }

                                    }
                                }
                                
                            }
                            catch (Exception ex)
                            { }

                            try
                            {
                                if (nOwnerCommission > 0)
                                {
                                    var ownerCommission = new UserCommission()
                                    {
                                        RefId = sPayorId,
                                        InvoiceNo = sInvoice,
                                        TransactionType = "Owner Commission",
                                        AccountType = "Inc",
                                        Amount = nOwnerCommission,
                                        LedgerCode = "4010",
                                        Debit = 0,
                                        Credit = nOwnerCommission,
                                        CreateDate = DateTime.Now,
                                        Remarks = objTenantPaymentHistory.TransactionType,
                                        Status = objTenantPaymentHistory.Status,
                                        UnitId = sUnit,
                                        UserType = objTenantPaymentHistory.ToUserType.ToString(),
                                        OwnerId = objTenantPaymentHistory.ToUser,
                                        Month = objTenantPaymentHistory.Month,
                                        Year = objTenantPaymentHistory.Year
                                    };

                                    if (new AdminUserCommissionDA(true, false).Insert(ownerCommission))
                                    {

                                    }
                                }
                            }
                            catch (Exception ex)
                            { }

                            try
                            {
                                if (nDealerCommission > 0)
                                {
                                    var dealerCommission = new UserCommission()
                                    {
                                        RefId = sDealerId,
                                        InvoiceNo = sInvoice,
                                        TransactionType = "Dealer Commission",
                                        AccountType = "Inc",
                                        Amount = nDealerCommission,
                                        LedgerCode = "4010",
                                        Debit = 0,
                                        Credit = nDealerCommission,
                                        CreateDate = DateTime.Now,
                                        Remarks = objTenantPaymentHistory.TransactionType,
                                        Status = objTenantPaymentHistory.Status,
                                        UnitId = sUnit,
                                        UserType = ((Int16)EnumUserType.Dealer).ToString(),
                                        OwnerId = objTenantPaymentHistory.ToUser,
                                        Month = objTenantPaymentHistory.Month,
                                        Year = objTenantPaymentHistory.Year
                                    };
                                    if (new AdminUserCommissionDA(true, false).Insert(dealerCommission))
                                    {

                                    }
                                }
                            }
                            catch (Exception ex)
                            { }

                            try
                            {
                                if (nSalesPartnerCommission > 0)
                                {
                                    var salesPartnerCommission = new UserCommission()
                                    {
                                        RefId = sSalesPartnerId,
                                        InvoiceNo = sInvoice,
                                        TransactionType = "Sales Partner Commission",
                                        AccountType = "Inc",
                                        Amount = nSalesPartnerCommission,
                                        LedgerCode = "4010",
                                        Debit = 0,
                                        Credit = nSalesPartnerCommission,
                                        CreateDate = DateTime.Now,
                                        Remarks = objTenantPaymentHistory.TransactionType,
                                        Status = objTenantPaymentHistory.Status,
                                        UnitId = sUnit,
                                        UserType = ((Int16)EnumUserType.SalesPartner).ToString(),
                                        OwnerId = objTenantPaymentHistory.ToUser,
                                        Month = objTenantPaymentHistory.Month,
                                        Year = objTenantPaymentHistory.Year
                                    };

                                    if (new AdminUserCommissionDA(true, false).Insert(salesPartnerCommission))
                                    {

                                    }
                                }
                            }
                            catch (Exception ex)
                            { }


                        }

                    }

                }
                else if(sLedgerCode == "5040" && sNote == "ach")  //Bank Processing fees
                {
                    if(nAmount > 0)
                    {
                        sPayorId = objTenantPaymentHistory.FromUser;
                        var FinTranCredit = new FinancialTransaction()
                        {
                            Serial = sOwnerTransactionNo,
                            AccountType = "Asset",
                            LedgerCode = "1010",
                            InvoiceNo = sInvoice,
                            RefId = sPayorId,
                            Amount = nAmount,
                            Debit = 0,
                            Credit = nAmount,
                            CreateDate = DateTime.Now,
                            EntryType = "Credit",
                            Remarks = objTenantPaymentHistory.TransactionType
                        };
                        var FinTranDebit = new FinancialTransaction()
                        {
                            Serial = sOwnerTransactionNo,
                            AccountType = objAccount != null ? objAccount.accountTypeId : "Exp",
                            LedgerCode = sLedgerCode,
                            InvoiceNo = sInvoice,
                            RefId = sPayorId,
                            Amount = nAmount,
                            Debit = nAmount,
                            Credit = 0,
                            CreateDate = DateTime.Now,
                            EntryType = "Debit",
                            Remarks = objTenantPaymentHistory.TransactionType
                        };

                        if (new FinancialTransactionDA(true, false).Insert(FinTranDebit))
                        {
                            if (new FinancialTransactionDA(true, false).Insert(FinTranCredit))
                            {

                            }
                        }

                        var achFee = new BillPayment()
                        {
                            RefId = sPayorId,
                            InvoiceNo = sInvoice,
                            TransactionType = "ACH Process Fee",
                            AccountType = "COG",
                            Amount = Convert.ToDecimal(nAmount),
                            LedgerCode = "5040",
                            Debit = 0,
                            Credit = Convert.ToDecimal(nAmount),
                            CreateDate = DateTime.Now,
                            Remarks = "ach",
                            Status = objTenantPaymentHistory.Status,
                            UnitId = objTenantPaymentHistory.UnitNo,
                            UserType = objTenantPaymentHistory.FromUserType.ToString(),
                            OwnerId = objTenantPaymentHistory.FromUser,
                            Month = objTenantPaymentHistory.Month,
                            Year = objTenantPaymentHistory.Year
                        };
                        if (new BillPaymentDA(true, false).Insert(achFee))
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