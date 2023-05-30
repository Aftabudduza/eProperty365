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
    public partial class AddVendor : System.Web.UI.Page
    {
        public string sUrl = string.Empty;

        #region Events     
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropdowns();
                FillVendor();
                Session["vendorUrl"] = null;
                try
                {
                    if (Request.QueryString["R"] != null)
                    {
                        sUrl = Request.QueryString["R"].ToString();
                        Session["vendorUrl"] = sUrl.ToLower();
                    }
                }
                catch
                {
                    sUrl = "";
                }

                if ((Session["UserObject"] != null))
                {
                    Session["VendorId"] = null;
                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["VendorId"].ToString());
                    }
                    catch
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                        lblHeadline.InnerText = "Edit Vendor";
                        Session["VendorId"] = CId;
                        FillControls(Convert.ToInt32(Session["VendorId"].ToString()));
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
                            //ddlPropertyManager.DataSource = new PropertyManagerProfileDA().GetByOwnerId(ddlOwner.SelectedValue);
                            //ddlPropertyManager.DataTextField = "FirstName";
                            //ddlPropertyManager.DataValueField = "Serial";
                            ddlPropertyManager.DataBind();
                            ddlPropertyManager.SelectedValue = "-1";
                        }
                        catch (Exception ex)
                        {
                        }

                    }

                 }
            }
        }        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                VendorProfile obj = new VendorProfile();
                obj = SetData(obj);

                if (Session["vendorUrl"] != null)
                {
                    sUrl = Session["vendorUrl"].ToString();
                }

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
                        ClearControls();
                        FillVendor();
                       // Utility.DisplayMsg("Vendor saved successfully!", this);
                        //Utility.DisplayMsgAndRedirect("Vendor saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");

                        if (sUrl != string.Empty)
                        {
                            if (sUrl.Trim() == "owner")
                            {
                                Utility.DisplayMsgAndRedirect("Vendor saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                            }
                            else if (sUrl.Trim() == "home")
                            {
                                Utility.DisplayMsgAndRedirect("Vendor saved successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                            }
                            else if (sUrl.Trim() == "location")
                            {
                                Utility.DisplayMsgAndRedirect("Vendor saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                            }
                            else
                            {                               
                                Utility.DisplayMsg("Vendor saved successfully!", this);
                            }
                        }
                        else
                        {                           
                            Utility.DisplayMsg("Vendor saved successfully!", this);
                        }

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
                      //  Session["VendorId"] = null;
                        ClearControls();
                        FillVendor();
                        //Utility.DisplayMsg("Vendor updated successfully!", this);
                        //  Utility.DisplayMsgAndRedirect("Vendor updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                        if (sUrl != string.Empty)
                        {
                            if (sUrl.Trim() == "owner")
                            {
                                Utility.DisplayMsgAndRedirect("Vendor updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                            }
                            else if (sUrl.Trim() == "home")
                            {
                                Utility.DisplayMsgAndRedirect("Vendor updated successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                            }
                            else if (sUrl.Trim() == "location")
                            {
                                Utility.DisplayMsgAndRedirect("Vendor updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                            }
                            else
                            {
                                Utility.DisplayMsg("Vendor updated successfully!", this);
                            }
                        }
                        else
                        {
                            Utility.DisplayMsg("Vendor updated successfully!", this);
                        }
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
                string sOwnerId = "";
                string sPropertyManagerId = "";
                string sLocationId = "";

                sOwnerId = ddlOwner.SelectedValue != String.Empty && ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";
                sPropertyManagerId = ddlPropertyManager.SelectedValue != String.Empty && ddlPropertyManager.SelectedValue != "-1" ? ddlPropertyManager.SelectedValue : "";
                sLocationId = ddlLocation.SelectedValue != String.Empty && ddlLocation.SelectedValue != "-1" ? ddlLocation.SelectedValue : "";

                List<VendorProfile> obj = new VendorDA().GetBySearch(sOwnerId, sPropertyManagerId, sLocationId);
                gvContactList.DataSource = obj;
                gvContactList.DataBind();
            }
            catch (Exception ex)
            { }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvContactList.Rows[row.RowIndex].FindControl("lblId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new VendorDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillVendor();
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
            FillVendor();
        }
        protected void gvContactList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillVendor();
        }
        protected void ddlOwner_SelectedIndexChanged(object sender, EventArgs e)
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
                //ddlPropertyManager.DataSource = new PropertyManagerProfileDA().GetByOwnerId(ddlOwner.SelectedValue);
                //ddlPropertyManager.DataTextField = "FirstName";
                //ddlPropertyManager.DataValueField = "Serial";
                ddlPropertyManager.DataBind();
                ddlPropertyManager.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
            }
            try
            {
                ddlLocation.Items.Clear();
                ddlLocation.AppendDataBoundItems = true;
                ddlLocation.Items.Add(new ListItem("Select Location", "-1"));
                ddlLocation.DataSource = new LocationDA().GetByOwner(ddlOwner.SelectedValue);
                ddlLocation.DataTextField = "LocationName";
                ddlLocation.DataValueField = "Serial";
                ddlLocation.DataBind();
                ddlLocation.SelectedValue = "-1";
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlPropertyManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlLocation.Items.Clear();
                ddlLocation.AppendDataBoundItems = true;
                ddlLocation.Items.Add(new ListItem("Select Location", "-1"));
                ddlLocation.DataSource = new LocationDA().GetByPropertyManager(ddlPropertyManager.SelectedValue);
                ddlLocation.DataTextField = "LocationName";
                ddlLocation.DataValueField = "Serial";
                ddlLocation.DataBind();
                ddlLocation.SelectedValue = "-1";
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearControls();
            if (Session["vendorUrl"] != null)
            {
                sUrl = Session["vendorUrl"].ToString();
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
        private void FillDropdowns()
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
                        string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                        ddlOwner.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
                //ddlOwner.DataSource = new OwnerProfileDA().GetAllOwnersInfo();
                //ddlOwner.DataTextField = "FirstName";
                //ddlOwner.DataValueField = "Serial";
                ddlOwner.DataBind();
                ddlOwner.SelectedValue = "-1";
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
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtApproveDate.Text = "";
            txtApprovedBy.Text = "";
            txtBIN.Text = "";
            txtCity.Text = "";
            txtCompanyName.Text = "";
            txtContactName.Text = "";
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
            txtEmail.Text = "";
            txtEndDate1.Text = "";
            txtEndDate2.Text = "";
            txtEndDate3.Text = "";
            txtEndDate4.Text = "";
            txtFiledDate1.Text = "";
            txtFiledDate2.Text = "";
            txtFiledDate3.Text = "";
            txtFiledDate4.Text = "";
            txtNumber.Text = "";
            txtPersonFiled1.Text = "";
            txtPersonFiled2.Text = "";
            txtPersonFiled3.Text = "";
            txtPersonFiled4.Text = "";
            txtRegion.Text = "";
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
            //txtUnit.Text = "";
            //txtUnitID.Text = "";
            txtValue1.Text = "";
            txtValue2.Text = "";
            txtValue3.Text = "";
            txtValue4.Text = "";
            txtZip.Text = "";           
            btnSave.Text = "Add";
            lblHeadline.InnerText = "Add Vendor";
        }       
        private VendorProfile SetData(VendorProfile obj)
        {
            try
            {
                obj = new VendorProfile();

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
                if ((!string.IsNullOrEmpty(txtCompanyName.Text.ToString())) && (txtCompanyName.Text.ToString() != string.Empty))
                {
                    obj.CompanyName = txtCompanyName.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.CompanyName = "";
                }

                if ((!string.IsNullOrEmpty(txtContactName.Text.ToString())) && (txtContactName.Text.ToString() != string.Empty))
                {
                    obj.ContractName = txtContactName.Text.ToString();
                }
                else
                {
                    obj.ContractName = "";
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
                    obj.Address1 = txtAddress1.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Address1 = "";
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
                obj.City = txtCity.Text.ToString().Trim();
                obj.Zip = txtZip.Text.ToString().Trim();
                obj.Email = txtEmail.Text.ToString().Trim();
                obj.Phone = txtNumber.Text.ToString().Trim();
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

                if (ddlLocation.SelectedValue != "-1")
                {
                    obj.PropertyLocationId = ddlLocation.SelectedValue != "-1" ? ddlLocation.SelectedValue : "";
                }
                else
                {
                    if (Session["PropertyLocationId"] != null)
                    {
                        if (Session["PropertyLocationId"].ToString() != string.Empty)
                        {
                            obj.PropertyLocationId = Session["PropertyLocationId"].ToString();
                        }
                        else
                        {
                            obj.PropertyLocationId = ddlLocation.SelectedValue != "-1" ? ddlLocation.SelectedValue : "";
                        }
                    }
                }

                if (ddlPropertyManager.SelectedValue != "-1")
                {
                    obj.PropertyManagerId = ddlPropertyManager.SelectedValue != "-1" ? ddlPropertyManager.SelectedValue : "";
                }
                else
                {
                    if (Session["PropertyManagerId"] != null)
                    {
                        if (Session["PropertyManagerId"].ToString() != string.Empty)
                        {
                            obj.PropertyManagerId = Session["PropertyManagerId"].ToString();
                        }
                        else
                        {
                            obj.PropertyManagerId = ddlPropertyManager.SelectedValue != "-1" ? ddlPropertyManager.SelectedValue : "";
                        }
                    }
                }

                if (ddlOwner.SelectedValue != "-1")
                {
                    obj.OwnerId = ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";
                }
                else
                {
                    if (Session["OwnerId"] != null)
                    {
                        if (Session["OwnerId"].ToString() != string.Empty)
                        {
                            obj.OwnerId = Session["OwnerId"].ToString();
                        }
                        else
                        {
                            obj.OwnerId = ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";
                        }
                    }
                }

                //obj.UnitID = txtUnitID.Text.ToString().Trim();
                //obj.UnitName = txtUnit.Text.ToString().Trim();

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
                            txtContactName.Text = obj.ContractName;
                        }
                        else
                        {
                            txtContactName.Text = "";
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
                            txtAddress1.Text = obj.Address1;
                        }
                        else
                        {
                            txtAddress1.Text = "";
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
                            ddlState.SelectedValue = "";
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
                        if (obj.Email != null && obj.Email.ToString() != string.Empty)
                        {
                            txtEmail.Text = obj.Email;
                        }
                        else
                        {
                            txtEmail.Text = "";
                        }
                        if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
                        {
                            txtNumber.Text = obj.Phone;
                        }
                        else
                        {
                            txtNumber.Text = "";
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

                        btnSave.Text = "Update";

                    }
                }
            }
            catch(Exception e)
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

                gvContactList.DataSource = obj;
                gvContactList.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion     
    }
}