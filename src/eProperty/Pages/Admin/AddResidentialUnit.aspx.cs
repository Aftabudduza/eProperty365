using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using eProperty.Models;
//using eProperty.Models;
//using Newtonsoft.Json;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.ViewModel;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace eProperty.Pages.Admin
{
    public partial class AddResidentialUnit : Page
    {
        private string _errStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropdowns();
                txtUnitName.ReadOnly = false;
                txtUnitType.ReadOnly = false;

                if (Session["UserObject"] != null)
                {
                    Session["AddResidentialUnitId"] = null;
                    Session["ResidentialUnitSpecsChildId"] = null;
                    Session["ResidentialUnitSpecsId"] = null;
                    Session["ResidentialUnitSerial"] = null;

                    int cId;
                    try
                    {
                        cId = Convert.ToInt32(Request.QueryString["AddResidentialUnitId"]);
                    }
                    catch
                    {
                        cId = 0;
                    }
                    //FillControls(Convert.ToInt32(Session["AddResidentialUnitId"].ToString()));
                    //FillFeaturesGrid();

                    var isAdmin = false;
                    if (Session["UserObject"] != null)
                        isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                            ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                            : false;

                    if (isAdmin == false && Session["OwnerId"] != null)
                    {
                        ddlOwnerIdTop.SelectedValue = Session["OwnerId"].ToString();
                        ddlOwnerIdTop.Enabled = false;
                        ddlOwnerIdTop.SelectedValue = Session["OwnerId"].ToString();
                        ddlOwnerIdTop.Enabled = false;

                        ddlPropertyManagerID.Items.Clear();
                        ddlPropertyManagerID.AppendDataBoundItems = true;
                        ddlPropertyManagerID.Items.Add(new ListItem("No Property Manager", "-1"));
                        List<PropertyManagerProfile> objPropertyManagers = new PropertyManagerProfileDA().GetByOwnerId(Session["OwnerId"].ToString());
                        if (objPropertyManagers != null && objPropertyManagers.Count > 0)
                        {
                            foreach (PropertyManagerProfile obj in objPropertyManagers)
                            {
                                string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                                ddlPropertyManagerID.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                            }
                        }
                        ddlPropertyManagerID.DataBind();
                        ddlPropertyManagerID.SelectedValue = "-1";

                        ddlLocationID.Items.Clear();
                        ddlLocationID.AppendDataBoundItems = true;
                        ddlLocationID.Items.Add(new ListItem("Select Location", "-1"));
                        ddlLocationID.DataSource = new LocationDA().GetByOwner(Session["OwnerId"].ToString());
                        ddlLocationID.DataTextField = "LocationName";
                        ddlLocationID.DataValueField = "Serial";
                        ddlLocationID.DataBind();
                        ddlLocationID.SelectedValue = "-1";
                    }

                    FillResidentialUnit();
                    txtUnitID.Text = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnit");
                    Session["ResidentialUnitSerial"] = txtUnitID.Text.ToString();
                    if (cId > 0)
                    {
                        H1.InnerText = "Change Resident Unit";
                        Session["AddResidentialUnitId"] = cId;
                        FillControls(cId);
                    }


                    SystemInformation objGlobal = new AdminSystemInformationDA().GetGlobalInfo();

                    if (objGlobal != null)
                    {
                        decimal nTotalAppFee = (objGlobal.ApplicationFee != null ? Convert.ToDecimal(objGlobal.ApplicationFee) : 0) + (objGlobal.ScreeningFee != null ? Convert.ToDecimal(objGlobal.ScreeningFee) : 0);

                        txtApplicationFee.Text = Convert.ToDecimal(nTotalAppFee).ToString("#.00");
                        hdnTotalFee.Value = Convert.ToDecimal(nTotalAppFee).ToString("#.00");
                        txtAdvertised.Text = txtApplicationFee.Text.ToString();
                        if (objGlobal.ApplicationFee != null && objGlobal.ApplicationFee.ToString() != string.Empty)
                        {
                            hdnApplicationFee.Value = objGlobal.ApplicationFee != null ? Convert.ToDecimal(objGlobal.ApplicationFee).ToString("#.00") : "0";
                        }

                        if (objGlobal.ScreeningFee != null && objGlobal.ScreeningFee.ToString() != string.Empty)
                        {
                            hdnScreenFee.Value = objGlobal.ScreeningFee != null ? Convert.ToDecimal(objGlobal.ScreeningFee).ToString("#.00") : "0";
                        }
                    }
                }
            }
        }
        private void FillFeaturesGrid()
        {
            //if (Session["ResidentialUnitSerial"] != null ||
            //    !string.IsNullOrEmpty(Session["ResidentialUnitSerial"]?.ToString()))
            //{
            //    gvFeatureNameList.DataSource =
            //        new ResidentialUnitDa().GetAllFeatureList(Session["ResidentialUnitSerial"]?.ToString());
            //    gvFeatureNameList.DataBind();
            //}
            //else
            //{
            //    gvFeatureNameList.DataSource = new List<ResidentialUnitSpecsChild>();
            //    gvFeatureNameList.DataBind();
            //}
            //btnAddFeatureName.Text = "Add";
        }
        private void FillControls(int nId)
        {
            if (nId <= 0) return;
            var obj = new ResidentialUnitDa().GetbyId(nId);
            if (obj == null) return;
            Session["AddResidentialUnitId"] = obj.Id;
            Session["ResidentialUnitSerial"] = obj.Serial;
            ddlOwnerIdTop.SelectedValue = !string.IsNullOrEmpty(obj.OwnerId) ? obj.OwnerId : "-1";
            ddlPropertyManagerID.SelectedValue = !string.IsNullOrEmpty(obj.PropertyManagerSerialId)
                ? obj.PropertyManagerSerialId
                : "-1";
            ddlLocationID.SelectedValue = !string.IsNullOrEmpty(obj.LocationSerialId) ? obj.LocationSerialId : "-1";
            txtUnitID.Text = !string.IsNullOrEmpty(obj.Serial) ? obj.Serial : string.Empty;
            txtUnitName.Text = !string.IsNullOrEmpty(obj.UnitName) ? obj.UnitName : string.Empty;
            txtUnitType.Text = !string.IsNullOrEmpty(obj.UnitType) ? obj.UnitType : string.Empty;
            txtSpecialComments.Text = !string.IsNullOrEmpty(obj.SpecialStatements) ? obj.SpecialStatements : string.Empty;
            // btnSaveBasicUnit.Text = "Update Unit/Lots";
            Session["ResidentialUnitSpecsChildId"] = null;
            Session["ResidentialUnitSpecsId"] = null;

            if (obj.IsBackgroundCheck != null)
            {
                if (Convert.ToBoolean(obj.IsBackgroundCheck) == true)
                {
                    rdoScreening.Items[0].Selected = true;
                    txtApplicationFee.Text = hdnTotalFee.Value;
                }
                else
                {
                    rdoScreening.Items[1].Selected = true;
                    txtApplicationFee.Text = hdnApplicationFee.Value;
                }
            }
            else
            {
                rdoScreening.Items[0].Selected = true;
                txtApplicationFee.Text = hdnTotalFee.Value;
            }

            FillFeaturesGrid();
            FillSpecsGrid();
            // FillWebPageImageGrid();
            txtShortWebUrl.Text = !string.IsNullOrEmpty(obj.ShortWebUrl) ? obj.ShortWebUrl : string.Empty;

            string sweb = Utility.WebUrl + "/Pages/Resident/ResidentialAddResponceTemplate_Login.aspx?ResidentialUnitSerial=" + txtUnitID.Text.ToString().Trim();
            lblWebUrl.InnerHtml = "<a href=" + sweb + " target='_blank'>" + sweb + "</a>";

            if (!string.IsNullOrEmpty(obj.ImageType))
            {
                if (obj.ImageType == "1")
                {
                    rdoRentType.Items[0].Selected = true;
                }
                else if (obj.ImageType == "2")
                {
                    rdoRentType.Items[1].Selected = true;
                }
            }
            else
            {
                rdoRentType.Items[0].Selected = true;
            }

        }

        private void FillControlForFeatures(int nId)
        {
            if (nId <= 0) return;
            var obj = new ResidentialUnitDa().GetSpescFeaturebyId(nId);
            if (obj == null) return;
            Session["ResidentialUnitSpecsChildId"] = null;
            Session["ResidentialUnitSpecsChildId"] = obj.Id;
            //txtFeatureName.Text = !string.IsNullOrEmpty(obj.FeatureName) ? obj.FeatureName : string.Empty;
            //btnAddFeatureName.Text = "Update";
        }

        private void FillDropdowns()
        {
            try
            {
                ddlOwnerIdTop.Items.Clear();
                ddlOwnerIdTop.AppendDataBoundItems = true;
                ddlOwnerIdTop.Items.Add(new ListItem("Select Owner", "-1"));
                //List<OwnerProfile> objOwners = new AdminOwnerProfileDA().GetAllOwnersInfo();
                List<OwnerProfile> objOwners = new OwnerProfileDA().GetAllOwnersInfo();
                if (objOwners != null && objOwners.Count > 0)
                {
                    foreach (OwnerProfile obj in objOwners)
                    {
                        string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                        ddlOwnerIdTop.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
                ddlOwnerIdTop.DataBind();
                ddlOwnerIdTop.SelectedValue = "-1";

                //ddlPropertyManagerID.Items.Clear();
                //ddlPropertyManagerID.AppendDataBoundItems = true;
                //ddlPropertyManagerID.Items.Add(new ListItem("Select Property Manager", "-1"));
                //ddlPropertyManagerID.DataSource =
                //    new PropertyManagerProfileDA().GetByOwnerId(ddlOwnerIdTop.SelectedValue);
                //ddlPropertyManagerID.DataTextField = "FirstName";
                //ddlPropertyManagerID.DataValueField = "Serial";
                //ddlPropertyManagerID.DataBind();
                //ddlPropertyManagerID.SelectedValue = "-1";

                //ddlLocationID.Items.Clear();
                //ddlLocationID.AppendDataBoundItems = true;
                //ddlLocationID.Items.Add(new ListItem("Select Location", "-1"));
                //ddlLocationID.DataSource = new LocationDA().GetByOwner(ddlOwnerIdTop.SelectedValue);
                //ddlLocationID.DataTextField = "LocationName";
                //ddlLocationID.DataValueField = "Serial";
                //ddlLocationID.DataBind();
                //ddlLocationID.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder("Something Bad happened! Please check following message:");
                sb.AppendLine();
                sb.Append(ex);
                //lblErrorMsg.Visible = true;
                //lblErrorMsg.Text = sb.ToString();
            }
        }

        protected void ddlOwnerIdTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlPropertyManagerID.Items.Clear();
                ddlPropertyManagerID.AppendDataBoundItems = true;
                ddlPropertyManagerID.Items.Add(new ListItem("No Property Manager", "-1"));
                List<PropertyManagerProfile> objPropertyManagers = new PropertyManagerProfileDA().GetByOwnerId(ddlOwnerIdTop.SelectedValue);
                if (objPropertyManagers != null && objPropertyManagers.Count > 0)
                {
                    foreach (PropertyManagerProfile obj in objPropertyManagers)
                    {
                        string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                        ddlPropertyManagerID.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
                ddlPropertyManagerID.DataBind();
                ddlPropertyManagerID.SelectedValue = "-1";

                ddlLocationID.Items.Clear();
                ddlLocationID.AppendDataBoundItems = true;
                ddlLocationID.Items.Add(new ListItem("Select Location", "-1"));
                ddlLocationID.DataSource = new LocationDA().GetByOwner(ddlOwnerIdTop.SelectedValue);
                ddlLocationID.DataTextField = "LocationName";
                ddlLocationID.DataValueField = "Serial";
                ddlLocationID.DataBind();
                ddlLocationID.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder("Something Bad happened! Please check following message:");
                sb.AppendLine();
                sb.Append(ex);
                //lblErrorMsg.Visible = true;
                //lblErrorMsg.Text = sb.ToString();
            }
        }

        //protected void ddlPropertyManagerID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(ddlPropertyManagerID.SelectedValue != "-1")
        //    {
        //        try
        //        {
        //            ddlLocationID.Items.Clear();
        //            ddlLocationID.AppendDataBoundItems = true;
        //            ddlLocationID.Items.Add(new ListItem("Select Location", "-1"));
        //            ddlLocationID.DataSource = new LocationDA().GetByPropertyManager(ddlPropertyManagerID.SelectedValue);
        //            ddlLocationID.DataTextField = "LocationName";
        //            ddlLocationID.DataValueField = "Serial";
        //            ddlLocationID.DataBind();
        //            ddlLocationID.SelectedValue = "-1";
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }

        //}
        private void ClearControls()
        {
            txtUnitName.Text = "";
            txtUnitType.Text = "";
            txtUnitID.Text = "";
            txtSpecialComments.Text = "";
            //ddlOwnerIdTop.SelectedValue = "-1";
            ddlPropertyManagerID.SelectedValue = "-1";
            ddlLocationID.SelectedValue = "-1";

            txtUnitID.Text = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnit");
            Session["ResidentialUnitSerial"] = txtUnitID.Text.ToString();
            // btnSaveBasicUnit.Text = "Create Unit/Lots";
        }

        private ResidentialUnit SetData(ResidentialUnit obj)
        {
            if (Session["AddResidentialUnitId"] != null && Convert.ToInt32(Session["AddResidentialUnitId"]) > 0)
                obj.Id = Convert.ToInt32(Session["AddResidentialUnitId"].ToString());
            obj.OwnerId = ddlOwnerIdTop.SelectedValue != "-1" ? ddlOwnerIdTop.SelectedValue.Trim() : "-1";
            obj.PropertyManagerSerialId = ddlPropertyManagerID.SelectedValue != "-1"
                ? ddlPropertyManagerID.SelectedValue.Trim()
                : "-1";
            obj.LocationSerialId = ddlLocationID.SelectedValue != "-1" ? ddlLocationID.SelectedValue.Trim() : "-1";
            if (Session["AddResidentialUnitId"] != null)
            {
                obj.Serial = !string.IsNullOrEmpty(txtUnitID.Text.ToString()) ? txtUnitID.Text.ToString() : Session["ResidentialUnitSerial"].ToString();
                obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                obj.UpdatedDate = DateTime.Now;
            }
            else
            {
                obj.Serial = !string.IsNullOrEmpty(txtUnitID.Text.ToString()) ? txtUnitID.Text.ToString() : new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnit");
                obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                obj.CreatedDate = DateTime.Now;
            }

            obj.UnitName = !string.IsNullOrEmpty(txtUnitName.Text) ? txtUnitName.Text : string.Empty;
            obj.UnitType = !string.IsNullOrEmpty(txtUnitType.Text) ? txtUnitType.Text : string.Empty;
            obj.SpecialStatements = !string.IsNullOrEmpty(txtSpecialComments.Text) ? txtSpecialComments.Text : string.Empty;

            bool bIsCheck = false;
            if (rdoScreening.Items[0].Selected == true)
            {
                bIsCheck = true;
            }
            else if (rdoScreening.Items[1].Selected == true)
            {
                bIsCheck = false;
            }
            obj.IsBackgroundCheck = bIsCheck;
            return obj;
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx", false);
        }

        public string Validate_Control()
        {
            if (ddlOwnerIdTop.SelectedValue == "-1")
                _errStr += "Please select Owner ID" + Environment.NewLine;
            //if (ddlPropertyManagerID.SelectedValue == "-1")
            //    _errStr += "Please enter Property Manager" + Environment.NewLine;
            if (ddlLocationID.SelectedValue == "-1")
                _errStr += "Please enter Location Id" + Environment.NewLine;
            //if (txtUnitID.Text.Trim().Length <= 0)
            //{
            //    _errStr += "Please enter Unit ID" + Environment.NewLine;
            //}
            if (txtUnitType.Text.Trim().Length <= 0)
                _errStr += "Please enter Unit Type" + Environment.NewLine;

            List<ResidentialUnit> objCount = null;

            objCount = new ResidentialUnitDa().GetByOwnerAndUnitName(ddlOwnerIdTop.SelectedValue, txtUnitName.Text.ToString().Trim());

            if (Session["AddResidentialUnitId"] != null)
            {
                if (objCount.Count > 1)
                {
                    _errStr += "Unit Name is already exist !!! " + Environment.NewLine;
                }

            }
            else
            {
                if (objCount.Count > 0)
                {
                    _errStr += "Unit Name is already exist !!! " + Environment.NewLine;
                }
            }

            return _errStr;
        }

        protected void btnEditSpecsChild_Click(object sender, EventArgs e)
        {
            //var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            //if (row != null)
            //{
            //    var hdId = (Label)gvFeatureNameList.Rows[row.RowIndex].FindControl("lblSpecsChildId");

            //    if (!string.IsNullOrEmpty(hdId.Text))
            //        FillControlForFeatures(Convert.ToInt32(hdId.Text));
            //}
        }

        protected void btnDeleteSpecsChild_Click(object sender, EventArgs e)
        {
            //var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            //if (row != null)
            //{
            //    var hdId = (Label)gvFeatureNameList.Rows[row.RowIndex].FindControl("lblSpecsChildId");

            //    if (!string.IsNullOrEmpty(hdId.Text))
            //        if (new ResidentialUnitDa().DeleteSpecFeatureById(Convert.ToInt32(hdId.Text)))
            //            FillFeaturesGrid();
            //}
        }

        protected void btnAddFeatureName_Click(object sender, EventArgs e)
        {
            //Session["ResidentialUnitSerial"] = obj.Serial;
            if (Session["ResidentialUnitSerial"] == null &&
                string.IsNullOrEmpty(Session["ResidentialUnitSerial"]?.ToString()))
                return;
            var obj = new ResidentialUnitSpecsChild();
            obj = SetFeatureData(obj);
            if (Session["ResidentialUnitSpecsChildId"] != null ||
                !string.IsNullOrEmpty(Session["ResidentialUnitSpecsChildId"]?.ToString()))
            {
                if (new ResidentialUnitDa().UpdateFeature(obj))
                {
                    Session["ResidentialUnitSpecsChildId"] = null;
                    Utility.DisplayMsg("Feature updated successfully!", this);
                }
                else
                {
                    Utility.DisplayMsg("Feature not updated!", this);
                }
            }
            else
            {
                if (new ResidentialUnitDa().InsertFeature(obj))
                {
                    Session["ResidentialUnitSpecsChildId"] = null;
                    Utility.DisplayMsg("Feature saved successfully!", this);
                }
                else
                {
                    Utility.DisplayMsg("Feature not saved!", this);
                }
            }
            FillFeaturesGrid();
            //txtFeatureName.Text = string.Empty;
        }

        public ResidentialUnitSpecsChild SetFeatureData(ResidentialUnitSpecsChild obj)
        {
            if (Session["ResidentialUnitSerial"] == null &&
                Convert.ToString(Session["ResidentialUnitSerial"]) == string.Empty)
                return obj;
            if (Session["ResidentialUnitSpecsChildId"] != null &&
                Convert.ToInt32(Session["ResidentialUnitSpecsChildId"]) > 0)
                obj.Id = Convert.ToInt32(Session["ResidentialUnitSpecsChildId"].ToString());
            //Session["ResidentialUnitSerial"] = obj.Serial;
            //obj.FeatureName = txtFeatureName.Text;
            obj.ResidentialUnitSpecsSerialId = Session["ResidentialUnitSerial"]?.ToString();
            return obj;
        }

        private void FillResidentialUnit()
        {
            try
            {
                var isAdmin = false;
                if (Session["UserObject"] != null)
                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                        ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                        : false;

                List<ResidentialUnit> obj = null;
                if (isAdmin == true)
                {
                    obj = new ResidentialUnitDa().GetAllInformation();
                }
                else
                {
                    if (Session["OwnerId"] != null)
                        obj = new ResidentialUnitDa().GetByOwner(Session["OwnerId"].ToString());
                }


                gvUnitList.DataSource = obj;
                gvUnitList.DataBind();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected void gvUnitList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUnitList.PageIndex = e.NewPageIndex;
            FillResidentialUnit();
        }

        protected void gvUnitList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillResidentialUnit();
        }

        protected void btnUnitEdit_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            if (row != null)
            {
                var hdId = (Label)gvUnitList.Rows[row.RowIndex].FindControl("lblId");

                if (!string.IsNullOrEmpty(hdId.Text))
                    FillControls(Convert.ToInt32(hdId.Text));
                BindVideo(Session["ResidentialUnitSerial"]?.ToString());
                FillControlsSpecsEdit();
            }
        }

        protected void btnUnitDelete_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            if (row != null)
            {
                var hdId = (Label)gvUnitList.Rows[row.RowIndex].FindControl("lblId");

                if (!string.IsNullOrEmpty(hdId.Text))
                {
                    var sql = " delete from ResidentialUnit where Id = '" + hdId.Text.ToString() + "' ";
                    Utility.RunCMD(sql);
                    FillResidentialUnit();
                    Utility.DisplayMsg("Residential unit deleted successfully!", this);

                    //if (new ResidentialUnitDa().DeleteById(Convert.ToInt32(hdId.Text)))
                    //{
                    //    FillResidentialUnit();
                    //    Utility.DisplayMsg("Residential unit deleted successfully!", this);
                    //}
                }

            }
        }

        protected void btnSaveBasicUnit_Click(object sender, EventArgs e)
        {
            _errStr = Validate_Control();
            if (_errStr.Length <= 0)
            {
                var obj = new ResidentialUnit();
                obj = SetData(obj);
                var username = "";
                if (Session["UserObject"] != null)
                    username = ((UserProfile)Session["UserObject"]).Username;
                var sql = " update UserProfile set HasPropertyUnit = 1  where Username = '" + username + "' ";

                var objResidentialUnitSpecs = new ResidentialUnitSpecs();
                objResidentialUnitSpecs = SetDataForSpecs(objResidentialUnitSpecs);

                if (Session["AddResidentialUnitId"] == null || ReferenceEquals(Session["AddResidentialUnitId"], "0"))
                {
                    if (new ResidentialUnitDa(true, false).Insert(obj))
                    {
                        Utility.RunCMD(sql);
                        Utility.RunCMDMain(sql);
                        //Session["AddResidentialUnitId"] = null;
                        //ClearControls();
                        if (objResidentialUnitSpecs.UnitMonthlyRent != null && Convert.ToDecimal(objResidentialUnitSpecs.UnitMonthlyRent) > 0)
                        {
                            try
                            {
                                //var objResidentialUnitSpecs = new ResidentialUnitSpecs();
                                //objResidentialUnitSpecs = SetDataForSpecs(objResidentialUnitSpecs);

                                if (Session["ResidentialUnitSpecsId"] != null ||
                                    !string.IsNullOrEmpty(Session["ResidentialUnitSpecsId"]?.ToString()))
                                {
                                    if (new ResidentialUnitDa(true).UpdateSpecs(objResidentialUnitSpecs))
                                    {
                                        Session["ResidentialUnitSpecsId"] = null;
                                        Session["serial"] = null;
                                        FillSpecsGrid();
                                        ClearSpecControls();
                                    }
                                }
                                else
                                {
                                    if (new ResidentialUnitDa(true, false).InsertSpecs(objResidentialUnitSpecs))
                                    {
                                        Session["ResidentialUnitSpecsId"] = null;
                                        FillSpecsGrid();
                                        ClearSpecControls();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                        FillResidentialUnit();
                        Session["AddResidentialUnitId"] = null;
                        ClearControls();
                        Utility.DisplayMsg("Residential unit saved successfully!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Residential unit not saved!", this);
                    }
                }
                else
                {
                    if (new ResidentialUnitDa().Update(obj))
                    {
                        Utility.RunCMD(sql);
                        Utility.RunCMDMain(sql);
                        //Session["AddResidentialUnitId"] = null;
                        //ClearControls();

                        if (objResidentialUnitSpecs.UnitMonthlyRent != null && Convert.ToDecimal(objResidentialUnitSpecs.UnitMonthlyRent) > 0)
                        {
                            try
                            {
                                //var objResidentialUnitSpecs = new ResidentialUnitSpecs();
                                //objResidentialUnitSpecs = SetDataForSpecs(objResidentialUnitSpecs);

                                if (Session["ResidentialUnitSpecsId"] != null ||
                                    !string.IsNullOrEmpty(Session["ResidentialUnitSpecsId"]?.ToString()))
                                {
                                    if (new ResidentialUnitDa(true).UpdateSpecs(objResidentialUnitSpecs))
                                    {
                                        Session["ResidentialUnitSpecsId"] = null;
                                        Session["serial"] = null;
                                        FillSpecsGrid();
                                        ClearSpecControls();
                                    }
                                }
                                else
                                {
                                    if (new ResidentialUnitDa(true, false).InsertSpecs(objResidentialUnitSpecs))
                                    {
                                        Session["ResidentialUnitSpecsId"] = null;
                                        FillSpecsGrid();
                                        ClearSpecControls();
                                    }
                                }


                            }
                            catch (Exception ex)
                            {

                            }

                        }

                        FillResidentialUnit();
                        Session["AddResidentialUnitId"] = null;
                        ClearControls();
                        Utility.DisplayMsg("Residential unit updated successfully!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Residential unit not updated!", this);
                    }
                }


            }
            else
            {
                Utility.DisplayMsg(_errStr, this);
            }
        }

        protected void btnDuplicate_OnClick(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            if (row != null)
            {
                var hdId = (Label)gvUnitList.Rows[row.RowIndex].FindControl("lblId");

                if (!string.IsNullOrEmpty(hdId.Text))
                {
                    if (Convert.ToInt32(hdId.Text) > 0 && !string.IsNullOrEmpty(txtUnitID.Text))
                    {
                        if (new ResidentialUnitDa().CreateDuplicateValue(Convert.ToInt32(hdId.Text), txtUnitID.Text))
                        {
                            txtUnitID.Text = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnit");
                            FillResidentialUnit();
                            Utility.DisplayMsg("Sucessfully Duplicate Residential Unit ", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Failed to Duplicate Residential Unit !", this);
                        }
                    }

                }

            }
        }

        //// Specs Entry
        private void FillControlsSpecsEdit()
        {
            var unitId = Session["ResidentialUnitSerial"].ToString();
            var obj = new ResidentialUnitDa().GetSpecsbyIdEdit(unitId);
            if (obj != null) //---------- obj check amin
            {
                Session["ResidentialUnitSpecsId"] = obj.Id;
                Session["serial"] = obj.Serial;

                txtTotalSize.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitSize))
                    ? obj.UnitSize.ToString()
                    : string.Empty;
                txtNumberfloors.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitNumberFloors))
                    ? obj.UnitNumberFloors.ToString()
                    : string.Empty;
                txtMonthlyRentAmount.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitMonthlyRent))
                    ? obj.UnitMonthlyRent.ToString()
                    : string.Empty;
                txtLotSize.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitLotSize))
                    ? obj.UnitLotSize.ToString()
                    : string.Empty;
                //txtApplicationFee.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitApplicationFee))
                //    ? obj.UnitApplicationFee.ToString()
                //    : hdnTotalFee.Value;
                txtAgentName.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitAgentName))
                    ? obj.UnitAgentName
                    : string.Empty;
                txtAgentPhoneNumber.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitAgentPhone))
                    ? obj.UnitAgentPhone
                    : string.Empty;
                txtBroker.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitBrokerName))
                    ? obj.UnitBrokerName
                    : string.Empty;
                txtBrokerPhoneNumber.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitBrokerPhone))
                    ? obj.UnitBrokerPhone
                    : string.Empty;
                txtSpecialStatementBtm.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitSpecialStatements))
                    ? obj.UnitSpecialStatements
                    : string.Empty;
                ddlNumberofBaths.SelectedValue = !string.IsNullOrEmpty(Convert.ToString(obj.UnitNumberOfBaths))
                    ? obj.UnitNumberOfBaths.ToString()
                    : "-1";
                ddlNumberBedrooms.SelectedValue = !string.IsNullOrEmpty(Convert.ToString(obj.UnitNumberBedrooms))
                    ? obj.UnitNumberBedrooms.ToString()
                    : "-1";
                chkIncludedStatement.Checked = (bool)obj.IsUnitIncludeCardProcessCharge;
                btnSaveSpecs.Text = "Update Specs";

                if (rdoScreening.Items[0].Selected == true)
                {
                    txtApplicationFee.Text = hdnTotalFee.Value;
                }
                else if (rdoScreening.Items[1].Selected == true)
                {
                    txtApplicationFee.Text = hdnApplicationFee.Value;
                }

                txtAdvertised.Text = !string.IsNullOrEmpty(Convert.ToString(obj.AdvertisedFee))
                   ? obj.AdvertisedFee.ToString()
                   : txtApplicationFee.Text.ToString();

            }

        }
        private void FillControlsSpecs(int nId)
        {
            if (nId <= 0) return;
            var obj = new ResidentialUnitDa().GetSpecsbyId(nId);
            if (obj == null) return;
            Session["ResidentialUnitSpecsId"] = obj.Id;
            Session["serial"] = obj.Serial;
            txtTotalSize.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitSize))
                ? obj.UnitSize.ToString()
                : string.Empty;
            txtNumberfloors.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitNumberFloors))
                ? obj.UnitNumberFloors.ToString()
                : string.Empty;
            txtMonthlyRentAmount.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitMonthlyRent))
                ? obj.UnitMonthlyRent.ToString()
                : string.Empty;
            txtLotSize.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitLotSize))
                ? obj.UnitLotSize.ToString()
                : string.Empty;
            txtApplicationFee.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitApplicationFee))
                ? obj.UnitApplicationFee.ToString()
                : string.Empty;
            txtAgentName.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitAgentName))
                ? obj.UnitAgentName
                : string.Empty;
            txtAgentPhoneNumber.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitAgentPhone))
                ? obj.UnitAgentPhone
                : string.Empty;
            txtBroker.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitBrokerName))
                ? obj.UnitBrokerName
                : string.Empty;
            txtBrokerPhoneNumber.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitBrokerPhone))
                ? obj.UnitBrokerPhone
                : string.Empty;
            txtSpecialStatementBtm.Text = !string.IsNullOrEmpty(Convert.ToString(obj.UnitSpecialStatements))
                ? obj.UnitSpecialStatements
                : string.Empty;
            ddlNumberofBaths.SelectedValue = !string.IsNullOrEmpty(Convert.ToString(obj.UnitNumberOfBaths))
                ? obj.UnitNumberOfBaths.ToString()
                : "-1";
            ddlNumberBedrooms.SelectedValue = !string.IsNullOrEmpty(Convert.ToString(obj.UnitNumberBedrooms))
                ? obj.UnitNumberBedrooms.ToString()
                : "-1";
            chkIncludedStatement.Checked = (bool)obj.IsUnitIncludeCardProcessCharge;
            btnSaveSpecs.Text = "Update Specs";
        }

        private void FillSpecsGrid()
        {
            if (Session["ResidentialUnitSerial"] != null ||
                !string.IsNullOrEmpty(Session["ResidentialUnitSerial"]?.ToString()))
            {
                // gvResidentialUnitSpecs.DataSource =
                new ResidentialUnitDa().GetAllSpecsList(Session["ResidentialUnitSerial"]?.ToString());
                // gvResidentialUnitSpecs.DataBind();
            }
            else
            {
                // gvResidentialUnitSpecs.DataSource = new List<ResidentialUnitSpecs>();
                // gvResidentialUnitSpecs.DataBind();
            }
            btnSaveSpecs.Text = "Save Specs";
        }
        protected void rdoScreening_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoScreening.SelectedValue != null)
            {
                if (rdoScreening.Items[0].Selected == true)
                {
                    txtApplicationFee.Text = hdnTotalFee.Value;
                }
                else if (rdoScreening.Items[1].Selected == true)
                {
                    txtApplicationFee.Text = hdnApplicationFee.Value;
                }
                else
                {
                    txtApplicationFee.Text = hdnTotalFee.Value;
                }
            }
            else
            {
                txtApplicationFee.Text = hdnTotalFee.Value;
            }
        }
        protected void btnSaveSpecs_Click(object sender, EventArgs e)
        {
            if (Session["ResidentialUnitSerial"] == null &&
                string.IsNullOrEmpty(Session["ResidentialUnitSerial"]?.ToString()))
                return;

            string sImageType = "1";
            string sShortUrl = !string.IsNullOrEmpty(txtShortWebUrl.Text) ? txtShortWebUrl.Text : string.Empty;

            if (rdoUnitRentType.Items[0].Selected == true)
            {
                sImageType = "1";
            }
            else if (rdoUnitRentType.Items[1].Selected == true)
            {
                sImageType = "2";
            }

            bool bIsCheck = false;

            if (rdoScreening.Items[0].Selected == true)
            {
                bIsCheck = true;
            }
            else if (rdoScreening.Items[1].Selected == true)
            {
                bIsCheck = false;
            }

            var username = "";
            if (Session["UserObject"] != null)
            {
                username = ((UserProfile)Session["UserObject"]).Username;
            }

            var sql = " update UserProfile set HasPropertyUnit = 1  where Username = '" + username + "' ";

            _errStr = ValidateSpecs();

            if (_errStr.Length <= 0)
            {
                var unitId = !string.IsNullOrEmpty(txtUnitID.Text) ? txtUnitID.Text : string.Empty;
                var GetUnitId = new ResidentialUnitDa().GetbySerial(unitId);
                var unitObj = new ResidentialUnit();
                try
                {
                    if (GetUnitId != null)
                    {
                        unitObj.Id = GetUnitId.Id;
                        unitObj.Serial = !string.IsNullOrEmpty(txtUnitID.Text) ? txtUnitID.Text : string.Empty; // Session["ResidentialUnitSerial"].ToString();
                        unitObj.UnitName = !string.IsNullOrEmpty(txtUnitName.Text) ? txtUnitName.Text : string.Empty;
                        unitObj.UnitType = !string.IsNullOrEmpty(txtUnitType.Text) ? txtUnitType.Text : string.Empty;
                        unitObj.OwnerId = ddlOwnerIdTop.SelectedValue;
                        unitObj.PropertyManagerSerialId = ddlPropertyManagerID.SelectedValue.ToString() != "" ? ddlPropertyManagerID.SelectedValue.ToString() : "";
                        unitObj.LocationSerialId = ddlLocationID.SelectedValue.ToString() != "" ? ddlLocationID.SelectedValue.ToString() : "";
                        unitObj.SpecialStatements = !string.IsNullOrEmpty(txtSpecialComments.Text) ? txtSpecialComments.Text : string.Empty;
                        unitObj.IsBackgroundCheck = bIsCheck;
                        //unitObj.Serial = GetUnitId.Serial;
                        //unitObj.UnitName = GetUnitId.UnitName;
                        //unitObj.UnitType = GetUnitId.UnitType;
                        //unitObj.SpecialStatements = GetUnitId.SpecialStatements;
                        //unitObj.OwnerId = GetUnitId.OwnerId;
                        //unitObj.PropertyManagerSerialId = GetUnitId.PropertyManagerSerialId;
                        //unitObj.LocationSerialId = GetUnitId.LocationSerialId;
                        //unitObj.ImageType = sImageType;
                        //unitObj.ShortWebUrl = sShortUrl;
                        // new ResidentialUnitDa().Update(unitObj);

                        if (new ResidentialUnitDa().Update(unitObj))
                        {
                            Utility.RunCMD(sql);
                            Utility.RunCMDMain(sql);
                        }
                    }
                    else
                    {

                        unitObj.Serial = !string.IsNullOrEmpty(txtUnitID.Text) ? txtUnitID.Text : string.Empty; // Session["ResidentialUnitSerial"].ToString();
                        unitObj.UnitName = !string.IsNullOrEmpty(txtUnitName.Text) ? txtUnitName.Text : string.Empty;
                        unitObj.UnitType = !string.IsNullOrEmpty(txtUnitType.Text) ? txtUnitType.Text : string.Empty;
                        unitObj.OwnerId = ddlOwnerIdTop.SelectedValue;
                        unitObj.PropertyManagerSerialId = ddlPropertyManagerID.SelectedValue.ToString() != "" ? ddlPropertyManagerID.SelectedValue.ToString() : "";
                        unitObj.LocationSerialId = ddlLocationID.SelectedValue.ToString() != "" ? ddlLocationID.SelectedValue.ToString() : "";
                        unitObj.SpecialStatements = !string.IsNullOrEmpty(txtSpecialComments.Text) ? txtSpecialComments.Text : string.Empty;
                        unitObj.ImageType = sImageType;
                        unitObj.ShortWebUrl = sShortUrl;
                        unitObj.IsBackgroundCheck = bIsCheck;

                        //  new ResidentialUnitDa().Insert(unitObj);

                        if (new ResidentialUnitDa(true, false).Insert(unitObj))
                        {
                            Utility.RunCMD(sql);
                            Utility.RunCMDMain(sql);
                        }

                    }
                }
                catch (Exception ex)
                {

                }


                try
                {
                    var obj = new ResidentialUnitSpecs();
                    obj = SetDataForSpecs(obj);

                    if (Session["ResidentialUnitSpecsId"] != null ||
                        !string.IsNullOrEmpty(Session["ResidentialUnitSpecsId"]?.ToString()))
                    {
                        if (new ResidentialUnitDa(true).UpdateSpecs(obj))
                        {
                            Session["ResidentialUnitSpecsId"] = null;
                            Session["serial"] = null;
                            FillResidentialUnit();
                            FillSpecsGrid();
                            ClearSpecControls();
                            Utility.DisplayMsg("Specs updated successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Specs not updated!", this);
                        }
                    }
                    else
                    {
                        if (new ResidentialUnitDa(true, false).InsertSpecs(obj))
                        {
                            Session["ResidentialUnitSpecsId"] = null;
                            FillResidentialUnit();
                            FillSpecsGrid();
                            ClearSpecControls();
                            Utility.DisplayMsg("Specs saved successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Specs not saved!", this);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                Utility.DisplayMsg(_errStr, this);
            }

        }

        protected void btnEditSpecs_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            if (row != null)
            {
                //var hdId = (Label)gvResidentialUnitSpecs.Rows[row.RowIndex].FindControl("lblSpecsId");

                //if (!string.IsNullOrEmpty(hdId.Text))
                //    FillControlsSpecs(Convert.ToInt32(hdId.Text));
            }
        }

        protected void btnDeleteSpecs_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            //if (row != null)
            //{
            //   // var hdId = (Label)gvResidentialUnitSpecs.Rows[row.RowIndex].FindControl("lblSpecsId");

            //    if (!string.IsNullOrEmpty(hdId.Text))
            //        if (new ResidentialUnitDa().DeleteSpecById(Convert.ToInt32(hdId.Text)))
            //        {
            //            Utility.DisplayMsg("Unit spec deleted successfully!", this);
            //            Session["ResidentialUnitSpecsId"] = null;
            //            ClearSpecControls();
            //            FillSpecsGrid();
            //        }
            //}
        }

        private void ClearSpecControls()
        {
            txtTotalSize.Text = string.Empty;
            txtNumberfloors.Text = string.Empty;
            txtMonthlyRentAmount.Text = string.Empty;
            txtLotSize.Text = string.Empty;
            // txtApplicationFee.Text = string.Empty;
            txtAgentName.Text = string.Empty;
            txtAgentPhoneNumber.Text = string.Empty;
            txtBroker.Text = string.Empty;
            txtBrokerPhoneNumber.Text = string.Empty;
            ddlNumberofBaths.SelectedValue = "-1";
            ddlNumberBedrooms.SelectedValue = "-1";
            chkIncludedStatement.Checked = false;
            Session["serial"] = "";
            txtSpecialStatementBtm.Text = string.Empty;
            txtShortWebUrl.Text = string.Empty;
            rdoUnitRentType.SelectedValue = null;
            txtAdvertised.Text = string.Empty;
        }

        private ResidentialUnitSpecs SetDataForSpecs(ResidentialUnitSpecs obj)
        {
            if (Session["ResidentialUnitSpecsId"] != null && Convert.ToInt32(Session["ResidentialUnitSpecsId"]) > 0)
                obj.Id = Convert.ToInt32(Session["ResidentialUnitSpecsId"].ToString());
            obj.ResidentialUnitSerialId = !string.IsNullOrEmpty(txtUnitID.Text.ToString()) ? txtUnitID.Text.ToString() : Session["ResidentialUnitSerial"].ToString();
            obj.UnitSize = !string.IsNullOrEmpty(txtTotalSize.Text) ? Convert.ToDecimal(txtTotalSize.Text.ToString()) : 0;
            obj.UnitNumberFloors = !string.IsNullOrEmpty(txtNumberfloors.Text) ? Convert.ToInt32(txtNumberfloors.Text.ToString()) : 0;
            obj.UnitMonthlyRent = !string.IsNullOrEmpty(txtMonthlyRentAmount.Text) ? Convert.ToDecimal(txtMonthlyRentAmount.Text.ToString()) : 0;
            obj.UnitLotSize = !string.IsNullOrEmpty(txtTotalSize.Text) ? Convert.ToInt32(txtTotalSize.Text.ToString()) : 0;
            obj.UnitApplicationFee = !string.IsNullOrEmpty(txtApplicationFee.Text) ? Convert.ToDecimal(txtApplicationFee.Text.ToString()) : 0;
            obj.UnitAgentName = !string.IsNullOrEmpty(txtAgentName.Text) ? txtAgentName.Text : string.Empty;
            obj.UnitAgentPhone = !string.IsNullOrEmpty(txtAgentPhoneNumber.Text) ? txtAgentPhoneNumber.Text : string.Empty;
            obj.UnitBrokerName = !string.IsNullOrEmpty(txtBroker.Text) ? txtBroker.Text : string.Empty;
            obj.UnitBrokerPhone = !string.IsNullOrEmpty(txtBrokerPhoneNumber.Text) ? txtBrokerPhoneNumber.Text : string.Empty;

            obj.UnitNumberOfBaths = Convert.ToInt32(ddlNumberofBaths.SelectedValue) > 0 ? Convert.ToInt32(ddlNumberofBaths.SelectedValue) : -1;
            obj.UnitNumberBedrooms = Convert.ToDecimal(ddlNumberBedrooms.SelectedValue) > 0 ? Convert.ToInt32(ddlNumberBedrooms.SelectedValue) : -1;

            obj.IsUnitIncludeCardProcessCharge = chkIncludedStatement.Checked;

            if (Session["ResidentialUnitSpecsId"] != null)
            {
                if (Session["serial"].ToString() != "")
                {
                    obj.Serial = Session["serial"].ToString();
                }

                obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                obj.UpdatedDate = DateTime.Now;
            }
            else
            {
                obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnitSpecs");
                obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                obj.CreatedDate = DateTime.Now;
            }

            obj.UnitSpecialStatements = !string.IsNullOrEmpty(txtSpecialStatementBtm.Text) ? txtSpecialStatementBtm.Text : string.Empty;
            obj.AdvertisedFee = !string.IsNullOrEmpty(txtAdvertised.Text) ? Convert.ToDecimal(txtAdvertised.Text.ToString()) : 0;

            return obj;
        }

        //Webpage and Image

        protected void btnSaveWebPagImage_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnUnitImageUpload_Click(object sender, EventArgs e)
        {
            if (Session["ResidentialUnitSerial"] == null &&
                string.IsNullOrEmpty(Session["ResidentialUnitSerial"]?.ToString()))
                return;
            var obj = new ResidentialUnitWebImage();
            SetDataForWebPageImage(obj);

            if (Session["ResidentialUnitWebpageId"] != null ||
                !string.IsNullOrEmpty(Session["ResidentialUnitWebpageId"]?.ToString()))
            {
                if (new ResidentialUnitDa().UpdateWebImage(obj))
                {
                    Session["ResidentialUnitWebpageId"] = null;
                    //Utility.DisplayMsg("Web Page updated successfully!", this);
                }
                else
                {
                    Utility.DisplayMsg("Specs not updated!", this);
                }
            }
            else
            {
                if (new ResidentialUnitDa().InsertWebImage(obj))
                {
                    Session["ResidentialUnitWebpageId"] = null;
                    //Utility.DisplayMsg("Web Page saved successfully!", this);
                }
                else
                {
                    Utility.DisplayMsg("Specs not saved!", this);
                }
            }

            //foreach (var file in fileUnitImageUpload.PostedFiles)
            //{
            //    string sfile = file.FileName;
            //    string uploadPath = "~/Uploads/Images";
            //    if (!Directory.Exists(uploadPath))
            //        Directory.CreateDirectory(uploadPath);
            //    string spath = Server.MapPath(uploadPath);
            //    string sfullpath = Path.Combine(spath, sfile);

            //    file.SaveAs(sfullpath);
            //    ////////////////////////////////////////////////////////////////////////////

            //    var objChild = new ResidentialUnitWebImageChild
            //    {
            //        ImagePath = sfullpath,
            //        ResidentialUnitSerialId = obj.ResidentialUnitSerialId,
            //        ResidentialUnitWebImageSerialId = obj.Serial
            //    };

            //    if (Session["ResidentialUnitWebImageChildId"] != null ||
            //        !string.IsNullOrEmpty(Session["ResidentialUnitWebImageChildId"]?.ToString()))
            //    {
            //        objChild.Id = Convert.ToInt32(Session["ResidentialUnitWebImageChildId"]?.ToString());
            //    }

            //    if (Session["ResidentialUnitWebImageChildId"] != null ||
            //        !string.IsNullOrEmpty(Session["ResidentialUnitWebImageChildId"]?.ToString()))
            //    {
            //        if (new ResidentialUnitDa().UpdateWebImageChild(objChild))
            //        {
            //            Session["ResidentialUnitWebImageChildId"] = null;
            //            Utility.DisplayMsg("Web Page updated successfully!", this);
            //        }
            //        else
            //        {
            //            Utility.DisplayMsg("Web Pages not updated!", this);
            //        }
            //    }
            //    else
            //    {
            //        if (new ResidentialUnitDa().InsertWebImageChild(objChild))
            //        {
            //            Session["ResidentialUnitWebImageChildId"] = null;
            //            //Utility.DisplayMsg("Web Page saved successfully!", this);
            //        }
            //        else
            //        {
            //            Utility.DisplayMsg("Web Page not saved!", this);
            //        }
            //    }
            //}
            // FillWebPageImageGrid();
            ClearWebPageControls();
        }

        protected void btnDeleteImageVideo_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnEditImageVideo_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void FillControlForWebPageImage(int nId)
        {
            if (nId <= 0) return;
            var obj = new ResidentialUnitDa().GetWebpagebyId(nId);
            if (obj == null) return;
            Session["ResidentialUnitWebpageId"] = null;
            Session["ResidentialUnitWebpageId"] = obj.Id;
            Session["ResidentialUnitSerial"] = null;
            Session["ResidentialUnitSerial"] = obj.Serial;
            txtTitleCaption.Text = !string.IsNullOrEmpty(obj.TitleCaption) ? obj.TitleCaption : string.Empty;
            txtShortDescription.Text = !string.IsNullOrEmpty(obj.ShortDescription) ? obj.ShortDescription : string.Empty;
            txtLongDescription.Text = !string.IsNullOrEmpty(obj.LongDescription) ? obj.LongDescription : string.Empty;
            // btnSaveWebPagImage.Text = "Update Web Page";
            FillImagesListForWebPage(obj.Serial);
        }
        private void FillImagesListForWebPage(string serial)
        {
            if (!string.IsNullOrEmpty(serial))
            {
                dtlImageVideo.DataSource = new ResidentialUnitDa().GetWebpageImagesbySerial(serial);
                dtlImageVideo.DataBind();
            }
            else
            {
                dtlImageVideo.DataSource = null;
                dtlImageVideo.DataBind();
            }
        }
        private void ClearWebPageControls()
        {
            Session["ResidentialUnitWebpageId"] = null;
            txtTitleCaption.Text = string.Empty;
            txtShortDescription.Text = string.Empty;
            txtLongDescription.Text = string.Empty;
            rdoRentType.SelectedValue = null;
            // btnSaveWebPagImage.Text = "Create Web Page";
            FillImagesListForWebPage(null);
        }
        private ResidentialUnitWebImage SetDataForWebPageImage(ResidentialUnitWebImage obj)
        {
            if (Session["ResidentialUnitWebpageId"] != null && Convert.ToInt32(Session["ResidentialUnitWebpageId"]) > 0)
                obj.Id = Convert.ToInt32(Session["ResidentialUnitWebpageId"].ToString());
            obj.ResidentialUnitSerialId = Session["ResidentialUnitSerial"].ToString();
            obj.TitleCaption = !string.IsNullOrEmpty(txtTitleCaption.Text) ? txtTitleCaption.Text : string.Empty;
            obj.ShortDescription = !string.IsNullOrEmpty(txtShortDescription.Text) ? txtShortDescription.Text : string.Empty;
            obj.LongDescription = !string.IsNullOrEmpty(txtLongDescription.Text) ? txtLongDescription.Text : string.Empty;

            if (rdoRentType.Items[0].Selected == true)
            {
                obj.ImageType = "ForRent";
            }
            else if (rdoRentType.Items[1].Selected == true)
            {
                obj.ImageType = "Rented";
            }

            obj.IsUsed = true;

            if (Session["ResidentialUnitWebpageId"] != null)
            {
                obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                obj.UpdatedDate = DateTime.Now;
            }
            else
            {
                obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnitWebImage");
                obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                obj.CreatedDate = DateTime.Now;
            }
            return obj;
        }

        [WebMethod]
        public static string test(string value, string unitSerialId, int hiddenfieldId)
        {

            ResidentialUnitSpecsChild accObj = new ResidentialUnitSpecsChild();
            var obj = new ResidentialUnitSpecsChild();
            if (hiddenfieldId > 0)
            {
                obj.Id = hiddenfieldId;
                obj.FeatureName = value;
                obj.ResidentialUnitSpecsSerialId = unitSerialId;
            }
            else
            {
                obj.FeatureName = value;
                obj.ResidentialUnitSpecsSerialId = unitSerialId;
            }


            // obj = SetFeatur(obj, value);
            if (hiddenfieldId > 0)
            {
                if (new ResidentialUnitDa().UpdateFeature(obj))
                {
                    HttpContext.Current.Session["ResidentialUnitSpecsChildId"] = null;
                    var accObj2 = (new ResidentialUnitDa().GetAllFeatureList(unitSerialId)).ToList();//.OrderByDescending(x => x.Id).FirstOrDefault();
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(accObj2);
                    return json;
                }
                else
                {
                    //Utility.DisplayMsg("Feature not updated!", this);
                }
            }
            else
            {
                if (new ResidentialUnitDa().InsertFeature(obj))
                {
                    var accObj23 = (new ResidentialUnitDa().GetAllFeatureList(unitSerialId)).ToList();//.OrderByDescending(x => x.Id).FirstOrDefault();
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(accObj23);
                    return json;

                }
                else
                {
                    // Utility.DisplayMsg("Feature not saved!", this);
                }
            }
            //FillFeaturesGrid();
            // txtFeatureName.Text = string.Empty;
            return accObj.ToString();
        }
        [WebMethod]
        public static string GetResidentialQuickFeaturesView(string unitSerialId)
        {
            HttpContext.Current.Session["ResidentialUnitSerial"] = unitSerialId;
            // List<ResidentialUnitSpecsChild> accObj = new List<ResidentialUnitSpecsChild>();
            var accObj = new ResidentialUnitDa().GetAllFeatureList(unitSerialId);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(accObj);
            return json;

        }
        //deleteFeature
        [WebMethod]
        public static bool DeleteFeatureItem(string FeatureItem)
        {
            string str = "";
            string[] deleteItemlist = FeatureItem.Trim(',').Split(',');
            try
            {
                for (int i = 0; i < deleteItemlist.Length; i++)
                {
                    new ResidentialUnitDa().DeleteSpecFeatureById(Convert.ToInt32(deleteItemlist[i]));
                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return true;

        }
        public static ResidentialUnitSpecsChild SetFeatur(ResidentialUnitSpecsChild obj, string txt)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
                Convert.ToString(HttpContext.Current.Session["ResidentialUnitSerial"]) == string.Empty)
            {
                return obj;
            }
            if (HttpContext.Current.Session["ResidentialUnitSpecsChildId"] != null && Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitSpecsChildId"]) > 0)
            {
                obj.Id = Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitSpecsChildId"].ToString());
                //Session["ResidentialUnitSerial"] = obj.Serial;

            }
            obj.FeatureName = txt;
            obj.ResidentialUnitSpecsSerialId = HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString();
            return obj;
        }

        public string ValidateSpecs()
        {
            _errStr = Validate_Control();
            if (_errStr.Length < 0)
            {
                _errStr = "";
                //txtTotalSize
                if (txtTotalSize.Text.Trim().Length <= 0)
                    _errStr += "Please enter Total Size" + Environment.NewLine;
                if (txtNumberfloors.Text.Trim().Length <= 0)
                    _errStr += "Please enter Number of floors" + Environment.NewLine;
                if (txtMonthlyRentAmount.Text.Trim().Length <= 0)
                    _errStr += "Please enter Monthly Rent Amount " + Environment.NewLine;
                if (txtLotSize.Text.Trim().Length <= 0)
                    _errStr += "Please enter Total Lot Size" + Environment.NewLine;
                if (txtApplicationFee.Text.Trim().Length <= 0)
                    _errStr += "Please enter Application Free" + Environment.NewLine;
                //if (txtAgentName.Text.Trim().Length <= 0)
                //    _errStr += "Please enter Agent Name" + Environment.NewLine;
                //if (txtAgentPhoneNumber.Text.Trim().Length <= 0)
                //    _errStr += "Please enter Agent Phone Number" + Environment.NewLine;
                //if (txtBroker.Text.Trim().Length <= 0)
                //    _errStr += "Please enter Broker" + Environment.NewLine;
                //if (txtBrokerPhoneNumber.Text.Trim().Length <= 0)
                //    _errStr += "Please enter Broker Phone number" + Environment.NewLine;
                if (ddlNumberofBaths.SelectedValue == "-1")
                    _errStr += "Please select Number of Bath " + Environment.NewLine;
                if (ddlNumberBedrooms.SelectedValue == "-1")
                    _errStr += "Please select Number of Bedrooms" + Environment.NewLine;
            }

            return _errStr;
        }

        [WebMethod(EnableSession = true)]
        public static void WebPageImage2(string savingvalues)
        {
        }

        [WebMethod(EnableSession = true)]
        public static string WebPageImage(string Image, string ImageName, string TitleCap, string ShortDesc, string longDesc)
        {

            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new ResidentialUnitWebImage();
            SetDataForWebPageImage_New(obj, Image, ImageName, TitleCap, ShortDesc, longDesc);
            try
            {
                if (new ResidentialUnitDa().InsertWebImage(obj))
                {
                    HttpContext.Current.Session["ResidentialUnitWebpageId"] = null;
                }
                obj.ImagePath = "../../Uploads/Images/" + obj.ImageName;
                //"..\..\Uploads\Images\images(2).jpg"
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(obj);
                return json;
            }
            catch (Exception)
            {

                return "";
            }
        }

        private static ResidentialUnitWebImage SetDataForWebPageImage_New(ResidentialUnitWebImage obj, string Image, string ImageName, string TitleCap, string ShortDesc, string longDesc)
        {
            if (HttpContext.Current.Session["ResidentialUnitWebpageId"] != null && Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitWebpageId"]) > 0)
                obj.Id = Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitWebpageId"].ToString());
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            obj.TitleCaption = !string.IsNullOrEmpty(TitleCap) ? TitleCap : string.Empty;
            obj.ShortDescription = !string.IsNullOrEmpty(ShortDesc) ? ShortDesc : string.Empty;
            obj.LongDescription = !string.IsNullOrEmpty(longDesc) ? longDesc : string.Empty;

            byte[] getImageData = Convert.FromBase64String(Image);
            string sfile = ImageName;
            string direc = "~/Uploads/";
            string uploadPath = "~/Uploads/Images";
            var filepath = HttpContext.Current.Server.MapPath(uploadPath);
            if (!Directory.Exists(filepath))
            {
                string spath = HttpContext.Current.Server.MapPath(direc);
                DirectorySecurity ds = Directory.GetAccessControl(spath);
                ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(spath, ds);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadPath));
            }

            string sfullpath = Path.Combine(filepath, sfile);
            if (!File.Exists(sfullpath))
            {
                File.WriteAllBytes(sfullpath, getImageData);
            }
            else
            {
                if (File.Exists(sfullpath))
                {
                    File.Delete(sfullpath);
                    File.WriteAllBytes(sfullpath, getImageData);
                }
            }

            obj.ImageName = ImageName;
            obj.ImagePath = sfullpath;
            if (HttpContext.Current.Session["ResidentialUnitWebpageId"] != null)
            {
                obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                obj.UpdatedDate = DateTime.Now;
            }
            else
            {
                obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnitWebImage");
                obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                obj.CreatedDate = DateTime.Now;
            }
            return obj;
        }

        [WebMethod]
        public static string GetAllWebImageByUnitId(string unitSerialId)
        {
            HttpContext.Current.Session["ResidentialUnitSerial"] = unitSerialId;
            List<ResidentialUnitWebImage> accObj = new List<ResidentialUnitWebImage>();
            List<ResidentialUnitWebImage> lstWebImg = new List<ResidentialUnitWebImage>();
            accObj = new ResidentialUnitDa().GetAllWebList(unitSerialId);
            foreach (ResidentialUnitWebImage a in accObj)
            {
                ResidentialUnitWebImage obj = new ResidentialUnitWebImage();
                obj.ResidentialUnitSerialId = a.ResidentialUnitSerialId;
                obj.Serial = a.Serial;
                obj.IsUsed = (a.IsUsed != null && a.IsUsed == true) ? true : false;
                obj.IsDelete = a.IsDelete;
                obj.ImageName = a.ImageName;
                obj.ImagePath = "../../Uploads/Images/" + obj.ImageName;

                lstWebImg.Add(obj);
            }
            //accObj.ImagePath = "../../Uploads/Images/" + obj.ImageName;
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstWebImg);
            return json;

        }

        [WebMethod]
        public static string DeleteWebImage(string serial, string unitid)
        {
            if (serial != "" && unitid != "")
            {
                //Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id)
                if (new ResidentialUnitDa().DeleteWebImage(serial, unitid,
                    Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id)))
                {
                    List<ResidentialUnitWebImage> accObj = new List<ResidentialUnitWebImage>();
                    List<ResidentialUnitWebImage> lstWebImg = new List<ResidentialUnitWebImage>();
                    accObj = new ResidentialUnitDa().GetAllWebList(unitid);
                    foreach (ResidentialUnitWebImage a in accObj)
                    {
                        ResidentialUnitWebImage obj = new ResidentialUnitWebImage();
                        obj.ResidentialUnitSerialId = a.ResidentialUnitSerialId;
                        obj.Serial = a.Serial;
                        obj.IsUsed = a.IsUsed == true ? true : false;
                        obj.IsDelete = a.IsDelete;
                        obj.ImageName = a.ImageName;
                        obj.ImagePath = "../../Uploads/Images/" + obj.ImageName;

                        lstWebImg.Add(obj);
                    }
                    //accObj.ImagePath = "../../Uploads/Images/" + obj.ImageName;
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(lstWebImg);
                    return json;
                }

            }

            return "";
        }
        [WebMethod]
        public static string WebPageVideo(string vFile)
        {
            var str = "";

            return str;

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //string dfd  = FileUpload1.PostedFiles.FileName;
            if (FileUpload1.PostedFile != null)
            {
                try
                {

                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string direc = "~/Uploads/";
                    string uploadPath = "~/Uploads/Video/";
                    var filepath = Server.MapPath(uploadPath);
                    if (!Directory.Exists(filepath))
                    {
                        string spath = Server.MapPath(direc);
                        DirectorySecurity ds = Directory.GetAccessControl(spath);
                        ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                        Directory.SetAccessControl(spath, ds);
                        Directory.CreateDirectory(Server.MapPath(uploadPath));
                    }
                    string sfullpath = Path.Combine(filepath, FileName);
                    if (!File.Exists(sfullpath))
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath(uploadPath + FileName));
                    }
                    else
                    {
                        if (File.Exists(sfullpath))
                        {
                            File.Delete(sfullpath);
                            FileUpload1.PostedFile.SaveAs(Server.MapPath(uploadPath + FileName));
                        }

                    }

                    var obj = new ResidentialUnitWebImageVideo();
                    obj.ResidentialUnitSerialId = Session["ResidentialUnitSerial"].ToString();
                    obj.VideoName = FileName;
                    obj.VideoPath = sfullpath;
                    obj.LongDescription = txtLoggDescriptionVideo.Text.ToString().Trim();
                    obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnitWebImageVideo");
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;

                    if (new ResidentialUnitDa().InsertWebImageVideo(obj))
                    {
                        //Session["ResidentialUnitSerial"]?.ToString()
                        BindVideo(Session["ResidentialUnitSerial"]?.ToString());
                        txtLoggDescriptionVideo.Text = "";
                        FileUpload1.Dispose();
                        //FileUpload1.PostedFile.InputStream.Dispose();
                        // FileUpload1.PostedFile == null;
                    }
                }
                catch (Exception ex)
                {
                    //lblMessage.Text = "Your file not uploaded";
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        public void BindVideo(string unitId)
        {
            var recidentialVideo = new List<ResidentialUnitWebImageVideo>();
            var recidentialVideoNew = new List<ResidentialUnitWebImageVideo>();
            recidentialVideo = new ResidentialUnitDa().GetAllvideo(Session["ResidentialUnitSerial"]?.ToString());
            foreach (ResidentialUnitWebImageVideo a in recidentialVideo)
            {
                var obj = new ResidentialUnitWebImageVideo();
                obj.ResidentialUnitSerialId = a.ResidentialUnitSerialId;
                obj.VideoName = a.VideoName;
                obj.VideoPath = "../../Uploads/Video/" + obj.VideoName;
                recidentialVideoNew.Add(obj);
            }
            gvVideo.DataSource = recidentialVideoNew;
            gvVideo.DataBind();
        }
        [WebMethod]
        public static string CreateWebPage(string updateIsUse)
        {
            string str = "";
            string[] isUsedListlist = updateIsUse.Trim(',').Split(',');
            var ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            var lstOfWebImage = new List<ResidentialUnitWebImage>();
            lstOfWebImage = new ResidentialUnitDa().GetAllWebpagesList(ResidentialUnitSerialId);
            var NewListOfwebImage = new List<ResidentialUnitWebImage>();
            try
            {
                for (int i = 0; i < isUsedListlist.Length; i++)
                {
                    var obj = new ResidentialUnitWebImage();
                    obj =
                        lstOfWebImage.FirstOrDefault(
                            x => x.Serial == isUsedListlist[i] && x.ResidentialUnitSerialId == ResidentialUnitSerialId);
                    obj.IsUsed = true;
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                    NewListOfwebImage.Add(obj);
                    //new ResidentialUnitDa().DeleteSpecFeatureById(isUsedListlist[i],);
                }
                if (new ResidentialUnitDa().UpdateWebImageIsUseToFalseByUnitId(ResidentialUnitSerialId))
                {
                    if (new ResidentialUnitDa().UpdateResidentialUnitWebImageBulk(NewListOfwebImage))
                    {
                        return "true";
                    }
                }
            }
            catch (Exception ex)
            {

                return "false";
            }

            return "true";
        }
        [WebMethod]
        public static string GetCountry()
        {
            var lstOfComboData = new List<ComboData>();
            var lstOfCountry = new ResidentialUnitDa().GetCountrlList();
            foreach (RefCountries aCountry in lstOfCountry)
            {
                ComboData c = new ComboData();
                c.Data = aCountry.COUNTRYNAME;
                c.Id2 = aCountry.COUNTRY;
                lstOfComboData.Add(c);

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstOfComboData);
            return json;
        }
        [WebMethod]
        public static string GetState()
        {
            var lstOfComboData = new List<ComboData>();
            var lstOfStates = new ResidentialUnitDa().GetStateList();
            foreach (RefStates aStates in lstOfStates)
            {
                ComboData c = new ComboData();
                c.Data = aStates.STATENAME;
                c.Id2 = aStates.STATE;
                lstOfComboData.Add(c);

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstOfComboData);
            return json;
        }
        [WebMethod]
        public static string GetCity()
        {
            var lstOfComboData = new List<ComboData>();
            var lstOfCity = new ResidentialUnitDa().GetCityList();
            foreach (Cities aCity in lstOfCity)
            {
                ComboData c = new ComboData();
                c.Id = aCity.Id;
                c.Data = aCity.city;
                c.Id2 = aCity.state_code;
                lstOfComboData.Add(c);

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstOfComboData);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string WebPageImageEquipment(string Image, string ImageName, string PicDesc)
        {

            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new ResidentialUnitEquipmentImage();
            SetDataForWebPageImage_Eq(obj, Image, ImageName, PicDesc);
            try
            {
                if (new ResidentialUnitDa().InsertEquipmentImage(obj))
                {
                    HttpContext.Current.Session["ResidentialUnitWebpageId"] = null;
                }
                obj.ImagePath = "../../Uploads/Images/" + obj.ImageName;
                obj.Description = PicDesc;
                //"..\..\Uploads\Images\images(2).jpg"
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(obj);
                return json;
            }
            catch (Exception)
            {

                return "";
            }
        }

        public static ResidentialUnitEquipmentImage SetDataForWebPageImage_Eq(ResidentialUnitEquipmentImage obj, string Image, string ImageName, string PicDesc)
        {
            if (HttpContext.Current.Session["ResidentialUnitWebpageId"] != null && Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitWebpageId"]) > 0)
                obj.Id = Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitWebpageId"].ToString());
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            obj.Description = !string.IsNullOrEmpty(PicDesc) ? PicDesc : string.Empty;

            byte[] getImageData = Convert.FromBase64String(Image);
            string sfile = ImageName;
            string direc = "~/Uploads/";
            string uploadPath = "~/Uploads/Images";
            var filepath = HttpContext.Current.Server.MapPath(uploadPath);

            if (!Directory.Exists(filepath))
            {
                string spath = HttpContext.Current.Server.MapPath(direc);
                DirectorySecurity ds = Directory.GetAccessControl(spath);
                ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(spath, ds);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadPath));
            }

            string sfullpath = Path.Combine(filepath, sfile);
            if (!File.Exists(sfullpath))
            {
                File.WriteAllBytes(sfullpath, getImageData);
            }
            else
            {
                if (File.Exists(sfullpath))
                {
                    File.Delete(sfullpath);
                    File.WriteAllBytes(sfullpath, getImageData);
                }
            }

            obj.ImageName = ImageName;
            obj.ImagePath = sfullpath;
            if (HttpContext.Current.Session["ResidentialUnitWebpageId"] != null)
            {
                obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                obj.UpdatedDate = DateTime.Now;
            }
            else
            {
                obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialEquipmentImage");
                obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                obj.CreatedDate = DateTime.Now;
            }
            return obj;
        }
        [WebMethod(EnableSession = true)] //ResidentialUnitEquipment
        public static string SaveEquipment(ResidentialUnitEquipment obj)
        {
            try
            {
                if (obj.Id > 0)
                {
                    ResidentialUnitEquipment newObj = new ResidentialUnitDa().GetRentalEquipmentData(obj.Id);
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                    obj.CreatedBy = newObj.CreatedBy;
                    obj.CreatedDate = newObj.CreatedDate;
                    if (new ResidentialUnitDa().UpdateEquipment(obj))
                    {

                        return "true";
                    }

                }
                else
                {
                    obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialUnitEquipment");
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                    if (new ResidentialUnitDa().InsertEquipment(obj))
                    {
                        return "true";
                    }
                }
            }
            catch (Exception)
            {

                return "false";
            }


            //SetDataForWebPageImage_Eq(obj, Image, ImageName, PicDesc);
            return "false";
        }
        [WebMethod(EnableSession = true)]
        public static string LoadEquipmentData(string unitSerialId)
        {

            var VmRentalEquipment = new VmRentalEquipment();
            var ResidentialUnitSerial = HttpContext.Current.Session["ResidentialUnitSerial"];
            var lstofImage = new List<ResidentialUnitEquipmentImage>();

            var lstofImageForUI = new List<ResidentialUnitEquipmentImage>();
            if (ResidentialUnitSerial != null)
            {
                lstofImage = new ResidentialUnitDa().GetAllRentalEqImage(ResidentialUnitSerial.ToString());
                foreach (ResidentialUnitEquipmentImage aimg in lstofImage)
                {
                    ResidentialUnitEquipmentImage obj = new ResidentialUnitEquipmentImage();
                    obj.ImageName = aimg.ImageName;
                    obj.ImagePath = "../../Uploads/Images/" + obj.ImageName;
                    obj.Description = aimg.Description;
                    lstofImageForUI.Add(obj);
                }
                VmRentalEquipment.EqIage = lstofImageForUI;
                VmRentalEquipment.RentalUnit = new ResidentialUnitDa().GetAllRentalEquipmentData(ResidentialUnitSerial.ToString());
            }



            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(VmRentalEquipment);
            return json;
        }

        #region Communication
        [WebMethod(EnableSession = true)]
        public static string LoadPropertyManager(string ownerId)
        {
            var managerlst = new PropertyManagerProfileDA().GetByOwnerId(ownerId);
            var Propertymanager = new List<ComboData>();
            foreach (var aData in managerlst)
            {
                ComboData c = new ComboData();

                //    ddlPropertyManagerID.DataTextField = "FirstName";
                // ddlPropertyManagerID.DataValueField = "Serial";
                c.Id = aData.Id;
                c.Data = aData.FirstName + " " + aData.LastName;
                c.Id2 = aData.Serial;
                Propertymanager.Add(c);
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(Propertymanager);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string SaveCommunication(ResidentialCommunication obj)
        {
            try
            {
                try
                {
                    var strFromAddress = "";
                    var strToAddress = "";
                    var strOwnerSerial = "";

                    if (HttpContext.Current.Session["UserObject"] != null)
                    {
                        strFromAddress = ((UserProfile)HttpContext.Current.Session["UserObject"]).Email != null
                           ? Convert.ToString(((UserProfile)HttpContext.Current.Session["UserObject"]).Email)
                           : ConfigurationManager.AppSettings["fromAddress"];

                        strOwnerSerial = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId != null
                           ? Convert.ToString(((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId)
                           : ConfigurationManager.AppSettings["fromAddress"];
                    }

                    //SystemInformation objSystem = new SystemInformationDA().GetByOwner(strOwnerSerial);
                    //if (objSystem != null && !string.IsNullOrEmpty(objSystem.ComEmailAddress1))
                    //{
                    //    strToAddress = objSystem.ComEmailAddress1;
                    //}
                    //else
                    //{
                    //    strToAddress = ConfigurationManager.AppSettings["fromAddress"];
                    //}

                    if (obj.SendMassageTo != null && !string.IsNullOrEmpty(obj.SendMassageTo))
                    {
                        strToAddress = obj.SendMassageTo;
                    }
                    else
                    {
                        strToAddress = ConfigurationManager.AppSettings["fromAddress"];
                    }

                    var strMailServer = ConfigurationManager.AppSettings["strMailServer"];
                    var strMailUser = ConfigurationManager.AppSettings["strMailUser"];
                    var strMailPassword = ConfigurationManager.AppSettings["strMailPassword"];
                    var strMailPort = ConfigurationManager.AppSettings["strMailPort"];

                    var strBccAddress = ConfigurationManager.AppSettings["bccAddress"];

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.From = new MailAddress(strFromAddress, "Support", System.Text.Encoding.UTF8);
                    mail.To.Add(strToAddress);
                    mail.Bcc.Add(strBccAddress);
                    mail.Subject = "eProperty365 Communication " + obj.RequestType;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = "You have received message from eProperty365 Communication "; // obj.Massage.ToString();
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential(strMailUser, strMailPassword);
                    client.Port = Convert.ToInt32(strMailPort);
                    client.Host = strMailServer;
                    client.EnableSsl = true;
                    try
                    {
                        //Add this line to bypass the certificate validation
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                System.Security.Cryptography.X509Certificates.X509Chain chain,
                                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                        client.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        Exception ex2 = ex;
                        string errorMessage = string.Empty;
                        while (ex2 != null)
                        {
                            errorMessage += ex2.ToString();
                            ex2 = ex2.InnerException;
                        }

                    }
                }
                catch (Exception ex)
                {
                }

                if (obj.Id > 0)
                {
                    ResidentialCommunication newObj = new ResidentialUnitDa().GetResidentialCommunication(obj.Id);
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                    obj.CreatedBy = newObj.CreatedBy;
                    obj.CreatedDate = newObj.CreatedDate;
                    obj.Serial = newObj.Serial;
                    obj.PropertyManagerSerialId = newObj.PropertyManagerSerialId;
                    if (new ResidentialUnitDa().UpdateResidentialCommunication(obj))
                    {
                        return "true";
                    }

                }
                else
                {
                    obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialCommunication");
                    obj.PropertyManagerSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                    if (new ResidentialUnitDa().InsertResidentialUnitEquipment(obj))
                    {
                        return "true";
                    }
                }

            }
            catch (Exception ex)
            {
                return "false";
            }

            return "";
        }
        [WebMethod(EnableSession = true)]
        public static string LoadCommunication(string unitSerialId)
        {
            var lstOfResidentialCommunication = new List<uspGetCommunicationGridData_Result>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] != "" && HttpContext.Current.Session["ResidentialUnitSerial"] != null)
            {
                lstOfResidentialCommunication = new ResidentialUnitDa().LoadCommunication(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstOfResidentialCommunication);
            return json;
            //return "";
        }
        [WebMethod(EnableSession = true)]
        public static string DeleteCommunication(int obj)
        {
            if (obj > 0)
            {
                try
                {
                    ResidentialCommunication newObj = new ResidentialUnitDa().GetResidentialCommunication(obj);
                    newObj.DeletedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    newObj.DeletedDate = DateTime.Now;
                    if (new ResidentialUnitDa().UpdateResidentialCommunication(newObj))
                    {

                        return "true";
                    }
                }
                catch (Exception)
                {

                    return "false";
                }

            }
            return "false";
        }
        //Load Rental Document Grid
        //
        [WebMethod(EnableSession = true)]
        public static string LoadRentalDocument(string unitSerialId)
        {
            var ResidentialDocument = new List<ResidentalDocumentListOfRental>();
            var ResidentialRentalDocument = new List<ResidentalDocumentListOfRental>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] != "" && HttpContext.Current.Session["ResidentialUnitSerial"] != null)
            {
                ResidentialDocument = new ResidentialUnitDa().GetRentalDocumentData(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
                //ResidentialDocument = new ResidentialUnitDa().GetRentalDocumentData("100000000004");
                if (ResidentialDocument.Count > 0)
                {
                    foreach (var ResidentalDocumentListOfRental in ResidentialDocument)
                    {
                        ResidentalDocumentListOfRental obj = new ResidentalDocumentListOfRental();
                        obj.Id = ResidentalDocumentListOfRental.Id;
                        obj.DocumentDescription = ResidentalDocumentListOfRental.DocumentDescription;
                        obj.FileName = ResidentalDocumentListOfRental.FileName;
                        obj.IsViewedOrDownloaded = ResidentalDocumentListOfRental.IsViewedOrDownloaded;
                        obj.FilePath = "../../Uploads/Images/Rental/" + obj.FileName;
                        ResidentialRentalDocument.Add(obj);
                    }
                }


            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ResidentialRentalDocument);
            return json;
        }

        //
        [WebMethod(EnableSession = true)]
        public static string SaveRentalDocument(string Image, string documentDescription, string FileNameExtention, string FileName, int Id)
        {
            var lastInsertedDoc = new ResidentalDocumentListOfRental();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new ResidentalDocumentListOfRental();
            byte[] getImageData = Convert.FromBase64String(Image);
            // string sfile = FileName + "." + FileNameExtention;
            string sfile = FileName;
            string direc = "~/Uploads/";
            //  string uploadPath = "~/Uploads/Images/Rental/";
            string uploadPath = "~/Uploads/Rental/" + HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            var filepath = HttpContext.Current.Server.MapPath(uploadPath);
            if (!Directory.Exists(filepath))
            {
                string spath = HttpContext.Current.Server.MapPath(direc);
                DirectorySecurity ds = Directory.GetAccessControl(spath);
                ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(spath, ds);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadPath));
            }

            string sfullpath = Path.Combine(filepath, sfile);
            try
            {
                if (!File.Exists(sfullpath))
                {
                    File.WriteAllBytes(sfullpath, getImageData);
                }
                else
                {
                    if (File.Exists(sfullpath))
                    {
                        File.Delete(sfullpath);
                        File.WriteAllBytes(sfullpath, getImageData);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            //if (!File.Exists(sfullpath))
            //{
            //    File.WriteAllBytes(sfullpath, getImageData);
            //}
            //obj.FilePath = "Uploads/Images/Rental/"+ sfile;

            obj.FilePath = "../../Uploads/Rental/" + HttpContext.Current.Session["ResidentialUnitSerial"].ToString() + "/" + sfile;
            obj.FileName = sfile;
            obj.DocumentDescription = documentDescription;

            string username = "";
            if (HttpContext.Current.Session["UserObject"] != null)
            {
                username = ((UserProfile)HttpContext.Current.Session["UserObject"]).Username;
            }

            string SQL = " update UserProfile set HasDocuments = 1  where Username = '" + username + "' ";

            try
            {
                if (Id > 0)
                {
                    var NewRentalDocumentObject = new ResidentialUnitDa().GetRentalDocumentDataById(Id);
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                    obj.CreatedBy = NewRentalDocumentObject.CreatedBy;
                    obj.CreatedDate = NewRentalDocumentObject.CreatedDate;
                    obj.Serial = NewRentalDocumentObject.Serial;
                    obj.ResidentialUnitSerialId = NewRentalDocumentObject.ResidentialUnitSerialId;
                    //obj.IsViewedOrDownloaded = NewRentalDocumentObject.IsViewedOrDownloaded;
                    if (new ResidentialUnitDa().UpdateResidentialRentalDocument(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);

                        lastInsertedDoc = new ResidentialUnitDa().GetLastResidentialRentalDocumentId();
                    }
                }
                else
                {
                    obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialDocumentRental");
                    obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                    obj.IsViewedOrDownloaded = "-1";
                    if (new ResidentialUnitDa().InsertResidentialRentalDocument(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);

                        lastInsertedDoc = new ResidentialUnitDa().GetLastResidentialRentalDocumentId();
                    }
                }


                //"..\..\Uploads\Images\images(2).jpg"
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lastInsertedDoc);
                return json;
            }
            catch (Exception)
            {

                return "";
            }
        }

        [WebMethod(EnableSession = true)]
        public static string DeleteRentalDocument(int obj)
        {
            if (obj > 0)
            {
                try
                {
                    ResidentalDocumentListOfRental newObj = new ResidentialUnitDa().GetRentalDocumentDataById(obj);
                    newObj.DeletedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    newObj.DeletedDate = DateTime.Now;
                    if (new ResidentialUnitDa().UpdateResidentialRentalDocument(newObj))
                    {

                        return "true";
                    }
                }
                catch (Exception)
                {


                }

            }
            return "false";
        }
        [WebMethod(EnableSession = true)]
        public static string updateStatus(int id, string Status)
        {
            if (id > 0)
            {
                try
                {
                    ResidentalDocumentListOfRental newObj = new ResidentialUnitDa().GetRentalDocumentDataById(id);
                    newObj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    newObj.UpdatedDate = DateTime.Now;
                    newObj.IsViewedOrDownloaded = Status;
                    if (new ResidentialUnitDa().UpdateResidentialRentalDocument(newObj))
                    {

                        return "true";
                    }
                }
                catch (Exception)
                {


                }

            }
            return "false";
        }
        //Get Document Id
        [WebMethod(EnableSession = true)]
        public static string GetDocumentId(string ownerId)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
             string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var documrntId = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialDocument");
            return documrntId;
        }

        [WebMethod(EnableSession = true)]
        public static string LoadDocument(string unitSerialId)
        {
            var ResidentialDocumentResidentialDocument = new List<ResidentialDocument>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] != "" && HttpContext.Current.Session["ResidentialUnitSerial"] != null)
            {
                ResidentialDocumentResidentialDocument = new ResidentialUnitDa().GetDocumentData(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ResidentialDocumentResidentialDocument);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string SaveDocument(ResidentialDocument obj)
        {
            try
            {
                string username = "";
                if (HttpContext.Current.Session["UserObject"] != null)
                {
                    username = ((UserProfile)HttpContext.Current.Session["UserObject"]).Username;
                }

                string SQL = " update UserProfile set HasDocuments = 1  where Username = '" + username + "' ";

                if (obj.Id > 0)
                {
                    ResidentialDocument newObj = new ResidentialUnitDa().GetResidentialDocument(obj.Id);
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                    obj.CreatedBy = newObj.CreatedBy;
                    obj.CreatedDate = newObj.CreatedDate;
                    obj.Serial = newObj.Serial;
                    obj.PropertyManagerSerialId = newObj.PropertyManagerSerialId;
                    if (new ResidentialUnitDa().UpdateResidentialDocument(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);

                        return "true";
                    }

                }
                else
                {
                    obj.Serial = obj.DocumrntId; //new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialDocument");
                    obj.PropertyManagerSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                    if (new ResidentialUnitDa().InsertResidentialDocument(obj))
                    {
                        Utility.RunCMD(SQL);
                        Utility.RunCMDMain(SQL);

                        return "true";
                    }
                }
            }
            catch (Exception ex)
            {

                return "false";
            }
            return "";
        }

        [WebMethod(EnableSession = true)]
        public static string DeleteDocument(int obj)
        {
            if (obj > 0)
            {
                try
                {
                    ResidentialDocument newObj = new ResidentialUnitDa().GetResidentialDocument(obj);
                    newObj.DeletedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    newObj.DeletedDate = DateTime.Now;
                    if (new ResidentialUnitDa().UpdateResidentialDocument(newObj))
                    {
                        return "true";
                    }
                }
                catch (Exception)
                {

                    return "false";
                }

            }
            return "false";
        }
        #endregion
        [WebMethod(EnableSession = true)]
        #region Maintain Manager

        public static string GetMaintenanceManagerAllData(string unitSerialId)
        {
            var ResidentialMaintenenceAll = new ResidentialMaintenenceAll();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] != "" &&
                HttpContext.Current.Session["ResidentialUnitSerial"] != null)
            {
                ResidentialUnitEquipment Equipment = new ResidentialUnitDa().GetAllRentalEquipmentData(unitSerialId);
                var ResidentialMaintainesManagerImage = new ResidentialUnitDa().GetResidentialMaintainesManagerImage(unitSerialId);
                var ResidentialMaintainesManagerMaster = new ResidentialUnitDa().GetResidentialMaintainesManagerMaster(unitSerialId);
                var ListOfResidentialMaintainesManagerSchedules = new ResidentialUnitDa().GetResidentialMaintainesManagerSchedule(unitSerialId);
                var ListOfResidentialMaintainesManagerPartDatas = new ResidentialUnitDa().GetResidentialMaintainesManagerPartData(unitSerialId);
                var ListOfResidentialMaintainesManagerVandorDatas = new ResidentialUnitDa().GetResidentialMaintainesManagerVandorData(unitSerialId);
                if (ResidentialMaintainesManagerImage != null)
                {
                    ResidentialMaintainesManagerImage.ImageURL = "../../Uploads/Images/" + ResidentialMaintainesManagerImage.ImageName;
                }

                ResidentialMaintenenceAll.ResidentialUnitEquipment = Equipment;
                ResidentialMaintenenceAll.ResidentialMaintainesManagerMaster = ResidentialMaintainesManagerMaster;
                ResidentialMaintenenceAll.ResidentialMaintainesManagerImage = ResidentialMaintainesManagerImage;
                ResidentialMaintenenceAll.ListOfResidentialMaintainesManagerSchedules = ListOfResidentialMaintainesManagerSchedules;
                ResidentialMaintenenceAll.ListOfResidentialMaintainesManagerPartDatas = ListOfResidentialMaintainesManagerPartDatas;
                ResidentialMaintenenceAll.ListOfResidentialMaintainesManagerVandorDatas = ListOfResidentialMaintainesManagerVandorDatas;
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ResidentialMaintenenceAll);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string MaintenanceImageUpload(string Image, string ImageName)
        {

            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new ResidentialMaintainesManagerImage();
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

            // obj.Id = Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitWebpageId"].ToString());

            //obj.Description = !string.IsNullOrEmpty(PicDesc) ? PicDesc : string.Empty;

            byte[] getImageData = Convert.FromBase64String(Image);
            string sfile = ImageName;
            string direc = "~/Uploads/";
            string uploadPath = "~/Uploads/Images";
            var filepath = HttpContext.Current.Server.MapPath(uploadPath);
            if (!Directory.Exists(filepath))
            {
                string spath = HttpContext.Current.Server.MapPath(direc);
                DirectorySecurity ds = Directory.GetAccessControl(spath);
                ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(spath, ds);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadPath));
            }

            string sfullpath = Path.Combine(filepath, sfile);
            if (!File.Exists(sfullpath))
            {
                File.WriteAllBytes(sfullpath, getImageData);
            }

            obj.ImageName = ImageName;
            obj.ImageURL = sfullpath;
            if (HttpContext.Current.Session["ResidentialUnitWebpageId"] != null)
            {
                obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                obj.UpdatedDate = DateTime.Now;
            }
            else
            {
                obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialMaintenanceImage");
                obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                obj.CreatedDate = DateTime.Now;
            }
            try
            {
                if (new ResidentialUnitDa().InsertMaintenanceImage(obj))
                {
                    HttpContext.Current.Session["ResidentialUnitWebpageId"] = null;
                }
                obj.ImageURL = "../../Uploads/Images/" + obj.ImageName;

                //"..\..\Uploads\Images\images(2).jpg"
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(obj);
                return json;
            }
            catch (Exception ex)
            {

                return "";
            }

        }
        [WebMethod(EnableSession = true)]
        public static string SaveMaintenanceSchedule(ResidentialMaintainesManagerSchedule obj)
        {
            if (obj != null)
            {
                try
                {
                    if (obj.Id > 0)
                    {
                        ResidentialMaintainesManagerSchedule newObj = new ResidentialUnitDa().GetResidentialMaintainesManagerScheduleById(obj.Id);
                        obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                        obj.UpdatedDate = DateTime.Now;
                        obj.CreatedBy = newObj.CreatedBy;
                        obj.CreatedDate = newObj.CreatedDate;
                        obj.Serial = newObj.Serial;
                        obj.ResidentialUnitSerialId = newObj.ResidentialUnitSerialId;
                        if (new ResidentialUnitDa().UpdateResidentialMaintainesManagerSchedule(obj))
                        {

                            var newobject = new ResidentialUnitDa().GetResidentialMaintainesManagerSchedule(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
                            var jsonSerialiser = new JavaScriptSerializer();
                            var json = jsonSerialiser.Serialize(newobject);
                            return json;
                        }

                    }
                    else
                    {
                        obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialMaintenanceSchedule");
                        obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                        obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                        obj.CreatedDate = DateTime.Now;
                        if (new ResidentialUnitDa().InsertResidentialSchedule(obj))
                        {
                            var newobject = new ResidentialUnitDa().GetLastResidentialSchedule();
                            var jsonSerialiser = new JavaScriptSerializer();
                            var json = jsonSerialiser.Serialize(newobject);
                            return json;
                        }
                    }
                }
                catch (Exception)
                {



                }
            }

            return "";
        }
        [WebMethod(EnableSession = true)]
        public static string SendCote(string code, string MainTo)
        {
            var mass = "";
            //var toAddress = new ResidentialAddResponceTemplateDa().GetTenantEmailAddress((HttpContext.Current.Session["TenentId"])?.ToString());
            var strMailServer = ConfigurationManager.AppSettings["strMailServer"];
            var strMailUser = ConfigurationManager.AppSettings["strMailUser"];
            var strMailPassword = ConfigurationManager.AppSettings["strMailPassword"];
            var strMailPort = ConfigurationManager.AppSettings["strMailPort"];
            var strFromAddress = ConfigurationManager.AppSettings["fromAddress"];
            var strBccAddress = ConfigurationManager.AppSettings["bccAddress"];

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress(strFromAddress, "Support", System.Text.Encoding.UTF8);
            mail.To.Add(MainTo);
            mail.Bcc.Add(strBccAddress);
            mail.Subject = "Quote Request";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Thank You. This is your Quote :-" + code;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(strMailUser, strMailPassword);
            client.Port = Convert.ToInt32(strMailPort);
            client.Host = strMailServer;
            client.EnableSsl = true;
            try
            {
                //Add this line to bypass the certificate validation
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                client.Send(mail);
                mass = "true";
                //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                mass = "false";
                //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(mass);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string SendWinningNotice(string MainTo, string code)
        {
            var mass = "";
            //var toAddress = new ResidentialAddResponceTemplateDa().GetTenantEmailAddress((HttpContext.Current.Session["TenentId"])?.ToString());
            var strMailServer = ConfigurationManager.AppSettings["strMailServer"];
            var strMailUser = ConfigurationManager.AppSettings["strMailUser"];
            var strMailPassword = ConfigurationManager.AppSettings["strMailPassword"];
            var strMailPort = ConfigurationManager.AppSettings["strMailPort"];
            var strFromAddress = ConfigurationManager.AppSettings["fromAddress"];
            var strBccAddress = ConfigurationManager.AppSettings["bccAddress"];

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress(strFromAddress, "Support", System.Text.Encoding.UTF8);
            mail.To.Add(MainTo);
            mail.Bcc.Add(strBccAddress);
            mail.Subject = "Code Request";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Congratulation !!! You have won the quote; Quote Id: " + code;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(strMailUser, strMailPassword);
            client.Port = Convert.ToInt32(strMailPort);
            client.Host = strMailServer;
            client.EnableSsl = true;
            try
            {
                //Add this line to bypass the certificate validation
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                client.Send(mail);
                mass = "true";
                //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                mass = "false";
                //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(mass);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string DeleteMaintenanceSchedule(int obj)
        {
            if (obj > 0)
            {
                try
                {
                    ResidentialMaintainesManagerSchedule newObj = new ResidentialUnitDa().GetResidentialMaintainesManagerScheduleById(obj);
                    newObj.DeletedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    newObj.DeletedDate = DateTime.Now;
                    if (new ResidentialUnitDa().UpdateResidentialMaintainesManagerSchedule(newObj))
                    {
                        var ListOfResidentialMaintainesManagerSchedules = new ResidentialUnitDa().GetResidentialMaintainesManagerSchedule(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString());
                        var jsonSerialiser = new JavaScriptSerializer();
                        var json = jsonSerialiser.Serialize(ListOfResidentialMaintainesManagerSchedules);
                        return json;

                    }
                }
                catch (Exception)
                {

                    return "";
                }

            }
            return "";
        }

        [WebMethod(EnableSession = true)]
        public static string SetScheduleWork(List<EventManagement> Obj)
        {
            if (Obj.Count > 0)
            {
                new ResidentialUnitDa().SetSchedule(Obj);
            }
            else
            {

                return "false";
            }

            return "true";
        }
        [WebMethod(EnableSession = true)]
        public static string LoadEquipmentGrid(EventManagement Obj)
        {
            var listOfEquipment = new List<usp_GetEquipmentListByUnitId_PropertyManager_LocationId_Result>();
            try
            {
                listOfEquipment = new ResidentialUnitDa().GetEquipmentList(Obj);
            }
            catch (Exception ex)
            {


            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(listOfEquipment);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string GetSingleEquipment(EventManagement obj)
        {
            var VmRentalEquipment = new VmRentalEquipment();
            //var ResidentialUnitSerial = HttpContext.Current.Session["ResidentialUnitSerial"];
            var lstofImage = new List<ResidentialUnitEquipmentImage>();

            var lstofImageForUI = new List<ResidentialUnitEquipmentImage>();
            //  if (ResidentialUnitSerial != null)
            //  {
            lstofImage = new ResidentialUnitDa().GetAllRentalEqImage_Single(obj.Id);
            foreach (ResidentialUnitEquipmentImage aimg in lstofImage)
            {
                ResidentialUnitEquipmentImage cobj = new ResidentialUnitEquipmentImage();
                cobj.ImageName = aimg.ImageName;
                cobj.ImagePath = "../../Uploads/Images/" + cobj.ImageName;
                cobj.Description = aimg.Description;
                lstofImageForUI.Add(cobj);
            }
            VmRentalEquipment.EqIage = lstofImageForUI;
            VmRentalEquipment.RentalUnit = new ResidentialUnitDa().GetAllRentalEquipmentData_Id(obj.Id);
            // }



            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(VmRentalEquipment);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string DeleteEquipment(EventManagement obj)
        {

            try
            {
                new ResidentialUnitDa().DeleteEquipmentById(obj.Id);
            }
            catch (Exception)
            {

                return "false";
            }

            return "true";
        }
        #endregion

        #region Part
        [WebMethod(EnableSession = true)]
        public static string SavePartData(ResidentialMaintainesManagerPartData obj)
        {
            if (obj != null)
            {
                try
                {
                    if (obj.Id > 0)
                    {
                        ResidentialMaintainesManagerPartData newObj = new ResidentialUnitDa().GetPartDataById(obj.Id);
                        obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                        obj.UpdatedDate = DateTime.Now;
                        obj.CreatedBy = newObj.CreatedBy;
                        obj.CreatedDate = newObj.CreatedDate;
                        obj.Serial = newObj.Serial;
                        obj.ResidentialUnitSerialId = newObj.ResidentialUnitSerialId;
                        if (new ResidentialUnitDa().Updatepartdata(obj))
                        {

                            var newobject = new ResidentialUnitDa().GetResidentialMaintainesManagerPartData(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
                            var jsonSerialiser = new JavaScriptSerializer();
                            var json = jsonSerialiser.Serialize(newobject);
                            return json;
                        }

                    }
                    else
                    {
                        obj.Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialPart");
                        obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                        obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                        obj.CreatedDate = DateTime.Now;
                        if (new ResidentialUnitDa().InsertPart(obj))
                        {
                            var newobject = new ResidentialUnitDa().GetLastResidentialPartData();
                            var jsonSerialiser = new JavaScriptSerializer();
                            var json = jsonSerialiser.Serialize(newobject);
                            return json;
                        }
                    }
                }
                catch (Exception)
                {



                }
            }

            return "";
        }
        [WebMethod(EnableSession = true)]
        public static string DeletePart(int obj)
        {
            if (obj > 0)
            {
                try
                {
                    ResidentialMaintainesManagerPartData newObj = new ResidentialUnitDa().GetPartDataById(obj);
                    newObj.DeletedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    newObj.DeletedDate = DateTime.Now;
                    if (new ResidentialUnitDa().Updatepartdata(newObj))
                    {

                        var ListOfResidentialMaintainesManagerPartData = new ResidentialUnitDa().GetResidentialMaintainesManagerPartData(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString());
                        var jsonSerialiser = new JavaScriptSerializer();
                        var json = jsonSerialiser.Serialize(ListOfResidentialMaintainesManagerPartData);
                        return json;
                    }
                }
                catch (Exception)
                {

                    return "";
                }

            }
            return "";
        }

        #endregion

        #region Vendor
        [WebMethod(EnableSession = true)]
        public static string GetVendor(string ownerId)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var vendorlst =
                new ResidentialUnitDa().LoadVendor(ownerId);
            var lstOfComboData = new List<ComboData>();
            foreach (VendorProfile avendor in vendorlst)
            {
                ComboData c = new ComboData();
                c.Data = avendor.Title;
                c.Id = avendor.Id;
                c.Id2 = avendor.OwnerId;
                lstOfComboData.Add(c);

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstOfComboData);
            return json;

        }
        [WebMethod(EnableSession = true)]
        public static string SaveVendorData(ResidentialMaintainesManagerVandorData obj)
        {
            if (obj != null)
            {
                try
                {
                    if (obj.Id > 0)
                    {
                        ResidentialMaintainesManagerVandorData newObj = new ResidentialUnitDa().GetResidentialVendorById(obj.Id);
                        obj.UpdatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                        obj.UpdatedDate = DateTime.Now;
                        obj.CreatedBy = newObj.CreatedBy;
                        obj.CreatedDate = newObj.CreatedDate;
                        obj.Serial = newObj.Serial;
                        obj.ResidentialUnitSerialId = newObj.ResidentialUnitSerialId;


                        if (new ResidentialUnitDa().UpdateVendordata(obj))
                        {

                            var newobject = new ResidentialUnitDa().GetResidentialMaintainesManagerVandorData(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
                            var jsonSerialiser = new JavaScriptSerializer();
                            var json = jsonSerialiser.Serialize(newobject);
                            return json;
                        }

                    }
                    else
                    {
                        obj.Serial = obj.VendorId.ToString();//new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialVendor");
                        obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                        obj.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                        obj.CreatedDate = DateTime.Now;
                        if (new ResidentialUnitDa().InsertVendor(obj))
                        {
                            var newobject = new ResidentialUnitDa().GetLastResidentialVendorData();
                            var jsonSerialiser = new JavaScriptSerializer();
                            var json = jsonSerialiser.Serialize(newobject);
                            return json;
                        }
                    }
                }
                catch (Exception)
                {



                }
            }

            return "";
        }

        [WebMethod(EnableSession = true)]
        public static string DeleteVendor(int obj)
        {
            if (obj > 0)
            {
                try
                {
                    ResidentialMaintainesManagerVandorData newObj = new ResidentialUnitDa().GetResidentialVendorById(obj);
                    newObj.DeletedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                    newObj.DeletedDate = DateTime.Now;
                    if (new ResidentialUnitDa().UpdateVendordata(newObj))
                    {

                        var ListOfResidentialMaintainesManagerPartData = new ResidentialUnitDa().GetResidentialMaintainesManagerVandorData(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString());
                        var jsonSerialiser = new JavaScriptSerializer();
                        var json = jsonSerialiser.Serialize(ListOfResidentialMaintainesManagerPartData);
                        return json;
                    }
                }
                catch (Exception ex)
                {

                    return "";
                }

            }
            return "";
        }
        //DeleteVendor
        [WebMethod(EnableSession = true)]
        public static string GetVendorQuoteId(string ownerId)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
             string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var Serial = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialVendor");
            return Serial;
        }
        [WebMethod(EnableSession = true)]
        public static string GetSender()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
             string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var OwnerId = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId;
            var res = new ResidentialUnitDa().GetSenderList(OwnerId);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string GetOwnerMessage(string RequestType, string SenderId)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
             string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var OwnerId = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId;
            var UnitIId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            var res = new ResidentialUnitDa().GetOwnerMessage(OwnerId, UnitIId, RequestType, SenderId);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string SaveMessageOwner(int QuestionId, string txtData)
        {
            var res = new List<usp_GetMessage_Owner_Result>();
            if (QuestionId > 0 && txtData != "")
            {
                var OwnerId = ((UserProfile)HttpContext.Current.Session["UserObject"]).OwnerId;
                var GetCommunicationById = new ResidentialUnitDa().GetCommunicationById(QuestionId);
                var commucation = new communicationPanel();
                commucation.FromUser = OwnerId;
                commucation.ToUser = GetCommunicationById.FromUser;
                commucation.RequestType = GetCommunicationById.RequestType;
                commucation.QuestionId = QuestionId;
                commucation.isAnswered = true;
                commucation.Message = txtData;
                commucation.UnitId = GetCommunicationById.UnitId;
                commucation.CreatedBy = Convert.ToInt16(((UserProfile)HttpContext.Current.Session["UserObject"]).Id);
                commucation.CreatedDate = DateTime.Now;
                if (new ResidentialUnitDa().InsertCommunication(commucation))
                {
                    res = new ResidentialUnitDa().GetOwnerMessage(OwnerId, HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), "", "");
                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
        #endregion

        #region Web Analytics
        [WebMethod(EnableSession = true)]
        public static string GetWebAnalyticsBarChartData(WebAnalyticsModel Obj)
        {
            List<Barchart> result = new ResidentialUnitDa().GetWebAnalyticsBarChartData(Obj);

            //var VmRentalEquipment = new VmRentalEquipment();
            //var ResidentialUnitSerial = HttpContext.Current.Session["ResidentialUnitSerial"];
            //var lstofImage = new List<ResidentialUnitEquipmentImage>();

            //var lstofImageForUI = new List<ResidentialUnitEquipmentImage>();
            //if (ResidentialUnitSerial != null)
            //{
            //    lstofImage = new ResidentialUnitDa().GetAllRentalEqImage(ResidentialUnitSerial.ToString());
            //    foreach (ResidentialUnitEquipmentImage aimg in lstofImage)
            //    {
            //        ResidentialUnitEquipmentImage obj = new ResidentialUnitEquipmentImage();
            //        obj.ImageName = aimg.ImageName;
            //        obj.ImagePath = "../../Uploads/Images/" + obj.ImageName;
            //        obj.Description = aimg.Description;
            //        lstofImageForUI.Add(obj);
            //    }
            //    VmRentalEquipment.EqIage = lstofImageForUI;
            //    VmRentalEquipment.RentalUnit = new ResidentialUnitDa().GetAllRentalEquipmentData(ResidentialUnitSerial.ToString());
            //}



            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(result);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string GetWebAnalyticsData(WebAnalyticsModel Obj)
        {
            List<WebAnalyticsModel> result = new ResidentialUnitDa().GetWebAnalyticsData(Obj);

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(result);
            return json;
        }

        #endregion

        [WebMethod(EnableSession = true)]
        public static string GetAllLeaseDocument()
        {
            var url = "https://www.eproperty365.net/leases/";
            // var filepath = HttpContext.Current.Server.MapPath(uploadPath);
            var ListOfLeaseFile = new List<LeaseFile>();
            if (!Directory.Exists(url))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string html = reader.ReadToEnd();
                        Regex regex = new Regex(GetDirectoryListingRegexForUrl(url));
                        MatchCollection matches = regex.Matches(html);
                        if (matches.Count > 0)
                        {
                            foreach (Match match in matches)
                            {
                                if (match.Success)
                                {
                                    var fileFullUrl = match.Value;
                                    if (fileFullUrl.Contains("leases"))
                                    {
                                        string[] stringSeparators = new string[] { "leases" };
                                        string[] firstNames = fileFullUrl.Split(stringSeparators, StringSplitOptions.None);
                                        string[] docFile = firstNames[1].Split('/');
                                        var FileName = docFile[1];
                                        var lease = new LeaseFile();
                                        lease.FileName = FileName.Remove(FileName.Length - 1);
                                        lease.FilePath = url + lease.FileName;
                                        ListOfLeaseFile.Add(lease);

                                    }
                                }
                            }
                        }
                    }

                }
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ListOfLeaseFile);
            return json;
        }
        public static string GetDirectoryListingRegexForUrl(string url)
        {
            if (url.Equals("https://www.eproperty365.net/leases/"))
            {
                return "\\\"([^\"]*)\\\"";
            }
            throw new NotSupportedException();
        }


    }
}