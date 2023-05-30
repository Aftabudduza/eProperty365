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

namespace eProperty.Pages.Admin
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        public String errStr = String.Empty;
        private Regex reEmail = new Regex("^(?:[0-9A-Z_-]+(?:\\.[0-9A-Z_-]+)*@[0-9A-Z-]+(?:\\.[0-9A-Z-]+)*(?:\\.[A-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                if ((Session["UserObject"] != null))
                {
                    Session["AddUserId"] = null;
                    Session["AddUserEmail"] = null;
                    int CId = 0;
                    try
                    {
                        CId = Convert.ToInt32(Request.QueryString["AddUserId"].ToString());
                    }
                    catch(Exception ex)
                    {
                        CId = 0;
                    }
                    if (CId > 0)
                    {
                        Session["AddUserId"] = CId;
                        FillControls(CId);
                    }
                }
            }
        }
        #region Events

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

                    if (Session["AddUserId"] != null || Session["AddUserId"] != "0")
                    {
                        if (new UserProfileDA().Update(objUser))
                        {
                            if (new AdminUserProfileDA().Update(objUser))
                            {

                            }
                            Session["AddUserEmail"] = null;
                            Session["AddUserId"] = null;
                            ClearControls();

                            Utility.DisplayMsg("Password updated successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Password not updated!", this);
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
        }
        
       
        #endregion
        #region Method
        private void ClearControls()
        {
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtPassword.Text = "";
        }
        public string Validate_Control()
        {
            try
            {
                if (txtPassword.Text.ToString().Length <= 0)
                {
                    errStr += "Please enter Current Password" + Environment.NewLine;
                }

                if (Session["AddUserEmail"] != null)
                {
                    UserProfile objUsers2 = new UserProfileDA().GetUserByEmailPassword(Session["AddUserEmail"].ToString(), txtPassword.Text.ToString());

                    if (objUsers2 == null)
                    {
                        errStr += "Current Password not found. Please enter correct password" + Environment.NewLine;
                    }
                }

                if (txtPassword.Text.ToString() == txtNewPassword.Text.ToString())
                {
                    errStr += "New Password can not be same as Current Password" + Environment.NewLine;
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
                if (Session["AddUserId"] != null && Convert.ToInt32(Session["AddUserId"]) > 0)
                {
                    obj = new UserProfileDA().GetUserByUserID(Convert.ToInt32(Session["AddUserId"].ToString()));
                    
                }
                if (obj != null)
                {
                    if (Session["AddUserId"] != null)
                    {
                        obj.UpdatedBy = Convert.ToInt16(((UserProfile) Session["UserObject"]).Id);
                        obj.UpdatedDate = DateTime.Now;
                    }


                    if (!string.IsNullOrEmpty(txtNewPassword.Text.ToString()))
                    {
                        obj.Password = Utility.base64Encode(txtNewPassword.Text.ToString());
                    }
                    else
                    {
                        obj.Password = Utility.base64Encode("1234");
                    }
                }
            }
            catch (Exception e)
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
                    UserProfile obj = new AdminUserProfileDA().GetUserByUserID(nId);
                    if (obj != null)
                    {
                        Session["AddUserId"] = obj.Id;

                        if (obj.Email != null && obj.Email.ToString() != string.Empty)
                        {
                            Session["AddUserEmail"] = obj.Email;
                        }
                        if (obj.Password != null && obj.Password.ToString() != string.Empty)
                        {
                            txtPassword.Text = Utility.base64Decode(obj.Password);
                            txtPassword.Attributes.Add("value", Utility.base64Decode(obj.Password.ToString()));
                        }
                       

                    }
                }
            }
            catch (Exception e)
            {

            }
        }
       

        #endregion

    }
}