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
    public partial class AddOwner : System.Web.UI.Page
    {
        public String errStr = String.Empty;
        private Regex reEmail = new Regex("^(?:[0-9A-Z_-]+(?:\\.[0-9A-Z_-]+)*@[0-9A-Z-]+(?:\\.[0-9A-Z-]+)*(?:\\.[A-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        #region Events General
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                   
                    Session["AddOwnerId"] = null;
                    Session["AddSystemId"] = null;
                    Session["CardId"] = null;
                    Session["ContactId"] = null;
                    Session["AddUserId"] = null;
                    Session["AddPropertyManagerId"] = null;
                    FillDropdownsSystem();
                    FillContactGridSystem();
                    FillLedgerGridSystem();
                    FillPaymentGridSystem();
                    FillCardGridSystem();
                    FillContactType();
                    FillContacts();
                    FillUsers();
                    FillDropdowns();
                    FillVendor();
                    FillDropdownsPropertyManager();

                    if (Session["Username"] != null)
                    {
                        SystemInformation objSystem = new SystemInformationDA().GetByUsername(Session["Username"].ToString());
                        if (objSystem != null)
                        {
                            Session["AddSystemId"] = objSystem.Id;
                            FillControlsSystem(Convert.ToInt32(objSystem.Id));
                        }
                    }

                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["AddOwnerId"].ToString());
                    }
                    catch
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                       
                        Session["AddOwnerId"] = CId;
                        FillControls(Convert.ToInt32(Session["AddOwnerId"].ToString()));
                    }

                    if (Session["OwnerId"] != null)
                    {
                        OwnerProfile objOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
                        if (objOwner != null)
                        {
                            //lblHeadline.InnerText = "Change Owner Profile";
                            Session["AddOwnerId"] = objOwner.Id;
                            FillControls(Convert.ToInt32(objOwner.Id));
                        }

                        PropertyManagerProfile objPropertyManager = new PropertyManagerProfileDA().GetByOwner(Session["OwnerId"].ToString());
                        if (objPropertyManager != null)
                        {
                            Session["AddPropertyManagerId"] = objPropertyManager.Id;
                            FillControlsPropertyManager(Convert.ToInt32(objPropertyManager.Id));
                        }

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
                    OwnerProfile objOwnerProfile = new OwnerProfile();
                    objOwnerProfile = SetData(objOwnerProfile);
                    string username = "";
                    if (Session["UserObject"] != null)
                    {
                        username = ((UserProfile)Session["UserObject"]).Username;
                    }
                    string SQL = " update UserProfile set HasOwnerProfile = 1, OwnerId = '" + objOwnerProfile.Serial + "'  where Username = '" + username + "' ";

                    string SQLSystem = " update SystemInformation set OwnerId = '" + objOwnerProfile.Serial + "'  where Username = '" + username + "' ";
                    string SQLPayment = " update PaymentInformation set OwnerId = '" + objOwnerProfile.Serial + "'  where Username = '" + username + "' ";

                    if (Session["AddOwnerId"] == null || Session["AddOwnerId"].ToString() == "0")
                    {
                        if (new OwnerProfileDA().Insert(objOwnerProfile))
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMD(SQLSystem);
                            Utility.RunCMD(SQLPayment);
                            if (new AdminOwnerProfileDA().Insert(objOwnerProfile))
                            {
                                Utility.RunCMDMain(SQL);
                                Utility.RunCMDMain(SQLSystem);
                                Utility.RunCMDMain(SQLPayment);
                            }
                           
                            Session["AddOwnerId"] = null;
                            ClearControls();
                            Utility.DisplayMsg("Owner Created successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Owner not Created!", this);
                        }
                    }
                    else
                    {
                        if (new OwnerProfileDA().Update(objOwnerProfile))
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMD(SQLSystem);
                            Utility.RunCMD(SQLPayment);
                            if (new AdminOwnerProfileDA().Update(objOwnerProfile))
                            {
                                Utility.RunCMDMain(SQL);
                                Utility.RunCMDMain(SQLSystem);
                                Utility.RunCMDMain(SQLPayment);
                            }

                            Session["AddOwnerId"] = null;
                            ClearControls();
                            Utility.DisplayMsg("Owner updated successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Owner not updated!", this);
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

        #endregion

        #region Method General
        private void FillDropdowns()
        {
            try
            {
                ddlCountry.Items.Clear();
                ddlCountry.AppendDataBoundItems = true;
                ddlCountry.Items.Add(new ListItem("Select Country", "-1"));              
                ddlCountry.DataSource = new CountryDA().GetAllRefCountries();
                ddlCountry.DataTextField = "COUNTRYNAME";
                ddlCountry.DataValueField = "COUNTRY";
                ddlCountry.DataBind();
                ddlCountry.SelectedValue = "US";
            }
            catch (Exception ex)
            {

            }
            try
            {
                ddlCompanySate.Items.Clear();
                ddlCompanySate.AppendDataBoundItems = true;
                ddlCompanySate.DataSource = new StateDA().GetAllRefStates();
                ddlCompanySate.DataTextField = "STATENAME";
                ddlCompanySate.DataValueField = "STATE";
                ddlCompanySate.DataBind();


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
                ddlStateVendor.Items.Clear();
                ddlStateVendor.AppendDataBoundItems = true;
                ddlStateVendor.DataSource = new StateDA().GetAllRefStates();
                ddlStateVendor.DataTextField = "STATENAME";
                ddlStateVendor.DataValueField = "STATE";
                ddlStateVendor.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        private void ClearControls()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtCompanyName.Text = "";
            txtDate.Text = "";
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtRegion.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
            txtEIN.Text = "";
            rdoEIN.SelectedValue = null;
            rdoCompanyType.SelectedValue = null;
            btnSave.Text = "Create Owner Account";
            lblHeadline.InnerText = "Create / Change Owner Profile";
        }
        public string Validate_Control()
        {
            try
            {
                if (txtFirstName.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter First Name" + Environment.NewLine;
                }               
                if (txtLastName.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Last Name" + Environment.NewLine;
                }
                if (txtCompanyName.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Company Name" + Environment.NewLine;
                }
                if (txtAddress.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Address" + Environment.NewLine;
                }
                if (ddlCountry.SelectedValue == "-1")
                {
                    errStr += "Please select Country" + Environment.NewLine;
                }
                if (txtCity.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter City" + Environment.NewLine;
                }
                if (txtZip.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Zip Code" + Environment.NewLine;
                }

                if (txtLateFee.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Late Fee" + Environment.NewLine;
                }
                else
                {
                    if (Convert.ToDecimal(txtLateFee.Text.ToString().Trim()) <= 5)
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
                    if (Convert.ToDecimal(txtChargeBackFee.Text.ToString().Trim()) <= 5)
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
        private OwnerProfile SetData(OwnerProfile obj)
        {
            try
            {
                if (Session["AddOwnerId"] != null && Convert.ToInt32(Session["AddOwnerId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["AddOwnerId"].ToString());
                }

                if (!string.IsNullOrEmpty(txtFirstName.Text.ToString()))
                {
                    obj.FirstName = txtFirstName.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.FirstName = "";
                }
                if (!string.IsNullOrEmpty(txtLastName.Text.ToString()))
                {
                    obj.LastName = txtLastName.Text.ToString();
                }
                else
                {
                    obj.LastName = "";
                }
                if (!string.IsNullOrEmpty(txtCompanyName.Text.ToString()))
                {
                    obj.CompanyName = txtCompanyName.Text.ToString();
                }
                else
                {
                    obj.CompanyName = "";
                }
                if (ddlCompanySate.SelectedValue != "-1")
                {
                    obj.FoundedState = ddlCompanySate.SelectedValue.ToString();
                }
                else
                {
                    obj.FoundedState = "";
                }

                if (rdoCompanyType.Items[0].Selected == true)
                {
                    obj.OrganizationType = 1;
                }
                else if (rdoCompanyType.Items[1].Selected == true)
                {
                    obj.OrganizationType = 2;
                }
                else if (rdoCompanyType.Items[2].Selected == true)
                {
                    obj.OrganizationType = 3;
                }
                else if (rdoCompanyType.Items[3].Selected == true)
                {
                    obj.OrganizationType = 4;
                }
                else if (rdoCompanyType.Items[4].Selected == true)
                {
                    obj.OrganizationType = 5;
                }

                obj.FoundedOn = txtDate.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtDate.Text.ToString().Trim()) : DateTime.Now;

                if (!string.IsNullOrEmpty(txtAddress.Text.ToString()))
                {
                    obj.Address = txtAddress.Text.ToString();
                }
                else
                {
                    obj.Address = "";
                }
                if (!string.IsNullOrEmpty(txtAddress1.Text.ToString()))
                {
                    obj.Address1 = txtAddress1.Text.ToString();
                }
                else
                {
                    obj.Address1 = "";
                }


                if (ddlCountry.SelectedValue != "-1")
                {
                    obj.Country = ddlCountry.SelectedValue.ToString();
                }
                else
                {
                    obj.Country = "";
                }
                if (!string.IsNullOrEmpty(txtRegion.Text.ToString()))
                {
                    obj.Region = txtRegion.Text.ToString();
                }
                else
                {
                    obj.Region = "";
                }

                if (ddlState.SelectedValue != "-1")
                {
                    obj.State = ddlState.SelectedValue.ToString();
                }
                else
                {
                    obj.State = "";
                }
                if (!string.IsNullOrEmpty(txtCity.Text.ToString()))
                {
                    obj.City = txtCity.Text.ToString();
                }
                else
                {
                    obj.City = "";
                }

                if (!string.IsNullOrEmpty(txtZip.Text.ToString()))
                {
                    obj.Zip = txtZip.Text.ToString();
                }
                else
                {
                    obj.Zip = "";
                }
                if (rdoEIN.Items[0].Selected == true)
                {
                    obj.TypeOfNumber = 1;
                }
                else if (rdoEIN.Items[1].Selected == true)
                {
                    obj.TypeOfNumber = 2;
                }

                if (!string.IsNullOrEmpty(txtEIN.Text.ToString()))
                {
                    obj.FedNumber = txtEIN.Text.ToString();
                }
                else
                {
                    obj.FedNumber = "";
                }

                if (obj.FedNumber.Trim().Length > 4)
                {
                    obj.FedLast4Digit = obj.FedNumber.Substring(obj.FedNumber.Length - 4, 4);
                }
                else
                {
                    obj.FedLast4Digit = "";
                }

                if (Session["AddOwnerId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;                    
                }
                else
                {
                    obj.Serial = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                }

                if (Session["AddOwnerId"] != null)
                {
                    if (Session["OwnerId"] != null)
                    {
                        if (Session["OwnerId"].ToString() != string.Empty)
                        {
                            OwnerProfile TempOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
                            if (TempOwner == null)
                            {
                                obj.Serial = Session["OwnerId"].ToString();
                            }
                            else
                            {
                                obj.Serial = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                            }
                        }
                        else
                        {
                            obj.Serial = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                        }
                    }
                    else
                    {
                        obj.Serial = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                    }
                }
                

                obj.IsDelete = false;

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
                    OwnerProfile obj = new OwnerProfileDA().GetByID(nId);
                    if (obj != null)
                    {
                        Session["AddOwnerId"] = obj.Id;
                        Session["OwnerId"] = obj.Serial;
                        if (obj.FirstName != null && obj.FirstName.ToString() != string.Empty)
                        {
                            txtFirstName.Text = obj.FirstName;
                        }
                        else
                        {
                            txtFirstName.Text = "";
                        }

                        if (obj.LastName != null && obj.LastName.ToString() != string.Empty)
                        {
                            txtLastName.Text = obj.LastName;
                        }
                        else
                        {
                            txtLastName.Text = "";
                        }

                        if (obj.OrganizationType != null)
                        {
                            if (Convert.ToInt32(obj.OrganizationType) == 1)
                            {
                                rdoCompanyType.Items[0].Selected = true;
                            }
                            else if (Convert.ToInt32(obj.OrganizationType) == 2)
                            {
                                rdoCompanyType.Items[1].Selected = true;
                            }
                            else if (Convert.ToInt32(obj.OrganizationType) == 3)
                            {
                                rdoCompanyType.Items[2].Selected = true;
                            }
                            else if (Convert.ToInt32(obj.OrganizationType) == 4)
                            {
                                rdoCompanyType.Items[3].Selected = true;
                            }
                            else if (Convert.ToInt32(obj.OrganizationType) == 5)
                            {
                                rdoCompanyType.Items[4].Selected = true;
                            }
                        }                       

                        if (obj.CompanyName != null && obj.CompanyName.ToString() != string.Empty)
                        {
                            txtCompanyName.Text = obj.CompanyName;
                        }
                        else
                        {
                            txtCompanyName.Text = "";
                        }

                        if (obj.FoundedOn != null && obj.FoundedOn  != DateTime.MinValue)
                        {
                            txtDate.Text  = Convert.ToDateTime(obj.FoundedOn).ToString("MMM/yyyy");
                        }
                        if (obj.FoundedState != null && obj.FoundedState.ToString() != string.Empty)
                        {
                            ddlCompanySate.SelectedValue = obj.FoundedState.ToString();
                        }
                        if (obj.Address != null && obj.Address.ToString() != string.Empty)
                        {
                            txtAddress.Text = obj.Address;
                        }
                        else
                        {
                            txtAddress.Text = "";
                        }
                        if (obj.Address1 != null && obj.Address1.ToString() != string.Empty)
                        {
                            txtAddress1.Text = obj.Address;
                        }
                        else
                        {
                            txtAddress1.Text = "";
                        }

                        if (obj.Country != null && obj.Country.ToString() != string.Empty)
                        {
                            ddlCountry.SelectedValue = obj.Country.ToString();
                        }
                        if (obj.Region != null && obj.Region.ToString() != string.Empty)
                        {
                            txtRegion.Text = obj.Region;
                        }
                        else
                        {
                            txtRegion.Text = "";
                        }


                        if (obj.State != null && obj.State.ToString() != string.Empty)
                        {
                            ddlState.SelectedValue = obj.State.ToString();
                        }

                        if (obj.City != null && obj.City.ToString() != string.Empty)
                        {
                            txtCity.Text = obj.City;
                        }
                        else
                        {
                            txtCity.Text = "";
                        }
                        if (obj.Zip != null && obj.Zip.ToString() != string.Empty)
                        {
                            txtZip.Text = obj.Zip;
                        }
                        else
                        {
                            txtZip.Text = "";
                        }

                        if (obj.FedNumber != null && obj.FedNumber.ToString() != string.Empty)
                        {
                            txtEIN.Text = obj.FedNumber;
                        }
                        else
                        {
                            txtEIN.Text = "";
                        }

                        if (obj.TypeOfNumber != null)
                        {
                            if (Convert.ToInt32(obj.TypeOfNumber) == 1)
                            {
                                rdoEIN.Items[0].Selected = true;
                            }
                            else if (Convert.ToInt32(obj.TypeOfNumber) == 2)
                            {
                                rdoEIN.Items[1].Selected = true;
                            }
                        }

                        if (obj.Serial != null && obj.Serial.ToString() != string.Empty)
                        {
                            lblId.Text = obj.Serial;
                        }

                        btnSave.Text = "Update Owner Account";

                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        #endregion

        #region Events  System     
        protected void btnSubmitSystem_Click(object sender, EventArgs e)
        {
            errStr = string.Empty;
            errStr = Validate_Control();
            if (errStr.Length <= 0)
            {
                try
                {
                    SystemInformation objSystemInformation = new SystemInformation();
                    objSystemInformation = SetDataSystem(objSystemInformation);
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
                            ClearControlsSystem();
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
                            ClearControlsSystem();
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
        protected void btnCloseSystem_Click(object sender, EventArgs e)
        {
            ClearControlsSystem();
        }

        protected void btnAddTypeOfContact_Click(object sender, EventArgs e)
        {
            SaveBasicDataSystem(true, false, false);
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
                    FillContactGridSystem();
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
            SaveBasicDataSystem(false, false, true);
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
                    FillPaymentGridSystem();
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
            SaveBasicDataSystem(false, true, false);
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
                    FillLedgerGridSystem();
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
            SaveCardDataSystem();
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

                        FillCardGridSystem();
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

        #region Method System
        private void FillDropdownsSystem()
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
        private void ClearControlsSystem()
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
      
        private SystemInformation SetDataSystem(SystemInformation obj)
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
                
                //if (!string.IsNullOrEmpty(txtComEmailUser1.Text.ToString()))
                //{
                //    obj.ComUsername1 = txtComEmailUser1.Text.ToString();
                //}
                //else
                //{
                //    obj.ComUsername1 = "";
                //}
                //if (!string.IsNullOrEmpty(txtComEmailAddress1.Text.ToString()))
                //{
                //    obj.ComEmailAddress1 = txtComEmailAddress1.Text.ToString();
                //}
                //else
                //{
                //    obj.ComEmailAddress1 = "";
                //}

                //if (!string.IsNullOrEmpty(txtComEmailUser2.Text.ToString()))
                //{
                //    obj.ComUsername2 = txtComEmailUser2.Text.ToString();
                //}
                //else
                //{
                //    obj.ComUsername2 = "";
                //}
                //if (!string.IsNullOrEmpty(txtComEmailAddress2.Text.ToString()))
                //{
                //    obj.ComEmailAddress2 = txtComEmailAddress2.Text.ToString();
                //}
                //else
                //{
                //    obj.ComEmailAddress2 = "";
                //}
                //if (!string.IsNullOrEmpty(txtComEmailUser3.Text.ToString()))
                //{
                //    obj.ComUsername3 = txtComEmailUser3.Text.ToString();
                //}
                //else
                //{
                //    obj.ComUsername3 = "";
                //}
                //if (!string.IsNullOrEmpty(txtComEmailAddress3.Text.ToString()))
                //{
                //    obj.ComEmailAddress3 = txtComEmailAddress3.Text.ToString();
                //}
                //else
                //{
                //    obj.ComEmailAddress3 = "";
                //}
                //if (!string.IsNullOrEmpty(txtComEmailUser4.Text.ToString()))
                //{
                //    obj.ComUsername4 = txtComEmailUser4.Text.ToString();
                //}
                //else
                //{
                //    obj.ComUsername4 = "";
                //}
                //if (!string.IsNullOrEmpty(txtComEmailAddress4.Text.ToString()))
                //{
                //    obj.ComEmailAddress4 = txtComEmailAddress4.Text.ToString();
                //}
                //else
                //{
                //    obj.ComEmailAddress4 = "";
                //}

                //if (!string.IsNullOrEmpty(txtAccountId.Text.ToString()))
                //{
                //    obj.AccountPackageId = txtAccountId.Text.ToString();
                //}
                //else
                //{
                //    obj.AccountPackageId = "";
                //}

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


                //if (!string.IsNullOrEmpty(txtUnitCost.Text.ToString()))
                //{
                //    obj.UnitPrice = Convert.ToDecimal(txtUnitCost.Text.ToString());
                //}
                //else
                //{
                //    obj.UnitPrice = 0;
                //}
                //if (!string.IsNullOrEmpty(txtTotalUnit.Text.ToString()))
                //{
                //    obj.NoOfUnit = Convert.ToInt32(txtTotalUnit.Text.ToString());
                //}
                //else
                //{
                //    obj.NoOfUnit = 0;
                //}

                //if (rdoAccount.Items[0].Selected == true)
                //{
                //    obj.FeeTypeCheck = 1;
                //    if (!string.IsNullOrEmpty(txtCheckAmount.Text.ToString()))
                //    {
                //        obj.FeePercentageCheck = Convert.ToDecimal(txtCheckAmount.Text.ToString());
                //        obj.FeeFlatAmountCheck = 0;
                //    }
                //    else
                //    {
                //        obj.FeePercentageCheck = 0;
                //        obj.FeeFlatAmountCheck = 0;
                //    }
                //}
                //else if (rdoAccount.Items[1].Selected == true)
                //{
                //    obj.FeeTypeCheck = 2;
                //    if (!string.IsNullOrEmpty(txtCheckAmount.Text.ToString()))
                //    {
                //        obj.FeeFlatAmountCheck = Convert.ToDecimal(txtCheckAmount.Text.ToString());
                //        obj.FeePercentageCheck = 0;
                //    }
                //    else
                //    {
                //        obj.FeeFlatAmountCheck = 0;
                //        obj.FeePercentageCheck = 0;
                //    }
                //}

                //if (!string.IsNullOrEmpty(txtScreeningFee.Text.ToString()))
                //{
                //    obj.ScreeningFee = Convert.ToDecimal(txtScreeningFee.Text.ToString());
                //}
                //else
                //{
                //    obj.ScreeningFee = 0;
                //}
                //if (!string.IsNullOrEmpty(txtLateFee.Text.ToString()))
                //{
                //    obj.LateRentPercentage = Convert.ToDecimal(txtLateFee.Text.ToString());
                //}
                //else
                //{
                //    obj.LateRentPercentage = 0;
                //}

                //if (!string.IsNullOrEmpty(txtChargeBackFee.Text.ToString()))
                //{
                //    obj.ChargeBackFee = Convert.ToDecimal(txtChargeBackFee.Text.ToString());
                //}
                //else
                //{
                //    obj.ChargeBackFee = 0;
                //}


            }
            catch (Exception ex)
            {
            }

            return obj;
        }
        private void FillControlsSystem(int nId)
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

                        //if (obj.ComUsername1 != null && obj.ComUsername1.ToString() != string.Empty)
                        //{
                        //    txtComEmailUser1.Text = obj.ComUsername1;
                        //}
                        //else
                        //{
                        //    txtComEmailUser1.Text = "";
                        //}

                        //if (obj.ComUsername2 != null && obj.ComUsername2.ToString() != string.Empty)
                        //{
                        //    txtComEmailUser2.Text = obj.ComUsername2;
                        //}
                        //else
                        //{
                        //    txtComEmailUser2.Text = "";
                        //}

                        if (Session["UserId"] != null)
                        {
                            UserProfile objUser = new UserProfileDA().GetUserByUserID(Convert.ToInt32(Session["UserId"]));
                            if (objUser != null)
                            {
                                if (objUser.Password != null && objUser.Password.ToString() != string.Empty)
                                {
                                    txtAccountUserPassword.Text = objUser.Password;
                                    txtAccountUserPassword.Attributes.Add("value", objUser.Password.ToString().Trim());

                                    txtDocUserPassword.Text = objUser.Password;
                                    txtDocUserPassword.Attributes.Add("value", objUser.Password.ToString().Trim());
                                }
                                else
                                {
                                    txtAccountUserPassword.Text = "";
                                    txtDocUserPassword.Text = "";
                                }

                                if (objUser.Email != null && objUser.Email.ToString() != string.Empty)
                                {
                                    txtAccountUserEmail.Text = objUser.Email;
                                    txtDocUserEmail.Text = objUser.Email;
                                }
                                else
                                {
                                    txtAccountUserEmail.Text = "";
                                    txtDocUserEmail.Text = "";
                                }

                            }
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

                        //if (obj.UnitPrice != null && obj.UnitPrice.ToString() != string.Empty)
                        //{
                        //    txtUnitCost.Text = Convert.ToDecimal(obj.UnitPrice).ToString("#.00");
                        //}

                        //if (obj.NoOfUnit != null && obj.NoOfUnit.ToString() != string.Empty)
                        //{
                        //    txtTotalUnit.Text = Convert.ToInt32(obj.NoOfUnit).ToString();
                        //}

                        //if (obj.ScreeningFee != null && obj.ScreeningFee.ToString() != string.Empty)
                        //{
                        //    txtScreeningFee.Text = Convert.ToDecimal(obj.ScreeningFee).ToString("#.00");
                        //}
                        //if (obj.LateRentPercentage != null && obj.LateRentPercentage.ToString() != string.Empty)
                        //{
                        //    txtLateFee.Text = Convert.ToDecimal(obj.LateRentPercentage).ToString("#.00");
                        //}
                        //if (obj.ChargeBackFee != null && obj.ChargeBackFee.ToString() != string.Empty)
                        //{
                        //    txtChargeBackFee.Text = Convert.ToDecimal(obj.ChargeBackFee).ToString("#.00");
                        //}

                        //if (obj.FeeTypeCheck != null)
                        //{
                        //    if (Convert.ToInt32(obj.FeeTypeCheck) == 1)
                        //    {
                        //        rdoAccount.Items[0].Selected = true;
                        //        if (obj.FeePercentageCheck != null && obj.FeePercentageCheck.ToString() != string.Empty)
                        //        {
                        //            txtCheckAmount.Text = Convert.ToDecimal(obj.FeePercentageCheck).ToString("#.00");
                        //        }
                        //    }
                        //    else if (Convert.ToInt32(obj.FeeTypeCheck) == 2)
                        //    {
                        //        rdoAccount.Items[1].Selected = true;
                        //        if (obj.FeeFlatAmountCheck != null && obj.FeeFlatAmountCheck.ToString() != string.Empty)
                        //        {
                        //            txtCheckAmount.Text = Convert.ToDecimal(obj.FeeFlatAmountCheck).ToString("#.00");
                        //        }
                        //    }
                        //}

                        btnSaveSystem.Text = "Update";

                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        private void FillPaymentGridSystem()
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
        private void FillContactGridSystem()
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
        private void FillLedgerGridSystem()
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
        private void FillCardGridSystem()
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
        private void SaveBasicDataSystem(bool IsContact, bool IsLedger, bool IsPayType)
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
                            FillContactGridSystem();
                        }
                        else if (IsLedger == true)
                        {
                            FillLedgerGridSystem();
                        }
                        else if (IsPayType == true)
                        {
                            FillPaymentGridSystem();
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
                            FillContactGridSystem();
                        }
                        else if (IsLedger == true)
                        {
                            FillLedgerGridSystem();
                        }
                        else if (IsPayType == true)
                        {
                            FillPaymentGridSystem();
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
        private void SaveCardDataSystem()
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

                        FillCardGridSystem();
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

                        FillCardGridSystem();

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

        #region Events Contact
        protected void btnSubmitContact_Click(object sender, EventArgs e)
        {
            try
            {
                ContactInformation obj = new ContactInformation();
                obj = SetDataContact(obj);
                string username = "";

                if (Session["UserObject"] != null)
                {
                    username = ((UserProfile)Session["UserObject"]).Username;
                }

                string SQL = " update UserProfile set HasContactProfile = 1  where Username = '" + username + "' ";

                if (Session["ContactId"] == null || Session["ContactId"] == "0")
                {
                    if (new ContactInformationDA().Insert(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);
                        Session["ContactId"] = null;
                        ClearControlsContact();
                        FillContacts();
                        Utility.DisplayMsg("Contact saved successfully!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Contact not saved!", this);
                    }
                }
                else
                {
                    if (new ContactInformationDA().Update(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);
                        Session["ContactId"] = null;
                        ClearControlsContact();
                        FillContacts();
                        Utility.DisplayMsg("Contact updated successfully!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Contact not updated!", this);
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }
        protected void btnCloseContact_Click(object sender, EventArgs e)
        {
            ClearControlsContact();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvContactList.Rows[row.RowIndex].FindControl("lblId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new ContactInformationDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillContacts();
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvContactList.Rows[row.RowIndex].FindControl("lblId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                FillControlsContact(Convert.ToInt32(hdId.Text));
            }
        }
        protected void gvContactList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContactList.PageIndex = e.NewPageIndex;
            FillContacts();
        }
        protected void gvContactList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillContacts();
        }
        #endregion

        #region Method Contact
        private void ClearControlsContact()
        {
            txtContactName.Text = "";
            txtContactTitle.Text = "";
            txtNumber.Text = "";
            txtEmail.Text = "";
            txtEmergency.Text = "";
            ddlType.SelectedValue = "-1";
            chkEmergency.Checked = false;
            chkLocation.Checked = false;
            chkSend.Checked = false;
            btnSubmitContact.Text = "Add Contact";

        }
        private void FillContactType()
        {
            try
            {

                ddlType.Items.Clear();
                ddlType.AppendDataBoundItems = true;
                ddlType.Items.Add(new ListItem("Select Type of Contact", "-1"));
                ddlType.SelectedValue = "-1";

                ddlType.DataSource = new ChildDA().GetMonthByParentID((Int32)EnumBasicData.ContactType);

                ddlType.DataTextField = "Description";
                ddlType.DataValueField = "Description";
                ddlType.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        private ContactInformation SetDataContact(ContactInformation obj)
        {
            try
            {
                if (Session["ContactId"] != null && Convert.ToInt32(Session["ContactId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["ContactId"].ToString());
                }

                if ((!string.IsNullOrEmpty(txtContactName.Text.ToString())) && (txtContactName.Text.ToString() != string.Empty))
                {
                    obj.Name = txtContactName.Text.ToString();
                }
                else
                {
                    obj.Name = "";
                }
                if ((!string.IsNullOrEmpty(txtContactTitle.Text.ToString())) && (txtContactTitle.Text.ToString() != string.Empty))
                {
                    obj.Title = txtContactTitle.Text.ToString();
                }
                else
                {
                    obj.Title = "";
                }
                if (ddlType.SelectedValue != "-1")
                {
                    obj.TypeOfContact = ddlType.SelectedValue.ToString();
                }
                else
                {
                    obj.TypeOfContact = "";
                }
                if ((!string.IsNullOrEmpty(txtNumber.Text.ToString())) && (txtNumber.Text.ToString() != string.Empty))
                {
                    obj.Phone = txtNumber.Text.ToString();
                }
                else
                {
                    obj.Phone = "";
                }
                if ((!string.IsNullOrEmpty(txtEmail.Text.ToString())) && (txtEmail.Text.ToString() != string.Empty))
                {
                    obj.Email = txtEmail.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Email = "";
                }

                if ((!string.IsNullOrEmpty(txtEmergency.Text.ToString())) && (txtEmergency.Text.ToString() != string.Empty))
                {
                    obj.EmergencyContact = txtEmergency.Text.ToString();
                }
                else
                {
                    obj.EmergencyContact = "";
                }

                if (chkEmergency.Checked)
                {
                    obj.IsEmergencyContact = true;
                }
                else
                {
                    obj.IsEmergencyContact = false;
                }
                if (chkLocation.Checked)
                {
                    obj.IsLocation = true;
                }
                else
                {
                    obj.IsLocation = false;
                }
                if (chkSend.Checked)
                {
                    obj.IsSentEmail = true;
                }
                else
                {
                    obj.IsSentEmail = false;
                }

                if (Session["OwnerId"] != null)
                {
                    if (Session["OwnerId"].ToString() != string.Empty)
                    {
                        OwnerProfile TempOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
                        if (TempOwner != null)
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
                        obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                    }
                }
                else
                {
                    obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                }

                if (Session["ContactId"] == null || obj.LocationId == null || obj.LocationId == string.Empty)
                {
                    obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
                }

            }
            catch (Exception ex)
            {
            }

            return obj;
        }
        private void FillControlsContact(int nId)
        {
            try
            {
                if (nId > 0)
                {
                    ContactInformation obj = new ContactInformationDA().GetbyID(nId);
                    if (obj != null)
                    {
                        Session["ContactId"] = obj.Id;
                        if (obj.Name != null && obj.Name.ToString() != string.Empty)
                        {
                            txtContactName.Text = obj.Name;
                        }
                        else
                        {
                            txtContactName.Text = "";
                        }
                        if (obj.Title != null && obj.Title.ToString() != string.Empty)
                        {
                            txtContactTitle.Text = obj.Title;
                        }
                        else
                        {
                            txtContactTitle.Text = "";
                        }

                        if (obj.TypeOfContact != null && obj.TypeOfContact.ToString() != string.Empty)
                        {
                            ddlType.SelectedValue = obj.TypeOfContact.ToString();
                        }
                        else
                        {
                            ddlType.SelectedValue = "-1";
                        }

                        if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
                        {
                            txtNumber.Text = obj.Phone;
                        }
                        else
                        {
                            txtNumber.Text = "";
                        }

                        if (obj.Email != null && obj.Email.ToString() != string.Empty)
                        {
                            txtEmail.Text = obj.Email;
                        }
                        else
                        {
                            txtEmail.Text = "";
                        }

                        if (obj.IsSentEmail != null && obj.IsSentEmail.ToString() != string.Empty)
                        {
                            chkSend.Checked = Convert.ToBoolean(obj.IsSentEmail);
                        }
                        else
                        {
                            chkSend.Checked = false;
                        }
                        if (obj.IsLocation != null && obj.IsLocation.ToString() != string.Empty)
                        {
                            chkLocation.Checked = Convert.ToBoolean(obj.IsLocation);
                        }
                        else
                        {
                            chkLocation.Checked = false;
                        }
                        if (obj.IsEmergencyContact != null && obj.IsEmergencyContact.ToString() != string.Empty)
                        {
                            chkEmergency.Checked = Convert.ToBoolean(obj.IsEmergencyContact);
                        }
                        else
                        {
                            chkEmergency.Checked = false;
                        }

                        if (obj.EmergencyContact != null && obj.EmergencyContact.ToString() != string.Empty)
                        {
                            txtEmergency.Text = obj.Email;
                        }
                        else
                        {
                            txtEmergency.Text = "";
                        }

                        btnSubmitContact.Text = "Update Contact";

                    }
                }
            }
            catch
            {

            }
        }
        private void FillContacts()
        {
            try
            {
                List<ContactInformation> obj = null;
                if (Session["OwnerId"] != null)
                {
                    obj = new ContactInformationDA().GetByOwner(Session["OwnerId"].ToString());
                }

                gvContactList.DataSource = obj;
                gvContactList.DataBind();
            }
            catch
            {

            }
        }
        #endregion

        #region Events User
        protected void btnSubmitUser_Click(object sender, EventArgs e)
        {
            errStr = string.Empty;
            errStr = Validate_ControlUser();
            if (errStr.Length <= 0)
            {
                try
                {
                    UserProfile objUser = new UserProfile();
                    objUser = SetDataUser(objUser);
                    string username = "";

                    if (Session["UserObject"] != null)
                    {
                        username = ((UserProfile)Session["UserObject"]).Username;
                    }

                    string SQL = " update UserProfile set HasUserProfile = 1  where Username = '" + username + "' ";

                    if (Session["AddUserId"] == null || Session["AddUserId"] == "0")
                    {
                        if (new UserProfileDA().Insert(objUser))
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMDMain(SQL);
                            Session["AddUserId"] = null;
                            ClearControlsUser();
                            FillContacts();
                            Utility.DisplayMsg("User saved successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("User not saved!", this);
                        }
                    }
                    else
                    {
                        if (new UserProfileDA().Update(objUser))
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMDMain(SQL);
                            Session["AddUserId"] = null;
                            ClearControlsUser();
                            FillContacts();
                            Utility.DisplayMsg("User updated successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("User not updated!", this);
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
        protected void btnCloseUser_Click(object sender, EventArgs e)
        {
            ClearControlsUser();
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvUserList.Rows[row.RowIndex].FindControl("lblUserId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new ContactInformationDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillUsers();
                }
            }
        }
        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvUserList.Rows[row.RowIndex].FindControl("lblUserId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                Session["AddUserId"] = Convert.ToInt32(hdId.Text);
                FillControlsUser(Convert.ToInt32(hdId.Text));
            }
        }
        protected void gvUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserList.PageIndex = e.NewPageIndex;
            FillUsers();
        }

        protected void gvUserList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillUsers();
        }
        #endregion

        #region Method User
        private void ClearControlsUser()
        {
            txtUserName.Text = "";
            txtUserTitle.Text = "";
            txtUserNumber.Text = "";
            txtUserEmail.Text = "";
            ddlUserLevel.SelectedValue = "-1";
            chkUserLocation.Checked = false;
            chkAdmin.Checked = true;
            btnSaveUser.Text = "Save";
        }
        public string Validate_ControlUser()
        {
            try
            {
                if ((txtUserName.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Username" + Environment.NewLine;
                }
                if ((txtUserEmail.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter email address" + Environment.NewLine;
                }
                else
                {
                    if (!ValidEmail(txtUserEmail.Text.ToString().Trim()))
                    {
                        errStr += "Invalid email address" + Environment.NewLine;
                    }
                }

                if ((txtUserNumber.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Phone Number" + Environment.NewLine;
                }

                List<UserProfile> objUsers = new UserProfileDA().GetUsersByUserName2(txtUserName.Text.ToString());

                if (objUsers != null && objUsers.Count > 0)
                {
                    if (Session["AddUserId"] != null)
                    {
                        if (objUsers.Count > 1)
                        {
                            errStr += "Username already exist !! Please enter different Username." + Environment.NewLine;
                        }
                    }
                    else
                    {
                        errStr += "Username already exist !! Please enter different Username." + Environment.NewLine;
                    }
                }

                List<UserProfile> objUsers2 = new UserProfileDA().GetUsersByUserEmail2(txtUserEmail.Text.ToString());

                if (objUsers2 != null && objUsers2.Count > 0)
                {
                    if (Session["AddUserId"] != null)
                    {
                        if (objUsers2.Count > 1)
                        {
                            errStr += "Email already exist !! Please enter different Email." + Environment.NewLine;
                        }
                    }
                    else
                    {
                        errStr += "Email already exist !! Please enter different Email." + Environment.NewLine;
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return errStr;
        }
        public bool ValidEmail(string value)
        {
            if ((value == null))
                return false;
            return reEmail.IsMatch(value);
        }
        private UserProfile SetDataUser(UserProfile obj)
        {
            try
            {
                if (Session["AddUserId"] != null && Convert.ToInt32(Session["AddUserId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["AddUserId"].ToString());
                }

                if ((!string.IsNullOrEmpty(txtUserName.Text.ToString())) && (txtUserName.Text.ToString() != string.Empty))
                {
                    obj.Username = txtUserName.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Username = "";
                }
                if ((!string.IsNullOrEmpty(txtUserTitle.Text.ToString())) && (txtUserTitle.Text.ToString() != string.Empty))
                {
                    obj.Title = txtUserTitle.Text.ToString();
                }
                else
                {
                    obj.Title = "";
                }
                if (ddlUserLevel.SelectedValue != "-1")
                {
                    obj.SecurityLevel = ddlUserLevel.SelectedValue.ToString();
                }
                else
                {
                    obj.SecurityLevel = "";
                }
                if ((!string.IsNullOrEmpty(txtUserNumber.Text.ToString())) && (txtUserNumber.Text.ToString() != string.Empty))
                {
                    obj.Phone = txtUserNumber.Text.ToString();
                }
                else
                {
                    obj.Phone = "";
                }
                if ((!string.IsNullOrEmpty(txtUserEmail.Text.ToString())) && (txtUserEmail.Text.ToString() != string.Empty))
                {
                    obj.Email = txtUserEmail.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Email = "";
                }

                if (chkAdmin.Checked)
                {
                    obj.IsAdmin = false;
                }
                else
                {
                    obj.IsAdmin = true;
                }

                obj.CanLogin = false;
                obj.IsDeleted = false;
                obj.UserType = Convert.ToInt32(EnumUserType.Normal).ToString();
                obj.IsActive = true;
                obj.Password = Utility.base64Encode("1234");

                if (Session["OwnerId"] != null)
                {
                    if (Session["OwnerId"].ToString() != string.Empty)
                    {
                        OwnerProfile TempOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
                        if (TempOwner != null)
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
                        obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                    }
                }
                else
                {
                    obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                }

                if (Session["AddUserId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                }
                else
                {
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                    obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
                }

                if (obj.LocationId == string.Empty)
                {
                    obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
                }

                obj.Location = obj.LocationId;
                obj.DatabaseName = "";
                obj.DatabaseLocation = "";
                obj.Remarks = "";
                obj.HasCompletedFullProfile = false;
                obj.HasContactProfile = false;
                obj.HasLedgerCode = false;
                obj.HasOwnerProfile = false;
                obj.HasPropertyLocation = false;
                obj.HasPropertyManagerProfile = false;
                obj.HasPropertyUnit = false;
                obj.HasSystemInfo = false;
                obj.HasUserProfile = false;
                obj.HasVendorProfile = false;

            }
            catch (Exception e)
            {
            }


            return obj;
        }
        private void FillControlsUser(int nId)
        {
            try
            {
                if (nId > 0)
                {
                    UserProfile obj = new UserProfileDA().GetUserByUserID(nId);
                    if (obj != null)
                    {
                        Session["AddUserId"] = obj.Id;
                        if (obj.Username != null && obj.Username.ToString() != string.Empty)
                        {
                            txtUserName.Text = obj.Username;
                        }
                        else
                        {
                            txtUserName.Text = "";
                        }
                        if (obj.Title != null && obj.Title.ToString() != string.Empty)
                        {
                            txtUserTitle.Text = obj.Title;
                        }
                        else
                        {
                            txtUserTitle.Text = "";
                        }

                        if (obj.SecurityLevel != null && obj.SecurityLevel.ToString() != string.Empty)
                        {
                            ddlUserLevel.SelectedValue = obj.SecurityLevel.ToString();
                        }


                        if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
                        {
                            txtUserNumber.Text = obj.Phone;
                        }
                        else
                        {
                            txtUserNumber.Text = "";
                        }

                        if (obj.Email != null && obj.Email.ToString() != string.Empty)
                        {
                            txtUserEmail.Text = obj.Email;
                        }
                        else
                        {
                            txtUserEmail.Text = "";
                        }


                        if (obj.IsAdmin != null && obj.IsAdmin.ToString() != string.Empty)
                        {
                            chkAdmin.Checked = Convert.ToBoolean(!obj.IsAdmin);
                        }
                        else
                        {
                            chkAdmin.Checked = false;
                        }

                        btnSaveUser.Text = "Update";

                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        private void FillUsers()
        {
            try
            {
                List<UserProfile> obj = null;
                if (Session["OwnerId"] != null)
                {
                    obj = new UserProfileDA().GetByOwner(Session["OwnerId"].ToString());
                }

                gvUserList.DataSource = obj;
                gvUserList.DataBind();
            }
            catch (Exception e)
            {

            }
        }

        #endregion

        #region Events Vendor
        protected void btnSubmitVendor_Click(object sender, EventArgs e)
        {
            try
            {
                VendorProfile obj = new VendorProfile();
                obj = SetDataVendor(obj);
                string username = "";
                if (Session["UserObject"] != null)
                {
                    username = ((UserProfile)Session["UserObject"]).Username;
                }

                string SQL = " update UserProfile set HasVendorProfile = 1  where Username = '" + username + "' ";
                if (Session["VendorId"] == null || Session["VendorId"] == "0")
                {
                    if (new VendorDA().Insert(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);
                        Session["VendorId"] = null;
                        ClearControlsVendor();
                        FillVendor();
                        Utility.DisplayMsg("Vendor saved successfully!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Vendor not saved!", this);
                    }
                }
                else
                {
                    if (new VendorDA().Update(obj))
                    {
                        Session["VendorId"] = null;
                        ClearControlsVendor();
                        FillVendor();
                        Utility.DisplayMsg("Vendor updated successfully!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Vendor not updated!", this);
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }
        protected void btnCloseVendor_Click(object sender, EventArgs e)
        {
            ClearControlsVendor();
        }

        protected void btnDeleteVendor_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvVendorList.Rows[row.RowIndex].FindControl("lblVendorId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new VendorDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillVendor();
                }
            }
        }
        protected void btnEditVendor_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvVendorList.Rows[row.RowIndex].FindControl("lblVendorId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                FillControlsVendor(Convert.ToInt32(hdId.Text));
            }
        }
        protected void gvVendorList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVendorList.PageIndex = e.NewPageIndex;
            FillVendor();
        }
        protected void gvVendorList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillVendor();
        }

        #endregion

        #region Method Vendor
       
        private void ClearControlsVendor()
        {
            ddlStateVendor.SelectedValue = "-1";
            txtAddressV.Text = "";
            txtAddress1V.Text = "";
            txtApproveDate.Text = "";
            txtApprovedBy.Text = "";
            txtBIN.Text = "";
            txtCityV.Text = "";
            txtCompanyNameV.Text = "";
            txtContactNameVendor.Text = "";
            txtDocument1.Text = "";
            txtDocument2.Text = "";
            txtDocument3.Text = "";
            txtDocument4.Text = "";
            txtDocumentID1.Text = "";
            txtDocumentID2.Text = "";
            txtDocumentID3.Text = "";
            txtDocumentID4.Text = "";
            txtEffectDate1.Text = "";
            txtEffectDate2.Text = "";
            txtEffectDate3.Text = "";
            txtEffectDate4.Text = "";
            txtEmailVendor.Text = "";
            txtEndDate1.Text = "";
            txtEndDate2.Text = "";
            txtEndDate3.Text = "";
            txtEndDate4.Text = "";
            txtFiledDate1.Text = "";
            txtFiledDate2.Text = "";
            txtFiledDate3.Text = "";
            txtFiledDate4.Text = "";
            txtNumberVendor.Text = "";
            txtPersonFiled1.Text = "";
            txtPersonFiled2.Text = "";
            txtPersonFiled3.Text = "";
            txtPersonFiled4.Text = "";
            txtRegionV.Text = "";
            txtReviewBy1.Text = "";
            txtReviewBy2.Text = "";
            txtReviewBy3.Text = "";
            txtReviewBy4.Text = "";
            txtReviewDate1.Text = "";
            txtReviewDate2.Text = "";
            txtReviewDate3.Text = "";
            txtReviewDate4.Text = "";
            txtTitle.Text = "";
            txtType.Text = "";
            txtValue1.Text = "";
            txtValue2.Text = "";
            txtValue3.Text = "";
            txtValue4.Text = "";
            txtZipV.Text = "";
            btnSaveVendor.Text = "Add";
        }
        private VendorProfile SetDataVendor(VendorProfile obj)
        {
            try
            {
                if (Session["VendorId"] != null && Convert.ToInt32(Session["VendorId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["VendorId"].ToString());
                }

                if ((!string.IsNullOrEmpty(txtType.Text.ToString())) && (txtType.Text.ToString() != string.Empty))
                {
                    obj.TypeOfWork = txtType.Text.ToString();
                }
                else
                {
                    obj.TypeOfWork = "";
                }

                if ((!string.IsNullOrEmpty(txtTitle.Text.ToString())) && (txtTitle.Text.ToString() != string.Empty))
                {
                    obj.Title = txtTitle.Text.ToString();
                }
                else
                {
                    obj.Title = "";
                }
                if ((!string.IsNullOrEmpty(txtCompanyNameV.Text.ToString())) && (txtCompanyNameV.Text.ToString() != string.Empty))
                {
                    obj.CompanyName = txtCompanyNameV.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.CompanyName = "";
                }

                if ((!string.IsNullOrEmpty(txtContactNameVendor.Text.ToString())) && (txtContactNameVendor.Text.ToString() != string.Empty))
                {
                    obj.ContractName = txtContactNameVendor.Text.ToString();
                }
                else
                {
                    obj.ContractName = "";
                }

                if ((!string.IsNullOrEmpty(txtAddressV.Text.ToString())) && (txtAddressV.Text.ToString() != string.Empty))
                {
                    obj.Address = txtAddressV.Text.ToString();
                }
                else
                {
                    obj.Address = "";
                }
                if ((!string.IsNullOrEmpty(txtAddress1V.Text.ToString())) && (txtAddress1V.Text.ToString() != string.Empty))
                {
                    obj.Address1 = txtAddress1V.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Address1 = "";
                }

                if ((!string.IsNullOrEmpty(txtRegionV.Text.ToString())) && (txtRegionV.Text.ToString() != string.Empty))
                {
                    obj.Region = txtRegionV.Text.ToString();
                }
                else
                {
                    obj.Region = "";
                }
                if (ddlStateVendor.SelectedValue != "-1")
                {
                    obj.State = ddlStateVendor.SelectedValue.Trim();
                }
                else
                {
                    obj.State = "";
                }
                obj.City = txtCityV.Text.ToString().Trim();
                obj.Zip = txtZipV.Text.ToString().Trim();
                obj.Email = txtEmailVendor.Text.ToString().Trim();
                obj.Phone = txtNumberVendor.Text.ToString().Trim();
                obj.LicenseNo = txtBIN.Text.ToString().Trim();
                obj.ApprovedBy = txtApprovedBy.Text.ToString().Trim();
                obj.DateApplied = DateTime.Now;
                obj.DateApproved = txtApproveDate.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtApproveDate.Text.ToString().Trim()) : DateTime.Now;

                obj.Document1 = txtDocument1.Text.ToString().Trim();
                obj.Document2 = txtDocument2.Text.ToString().Trim();
                obj.Document3 = txtDocument3.Text.ToString().Trim();
                obj.Document4 = txtDocument4.Text.ToString().Trim();

                obj.Value1 = txtValue1.Text.ToString().Trim();
                obj.Value2 = txtValue2.Text.ToString().Trim();
                obj.Value3 = txtValue3.Text.ToString().Trim();
                obj.Value4 = txtValue4.Text.ToString().Trim();

                obj.EffectDate1 = txtEffectDate1.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEffectDate1.Text.ToString().Trim()) : DateTime.Now;
                obj.EffectDate2 = txtEffectDate2.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEffectDate2.Text.ToString().Trim()) : DateTime.Now;
                obj.EffectDate3 = txtEffectDate3.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEffectDate3.Text.ToString().Trim()) : DateTime.Now;
                obj.EffectDate4 = txtEffectDate4.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEffectDate4.Text.ToString().Trim()) : DateTime.Now;

                obj.EndDate1 = txtEndDate1.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEndDate1.Text.ToString().Trim()) : DateTime.Now;
                obj.EndDate2 = txtEndDate2.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEndDate2.Text.ToString().Trim()) : DateTime.Now;
                obj.EndDate3 = txtEndDate3.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEndDate3.Text.ToString().Trim()) : DateTime.Now;
                obj.EndDate4 = txtEndDate4.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEndDate4.Text.ToString().Trim()) : DateTime.Now;

                obj.ReviewBy1 = txtReviewBy1.Text.ToString().Trim();
                obj.ReviewBy2 = txtReviewBy2.Text.ToString().Trim();
                obj.ReviewBy3 = txtReviewBy3.Text.ToString().Trim();
                obj.ReviewBy4 = txtReviewBy4.Text.ToString().Trim();

                obj.ReviewDate1 = txtReviewDate1.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtReviewDate1.Text.ToString().Trim()) : DateTime.Now;
                obj.ReviewDate2 = txtReviewDate2.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtReviewDate2.Text.ToString().Trim()) : DateTime.Now;
                obj.ReviewDate3 = txtReviewDate3.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtReviewDate3.Text.ToString().Trim()) : DateTime.Now;
                obj.ReviewDate4 = txtReviewDate4.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtReviewDate4.Text.ToString().Trim()) : DateTime.Now;

                obj.DocumentID1 = txtDocumentID1.Text.ToString().Trim();
                obj.DocumentID2 = txtDocumentID2.Text.ToString().Trim();
                obj.DocumentID3 = txtDocumentID3.Text.ToString().Trim();
                obj.DocumentID4 = txtDocumentID4.Text.ToString().Trim();

                obj.FiledDate1 = txtFiledDate1.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtFiledDate1.Text.ToString().Trim()) : DateTime.Now;
                obj.FiledDate2 = txtFiledDate2.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtFiledDate2.Text.ToString().Trim()) : DateTime.Now;
                obj.FiledDate3 = txtFiledDate3.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtFiledDate3.Text.ToString().Trim()) : DateTime.Now;
                obj.FiledDate4 = txtFiledDate4.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtFiledDate4.Text.ToString().Trim()) : DateTime.Now;

                obj.PersonFiled1 = txtPersonFiled1.Text.ToString().Trim();
                obj.PersonFiled2 = txtPersonFiled2.Text.ToString().Trim();
                obj.PersonFiled3 = txtPersonFiled3.Text.ToString().Trim();
                obj.PersonFiled4 = txtPersonFiled4.Text.ToString().Trim();

                if (chkSendEmail.Checked == true)
                {
                    obj.IsSentEmail = true;
                }
                else
                {
                    obj.IsSentEmail = false;
                }

                if (Session["VendorId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                }
                else
                {
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                }

                if (Session["PropertyLocationId"] != null)
                {
                    if (Session["PropertyLocationId"].ToString() != string.Empty)
                    {
                        obj.PropertyLocationId = Session["PropertyLocationId"].ToString();
                    }
                    else
                    {
                        obj.PropertyLocationId = "";
                    }
                }
                else
                {
                    obj.PropertyLocationId = "";
                }

                if (Session["PropertyManagerId"] != null)
                {
                    if (Session["PropertyManagerId"].ToString() != string.Empty)
                    {
                        obj.PropertyManagerId = Session["PropertyManagerId"].ToString();
                    }
                    else
                    {
                        obj.PropertyManagerId = "";
                    }
                }
                else
                {
                    obj.PropertyManagerId = "";
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


            }
            catch (Exception ex)
            {
            }

            return obj;
        }
        private void FillControlsVendor(int nId)
        {
            try
            {
                if (nId > 0)
                {
                    VendorProfile obj = new VendorDA().GetbyID(nId);
                    if (obj != null)
                    {
                        Session["VendorId"] = obj.Id;

                        if (obj.TypeOfWork != null && obj.TypeOfWork.ToString() != string.Empty)
                        {
                            txtType.Text = obj.TypeOfWork;
                        }
                        else
                        {
                            txtType.Text = "";
                        }
                        if (obj.ContractName != null && obj.ContractName.ToString() != string.Empty)
                        {
                            txtContactNameVendor.Text = obj.ContractName;
                        }
                        else
                        {
                            txtContactNameVendor.Text = "";
                        }
                        if (obj.Title != null && obj.Title.ToString() != string.Empty)
                        {
                            txtTitle.Text = obj.Title;
                        }
                        else
                        {
                            txtTitle.Text = "";
                        }
                        if (obj.CompanyName != null && obj.CompanyName.ToString() != string.Empty)
                        {
                            txtCompanyNameV.Text = obj.CompanyName;
                        }
                        else
                        {
                            txtCompanyNameV.Text = "";
                        }
                        if (obj.Address != null && obj.Address.ToString() != string.Empty)
                        {
                            txtAddressV.Text = obj.Address;
                        }
                        else
                        {
                            txtAddressV.Text = "";
                        }
                        if (obj.Address1 != null && obj.Address1.ToString() != string.Empty)
                        {
                            txtAddress1V.Text = obj.Address1;
                        }
                        else
                        {
                            txtAddress1V.Text = "";
                        }

                        if (obj.Region != null && obj.Region.ToString() != string.Empty)
                        {
                            txtRegionV.Text = obj.Region;
                        }
                        else
                        {
                            txtRegionV.Text = "";
                        }
                        if (obj.State != null && obj.State.ToString() != string.Empty)
                        {
                            ddlStateVendor.SelectedValue = obj.State;
                        }
                        else
                        {
                            ddlStateVendor.SelectedValue = "";
                        }
                        if (obj.City != null && obj.City.ToString() != string.Empty)
                        {
                            txtCityV.Text = obj.City;
                        }
                        else
                        {
                            txtCityV.Text = "";
                        }
                        if (obj.Zip != null && obj.Zip.ToString() != string.Empty)
                        {
                            txtZipV.Text = obj.Zip;
                        }
                        else
                        {
                            txtZipV.Text = "";
                        }
                        if (obj.Email != null && obj.Email.ToString() != string.Empty)
                        {
                            txtEmailVendor.Text = obj.Email;
                        }
                        else
                        {
                            txtEmailVendor.Text = "";
                        }
                        if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
                        {
                            txtNumberVendor.Text = obj.Phone;
                        }
                        else
                        {
                            txtNumberVendor.Text = "";
                        }
                        if (obj.LicenseNo != null && obj.LicenseNo.ToString() != string.Empty)
                        {
                            txtBIN.Text = obj.LicenseNo;
                        }
                        else
                        {
                            txtBIN.Text = "";
                        }
                        if (obj.ApprovedBy != null && obj.ApprovedBy.ToString() != string.Empty)
                        {
                            txtApprovedBy.Text = obj.ApprovedBy;
                        }
                        else
                        {
                            txtApprovedBy.Text = "";
                        }
                        if (obj.DateApproved != null && obj.DateApproved.ToString() != string.Empty)
                        {
                            txtApproveDate.Text = Convert.ToDateTime(obj.DateApproved).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtApproveDate.Text = "";
                        }

                        if (obj.Document1 != null && obj.Document1.ToString() != string.Empty)
                        {
                            txtDocument1.Text = obj.Document1;
                        }
                        else
                        {
                            txtDocument1.Text = "";
                        }
                        if (obj.Value1 != null && obj.Value1.ToString() != string.Empty)
                        {
                            txtValue1.Text = obj.Value1;
                        }
                        else
                        {
                            txtValue1.Text = "";
                        }
                        if (obj.EffectDate1 != null && obj.EffectDate1.ToString() != string.Empty)
                        {
                            txtEffectDate1.Text = Convert.ToDateTime(obj.EffectDate1).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtEffectDate1.Text = "";
                        }
                        if (obj.EndDate1 != null && obj.EndDate1.ToString() != string.Empty)
                        {
                            txtEndDate1.Text = Convert.ToDateTime(obj.EndDate1).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtEndDate1.Text = "";
                        }

                        if (obj.ReviewBy1 != null && obj.ReviewBy1.ToString() != string.Empty)
                        {
                            txtReviewBy1.Text = obj.ReviewBy1;
                        }
                        else
                        {
                            txtReviewBy1.Text = "";
                        }
                        if (obj.ReviewDate1 != null && obj.ReviewDate1.ToString() != string.Empty)
                        {
                            txtReviewDate1.Text = Convert.ToDateTime(obj.ReviewDate1).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtReviewDate1.Text = "";
                        }
                        if (obj.DocumentID1 != null && obj.DocumentID1.ToString() != string.Empty)
                        {
                            txtDocumentID1.Text = obj.DocumentID1;
                        }
                        else
                        {
                            txtDocumentID1.Text = "";
                        }
                        if (obj.FiledDate1 != null && obj.FiledDate1.ToString() != string.Empty)
                        {
                            txtFiledDate1.Text = Convert.ToDateTime(obj.FiledDate1).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtFiledDate1.Text = "";
                        }
                        if (obj.PersonFiled1 != null && obj.PersonFiled1.ToString() != string.Empty)
                        {
                            txtPersonFiled1.Text = obj.PersonFiled1;
                        }
                        else
                        {
                            txtPersonFiled1.Text = "";
                        }

                        if (obj.Document2 != null && obj.Document2.ToString() != string.Empty)
                        {
                            txtDocument2.Text = obj.Document2;
                        }
                        else
                        {
                            txtDocument2.Text = "";
                        }
                        if (obj.Value2 != null && obj.Value2.ToString() != string.Empty)
                        {
                            txtValue2.Text = obj.Value2;
                        }
                        else
                        {
                            txtValue2.Text = "";
                        }
                        if (obj.EffectDate2 != null && obj.EffectDate2.ToString() != string.Empty)
                        {
                            txtEffectDate2.Text = Convert.ToDateTime(obj.EffectDate2).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtEffectDate2.Text = "";
                        }
                        if (obj.EndDate2 != null && obj.EndDate2.ToString() != string.Empty)
                        {
                            txtEndDate2.Text = Convert.ToDateTime(obj.EndDate2).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtEndDate2.Text = "";
                        }

                        if (obj.ReviewBy2 != null && obj.ReviewBy2.ToString() != string.Empty)
                        {
                            txtReviewBy2.Text = obj.ReviewBy2;
                        }
                        else
                        {
                            txtReviewBy2.Text = "";
                        }
                        if (obj.ReviewDate2 != null && obj.ReviewDate2.ToString() != string.Empty)
                        {
                            txtReviewDate2.Text = Convert.ToDateTime(obj.ReviewDate2).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtReviewDate2.Text = "";
                        }
                        if (obj.DocumentID2 != null && obj.DocumentID2.ToString() != string.Empty)
                        {
                            txtDocumentID2.Text = obj.DocumentID2;
                        }
                        else
                        {
                            txtDocumentID2.Text = "";
                        }
                        if (obj.FiledDate2 != null && obj.FiledDate2.ToString() != string.Empty)
                        {
                            txtFiledDate2.Text = Convert.ToDateTime(obj.FiledDate2).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtFiledDate2.Text = "";
                        }
                        if (obj.PersonFiled2 != null && obj.PersonFiled2.ToString() != string.Empty)
                        {
                            txtPersonFiled2.Text = obj.PersonFiled2;
                        }
                        else
                        {
                            txtPersonFiled2.Text = "";
                        }

                        if (obj.Document3 != null && obj.Document3.ToString() != string.Empty)
                        {
                            txtDocument3.Text = obj.Document3;
                        }
                        else
                        {
                            txtDocument3.Text = "";
                        }
                        if (obj.Value3 != null && obj.Value3.ToString() != string.Empty)
                        {
                            txtValue3.Text = obj.Value3;
                        }
                        else
                        {
                            txtValue3.Text = "";
                        }
                        if (obj.EffectDate3 != null && obj.EffectDate3.ToString() != string.Empty)
                        {
                            txtEffectDate3.Text = Convert.ToDateTime(obj.EffectDate3).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtEffectDate3.Text = "";
                        }
                        if (obj.EndDate3 != null && obj.EndDate3.ToString() != string.Empty)
                        {
                            txtEndDate3.Text = Convert.ToDateTime(obj.EndDate3).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtEndDate3.Text = "";
                        }

                        if (obj.ReviewBy3 != null && obj.ReviewBy3.ToString() != string.Empty)
                        {
                            txtReviewBy3.Text = obj.ReviewBy3;
                        }
                        else
                        {
                            txtReviewBy3.Text = "";
                        }
                        if (obj.ReviewDate3 != null && obj.ReviewDate3.ToString() != string.Empty)
                        {
                            txtReviewDate3.Text = Convert.ToDateTime(obj.ReviewDate3).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtReviewDate3.Text = "";
                        }
                        if (obj.DocumentID3 != null && obj.DocumentID3.ToString() != string.Empty)
                        {
                            txtDocumentID3.Text = obj.DocumentID3;
                        }
                        else
                        {
                            txtDocumentID3.Text = "";
                        }
                        if (obj.FiledDate3 != null && obj.FiledDate3.ToString() != string.Empty)
                        {
                            txtFiledDate3.Text = Convert.ToDateTime(obj.FiledDate3).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtFiledDate3.Text = "";
                        }
                        if (obj.PersonFiled3 != null && obj.PersonFiled3.ToString() != string.Empty)
                        {
                            txtPersonFiled3.Text = obj.PersonFiled3;
                        }
                        else
                        {
                            txtPersonFiled3.Text = "";
                        }

                        if (obj.Document4 != null && obj.Document4.ToString() != string.Empty)
                        {
                            txtDocument4.Text = obj.Document4;
                        }
                        else
                        {
                            txtDocument4.Text = "";
                        }
                        if (obj.Value4 != null && obj.Value4.ToString() != string.Empty)
                        {
                            txtValue4.Text = obj.Value4;
                        }
                        else
                        {
                            txtValue4.Text = "";
                        }
                        if (obj.EffectDate4 != null && obj.EffectDate4.ToString() != string.Empty)
                        {
                            txtEffectDate4.Text = Convert.ToDateTime(obj.EffectDate4).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtEffectDate4.Text = "";
                        }
                        if (obj.EndDate4 != null && obj.EndDate4.ToString() != string.Empty)
                        {
                            txtEndDate4.Text = Convert.ToDateTime(obj.EndDate4).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtEndDate4.Text = "";
                        }

                        if (obj.ReviewBy4 != null && obj.ReviewBy4.ToString() != string.Empty)
                        {
                            txtReviewBy4.Text = obj.ReviewBy4;
                        }
                        else
                        {
                            txtReviewBy4.Text = "";
                        }
                        if (obj.ReviewDate4 != null && obj.ReviewDate4.ToString() != string.Empty)
                        {
                            txtReviewDate4.Text = Convert.ToDateTime(obj.ReviewDate4).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtReviewDate4.Text = "";
                        }
                        if (obj.DocumentID4 != null && obj.DocumentID4.ToString() != string.Empty)
                        {
                            txtDocumentID4.Text = obj.DocumentID4;
                        }
                        else
                        {
                            txtDocumentID4.Text = "";
                        }
                        if (obj.FiledDate4 != null && obj.FiledDate4.ToString() != string.Empty)
                        {
                            txtFiledDate4.Text = Convert.ToDateTime(obj.FiledDate4).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtFiledDate4.Text = "";
                        }
                        if (obj.PersonFiled4 != null && obj.PersonFiled4.ToString() != string.Empty)
                        {
                            txtPersonFiled4.Text = obj.PersonFiled4;
                        }
                        else
                        {
                            txtPersonFiled4.Text = "";
                        }

                        btnSaveVendor.Text = "Update";

                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        private void FillVendor()
        {
            try
            {
                List<VendorProfile> obj = null;
                if (Session["OwnerId"] != null)
                {
                    obj = new VendorDA().GetByOwner(Session["OwnerId"].ToString());
                }

                gvVendorList.DataSource = obj;
                gvVendorList.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        #endregion


        #region Events Property Manager

        protected void btnSubmitPropertyManager_Click(object sender, EventArgs e)
        {
            errStr = string.Empty;
            errStr = Validate_Control();
            if (errStr.Length <= 0)
            {
                try
                {
                    PropertyManagerProfile objPropertyManagerProfile = new PropertyManagerProfile();
                    objPropertyManagerProfile = SetDataPropertyManager(objPropertyManagerProfile);
                    string username = "";
                    if (Session["UserObject"] != null)
                    {
                        username = ((UserProfile)Session["UserObject"]).Username;
                    }

                    string SQL = " update UserProfile set HasPropertyManagerProfile = 1  where Username = '" + username + "' ";

                    if (Session["AddPropertyManagerId"] == null || Session["AddPropertyManagerId"].ToString() == "0")
                    {
                        if (new PropertyManagerProfileDA().Insert(objPropertyManagerProfile))
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMDMain(SQL);

                            Session["AddPropertyManagerId"] = null;
                            ClearControls();
                            Utility.DisplayMsg("Property Manager Created successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Property Manager not Created!", this);
                        }
                    }
                    else
                    {
                        if (new PropertyManagerProfileDA().Update(objPropertyManagerProfile))
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMDMain(SQL);

                            Session["AddPropertyManagerId"] = null;
                            ClearControlsPropertyManager();
                            Utility.DisplayMsg("Property Manager updated successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Property Manager not updated!", this);
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
        protected void btnCancelPropertyManager_Click(object sender, EventArgs e)
        {
            ClearControlsPropertyManager();
        }

        #endregion

        #region Method Property Manager
        private void FillDropdownsPropertyManager()
        {
            try
            {
                ddlCountry.Items.Clear();
                ddlCountry.AppendDataBoundItems = true;
                ddlCountry.Items.Add(new ListItem("Select Country", "-1"));
                ddlCountry.DataSource = new CountryDA().GetAllRefCountries();
                ddlCountry.DataTextField = "COUNTRYNAME";
                ddlCountry.DataValueField = "COUNTRY";
                ddlCountry.DataBind();
                ddlCountry.SelectedValue = "US";
            }
            catch (Exception ex)
            {

            }
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
        }
        private void ClearControlsPropertyManager()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtCompanyName.Text = "";
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtRegion.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
            txtEIN.Text = "";
            rdoEIN.SelectedValue = null;
            btnSave.Text = "Save";
            lblHeadline.InnerText = "Create / Change Property Manager Profile";
        }
        public string Validate_ControlPropertyManager()
        {
            try
            {
                if (txtFirstName.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter First Name" + Environment.NewLine;
                }
                if (txtLastName.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Last Name" + Environment.NewLine;
                }
                if (txtCompanyName.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Company Name" + Environment.NewLine;
                }
                if (txtAddress.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Address" + Environment.NewLine;
                }
                if (ddlCountry.SelectedValue == "-1")
                {
                    errStr += "Please select Country" + Environment.NewLine;
                }
                if (txtCity.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter City" + Environment.NewLine;
                }
                if (txtZip.Text.ToString().Trim().Length <= 0)
                {
                    errStr += "Please enter Zip Code" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
            }

            return errStr;
        }
        private PropertyManagerProfile SetDataPropertyManager(PropertyManagerProfile obj)
        {
            try
            {
                if (Session["AddPropertyManagerId"] != null && Convert.ToInt32(Session["AddPropertyManagerId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["AddPropertyManagerId"].ToString());
                }

                if (!string.IsNullOrEmpty(txtFirstName.Text.ToString()))
                {
                    obj.FirstName = txtFirstName.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.FirstName = "";
                }
                if (!string.IsNullOrEmpty(txtLastName.Text.ToString()))
                {
                    obj.LastName = txtLastName.Text.ToString();
                }
                else
                {
                    obj.LastName = "";
                }
                if (!string.IsNullOrEmpty(txtCompanyName.Text.ToString()))
                {
                    obj.CompanyName = txtCompanyName.Text.ToString();
                }
                else
                {
                    obj.CompanyName = "";
                }

                if (!string.IsNullOrEmpty(txtAddress.Text.ToString()))
                {
                    obj.Address = txtAddress.Text.ToString();
                }
                else
                {
                    obj.Address = "";
                }
                if (!string.IsNullOrEmpty(txtAddress1.Text.ToString()))
                {
                    obj.Address1 = txtAddress1.Text.ToString();
                }
                else
                {
                    obj.Address1 = "";
                }


                if (ddlCountry.SelectedValue != "-1")
                {
                    obj.Country = ddlCountry.SelectedValue.ToString();
                }
                else
                {
                    obj.Country = "";
                }
                if (!string.IsNullOrEmpty(txtRegion.Text.ToString()))
                {
                    obj.Region = txtRegion.Text.ToString();
                }
                else
                {
                    obj.Region = "";
                }

                if (ddlState.SelectedValue != "-1")
                {
                    obj.State = ddlState.SelectedValue.ToString();
                }
                else
                {
                    obj.State = "";
                }
                if (!string.IsNullOrEmpty(txtCity.Text.ToString()))
                {
                    obj.City = txtCity.Text.ToString();
                }
                else
                {
                    obj.City = "";
                }

                if (!string.IsNullOrEmpty(txtZip.Text.ToString()))
                {
                    obj.Zip = txtZip.Text.ToString();
                }
                else
                {
                    obj.Zip = "";
                }
                if (rdoEIN.Items[0].Selected == true)
                {
                    obj.TypeOfNumber = 1;
                }
                else if (rdoEIN.Items[1].Selected == true)
                {
                    obj.TypeOfNumber = 2;
                }

                if (!string.IsNullOrEmpty(txtEIN.Text.ToString()))
                {
                    obj.FedNumber = txtEIN.Text.ToString();
                }
                else
                {
                    obj.FedNumber = "";
                }

                if (obj.FedNumber.Trim().Length > 4)
                {
                    obj.FedLast4Digit = obj.FedNumber.Substring(obj.FedNumber.Length - 4, 4);
                }
                else
                {
                    obj.FedLast4Digit = "";
                }

                if (Session["AddPropertyManagerId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                }
                else
                {
                    obj.Serial = new PropertyManagerProfileDA().MakeAutoGenSerial("P", "PropertyManager");
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                }

                if (Session["OwnerId"] != null)
                {
                    obj.OwnerId = Session["OwnerId"].ToString();
                }
                else
                {
                    obj.OwnerId = "";
                }

                obj.IsDelete = false;

            }
            catch (Exception ex)
            {
            }

            return obj;
        }
        private void FillControlsPropertyManager(int nId)
        {
            try
            {
                if (nId > 0)
                {
                    PropertyManagerProfile obj = new PropertyManagerProfileDA().GetByID(nId);
                    if (obj != null)
                    {
                        Session["AddPropertyManagerId"] = obj.Id;
                        if (obj.FirstName != null && obj.FirstName.ToString() != string.Empty)
                        {
                            txtFirstName.Text = obj.FirstName;
                        }
                        else
                        {
                            txtFirstName.Text = "";
                        }

                        if (obj.LastName != null && obj.LastName.ToString() != string.Empty)
                        {
                            txtLastName.Text = obj.LastName;
                        }
                        else
                        {
                            txtLastName.Text = "";
                        }

                        if (obj.CompanyName != null && obj.CompanyName.ToString() != string.Empty)
                        {
                            txtCompanyName.Text = obj.CompanyName;
                        }
                        else
                        {
                            txtCompanyName.Text = "";
                        }

                        if (obj.Address != null && obj.Address.ToString() != string.Empty)
                        {
                            txtAddress.Text = obj.Address;
                        }
                        else
                        {
                            txtAddress.Text = "";
                        }
                        if (obj.Address1 != null && obj.Address1.ToString() != string.Empty)
                        {
                            txtAddress1.Text = obj.Address;
                        }
                        else
                        {
                            txtAddress1.Text = "";
                        }

                        if (obj.Country != null && obj.Country.ToString() != string.Empty)
                        {
                            ddlCountry.SelectedValue = obj.Country.ToString();
                        }
                        if (obj.Region != null && obj.Region.ToString() != string.Empty)
                        {
                            txtRegion.Text = obj.Region;
                        }
                        else
                        {
                            txtRegion.Text = "";
                        }

                        if (obj.State != null && obj.State.ToString() != string.Empty)
                        {
                            ddlState.SelectedValue = obj.State.ToString();
                        }

                        if (obj.City != null && obj.City.ToString() != string.Empty)
                        {
                            txtCity.Text = obj.City;
                        }
                        else
                        {
                            txtCity.Text = "";
                        }
                        if (obj.Zip != null && obj.Zip.ToString() != string.Empty)
                        {
                            txtZip.Text = obj.Zip;
                        }
                        else
                        {
                            txtZip.Text = "";
                        }

                        if (obj.FedNumber != null && obj.FedNumber.ToString() != string.Empty)
                        {
                            txtEIN.Text = obj.FedNumber;
                        }
                        else
                        {
                            txtEIN.Text = "";
                        }

                        if (obj.TypeOfNumber != null)
                        {
                            if (Convert.ToInt32(obj.TypeOfNumber) == 1)
                            {
                                rdoEIN.Items[0].Selected = true;
                            }
                            else if (Convert.ToInt32(obj.TypeOfNumber) == 2)
                            {
                                rdoEIN.Items[1].Selected = true;
                            }
                        }

                        btnSave.Text = "Update";
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