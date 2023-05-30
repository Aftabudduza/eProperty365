using eProperty.MyFileIt;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Account;
using PropertyService.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                    FillDropdowns(); 

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

                        if (lblId.Text == string.Empty)
                        {
                            if (Session["OwnerId"] != null)
                            {
                                if (Session["OwnerId"].ToString() != string.Empty)
                                {
                                    lblId.Text = Session["OwnerId"].ToString();
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(lblId.Text))
                        {
                            lblId.Text = new AdminOwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                        }
                    }

                    if (Session["TeamId"] != null)
                    {
                        Dealer_SalesPartner objDealer_SalesPartner = new SalesPartnerDealerDashboardDA().GetBySerial(Session["TeamId"].ToString());
                        if(objDealer_SalesPartner != null)
                        {
                            ddlSalesPartner.SelectedValue = Session["TeamId"].ToString();
                            ddlSalesPartner.Enabled = false;
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
                        if (new OwnerProfileDA(true, false).Insert(objOwnerProfile))
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMD(SQLSystem);
                            Utility.RunCMD(SQLPayment);

                            if (new AdminOwnerProfileDA(true, false).Insert(objOwnerProfile))
                            {
                                Utility.RunCMDMain(SQL);
                                Utility.RunCMDMain(SQLSystem);
                                Utility.RunCMDMain(SQLPayment);
                            }
                           
                            if(saveFileItUser(objOwnerProfile))
                            {

                            }

                            Session["AddOwnerId"] = null;
                            ClearControls();
                            lblMsg.Text = "Successful Creating Account !";
                           // Utility.DisplayMsg("Successful Creating Account !", this);
                            if (Session["HasCompletedFullProfile"] != null)
                            {
                                if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == false)
                                {
                                    Utility.DisplayMsgAndRedirect("Successful Creating Account!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                                }
                                else
                                {
                                    Utility.DisplayMsgAndRedirect("Successful Creating Account!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                                }
                            }
                            else
                            {
                                Utility.DisplayMsgAndRedirect("Successful Creating Account!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Account not Created!";
                            Utility.DisplayMsg("Account not Created!", this);
                        }
                    }
                    else
                    {
                        if (new OwnerProfileDA().Update(objOwnerProfile))
                        {
                            Utility.RunCMD(SQL);
                            Utility.RunCMD(SQLSystem);
                            Utility.RunCMD(SQLPayment);

                            OwnerProfile existing = new AdminOwnerProfileDA().GetBySerial(objOwnerProfile.Serial);
                            if(existing != null)
                            {
                                objOwnerProfile.Id = existing.Id;
                                if (new AdminOwnerProfileDA().Update(objOwnerProfile))
                                {
                                    Utility.RunCMDMain(SQL);
                                    Utility.RunCMDMain(SQLSystem);
                                    Utility.RunCMDMain(SQLPayment);
                                }
                            }
                            else
                            {
                                if (new AdminOwnerProfileDA(true, false).Insert(objOwnerProfile))
                                {
                                    Utility.RunCMDMain(SQL);
                                    Utility.RunCMDMain(SQLSystem);
                                    Utility.RunCMDMain(SQLPayment);
                                }
                            }

                            if (saveFileItUser(objOwnerProfile))
                            {

                            }

                            Session["AddOwnerId"] = null;
                            ClearControls();
                            lblMsg.Text = "Successful Creating Account!";
                            // Utility.DisplayMsg("Successful Creating Account!", this);
                            if (Session["HasCompletedFullProfile"] != null)
                            {
                                if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == false)
                                {
                                    Utility.DisplayMsgAndRedirect("Successful Creating Account!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                                }
                                else
                                {
                                    Utility.DisplayMsgAndRedirect("Successful Creating Account!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                                }
                            }
                            else
                            {
                                Utility.DisplayMsgAndRedirect("Successful Creating Account!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Account not updated!";
                            Utility.DisplayMsg("Account not updated!", this);
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
        protected void btnAgreement_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.eproperty365.com/elements/termsandconditions/", false);
        }
        protected void btnAddAccount_Click(object sender, EventArgs e)
        {
            saveOwner();
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddOwnerSystem.aspx?R=owner", false);
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            saveOwner();
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddUser.aspx?R=owner", false);
        }

        protected void btnAddPropertyManager_Click(object sender, EventArgs e)
        {
            saveOwner();
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddPropertyManager.aspx?R=owner", false);
        }

        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            saveOwner();
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddContact.aspx?R=owner", false);
        }

        protected void btnAddVendor_Click(object sender, EventArgs e)
        {
            saveOwner();
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddVendor.aspx?R=owner", false);
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = chkAgree.Checked;
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

            //try
            //{
            //    ddlStateVendor.Items.Clear();
            //    ddlStateVendor.AppendDataBoundItems = true;
            //    ddlStateVendor.DataSource = new StateDA().GetAllRefStates();
            //    ddlStateVendor.DataTextField = "STATENAME";
            //    ddlStateVendor.DataValueField = "STATE";
            //    ddlStateVendor.DataBind();

            //}
            //catch (Exception ex)
            //{
            //}

            try
            {
                ddlSalesPartner.Items.Clear();
                ddlSalesPartner.AppendDataBoundItems = true;
                ddlSalesPartner.Items.Add(new ListItem("Select Sales Partner", "-1"));
                List<Dealer_SalesPartner> listDealerSalesPartner = new SalesPartnerDealerDashboardDA().GetAllSalesPartnerDealer(Convert.ToInt32(EnumUserType.SalesPartner));
                if(listDealerSalesPartner != null && listDealerSalesPartner.Count > 0)
                {
                    foreach(Dealer_SalesPartner objSalesPartner in listDealerSalesPartner)
                    {
                        string name = objSalesPartner.firstName + " " + objSalesPartner.lastName;
                        ddlSalesPartner.Items.Add(new ListItem(name, objSalesPartner.serialCode));
                    }
                }

                ddlSalesPartner.DataBind();
                ddlSalesPartner.SelectedValue = "-1";
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
            //btnSave.Enabled = false;
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
                
                if (ddlSalesPartner.SelectedValue == "-1")
                {
                    errStr += "Please select Sales Partner" + Environment.NewLine;
                }

                //if (txtLateFee.Text.ToString().Trim().Length <= 0)
                //{
                //    errStr += "Please enter Late Fee" + Environment.NewLine;
                //}
                //else
                //{
                //    if (Convert.ToDecimal(txtLateFee.Text.ToString().Trim()) <= 5)
                //    {
                //        errStr += "Please enter minimum value of 5%" + Environment.NewLine;
                //    }
                //}

                //if (txtChargeBackFee.Text.ToString().Trim().Length <= 0)
                //{
                //    errStr += "Please enter ChargeBack Fee" + Environment.NewLine;
                //}
                //else
                //{
                //    if (Convert.ToDecimal(txtChargeBackFee.Text.ToString().Trim()) <= 5)
                //    {
                //        errStr += "Please enter minimum value of $50" + Environment.NewLine;
                //    }
                //}

                if (chkAgree.Checked  == false)
                {
                    errStr += "Please check Terms and Condtions Agreement" + Environment.NewLine;
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
                obj = new OwnerProfile();

                if (Session["AddOwnerId"] != null && Convert.ToInt32(Session["AddOwnerId"]) > 0)
                {
                    obj = new OwnerProfileDA().GetByID(Convert.ToInt32(Session["AddOwnerId"]));
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

                if (!string.IsNullOrEmpty(lblId.Text.ToString()))
                {
                    obj.Serial = lblId.Text.ToString();
                }

                if (Session["AddOwnerId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;                    
                }
                else
                {
                  //  obj.Serial = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                }

                if (Session["AddOwnerId"] != null)
                {
                    if (Session["OwnerId"] != null)
                    {
                        if (Session["OwnerId"].ToString() != string.Empty)
                        {
                            obj.Serial = Session["OwnerId"].ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(obj.Serial))
                {
                    obj.Serial = new AdminOwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                }

                if (ddlSalesPartner.SelectedValue != "-1")
                {
                    obj.SalesPartnerId = ddlSalesPartner.SelectedValue.ToString();
                }
                else
                {
                    obj.SalesPartnerId = "";
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
                            txtAddress1.Text = obj.Address1;
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
                        if (obj.SalesPartnerId != null && obj.SalesPartnerId.ToString() != string.Empty)
                        {
                            ddlSalesPartner.SelectedValue = obj.SalesPartnerId.ToString();
                        }

                        chkAgree.Checked = true;

                      //  btnSave.Text = "Update Owner Account";

                    }
                }
            }
            catch (Exception ex)
            {
            }

        }       
        private void saveOwner()
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
                        //Utility.RunCMD(SQL);
                        //Utility.RunCMD(SQLSystem);
                        //Utility.RunCMD(SQLPayment);

                        if (new AdminOwnerProfileDA().Insert(objOwnerProfile))
                        {
                            //Utility.RunCMDMain(SQL);
                            //Utility.RunCMDMain(SQLSystem);
                            //Utility.RunCMDMain(SQLPayment);
                        }

                        Session["AddOwnerId"] = null;
                        ClearControls();
                        //lblMsg.Text = "Successful Creating Account !";
                        //Utility.DisplayMsg("Successful Creating Account !", this);
                    }
                    else
                    {
                        //lblMsg.Text = "Account not Created!";
                        //Utility.DisplayMsg("Account not Created!", this);
                    }
                }
                else
                {
                    if (new OwnerProfileDA().Update(objOwnerProfile))
                    {
                        //Utility.RunCMD(SQL);
                        //Utility.RunCMD(SQLSystem);
                        //Utility.RunCMD(SQLPayment);

                        OwnerProfile existing = new AdminOwnerProfileDA().GetBySerial(objOwnerProfile.Serial);
                        if (existing != null)
                        {
                            objOwnerProfile.Id = existing.Id;
                            if (new AdminOwnerProfileDA().Update(objOwnerProfile))
                            {
                                //Utility.RunCMDMain(SQL);
                                //Utility.RunCMDMain(SQLSystem);
                                //Utility.RunCMDMain(SQLPayment);
                            }
                        }
                        else
                        {
                            if (new AdminOwnerProfileDA().Insert(objOwnerProfile))
                            {
                                //Utility.RunCMDMain(SQL);
                                //Utility.RunCMDMain(SQLSystem);
                                //Utility.RunCMDMain(SQLPayment);
                            }
                        }



                        Session["AddOwnerId"] = null;
                        ClearControls();
                        //lblMsg.Text = "Successful Creating Account!";
                        //Utility.DisplayMsg("Successful Creating Account!", this);
                    }
                    else
                    {
                        //lblMsg.Text = "Account not updated!";
                        //Utility.DisplayMsg("Account not updated!", this);
                    }
                }
            }
            catch (Exception ex1)
            {
                //Utility.DisplayMsg(ex1.Message.ToString(), this);
            }
        }
        private bool saveFileItUser(OwnerProfile objOwnerProfile)
        {
            bool bSuccess = false;
            try
            {
                var strFileItUser = ConfigurationManager.AppSettings["FileItUser"];
                var strFileItPassword = ConfigurationManager.AppSettings["FileItPassword"];

                MyFileIt.MyFileItPEMainServiceClient obj = new MyFileIt.MyFileItPEMainServiceClient();
                MyFileIt.MyFileItResult result = new MyFileIt.MyFileItResult();
                List<AppUserDTO> objAppUsers = new List<AppUserDTO>();
                MyFileIt.AppUserDTO objAppUser = new MyFileIt.AppUserDTO();

                objAppUser = setdataFileItUser(objOwnerProfile);

                AppUserDTO ExistingUser = null;

                result = obj.GetAllAppUsers(strFileItUser, strFileItPassword, null);
                objAppUsers = result.AppUsers.Where(x => x.EMAILADDRESS.ToLower().Contains(objAppUser.EMAILADDRESS.ToLower())).ToList();
                ExistingUser = objAppUsers.FirstOrDefault();

                if (!string.IsNullOrEmpty(objAppUser.EMAILADDRESS) && !string.IsNullOrEmpty(objAppUser.PASSWORD))
                {
                    if (ExistingUser != null)
                    {
                        objAppUser.ID = ExistingUser.ID;
                        objAppUser.APPUSERID = ExistingUser.APPUSERID;

                        result = obj.UpdateAppUser(strFileItUser, strFileItPassword, objAppUser);
                        if (result.Success == true)
                        {
                            bSuccess = true;
                        }
                    }
                    else
                    {
                        result = obj.AddAppUser(strFileItUser, strFileItPassword, objAppUser, null);
                        if (result.Success == true)
                        {
                            bSuccess = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return bSuccess;

        }
        public AppUserDTO setdataFileItUser(OwnerProfile objOwnerProfile)
        {
            AppUserDTO objU = new AppUserDTO();
            string sEmail = string.Empty;
            string sPassword = string.Empty;
            string sPhone = string.Empty;
            if(Session["UserObject"] != null)
            {
                sEmail = ((UserProfile)Session["UserObject"]).Email != null ? Convert.ToString(((UserProfile)Session["UserObject"]).Email) : "";
                sPassword = ((UserProfile)Session["UserObject"]).Password != null ? Convert.ToString(((UserProfile)Session["UserObject"]).Password) : "";
                if(!string.IsNullOrEmpty(sPassword))
                {
                    sPassword = Utility.base64Decode(sPassword);
                }

                sPhone = ((UserProfile)Session["UserObject"]).Phone != null ? Convert.ToString(((UserProfile)Session["UserObject"]).Phone) : "";
            }
            
            try
            {
                objU.FIRSTNAME = objOwnerProfile.FirstName;
                objU.LASTNAME = objOwnerProfile.LastName;
                objU.APPUSERTYPEID = 4;
                objU.BIRTHDATE = null;
                objU.PHONE = sPhone;
                objU.PASSWORD = sPassword;
                if (objU.PASSWORD == null || objU.PASSWORD == string.Empty)
                {
                    objU.PASSWORD = "1234";
                }
              
                objU.ADDRESS1 = objOwnerProfile.Address;
                objU.CITY = objOwnerProfile.City;
                objU.STATECODE = objOwnerProfile.State;
                objU.ZIPCODE = objOwnerProfile.Zip;
                objU.SEX = "M";
                objU.PRIMARYAPPUSERID = null;
                objU.RELATIONSHIPTYPEID = null;
                objU.DATECREATED = DateTime.UtcNow;
                objU.EMAILADDRESS = sEmail;
                objU.USERNAME = objU.EMAILADDRESS;
                objU.APPUSERSTATUSID = 2;
                objU.Organizations = null;
            }

            catch (Exception e)
            {

            }
            return objU;

        }

        #endregion


    }
}