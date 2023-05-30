using PropertyService.BO;
using PropertyService.DA;
using PropertyService.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace eProperty.Pages.Admin
{
    public partial class AddOwnerSystem : System.Web.UI.Page
    {
        public string errStr = string.Empty;

        public string sUrl = string.Empty;

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["AddSystemId"] = null;
                Session["CardId"] = null;
                Session["accountUrl"] = null;
                FillDropdowns();
                FillContactGrid();
                FillLedgerGrid();
                FillPaymentGrid();
                FillCardGrid();

                try
                {
                    if(Request.QueryString["R"] != null)
                    {
                        sUrl = Request.QueryString["R"].ToString();
                        Session["accountUrl"] = sUrl.ToLower();
                    }                   
                }
                catch
                {
                    sUrl = "";
                }

                if (Session["Username"] != null)
                {
                    SystemInformation objSystem = new SystemInformationDA().GetByUsername(Session["Username"].ToString());
                    if (objSystem != null)
                    {
                        Session["AddSystemId"] = objSystem.Id;
                        FillControls(objSystem);
                    }
                }

                FillGlobalControls();               

                if (Session["UserObject"] != null)
                {
                    string sUserPassword = ((UserProfile)Session["UserObject"]).Password;
                    string sUserEmail = ((UserProfile)Session["UserObject"]).Email;

                    if (sUserPassword != null && sUserPassword.ToString() != string.Empty)
                    {
                        txtAccountUserPassword.Text = Utility.base64Decode(sUserPassword);
                        txtAccountUserPassword.Attributes.Add("value", Utility.base64Decode(sUserPassword.ToString()));

                        txtDocUserPassword.Text = Utility.base64Decode(sUserPassword);
                        txtDocUserPassword.Attributes.Add("value", Utility.base64Decode(sUserPassword.ToString()));
                    }

                    if (sUserEmail != null && sUserEmail.ToString() != string.Empty)
                    {
                        txtAccountUserEmail.Text = sUserEmail;
                        txtDocUserEmail.Text = sUserEmail;
                        txtComEmailAddress1.Text = sUserEmail;
                    }
                }

                if (Session["OwnerId"] != null)
                {
                    if (Session["OwnerId"].ToString() != string.Empty)
                    {
                        string sPackage = Session["OwnerId"].ToString().Substring(1);
                        txtAccountId.Text = "S" + sPackage;

                        OwnerProfile objOwner = new AdminOwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
                        if (objOwner != null)
                        {
                            txtComEmailUser1.Text = objOwner.FirstName + " " + objOwner.LastName;
                        }
                    }
                }

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            errStr = string.Empty;
            errStr = Validate_Control();
            if (Session["accountUrl"] != null)
            {
                sUrl = Session["accountUrl"].ToString();
            }

            if (errStr.Length <= 0)
            {
                try
                {
                    SystemInformation objSystemInformation = new SystemInformation();
                    objSystemInformation = SetData(objSystemInformation);

                    if(objSystemInformation != null)
                    {
                        string username = "";
                        if (Session["UserObject"] != null)
                        {
                            username = ((UserProfile)Session["UserObject"]).Username;
                        }
                        string SQL = " update UserProfile set HasSystemInfo = 1  where Username = '" + username + "' ";
                        string SQLPayment = " update PaymentInformation set OwnerId = '" + objSystemInformation.OwnerId + "'  where Username = '" + username + "' ";

                        if (Session["AddSystemId"] == null || Session["AddSystemId"].ToString() == "0")
                        {
                            if (new SystemInformationDA().Insert(objSystemInformation))
                            {
                                Utility.RunCMD(SQL);
                                Utility.RunCMD(SQLPayment);
                                if (new AdminSystemInformationDA().Insert(objSystemInformation))
                                {
                                    Utility.RunCMDMain(SQL);
                                    Utility.RunCMDMain(SQLPayment);
                                }

                                //  Session["AddSystemId"] = null;
                                //  ClearControls();
                                //    Utility.DisplayMsg("System Information Created successfully!", this);
                                //  Utility.DisplayMsgAndRedirect("System Information Created successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");

                                if (sUrl != string.Empty)
                                {
                                    if (sUrl.Trim() == "owner")
                                    {
                                        Utility.DisplayMsgAndRedirect("System Information Created successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                                    }
                                    else if (sUrl.Trim() == "home")
                                    {
                                        Utility.DisplayMsgAndRedirect("System Information Created successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                                    }
                                    else
                                    {
                                        ClearControls();
                                        Utility.DisplayMsg("System Information Created successfully!", this);
                                    }
                                }
                                else
                                {
                                    ClearControls();
                                    Utility.DisplayMsg("System Information Created successfully!", this);
                                }

                            }
                            else
                            {
                                Utility.DisplayMsg("System Information not Created!", this);
                            }
                        }
                        else
                        {
                            if (new SystemInformationDA().Update(objSystemInformation))
                            {
                                Utility.RunCMD(SQL);
                                Utility.RunCMD(SQLPayment);

                                SystemInformation objSystemOld = new AdminSystemInformationDA().GetByUsername(objSystemInformation.Username);
                                if (objSystemOld != null)
                                {
                                    objSystemInformation.Id = objSystemOld.Id;
                                    if (new AdminSystemInformationDA().Update(objSystemInformation))
                                    {
                                        Utility.RunCMDMain(SQL);
                                        Utility.RunCMDMain(SQLPayment);
                                    }
                                }
                                else
                                {
                                    if (new AdminSystemInformationDA().Insert(objSystemInformation))
                                    {
                                        Utility.RunCMDMain(SQL);
                                        Utility.RunCMDMain(SQLPayment);
                                    }
                                }

                                //    Session["AddSystemId"] = null;
                                //   ClearControls();
                                //Utility.DisplayMsg("System Information updated successfully!", this);

                                if(sUrl != string.Empty)
                                {
                                    if(sUrl.Trim() == "owner")
                                    {
                                        Utility.DisplayMsgAndRedirect("System Information updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                                    }
                                    else if(sUrl.Trim() == "home")
                                    {
                                        Utility.DisplayMsgAndRedirect("System Information updated successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                                    }
                                    else
                                    {
                                        ClearControls();
                                        Utility.DisplayMsg("System Information updated successfully!", this);
                                    }
                                }
                                else
                                {
                                    ClearControls();
                                    Utility.DisplayMsg("System Information updated successfully!", this);
                                }
                                

                            }
                            else
                            {
                                Utility.DisplayMsg("System Information not updated!", this);
                            }
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("Technical issues found. Please try again !", this);
                    }

                }
                catch (Exception ex1)
                {
                    Utility.DisplayMsg(ex1.Message.ToString(), this);
                }
            }
            else
            {
                Utility.DisplayMsg(errStr.ToString(), this);
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClearControls();
            FillGlobalControls();
        }
        
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearControls();
            if (Session["accountUrl"] != null)
            {
                sUrl = Session["accountUrl"].ToString();
            }
            if (sUrl != string.Empty)
            {
                if (sUrl.Trim() == "owner")
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddOwner.aspx", false);
                }
                else if (sUrl.Trim() == "home")
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/DashboardOwner.aspx", false);
                }
            }
            else
            {
                if (Session["HasCompletedFullProfile"] != null)
                {
                    if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == false)
                    {
                        Response.Redirect(Utility.WebUrl + "/Pages/DashboardOwner.aspx", false);
                    }
                    else
                    {
                        Response.Redirect(Utility.WebUrl + "/Pages/DashboardAdmin.aspx", false);
                    }
                }
            }
        }
        protected void btnAddTypeOfContact_Click(object sender, EventArgs e)
        {
            SaveBasicData(true, false, false);
            btnAddTypeOfContact.Text = "Add";
        }
        protected void btnDeleteContact_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvContactTypeList.Rows[row.RowIndex].FindControl("lblContactId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new ChildDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillContactGrid();
                }
            }
        }
        protected void btnEditContact_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvContactTypeList.Rows[row.RowIndex].FindControl("lblContactId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                Child obj = new ChildDA().GetChildbyID(Convert.ToInt32(hdId.Text.ToString()));
                if (obj != null)
                {
                    Session["ChildId"] = obj.Id;
                    txtTypeofContact.Text = obj.Description;
                    btnAddTypeOfContact.Text = "Update";
                }
            }
        }

        protected void btnPaymentType_Click(object sender, EventArgs e)
        {
            SaveBasicData(false, false, true);
            btnPaymentType.Text = "Add";
        }
        protected void btnDeletePaymentType_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvPaymentType.Rows[row.RowIndex].FindControl("lblPaymentTypeId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new ChildDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillPaymentGrid();
                }
            }
        }
        protected void btnEditPaymentType_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvPaymentType.Rows[row.RowIndex].FindControl("lblPaymentTypeId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                Child obj = new ChildDA().GetChildbyID(Convert.ToInt32(hdId.Text.ToString()));
                if (obj != null)
                {
                    Session["ChildId"] = obj.Id;
                    txtPaymentType.Text = obj.Description;
                    btnPaymentType.Text = "Update";
                }
            }
        }

        protected void btnLedger_Click(object sender, EventArgs e)
        {
            SaveBasicData(false, true, false);
            btnLedger.Text = "Add";
        }
        protected void btnDeleteLedger_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvLedger.Rows[row.RowIndex].FindControl("lblLedgerId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new ChildDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillLedgerGrid();
                }
            }
        }
        protected void btnEditLedger_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvLedger.Rows[row.RowIndex].FindControl("lblLedgerId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                Child obj = new ChildDA().GetChildbyID(Convert.ToInt32(hdId.Text.ToString()));
                if (obj != null)
                {
                    Session["ChildId"] = obj.Id;
                    txtLedger.Text = obj.Code;
                    txtLedgerName.Text = obj.Description;
                    btnLedger.Text = "Update";
                }
            }
        }

        protected void btnAddCard_Click(object sender, EventArgs e)
        {
            SaveCardData();
            btnAddCard.Text = "Add";
        }
        protected void btnDeleteCard_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvCard.Rows[row.RowIndex].FindControl("lblCardId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                PaymentInformation objPayment = new PaymentInformationDA().GetByID(Convert.ToInt32(hdId.Text));
                if (objPayment != null)
                {
                    if (new PaymentInformationDA().DeleteByOwnerCardAndCheckingAccount(objPayment.OwnerId, objPayment.CardNumber, objPayment.AccountNo))
                    {
                        if (new AdminPaymentInformationDA().DeleteByOwnerCardAndCheckingAccount(objPayment.OwnerId, objPayment.CardNumber, objPayment.AccountNo))
                        {
                        }

                        FillCardGrid();
                    }
                }

            }
        }
        protected void btnEditCard_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvCard.Rows[row.RowIndex].FindControl("lblCardId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                PaymentInformation obj = new PaymentInformationDA().GetByID(Convert.ToInt32(hdId.Text));
                if (obj != null)
                {
                    Session["CardId"] = obj.Id;
                    btnAddCard.Text = "Update";


                    txtCardAccountName.Text = "";
                    txtCardAddress.Text = "";
                    txtCardCity.Text = "";
                    txtCardZip.Text = "";
                    txtCardNumber.Text = "";
                    txtCVS.Text = "";
                    txtRoutingNo.Text = "";
                    txtRoutingNo2.Text = "";
                    txtCheckingAccount.Text = "";
                    txtCheckingAccount2.Text = "";

                    if (obj.AccountName != null && obj.AccountName.ToString() != string.Empty)
                    {
                        txtCardAccountName.Text = obj.AccountName;
                    }

                    if (obj.Address != null && obj.Address.ToString() != string.Empty)
                    {
                        txtCardAddress.Text = obj.Address;
                    }
                    if (obj.State != null && obj.State.ToString() != string.Empty)
                    {
                        ddlState.SelectedValue = obj.State;
                    }
                    if (obj.City != null && obj.City.ToString() != string.Empty)
                    {
                        txtCardCity.Text = obj.City;
                    }

                    if (obj.Zip != null && obj.Zip.ToString() != string.Empty)
                    {
                        txtCardZip.Text = obj.Zip;
                    }

                    if (obj.IsCheckingAccount != null && obj.IsCheckingAccount.ToString() != string.Empty && Convert.ToBoolean(obj.IsCheckingAccount))
                    {
                        rdoCardType.Items[0].Selected = false;
                        rdoCardType.Items[1].Selected = true;

                        if (obj.RoutingNo != null && obj.RoutingNo.ToString() != string.Empty)
                        {
                            txtRoutingNo.Text = obj.RoutingNo;
                            txtRoutingNo2.Text = obj.RoutingNo;
                        }                       

                        if (obj.AccountNo != null && obj.AccountNo.ToString() != string.Empty)
                        {
                            txtCheckingAccount.Text = obj.AccountNo;
                            txtCheckingAccount2.Text = obj.AccountNo;
                        }                        

                    }
                    else
                    {
                        rdoCardType.Items[0].Selected = true;
                        rdoCardType.Items[1].Selected = false;

                       

                        if (obj.CardNumber != null && obj.CardNumber.ToString() != string.Empty)
                        {
                            txtCardNumber.Text = obj.CardNumber;
                        }

                        if (obj.CVS != null && obj.CVS.ToString() != string.Empty)
                        {
                            txtCVS.Text = obj.CVS;
                        }

                       

                        if (obj.Month != null && obj.Month.ToString() != string.Empty)
                        {
                            ddlMonth.SelectedValue = obj.Month;
                        }

                        if (obj.Year != null && obj.Year.ToString() != string.Empty)
                        {
                            ddlYear.SelectedValue = obj.Year;
                        }

                    }                  
                }
            }
        }            

        #endregion
        #region Method
        private void FillDropdowns()
        {
            try
            {
                ddlState.Items.Clear();
                ddlState.AppendDataBoundItems = true;
                ddlState.DataSource = new StateDA().GetAllRefStates();
                ddlState.DataTextField = "STATENAME";
                ddlState.DataValueField = "STATE";
                ddlState.DataBind();
            }
            catch (Exception ex)
            {

            }

            try
            {
                ddlYear.Items.Clear();
                ddlYear.AppendDataBoundItems = true;
                int nStart = DateTime.UtcNow.Year;
                int nEnd = nStart + 10;

                for (int i = nStart; i <= nEnd; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                ddlYear.DataBind();
            }
            catch (Exception ex)
            {

            }

            try
            {
                ddlMonth.Items.Clear();
                ddlMonth.AppendDataBoundItems = true;
                int nStart = 1;
                int nEnd = 12;

                for (int i = nStart; i <= nEnd; i++)
                {
                    ddlMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                ddlMonth.DataBind();
            }
            catch (Exception ex)
            {

            }

        }
        private void ClearControls()
        {
            rdoFeeType.SelectedValue = null;
            txtWebUrl.Text = "";
            txtApplicationFee.Text = "";
            txtFeeAmount.Text = "";
            txtMonthlyCharge.Text = "";
            rdoAccount.SelectedValue = null;
            txtUnitCost.Text = "";
            txtTotalUnit.Text = "";
            txtCheckAmount.Text = "";
            txtLateFee.Text = "";
            txtChargeBackFee.Text = "";
            txtApplicationFee.Text = "";
            txtScreeningFee.Text = "";
            rdoAccount.SelectedValue = null;
            txtUnitCost.Text = "";
            txtTotalUnit.Text = "";
            txtCheckAmount.Text = "";
            txtLateFee.Text = "";
            txtChargeBackFee.Text = "";
            txtApplicationFee.Text = "";
            txtScreeningFee.Text = "";
            txtComEmailUser1.Text = "";
            txtComEmailAddress1.Text = "";
            txtComEmailUser2.Text = "";
            txtComEmailAddress2.Text = "";
            txtComEmailUser3.Text = "";
            txtComEmailAddress3.Text = "";
            txtComEmailUser4.Text = "";
            txtComEmailAddress4.Text = "";

        }
        public string Validate_Control()
        {
            try
            {
                if (txtLateFee.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Late Fee" + Environment.NewLine;
                }
                else
                {
                    if (Convert.ToDecimal(txtLateFee.Text.ToString().Trim()) < 5)
                    {
                        errStr += "Please enter minimum value of 5%" + Environment.NewLine;
                    }
                }

                if (txtChargeBackFee.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter ChargeBack Fee" + Environment.NewLine;
                }
                else
                {
                    if (Convert.ToDecimal(txtChargeBackFee.Text.ToString().Trim()) < 50)
                    {
                        errStr += "Please enter minimum value of $50" + Environment.NewLine;
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return errStr;
        }
        private SystemInformation SetData(SystemInformation obj)
        {
            try
            {
                obj = new SystemInformation();

                if (Session["AddSystemId"] != null && Convert.ToInt32(Session["AddSystemId"]) > 0)
                {
                    obj = new SystemInformationDA().GetByID(Convert.ToInt32(Session["AddSystemId"]));
                    obj.Id = Convert.ToInt32(Session["AddSystemId"].ToString());
                }

                

                if (Session["Username"] != null)
                {
                    if (Session["Username"].ToString() != string.Empty)
                    {
                        obj.Username = Session["Username"].ToString();
                        //obj = new SystemInformationDA().GetByUsername(Session["Username"].ToString());
                        //if (obj != null)
                        //    Session["AddSystemId"] = obj.Id;
                    }
                    else
                    {
                        obj.Username = "";
                    }
                }

               

                if (!string.IsNullOrEmpty(txtWebUrl.Text.ToString()))
                {
                    obj.Website = txtWebUrl.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Website = "";
                }
                if (!string.IsNullOrEmpty(txtComEmailUser1.Text.ToString()))
                {
                    obj.ComUsername1 = txtComEmailUser1.Text.ToString();
                }
                else
                {
                    obj.ComUsername1 = "";
                }
                if (!string.IsNullOrEmpty(txtComEmailAddress1.Text.ToString()))
                {
                    obj.ComEmailAddress1 = txtComEmailAddress1.Text.ToString();
                }
                else
                {
                    obj.ComEmailAddress1 = "";
                }

                if (!string.IsNullOrEmpty(txtComEmailUser2.Text.ToString()))
                {
                    obj.ComUsername2 = txtComEmailUser2.Text.ToString();
                }
                else
                {
                    obj.ComUsername2 = "";
                }
                if (!string.IsNullOrEmpty(txtComEmailAddress2.Text.ToString()))
                {
                    obj.ComEmailAddress2 = txtComEmailAddress2.Text.ToString();
                }
                else
                {
                    obj.ComEmailAddress2 = "";
                }
                if (!string.IsNullOrEmpty(txtComEmailUser3.Text.ToString()))
                {
                    obj.ComUsername3 = txtComEmailUser3.Text.ToString();
                }
                else
                {
                    obj.ComUsername3 = "";
                }
                if (!string.IsNullOrEmpty(txtComEmailAddress3.Text.ToString()))
                {
                    obj.ComEmailAddress3 = txtComEmailAddress3.Text.ToString();
                }
                else
                {
                    obj.ComEmailAddress3 = "";
                }
                if (!string.IsNullOrEmpty(txtComEmailUser4.Text.ToString()))
                {
                    obj.ComUsername4 = txtComEmailUser4.Text.ToString();
                }
                else
                {
                    obj.ComUsername4 = "";
                }
                if (!string.IsNullOrEmpty(txtComEmailAddress4.Text.ToString()))
                {
                    obj.ComEmailAddress4 = txtComEmailAddress4.Text.ToString();
                }
                else
                {
                    obj.ComEmailAddress4 = "";
                }

                if (!string.IsNullOrEmpty(txtAccountId.Text.ToString()))
                {
                    obj.AccountPackageId = txtAccountId.Text.ToString();
                }
                else
                {
                    obj.AccountPackageId = "";
                }

                if (!string.IsNullOrEmpty(txtApplicationFee.Text.ToString()))
                {
                    obj.ApplicationFee = Convert.ToDecimal(txtApplicationFee.Text.ToString());
                }
                else
                {
                    obj.ApplicationFee = 0;
                }

               

                if (!string.IsNullOrEmpty(txtMonthlyCharge.Text.ToString()))
                {
                    obj.MonthlySoftwareCharge = Convert.ToDecimal(txtMonthlyCharge.Text.ToString());
                }
                else
                {
                    obj.MonthlySoftwareCharge = 0;
                }

                obj.CreditCardProcessFees = 0;
                if (chkIncludeFee.Checked)
                {
                    obj.IncludeProcessFees = true;
                }
                else
                {
                    obj.IncludeProcessFees = false;
                }
                if (chkTenantPayFee.Checked)
                {
                    obj.TanentPayFees = true;
                }
                else
                {
                    obj.TanentPayFees = false;
                }

                if (chkIncludeCondoFee.Checked)
                {
                    obj.IncludeCondoProcessFees = true;
                }
                else
                {
                    obj.IncludeCondoProcessFees = false;
                }

                if (chkTenantPayCondoFee.Checked)
                {
                    obj.TanentPayCondoFees = true;
                }
                else
                {
                    obj.TanentPayCondoFees = false;
                }

                if (chkOneTime.Checked)
                {
                    obj.OneTimePay = true;
                }
                else
                {
                    obj.OneTimePay = false;
                }
                if (chkRecurring.Checked)
                {
                    obj.RecurringPay = true;
                }
                else
                {
                    obj.RecurringPay = false;
                }

                if (Session["UserObject"] != null)
                {
                    if (Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin))
                    {
                        obj.IsGlobalSystem = true;
                    }
                    else
                    {
                        obj.IsGlobalSystem = false;
                    }
                }
                else
                {
                    obj.IsGlobalSystem = false;
                }

                if (!string.IsNullOrEmpty(txtUnitCost.Text.ToString()))
                {
                    obj.UnitPrice = Convert.ToDecimal(txtUnitCost.Text.ToString());
                }
                else
                {
                    obj.UnitPrice = 0;
                }
                if (!string.IsNullOrEmpty(txtTotalUnit.Text.ToString()))
                {
                    obj.NoOfUnit = Convert.ToInt32(txtTotalUnit.Text.ToString());
                }
                else
                {
                    obj.NoOfUnit = 0;
                }

                if (!string.IsNullOrEmpty(txtScreeningFee.Text.ToString()))
                {
                    obj.ScreeningFee = Convert.ToDecimal(txtScreeningFee.Text.ToString());
                }
                else
                {
                    obj.ScreeningFee = 0;
                }
                if (!string.IsNullOrEmpty(txtLateFee.Text.ToString()))
                {
                    obj.LateRentPercentage = Convert.ToDecimal(txtLateFee.Text.ToString());
                }
                else
                {
                    obj.LateRentPercentage = 0;
                }

                if (!string.IsNullOrEmpty(txtChargeBackFee.Text.ToString()))
                {
                    obj.ChargeBackFee = Convert.ToDecimal(txtChargeBackFee.Text.ToString());
                }
                else
                {
                    obj.ChargeBackFee = 0;
                }

                if (rdoFeeType.Items[0].Selected == true)
                {
                    obj.FeeType = 1;
                    if (!string.IsNullOrEmpty(txtFeeAmount.Text.ToString()))
                    {
                        obj.FeePercentage = Convert.ToDecimal(txtFeeAmount.Text.ToString());
                        obj.FeeFlatAmount = 0;
                    }
                    else
                    {
                        obj.FeePercentage = 0;
                        obj.FeeFlatAmount = 0;
                    }
                }
                else if (rdoFeeType.Items[1].Selected == true)
                {
                    obj.FeeType = 2;
                    if (!string.IsNullOrEmpty(txtFeeAmount.Text.ToString()))
                    {
                        obj.FeeFlatAmount = Convert.ToDecimal(txtFeeAmount.Text.ToString());
                        obj.FeePercentage = 0;
                    }
                    else
                    {
                        obj.FeeFlatAmount = 0;
                        obj.FeePercentage = 0;
                    }
                }

                if (rdoAccount.Items[0].Selected == true)
                {
                    obj.FeeTypeCheck = 1;
                    if (!string.IsNullOrEmpty(txtCheckAmount.Text.ToString()))
                    {
                        obj.FeePercentageCheck = Convert.ToDecimal(txtCheckAmount.Text.ToString());
                        obj.FeeFlatAmountCheck = 0;
                    }
                    else
                    {
                        obj.FeePercentageCheck = 0;
                        obj.FeeFlatAmountCheck = 0;
                    }
                }
                else if (rdoAccount.Items[1].Selected == true)
                {
                    obj.FeeTypeCheck = 2;
                    if (!string.IsNullOrEmpty(txtCheckAmount.Text.ToString()))
                    {
                        obj.FeeFlatAmountCheck = Convert.ToDecimal(txtCheckAmount.Text.ToString());
                        obj.FeePercentageCheck = 0;
                    }
                    else
                    {
                        obj.FeeFlatAmountCheck = 0;
                        obj.FeePercentageCheck = 0;
                    }
                }

                if (Session["OwnerId"] != null)
                {
                    if (Session["OwnerId"].ToString() != string.Empty)
                    {
                        obj.OwnerId = Session["OwnerId"].ToString();
                    }
                    else
                    {
                        obj.OwnerId = "";
                    }
                }
                else
                {
                    obj.OwnerId = "";
                }

                if (!string.IsNullOrEmpty(txtAccountId.Text.ToString()))
                {
                    obj.AccountPackageId = txtAccountId.Text.ToString();
                }
                else
                {
                    string sPackage = obj.OwnerId.Substring(1);
                    sPackage = "S" + sPackage;
                    obj.AccountPackageId = sPackage;
                }

                

                //if (Session["AddSystemId"] == null ||  string.IsNullOrEmpty(obj.AccountPackageId))
                //{
                //    //obj.AccountPackageId = new AdminSystemInformationDA().MakeAutoGenSerial("P", "AccountPackage");
                //    string sPackage = obj.OwnerId.Substring(1);
                //    sPackage = "S" + sPackage;
                //    obj.AccountPackageId = sPackage;


                //}


            }
            catch (Exception ex)
            {
            }

            return obj;
        }
        //private void FillControls(int nId)
        //{
        //    try
        //    {
        //        if (nId > 0)
        //        {
        //            SystemInformation obj = new SystemInformationDA().GetByID(nId);
        //            if (obj != null)
        //            {
        //                Session["AddSystemId"] = obj.Id;

        //                if (obj.Website != null && obj.Website.ToString() != string.Empty)
        //                {
        //                    txtWebUrl.Text = obj.Website;
        //                }
        //                else
        //                {
        //                    txtWebUrl.Text = "";
        //                }
        //                if (obj.ComUsername1 != null && obj.ComUsername1.ToString() != string.Empty)
        //                {
        //                    txtComEmailUser1.Text = obj.ComUsername1;
        //                }
        //                else
        //                {
        //                    txtComEmailUser1.Text = "";
        //                }

        //                if (obj.ComUsername2 != null && obj.ComUsername2.ToString() != string.Empty)
        //                {
        //                    txtComEmailUser2.Text = obj.ComUsername2;
        //                }
        //                else
        //                {
        //                    txtComEmailUser2.Text = "";
        //                }

        //                if (obj.ComUsername3 != null && obj.ComUsername3.ToString() != string.Empty)
        //                {
        //                    txtComEmailUser3.Text = obj.ComUsername3;
        //                }
        //                else
        //                {
        //                    txtComEmailUser3.Text = "";
        //                }

        //                if (obj.ComUsername4 != null && obj.ComUsername4.ToString() != string.Empty)
        //                {
        //                    txtComEmailUser4.Text = obj.ComUsername4;
        //                }
        //                else
        //                {
        //                    txtComEmailUser4.Text = "";
        //                }


        //                if (obj.ComEmailAddress1 != null && obj.ComEmailAddress1.ToString() != string.Empty)
        //                {
        //                    txtComEmailAddress1.Text = obj.ComEmailAddress1;
        //                }
        //                else
        //                {
        //                    txtComEmailAddress1.Text = "";
        //                }

        //                if (obj.ComEmailAddress2 != null && obj.ComEmailAddress2.ToString() != string.Empty)
        //                {
        //                    txtComEmailAddress2.Text = obj.ComEmailAddress2;
        //                }
        //                else
        //                {
        //                    txtComEmailAddress2.Text = "";
        //                }

        //                if (obj.ComEmailAddress3 != null && obj.ComEmailAddress3.ToString() != string.Empty)
        //                {
        //                    txtComEmailAddress3.Text = obj.ComEmailAddress3;
        //                }
        //                else
        //                {
        //                    txtComEmailAddress3.Text = "";
        //                }

        //                if (obj.ComEmailAddress4 != null && obj.ComEmailAddress4.ToString() != string.Empty)
        //                {
        //                    txtComEmailUser4.Text = obj.ComEmailAddress4;
        //                }
        //                else
        //                {
        //                    txtComEmailUser4.Text = "";
        //                }

        //                if (Session["UserObject"] != null)
        //                {
        //                    string sUserPassword = ((UserProfile)Session["UserObject"]).Password;
        //                    string sUserEmail = ((UserProfile)Session["UserObject"]).Email;

        //                    if (sUserPassword != null && sUserPassword.ToString() != string.Empty)
        //                    {
        //                        txtAccountUserPassword.Text = Utility.base64Decode(sUserPassword);
        //                        txtAccountUserPassword.Attributes.Add("value", Utility.base64Decode(sUserPassword.ToString()));

        //                        txtDocUserPassword.Text = Utility.base64Decode(sUserPassword);
        //                        txtDocUserPassword.Attributes.Add("value", Utility.base64Decode(sUserPassword.ToString()));
        //                    }

        //                    if (sUserEmail != null && sUserEmail.ToString() != string.Empty)
        //                    {
        //                        txtAccountUserEmail.Text = sUserEmail;
        //                        txtDocUserEmail.Text = sUserEmail;
        //                    }

        //                }

        //                if (obj.AccountPackageId != null && obj.AccountPackageId.ToString() != string.Empty)
        //                {
        //                    txtAccountId.Text = obj.AccountPackageId;
        //                }

        //                //if (Session["UserId"] != null)
        //                //{
        //                //    UserProfile objUser = new UserProfileDA().GetUserByUserID(Convert.ToInt32(Session["UserId"]));
        //                //    if (objUser != null)
        //                //    {
        //                //        if (objUser.Password != null && objUser.Password.ToString() != string.Empty)
        //                //        {
        //                //            txtAccountUserPassword.Text = objUser.Password;
        //                //            txtAccountUserPassword.Attributes.Add("value", objUser.Password.ToString().Trim());

        //                //            txtDocUserPassword.Text = objUser.Password;
        //                //            txtDocUserPassword.Attributes.Add("value", objUser.Password.ToString().Trim());
        //                //        }
        //                //        else
        //                //        {
        //                //            txtAccountUserPassword.Text = "";
        //                //            txtDocUserPassword.Text = "";
        //                //        }

        //                //        if (objUser.Email != null && objUser.Email.ToString() != string.Empty)
        //                //        {
        //                //            txtAccountUserEmail.Text = objUser.Email;
        //                //            txtDocUserEmail.Text = objUser.Email;
        //                //        }
        //                //        else
        //                //        {
        //                //            txtAccountUserEmail.Text = "";
        //                //            txtDocUserEmail.Text = "";
        //                //        }

        //                //    }
        //                //}




        //                if (obj.MonthlySoftwareCharge != null && obj.MonthlySoftwareCharge.ToString() != string.Empty)
        //                {
        //                    txtMonthlyCharge.Text = Convert.ToDecimal(obj.MonthlySoftwareCharge).ToString("#.00");
        //                }

        //                if (obj.IncludeProcessFees != null)
        //                {
        //                    chkIncludeFee.Checked = Convert.ToBoolean(obj.IncludeProcessFees);
        //                }
        //                if (obj.TanentPayFees != null)
        //                {
        //                    chkTenantPayFee.Checked = Convert.ToBoolean(obj.TanentPayFees);
        //                }

        //                if (obj.IncludeCondoProcessFees != null)
        //                {
        //                    chkIncludeCondoFee.Checked = Convert.ToBoolean(obj.IncludeCondoProcessFees);
        //                }

        //                if (obj.TanentPayCondoFees != null)
        //                {
        //                    chkTenantPayCondoFee.Checked = Convert.ToBoolean(obj.TanentPayCondoFees);
        //                }
        //                if (obj.OneTimePay != null)
        //                {
        //                    chkOneTime.Checked = Convert.ToBoolean(obj.OneTimePay);
        //                }
        //                if (obj.RecurringPay != null)
        //                {
        //                    chkRecurring.Checked = Convert.ToBoolean(obj.RecurringPay);
        //                }

        //                if (obj.UnitPrice != null && obj.UnitPrice.ToString() != string.Empty)
        //                {
        //                    txtUnitCost.Text = Convert.ToDecimal(obj.UnitPrice).ToString("#.00");
        //                }

        //                if (obj.NoOfUnit != null && obj.NoOfUnit.ToString() != string.Empty)
        //                {
        //                    txtTotalUnit.Text = Convert.ToInt32(obj.NoOfUnit).ToString();
        //                }

        //                //if (obj.ApplicationFee != null && obj.ApplicationFee.ToString() != string.Empty)
        //                //{
        //                //    txtApplicationFee.Text = Convert.ToDecimal(obj.ApplicationFee).ToString("#.00");
        //                //}

        //                //if (obj.FeePercentage != null && obj.FeePercentage.ToString() != string.Empty)
        //                //{
        //                //    txtFeeAmount.Text = Convert.ToDecimal(obj.FeePercentage).ToString("#.00");
        //                //}

        //                //if (obj.FeeType != null)
        //                //{
        //                //    if (Convert.ToInt32(obj.FeeType) == 1)
        //                //    {
        //                //        rdoFeeType.Items[0].Selected = true;
        //                //        if (obj.FeePercentage != null && obj.FeePercentage.ToString() != string.Empty)
        //                //        {
        //                //            txtFeeAmount.Text = Convert.ToDecimal(obj.FeePercentage).ToString("#.00");
        //                //        }
        //                //    }
        //                //    else if (Convert.ToInt32(obj.FeeType) == 2)
        //                //    {
        //                //        rdoFeeType.Items[1].Selected = true;
        //                //        if (obj.FeeFlatAmount != null && obj.FeeFlatAmount.ToString() != string.Empty)
        //                //        {
        //                //            txtFeeAmount.Text = Convert.ToDecimal(obj.FeeFlatAmount).ToString("#.00");
        //                //        }
        //                //    }
        //                //}

        //                //if (obj.ScreeningFee != null && obj.ScreeningFee.ToString() != string.Empty)
        //                //{
        //                //    txtScreeningFee.Text = Convert.ToDecimal(obj.ScreeningFee).ToString("#.00");
        //                //}
        //                //if (obj.LateRentPercentage != null && obj.LateRentPercentage.ToString() != string.Empty)
        //                //{
        //                //    txtLateFee.Text = Convert.ToDecimal(obj.LateRentPercentage).ToString("#.00");
        //                //}
        //                //if (obj.ChargeBackFee != null && obj.ChargeBackFee.ToString() != string.Empty)
        //                //{
        //                //    txtChargeBackFee.Text = Convert.ToDecimal(obj.ChargeBackFee).ToString("#.00");
        //                //}

        //                //if (obj.FeeTypeCheck != null)
        //                //{
        //                //    if (Convert.ToInt32(obj.FeeTypeCheck) == 1)
        //                //    {
        //                //        rdoAccount.Items[0].Selected = true;
        //                //        if (obj.FeePercentageCheck != null && obj.FeePercentageCheck.ToString() != string.Empty)
        //                //        {
        //                //            txtCheckAmount.Text = Convert.ToDecimal(obj.FeePercentageCheck).ToString("#.00");
        //                //        }
        //                //    }
        //                //    else if (Convert.ToInt32(obj.FeeTypeCheck) == 2)
        //                //    {
        //                //        rdoAccount.Items[1].Selected = true;
        //                //        if (obj.FeeFlatAmountCheck != null && obj.FeeFlatAmountCheck.ToString() != string.Empty)
        //                //        {
        //                //            txtCheckAmount.Text = Convert.ToDecimal(obj.FeeFlatAmountCheck).ToString("#.00");
        //                //        }
        //                //    }
        //                //}

        //               // btnSave.Text = "Update";

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //}

        private void FillControls(SystemInformation obj)
        {
            try
            {
                if (obj != null)
                {
                    Session["AddSystemId"] = obj.Id;

                    if (obj.Website != null && obj.Website.ToString() != string.Empty)
                    {
                        txtWebUrl.Text = obj.Website;
                    }
                    else
                    {
                        txtWebUrl.Text = "";
                    }
                    if (obj.ComUsername1 != null && obj.ComUsername1.ToString() != string.Empty)
                    {
                        txtComEmailUser1.Text = obj.ComUsername1;
                    }
                    else
                    {
                        txtComEmailUser1.Text = "";
                    }

                    if (obj.ComUsername2 != null && obj.ComUsername2.ToString() != string.Empty)
                    {
                        txtComEmailUser2.Text = obj.ComUsername2;
                    }
                    else
                    {
                        txtComEmailUser2.Text = "";
                    }

                    if (obj.ComUsername3 != null && obj.ComUsername3.ToString() != string.Empty)
                    {
                        txtComEmailUser3.Text = obj.ComUsername3;
                    }
                    else
                    {
                        txtComEmailUser3.Text = "";
                    }

                    if (obj.ComUsername4 != null && obj.ComUsername4.ToString() != string.Empty)
                    {
                        txtComEmailUser4.Text = obj.ComUsername4;
                    }
                    else
                    {
                        txtComEmailUser4.Text = "";
                    }


                    if (obj.ComEmailAddress1 != null && obj.ComEmailAddress1.ToString() != string.Empty)
                    {
                        txtComEmailAddress1.Text = obj.ComEmailAddress1;
                    }
                    else
                    {
                        txtComEmailAddress1.Text = "";
                    }

                    if (obj.ComEmailAddress2 != null && obj.ComEmailAddress2.ToString() != string.Empty)
                    {
                        txtComEmailAddress2.Text = obj.ComEmailAddress2;
                    }
                    else
                    {
                        txtComEmailAddress2.Text = "";
                    }

                    if (obj.ComEmailAddress3 != null && obj.ComEmailAddress3.ToString() != string.Empty)
                    {
                        txtComEmailAddress3.Text = obj.ComEmailAddress3;
                    }
                    else
                    {
                        txtComEmailAddress3.Text = "";
                    }

                    if (obj.ComEmailAddress4 != null && obj.ComEmailAddress4.ToString() != string.Empty)
                    {
                        txtComEmailAddress4.Text = obj.ComEmailAddress4;
                    }
                    else
                    {
                        txtComEmailAddress4.Text = "";
                    }

                    if (Session["UserObject"] != null)
                    {
                        string sUserPassword = ((UserProfile)Session["UserObject"]).Password;
                        string sUserEmail = ((UserProfile)Session["UserObject"]).Email;

                        if (sUserPassword != null && sUserPassword.ToString() != string.Empty)
                        {
                            txtAccountUserPassword.Text = Utility.base64Decode(sUserPassword);
                            txtAccountUserPassword.Attributes.Add("value", Utility.base64Decode(sUserPassword.ToString()));

                            txtDocUserPassword.Text = Utility.base64Decode(sUserPassword);
                            txtDocUserPassword.Attributes.Add("value", Utility.base64Decode(sUserPassword.ToString()));
                        }

                        if (sUserEmail != null && sUserEmail.ToString() != string.Empty)
                        {
                            txtAccountUserEmail.Text = sUserEmail;
                            txtDocUserEmail.Text = sUserEmail;
                        }

                    }

                    if (obj.AccountPackageId != null && obj.AccountPackageId.ToString() != string.Empty)
                    {
                        txtAccountId.Text = obj.AccountPackageId;
                    }


                    if (obj.MonthlySoftwareCharge != null && obj.MonthlySoftwareCharge.ToString() != string.Empty)
                    {
                        txtMonthlyCharge.Text = Convert.ToDecimal(obj.MonthlySoftwareCharge).ToString("#.00");
                    }

                    if (obj.IncludeProcessFees != null)
                    {
                        chkIncludeFee.Checked = Convert.ToBoolean(obj.IncludeProcessFees);
                    }
                    if (obj.TanentPayFees != null)
                    {
                        chkTenantPayFee.Checked = Convert.ToBoolean(obj.TanentPayFees);
                    }

                    if (obj.IncludeCondoProcessFees != null)
                    {
                        chkIncludeCondoFee.Checked = Convert.ToBoolean(obj.IncludeCondoProcessFees);
                    }

                    if (obj.TanentPayCondoFees != null)
                    {
                        chkTenantPayCondoFee.Checked = Convert.ToBoolean(obj.TanentPayCondoFees);
                    }
                    if (obj.OneTimePay != null)
                    {
                        chkOneTime.Checked = Convert.ToBoolean(obj.OneTimePay);
                    }
                    if (obj.RecurringPay != null)
                    {
                        chkRecurring.Checked = Convert.ToBoolean(obj.RecurringPay);
                    }

                    if (obj.UnitPrice != null && obj.UnitPrice.ToString() != string.Empty)
                    {
                        txtUnitCost.Text = Convert.ToDecimal(obj.UnitPrice).ToString("#.00");
                    }

                    if (obj.NoOfUnit != null && obj.NoOfUnit.ToString() != string.Empty)
                    {
                        txtTotalUnit.Text = Convert.ToInt32(obj.NoOfUnit).ToString();
                    }



                }
            }
            catch (Exception ex)
            {
            }

        }

        private void FillPaymentGrid()
        {
            try
            {

                List<Child> objChildPayment = null;
                objChildPayment = new ChildDA().GetChildByParentID(Convert.ToInt32(EnumGlobalData.Payment));
                gvPaymentType.DataSource = objChildPayment;
                gvPaymentType.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        private void FillContactGrid()
        {
            try
            {
                List<Child> objChildTypeOfContact = null;
                objChildTypeOfContact = new ChildDA().GetChildByParentID(Convert.ToInt32(EnumGlobalData.ContactType));
                gvContactTypeList.DataSource = objChildTypeOfContact;
                gvContactTypeList.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        private void FillLedgerGrid()
        {
            try
            {
                List<Child> objChildLedger = null;
                objChildLedger = new ChildDA().GetChildByParentID(Convert.ToInt32(EnumGlobalData.Ledger));
                gvLedger.DataSource = objChildLedger;
                gvLedger.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        private void FillCardGrid()
        {
            try
            {
                List<PaymentInformation> objPayments = null;
                if (Session["Username"] != null)
                {
                    objPayments = new PaymentInformationDA().GetByUsername(Session["Username"].ToString());
                }
                gvCard.DataSource = objPayments;
                gvCard.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        private void SaveBasicData(bool IsContact, bool IsLedger, bool IsPayType)
        {
            try
            {
                Child objChild = new Child();
                int nUserDefinedId = 0;
                if (Session["ChildId"] != null && Convert.ToInt32(Session["ChildId"]) > 0)
                {
                    objChild.Id = Convert.ToInt32(Session["ChildId"].ToString());
                }

                if (IsContact == true)
                {
                    nUserDefinedId = Convert.ToInt32(EnumGlobalData.ContactType);
                    objChild.Code = txtTypeofContact.Text.ToString();
                    objChild.Description = txtTypeofContact.Text.ToString();
                }
                else if (IsLedger == true)
                {
                    nUserDefinedId = Convert.ToInt32(EnumGlobalData.Ledger);
                    objChild.Code = txtLedger.Text.ToString();
                    objChild.Description = txtLedgerName.Text.ToString();
                }
                else if (IsPayType == true)
                {
                    nUserDefinedId = Convert.ToInt32(EnumGlobalData.Payment);
                    objChild.Code = txtPaymentType.Text.ToString();
                    objChild.Description = txtPaymentType.Text.ToString();
                }

                if (nUserDefinedId != 0)
                {
                    objChild.UserDefinedId = nUserDefinedId;

                    Master objParent = new MasterDA().GetParentbyUserDefinedID(nUserDefinedId);
                    if (objParent != null)
                    {
                        objChild.ParentId = objParent.Id;
                    }
                    else
                    {
                        objChild.ParentId = 0;
                    }
                }
                else
                {
                    objChild.UserDefinedId = 0;
                    objChild.ParentId = 0;
                }

                if (Session["ChildId"] != null)
                {
                    objChild.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    objChild.UpdatedDate = DateTime.Now;
                }
                else
                {
                    objChild.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    objChild.CreatedDate = DateTime.Now;
                }

                if (Session["OwnerId"] != null)
                {
                    objChild.OwnerId = Session["OwnerId"].ToString();
                }
                else
                {
                    objChild.OwnerId = "";
                }


                string username = "";

                if (Session["UserObject"] != null)
                {
                    username = ((UserProfile)Session["UserObject"]).Username;
                }

                string SQL = " update UserProfile set HasLedgerCode = 1  where Username = '" + username + "' ";

                if (Session["ChildId"] == null || Session["ChildId"] == "0")
                {
                    if (new ChildDA().Insert(objChild))
                    {
                        if (IsLedger)
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMDMain(SQL);

                        }
                        txtLedger.Text = "";
                        txtLedgerName.Text = "";
                        txtTypeofContact.Text = "";
                        txtPaymentType.Text = "";
                        Session["ChildId"] = null;
                        if (IsContact == true)
                        {
                            FillContactGrid();
                        }
                        else if (IsLedger == true)
                        {
                            FillLedgerGrid();
                        }
                        else if (IsPayType == true)
                        {
                            FillPaymentGrid();
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("Data not saved!", this);
                    }
                }
                else
                {
                    if (new ChildDA().Update(objChild))
                    {
                        if (IsLedger)
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMDMain(SQL);
                        }
                        txtLedger.Text = "";
                        txtLedgerName.Text = "";
                        txtTypeofContact.Text = "";
                        txtPaymentType.Text = "";
                        Session["ChildId"] = null;
                        if (IsContact == true)
                        {
                            FillContactGrid();
                        }
                        else if (IsLedger == true)
                        {
                            FillLedgerGrid();
                        }
                        else if (IsPayType == true)
                        {
                            FillPaymentGrid();
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("Data not updated!", this);
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }
        private void SaveCardData()
        {
            try
            {
                PaymentInformation objPaymentInformation = new PaymentInformation();

                if (Session["CardId"] != null && Convert.ToInt32(Session["CardId"]) > 0)
                {
                    objPaymentInformation.Id = Convert.ToInt32(Session["CardId"].ToString());
                }
                if (Session["Username"] != null)
                {
                    if (Session["Username"].ToString() != string.Empty)
                    {
                        objPaymentInformation.Username = Session["Username"].ToString();
                    }
                    else
                    {
                        objPaymentInformation.Username = "";
                    }
                }

                if (Session["OwnerId"] != null)
                {
                    objPaymentInformation.OwnerId = Session["OwnerId"].ToString();
                }
                else
                {
                    objPaymentInformation.OwnerId = "";
                }

                objPaymentInformation.AccountName = txtCardAccountName.Text.ToString();
                objPaymentInformation.Address = txtCardAddress.Text.ToString();
                objPaymentInformation.Address1 = "";
                objPaymentInformation.City = txtCardCity.Text.ToString();
                objPaymentInformation.State = ddlState.SelectedValue;
                objPaymentInformation.Zip = txtCardZip.Text.ToString();

                if (rdoCardType.Items[0].Selected == true)
                {
                    objPaymentInformation.IsCheckingAccount = false;
                  
                    objPaymentInformation.CardNumber = txtCardNumber.Text.ToString();
                    objPaymentInformation.CVS = txtCVS.Text.ToString();
                    objPaymentInformation.Month = ddlMonth.SelectedValue;
                    objPaymentInformation.Year = ddlYear.SelectedValue;

                    if (objPaymentInformation.CardNumber.Trim().Length > 4)
                    {
                        objPaymentInformation.LastFourDigitCard = objPaymentInformation.CardNumber.Substring(objPaymentInformation.CardNumber.Length - 4, 4);
                    }
                    else
                    {
                        objPaymentInformation.LastFourDigitCard = "";
                    }

                    objPaymentInformation.AccountNo = "";
                    objPaymentInformation.RoutingNo = "";
                    objPaymentInformation.CheckNo = "";
                }
                else
                {
                    objPaymentInformation.IsCheckingAccount = true;
                    objPaymentInformation.AccountNo = txtCheckingAccount.Text.ToString();
                    objPaymentInformation.RoutingNo = txtRoutingNo.Text.ToString();
                    objPaymentInformation.CheckNo = "";
                  
                    objPaymentInformation.CardNumber = "";
                    objPaymentInformation.CVS = "";
                    objPaymentInformation.Month = "";
                    objPaymentInformation.Year = "";
                    objPaymentInformation.LastFourDigitCard = "";

                    if (objPaymentInformation.AccountNo.Trim().Length >= 4)
                    {
                        objPaymentInformation.LastFourDigitCard = objPaymentInformation.AccountNo.Substring(objPaymentInformation.AccountNo.Length - 4, 4);
                    }
                    else
                    {
                        objPaymentInformation.LastFourDigitCard = "";
                    }
                }



                if (Session["CardId"] == null || Session["CardId"] == "0")
                {
                    if (new PaymentInformationDA().Insert(objPaymentInformation))
                    {
                        if (new AdminPaymentInformationDA().Insert(objPaymentInformation))
                        {
                        }

                        FillCardGrid();
                        rdoCardType.SelectedValue = null;
                        txtCardAccountName.Text = "";
                        txtCardAddress.Text = "";
                        txtCardCity.Text = "";
                        txtCardZip.Text = "";
                        txtCardNumber.Text = "";
                        txtCVS.Text = "";
                        txtRoutingNo.Text = "";
                        txtRoutingNo2.Text = "";
                        txtCheckingAccount.Text = "";
                        txtCheckingAccount2.Text = "";
                        Session["CardId"] = null;
                        Utility.DisplayMsg("Successfully saved!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Data not saved!", this);
                    }
                }
                else
                {
                    if (new PaymentInformationDA().Update(objPaymentInformation))
                    {
                        PaymentInformation objPaymentAdmin = new AdminPaymentInformationDA().GetByOwnerCardAndCheckingAccount(objPaymentInformation.OwnerId, objPaymentInformation.CardNumber, objPaymentInformation.AccountNo);
                        if (objPaymentAdmin != null)
                        {
                            objPaymentInformation.Id = objPaymentAdmin.Id;
                            if (new AdminPaymentInformationDA().Update(objPaymentInformation))
                            {
                            }
                        }
                        else
                        {
                            if (new AdminPaymentInformationDA().Insert(objPaymentInformation))
                            {
                            }
                        }

                        FillCardGrid();

                        rdoCardType.SelectedValue = null;
                        txtCardAccountName.Text = "";
                        txtCardAddress.Text = "";
                        txtCardCity.Text = "";
                        txtCardZip.Text = "";
                        txtCardNumber.Text = "";
                        txtCVS.Text = "";
                        txtRoutingNo.Text = "";
                        txtRoutingNo2.Text = "";
                        txtCheckingAccount.Text = "";
                        txtCheckingAccount2.Text = "";

                        Session["CardId"] = null;
                        Utility.DisplayMsg("Successfully updated!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Data not updated!", this);
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }
        private void FillGlobalControls()
        {
            try
            {
                SystemInformation obj = new AdminSystemInformationDA().GetGlobalInfo();

                if(obj != null)
                {
                    if (obj.ApplicationFee != null && obj.ApplicationFee.ToString() != string.Empty)
                    {
                        txtApplicationFee.Text = Convert.ToDecimal(obj.ApplicationFee).ToString("#.00");
                    }

                    if (obj.ScreeningFee != null && obj.ScreeningFee.ToString() != string.Empty)
                    {
                        txtScreeningFee.Text = Convert.ToDecimal(obj.ScreeningFee).ToString("#.00");
                    }
                    if (obj.LateRentPercentage != null && obj.LateRentPercentage.ToString() != string.Empty)
                    {
                        txtLateFee.Text = Convert.ToDecimal(obj.LateRentPercentage).ToString("#.00");
                    }
                    if (obj.ChargeBackFee != null && obj.ChargeBackFee.ToString() != string.Empty)
                    {
                        txtChargeBackFee.Text = Convert.ToDecimal(obj.ChargeBackFee).ToString("#.00");
                    }

                    if (obj.FeeType != null)
                    {
                        if (Convert.ToInt32(obj.FeeType) == 1)
                        {
                            rdoFeeType.Items[0].Selected = true;
                            if (obj.FeePercentage != null && obj.FeePercentage.ToString() != string.Empty)
                            {
                                txtFeeAmount.Text = Convert.ToDecimal(obj.FeePercentage).ToString("#.00");
                            }
                        }
                        else if (Convert.ToInt32(obj.FeeType) == 2)
                        {
                            rdoFeeType.Items[1].Selected = true;
                            if (obj.FeeFlatAmount != null && obj.FeeFlatAmount.ToString() != string.Empty)
                            {
                                txtFeeAmount.Text = Convert.ToDecimal(obj.FeeFlatAmount).ToString("#.00");
                            }
                        }
                    }

                   

                    if (obj.FeeTypeCheck != null)
                    {
                        if (Convert.ToInt32(obj.FeeTypeCheck) == 1)
                        {
                            rdoAccount.Items[0].Selected = true;
                            if (obj.FeePercentageCheck != null && obj.FeePercentageCheck.ToString() != string.Empty)
                            {
                                txtCheckAmount.Text = Convert.ToDecimal(obj.FeePercentageCheck).ToString("#.00");
                            }
                        }
                        else if (Convert.ToInt32(obj.FeeTypeCheck) == 2)
                        {
                            rdoAccount.Items[1].Selected = true;
                            if (obj.FeeFlatAmountCheck != null && obj.FeeFlatAmountCheck.ToString() != string.Empty)
                            {
                                txtCheckAmount.Text = Convert.ToDecimal(obj.FeeFlatAmountCheck).ToString("#.00");
                            }
                        }
                    }

                    if (obj.UnitPrice != null && obj.UnitPrice.ToString() != string.Empty)
                    {
                        txtUnitCost.Text = Convert.ToDecimal(obj.UnitPrice).ToString("#.00");
                    }

                    txtApplicationFee.Enabled = false;
                    txtScreeningFee.Enabled = false;
                    txtLateFee.Enabled = false;
                    txtChargeBackFee.Enabled = false;
                    txtFeeAmount.Enabled = false;
                    txtCheckAmount.Enabled = false;
                    rdoAccount.Enabled = false;
                    rdoFeeType.Enabled = false;
                    txtUnitCost.Enabled = false;
                }
            }
            catch(Exception ex)
            {

            }
        }

        #endregion
    }
}