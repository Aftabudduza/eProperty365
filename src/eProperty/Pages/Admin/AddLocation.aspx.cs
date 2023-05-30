using PropertyService.BO;
using PropertyService.DA;
using PropertyService.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Admin
{
    public partial class AddLocation : System.Web.UI.Page
    {
        public String errStr = String.Empty;
        private Regex reEmail = new Regex("^(?:[0-9A-Z_-]+(?:\\.[0-9A-Z_-]+)*@[0-9A-Z-]+(?:\\.[0-9A-Z-]+)*(?:\\.[A-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        
        #region Events Location   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if ((Session["UserObject"] != null))
                {
                    Session["LocationId"] = null;
                    Session["LocationFileName"] = null;
                    Session["ContactId"] = null;
                    Session["AddUserId"] = null;
                    FillDropdowns();
                    //FillContactType();
                    //FillContacts();
                    //FillUsers();
                    //FillVendor();
                    //FillddlControlsCAM();
                    //FillCAMs();
                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["LocationId"].ToString());
                    }
                    catch
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                        lblHeadline.InnerText = "Edit Location";
                        Session["LocationId"] = CId;
                        FillControls(Convert.ToInt32(Session["LocationId"].ToString()));
                    }

                    bool isAdmin = false;
                    if (Session["UserObject"] != null)
                    {
                        isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin) : false;
                    }

                    if (isAdmin == false)
                    {
                        ddlOwner.SelectedValue = Session["OwnerId"].ToString();
                        ddlOwner.Enabled = false;
                        ddlOwnerTop.SelectedValue = Session["OwnerId"].ToString();
                        ddlOwnerTop.Enabled = false;
                    }

                    if (Session["OwnerId"] != null)
                    {
                        try
                        {
                            ddlPropertyManager.Items.Clear();
                            ddlPropertyManager.AppendDataBoundItems = true;
                            ddlPropertyManager.Items.Add(new ListItem("Select Property Manager", "-1"));
                            List<PropertyManagerProfile> objPropertyManagers = new PropertyManagerProfileDA().GetByOwnerId(ddlOwner.SelectedValue);
                            if (objPropertyManagers != null && objPropertyManagers.Count > 0)
                            {
                                foreach (PropertyManagerProfile obj in objPropertyManagers)
                                {
                                    string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                                    ddlPropertyManager.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                                }
                            }

                            ddlPropertyManager.DataBind();
                            ddlPropertyManager.SelectedValue = "-1";

                            //ddlPropertyManager.DataSource = new PropertyManagerProfileDA().GetByOwnerId(ddlOwner.SelectedValue);
                            //ddlPropertyManager.DataTextField = "FirstName";
                            //ddlPropertyManager.DataValueField = "Serial";
                            //ddlPropertyManager.DataBind();
                            //ddlPropertyManager.SelectedValue = "-1";
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            ddlPropertyManagerTop.Items.Clear();
                            ddlPropertyManagerTop.AppendDataBoundItems = true;
                            ddlPropertyManagerTop.Items.Add(new ListItem("Select Property Manager", "-1"));

                            List<PropertyManagerProfile> objPropertyManagers = new PropertyManagerProfileDA().GetByOwnerId(ddlOwnerTop.SelectedValue);
                            if (objPropertyManagers != null && objPropertyManagers.Count > 0)
                            {
                                foreach (PropertyManagerProfile obj in objPropertyManagers)
                                {
                                    string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                                    ddlPropertyManagerTop.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                                }
                            }

                            ddlPropertyManagerTop.DataBind();
                            ddlPropertyManagerTop.SelectedValue = "-1";

                            //ddlPropertyManagerTop.Items.Clear();
                            //ddlPropertyManagerTop.AppendDataBoundItems = true;
                            //ddlPropertyManagerTop.Items.Add(new ListItem("Select Property Manager", "-1"));
                            //ddlPropertyManagerTop.DataSource = new PropertyManagerProfileDA().GetByOwnerId(ddlOwnerTop.SelectedValue);
                            //ddlPropertyManagerTop.DataTextField = "FirstName";
                            //ddlPropertyManagerTop.DataValueField = "Serial";
                            //ddlPropertyManagerTop.DataBind();
                            //ddlPropertyManagerTop.SelectedValue = "-1";
                        }
                        catch (Exception ex)
                        {
                        }

                    }

                    FillLocation();

                    if (Session["LocationId"] == null)
                    {
                        lblId.Text  = new LocationDA().MakeAutoGenLocation("1", "Location");
                    }
                }
            }
        }
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

            //try
            //{
            //    ddlOwner.Items.Clear();
            //    ddlOwner.AppendDataBoundItems = true;
            //    ddlOwner.Items.Add(new ListItem("Select Owner", "-1"));
            //    ddlOwner.DataSource = new OwnerProfileDA().GetAllOwnersInfo();
            //    ddlOwner.DataTextField = "FirstName";
            //    ddlOwner.DataValueField = "Serial";
            //    ddlOwner.DataBind();
            //    ddlOwner.SelectedValue = "-1";
            //}
            //catch (Exception ex)
            //{
            //}
            //try
            //{
            //    ddlOwnerTop.Items.Clear();
            //    ddlOwnerTop.AppendDataBoundItems = true;
            //    ddlOwnerTop.Items.Add(new ListItem("Select Owner", "-1"));
            //    ddlOwnerTop.DataSource = new OwnerProfileDA().GetAllOwnersInfo();
            //    ddlOwnerTop.DataTextField = "FirstName";
            //    ddlOwnerTop.DataValueField = "Serial";
            //    ddlOwnerTop.DataBind();
            //    ddlOwnerTop.SelectedValue = "-1";
            //}
            //catch (Exception ex)
            //{
            //}
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
                ddlOwner.Items.Clear();
                ddlOwner.AppendDataBoundItems = true;
                ddlOwner.Items.Add(new ListItem("Select Owner", "-1"));

                ddlOwnerTop.Items.Clear();
                ddlOwnerTop.AppendDataBoundItems = true;
                ddlOwnerTop.Items.Add(new ListItem("Select Owner", "-1"));

                List<OwnerProfile> objOwners = new OwnerProfileDA().GetAllOwnersInfo();
                if (objOwners != null && objOwners.Count > 0)
                {
                    foreach (OwnerProfile obj in objOwners)
                    {
                        string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                        ddlOwner.Items.Add(new ListItem(sName, obj.Serial.ToString()));

                        ddlOwnerTop.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
                ddlOwner.DataBind();
                ddlOwner.SelectedValue = "-1";

                ddlOwnerTop.DataBind();
                ddlOwnerTop.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
            }


        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Location obj = new Location();
                obj = SetData(obj);

                string username = "";
                if (Session["UserObject"] != null)
                {
                    username = ((UserProfile)Session["UserObject"]).Username;
                }

                string SQL = " update UserProfile set HasPropertyLocation = 1  where Username = '" + username + "' ";

                if (Session["LocationId"] == null || Session["LocationId"] == "0")
                {                    
                    if (new LocationDA().Insert(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);
                        Session["LocationId"] = null;
                        FillLocation();
                        ClearControls();
                      //  Utility.DisplayMsg("Location saved successfully!", this);
                        if (Session["HasCompletedFullProfile"] != null)
                        {
                            if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == false)
                            {
                                Utility.DisplayMsgAndRedirect("Location saved successfully!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                            }
                            else
                            {
                                Utility.DisplayMsgAndRedirect("Location saved successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                            }
                        }
                        else
                        {
                            Utility.DisplayMsgAndRedirect("Location saved successfully!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("Location not saved!", this);
                    }
                }
                else
                {                    
                    if (new LocationDA().Update(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);
                        Session["LocationId"] = null;
                        FillLocation();
                        ClearControls();
                        // Utility.DisplayMsg("Location updated successfully!", this);      
                        if (Session["HasCompletedFullProfile"] != null)
                        {
                            if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == false)
                            {
                                Utility.DisplayMsgAndRedirect("Location updated successfully!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                            }
                            else
                            {
                                Utility.DisplayMsgAndRedirect("Location updated successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                            }
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("Location not updated!", this);
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
        }

        protected void btnBack_Click(object sender, EventArgs e)
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
        protected void btnImport_Click(object sender, EventArgs e)
        {
            ImportFromCSV();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportTransactionList();
        }

        protected void ddlOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlPropertyManager.Items.Clear();
                ddlPropertyManager.AppendDataBoundItems = true;
                ddlPropertyManager.Items.Add(new ListItem("Select Property Manager", "-1"));
                ddlPropertyManager.DataSource = new PropertyManagerProfileDA().GetByOwnerId(ddlOwner.SelectedValue);
                ddlPropertyManager.DataTextField = "FirstName";
                ddlPropertyManager.DataValueField = "Serial";
                ddlPropertyManager.DataBind();
                ddlPropertyManager.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvLocationList.Rows[row.RowIndex].FindControl("lblId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new LocationDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillLocation();
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvLocationList.Rows[row.RowIndex].FindControl("lblId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                FillControls(Convert.ToInt32(hdId.Text));
            }
        }
        protected void gvLocationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocationList.PageIndex = e.NewPageIndex;
            FillLocation();
        }
        protected void gvLocationList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillLocation();
        }
        protected void ddlOwnerTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlPropertyManagerTop.Items.Clear();
                ddlPropertyManagerTop.AppendDataBoundItems = true;
                ddlPropertyManagerTop.Items.Add(new ListItem("Select Property Manager", "-1"));
                //ddlPropertyManagerTop.DataSource = new PropertyManagerProfileDA().GetByOwnerId(ddlOwnerTop.SelectedValue);
                //ddlPropertyManagerTop.DataTextField = "FirstName";
                //ddlPropertyManagerTop.DataValueField = "Serial";

                List<PropertyManagerProfile> objPropertyManagers = new PropertyManagerProfileDA().GetByOwnerId(ddlOwnerTop.SelectedValue);
                if (objPropertyManagers != null && objPropertyManagers.Count > 0)
                {
                    foreach (PropertyManagerProfile obj in objPropertyManagers)
                    {
                        string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                        ddlPropertyManagerTop.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }

                ddlPropertyManagerTop.DataBind();
                ddlPropertyManagerTop.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<Location> obj = new LocationDA().GetBySearch(ddlOwnerTop.SelectedValue, ddlPropertyManagerTop.SelectedValue);
                gvLocationList.DataSource = obj;
                gvLocationList.DataBind();
            }
            catch (Exception ex)
            { }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            savelocation();
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddUser.aspx?R=location", false);
        }

        //protected void btnAddCAMExpense_Click(object sender, EventArgs e)
        //{
        //    savelocation();
        //    Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddCAMExpense.aspx?R=location", false);
        //}

        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            savelocation();
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddContact.aspx?R=location", false);
        }

        protected void btnAddVendor_Click(object sender, EventArgs e)
        {
            savelocation();
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddVendor.aspx?R=location", false);
        }

        #endregion

        #region Method Location                                                                                    
        private void ClearControls()
        {
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
            txtComment.Text = "";
            txtCost.Text = "";
            txtLatitude.Text = "";
            txtLongitute.Text = "";
            txtLocationName.Text = "";
            txtLots.Text = "";
            txtPropertyTax.Text = "";
            txtPurchasedDate.Text = "";
            txtRegion.Text = "";
            txtSchoolDistract.Text = "";
            txtSchoolTax.Text = "";
            txtSize.Text = "";
            txtUnits.Text = "";
            ddlCountry.SelectedValue = "-1";
            ddlOwner.SelectedValue = "-1";
            ddlPropertyManager.SelectedValue = "-1";
            ddlState.SelectedValue = "AL";
            chkCondo.Checked = false;
            chkRent.Checked = false;
            chkSecurity.Checked = false;
            btnSave.Text = "Add";
            lblHeadline.InnerText = "Add Location";
        }
        private Location SetData(Location obj)
        {
            try
            {
                obj = new Location();

                if (Session["LocationId"] != null && Convert.ToInt32(Session["LocationId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["LocationId"].ToString());
                }

                if (!string.IsNullOrEmpty(uplProduct.FileName))
                {
                    //read the file in
                    string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\Location\\");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fileName = this.uplProduct.FileName;
                    string nFile = Path.Combine(filePath, fileName);
                    Session["LocationFileName"] = fileName;
                    obj.Logo = fileName;

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
                    if (Session["LocationFileName"] != null)
                    {
                        obj.Logo = Session["LocationFileName"].ToString();
                    }
                }

                if (rdoLocationType.Items[0].Selected == true)
                {
                    obj.LocationType = "1";
                }
                else if (rdoLocationType.Items[1].Selected == true)
                {
                    obj.LocationType = "2";
                }
                else if (rdoLocationType.Items[2].Selected == true)
                {
                    obj.LocationType = "3";
                }

                if ((!string.IsNullOrEmpty(txtAddress.Text.ToString())) && (txtAddress.Text.ToString() != string.Empty))
                {
                    obj.Address = txtAddress.Text.ToString();
                }
                else
                {
                    obj.Address = "";
                }

                if ((!string.IsNullOrEmpty(txtAddress1.Text.ToString())) && (txtAddress1.Text.ToString() != string.Empty))
                {
                    obj.Address1 = txtAddress1.Text.ToString();
                }
                else
                {
                    obj.Address1 = "";
                }

                if (ddlCountry.SelectedValue != "-1")
                {
                    obj.Country = ddlCountry.SelectedValue.Trim();
                }
                else
                {
                    obj.Country = "";
                }

                if ((!string.IsNullOrEmpty(txtRegion.Text.ToString())) && (txtRegion.Text.ToString() != string.Empty))
                {
                    obj.Region = txtRegion.Text.ToString();
                }
                else
                {
                    obj.Region = "";
                }

                if (ddlState.SelectedValue != "-1")
                {
                    obj.State = ddlState.SelectedValue.Trim();
                }
                else
                {
                    obj.State = "";
                }

                if ((!string.IsNullOrEmpty(txtCity.Text.ToString())) && (txtCity.Text.ToString() != string.Empty))
                {
                    obj.City = txtCity.Text.ToString();
                }
                else
                {
                    obj.City = "";
                }

                obj.Zip = txtZip.Text.ToString().Trim();
                obj.LocationName = txtLocationName.Text.ToString().Trim();
                obj.SchoolDistract = txtSchoolDistract.Text.ToString().Trim();

                if (ddlOwner.SelectedValue != "-1")
                {
                    obj.OwnerId = ddlOwner.SelectedValue.Trim();
                }
                else
                {
                    obj.OwnerId = "";
                }
                if (ddlPropertyManager.SelectedValue != "-1")
                {
                    obj.PropertyManagerId = ddlPropertyManager.SelectedValue.Trim();
                }
                else
                {
                    obj.PropertyManagerId = "";
                }

                obj.Latitude = txtLatitude.Text.ToString().Trim();
                obj.Longitude = txtLongitute.Text.ToString().Trim();
                obj.Units = txtUnits.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtUnits.Text.ToString().Trim()) : 0;
                obj.Lots = txtLots.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtLots.Text.ToString().Trim()) : 0;
                obj.TotalSize = txtSize.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtSize.Text.ToString().Trim()) : 0;
                obj.Cost = txtCost.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtCost.Text.ToString().Trim()) : 0;
                obj.DatePurchased = txtPurchasedDate.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtPurchasedDate.Text.ToString().Trim()) : DateTime.Now;
                obj.SchoolTax = txtSchoolTax.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtSchoolTax.Text.ToString().Trim()) : 0;
                obj.PropertyTax = txtPropertyTax.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtPropertyTax.Text.ToString().Trim()) : 0;
                obj.Comment = txtComment.Text.ToString().Trim();
                obj.IsDelete = false;

                if (chkSecurity.Checked == true)
                {
                    obj.IsBackgroundCheck = true;
                }
                else
                {
                    obj.IsBackgroundCheck = false;
                }
                if (chkCondo.Checked == true)
                {
                    obj.IsCondoFee = true;
                }
                else
                {
                    obj.IsCondoFee = false;
                }
                if (chkRent.Checked == true)
                {
                    obj.IsRentMonthly = true;
                }
                else
                {
                    obj.IsRentMonthly = false;
                }

                if (Session["LocationId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                }
                else
                {
                  //  obj.Serial = new LocationDA().MakeAutoGenLocation("1", "Location");
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(lblId.Text.ToString()))
                {
                    obj.Serial = lblId.Text.ToString();
                }
                else
                {
                    obj.Serial = "";
                }

                if (Session["LocationId"] == null || string.IsNullOrEmpty(obj.Serial))
                {
                    obj.Serial = new LocationDA().MakeAutoGenLocation("1", "Location");
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
                    Location obj = new LocationDA().GetbyID(nId);
                    if (obj != null)
                    {
                        Session["LocationId"] = obj.Id;

                        if (obj.LocationType != null)
                        {
                            if (Convert.ToInt32(obj.LocationType) == 1)
                            {
                                rdoLocationType.Items[0].Selected = true;
                            }
                            else if (Convert.ToInt32(obj.LocationType) == 2)
                            {
                                rdoLocationType.Items[1].Selected = true;
                            }
                            else if (Convert.ToInt32(obj.LocationType) == 3)
                            {
                                rdoLocationType.Items[2].Selected = true;
                            }
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
                            ddlCountry.SelectedValue = obj.Country;
                        }
                        else
                        {
                            ddlCountry.SelectedValue = "-1";
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
                            ddlState.SelectedValue = obj.State;
                        }
                        else
                        {
                            ddlState.SelectedValue = "-1";
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
                        if (obj.LocationName != null && obj.LocationName.ToString() != string.Empty)
                        {
                            txtLocationName.Text = obj.LocationName;
                        }
                        else
                        {
                            txtLocationName.Text = "";
                        }
                        if (obj.Logo != null && obj.Logo != string.Empty)
                        {
                            imgLocation.ImageUrl = Utility.WebUrl + "/Uploads/Files/Location/" + obj.Logo;
                        }

                        if (obj.SchoolDistract != null && obj.SchoolDistract.ToString() != string.Empty)
                        {
                            txtSchoolDistract.Text = obj.SchoolDistract;
                        }
                        else
                        {
                            txtSchoolDistract.Text = "";
                        }

                        if (obj.OwnerId != null && obj.OwnerId.ToString() != string.Empty)
                        {
                            ddlOwner.SelectedValue = obj.OwnerId;
                        }
                        else
                        {
                            ddlOwner.SelectedValue = "-1";
                        }

                        if (obj.PropertyManagerId != null && obj.PropertyManagerId.ToString() != string.Empty)
                        {
                            ddlPropertyManager.SelectedValue = obj.PropertyManagerId;
                        }
                        else
                        {
                            ddlPropertyManager.SelectedValue = "-1";
                        }
                        if (obj.Latitude != null && obj.Latitude.ToString() != string.Empty)
                        {
                            txtLatitude.Text = obj.Latitude;
                        }
                        else
                        {
                            txtLatitude.Text = "";
                        }
                        if (obj.Longitude != null && obj.Longitude.ToString() != string.Empty)
                        {
                            txtLongitute.Text = obj.Longitude;
                        }
                        else
                        {
                            txtLongitute.Text = "";
                        }
                        if (obj.DatePurchased != null && obj.DatePurchased.ToString() != string.Empty)
                        {
                            txtPurchasedDate.Text = Convert.ToDateTime(obj.DatePurchased).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            txtPurchasedDate.Text = "";
                        }

                        if (obj.Units != null && obj.Units.ToString() != string.Empty)
                        {
                            txtUnits.Text = Convert.ToDecimal(obj.Units).ToString("#.00");
                        }
                        else
                        {
                            txtUnits.Text = "";
                        }
                        if (obj.Lots != null && obj.Lots.ToString() != string.Empty)
                        {
                            txtLots.Text = Convert.ToDecimal(obj.Lots).ToString("#.00");
                        }
                        else
                        {
                            txtLots.Text = "";
                        }
                        if (obj.TotalSize != null && obj.TotalSize.ToString() != string.Empty)
                        {
                            txtSize.Text = Convert.ToDecimal(obj.TotalSize).ToString("#.00");
                        }
                        else
                        {
                            txtSize.Text = "";
                        }
                        if (obj.Cost != null && obj.Cost.ToString() != string.Empty)
                        {
                            txtCost.Text = Convert.ToDecimal(obj.Cost).ToString("#.00");
                        }
                        else
                        {
                            txtCost.Text = "";
                        }
                        if (obj.SchoolTax != null && obj.SchoolTax.ToString() != string.Empty)
                        {
                            txtSchoolTax.Text = Convert.ToDecimal(obj.SchoolTax).ToString("#.00");
                        }
                        else
                        {
                            txtSchoolTax.Text = "";
                        }
                        if (obj.PropertyTax != null && obj.PropertyTax.ToString() != string.Empty)
                        {
                            txtPropertyTax.Text = Convert.ToDecimal(obj.PropertyTax).ToString("#.00");
                        }
                        else
                        {
                            txtPropertyTax.Text = "";
                        }

                        if (obj.Comment != null && obj.Comment.ToString() != string.Empty)
                        {
                            txtComment.Text = obj.Comment;
                        }
                        else
                        {
                            txtComment.Text = "";
                        }

                        if (obj.IsBackgroundCheck != null)
                        {
                            chkSecurity.Checked = Convert.ToBoolean(obj.IsBackgroundCheck);
                        }
                        if (obj.IsCondoFee != null)
                        {
                            chkCondo.Checked = Convert.ToBoolean(obj.IsCondoFee);
                        }
                        if (obj.IsRentMonthly != null)
                        {
                            chkRent.Checked = Convert.ToBoolean(obj.IsRentMonthly);
                        }
                        if (obj.Serial != null && obj.Serial.ToString() != string.Empty)
                        {
                            lblId.Text = obj.Serial;
                        }
                        btnSave.Text = "Update";

                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        private void FillLocation()
        {
            try
            {
                var isAdmin = false;
                if (Session["UserObject"] != null)
                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                        ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                        : false;

                List<Location> obj = null;
                if (isAdmin == true)
                {
                    obj = new LocationDA().GetAllInformation();
                }
                else
                {
                    if (Session["OwnerId"] != null)
                        obj = new LocationDA().GetByOwner(Session["OwnerId"].ToString());
                }

               

                gvLocationList.DataSource = obj;
                gvLocationList.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        private void savelocation()
        {
            try
            {
                Location obj = new Location();
                obj = SetData(obj);

                string username = "";
                if (Session["UserObject"] != null)
                {
                    username = ((UserProfile)Session["UserObject"]).Username;
                }

                string SQL = " update UserProfile set HasPropertyLocation = 1  where Username = '" + username + "' ";

                if (Session["LocationId"] == null || Session["LocationId"] == "0")
                {
                    if (new LocationDA().Insert(obj))
                    {
                        //Utility.RunCMD(SQL);
                        //Utility.RunCMDMain(SQL);
                        Session["LocationId"] = null;                       
                        ClearControls();
                        //Utility.DisplayMsg("Location saved successfully!", this);
                    }
                    else
                    {
                        //Utility.DisplayMsg("Location not saved!", this);
                    }
                }
                else
                {
                    if (new LocationDA().Update(obj))
                    {
                        //Utility.RunCMD(SQL);
                        //Utility.RunCMDMain(SQL);
                        Session["LocationId"] = null;                       
                        ClearControls();
                        //Utility.DisplayMsg("Location updated successfully!", this);
                    }
                    else
                    {
                        //Utility.DisplayMsg("Location not updated!", this);
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }

        private void ImportFromCSV()
        {
            try
            {
                int nTotal = 0;
               

                string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\LocationCSV\\");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = this.uplLogo.FileName;
                string csvPath = Path.Combine(filePath, fileName);

                try
                {
                    if (System.IO.File.Exists(csvPath))
                    {
                        System.IO.File.Delete(csvPath);
                    }

                }
                catch (Exception ex)
                {
                }

                uplLogo.SaveAs(csvPath);

             

                //Create a DataTable.
                DataTable dt = new DataTable();

                dt.Columns.AddRange(new DataColumn[26] { new DataColumn("LocationType", typeof(string)),
                                    new DataColumn("Address", typeof(string)),
                                    new DataColumn("Address1", typeof(string)),
                                    new DataColumn("Region", typeof(string)),
                                    new DataColumn("Country", typeof(string)),
                                    new DataColumn("State", typeof(string)),
                                    new DataColumn("City", typeof(string)),
                                    new DataColumn("Zip",typeof(string)),
                                    new DataColumn("OwnerId", typeof(string)),
                                    new DataColumn("PropertyManagerId", typeof(string)),
                                    new DataColumn("LocationName", typeof(string)),
                                    new DataColumn("SchoolDistract", typeof(string)),
                                    new DataColumn("Longitude", typeof(string)),
                                    new DataColumn("Latitude", typeof(string)),
                                    new DataColumn("Units", typeof(string)),
                                    new DataColumn("Lots", typeof(string)),
                                    new DataColumn("TotalSize",typeof(string)),
                                    new DataColumn("DatePurchased", typeof(string)),
                                    new DataColumn("Cost", typeof(string)),
                                    new DataColumn("SchoolTax", typeof(string)),
                                    new DataColumn("PropertyTax", typeof(string)),
                                    new DataColumn("Comment", typeof(string)),
                                    new DataColumn("IsBackgroundCheck", typeof(string)),
                                    new DataColumn("IsCondoFee", typeof(string)),
                                    new DataColumn("IsRentMonthly",typeof(string)),
                                    new DataColumn("Logo", typeof(string))});

                //Read the contents of CSV file.
                string csvData = File.ReadAllText(csvPath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        try
                        {
                            dt.Rows.Add();
                            int i = 0;

                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                        catch (Exception ex2)
                        {

                        }

                    }
                }

                if (dt != null && dt.Rows.Count > 1)
                {
                    dt.Rows.RemoveAt(0);
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                           

                            string sUnits = dr["Units"] != null ? dr["Units"].ToString() : "";
                            string sLots = dr["Lots"] != null ? dr["Lots"].ToString() : "";
                            string sTotalSize = dr["TotalSize"] != null ? dr["TotalSize"].ToString() : "";
                            string sCost = dr["Cost"] != null ? dr["Cost"].ToString() : "";
                            string sSchoolTax = dr["SchoolTax"] != null ? dr["SchoolTax"].ToString() : "";
                            string sPropertyTax = dr["PropertyTax"] != null ? dr["PropertyTax"].ToString() : "";
                            string dDatePurchased = dr["DatePurchased"] != null ? dr["DatePurchased"].ToString() : "";

                            string sIsBackgroundCheck = dr["IsBackgroundCheck"] != null ? dr["IsBackgroundCheck"].ToString() : "0";
                            string sIsCondoFee = dr["IsCondoFee"] != null ? dr["IsCondoFee"].ToString() : "0";
                            string sIsRentMonthly = dr["IsRentMonthly"] != null ? dr["IsRentMonthly"].ToString() : "0";

                            Location objLocation = new Location()
                            {
                                LocationType = dr["LocationType"] != null ? dr["LocationType"].ToString() : "1",
                                Serial = new LocationDA().MakeAutoGenLocation("1", "Location"),
                                Address = dr["Address"] != null ? dr["Address"].ToString() : "",
                                Address1 = dr["Address1"] != null ? dr["Address1"].ToString() : "",
                                Region = dr["Region"] != null ? dr["Region"].ToString() : "",
                                Country = dr["Country"] != null ? dr["Country"].ToString() : "",
                                State = dr["State"] != null ? dr["State"].ToString() : "",
                                City = dr["City"] != null ? dr["City"].ToString() : "",
                                Zip = dr["Zip"] != null ? dr["Zip"].ToString() : "",
                                OwnerId = dr["OwnerId"] != null ? dr["OwnerId"].ToString() : Session["OwnerId"].ToString(),
                                PropertyManagerId = dr["PropertyManagerId"] != null ? dr["PropertyManagerId"].ToString() : "",
                                LocationName = dr["LocationName"] != null ? dr["LocationName"].ToString() : "",
                                SchoolDistract = dr["SchoolDistract"] != null ? dr["SchoolDistract"].ToString() : "",
                                Longitude = dr["Longitude"] != null ? dr["Longitude"].ToString() : "",
                                Latitude = dr["Latitude"] != null ? dr["Latitude"].ToString() : "",
                                Units = sUnits.Trim().Length > 0 ? Convert.ToDecimal(sUnits.Trim()) : 0,
                                Lots = sLots.Trim().Length > 0 ? Convert.ToDecimal(sLots.Trim()) : 0,
                                TotalSize = sTotalSize.Trim().Length > 0 ? Convert.ToDecimal(sTotalSize.Trim()) : 0,
                                DatePurchased = dDatePurchased.Trim().Length > 0 ? Convert.ToDateTime(dDatePurchased.Trim()) : DateTime.Now,
                                Cost = sCost.Trim().Length > 0 ? Convert.ToDecimal(sCost.Trim()) : 0,
                                SchoolTax = sSchoolTax.Trim().Length > 0 ? Convert.ToDecimal(sSchoolTax.Trim()) : 0,
                                PropertyTax = sPropertyTax.Trim().Length > 0 ? Convert.ToDecimal(sPropertyTax.Trim()) : 0,
                                Comment = dr["Comment"] != null ? dr["Comment"].ToString() : "",
                                IsBackgroundCheck = sIsBackgroundCheck.Trim() == "1" ? true : false,
                                IsCondoFee = sIsCondoFee.Trim() == "1" ? true : false,
                                IsDelete = false,
                                IsRentMonthly = sIsRentMonthly.Trim() == "1"  ? true : false,
                                Logo = dr["Logo"] != null ? dr["Logo"].ToString() : "",                                
                                CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id),
                                CreatedDate = DateTime.Now
                            };

                            if (new LocationDA().Insert(objLocation))
                            {
                                nTotal = nTotal + 1;
                            }
                        }
                        catch (Exception ex2)
                        {
                            Utility.DisplayMsg("Technical Issues found !!", this);
                        }
                    }
                }

                if(nTotal > 0)
                {
                    FillLocation();
                    Utility.DisplayMsg("Location Imported successfully !!", this);
                }

                else
                {
                    Utility.DisplayMsg("Technical Issues found !!", this);
                }
                

            }
            catch (Exception ex1)
            {
                Utility.DisplayMsg(ex1.Message.ToString(), this);
            }
        }

        public void ExportTransactionList()
        {
            try
            {
                var isAdmin = false;
                if (Session["UserObject"] != null)
                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                        ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                        : false;

                List<Location> obj = null;
                if (isAdmin == true)
                {
                    obj = new LocationDA().GetAllInformation();
                }
                else
                {
                    if (Session["OwnerId"] != null)
                        obj = new LocationDA().GetByOwner(Session["OwnerId"].ToString());
                }


                string sCSV = "";
                string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\Location\\");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (Session["UserObject"] != null)
                {
                    UserProfile objUser = new UserProfile();
                    objUser = (UserProfile)Session["UserObject"];
                    string sOwnerId = objUser.OwnerId;
                    sCSV = sOwnerId + "_" + DateTime.Now.ToString("MMddyyyyhhmm") + ".csv";
                }

                string fileName = sCSV;
                string nFile = Path.Combine(filePath, fileName);

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

                List<Location> listIncome = obj.ToList();
                /*List to DataTable conversion*/
                DataTable dt = ToDataTable(listIncome);

                if (dt != null && dt.Rows.Count > 0)
                {
                    CreateCSVFile(dt, nFile);
                    Response.Redirect(Utility.WebUrl + "/Uploads/Files/Location/" + fileName, false);
                }
            }
            catch (Exception ex)
            {
            }

        }

      
        public void CreateCSVFile(DataTable dt, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            
            // we will write the table headers.


            int iColCount = dt.Columns.Count;


            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }


            sw.Write(sw.NewLine);


            // Now write all the rows.


            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        private DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }


            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }


            return tb;
        }

        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
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

        //protected void btnDeleteContact_Click(object sender, EventArgs e)
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
        //protected void btnEditContact_Click(object sender, EventArgs e)
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

        //        ddlType.DataSource = new ChildDA().GetByParentID((Int32)EnumBasicData.ContactType);

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
        //    errStr = Validate_ControlUser();
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

        //#region Events Vendor
        //protected void btnSubmitVendor_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        VendorProfile obj = new VendorProfile();
        //        obj = SetDataVendor(obj);
        //        string username = "";
        //        if (Session["UserObject"] != null)
        //        {
        //            username = ((UserProfile)Session["UserObject"]).Username;
        //        }

        //        string SQL = " update UserProfile set HasVendorProfile = 1  where Username = '" + username + "' ";
        //        if (Session["VendorId"] == null || Session["VendorId"] == "0")
        //        {
        //            if (new VendorDA().Insert(obj))
        //            {
        //                Utility.RunCMD(SQL);
        //                Utility.RunCMDMain(SQL);
        //                Session["VendorId"] = null;
        //                ClearControlsVendor();
        //                FillVendor();
        //                Utility.DisplayMsg("Vendor saved successfully!", this);
        //            }
        //            else
        //            {
        //                Utility.DisplayMsg("Vendor not saved!", this);
        //            }
        //        }
        //        else
        //        {
        //            if (new VendorDA().Update(obj))
        //            {
        //                Session["VendorId"] = null;
        //                ClearControlsVendor();
        //                FillVendor();
        //                Utility.DisplayMsg("Vendor updated successfully!", this);
        //            }
        //            else
        //            {
        //                Utility.DisplayMsg("Vendor not updated!", this);
        //            }
        //        }
        //    }
        //    catch (Exception ex1)
        //    {

        //    }
        //}
        //protected void btnCloseVendor_Click(object sender, EventArgs e)
        //{
        //    ClearControlsVendor();
        //}

        //protected void btnDeleteVendor_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //    Label hdId = (Label)gvVendorList.Rows[row.RowIndex].FindControl("lblVendorId");

        //    if (!String.IsNullOrEmpty(hdId.Text))
        //    {
        //        if (new VendorDA().DeleteByID(Convert.ToInt32(hdId.Text)))
        //        {
        //            FillVendor();
        //        }
        //    }
        //}
        //protected void btnEditVendor_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //    Label hdId = (Label)gvVendorList.Rows[row.RowIndex].FindControl("lblVendorId");

        //    if (!String.IsNullOrEmpty(hdId.Text))
        //    {
        //        FillControlsVendor(Convert.ToInt32(hdId.Text));
        //    }
        //}
        //protected void gvVendorList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvVendorList.PageIndex = e.NewPageIndex;
        //    FillVendor();
        //}
        //protected void gvVendorList_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    FillVendor();
        //}

        //#endregion

        //#region Method Vendor

        //private void ClearControlsVendor()
        //{
        //    ddlStateVendor.SelectedValue = "-1";
        //    txtAddressV.Text = "";
        //    txtAddress1V.Text = "";
        //    txtApproveDate.Text = "";
        //    txtApprovedBy.Text = "";
        //    txtBIN.Text = "";
        //    txtCityV.Text = "";
        //    txtCompanyNameV.Text = "";
        //    txtContactNameVendor.Text = "";
        //    txtDocument1.Text = "";
        //    txtDocument2.Text = "";
        //    txtDocument3.Text = "";
        //    txtDocument4.Text = "";
        //    txtDocumentID1.Text = "";
        //    txtDocumentID2.Text = "";
        //    txtDocumentID3.Text = "";
        //    txtDocumentID4.Text = "";
        //    txtEffectDate1.Text = "";
        //    txtEffectDate2.Text = "";
        //    txtEffectDate3.Text = "";
        //    txtEffectDate4.Text = "";
        //    txtEmailVendor.Text = "";
        //    txtEndDate1.Text = "";
        //    txtEndDate2.Text = "";
        //    txtEndDate3.Text = "";
        //    txtEndDate4.Text = "";
        //    txtFiledDate1.Text = "";
        //    txtFiledDate2.Text = "";
        //    txtFiledDate3.Text = "";
        //    txtFiledDate4.Text = "";
        //    txtNumberVendor.Text = "";
        //    txtPersonFiled1.Text = "";
        //    txtPersonFiled2.Text = "";
        //    txtPersonFiled3.Text = "";
        //    txtPersonFiled4.Text = "";
        //    txtRegionV.Text = "";
        //    txtReviewBy1.Text = "";
        //    txtReviewBy2.Text = "";
        //    txtReviewBy3.Text = "";
        //    txtReviewBy4.Text = "";
        //    txtReviewDate1.Text = "";
        //    txtReviewDate2.Text = "";
        //    txtReviewDate3.Text = "";
        //    txtReviewDate4.Text = "";
        //    txtTitle.Text = "";
        //    txtType.Text = "";
        //    txtValue1.Text = "";
        //    txtValue2.Text = "";
        //    txtValue3.Text = "";
        //    txtValue4.Text = "";
        //    txtZipV.Text = "";
        //    btnSaveVendor.Text = "Add";
        //}
        //private VendorProfile SetDataVendor(VendorProfile obj)
        //{
        //    try
        //    {
        //        if (Session["VendorId"] != null && Convert.ToInt32(Session["VendorId"]) > 0)
        //        {
        //            obj.Id = Convert.ToInt32(Session["VendorId"].ToString());
        //        }

        //        if ((!string.IsNullOrEmpty(txtType.Text.ToString())) && (txtType.Text.ToString() != string.Empty))
        //        {
        //            obj.TypeOfWork = txtType.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.TypeOfWork = "";
        //        }

        //        if ((!string.IsNullOrEmpty(txtTitle.Text.ToString())) && (txtTitle.Text.ToString() != string.Empty))
        //        {
        //            obj.Title = txtTitle.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Title = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtCompanyNameV.Text.ToString())) && (txtCompanyNameV.Text.ToString() != string.Empty))
        //        {
        //            obj.CompanyName = txtCompanyNameV.Text.ToString().ToLower().Trim();
        //        }
        //        else
        //        {
        //            obj.CompanyName = "";
        //        }

        //        if ((!string.IsNullOrEmpty(txtContactNameVendor.Text.ToString())) && (txtContactNameVendor.Text.ToString() != string.Empty))
        //        {
        //            obj.ContractName = txtContactNameVendor.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.ContractName = "";
        //        }

        //        if ((!string.IsNullOrEmpty(txtAddressV.Text.ToString())) && (txtAddressV.Text.ToString() != string.Empty))
        //        {
        //            obj.Address = txtAddressV.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Address = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtAddress1V.Text.ToString())) && (txtAddress1V.Text.ToString() != string.Empty))
        //        {
        //            obj.Address1 = txtAddress1V.Text.ToString().ToLower().Trim();
        //        }
        //        else
        //        {
        //            obj.Address1 = "";
        //        }

        //        if ((!string.IsNullOrEmpty(txtRegionV.Text.ToString())) && (txtRegionV.Text.ToString() != string.Empty))
        //        {
        //            obj.Region = txtRegionV.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Region = "";
        //        }
        //        if (ddlStateVendor.SelectedValue != "-1")
        //        {
        //            obj.State = ddlStateVendor.SelectedValue.Trim();
        //        }
        //        else
        //        {
        //            obj.State = "";
        //        }
        //        obj.City = txtCityV.Text.ToString().Trim();
        //        obj.Zip = txtZipV.Text.ToString().Trim();
        //        obj.Email = txtEmailVendor.Text.ToString().Trim();
        //        obj.Phone = txtNumberVendor.Text.ToString().Trim();
        //        obj.LicenseNo = txtBIN.Text.ToString().Trim();
        //        obj.ApprovedBy = txtApprovedBy.Text.ToString().Trim();
        //        obj.DateApplied = DateTime.Now;
        //        obj.DateApproved = txtApproveDate.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtApproveDate.Text.ToString().Trim()) : DateTime.Now;

        //        obj.Document1 = txtDocument1.Text.ToString().Trim();
        //        obj.Document2 = txtDocument2.Text.ToString().Trim();
        //        obj.Document3 = txtDocument3.Text.ToString().Trim();
        //        obj.Document4 = txtDocument4.Text.ToString().Trim();

        //        obj.Value1 = txtValue1.Text.ToString().Trim();
        //        obj.Value2 = txtValue2.Text.ToString().Trim();
        //        obj.Value3 = txtValue3.Text.ToString().Trim();
        //        obj.Value4 = txtValue4.Text.ToString().Trim();

        //        obj.EffectDate1 = txtEffectDate1.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEffectDate1.Text.ToString().Trim()) : DateTime.Now;
        //        obj.EffectDate2 = txtEffectDate2.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEffectDate2.Text.ToString().Trim()) : DateTime.Now;
        //        obj.EffectDate3 = txtEffectDate3.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEffectDate3.Text.ToString().Trim()) : DateTime.Now;
        //        obj.EffectDate4 = txtEffectDate4.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEffectDate4.Text.ToString().Trim()) : DateTime.Now;

        //        obj.EndDate1 = txtEndDate1.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEndDate1.Text.ToString().Trim()) : DateTime.Now;
        //        obj.EndDate2 = txtEndDate2.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEndDate2.Text.ToString().Trim()) : DateTime.Now;
        //        obj.EndDate3 = txtEndDate3.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEndDate3.Text.ToString().Trim()) : DateTime.Now;
        //        obj.EndDate4 = txtEndDate4.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtEndDate4.Text.ToString().Trim()) : DateTime.Now;

        //        obj.ReviewBy1 = txtReviewBy1.Text.ToString().Trim();
        //        obj.ReviewBy2 = txtReviewBy2.Text.ToString().Trim();
        //        obj.ReviewBy3 = txtReviewBy3.Text.ToString().Trim();
        //        obj.ReviewBy4 = txtReviewBy4.Text.ToString().Trim();

        //        obj.ReviewDate1 = txtReviewDate1.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtReviewDate1.Text.ToString().Trim()) : DateTime.Now;
        //        obj.ReviewDate2 = txtReviewDate2.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtReviewDate2.Text.ToString().Trim()) : DateTime.Now;
        //        obj.ReviewDate3 = txtReviewDate3.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtReviewDate3.Text.ToString().Trim()) : DateTime.Now;
        //        obj.ReviewDate4 = txtReviewDate4.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtReviewDate4.Text.ToString().Trim()) : DateTime.Now;

        //        obj.DocumentID1 = txtDocumentID1.Text.ToString().Trim();
        //        obj.DocumentID2 = txtDocumentID2.Text.ToString().Trim();
        //        obj.DocumentID3 = txtDocumentID3.Text.ToString().Trim();
        //        obj.DocumentID4 = txtDocumentID4.Text.ToString().Trim();

        //        obj.FiledDate1 = txtFiledDate1.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtFiledDate1.Text.ToString().Trim()) : DateTime.Now;
        //        obj.FiledDate2 = txtFiledDate2.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtFiledDate2.Text.ToString().Trim()) : DateTime.Now;
        //        obj.FiledDate3 = txtFiledDate3.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtFiledDate3.Text.ToString().Trim()) : DateTime.Now;
        //        obj.FiledDate4 = txtFiledDate4.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtFiledDate4.Text.ToString().Trim()) : DateTime.Now;

        //        obj.PersonFiled1 = txtPersonFiled1.Text.ToString().Trim();
        //        obj.PersonFiled2 = txtPersonFiled2.Text.ToString().Trim();
        //        obj.PersonFiled3 = txtPersonFiled3.Text.ToString().Trim();
        //        obj.PersonFiled4 = txtPersonFiled4.Text.ToString().Trim();

        //        if (chkSendEmail.Checked == true)
        //        {
        //            obj.IsSentEmail = true;
        //        }
        //        else
        //        {
        //            obj.IsSentEmail = false;
        //        }

        //        if (Session["VendorId"] != null)
        //        {
        //            obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
        //            obj.UpdatedDate = DateTime.Now;
        //        }
        //        else
        //        {
        //            obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
        //            obj.CreatedDate = DateTime.Now;
        //        }

        //        if (Session["PropertyLocationId"] != null)
        //        {
        //            if (Session["PropertyLocationId"].ToString() != string.Empty)
        //            {
        //                obj.PropertyLocationId = Session["PropertyLocationId"].ToString();
        //            }
        //            else
        //            {
        //                obj.PropertyLocationId = "";
        //            }
        //        }
        //        else
        //        {
        //            obj.PropertyLocationId = "";
        //        }

        //        if (Session["PropertyManagerId"] != null)
        //        {
        //            if (Session["PropertyManagerId"].ToString() != string.Empty)
        //            {
        //                obj.PropertyManagerId = Session["PropertyManagerId"].ToString();
        //            }
        //            else
        //            {
        //                obj.PropertyManagerId = "";
        //            }
        //        }
        //        else
        //        {
        //            obj.PropertyManagerId = "";
        //        }

        //        if (Session["OwnerId"] != null)
        //        {
        //            if (Session["OwnerId"].ToString() != string.Empty)
        //            {
        //                obj.OwnerId = Session["OwnerId"].ToString();
        //            }
        //            else
        //            {
        //                obj.OwnerId = "";
        //            }
        //        }
        //        else
        //        {
        //            obj.OwnerId = "";
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return obj;
        //}
        //private void FillControlsVendor(int nId)
        //{
        //    try
        //    {
        //        if (nId > 0)
        //        {
        //            VendorProfile obj = new VendorDA().GetbyID(nId);
        //            if (obj != null)
        //            {
        //                Session["VendorId"] = obj.Id;

        //                if (obj.TypeOfWork != null && obj.TypeOfWork.ToString() != string.Empty)
        //                {
        //                    txtType.Text = obj.TypeOfWork;
        //                }
        //                else
        //                {
        //                    txtType.Text = "";
        //                }
        //                if (obj.ContractName != null && obj.ContractName.ToString() != string.Empty)
        //                {
        //                    txtContactNameVendor.Text = obj.ContractName;
        //                }
        //                else
        //                {
        //                    txtContactNameVendor.Text = "";
        //                }
        //                if (obj.Title != null && obj.Title.ToString() != string.Empty)
        //                {
        //                    txtTitle.Text = obj.Title;
        //                }
        //                else
        //                {
        //                    txtTitle.Text = "";
        //                }
        //                if (obj.CompanyName != null && obj.CompanyName.ToString() != string.Empty)
        //                {
        //                    txtCompanyNameV.Text = obj.CompanyName;
        //                }
        //                else
        //                {
        //                    txtCompanyNameV.Text = "";
        //                }
        //                if (obj.Address != null && obj.Address.ToString() != string.Empty)
        //                {
        //                    txtAddressV.Text = obj.Address;
        //                }
        //                else
        //                {
        //                    txtAddressV.Text = "";
        //                }
        //                if (obj.Address1 != null && obj.Address1.ToString() != string.Empty)
        //                {
        //                    txtAddress1V.Text = obj.Address1;
        //                }
        //                else
        //                {
        //                    txtAddress1V.Text = "";
        //                }

        //                if (obj.Region != null && obj.Region.ToString() != string.Empty)
        //                {
        //                    txtRegionV.Text = obj.Region;
        //                }
        //                else
        //                {
        //                    txtRegionV.Text = "";
        //                }
        //                if (obj.State != null && obj.State.ToString() != string.Empty)
        //                {
        //                    ddlStateVendor.SelectedValue = obj.State;
        //                }
        //                else
        //                {
        //                    ddlStateVendor.SelectedValue = "";
        //                }
        //                if (obj.City != null && obj.City.ToString() != string.Empty)
        //                {
        //                    txtCityV.Text = obj.City;
        //                }
        //                else
        //                {
        //                    txtCityV.Text = "";
        //                }
        //                if (obj.Zip != null && obj.Zip.ToString() != string.Empty)
        //                {
        //                    txtZipV.Text = obj.Zip;
        //                }
        //                else
        //                {
        //                    txtZipV.Text = "";
        //                }
        //                if (obj.Email != null && obj.Email.ToString() != string.Empty)
        //                {
        //                    txtEmailVendor.Text = obj.Email;
        //                }
        //                else
        //                {
        //                    txtEmailVendor.Text = "";
        //                }
        //                if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
        //                {
        //                    txtNumberVendor.Text = obj.Phone;
        //                }
        //                else
        //                {
        //                    txtNumberVendor.Text = "";
        //                }
        //                if (obj.LicenseNo != null && obj.LicenseNo.ToString() != string.Empty)
        //                {
        //                    txtBIN.Text = obj.LicenseNo;
        //                }
        //                else
        //                {
        //                    txtBIN.Text = "";
        //                }
        //                if (obj.ApprovedBy != null && obj.ApprovedBy.ToString() != string.Empty)
        //                {
        //                    txtApprovedBy.Text = obj.ApprovedBy;
        //                }
        //                else
        //                {
        //                    txtApprovedBy.Text = "";
        //                }
        //                if (obj.DateApproved != null && obj.DateApproved.ToString() != string.Empty)
        //                {
        //                    txtApproveDate.Text = Convert.ToDateTime(obj.DateApproved).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtApproveDate.Text = "";
        //                }

        //                if (obj.Document1 != null && obj.Document1.ToString() != string.Empty)
        //                {
        //                    txtDocument1.Text = obj.Document1;
        //                }
        //                else
        //                {
        //                    txtDocument1.Text = "";
        //                }
        //                if (obj.Value1 != null && obj.Value1.ToString() != string.Empty)
        //                {
        //                    txtValue1.Text = obj.Value1;
        //                }
        //                else
        //                {
        //                    txtValue1.Text = "";
        //                }
        //                if (obj.EffectDate1 != null && obj.EffectDate1.ToString() != string.Empty)
        //                {
        //                    txtEffectDate1.Text = Convert.ToDateTime(obj.EffectDate1).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtEffectDate1.Text = "";
        //                }
        //                if (obj.EndDate1 != null && obj.EndDate1.ToString() != string.Empty)
        //                {
        //                    txtEndDate1.Text = Convert.ToDateTime(obj.EndDate1).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtEndDate1.Text = "";
        //                }

        //                if (obj.ReviewBy1 != null && obj.ReviewBy1.ToString() != string.Empty)
        //                {
        //                    txtReviewBy1.Text = obj.ReviewBy1;
        //                }
        //                else
        //                {
        //                    txtReviewBy1.Text = "";
        //                }
        //                if (obj.ReviewDate1 != null && obj.ReviewDate1.ToString() != string.Empty)
        //                {
        //                    txtReviewDate1.Text = Convert.ToDateTime(obj.ReviewDate1).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtReviewDate1.Text = "";
        //                }
        //                if (obj.DocumentID1 != null && obj.DocumentID1.ToString() != string.Empty)
        //                {
        //                    txtDocumentID1.Text = obj.DocumentID1;
        //                }
        //                else
        //                {
        //                    txtDocumentID1.Text = "";
        //                }
        //                if (obj.FiledDate1 != null && obj.FiledDate1.ToString() != string.Empty)
        //                {
        //                    txtFiledDate1.Text = Convert.ToDateTime(obj.FiledDate1).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtFiledDate1.Text = "";
        //                }
        //                if (obj.PersonFiled1 != null && obj.PersonFiled1.ToString() != string.Empty)
        //                {
        //                    txtPersonFiled1.Text = obj.PersonFiled1;
        //                }
        //                else
        //                {
        //                    txtPersonFiled1.Text = "";
        //                }

        //                if (obj.Document2 != null && obj.Document2.ToString() != string.Empty)
        //                {
        //                    txtDocument2.Text = obj.Document2;
        //                }
        //                else
        //                {
        //                    txtDocument2.Text = "";
        //                }
        //                if (obj.Value2 != null && obj.Value2.ToString() != string.Empty)
        //                {
        //                    txtValue2.Text = obj.Value2;
        //                }
        //                else
        //                {
        //                    txtValue2.Text = "";
        //                }
        //                if (obj.EffectDate2 != null && obj.EffectDate2.ToString() != string.Empty)
        //                {
        //                    txtEffectDate2.Text = Convert.ToDateTime(obj.EffectDate2).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtEffectDate2.Text = "";
        //                }
        //                if (obj.EndDate2 != null && obj.EndDate2.ToString() != string.Empty)
        //                {
        //                    txtEndDate2.Text = Convert.ToDateTime(obj.EndDate2).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtEndDate2.Text = "";
        //                }

        //                if (obj.ReviewBy2 != null && obj.ReviewBy2.ToString() != string.Empty)
        //                {
        //                    txtReviewBy2.Text = obj.ReviewBy2;
        //                }
        //                else
        //                {
        //                    txtReviewBy2.Text = "";
        //                }
        //                if (obj.ReviewDate2 != null && obj.ReviewDate2.ToString() != string.Empty)
        //                {
        //                    txtReviewDate2.Text = Convert.ToDateTime(obj.ReviewDate2).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtReviewDate2.Text = "";
        //                }
        //                if (obj.DocumentID2 != null && obj.DocumentID2.ToString() != string.Empty)
        //                {
        //                    txtDocumentID2.Text = obj.DocumentID2;
        //                }
        //                else
        //                {
        //                    txtDocumentID2.Text = "";
        //                }
        //                if (obj.FiledDate2 != null && obj.FiledDate2.ToString() != string.Empty)
        //                {
        //                    txtFiledDate2.Text = Convert.ToDateTime(obj.FiledDate2).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtFiledDate2.Text = "";
        //                }
        //                if (obj.PersonFiled2 != null && obj.PersonFiled2.ToString() != string.Empty)
        //                {
        //                    txtPersonFiled2.Text = obj.PersonFiled2;
        //                }
        //                else
        //                {
        //                    txtPersonFiled2.Text = "";
        //                }

        //                if (obj.Document3 != null && obj.Document3.ToString() != string.Empty)
        //                {
        //                    txtDocument3.Text = obj.Document3;
        //                }
        //                else
        //                {
        //                    txtDocument3.Text = "";
        //                }
        //                if (obj.Value3 != null && obj.Value3.ToString() != string.Empty)
        //                {
        //                    txtValue3.Text = obj.Value3;
        //                }
        //                else
        //                {
        //                    txtValue3.Text = "";
        //                }
        //                if (obj.EffectDate3 != null && obj.EffectDate3.ToString() != string.Empty)
        //                {
        //                    txtEffectDate3.Text = Convert.ToDateTime(obj.EffectDate3).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtEffectDate3.Text = "";
        //                }
        //                if (obj.EndDate3 != null && obj.EndDate3.ToString() != string.Empty)
        //                {
        //                    txtEndDate3.Text = Convert.ToDateTime(obj.EndDate3).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtEndDate3.Text = "";
        //                }

        //                if (obj.ReviewBy3 != null && obj.ReviewBy3.ToString() != string.Empty)
        //                {
        //                    txtReviewBy3.Text = obj.ReviewBy3;
        //                }
        //                else
        //                {
        //                    txtReviewBy3.Text = "";
        //                }
        //                if (obj.ReviewDate3 != null && obj.ReviewDate3.ToString() != string.Empty)
        //                {
        //                    txtReviewDate3.Text = Convert.ToDateTime(obj.ReviewDate3).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtReviewDate3.Text = "";
        //                }
        //                if (obj.DocumentID3 != null && obj.DocumentID3.ToString() != string.Empty)
        //                {
        //                    txtDocumentID3.Text = obj.DocumentID3;
        //                }
        //                else
        //                {
        //                    txtDocumentID3.Text = "";
        //                }
        //                if (obj.FiledDate3 != null && obj.FiledDate3.ToString() != string.Empty)
        //                {
        //                    txtFiledDate3.Text = Convert.ToDateTime(obj.FiledDate3).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtFiledDate3.Text = "";
        //                }
        //                if (obj.PersonFiled3 != null && obj.PersonFiled3.ToString() != string.Empty)
        //                {
        //                    txtPersonFiled3.Text = obj.PersonFiled3;
        //                }
        //                else
        //                {
        //                    txtPersonFiled3.Text = "";
        //                }

        //                if (obj.Document4 != null && obj.Document4.ToString() != string.Empty)
        //                {
        //                    txtDocument4.Text = obj.Document4;
        //                }
        //                else
        //                {
        //                    txtDocument4.Text = "";
        //                }
        //                if (obj.Value4 != null && obj.Value4.ToString() != string.Empty)
        //                {
        //                    txtValue4.Text = obj.Value4;
        //                }
        //                else
        //                {
        //                    txtValue4.Text = "";
        //                }
        //                if (obj.EffectDate4 != null && obj.EffectDate4.ToString() != string.Empty)
        //                {
        //                    txtEffectDate4.Text = Convert.ToDateTime(obj.EffectDate4).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtEffectDate4.Text = "";
        //                }
        //                if (obj.EndDate4 != null && obj.EndDate4.ToString() != string.Empty)
        //                {
        //                    txtEndDate4.Text = Convert.ToDateTime(obj.EndDate4).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtEndDate4.Text = "";
        //                }

        //                if (obj.ReviewBy4 != null && obj.ReviewBy4.ToString() != string.Empty)
        //                {
        //                    txtReviewBy4.Text = obj.ReviewBy4;
        //                }
        //                else
        //                {
        //                    txtReviewBy4.Text = "";
        //                }
        //                if (obj.ReviewDate4 != null && obj.ReviewDate4.ToString() != string.Empty)
        //                {
        //                    txtReviewDate4.Text = Convert.ToDateTime(obj.ReviewDate4).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtReviewDate4.Text = "";
        //                }
        //                if (obj.DocumentID4 != null && obj.DocumentID4.ToString() != string.Empty)
        //                {
        //                    txtDocumentID4.Text = obj.DocumentID4;
        //                }
        //                else
        //                {
        //                    txtDocumentID4.Text = "";
        //                }
        //                if (obj.FiledDate4 != null && obj.FiledDate4.ToString() != string.Empty)
        //                {
        //                    txtFiledDate4.Text = Convert.ToDateTime(obj.FiledDate4).ToString("MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    txtFiledDate4.Text = "";
        //                }
        //                if (obj.PersonFiled4 != null && obj.PersonFiled4.ToString() != string.Empty)
        //                {
        //                    txtPersonFiled4.Text = obj.PersonFiled4;
        //                }
        //                else
        //                {
        //                    txtPersonFiled4.Text = "";
        //                }

        //                btnSaveVendor.Text = "Update";

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
        //private void FillVendor()
        //{
        //    try
        //    {
        //        List<VendorProfile> obj = null;
        //        if (Session["OwnerId"] != null)
        //        {
        //            obj = new VendorDA().GetByOwner(Session["OwnerId"].ToString());
        //        }

        //        gvVendorList.DataSource = obj;
        //        gvVendorList.DataBind();

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}


        //#endregion

        //#region Events CAM    

        //protected void btnSubmitCAM_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CAMExpense obj = new CAMExpense();
        //        obj = SetDataCAM(obj);

        //        if (Session["CAMId"] == null || Session["CAMId"] == "0")
        //        {
        //            if (new CAMExpenseDA().Insert(obj))
        //            {
        //                Session["CAMId"] = null;
        //                ClearControlsCAM();
        //                FillCAMs();
        //                Utility.DisplayMsg("CAM Expense saved successfully!", this);
        //            }
        //            else
        //            {
        //                Utility.DisplayMsg("CAM Expense not saved!", this);
        //            }
        //        }
        //        else
        //        {
        //            if (new CAMExpenseDA().Update(obj))
        //            {
        //                Session["CAMId"] = null;
        //                ClearControlsCAM();
        //                FillCAMs();
        //                Utility.DisplayMsg("CAM Expense updated successfully!", this);
        //            }
        //            else
        //            {
        //                Utility.DisplayMsg("CAM Expense not updated!", this);
        //            }
        //        }
        //    }
        //    catch (Exception ex1)
        //    {

        //    }
        //}
        //protected void btnCloseCAM_Click(object sender, EventArgs e)
        //{
        //    ClearControlsCAM();
        //}
        //protected void btnsearchCAM_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Session["CAMSearch"] = null;
        //        if (txtFromDate.Text.ToString().Trim() != string.Empty && txtToDate.Text.ToString().Trim() != string.Empty)
        //        {
        //            List<CAMExpense> obj = new CAMExpenseDA().GetBySearch(Convert.ToDateTime(txtFromDate.Text.ToString().Trim()), Convert.ToDateTime(txtFromDate.Text.ToString().Trim()));
        //            gvCAMList.DataSource = obj;
        //            gvCAMList.DataBind();
        //        }
        //    }
        //    catch (Exception ex) { }
        //}
        //protected void btnDeleteCAM_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //    Label hdId = (Label)gvCAMList.Rows[row.RowIndex].FindControl("lblCAMId");

        //    if (!String.IsNullOrEmpty(hdId.Text))
        //    {
        //        if (new CAMExpenseDA().DeleteByID(Convert.ToInt32(hdId.Text)))
        //        {
        //            FillCAMs();
        //        }
        //    }
        //}
        //protected void btnEditCAM_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //    Label hdId = (Label)gvCAMList.Rows[row.RowIndex].FindControl("lblCAMId");

        //    if (!String.IsNullOrEmpty(hdId.Text))
        //    {
        //        FillControlsCAM(Convert.ToInt32(hdId.Text));
        //    }
        //}
        //protected void gvCAMList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvCAMList.PageIndex = e.NewPageIndex;
        //    FillCAMs();
        //}
        //protected void gvCAMList_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    FillCAMs();
        //}
        //#endregion

        //#region Method CAM

        //private void ClearControlsCAM()
        //{
        //    txtName.Text = "";
        //    txtExpenseType.Text = "";
        //    txtNumberCAM.Text = "";
        //    txtCheckNo.Text = "";
        //    btnSaveCAM.Text = "Add Expense Contact";
        //    lblHeadline.InnerText = "Add Expense Contact";
        //}
        //private void FillddlControlsCAM()
        //{
        //    try
        //    {
        //        ddlLedgerType.Items.Clear();
        //        ddlLedgerType.AppendDataBoundItems = true;
        //        ddlLedgerType.Items.Add(new ListItem("Select Ledger Type", "-1"));
        //        ddlLedgerType.SelectedValue = "-1";
        //        ddlLedgerType.DataSource = new ChildDA().GetChildByParentID(Convert.ToInt32(EnumGlobalData.Ledger));
        //        ddlLedgerType.DataTextField = "Description";
        //        ddlLedgerType.DataValueField = "Code";
        //        ddlLedgerType.DataBind();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    try
        //    {
        //        ddlPaidBy.Items.Clear();
        //        ddlPaidBy.AppendDataBoundItems = true;
        //        ddlPaidBy.Items.Add(new ListItem("Select Payment Type", "-1"));
        //        ddlPaidBy.SelectedValue = "-1";
        //        ddlPaidBy.DataSource = new ChildDA().GetChildByParentID(Convert.ToInt32(EnumGlobalData.Payment));
        //        ddlPaidBy.DataTextField = "Description";
        //        ddlPaidBy.DataValueField = "Code";
        //        ddlPaidBy.DataBind();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //private CAMExpense SetDataCAM(CAMExpense obj)
        //{
        //    try
        //    {
        //        if (Session["CAMId"] != null && Convert.ToInt32(Session["CAMId"]) > 0)
        //        {
        //            obj.Id = Convert.ToInt32(Session["CAMId"].ToString());
        //        }

        //        if ((!string.IsNullOrEmpty(txtName.Text.ToString())) && (txtName.Text.ToString() != string.Empty))
        //        {
        //            obj.Name = txtName.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.Name = "";
        //        }

        //        if ((!string.IsNullOrEmpty(txtExpenseType.Text.ToString())) && (txtExpenseType.Text.ToString() != string.Empty))
        //        {
        //            obj.TypeOfExpense = txtExpenseType.Text.ToString();
        //        }
        //        else
        //        {
        //            obj.TypeOfExpense = "";
        //        }

        //        if (ddlLedgerType.SelectedValue != "-1")
        //        {
        //            obj.LedgerAccount = ddlLedgerType.SelectedValue.ToString();
        //        }
        //        else
        //        {
        //            obj.LedgerAccount = "";
        //        }
        //        if (ddlPaidBy.SelectedValue != "-1")
        //        {
        //            obj.PaidBy = ddlPaidBy.SelectedValue.ToString();
        //        }
        //        else
        //        {
        //            obj.PaidBy = "";
        //        }
        //        if ((!string.IsNullOrEmpty(txtNumberCAM.Text.ToString())) && (txtNumberCAM.Text.ToString() != string.Empty))
        //        {
        //            obj.Amount = Convert.ToDecimal(txtNumberCAM.Text.ToString());
        //        }
        //        else
        //        {
        //            obj.Amount = 0;
        //        }
        //        if ((!string.IsNullOrEmpty(txtCheckNo.Text.ToString())) && (txtCheckNo.Text.ToString() != string.Empty))
        //        {
        //            obj.CheckNumber = txtCheckNo.Text.ToString().ToLower().Trim();
        //        }
        //        else
        //        {
        //            obj.CheckNumber = "";
        //        }
        //        if (rdoCAM.Items[0].Selected == true)
        //        {
        //            obj.IsCAM = true;
        //        }
        //        else
        //        {
        //            obj.IsCAM = false;
        //        }

        //        obj.IsDelete = false;

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

        //        if (Session["CAMId"] == null || obj.PaidDate == null)
        //        {
        //            obj.PaidDate = DateTime.Now;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return obj;
        //}
        //private void FillControlsCAM(int nId)
        //{
        //    try
        //    {
        //        if (nId > 0)
        //        {
        //            CAMExpense obj = new CAMExpenseDA().GetbyID(nId);
        //            if (obj != null)
        //            {
        //                Session["CAMId"] = obj.Id;
        //                if (obj.Name != null && obj.Name.ToString() != string.Empty)
        //                {
        //                    txtName.Text = obj.Name;
        //                }
        //                else
        //                {
        //                    txtName.Text = "";
        //                }
        //                if (obj.TypeOfExpense != null && obj.TypeOfExpense.ToString() != string.Empty)
        //                {
        //                    txtExpenseType.Text = obj.TypeOfExpense;
        //                }
        //                else
        //                {
        //                    txtExpenseType.Text = "";
        //                }

        //                if (obj.LedgerAccount != null && obj.LedgerAccount.ToString() != string.Empty)
        //                {
        //                    ddlLedgerType.SelectedValue = obj.LedgerAccount.ToString();
        //                }
        //                else
        //                {
        //                    ddlLedgerType.SelectedValue = "-1";
        //                }

        //                if (obj.PaidBy != null && obj.PaidBy.ToString() != string.Empty)
        //                {
        //                    ddlPaidBy.SelectedValue = obj.PaidBy.ToString();
        //                }
        //                else
        //                {
        //                    ddlPaidBy.SelectedValue = "-1";
        //                }
        //                if (obj.Amount != null && obj.Amount.ToString() != string.Empty)
        //                {
        //                    txtNumberCAM.Text = Convert.ToDecimal(obj.Amount).ToString("#.00");
        //                }
        //                if (obj.CheckNumber != null && obj.CheckNumber.ToString() != string.Empty)
        //                {
        //                    txtCheckNo.Text = obj.CheckNumber;
        //                }
        //                else
        //                {
        //                    txtCheckNo.Text = "";
        //                }

        //                if (obj.IsCAM != null)
        //                {
        //                    if (Convert.ToInt32(obj.IsCAM) == 1)
        //                    {
        //                        rdoCAM.Items[0].Selected = true;
        //                    }
        //                    else
        //                    {
        //                        rdoCAM.Items[1].Selected = true;
        //                    }
        //                }

        //                lblPropertyLocation.InnerText = obj.PropertyLocationId;
        //                btnSave.Text = "Update Expense Contact";

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
        //private void FillCAMs()
        //{
        //    try
        //    {
        //        List<CAMExpense> obj = null;
        //        if (Session["PropertyLocationId"] != null)
        //        {
        //            obj = new CAMExpenseDA().GetByPropertyLocation(Session["PropertyLocationId"].ToString());
        //        }
        //        else if (Session["OwnerId"] != null)
        //        {
        //            obj = new CAMExpenseDA().GetByOwner(Session["OwnerId"].ToString());
        //        }

        //        gvCAMList.DataSource = obj;
        //        gvCAMList.DataBind();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //#endregion

    }
}