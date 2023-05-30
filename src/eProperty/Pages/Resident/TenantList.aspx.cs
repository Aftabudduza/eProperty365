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
namespace eProperty.Pages.Resident
{
    public partial class TenantList : System.Web.UI.Page
    {
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

                        ddlLocation.Items.Clear();
                        ddlLocation.AppendDataBoundItems = true;
                        ddlLocation.Items.Add(new ListItem("Select Location", "-1"));
                        ddlLocation.DataSource = new LocationDA().GetByOwner(ddlOwner.SelectedValue);
                        ddlLocation.DataTextField = "LocationName";
                        ddlLocation.DataValueField = "Serial";
                        ddlLocation.DataBind();
                        ddlLocation.SelectedValue = "-1";

                        ddlUnit.Items.Clear();
                        ddlUnit.AppendDataBoundItems = true;
                        ddlUnit.Items.Add(new ListItem("Select Unit", "-1"));
                        List<ResidentialUnit> objResidentialUnits = new ResidentialUnitDa().GetByOwner(ddlOwner.SelectedValue);
                        if (objResidentialUnits != null && objResidentialUnits.Count > 0)
                        {
                            foreach (ResidentialUnit obj in objResidentialUnits)
                            {
                                string sName = obj.UnitName.ToString() + " (" + obj.Serial.ToString() + ")";
                                ddlUnit.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                            }
                        }
                        //ddlUnit.DataSource = new ResidentialUnitDa().GetByOwner(ddlOwner.SelectedValue);
                        //ddlUnit.DataTextField = "Serial";
                        //ddlUnit.DataValueField = "Serial";
                        ddlUnit.DataBind();
                        ddlUnit.SelectedValue = "-1";

                        FillTenantList(ddlOwner.SelectedValue, "", "", "", "");

                    }


                }
            }
        }

        #region Events Support
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sOwnerId = "";
                string sPropertyManagerId = "";
                string sLocationId = "";
                string sUnitId = "";
                string sTenantId = "";

                sOwnerId = ddlOwner.SelectedValue != String.Empty && ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";
                sPropertyManagerId = ddlPropertyManager.SelectedValue != String.Empty && ddlPropertyManager.SelectedValue != "-1" ? ddlPropertyManager.SelectedValue : "";
                sLocationId = ddlLocation.SelectedValue != String.Empty && ddlLocation.SelectedValue != "-1" ? ddlLocation.SelectedValue : "";
                sUnitId = ddlUnit.SelectedValue != String.Empty && ddlUnit.SelectedValue != "-1" ? ddlUnit.SelectedValue : "";
                sTenantId = txtTenantName.Text.ToString().Trim() != "" ? txtTenantName.Text.ToString().Trim() : "";

                FillTenantList(sOwnerId, sPropertyManagerId, sLocationId, sUnitId, sTenantId);
            }
            catch (Exception ex)
            { }
        }
        protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocation.PageIndex = e.NewPageIndex;

            string sOwnerId = "";
            string sPropertyManagerId = "";
            string sLocationId = "";
            string sUnitId = "";
            string sTenantId = "";

            sOwnerId = ddlOwner.SelectedValue != String.Empty && ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";
            sPropertyManagerId = ddlPropertyManager.SelectedValue != String.Empty && ddlPropertyManager.SelectedValue != "-1" ? ddlPropertyManager.SelectedValue : "";
            sLocationId = ddlLocation.SelectedValue != String.Empty && ddlLocation.SelectedValue != "-1" ? ddlLocation.SelectedValue : "";
            sUnitId = ddlUnit.SelectedValue != String.Empty && ddlUnit.SelectedValue != "-1" ? ddlUnit.SelectedValue : "";
            sTenantId = txtTenantName.Text.ToString().Trim() != "" ? txtTenantName.Text.ToString().Trim() : "";

            FillTenantList(sOwnerId, sPropertyManagerId, sLocationId, sUnitId, sTenantId);

        }
        protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sOwnerId = "";
            string sPropertyManagerId = "";
            string sLocationId = "";
            string sUnitId = "";
            string sTenantId = "";

            sOwnerId = ddlOwner.SelectedValue != String.Empty && ddlOwner.SelectedValue != "-1" ? ddlOwner.SelectedValue : "";
            sPropertyManagerId = ddlPropertyManager.SelectedValue != String.Empty && ddlPropertyManager.SelectedValue != "-1" ? ddlPropertyManager.SelectedValue : "";
            sLocationId = ddlLocation.SelectedValue != String.Empty && ddlLocation.SelectedValue != "-1" ? ddlLocation.SelectedValue : "";
            sUnitId = ddlUnit.SelectedValue != String.Empty && ddlUnit.SelectedValue != "-1" ? ddlUnit.SelectedValue : "";
            sTenantId = txtTenantName.Text.ToString().Trim() != "" ? txtTenantName.Text.ToString().Trim() : "";

            FillTenantList(sOwnerId, sPropertyManagerId, sLocationId, sUnitId, sTenantId);
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
            try
            {
                ddlUnit.Items.Clear();
                ddlUnit.AppendDataBoundItems = true;
                ddlUnit.Items.Add(new ListItem("Select Unit", "-1"));
                List<ResidentialUnit> objResidentialUnits = new ResidentialUnitDa().GetByOwner(ddlOwner.SelectedValue);
                if (objResidentialUnits != null && objResidentialUnits.Count > 0)
                {
                    foreach (ResidentialUnit obj in objResidentialUnits)
                    {
                        string sName = obj.UnitName.ToString() + " (" + obj.Serial.ToString() + ")";
                        ddlUnit.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
                //ddlUnit.DataSource = new ResidentialUnitDa().GetByOwner(ddlOwner.SelectedValue);
                //ddlUnit.DataTextField = "Serial";
                //ddlUnit.DataValueField = "Serial";
                ddlUnit.DataBind();
                ddlUnit.SelectedValue = "-1";
            }
            catch (Exception)
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
            try
            {
                ddlUnit.Items.Clear();
                ddlUnit.AppendDataBoundItems = true;
                ddlUnit.Items.Add(new ListItem("Select Unit", "-1"));
                List<ResidentialUnit> objResidentialUnits = new ResidentialUnitDa().GetByPropertyManager(ddlPropertyManager.SelectedValue);
                if (objResidentialUnits != null && objResidentialUnits.Count > 0)
                {
                    foreach (ResidentialUnit obj in objResidentialUnits)
                    {
                        string sName = obj.UnitName.ToString() + " (" + obj.Serial.ToString() + ")";
                        ddlUnit.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
                //ddlUnit.DataSource = new ResidentialUnitDa().GetByPropertyManager(ddlPropertyManager.SelectedValue);
                //ddlUnit.DataTextField = "Serial";
                //ddlUnit.DataValueField = "Serial";
                ddlUnit.DataBind();
                ddlUnit.SelectedValue = "-1";
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlUnit.Items.Clear();
                ddlUnit.AppendDataBoundItems = true;
                ddlUnit.Items.Add(new ListItem("Select Unit", "-1"));
                List<ResidentialUnit> objResidentialUnits = new ResidentialUnitDa().GetByLocation(ddlLocation.SelectedValue);
                if (objResidentialUnits != null && objResidentialUnits.Count > 0)
                {
                    foreach (ResidentialUnit obj in objResidentialUnits)
                    {
                        string sName = obj.UnitName.ToString() + " (" + obj.Serial.ToString() + ")";
                        ddlUnit.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
                //ddlUnit.DataSource = new ResidentialUnitDa().GetByLocation(ddlLocation.SelectedValue);
                //ddlUnit.DataTextField = "Serial";
                //ddlUnit.DataValueField = "Serial";
                ddlUnit.DataBind();
                ddlUnit.SelectedValue = "-1";
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            if (row != null)
            {
                Label hdApplicationId = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblAppId");
                if (!string.IsNullOrEmpty(hdApplicationId.Text))
                {
                    hdnAppId.Value = hdApplicationId.Text.ToString();
                    HttpContext.Current.Session["TenentId"] = hdnAppId.Value;
                }
                
               
                Label hdUnitSerial = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblUnitSerialId");
                if (!string.IsNullOrEmpty(hdUnitSerial.Text))
                {
                    hdnUnitSerial.Value = hdUnitSerial.Text.ToString();
                    HttpContext.Current.Session["ResidentialUnitSerial"] = hdnUnitSerial.Value;
                }
               

                Label hdTenantPassword = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblTenantPassword");
                if (!string.IsNullOrEmpty(hdTenantPassword.Text))
                {
                    hdnPassword.Value = hdTenantPassword.Text.ToString();
                    HttpContext.Current.Session["TenantPassword"] = hdnPassword.Value;
                }

                try
                {
                    if (!string.IsNullOrEmpty(hdnUnitSerial.Value) && !string.IsNullOrEmpty(hdnAppId.Value))
                    {
                        //HttpContext.Current.Session["ResidentialUnitSerial"] = hdnUnitSerial.Value;
                        //HttpContext.Current.Session["TenentId"] = hdnAppId.Value;
                      //  HttpContext.Current.Session["TenantPassword"] = hdnPassword.Value;
                        Response.Redirect(Utility.WebUrl + "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx?ResidentialUnitSerial=" + hdnUnitSerial.Value + "&&TenentId=" + hdnAppId.Value, false);

                    }
                    else
                    {
                        Utility.DisplayMsg("Please click on details button to select application", this);
                    }
                }
                catch (Exception ex)
                {


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

        }
        private void FillTenantList(string ownerId, string propertyManagerId, string locationId, string unitId, string tenantId)
        {
            try
            {
                List<usp_GetTenantApplication_Result> obj = null;
                obj = new ResidentialAddResponceTemplateDa().GetResidentTenantsBySearch(ownerId, propertyManagerId, locationId, unitId, tenantId);
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