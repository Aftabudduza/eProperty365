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
using PropertyService.Admin.DA;
using System.Data.SqlClient;

namespace eProperty.Pages.Admin
{
    public partial class UserReport : System.Web.UI.Page
    {
        public String errStr = String.Empty;
        private Regex reEmail = new Regex("^(?:[0-9A-Z_-]+(?:\\.[0-9A-Z_-]+)*@[0-9A-Z-]+(?:\\.[0-9A-Z-]+)*(?:\\.[A-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        public string sUrl = string.Empty;

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillUsers();               
            }
        }
       
        protected void btnBack_Click(object sender, EventArgs e)
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
        protected void btnLoginSupport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtLoginEmail.Text.Trim()) && txtLoginEmail.Text.ToString().Trim() == "E365supp0rt%#!$")
                {
                    divSearchUser.Visible = true;
                    divSearchUserList.Visible = true;
                    lblValid.Visible = false;
                }
                else
                {
                    lblValid.Visible = true;
                    divSearchUser.Visible = false;
                    divSearchUserList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblValid.Visible = true;
                divSearchUser.Visible = false;
                divSearchUserList.Visible = false;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                Session["AddUserSearch"] = null;
                if (search.Value.ToString().Trim() != string.Empty)
                {
                    string strWhere = string.Empty;
                    strWhere = search.Value.ToString().Trim();
                    Session["AddUserSearch"] = strWhere;
                    List<UserProfile> obj = null;
                    obj = new UserProfileDA().GetBySearch(strWhere);
                    gvContactList.DataSource = obj;
                    gvContactList.DataBind();
                }
            }
            catch (Exception ex) { }
        }
        protected void gvContactList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContactList.PageIndex = e.NewPageIndex;
            FillUsers();
        }

        protected void gvContactList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillUsers();
        }
        #endregion
        #region Method        
        private void FillUsers()
        {
            try
            {
                List<UserProfile> obj = null;
                if (Session["AddUserSearch"] != null)
                {
                    obj = new UserProfileDA().GetBySearch(Session["AddUserSearch"].ToString());
                }
                else
                {
                    obj = new UserProfileDA().GetAllUsers();
                }                    

                gvContactList.DataSource = obj;
                gvContactList.DataBind();

            }
            catch (Exception e)
            {

            }
        }

        #endregion

    }
}