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
    public partial class AddBasicData : System.Web.UI.Page
    {
        #region Method
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillContactType();
                ddlType.Enabled = true;
                if (Session["UserObject"] != null)
                {
                    Session["ChildId"] = null;
                    Session["IsLedger"] = null;
                    int nLevel = 0;
                    try
                    {
                        nLevel = Convert.ToInt32(Request.QueryString["Level"].ToString());
                    }
                    catch (Exception ex)
                    {
                        nLevel = 0;
                    }
                    if (nLevel == 1)
                    {
                        Session["IsLedger"] = true;
                        ddlType.SelectedValue = Convert.ToInt32(EnumGlobalData.Ledger).ToString();
                        ddlType.Enabled = false;
                        FillGrid();
                    }

                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["ChildId"].ToString());
                    }
                    catch (Exception ex)
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                        lblHeadline.InnerText = "Edit";
                        Session["ChildId"] = CId;
                        FillControls(Convert.ToInt32(Session["ChildId"].ToString()));
                    }
                }
            }
        }
        private void ClearControls()
        {
            txtContactName.Text = "";
            txtCode.Text = "";
            btnSave.Text = "Add";
            lblHeadline.InnerText = "Add";
        }
        private void FillContactType()
        {
            try
            {

                ddlType.Items.Clear();
                ddlType.AppendDataBoundItems = true;
                ddlType.Items.Add(new ListItem("Select Type", "-1"));
                ddlType.SelectedValue = "-1";

                ddlType.DataSource = Utility.GetAll<EnumGlobalData>();
                ddlType.DataTextField = "Value";
                ddlType.DataValueField = "Key";
                ddlType.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        private Child SetData(Child objChild)
        {
            objChild = new Child();

            if (Session["ChildId"] != null && Convert.ToInt32(Session["ChildId"]) > 0)
            {
                objChild.Id = Convert.ToInt32(Session["ChildId"].ToString());
            }

            if ((!string.IsNullOrEmpty(txtContactName.Text.ToString())) && (txtContactName.Text.ToString() != string.Empty))
            {
                objChild.Description = txtContactName.Text.ToString();
            }
            else
            {
                objChild.Description = "";
            }
            if ((!string.IsNullOrEmpty(txtCode.Text.ToString())) && (txtCode.Text.ToString() != string.Empty))
            {
                objChild.Code = txtCode.Text.ToString();
            }
            else
            {
                objChild.Code = "";
            }

            if (ddlType.SelectedValue != "-1")
            {
                objChild.UserDefinedId = Convert.ToInt32(ddlType.SelectedValue);

                Master objParent = new MasterDA().GetParentbyUserDefinedID(Convert.ToInt32(ddlType.SelectedValue));
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

            return objChild;
        }
        private void FillControls(int nId)
        {
            try
            {
                if (nId > 0)
                {
                    Child obj = new ChildDA().GetChildbyID(nId);
                    if (obj != null)
                    {
                        Session["ChildId"] = obj.Id;
                        if (obj.Description != null && obj.Description.ToString() != string.Empty)
                        {
                            txtContactName.Text = obj.Description;
                        }
                        else
                        {
                            txtContactName.Text = "";
                        }
                        if (obj.Code != null && obj.Code.ToString() != string.Empty)
                        {
                            txtCode.Text = obj.Code;
                        }
                        else
                        {
                            txtCode.Text = "";
                        }

                        btnSave.Text = "Update";

                    }
                }
            }
            catch
            {

            }
        }

        private void FillGrid()
        {
            try
            {
                List<Child> objChild = null;
                if (ddlType.SelectedValue != "-1")
                {
                    lblHeadline.InnerText = "Add  " + (ddlType.SelectedItem).Text;

                    objChild = new ChildDA().GetChildByParentID(Convert.ToInt32(ddlType.SelectedValue));
                }

                gvDataList.DataSource = objChild;
                gvDataList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion      
        #region Events

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Child obj = new Child();
                obj = SetData(obj);

                string username = "";

                if (Session["UserObject"] != null)
                {
                    username = ((UserProfile)Session["UserObject"]).Username;
                }

                string SQL = " update UserProfile set HasLedgerCode = 1  where Username = '" + username + "' ";               

                if (Session["ChildId"] == null || Session["ChildId"] == "0")
                {
                    if (new ChildDA().Insert(obj))
                    {
                        if (Session["IsLedger"] != null)
                        {
                            if (Convert.ToBoolean(Session["IsLedger"]) == true)
                            {
                                Utility.RunCMD(SQL);
                                Utility.RunCMDMain(SQL);
                            }
                        }

                        Session["ContactId"] = null;
                        ClearControls();
                        FillGrid();
                        Utility.DisplayMsg("Data saved successfully!", this);
                    }
                    else
                    {
                        Utility.DisplayMsg("Data not saved!", this);
                    }
                }
                else
                {
                    if (new ChildDA().Update(obj))
                    {
                        if (Session["IsLedger"] != null)
                        {
                            if (Convert.ToBoolean(Session["IsLedger"]) == true)
                            {
                                Utility.RunCMD(SQL);
                                Utility.RunCMDMain(SQL);
                            }
                        }

                        Session["ChildId"] = null;
                        ClearControls();
                        FillGrid();
                        Utility.DisplayMsg("Data updated successfully!", this);
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
       
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvDataList.Rows[row.RowIndex].FindControl("lblId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new ChildDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillGrid();
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvDataList.Rows[row.RowIndex].FindControl("lblId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                FillControls(Convert.ToInt32(hdId.Text));
            }
        }
        protected void gvDataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDataList.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void gvDataList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillGrid();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedIndex > 0)
            {
                FillGrid();
            }
        }

        #endregion
    }
}