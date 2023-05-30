using PropertyService.BO;
using PropertyService.DA;
using PropertyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Admin
{
    public partial class AddPropertyManager : System.Web.UI.Page
    {
        public String errStr = String.Empty;
        private Regex reEmail = new Regex("^(?:[0-9A-Z_-]+(?:\\.[0-9A-Z_-]+)*@[0-9A-Z-]+(?:\\.[0-9A-Z-]+)*(?:\\.[A-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        public string sUrl = string.Empty;
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                    FillDropdowns();
                    //FillContactType();
                    //FillContacts();
                    //FillUsers();
                    Session["AddPropertyManagerId"] = null;
                    Session["ContactId"] = null;
                    Session["AddUserId"] = null;
                    Session["managerUrl"] = null;
                    try
                    {
                        if (Request.QueryString["R"] != null)
                        {
                            sUrl = Request.QueryString["R"].ToString();
                            Session["managerUrl"] = sUrl.ToLower();
                        }
                    }
                    catch
                    {
                        sUrl = "";
                    }

                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["AddPropertyManagerId"].ToString());
                    }
                    catch
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                        lblHeadline.InnerText = "Change Property Manager Profile";
                        Session["AddPropertyManagerId"] = CId;
                        FillControls(Convert.ToInt32(Session["AddPropertyManagerId"].ToString()));
                    }

                    if (Session["OwnerId"] != null)
                    {
                        PropertyManagerProfile objPropertyManager = new PropertyManagerProfileDA().GetByOwner(Session["OwnerId"].ToString());
                        if (objPropertyManager != null)
                        {
                            lblHeadline.InnerText = "Change Property Manager Profile";
                            Session["AddPropertyManagerId"] = objPropertyManager.Id;
                            FillControls(Convert.ToInt32(objPropertyManager.Id));
                        }
                    }

                    if (Session["AddPropertyManagerId"] == null)
                    {
                        lblId.Text = new PropertyManagerProfileDA().MakeAutoGenSerial("P", "PropertyManager");
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
                    PropertyManagerProfile objPropertyManagerProfile = new PropertyManagerProfile();
                    objPropertyManagerProfile = SetData(objPropertyManagerProfile);
                    string username = "";
                    if (Session["UserObject"] != null)
                    {
                        username = ((UserProfile)Session["UserObject"]).Username;
                    }

                    if (Session["managerUrl"] != null)
                    {
                        sUrl = Session["managerUrl"].ToString();
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
                           // Utility.DisplayMsg("Property Manager Created successfully!", this);
                          //  Utility.DisplayMsgAndRedirect("Property Manager Created successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");

                            if (sUrl != string.Empty)
                            {
                                if (sUrl.Trim() == "owner")
                                {
                                    Utility.DisplayMsgAndRedirect("Property Manager Created successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                                }                               
                                else
                                {
                                    Utility.DisplayMsg("Property Manager Created successfully!", this);
                                }
                            }
                            else
                            {
                                Utility.DisplayMsg("Property Manager Created successfully!", this);
                            }

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

                           // Session["AddPropertyManagerId"] = null;
                            ClearControls();
                            //  Utility.DisplayMsg("Property Manager updated successfully!", this);
                            //  Utility.DisplayMsgAndRedirect("Property Manager updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");

                            if (sUrl != string.Empty)
                            {
                                if (sUrl.Trim() == "owner")
                                {
                                    Utility.DisplayMsgAndRedirect("Property Manager updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                                }
                                else
                                {
                                    Utility.DisplayMsg("Property Manager updated successfully!", this);
                                }
                            }
                            else
                            {
                                Utility.DisplayMsg("Property Manager updated successfully!", this);
                            }

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
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClearControls();
            //if (Session["HasCompletedFullProfile"] != null)
            //{
            //    if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == false)
            //    {
            //        Response.Redirect(Utility.WebUrl + "/Pages/DashboardOwner.aspx", false);
            //    }
            //    else
            //    {
            //        Response.Redirect(Utility.WebUrl + "/Pages/DashboardAdmin.aspx", false);
            //    }
            //}
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearControls();
            if (Session["managerUrl"] != null)
            {
                sUrl = Session["managerUrl"].ToString();
            }
            if (sUrl != string.Empty)
            {
                if (sUrl.Trim() == "owner")
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddOwner.aspx", false);
                }
                else 
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
        #endregion

        #region Method
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
        private void ClearControls()
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
            //btnSave.Enabled = false;
            lblHeadline.InnerText = "Create / Change Property Manager Profile";
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
            }
            catch (Exception ex)
            {
            }

            return errStr;
        }      
        private PropertyManagerProfile SetData(PropertyManagerProfile obj)
        {
            try
            {
                obj = new PropertyManagerProfile();

                if (Session["AddPropertyManagerId"] != null && Convert.ToInt32(Session["AddPropertyManagerId"]) > 0)
                {
                    obj = new PropertyManagerProfileDA().GetByID(Convert.ToInt32(Session["AddPropertyManagerId"]));
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

                if (!string.IsNullOrEmpty(lblId.Text.ToString()))
                {
                    obj.Serial = lblId.Text.ToString();
                }
                else
                {
                    obj.Serial = "";
                }

                if (Session["AddPropertyManagerId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;                    
                }
                else
                {
                 //   obj.Serial = new PropertyManagerProfileDA().MakeAutoGenSerial("P", "PropertyManager");
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                }

                if (Session["OwnerId"] != null)
                {
                    obj.OwnerId = Session["OwnerId"].ToString();
                }

                if (Session["AddPropertyManagerId"] == null || string.IsNullOrEmpty(obj.Serial))
                {
                    obj.Serial = new PropertyManagerProfileDA().MakeAutoGenSerial("P", "PropertyManager");
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
                        if (obj.Serial != null && obj.Serial.ToString() != string.Empty)
                        {
                            lblId.Text = obj.Serial;
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


        //#region Events Contact
        //protected void btnSubmitContact_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ContactInformation obj = new ContactInformation();
        //        obj = SetDataContact(obj);
        //        string username = "";

        //        if (Session["UserObject"] != null)
        //        {
        //            username = ((UserProfile)Session["UserObject"]).Username;
        //        }

        //        string SQL = " update UserProfile set HasContactProfile = 1  where Username = '" + username + "' ";

        //        if (Session["ContactId"] == null || Session["ContactId"] == "0")
        //        {
        //            if (new ContactInformationDA().Insert(obj))
        //            {
        //                Utility.RunCMD(SQL);
        //                Utility.RunCMDMain(SQL);
        //                Session["ContactId"] = null;
        //                ClearControlsContact();
        //                FillContacts();
        //                Utility.DisplayMsg("Contact saved successfully!", this);
        //            }
        //            else
        //            {
        //                Utility.DisplayMsg("Contact not saved!", this);
        //            }
        //        }
        //        else
        //        {
        //            if (new ContactInformationDA().Update(obj))
        //            {
        //                Utility.RunCMD(SQL);
        //                Utility.RunCMDMain(SQL);
        //                Session["ContactId"] = null;
        //                ClearControlsContact();
        //                FillContacts();
        //                Utility.DisplayMsg("Contact updated successfully!", this);
        //            }
        //            else
        //            {
        //                Utility.DisplayMsg("Contact not updated!", this);
        //            }
        //        }
        //    }
        //    catch (Exception ex1)
        //    {

        //    }
        //}
        //protected void btnCloseContact_Click(object sender, EventArgs e)
        //{
        //    ClearControlsContact();
        //}

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //    Label hdId = (Label)gvContactList.Rows[row.RowIndex].FindControl("lblId");

        //    if (!String.IsNullOrEmpty(hdId.Text))
        //    {
        //        if (new ContactInformationDA().DeleteByID(Convert.ToInt32(hdId.Text)))
        //        {
        //            FillContacts();
        //        }
        //    }
        //}
        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //    Label hdId = (Label)gvContactList.Rows[row.RowIndex].FindControl("lblId");

        //    if (!String.IsNullOrEmpty(hdId.Text))
        //    {
        //        FillControlsContact(Convert.ToInt32(hdId.Text));
        //    }
        //}
        //protected void gvContactList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvContactList.PageIndex = e.NewPageIndex;
        //    FillContacts();
        //}
        //protected void gvContactList_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    FillContacts();
        //}
        //#endregion

        //#region Method Contact
        //private void ClearControlsContact()
        //{
        //    txtContactName.Text = "";
        //    txtContactTitle.Text = "";
        //    txtNumber.Text = "";
        //    txtEmail.Text = "";
        //    txtEmergency.Text = "";
        //    ddlType.SelectedValue = "-1";
        //    chkEmergency.Checked = false;
        //    chkLocation.Checked = false;
        //    chkSend.Checked = false;
        //    btnSubmitContact.Text = "Add Contact";

        //}
        //private void FillContactType()
        //{
        //    try
        //    {

        //        ddlType.Items.Clear();
        //        ddlType.AppendDataBoundItems = true;
        //        ddlType.Items.Add(new ListItem("Select Type of Contact", "-1"));
        //        ddlType.SelectedValue = "-1";

        //        ddlType.DataSource = new ChildDA().GetMonthByParentID((Int32)EnumBasicData.ContactType);

        //        ddlType.DataTextField = "Description";
        //        ddlType.DataValueField = "Description";
        //        ddlType.DataBind();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //private ContactInformation SetDataContact(ContactInformation obj)
        //{
        //    try
        //    {
        //        if (Session["ContactId"] != null && Convert.ToInt32(Session["ContactId"]) > 0)
        //        {
        //            obj.Id = Convert.ToInt32(Session["ContactId"].ToString());
        //        }

        //        if ((!string.IsNullOrEmpty(txtContactName.Text.ToString())) && (txtContactName.Text.ToString() != string.Empty))
        //        {
        //            obj.Name = txtContactName.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Name = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtContactTitle.Text.ToString())) && (txtContactTitle.Text.ToString() != string.Empty))
        //        {
        //            obj.Title = txtContactTitle.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Title = "";
        //        }
        //        if (ddlType.SelectedValue != "-1")
        //        {
        //            obj.TypeOfContact = ddlType.SelectedValue.ToString();
        //        }
        //        else
        //        {
        //            obj.TypeOfContact = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtNumber.Text.ToString())) && (txtNumber.Text.ToString() != string.Empty))
        //        {
        //            obj.Phone = txtNumber.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Phone = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtEmail.Text.ToString())) && (txtEmail.Text.ToString() != string.Empty))
        //        {
        //            obj.Email = txtEmail.Text.ToString().ToLower().Trim();
        //        }
        //        else
        //        {
        //            obj.Email = "";
        //        }

        //        if ((!string.IsNullOrEmpty(txtEmergency.Text.ToString())) && (txtEmergency.Text.ToString() != string.Empty))
        //        {
        //            obj.EmergencyContact = txtEmergency.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.EmergencyContact = "";
        //        }

        //        if (chkEmergency.Checked)
        //        {
        //            obj.IsEmergencyContact = true;
        //        }
        //        else
        //        {
        //            obj.IsEmergencyContact = false;
        //        }
        //        if (chkLocation.Checked)
        //        {
        //            obj.IsLocation = true;
        //        }
        //        else
        //        {
        //            obj.IsLocation = false;
        //        }
        //        if (chkSend.Checked)
        //        {
        //            obj.IsSentEmail = true;
        //        }
        //        else
        //        {
        //            obj.IsSentEmail = false;
        //        }

        //        if (Session["OwnerId"] != null)
        //        {
        //            if (Session["OwnerId"].ToString() != string.Empty)
        //            {
        //                OwnerProfile TempOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
        //                if (TempOwner != null)
        //                {
        //                    obj.OwnerId = Session["OwnerId"].ToString();
        //                }
        //                else
        //                {
        //                    obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
        //                }
        //            }
        //            else
        //            {
        //                obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
        //            }
        //        }
        //        else
        //        {
        //            obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
        //        }

        //        if (Session["ContactId"] == null || obj.LocationId == null || obj.LocationId == string.Empty)
        //        {
        //            obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return obj;
        //}
        //private void FillControlsContact(int nId)
        //{
        //    try
        //    {
        //        if (nId > 0)
        //        {
        //            ContactInformation obj = new ContactInformationDA().GetbyID(nId);
        //            if (obj != null)
        //            {
        //                Session["ContactId"] = obj.Id;
        //                if (obj.Name != null && obj.Name.ToString() != string.Empty)
        //                {
        //                    txtContactName.Text = obj.Name;
        //                }
        //                else
        //                {
        //                    txtContactName.Text = "";
        //                }
        //                if (obj.Title != null && obj.Title.ToString() != string.Empty)
        //                {
        //                    txtContactTitle.Text = obj.Title;
        //                }
        //                else
        //                {
        //                    txtContactTitle.Text = "";
        //                }

        //                if (obj.TypeOfContact != null && obj.TypeOfContact.ToString() != string.Empty)
        //                {
        //                    ddlType.SelectedValue = obj.TypeOfContact.ToString();
        //                }
        //                else
        //                {
        //                    ddlType.SelectedValue = "-1";
        //                }

        //                if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
        //                {
        //                    txtNumber.Text = obj.Phone;
        //                }
        //                else
        //                {
        //                    txtNumber.Text = "";
        //                }

        //                if (obj.Email != null && obj.Email.ToString() != string.Empty)
        //                {
        //                    txtEmail.Text = obj.Email;
        //                }
        //                else
        //                {
        //                    txtEmail.Text = "";
        //                }

        //                if (obj.IsSentEmail != null && obj.IsSentEmail.ToString() != string.Empty)
        //                {
        //                    chkSend.Checked = Convert.ToBoolean(obj.IsSentEmail);
        //                }
        //                else
        //                {
        //                    chkSend.Checked = false;
        //                }
        //                if (obj.IsLocation != null && obj.IsLocation.ToString() != string.Empty)
        //                {
        //                    chkLocation.Checked = Convert.ToBoolean(obj.IsLocation);
        //                }
        //                else
        //                {
        //                    chkLocation.Checked = false;
        //                }
        //                if (obj.IsEmergencyContact != null && obj.IsEmergencyContact.ToString() != string.Empty)
        //                {
        //                    chkEmergency.Checked = Convert.ToBoolean(obj.IsEmergencyContact);
        //                }
        //                else
        //                {
        //                    chkEmergency.Checked = false;
        //                }

        //                if (obj.EmergencyContact != null && obj.EmergencyContact.ToString() != string.Empty)
        //                {
        //                    txtEmergency.Text = obj.Email;
        //                }
        //                else
        //                {
        //                    txtEmergency.Text = "";
        //                }

        //                btnSubmitContact.Text = "Update Contact";

        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        //private void FillContacts()
        //{
        //    try
        //    {
        //        List<ContactInformation> obj = null;
        //        if (Session["OwnerId"] != null)
        //        {
        //            obj = new ContactInformationDA().GetByOwner(Session["OwnerId"].ToString());
        //        }

        //        gvContactList.DataSource = obj;
        //        gvContactList.DataBind();
        //    }
        //    catch
        //    {

        //    }
        //}
        //#endregion

        //#region Events User
        //protected void btnSubmitUser_Click(object sender, EventArgs e)
        //{
        //    errStr = string.Empty;
        //    errStr = Validate_Control();
        //    if (errStr.Length <= 0)
        //    {
        //        try
        //        {
        //            UserProfile objUser = new UserProfile();
        //            objUser = SetDataUser(objUser);
        //            string username = "";

        //            if (Session["UserObject"] != null)
        //            {
        //                username = ((UserProfile)Session["UserObject"]).Username;
        //            }

        //            string SQL = " update UserProfile set HasUserProfile = 1  where Username = '" + username + "' ";

        //            if (Session["AddUserId"] == null || Session["AddUserId"] == "0")
        //            {
        //                if (new UserProfileDA().Insert(objUser))
        //                {
        //                    Utility.RunCMD(SQL);
        //                    Utility.RunCMDMain(SQL);
        //                    Session["AddUserId"] = null;
        //                    ClearControlsUser();
        //                    FillContacts();
        //                    Utility.DisplayMsg("User saved successfully!", this);
        //                }
        //                else
        //                {
        //                    Utility.DisplayMsg("User not saved!", this);
        //                }
        //            }
        //            else
        //            {
        //                if (new UserProfileDA().Update(objUser))
        //                {
        //                    Utility.RunCMD(SQL);
        //                    Utility.RunCMDMain(SQL);
        //                    Session["AddUserId"] = null;
        //                    ClearControlsUser();
        //                    FillContacts();
        //                    Utility.DisplayMsg("User updated successfully!", this);
        //                }
        //                else
        //                {
        //                    Utility.DisplayMsg("User not updated!", this);
        //                }
        //            }
        //        }
        //        catch (Exception ex1)
        //        {
        //            Utility.DisplayMsg(ex1.Message.ToString(), this);
        //        }
        //    }
        //    else
        //    {
        //        Utility.DisplayMsg(errStr.ToString(), this);
        //    }

        //}
        //protected void btnCloseUser_Click(object sender, EventArgs e)
        //{
        //    ClearControlsUser();
        //}

        //protected void btnDeleteUser_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //    Label hdId = (Label)gvUserList.Rows[row.RowIndex].FindControl("lblUserId");

        //    if (!String.IsNullOrEmpty(hdId.Text))
        //    {
        //        if (new ContactInformationDA().DeleteByID(Convert.ToInt32(hdId.Text)))
        //        {
        //            FillUsers();
        //        }
        //    }
        //}
        //protected void btnEditUser_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //    Label hdId = (Label)gvUserList.Rows[row.RowIndex].FindControl("lblUserId");

        //    if (!String.IsNullOrEmpty(hdId.Text))
        //    {
        //        Session["AddUserId"] = Convert.ToInt32(hdId.Text);
        //        FillControlsUser(Convert.ToInt32(hdId.Text));
        //    }
        //}
        //protected void gvUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvUserList.PageIndex = e.NewPageIndex;
        //    FillUsers();
        //}

        //protected void gvUserList_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    FillUsers();
        //}
        //#endregion

        //#region Method User
        //private void ClearControlsUser()
        //{
        //    txtUserName.Text = "";
        //    txtUserTitle.Text = "";
        //    txtUserNumber.Text = "";
        //    txtUserEmail.Text = "";
        //    ddlUserLevel.SelectedValue = "-1";
        //    chkUserLocation.Checked = false;
        //    chkAdmin.Checked = true;
        //    btnSaveUser.Text = "Add";
        //}
        //public string Validate_ControlUser()
        //{
        //    try
        //    {
        //        if ((txtUserName.Text.ToString().Length) <= 0)
        //        {
        //            errStr += "Please enter Username" + Environment.NewLine;
        //        }
        //        if ((txtUserEmail.Text.ToString().Length) <= 0)
        //        {
        //            errStr += "Please enter email address" + Environment.NewLine;
        //        }
        //        else
        //        {
        //            if (!ValidEmail(txtUserEmail.Text.ToString().Trim()))
        //            {
        //                errStr += "Invalid email address" + Environment.NewLine;
        //            }
        //        }

        //        if ((txtUserNumber.Text.ToString().Length) <= 0)
        //        {
        //            errStr += "Please enter Phone Number" + Environment.NewLine;
        //        }

        //        List<UserProfile> objUsers = new UserProfileDA().GetUsersByUserName2(txtUserName.Text.ToString());

        //        if (objUsers != null && objUsers.Count > 0)
        //        {
        //            if (Session["AddUserId"] != null)
        //            {
        //                if (objUsers.Count > 1)
        //                {
        //                    errStr += "Username already exist !! Please enter different Username." + Environment.NewLine;
        //                }
        //            }
        //            else
        //            {
        //                errStr += "Username already exist !! Please enter different Username." + Environment.NewLine;
        //            }
        //        }

        //        List<UserProfile> objUsers2 = new UserProfileDA().GetUsersByUserEmail2(txtUserEmail.Text.ToString());

        //        if (objUsers2 != null && objUsers2.Count > 0)
        //        {
        //            if (Session["AddUserId"] != null)
        //            {
        //                if (objUsers2.Count > 1)
        //                {
        //                    errStr += "Email already exist !! Please enter different Email." + Environment.NewLine;
        //                }
        //            }
        //            else
        //            {
        //                errStr += "Email already exist !! Please enter different Email." + Environment.NewLine;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return errStr;
        //}
        //public bool ValidEmail(string value)
        //{
        //    if ((value == null))
        //        return false;
        //    return reEmail.IsMatch(value);
        //}
        //private UserProfile SetDataUser(UserProfile obj)
        //{
        //    try
        //    {
        //        if (Session["AddUserId"] != null && Convert.ToInt32(Session["AddUserId"]) > 0)
        //        {
        //            obj.Id = Convert.ToInt32(Session["AddUserId"].ToString());
        //        }

        //        if ((!string.IsNullOrEmpty(txtUserName.Text.ToString())) && (txtUserName.Text.ToString() != string.Empty))
        //        {
        //            obj.Username = txtUserName.Text.ToString().ToLower().Trim();
        //        }
        //        else
        //        {
        //            obj.Username = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtUserTitle.Text.ToString())) && (txtUserTitle.Text.ToString() != string.Empty))
        //        {
        //            obj.Title = txtUserTitle.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Title = "";
        //        }
        //        if (ddlUserLevel.SelectedValue != "-1")
        //        {
        //            obj.SecurityLevel = ddlUserLevel.SelectedValue.ToString();
        //        }
        //        else
        //        {
        //            obj.SecurityLevel = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtUserNumber.Text.ToString())) && (txtUserNumber.Text.ToString() != string.Empty))
        //        {
        //            obj.Phone = txtUserNumber.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Phone = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtUserEmail.Text.ToString())) && (txtUserEmail.Text.ToString() != string.Empty))
        //        {
        //            obj.Email = txtUserEmail.Text.ToString().ToLower().Trim();
        //        }
        //        else
        //        {
        //            obj.Email = "";
        //        }

        //        if (chkAdmin.Checked)
        //        {
        //            obj.IsAdmin = false;
        //        }
        //        else
        //        {
        //            obj.IsAdmin = true;
        //        }

        //        obj.CanLogin = false;
        //        obj.IsDeleted = false;
        //        obj.UserType = Convert.ToInt32(EnumUserType.Normal).ToString();
        //        obj.IsActive = true;
        //        obj.Password = Utility.base64Encode("1234");

        //        if (Session["OwnerId"] != null)
        //        {
        //            if (Session["OwnerId"].ToString() != string.Empty)
        //            {
        //                OwnerProfile TempOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
        //                if (TempOwner == null)
        //                {
        //                    obj.OwnerId = Session["OwnerId"].ToString();
        //                }
        //                else
        //                {
        //                    obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
        //                }
        //            }
        //            else
        //            {
        //                obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
        //            }
        //        }
        //        else
        //        {
        //            obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
        //        }

        //        if (Session["AddUserId"] != null)
        //        {
        //            obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
        //            obj.UpdatedDate = DateTime.Now;
        //        }
        //        else
        //        {
        //            obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
        //            obj.CreatedDate = DateTime.Now;
        //            obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
        //        }

        //        if (obj.LocationId == string.Empty)
        //        {
        //            obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
        //        }

        //        obj.Location = obj.LocationId;
        //        obj.DatabaseName = "";
        //        obj.DatabaseLocation = "";
        //        obj.Remarks = "";
        //        obj.HasCompletedFullProfile = false;
        //        obj.HasContactProfile = false;
        //        obj.HasLedgerCode = false;
        //        obj.HasOwnerProfile = false;
        //        obj.HasPropertyLocation = false;
        //        obj.HasPropertyManagerProfile = false;
        //        obj.HasPropertyUnit = false;
        //        obj.HasSystemInfo = false;
        //        obj.HasUserProfile = false;
        //        obj.HasVendorProfile = false;

        //    }
        //    catch (Exception e)
        //    {
        //    }


        //    return obj;
        //}
        //private void FillControlsUser(int nId)
        //{
        //    try
        //    {
        //        if (nId > 0)
        //        {
        //            UserProfile obj = new UserProfileDA().GetUserByUserID(nId);
        //            if (obj != null)
        //            {
        //                Session["AddUserId"] = obj.Id;
        //                if (obj.Username != null && obj.Username.ToString() != string.Empty)
        //                {
        //                    txtUserName.Text = obj.Username;
        //                }
        //                else
        //                {
        //                    txtUserName.Text = "";
        //                }
        //                if (obj.Title != null && obj.Title.ToString() != string.Empty)
        //                {
        //                    txtUserTitle.Text = obj.Title;
        //                }
        //                else
        //                {
        //                    txtUserTitle.Text = "";
        //                }

        //                if (obj.SecurityLevel != null && obj.SecurityLevel.ToString() != string.Empty)
        //                {
        //                    ddlUserLevel.SelectedValue = obj.SecurityLevel.ToString();
        //                }


        //                if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
        //                {
        //                    txtUserNumber.Text = obj.Phone;
        //                }
        //                else
        //                {
        //                    txtUserNumber.Text = "";
        //                }

        //                if (obj.Email != null && obj.Email.ToString() != string.Empty)
        //                {
        //                    txtUserEmail.Text = obj.Email;
        //                }
        //                else
        //                {
        //                    txtUserEmail.Text = "";
        //                }


        //                if (obj.IsAdmin != null && obj.IsAdmin.ToString() != string.Empty)
        //                {
        //                    chkAdmin.Checked = Convert.ToBoolean(!obj.IsAdmin);
        //                }
        //                else
        //                {
        //                    chkAdmin.Checked = false;
        //                }

        //                btnSaveUser.Text = "Update";

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
        //private void FillUsers()
        //{
        //    try
        //    {
        //        List<UserProfile> obj = null;
        //        if (Session["OwnerId"] != null)
        //        {
        //            obj = new UserProfileDA().GetByOwner(Session["OwnerId"].ToString());
        //        }

        //        gvUserList.DataSource = obj;
        //        gvUserList.DataBind();
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        //#endregion

    }
}