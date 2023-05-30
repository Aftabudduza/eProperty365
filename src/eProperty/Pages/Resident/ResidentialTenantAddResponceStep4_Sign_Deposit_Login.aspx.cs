using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;
using PropertyService.Admin.DA;
using PropertyService.DA;
using PropertyService.Enums;
using eProperty.MyFileIt;
using System.Configuration;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialTenantAddResponceStep4_Sign_Deposit_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["TenentId"] = null;
                Session["ResidentialUnitSerial"] = null;
                Session["TenantPassword"] = null;
                string residentialId, tenantId;
                try
                {
                    residentialId = Request.QueryString["ResidentialUnitSerial"];
                }
                catch (Exception ex)
                {
                    residentialId = "";
                }
                try
                {
                    tenantId = Request.QueryString["TenentId"];
                }
                catch (Exception ex)
                {
                    tenantId = "";
                }
                if (residentialId != "" && tenantId != "" && residentialId != null && tenantId != null)
                {
                    Session["TenentId"] = tenantId;
                    Session["ResidentialUnitSerial"] = residentialId;
                }
                else
                {
                    
                }
            }
            //Session["TenantPassword"] = "123";
        }
        [WebMethod(EnableSession = true)]
        public static string GetAdditionalDoc()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()) && HttpContext.Current.Session["TenantPassword"] == null)
                return "";
            var ResidentialDoc = new ResidentialAddResponceTemplateDa().GetRentalDocList_Step4(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), HttpContext.Current.Session["TenentId"].ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ResidentialDoc);
            return json;

        }
        [WebMethod(EnableSession = true)]
        public static string GetLogin(ResidentialTenantSignIn Obj)
        {
            var checkValidTenant = new ResidentialTenantSignIn();
            bool bIsSaveTenant = false;
            if (Obj !=null)
            {
                checkValidTenant = new ResidentialAddResponceTemplateDa().CheckValideTenant(Obj);
                if (checkValidTenant !=null)
                {
                    checkValidTenant.Password = Obj.Password;
                    checkValidTenant.UpdatedDate = DateTime.Now;
                    if (new ResidentialAddResponceTemplateDa().UpdateTenantPassword(checkValidTenant))
                    {
                        HttpContext.Current.Session["TenantPassword"] = checkValidTenant.Password;

                        try
                        {
                            UserProfile objTenant = new UserProfile();

                            UserProfile objOldTenant = new AdminUserProfileDA().GetUserByEmail(Obj.EmailId);

                            ResidentialUnit objUnit = new ResidentialUnitDa().GetbySerial(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());

                            objTenant.Email = Obj.EmailId;
                            objTenant.Password = Utility.base64Encode(Obj.Password);
                            objTenant.IsAdmin = false;
                            objTenant.UserType = Convert.ToInt32(EnumUserType.Resident).ToString();
                            string tempEmail = objTenant.Email;
                            char ch = '@';
                            int idx = tempEmail.IndexOf(ch);
                            string username = tempEmail.Substring(0, idx);

                            objTenant.Username = username;
                            objTenant.Title = objTenant.Username;
                            objTenant.Phone = checkValidTenant.PhoneNumber != null ? checkValidTenant.PhoneNumber : "";
                            objTenant.LocationId = objUnit != null ? objUnit.LocationSerialId : "";
                            objTenant.Location = "";
                            objTenant.DatabaseName = "";
                            objTenant.DatabaseLocation = "";
                            objTenant.CanLogin = true;
                            objTenant.IsDeleted = false;
                            objTenant.SecurityLevel = "2 and Up";
                            objTenant.IsActive = true;
                            objTenant.OwnerId = objUnit != null ? objUnit.OwnerId : "";
                            objTenant.Remarks = "";
                            objTenant.HasCompletedFullProfile = false;
                            objTenant.HasContactProfile = false;
                            objTenant.HasLedgerCode = false;
                            objTenant.HasOwnerProfile = false;
                            objTenant.HasPropertyLocation = false;
                            objTenant.HasPropertyManagerProfile = false;
                            objTenant.HasPropertyUnit = false;
                            objTenant.HasSystemInfo = false;
                            objTenant.HasUserProfile = true;
                            objTenant.HasVendorProfile = false;
                            objTenant.HasAccountSystem = false;
                            objTenant.HasDocuments = false;
                            objTenant.HasFinishedTenantImport = false;
                            objTenant.CreatedDate = DateTime.Now;

                            PropertyEntities mEntity = new PropertyEntities();
                            if (objOldTenant != null)
                            {
                                objTenant.Id = objOldTenant.Id;
                                if (new AdminUserProfileDA().Update(objTenant))
                                {
                                    HttpContext.Current.Session["UserObject"] = objTenant;
                                    HttpContext.Current.Session["UserType"] = objTenant.UserType;
                                    HttpContext.Current.Session["Username"] = objTenant.Username;
                                    HttpContext.Current.Session["bIsLogin"] = true;
                                    HttpContext.Current.Session["OwnerId"] = objTenant.OwnerId;
                                    HttpContext.Current.Session["UserId"] = objTenant.Id;

                                    Utility.LoginUser = objTenant.Username;

                                    bIsSaveTenant = true;

                                    //HttpContext.Current.Cache.Add(objTenant.Username, "", null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                                    //string sLastPage = System.Web.Security.FormsAuthentication.GetRedirectUrl(HttpContext.Current.User.Identity.Name, false);
                                    //System.Web.Security.FormsAuthentication.RedirectFromLoginPage(objTenant.Username, true);
                                    UserProfile objOldTenant1 = new UserProfileDA().GetUserByEmail(Obj.EmailId);
                                    if (objOldTenant1 != null)
                                    {
                                        objTenant.Id = objOldTenant1.Id;
                                        if (new UserProfileDA().Update(objTenant))
                                        {

                                        }
                                    }
                                    else
                                    {
                                        if (new UserProfileDA(true, false).Insert(objTenant))
                                        {

                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (new AdminUserProfileDA(true, false).Insert(objTenant))
                                {
                                    HttpContext.Current.Session["UserObject"] = objTenant;
                                    HttpContext.Current.Session["UserType"] = objTenant.UserType;
                                    HttpContext.Current.Session["Username"] = objTenant.Username;
                                    HttpContext.Current.Session["bIsLogin"] = true;
                                    HttpContext.Current.Session["OwnerId"] = objTenant.OwnerId;
                                    Utility.LoginUser = objTenant.Username;

                                    bIsSaveTenant = true;

                                    //HttpContext.Current.Cache.Add(objTenant.Username, "", null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                                    //string sLastPage = System.Web.Security.FormsAuthentication.GetRedirectUrl(HttpContext.Current.User.Identity.Name, false);
                                    //System.Web.Security.FormsAuthentication.RedirectFromLoginPage(objTenant.Username, true);

                                    UserProfile objOldTenant1 = new UserProfileDA().GetUserByEmail(Obj.EmailId);
                                    if (objOldTenant1 != null)
                                    {
                                        objTenant.Id = objOldTenant1.Id;
                                        if (new UserProfileDA().Update(objTenant))
                                        {

                                        }
                                    }
                                    else
                                    {
                                        if (new UserProfileDA(true, false).Insert(objTenant))
                                        {

                                        }
                                    }
                                }
                            }

                            if (bIsSaveTenant == true)
                            {
                                if (saveFileItUser(objTenant))
                                {

                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }                      

                    }                  

                }
                
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(checkValidTenant);
            return json;
        }
        private static bool saveFileItUser(UserProfile objUserProfile)
        {
            bool bSuccess = false;
            try
            {
                var strFileItUser = ConfigurationManager.AppSettings["FileItUser"];
                var strFileItPassword = ConfigurationManager.AppSettings["FileItPassword"];

                MyFileIt.MyFileItPEMainServiceClient obj = new MyFileIt.MyFileItPEMainServiceClient();
                MyFileIt.MyFileItResult result = new MyFileIt.MyFileItResult();
                List<AppUserDTO> objAppUsers = new List<AppUserDTO>();
                MyFileIt.AppUserDTO objAppUser = new MyFileIt.AppUserDTO();

                objAppUser = setdataFileItUser(objUserProfile);

                AppUserDTO ExistingUser = null;

                result = obj.GetAllAppUsers(strFileItUser, strFileItPassword, null);
                objAppUsers = result.AppUsers.Where(x => x.EMAILADDRESS.ToLower().Contains(objAppUser.EMAILADDRESS.ToLower())).ToList();
                ExistingUser = objAppUsers.FirstOrDefault();

                if (!string.IsNullOrEmpty(objAppUser.EMAILADDRESS) && !string.IsNullOrEmpty(objAppUser.PASSWORD))
                {
                    if (ExistingUser != null)
                    {
                        objAppUser.ID = ExistingUser.ID;
                        objAppUser.APPUSERID = ExistingUser.APPUSERID;

                        result = obj.UpdateAppUser(strFileItUser, strFileItPassword, objAppUser);
                        if (result.Success == true)
                        {
                            bSuccess = true;
                        }
                    }
                    else
                    {
                        result = obj.AddAppUser(strFileItUser, strFileItPassword, objAppUser, null);
                        if (result.Success == true)
                        {
                            bSuccess = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return bSuccess;

        }
        public static AppUserDTO setdataFileItUser(UserProfile objUserProfile)
        {
            AppUserDTO objU = new AppUserDTO();           
            string sPassword = string.Empty;
            if (HttpContext.Current.Session["TenantPassword"] != null)
            {
                sPassword = HttpContext.Current.Session["TenantPassword"].ToString();
            }

            ResidentialTenantSignIn objResidentialTenantSignIn = new ResidentialAddResponceTemplateDa().GetResidentialTenantInfoByEmail(objUserProfile.Email);

            try
            {
                objU.FIRSTNAME = objResidentialTenantSignIn != null ? objResidentialTenantSignIn.FirstName : "";
                objU.LASTNAME = objResidentialTenantSignIn != null ? objResidentialTenantSignIn.LastName : "";
                objU.APPUSERTYPEID = 4;
                objU.BIRTHDATE = null;
                objU.PHONE = objResidentialTenantSignIn != null ? objResidentialTenantSignIn.PhoneNumber : ""; 
                objU.PASSWORD = sPassword;
                if (objU.PASSWORD == null || objU.PASSWORD == string.Empty)
                {
                    objU.PASSWORD = "1234";
                }

                objU.ADDRESS1 = "ADDRESS1";
                objU.CITY = "CITY";
                objU.STATECODE = "PA";
                objU.ZIPCODE = "10000";
                objU.SEX = "M";
                objU.PRIMARYAPPUSERID = null;
                objU.RELATIONSHIPTYPEID = null;
                objU.DATECREATED = DateTime.UtcNow;
                objU.EMAILADDRESS = objResidentialTenantSignIn != null ? objResidentialTenantSignIn.EmailId : "";
                objU.USERNAME = objU.EMAILADDRESS;
                objU.APPUSERSTATUSID = 2;
                objU.Organizations = null;
            }

            catch (Exception e)
            {

            }

            return objU;

        }

    }
}