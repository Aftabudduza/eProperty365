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
    public partial class AddOwnerSystem_back : System.Web.UI.Page
    {
        public String errStr = String.Empty;
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["AddSystemId"] = null;
                Session["CardId"] = null;
                FillDropdowns();
                FillContactGrid();
                FillLedgerGrid();
                FillPaymentGrid();
                FillCardGrid();

                if (Session["Username"] != null)
                {
                    SystemInformation objSystem = new SystemInformationDA().GetByUsername(Session["Username"].ToString());
                    if (objSystem != null)
                    {
                        Session["AddSystemId"] = objSystem.Id;
                        FillControls(Convert.ToInt32(objSystem.Id));
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            errStr = string.Empty;
            errStr = Validate_Control();
            if (errStr.Length <= 0)
            {
                try
                {
                    SystemInformation objSystemInformation = new SystemInformation();
                    objSystemInformation = SetData(objSystemInformation);
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

                            Session["AddSystemId"] = null;
                            ClearControls();
                            Utility.DisplayMsg("System Information Created successfully!", this);
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
                            if (new AdminSystemInformationDA().Update(objSystemInformation))
                            {
                                Utility.RunCMDMain(SQL);
                                Utility.RunCMDMain(SQLPayment);
                            }

                            Session["AddSystemId"] = null;
                            ClearControls();
                            Utility.DisplayMsg("System Information updated successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("System Information not updated!", this);
                        }
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

                        if (obj.AccountName != null && obj.AccountName.ToString() != string.Empty)
                        {
                            txtCardAccountName.Text = obj.AccountName;
                        }

                        if (obj.Address != null && obj.Address.ToString() != string.Empty)
                        {
                            txtCardAddress.Text = obj.Address;
                        }

                        if (obj.City != null && obj.City.ToString() != string.Empty)
                        {
                            txtCardCity.Text = obj.City;
                        }

                        if (obj.Zip != null && obj.Zip.ToString() != string.Empty)
                        {
                            txtCardZip.Text = obj.Zip;
                        }

                        if (obj.CardNumber != null && obj.CardNumber.ToString() != string.Empty)
                        {
                            txtCardNumber.Text = obj.CardNumber;
                        }

                        if (obj.CVS != null && obj.CVS.ToString() != string.Empty)
                        {
                            txtCVS.Text = obj.CVS;
                        }

                        if (obj.State != null && obj.State.ToString() != string.Empty)
                        {
                            ddlState.SelectedValue = obj.State;
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
            txtEmailServer.Text = "";
            txtEmailUserName.Text = "";
            txtEmailPassword.Text = "";
            txtGateway.Text = "";
            txtTransactionKey.Text = "";
            txtUserId.Text = "";
            txtUserPassword.Text = "";
            txtGateway1.Text = "";
            txtTransactionKey1.Text = "";
            txtUserId1.Text = "";
            txtUserPassword1.Text = "";
            txtApplicationFee.Text = "";
            txtFeeAmount.Text = "";
            txtMonthlyCharge.Text = "";
        }
        public string Validate_Control()
        {
            try
            {
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
                if (Session["AddSystemId"] != null && Convert.ToInt32(Session["AddSystemId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["AddSystemId"].ToString());
                }
                if (Session["Username"] != null)
                {
                    if (Session["Username"].ToString() != string.Empty)
                    {
                        obj.Username = Session["Username"].ToString();
                    }
                    else
                    {
                        obj.Username = "";
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
                        obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                    }
                }                
                else
                {
                    obj.OwnerId = "";
                }

                if (!string.IsNullOrEmpty(txtWebUrl.Text.ToString()))
                {
                    obj.Website = txtWebUrl.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Website = "";
                }
                if (!string.IsNullOrEmpty(txtEmailServer.Text.ToString()))
                {
                    obj.EmailServer1 = txtEmailServer.Text.ToString();
                }
                else
                {
                    obj.EmailServer1 = "";
                }
                if (!string.IsNullOrEmpty(txtEmailUserName.Text.ToString()))
                {
                    obj.EmailUser1 = txtEmailUserName.Text.ToString();
                }
                else
                {
                    obj.EmailUser1 = "";
                }
                if (!string.IsNullOrEmpty(txtEmailPassword.Text.ToString()))
                {
                    obj.EmailPassword1 = txtEmailPassword.Text.ToString();
                }
                else
                {
                    obj.EmailPassword1 = "";
                }

                if (!string.IsNullOrEmpty(txtGateway.Text.ToString()))
                {
                    obj.SecurityLink = txtGateway.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.SecurityLink = "";
                }
                if (!string.IsNullOrEmpty(txtTransactionKey.Text.ToString()))
                {
                    obj.SecurityKey = txtTransactionKey.Text.ToString();
                }
                else
                {
                    obj.SecurityKey = "";
                }
                if (!string.IsNullOrEmpty(txtUserId.Text.ToString()))
                {
                    obj.SecurityUser = txtUserId.Text.ToString();
                }
                else
                {
                    obj.SecurityUser = "";
                }
                if (!string.IsNullOrEmpty(txtUserPassword.Text.ToString()))
                {
                    obj.SecurityPassword = txtUserPassword.Text.ToString();
                }
                else
                {
                    obj.SecurityPassword = "";
                }
                if (!string.IsNullOrEmpty(txtGateway1.Text.ToString()))
                {
                    obj.CreditCardLink = txtGateway1.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.CreditCardLink = "";
                }
                if (!string.IsNullOrEmpty(txtTransactionKey1.Text.ToString()))
                {
                    obj.CreditCardKey = txtTransactionKey1.Text.ToString();
                }
                else
                {
                    obj.CreditCardKey = "";
                }
                if (!string.IsNullOrEmpty(txtUserId1.Text.ToString()))
                {
                    obj.CreditCardUser = txtUserId1.Text.ToString();
                }
                else
                {
                    obj.CreditCardUser = "";
                }
                if (!string.IsNullOrEmpty(txtUserPassword1.Text.ToString()))
                {
                    obj.CreditCardPassword = txtUserPassword1.Text.ToString();
                }
                else
                {
                    obj.CreditCardPassword = "";
                }

                if (!string.IsNullOrEmpty(txtApplicationFee.Text.ToString()))
                {
                    obj.ApplicationFee = Convert.ToDecimal(txtApplicationFee.Text.ToString());
                }
                else
                {
                    obj.ApplicationFee = 0;
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

                if (!string.IsNullOrEmpty(this.uplProduct.FileName))
                {
                    //read the file in
                    string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\Owner\\");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fileName = this.uplProduct.FileName;
                    string nFile = Path.Combine(filePath, fileName);
                    Session["FileName"] = fileName;
                    obj.DocumentLink = fileName;
                    try
                    {
                        if (System.IO.File.Exists(nFile))
                        {
                            System.IO.File.Delete(nFile);
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                    uplProduct.SaveAs(nFile);
                }
                else
                {
                    if(Session["FileName"] != null)
                    {
                        obj.DocumentLink = Session["FileName"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return obj;
        }
        private void FillControls(int nId)
        {
            try
            {
                if (nId > 0)
                {
                    SystemInformation obj = new SystemInformationDA().GetByID(nId);
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

                        if (obj.EmailServer1 != null && obj.EmailServer1.ToString() != string.Empty)
                        {
                            txtEmailServer.Text = obj.EmailServer1;
                        }
                        else
                        {
                            txtEmailServer.Text = "";
                        }

                        if (obj.EmailUser1 != null && obj.EmailUser1.ToString() != string.Empty)
                        {
                            txtEmailUserName.Text = obj.EmailUser1;
                        }
                        else
                        {
                            txtEmailUserName.Text = "";
                        }

                       
                        if (obj.EmailPassword1 != null && obj.EmailPassword1.ToString() != string.Empty)
                        {
                            txtEmailPassword.Text = obj.EmailPassword1;
                            txtEmailPassword.Attributes.Add("value", obj.EmailPassword1.ToString().Trim());
                            
                        }
                        else
                        {
                            txtEmailPassword.Text = "";
                        }

                        if (obj.SecurityLink != null && obj.SecurityLink.ToString() != string.Empty)
                        {
                            txtGateway.Text = obj.SecurityLink;
                        }
                        else
                        {
                            txtGateway.Text = "";
                        }

                      
                        if (obj.SecurityKey != null && obj.SecurityKey.ToString() != string.Empty)
                        {
                            txtTransactionKey.Text = obj.SecurityKey;
                        }
                        else
                        {
                            txtTransactionKey.Text = "";
                        }                       

                        if (obj.SecurityUser != null && obj.SecurityUser.ToString() != string.Empty)
                        {
                            txtUserId.Text = obj.SecurityUser;
                        }
                        else
                        {
                            txtUserId.Text = "";
                        }
                        if (obj.SecurityPassword != null && obj.SecurityPassword.ToString() != string.Empty)
                        {
                            txtUserPassword.Text = obj.SecurityPassword;
                            txtUserPassword.Attributes.Add("value", obj.SecurityPassword.ToString().Trim());
                        }
                        else
                        {
                            txtUserPassword.Text = "";
                        }

                        if (obj.CreditCardLink != null && obj.CreditCardLink.ToString() != string.Empty)
                        {
                            txtGateway1.Text = obj.CreditCardLink;
                        }
                        else
                        {
                            txtGateway1.Text = "";
                        }

                        if (obj.CreditCardKey != null && obj.CreditCardKey.ToString() != string.Empty)
                        {
                            txtTransactionKey1.Text = obj.CreditCardKey;
                        }
                        else
                        {
                            txtTransactionKey1.Text = "";
                        }

                        if (obj.CreditCardUser != null && obj.CreditCardUser.ToString() != string.Empty)
                        {
                            txtUserId1.Text = obj.CreditCardUser;
                        }
                        else
                        {
                            txtUserId1.Text = "";
                        }
                        if (obj.CreditCardPassword != null && obj.CreditCardPassword.ToString() != string.Empty)
                        {
                            txtUserPassword1.Text = obj.CreditCardPassword;
                            txtUserPassword1.Attributes.Add("value", obj.CreditCardPassword.ToString().Trim());
                        }
                        else
                        {
                            txtUserPassword1.Text = "";
                        }

                        if (obj.ApplicationFee != null && obj.ApplicationFee.ToString() != string.Empty)
                        {
                            txtApplicationFee.Text = Convert.ToDecimal(obj.ApplicationFee).ToString("#.00");
                        }

                        if (obj.FeePercentage != null && obj.FeePercentage.ToString() != string.Empty)
                        {
                            txtFeeAmount.Text = Convert.ToDecimal(obj.FeePercentage).ToString("#.00");
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

                        if (obj.DocumentLink != null && obj.DocumentLink != string.Empty)
                        {
                            lblDoc.InnerText = obj.DocumentLink;
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

                        btnSave.Text = "Update";

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

                if (rdoCardType.Items[0].Selected == true)
                {
                    objPaymentInformation.IsCheckingAccount = false;
                    objPaymentInformation.AccountName = txtCardAccountName.Text.ToString();
                    objPaymentInformation.Address = txtCardAccountName.Text.ToString();
                    objPaymentInformation.Address1 = "";
                    objPaymentInformation.City = txtCardCity.Text.ToString();
                    objPaymentInformation.State = "";
                    objPaymentInformation.Zip = txtCardZip.Text.ToString();
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
                    objPaymentInformation.AccountName = "";
                    objPaymentInformation.Address = "";
                    objPaymentInformation.Address1 = "";
                    objPaymentInformation.City = "";
                    objPaymentInformation.State = "";
                    objPaymentInformation.Zip = "";
                    objPaymentInformation.CardNumber = "";
                    objPaymentInformation.CVS = "";
                    objPaymentInformation.Month = "";
                    objPaymentInformation.Year = "";
                    objPaymentInformation.LastFourDigitCard = "";

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


        #endregion
    }
}