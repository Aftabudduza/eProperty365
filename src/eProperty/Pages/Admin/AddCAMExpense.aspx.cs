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
    public partial class AddCAMExpense : System.Web.UI.Page
    {
        public string sUrl = string.Empty;

        #region Events      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillddlControlsCAM();
                FillCAMs();
                if ((Session["UserObject"] != null))
                {
                    Session["CAMUrl"] = null;

                    try
                    {
                        if (Request.QueryString["R"] != null)
                        {
                            sUrl = Request.QueryString["R"].ToString();
                            Session["CAMUrl"] = sUrl.ToLower();
                        }
                    }
                    catch
                    {
                        sUrl = "";
                    }

                    Session["CAMId"] = null;
                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["CAMId"].ToString());
                    }
                    catch
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                        lblHeadline.InnerText = "Edit Expense";
                        Session["CAMId"] = CId;
                        FillControlsCAM(Convert.ToInt32(Session["CAMId"].ToString()));
                    }
                }
            }
        }
        protected void btnSubmitCAM_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["CAMUrl"] != null)
                {
                    sUrl = Session["CAMUrl"].ToString();
                }

                CAMExpense obj = new CAMExpense();
                obj = SetDataCAM(obj);

                if (Session["CAMId"] == null || Session["CAMId"] == "0")
                {                    
                    if (new CAMExpenseDA().Insert(obj))
                    {
                        Session["CAMId"] = null;
                        ClearControlsCAM();
                        FillCAMs();
                        //  Utility.DisplayMsgAndRedirect("CAM Expense saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                        // Utility.DisplayMsg("CAM Expense saved successfully!", this);

                        if (sUrl != string.Empty)
                        {
                            if (sUrl.Trim() == "location")
                            {
                                Utility.DisplayMsgAndRedirect("CAM Expense saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                            }
                            else
                            {
                                Utility.DisplayMsg("CAM Expense saved successfully!", this);
                            }
                        }
                        else
                        {
                            Utility.DisplayMsg("CAM Expense saved successfully!", this);
                        }

                    }
                    else
                    {
                        Utility.DisplayMsg("CAM Expense not saved!", this);
                    }
                }
                else
                {                    
                    if (new CAMExpenseDA().Update(obj))
                    {
                      //  Session["CAMId"] = null;
                        ClearControlsCAM();
                        FillCAMs();
                        // Utility.DisplayMsgAndRedirect("CAM Expense updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                        //Utility.DisplayMsg("CAM Expense updated successfully!", this);    

                        if (sUrl != string.Empty)
                        {
                            if (sUrl.Trim() == "location")
                            {
                                Utility.DisplayMsgAndRedirect("CAM Expense updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                            }
                            else
                            {
                                Utility.DisplayMsg("CAM Expense updated successfully!", this);
                            }
                        }
                        else
                        {
                            Utility.DisplayMsg("CAM Expense updated successfully!", this);
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("CAM Expense not updated!", this);
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }
        protected void btnCloseCAM_Click(object sender, EventArgs e)
        {
            ClearControlsCAM();
        }
        protected void btnsearchCAM_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CAMSearch"] = null;
                if (txtFromDate.Text.ToString().Trim() != string.Empty && txtToDate.Text.ToString().Trim() != string.Empty)
                {
                    List<CAMExpense> obj = new CAMExpenseDA().GetBySearch(Convert.ToDateTime(txtFromDate.Text.ToString().Trim()), Convert.ToDateTime(txtFromDate.Text.ToString().Trim()));
                    gvCAMList.DataSource = obj;
                    gvCAMList.DataBind();
                }
            }
            catch (Exception ex) { }
        }
        protected void btnDeleteCAM_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvCAMList.Rows[row.RowIndex].FindControl("lblCAMId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new CAMExpenseDA().DeleteByID(Convert.ToInt32(hdId.Text)))
                {
                    FillCAMs();
                }
            }
        }
        protected void btnEditCAM_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvCAMList.Rows[row.RowIndex].FindControl("lblCAMId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                FillControlsCAM(Convert.ToInt32(hdId.Text));
            }
        }
        protected void gvCAMList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCAMList.PageIndex = e.NewPageIndex;
            FillCAMs();
        }
        protected void gvCAMList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillCAMs();
        }
        #endregion
        #region Method

        private void ClearControlsCAM()
        {
            txtName.Text = "";
            txtExpenseType.Text = "";
            txtNumber.Text = "";
            txtCheckNo.Text = "";
            btnSaveCAM.Text = "Add Expense Contact";
            lblHeadline.InnerText = "Add Expense Contact";
        }
        private void FillddlControlsCAM()
        {
            try
            {
                ddlLedgerType.Items.Clear();
                ddlLedgerType.AppendDataBoundItems = true;
                ddlLedgerType.Items.Add(new ListItem("Select Ledger Type", "-1"));
                ddlLedgerType.SelectedValue = "-1";
                ddlLedgerType.DataSource = new ChildDA().GetChildByParentID(Convert.ToInt32(EnumGlobalData.Ledger));
                ddlLedgerType.DataTextField = "Description";
                ddlLedgerType.DataValueField = "Code";
                ddlLedgerType.DataBind();

            }
            catch (Exception ex)
            {

            }
            try
            {
                ddlPaidBy.Items.Clear();
                ddlPaidBy.AppendDataBoundItems = true;
                ddlPaidBy.Items.Add(new ListItem("Select Payment Type", "-1"));
                ddlPaidBy.SelectedValue = "-1";
                ddlPaidBy.DataSource = new ChildDA().GetChildByParentID(Convert.ToInt32(EnumGlobalData.Payment));
                ddlPaidBy.DataTextField = "Description";
                ddlPaidBy.DataValueField = "Code";
                ddlPaidBy.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        private CAMExpense SetDataCAM(CAMExpense obj)
        {
            try
            {
                obj = new CAMExpense();

                if (Session["CAMId"] != null && Convert.ToInt32(Session["CAMId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["CAMId"].ToString());
                }

                if ((!string.IsNullOrEmpty(txtName.Text.ToString())) && (txtName.Text.ToString() != string.Empty))
                {
                    obj.Name = txtName.Text.ToString();
                }
                else
                {
                    obj.Name = "";
                }

                if ((!string.IsNullOrEmpty(txtExpenseType.Text.ToString())) && (txtExpenseType.Text.ToString() != string.Empty))
                {
                    obj.TypeOfExpense = txtExpenseType.Text.ToString();
                }
                else
                {
                    obj.TypeOfExpense = "";
                }

                if (ddlLedgerType.SelectedValue != "-1")
                {
                    obj.LedgerAccount = ddlLedgerType.SelectedValue.ToString();
                }
                else
                {
                    obj.LedgerAccount = "";
                }
                if (ddlPaidBy.SelectedValue != "-1")
                {
                    obj.PaidBy = ddlPaidBy.SelectedValue.ToString();
                }
                else
                {
                    obj.PaidBy = "";
                }
                if ((!string.IsNullOrEmpty(txtNumber.Text.ToString())) && (txtNumber.Text.ToString() != string.Empty))
                {
                    obj.Amount = Convert.ToDecimal(txtNumber.Text.ToString());
                }
                else
                {
                    obj.Amount =0;
                }
                if ((!string.IsNullOrEmpty(txtCheckNo.Text.ToString())) && (txtCheckNo.Text.ToString() != string.Empty))
                {
                    obj.CheckNumber = txtCheckNo.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.CheckNumber = "";
                }
                if (rdoCAM.Items[0].Selected == true)
                {
                    obj.IsCAM = true;
                }
                else
                {
                    obj.IsCAM = false;
                }

                obj.IsDelete = false;

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

                if (Session["CAMId"] == null || obj.PaidDate == null)
                {
                    obj.PaidDate = DateTime.Now;
                }

            }
            catch (Exception ex)
            {
            }           

            return obj;
        }
        private void FillControlsCAM(int nId)
        {
            try
            {
                if (nId > 0)
                {
                    CAMExpense obj = new CAMExpenseDA().GetbyID(nId);
                    if (obj != null)
                    {
                        Session["CAMId"] = obj.Id;
                        if (obj.Name != null && obj.Name.ToString() != string.Empty)
                        {
                            txtName.Text = obj.Name;
                        }
                        else
                        {
                            txtName.Text = "";
                        }
                        if (obj.TypeOfExpense != null && obj.TypeOfExpense.ToString() != string.Empty)
                        {
                            txtExpenseType.Text = obj.TypeOfExpense;
                        }
                        else
                        {
                            txtExpenseType.Text = "";
                        }

                        if (obj.LedgerAccount != null && obj.LedgerAccount.ToString() != string.Empty)
                        {
                            ddlLedgerType.SelectedValue = obj.LedgerAccount.ToString();
                        }
                        else
                        {
                            ddlLedgerType.SelectedValue = "-1";
                        }

                        if (obj.PaidBy != null && obj.PaidBy.ToString() != string.Empty)
                        {
                            ddlPaidBy.SelectedValue = obj.PaidBy.ToString();
                        }
                        else
                        {
                            ddlPaidBy.SelectedValue = "-1";
                        }
                        if (obj.Amount != null && obj.Amount.ToString() != string.Empty)
                        {
                            txtNumber.Text = Convert.ToDecimal(obj.Amount).ToString("#.00");
                        }
                        if (obj.CheckNumber != null && obj.CheckNumber.ToString() != string.Empty)
                        {
                            txtCheckNo.Text = obj.CheckNumber;
                        }
                        else
                        {
                            txtCheckNo.Text = "";
                        }

                        if (obj.IsCAM != null)
                        {
                            if (Convert.ToInt32(obj.IsCAM) == 1)
                            {
                                rdoCAM.Items[0].Selected = true;
                            }
                            else 
                            {
                                rdoCAM.Items[1].Selected = true;
                            }
                        }

                        lblPropertyLocation.InnerText = obj.PropertyLocationId;
                        btnSaveCAM.Text = "Update Expense Contact";

                    }
                }
            }
            catch(Exception e)
            {

            }
        }
        private void FillCAMs()
        {
            try
            {
                List<CAMExpense> obj = null;
                if (Session["PropertyLocationId"] != null)
                {
                    obj = new CAMExpenseDA().GetByPropertyLocation(Session["PropertyLocationId"].ToString());
                }
                else if (Session["OwnerId"] != null)
                {
                    obj = new CAMExpenseDA().GetByOwner(Session["OwnerId"].ToString());
                }

                gvCAMList.DataSource = obj;
                gvCAMList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
    }
}