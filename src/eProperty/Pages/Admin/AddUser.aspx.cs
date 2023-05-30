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
    public partial class AddUser : System.Web.UI.Page
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
                if ((Session["UserObject"] != null))
                {
                    Session["AddUserId"] = null;
                    Session["AddUserSearch"] = null;
                    Session["LogoFileName"] = null;
                    Session["userUrl"] = null;

                    try
                    {
                        if (Request.QueryString["R"] != null)
                        {
                            sUrl = Request.QueryString["R"].ToString();
                            Session["userUrl"] = sUrl.ToLower();
                        }
                    }
                    catch
                    {
                        sUrl = "";
                    }

                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["AddUserId"].ToString());
                    }
                    catch (Exception ex)
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                        UserProfile objTemp = new AdminUserProfileDA().GetUserByUserID(CId);
                        if (objTemp != null)
                        {
                            UserProfile obj = new UserProfileDA().GetUserByEmail(objTemp.Email);
                            if (obj != null)
                            {
                                Session["AddUserId"] = obj.Id;
                                FillControls(obj.Id);
                            }
                                
                        }
                    }
                    else
                    {
                        //if (Session["UserObject"] != null)
                        //{
                        //    CId = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                        //    if (CId > 0)
                        //    {
                        //        Session["AddUserId"] = CId;
                        //        FillControls(CId);
                        //    }
                        //}

                    }

                    int IsTopMenu = 0;
                    try
                    {
                        IsTopMenu = Convert.ToInt32(Request.QueryString["IsTopMenu"].ToString());
                      
                    }
                    catch (Exception ex)
                    {
                        IsTopMenu = 0;
                    }


                    int nUserType = Convert.ToInt16(((UserProfile)Session["UserObject"]).UserType);
                    if(IsTopMenu == 0 && (nUserType == 1 || nUserType == 2 || nUserType == 3))
                    {
                        divSearchUser.Visible = true;
                        divSearchUserList.Visible = true;
                        //btnBack.Visible = true;
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
                    UserProfile objUser = new UserProfile();
                    objUser = SetData(objUser);
                    string username = "";

                    if (Session["userUrl"] != null)
                    {
                        sUrl = Session["userUrl"].ToString();
                    }

                    if (Session["UserObject"] != null)
                    {
                        username = ((UserProfile)Session["UserObject"]).Username;
                    }

                    string SQL = " update UserProfile set HasUserProfile = 1  where Username = '" + username + "' ";

                    if (Session["AddUserId"] == null || Session["AddUserId"] == "0")
                    {
                        if (new UserProfileDA().Insert(objUser))
                        {
                            Utility.RunCMD(SQL);
                            //if (new AdminUserProfileDA().Insert(objUser))
                            //{
                            //    Utility.RunCMDMain(SQL);
                            //}
                           
                            Session["AddUserId"] = null;
                            ClearControls();
                            FillUsers();
                            // Utility.DisplayMsg("User saved successfully!", this);

                            if (sUrl != string.Empty)
                            {
                                if (sUrl.Trim() == "owner")
                                {
                                    Utility.DisplayMsgAndRedirect("User saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                                }
                                else if (sUrl.Trim() == "home")
                                {
                                    Utility.DisplayMsgAndRedirect("User saved successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                                }
                                else if (sUrl.Trim() == "location")
                                {
                                    Utility.DisplayMsgAndRedirect("User saved successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                                }
                                else
                                {
                                    Utility.DisplayMsg("User saved successfully!", this);
                                }
                            }
                            else
                            {
                                Utility.DisplayMsg("User saved successfully!", this);
                            }

                        }
                        else
                        {
                            Utility.DisplayMsg("User not saved!", this);
                        }
                    }
                    else
                    {
                        if (new UserProfileDA().Update(objUser))
                        {
                            Utility.RunCMD(SQL);
                            UserProfile objExistUser = new AdminUserProfileDA().GetUserByEmail(objUser.Email);
                            if (objExistUser != null)
                            {
                                objUser.Id = objExistUser.Id;
                                if (new AdminUserProfileDA().Update(objUser))
                                {
                                    Utility.RunCMDMain(SQL);
                                }
                            }
                           
                          //  Session["AddUserId"] = null;
                            ClearControls();
                            FillUsers();
                            //  Utility.DisplayMsg("User updated successfully!", this);
                            if (sUrl != string.Empty)
                            {
                                if (sUrl.Trim() == "owner")
                                {
                                    Utility.DisplayMsgAndRedirect("User updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddOwner.aspx");
                                }
                                else if (sUrl.Trim() == "home")
                                {
                                    Utility.DisplayMsgAndRedirect("User updated successfully!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                                }
                                else if (sUrl.Trim() == "location")
                                {
                                    Utility.DisplayMsgAndRedirect("User updated successfully!", this, Utility.WebUrl + "/Pages/Admin/AddLocation.aspx");
                                }
                                else
                                {
                                    Utility.DisplayMsg("User updated successfully!", this);
                                }
                            }
                            else
                            {
                                Utility.DisplayMsg("User updated successfully!", this);
                            }

                        }
                        else
                        {
                            Utility.DisplayMsg("User not updated!", this);
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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearControls();
            if (Session["userUrl"] != null)
            {
                sUrl = Session["userUrl"].ToString();
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
                Session["AddUserSearch"] = null;
                if (search.Value.ToString().Trim() != string.Empty)
                {
                    string strWhere = string.Empty;
                    strWhere = search.Value.ToString().Trim();
                    Session["AddUserSearch"] = strWhere;
                    List<UserProfile> obj = null;

                    var isAdmin = false;
                    if (Session["UserObject"] != null)
                    {
                        isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                              ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                              : false;

                        if (isAdmin == true)
                        {
                            obj = new UserProfileDA().GetBySearch(strWhere);
                        }
                        else if (Session["OwnerId"] != null)
                        {
                            obj = new UserProfileDA().GetBySearchAndOwnerId(strWhere, Session["OwnerId"].ToString());
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
                    FillUsers();
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvContactList.Rows[row.RowIndex].FindControl("lblId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                Session["AddUserId"] = Convert.ToInt32(hdId.Text);
                FillControls(Convert.ToInt32(hdId.Text));
            }
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
        private void ClearControls()
        {
            txtContactName.Text = "";
            txtContactTitle.Text = "";
            txtNumber.Text = "";
            txtEmail.Text = "";
            ddlType.SelectedValue = "-1";
            chkLocation.Checked = false;
            chkAdmin.Checked = true;
            btnSave.Text = "Add";
            lblHeadline.InnerText = "Add User";
            txtPassword.Text = "";
            imgLogo.ImageUrl = "";
            txtPassword.Attributes.Add("value", "");
        }
        public string Validate_Control()
        {
            try
            {
                if ((txtContactName.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Username" + Environment.NewLine;
                }
                if ((txtEmail.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter email address" + Environment.NewLine;
                }
                else
                {
                    if (!ValidEmail(txtEmail.Text.ToString().Trim()))
                    {
                        errStr += "Invalid email address" + Environment.NewLine;
                    }
                }
                if ((txtPassword.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Password" + Environment.NewLine;
                }
                if ((txtNumber.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Phone Number" + Environment.NewLine;
                }

                List<UserProfile> objUsers = new UserProfileDA().GetUsersByUserName2(txtContactName.Text.ToString());

                if (objUsers != null && objUsers.Count > 0)
                {
                    if (Session["AddUserId"] != null)
                    {
                        if (objUsers.Count > 1)
                        {
                            errStr += "Username already exist !! Please enter different Username." + Environment.NewLine;
                        }
                    }
                    else
                    {
                        errStr += "Username already exist !! Please enter different Username." + Environment.NewLine;
                    }
                }

                List<UserProfile> objUsers2 = new UserProfileDA().GetUsersByUserEmail2(txtEmail.Text.ToString());

                if (objUsers2 != null && objUsers2.Count > 0)
                {
                    if (Session["AddUserId"] != null)
                    {
                        if (objUsers2.Count > 1)
                        {
                            errStr += "Email already exist !! Please enter different Email." + Environment.NewLine;
                        }
                    }
                    else
                    {
                        errStr += "Email already exist !! Please enter different Email." + Environment.NewLine;
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return errStr;
        }
        public bool ValidEmail(string value)
        {
            if ((value == null))
                return false;
            return reEmail.IsMatch(value);
        }
        private UserProfile SetData(UserProfile obj)
        {
            try
            {
                obj = new UserProfile();

                if (Session["AddUserId"] != null && Convert.ToInt32(Session["AddUserId"]) > 0)
                {
                    obj = new UserProfileDA().GetUserByUserID(Convert.ToInt32(Session["AddUserId"]));
                    obj.Id = Convert.ToInt32(Session["AddUserId"].ToString());
                }

                if ((!string.IsNullOrEmpty(txtContactName.Text.ToString())) && (txtContactName.Text.ToString() != string.Empty))
                {
                    obj.Username = txtContactName.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Username = "";
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
                    obj.SecurityLevel = ddlType.SelectedValue.ToString();
                }
                else
                {
                    obj.SecurityLevel = "";
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

                if (chkAdmin.Checked)
                {
                    obj.IsAdmin = false;
                }
                else
                {
                    obj.IsAdmin = true;
                }

               

                if (Request.QueryString["IsTopMenu"] != null && Request.QueryString["IsTopMenu"] != string.Empty)
                {
                    if (Session["UserType"] != null)
                    {
                        obj.UserType = Session["UserType"].ToString();
                    }
                    else
                    {
                        obj.UserType = Convert.ToInt32(EnumUserType.Normal).ToString();
                    }
                }
                else
                {
                    if (Session["UserObject"] != null)
                    {
                        string sUserType = ((UserProfile)Session["UserObject"]).UserType;
                        string sUserEmail = ((UserProfile)Session["UserObject"]).Email;

                        if(sUserEmail == obj.Email.Trim())
                        {
                            obj.UserType = sUserType;
                        }
                        else
                        {
                            obj.UserType = Convert.ToInt32(EnumUserType.Normal).ToString();
                        }
                    }
                    else
                    {
                        obj.UserType = Convert.ToInt32(EnumUserType.Normal).ToString();
                    }
                   
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
                            obj.OwnerId = Session["OwnerId"].ToString();
                        }
                    }
                    else
                    {
                       // obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                    }
                }
                else
                {
                   // obj.OwnerId = new OwnerProfileDA().MakeAutoGenSerial("O", "Owner");
                }

                if (Session["AddUserId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                }
                else
                {
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                    obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
                    obj.CanLogin = false;
                    obj.IsDeleted = false;
                    obj.IsActive = true;
                    obj.DatabaseName = "";
                    obj.DatabaseLocation = "";
                    obj.Remarks = "";
                    obj.HasCompletedFullProfile = false;
                    obj.HasContactProfile = false;
                    obj.HasLedgerCode = false;
                    obj.HasOwnerProfile = false;
                    obj.HasPropertyLocation = false;
                    obj.HasPropertyManagerProfile = false;
                    obj.HasPropertyUnit = false;
                    obj.HasSystemInfo = false;
                    obj.HasUserProfile = false;
                    obj.HasVendorProfile = false;
                    obj.HasAccountSystem = false;
                    obj.HasDocuments = false;
                    obj.HasFinishedTenantImport = false;

                }

                if (obj.LocationId == null || obj.LocationId == string.Empty)
                {
                    obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
                }

               

                if ((!string.IsNullOrEmpty(txtPassword.Text.ToString())) && (txtPassword.Text.ToString() != string.Empty))
                {
                    obj.Password = Utility.base64Encode(txtPassword.Text.ToString());
                }
                else
                {
                    obj.Password = Utility.base64Encode("1234");
                }

                if (!string.IsNullOrEmpty(uplLogo.FileName))
                {
                    //read the file in
                    string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\User\\");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fileName = this.uplLogo.FileName;
                    string nFile = Path.Combine(filePath, fileName);
                    Session["LogoFileName"] = fileName;
                    obj.Location = fileName;

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

                    uplLogo.SaveAs(nFile);
                }
                else
                {
                    if (Session["LogoFileName"] != null)
                    {
                        obj.Location = Session["LogoFileName"].ToString();
                    }
                }

                //obj.DatabaseLocation = GetFilePathFromConnectionString(Utility.CONNECTIONSTRINGNEW);
            }
            catch (Exception e)
            {
            }


            return obj;
        }
        public string GetFilePathFromConnectionString(string connectionString)
        {
            var attachDbFileName = new SqlConnectionStringBuilder(connectionString).AttachDBFilename;
            return attachDbFileName.Replace("|DataDirectory|", AppDomain.CurrentDomain.GetData("DataDirectory").ToString());
        }

        private void FillControls(int nId)
        {
            try
            {
                if (nId > 0)
                {
                    UserProfile obj = new UserProfileDA().GetUserByUserID(nId);
                    if (obj != null)
                    {
                        Session["AddUserId"] = obj.Id;
                        if (obj.Username != null && obj.Username.ToString() != string.Empty)
                        {
                            txtContactName.Text = obj.Username;
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

                        if (obj.SecurityLevel != null && obj.SecurityLevel.ToString() != string.Empty)
                        {
                            ddlType.SelectedValue = obj.SecurityLevel.ToString();
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


                        if (obj.IsAdmin != null && obj.IsAdmin.ToString() != string.Empty)
                        {
                            chkAdmin.Checked = Convert.ToBoolean(!obj.IsAdmin);
                        }
                        else
                        {
                            chkAdmin.Checked = false;
                        }

                        if (obj.Password != null && obj.Password.ToString() != string.Empty)
                        {
                            txtPassword.Text = Utility.base64Decode(obj.Password);
                            txtPassword.Attributes.Add("value", Utility.base64Decode(obj.Password.ToString()));
                        }
                        else
                        {
                            txtPassword.Text = "";
                        }

                        if (obj.Location != null && obj.Location != string.Empty)
                        {
                            imgLogo.ImageUrl = Utility.WebUrl + "/Uploads/Files/User/" + obj.Location;
                            Session["LogoFileName"] = obj.Location;
                        }

                        btnSave.Text = "Update";
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        private void FillUsers()
        {
            try
            {
                List<UserProfile> obj = null;

                var isAdmin = false;
               
                if (Session["UserObject"] != null)
                {
                    string sUserType = ((UserProfile)Session["UserObject"]).UserType;

                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                          ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                          : false;

                    if (isAdmin == true)
                    {
                        obj = new UserProfileDA().GetAllUsers();
                    }
                    else if (Session["OwnerId"] != null && (sUserType == "1" || sUserType == "2" || sUserType == "3"))
                    {
                        obj = new UserProfileDA().GetByOwner(Session["OwnerId"].ToString());
                    }

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