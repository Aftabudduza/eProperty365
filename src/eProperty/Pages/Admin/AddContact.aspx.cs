using PropertyService.BO;
using PropertyService.DA;
using PropertyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Admin
{
    public partial class AddContact : System.Web.UI.Page
    {
        public string sUrl = string.Empty;

        #region Events      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["contactUrl"] = null;

                FillDropdowns();
                FillContacts();

                try
                {
                    if (Request.QueryString["R"] != null)
                    {
                        sUrl = Request.QueryString["R"].ToString();
                        Session["contactUrl"] = sUrl.ToLower();
                    }
                }
                catch
                {
                    sUrl = "";
                }

                if ((Session["UserObject"] != null))
                {
                    Session["ContactId"] = null;
                    Session["ContactSearch"] = null;
                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["ContactId"].ToString());
                    }
                    catch
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                        lblHeadline.InnerText = "Edit Contact";
                        Session["ContactId"] = CId;
                        FillControls(Convert.ToInt32(Session["ContactId"].ToString()));
                    }
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ContactInformation obj = new ContactInformation();
                obj = SetData(obj);

                if (Session["contactUrl"] != null)
                {
                    sUrl = Session["contactUrl"].ToString();
                }

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
                        ClearControls();
                        FillContacts();
                        // Utility.DisplayMsg("Contact saved successfully!", this);
                        //   Utility.DisplayMsgAndRedirect("Contact saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");

                        if (sUrl != string.Empty)
                        {
                            if (sUrl.Trim() == "owner")
                            {
                                Utility.DisplayMsgAndRedirect("Contact saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                            }
                            else if (sUrl.Trim() == "home")
                            {
                                Utility.DisplayMsgAndRedirect("Contact saved successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                            }
                            else if (sUrl.Trim() == "location")
                            {
                                Utility.DisplayMsgAndRedirect("Contact saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                            }
                            else
                            {
                                Utility.DisplayMsg("Contact saved successfully!", this);
                            }
                        }
                        else
                        {
                            Utility.DisplayMsg("Contact saved successfully!", this);
                        }

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
                       // Session["ContactId"] = null;
                        ClearControls();
                        FillContacts();
                        // Utility.DisplayMsg("Contact updated successfully!", this);  
                        // Utility.DisplayMsgAndRedirect("Contact updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");

                        if (sUrl != string.Empty)
                        {
                            if (sUrl.Trim() == "owner")
                            {
                                Utility.DisplayMsgAndRedirect("Contact updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                            }
                            else if (sUrl.Trim() == "home")
                            {
                                Utility.DisplayMsgAndRedirect("Contact updated successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                            }
                            else if (sUrl.Trim() == "location")
                            {
                                Utility.DisplayMsgAndRedirect("Contact updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                            }
                            else
                            {
                                Utility.DisplayMsg("Contact updated successfully!", this);
                            }
                        }
                        else
                        {
                            Utility.DisplayMsg("Contact updated successfully!", this);
                        }

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
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ContactSearch"] = null;
                if (search.Value.ToString().Trim() != string.Empty)
                {
                    string strWhere = string.Empty;
                    strWhere = search.Value.ToString().Trim();
                    Session["ContactSearch"] = strWhere;
                  //  List<ContactInformation> obj = new ContactInformationDA().GetBySearch(strWhere);
                    List<ContactInformation> obj = null;

                    var isAdmin = false;
                    if (Session["UserObject"] != null)
                    {
                        isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                              ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                              : false;

                        if (isAdmin == true)
                        {
                            obj = new ContactInformationDA().GetBySearch(strWhere);
                        }
                        else if (Session["OwnerId"] != null)
                        {
                            obj = new ContactInformationDA().GetBySearchAndOwnerId(strWhere, Session["OwnerId"].ToString());
                        }

                    }

                    gvContactList.DataSource = obj;
                    gvContactList.DataBind();
                }
            }
            catch (Exception ex) { }
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
                FillControls(Convert.ToInt32(hdId.Text));
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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearControls();
            if (Session["contactUrl"] != null)
            {
                sUrl = Session["contactUrl"].ToString();
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
                else if (sUrl.Trim() == "location")
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddLocation.aspx", false);
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

        private void ClearControls()
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
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtRegion.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
            ddlCountry.SelectedValue = "-1";
            btnSave.Text = "Add Contact";
            lblHeadline.InnerText = "Create / Change Contact Information";
        }
        private void FillDropdowns()
        {
            try
            {

                ddlType.Items.Clear();
                ddlType.AppendDataBoundItems = true;
                ddlType.Items.Add(new ListItem("None", "-1"));
                ddlType.SelectedValue = "-1";

                ddlType.DataSource = new ChildDA().GetByParentID((Int32)EnumBasicData.ContactType);

                ddlType.DataTextField = "Description";
                ddlType.DataValueField = "Description";
                ddlType.DataBind();

            }
            catch (Exception ex)
            {

            }

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
        private ContactInformation SetData(ContactInformation obj)
        {
            try
            {
                obj = new ContactInformation();

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


                        btnSave.Text = "Update Contact";

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
                var isAdmin = false;
                if (Session["UserObject"] != null)
                {
                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                          ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                          : false;

                    if (isAdmin == true)
                    {
                        obj = new ContactInformationDA().GetAllContactInformation();
                    }
                    else if (Session["OwnerId"] != null)
                    {
                        obj = new ContactInformationDA().GetByOwner(Session["OwnerId"].ToString());
                    }

                }

                gvContactList.DataSource = obj;
                gvContactList.DataBind();
            }
            catch
            {

            }
        }

        #endregion
    }
}