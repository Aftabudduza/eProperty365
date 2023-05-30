﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.Admin.DA;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml.Linq;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using PropertyService.ViewModel;

namespace eProperty.Pages
{
    public partial class DashboardOwner : System.Web.UI.Page
    {
        public String errStr = String.Empty;
        private System.Net.Mail.SmtpClient objSmtpClient;
        private System.Net.Mail.MailMessage objMailMessage;
        private Regex reEmail = new Regex("^(?:[0-9A-Z_-]+(?:\\.[0-9A-Z_-]+)*@[0-9A-Z-]+(?:\\.[0-9A-Z-]+)*(?:\\.[A-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["LogoFileName"] = null;
                FillOwner();

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
                    List<PropertyManagerProfile> objPropertyManagers = new PropertyManagerProfileDA().GetByOwnerId(Session["OwnerId"].ToString());
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
                    ddlLocation.DataSource = new LocationDA().GetByOwner(Session["OwnerId"].ToString());
                    ddlLocation.DataTextField = "LocationName";
                    ddlLocation.DataValueField = "Serial";
                    ddlLocation.DataBind();
                    ddlLocation.SelectedValue = "-1";

                    ddlUnit.Items.Clear();
                    ddlUnit.AppendDataBoundItems = true;
                    ddlUnit.Items.Add(new ListItem("Select Unit", "-1"));
                    List<ResidentialUnit> objResidentialUnits = new ResidentialUnitDa().GetByOwner(Session["OwnerId"].ToString());
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

                    ddlPayment.Items.Clear();
                    ddlPayment.AppendDataBoundItems = true;
                    for (int i = 1; i <= 30; i++)
                    {
                        ddlPayment.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }

                    ddlPayment.DataBind();

                    if(ddlOwner.SelectedValue != null && ddlOwner.SelectedValue != "-1")
                    {
                        FillTenantList(ddlOwner.SelectedValue, "", "", "", "");
                    }
					else
					{
                        divBottom.Visible = false;
					}

                    FillChartData(Session["OwnerId"].ToString());
                }

                if (Session["UserObject"] != null)
                {
                    string sSQL = " select ISNULL(u.HasOwnerProfile,0) HasOwnerProfile, ISNULL(u.HasPropertyLocation,0) HasPropertyLocation, ISNULL(u.HasPropertyUnit,0) HasPropertyUnit, ISNULL(u.HasFinishedTenantImport,0) HasFinishedTenantImport, ISNULL(u.HasAccountSystem,0) HasAccountSystem, ISNULL(u.HasDocuments,0) HasDocuments  from UserProfile u where u.Email = '" + ((UserProfile)Session["UserObject"]).Email.Trim() + "'";

                    try
                    {
                        string constr = ConfigurationManager.ConnectionStrings["SQLDBOwner"].ConnectionString;
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
                                    litop2.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Admin/AddOwner.aspx'>1) Setup Owners Profile </a>";
                                }
                                else
                                {
                                    litop2.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Admin/AddOwner.aspx'>1) Setup Owners Profile </a>";
                                }

                                if (!Convert.IsDBNull(dr["HasPropertyLocation"]) && Convert.ToBoolean(dr["HasPropertyLocation"].ToString()) == true)
                                {
                                    litop3.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Admin/AddLocation.aspx'>2) Setup Property Location Profile </a>";
                                }
                                else
                                {
                                    litop3.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Admin/AddLocation.aspx'>2) Setup Property Location Profile </a>";
                                }

                                if (!Convert.IsDBNull(dr["HasPropertyUnit"]) && Convert.ToBoolean(dr["HasPropertyUnit"].ToString()) == true)
                                {
                                    litop4.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx'>3) Setup Property Unit Profile </a>";
                                }
                                else
                                {
                                    litop4.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx'>3) Setup Property Unit Profile </a>";
                                }

                                if (!Convert.IsDBNull(dr["HasDocuments"]) && Convert.ToBoolean(dr["HasDocuments"].ToString()) == true)
                                {
                                    litop5.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx'>4) Setup Document Management System </a>";
                                }
                                else
                                {
                                    litop5.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx'>4) Setup Document Management System </a>";
                                }

                                if (!Convert.IsDBNull(dr["HasAccountSystem"]) && Convert.ToBoolean(dr["HasAccountSystem"].ToString()) == true)
                                {
                                    litop6.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Account/AddChartofAccount.aspx'>5) Setup Accounting System Profile </a>";
                                }
                                else
                                {
                                    litop6.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Account/AddChartofAccount.aspx'>5) Setup Accounting System Profile</a>";
                                }

                                if (!Convert.IsDBNull(dr["HasFinishedTenantImport"]) && Convert.ToBoolean(dr["HasFinishedTenantImport"].ToString()) == true)
                                {
                                    litop7.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Resident/ImportTenantProfile.aspx'>6) Setup Existing Tenants Profile Import </a>";
                                }
                                else
                                {
                                    litop7.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Resident/ImportTenantProfile.aspx'>6) Setup Existing Tenants Profile Import</a>";
                                }



                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }


                    int newUser = 0;
                    try
                    {
                        newUser = Convert.ToInt32(Request.QueryString["N"].ToString());
                    }
                    catch (Exception ex)
                    {
                        newUser = 0;
                    }
                    if (newUser > 0)
                    {
                        Utility.DisplayMsg("Registration successful !", this);
                    }

                    if (Session["HasCompletedFullProfile"] != null)
                    {
                        if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == true)
                        {
                            divLinkMenu.Visible = false;
                        }
                        else
                        {
                            divLinkMenu.Visible = true;
                        }
                    }
                }

                
            }
        }
        #region Method Support
        private void ClearControlsTenant()
        {
            hdnAppId.Value = "";
            hdnOwnerId.Value = "";
            hdnPropertyManagerId.Value = "";
            hdnUnitSerial.Value = "";
            hdnLocationId.Value = "";
            hdnLocationName.Value = "";
            txtSecurityDeposit.Text = "";
            txtOneMonthRent.Text = "";
            txtFirstMonthRent.Text = "";
            txtProrate.Text = "";
            txtTotalRent.Text = "";

        }
        private void FillOwner()
        {
            try
            {
                ddlOwner.Items.Clear();
                ddlOwner.AppendDataBoundItems = true;
                ddlOwner.Items.Add(new ListItem("Select Owner", "-1"));
                //List<OwnerProfile> objOwners = new AdminOwnerProfileDA().GetAllOwnersInfo();
                List<OwnerProfile> objOwners = new OwnerProfileDA().GetAllOwnersInfo();
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
        private void ApproveTenant(string sApplicationId)
        {
            try
            {
                Random generator = new Random();
                int r = generator.Next(1, 1000000);
                string sApproveCode = r.ToString().PadLeft(6, '0');

                string sLocationId = (hdnLocationId.Value != null ? hdnLocationId.Value.ToString() : "");
                string sLocationName = (hdnLocationName.Value != null ? hdnLocationName.Value.ToString() : "");
                //  DateTime dDueDate = txtPayment.Text.ToString() != null ? Convert.ToDateTime(txtPayment.Text.ToString()) : DateTime.Now;

                ResidentialTenantSignIn objTenant = new ResidentialAddResponceTemplateDa().GetTenantbyId(sApplicationId);
                if (objTenant != null)
                {
                    objTenant.ApproveStatus = "Approved";
                    objTenant.UpdatedDate = DateTime.Now;
                    objTenant.ApprovalCode = sApproveCode;
                    objTenant.IsActive = true;

                    if (new ResidentialAddResponceTemplateDa().UpdateTenant(objTenant))
                    {


                        TenantRentalFee objTenantFee = new TenantRentalFee();

                        objTenantFee.ApplicationId = sApplicationId;
                        objTenantFee.OwnerId = (hdnOwnerId.Value != null ? hdnOwnerId.Value.ToString() : "");
                        objTenantFee.PropertyManagerId = (hdnPropertyManagerId.Value != null ? hdnPropertyManagerId.Value.ToString() : "");
                        objTenantFee.LocationId = sLocationId;
                        objTenantFee.UnitId = (hdnUnitSerial.Value != null ? hdnUnitSerial.Value.ToString() : "");
                        objTenantFee.CreateDate = DateTime.Now;
                        objTenantFee.UpdatedDate = DateTime.Now;
                        objTenantFee.SecurityDeposit = txtSecurityDeposit.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtSecurityDeposit.Text.ToString().Trim()) : 0;
                        objTenantFee.MonthlyRent = txtOneMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtOneMonthRent.Text.ToString().Trim()) : 0;
                        objTenantFee.ProrateAmount = txtProrate.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtProrate.Text.ToString().Trim()) : 0;
                        objTenantFee.FirstMonthRent = txtFirstMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtFirstMonthRent.Text.ToString().Trim()) : 0;
                        objTenantFee.Total = txtTotalRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtTotalRent.Text.ToString().Trim()) : 0;
                        //objTenantFee.MonthlyPaymentDueDate = txtPayment.Text.ToString() != null ? txtPayment.Text.ToString() : "";
                        objTenantFee.MonthlyPaymentDueDate = ddlPayment.SelectedValue.ToString() != null ? ddlPayment.SelectedValue.ToString() : "";

                        TenantRentalFee objTenantRentalFee = new ResidentialAddResponceTemplateDa().GetTenantRentalFeeById(sApplicationId);
                        if (objTenantRentalFee != null && objTenantRentalFee.Id > 0)
                        {
                            objTenantFee.Id = objTenantRentalFee.Id;
                            objTenantFee.CreateDate = objTenantRentalFee.CreateDate;

                            if (new ResidentialAddResponceTemplateDa().UpdateTenantRentalFee(objTenantFee))
                            {
                            }
                        }
                        else
                        {
                            if (new ResidentialAddResponceTemplateDa().InsertTenantRentalFee(objTenantFee))
                            {
                            }
                        }

                        bool nextStep = new ResidentialAddResponceTemplateDa().SetUpStep(objTenant.SerialId, objTenant.UnitId, "Residential");

                        bool bIsSent = SendApproveEmailToTenant(objTenant.FirstName, objTenant.LastName, objTenant.EmailId, objTenant.ApprovalCode, sLocationName, objTenant.UnitId, objTenant.SerialId);
                        if (bIsSent)
                        {
                            ClearControlsTenant();
                            FillTenantList(ddlOwner.SelectedValue, "", "", "", "");
                            Utility.DisplayMsg("Your Application is approved. Please check email to activate your account. Thanks for staying with us !!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Failed to send email. Please contact with support team !!", this);
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("Approve Failed !!", this);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public bool SendApproveEmailToTenant(string sFirstName, string sLastName, string sEmail, string sApproveCode, string sLocationName, string sUnitId, string sTenantId)
        {
            bool IsSentSuccessful = false;
            try
            {
                String strMailServer = string.Empty;
                String strMailUser = string.Empty;
                String strMailPassword = string.Empty;
                String strMailPort = System.Configuration.ConfigurationManager.AppSettings.Get("strMailPort");
                String isMailLive = System.Configuration.ConfigurationManager.AppSettings.Get("isMailLive");

                strMailUser = System.Configuration.ConfigurationManager.AppSettings.Get("strMailUser");
                strMailPassword = System.Configuration.ConfigurationManager.AppSettings.Get("strMailPassword");
                strMailServer = System.Configuration.ConfigurationManager.AppSettings.Get("strMailServer");

                if (isMailLive == "true")
                {
                    objSmtpClient = new System.Net.Mail.SmtpClient(strMailServer, Convert.ToInt32(strMailPort));
                    objSmtpClient.UseDefaultCredentials = false;
                    objSmtpClient.Credentials = new System.Net.NetworkCredential(strMailUser, strMailPassword);
                }
                else
                {
                    objSmtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    objSmtpClient.UseDefaultCredentials = false;
                    objSmtpClient.EnableSsl = true;
                    objSmtpClient.Credentials = new System.Net.NetworkCredential("info.visaformalaysia@gmail.com", "admin_321");
                    // objSmtpClient.Credentials = new System.Net.NetworkCredential("adi.email.test@gmail.com", "adiadmin123");
                }

                string from_address = "";
                string to_address = "";
                string bcc_address = "";

                from_address = System.Configuration.ConfigurationManager.AppSettings.Get("fromAddress");

                try
                {
                    to_address = sEmail;
                }
                catch (Exception e)
                {
                    to_address = System.Configuration.ConfigurationManager.AppSettings.Get("toAddress");
                }

                try
                {
                    bcc_address = System.Configuration.ConfigurationManager.AppSettings.Get("bccAddress");
                }
                catch (Exception e)
                {
                    bcc_address = "sbutcher@eproperty365.com";
                }

                objMailMessage = new System.Net.Mail.MailMessage();
                objMailMessage.From = new System.Net.Mail.MailAddress(from_address, "Support");
                objMailMessage.To.Add(new System.Net.Mail.MailAddress(to_address));
                objMailMessage.Bcc.Add(bcc_address);
                objMailMessage.Subject = "Approval";
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Body = this.ApproveEmailHtml(sFirstName, sLastName, sEmail, sApproveCode, sLocationName, sUnitId, sTenantId).ToString();
                objSmtpClient.Send(objMailMessage);
                IsSentSuccessful = true;

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if ((objSmtpClient == null) == false)
                {
                    objSmtpClient = null;
                }

                if ((objMailMessage == null) == false)
                {
                    objMailMessage.Dispose();
                    objMailMessage = null;
                }
            }

            return IsSentSuccessful;
        }

        public System.Text.StringBuilder ApproveEmailHtml(string sFirstName, string sLastName, string sEmail, string sApproveCode, string sLocationName, string sUnitId, string sTenantId)
        {
            System.Text.StringBuilder emailbody = new System.Text.StringBuilder();
            string sWeb = System.Configuration.ConfigurationManager.AppSettings.Get("WebURL");
            string sSignInUrl = Utility.WebUrl + "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit_Login.aspx?ResidentialUnitSerial=" + sUnitId + "&&TenentId=" + sTenantId;
            try
            {
                emailbody.Append("<table>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><img  alt='eProperty365' width='120' src='" + sWeb + "/Images/logo.png'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2' style='text-align:Left;font-size:14px;font-weight:bold;color:0000cc;'>Dear " + sFirstName.Trim() + ",</td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>The owner has approved your application for " + sLocationName + ", " + sUnitId + " as long as you fill out the following agreements and they are signed and the payments are paid.Below is a click to completed the rental agreenet and any other documents that need you to fill out </p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p><a href='" + sSignInUrl + "' target='_blank'>Click on link to fill out the Residential Tenant  Step 4 sign & Pay Deposit form  </a></p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>YOUR APPROVAL CODE IS: <b>" + sApproveCode + " </b></p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>You will need to enter this code into login</p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'>Best regards</td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>Management</p> </td></tr>");
                emailbody.Append("</table>");



            }
            catch (Exception ex)
            {
            }
            return emailbody;
        }

        private void DisApproveTenant(string sApplicationId)
        {
            try
            {
                string sLocationId = (hdnLocationId.Value != null ? hdnLocationId.Value.ToString() : "");
                string sLocationName = (hdnLocationName.Value != null ? hdnLocationName.Value.ToString() : "");

                ResidentialTenantSignIn objTenant = new ResidentialAddResponceTemplateDa().GetTenantbyId(sApplicationId);
                if (objTenant != null)
                {
                    objTenant.ApproveStatus = "Disapproved";
                    objTenant.UpdatedDate = DateTime.Now;
                    objTenant.ApprovalCode = "";

                    if (new ResidentialAddResponceTemplateDa().UpdateTenant(objTenant))
                    {
                        TenantRentalFee objTenantRentalFee = new ResidentialAddResponceTemplateDa().GetTenantRentalFeeById(sApplicationId);
                        if (objTenantRentalFee != null && objTenantRentalFee.Id > 0)
                        {
                            objTenantRentalFee.Id = objTenantRentalFee.Id;
                            objTenantRentalFee.UpdatedDate = DateTime.Now;
                            objTenantRentalFee.SecurityDeposit = 0;
                            objTenantRentalFee.MonthlyRent = 0;
                            objTenantRentalFee.ProrateAmount = 0;
                            objTenantRentalFee.FirstMonthRent = 0;
                            objTenantRentalFee.Total = 0;
                            if (new ResidentialAddResponceTemplateDa().UpdateTenantRentalFee(objTenantRentalFee))
                            {
                            }
                        }

                        bool bIsSent = SendDisApproveEmailToTenant(objTenant.FirstName, objTenant.LastName, objTenant.EmailId, sLocationName, objTenant.UnitId);
                        if (bIsSent)
                        {
                            ClearControlsTenant();
                            FillTenantList(ddlOwner.SelectedValue, "", "", "", "");
                            Utility.DisplayMsg("Your Application is disapproved. Thanks for applying !!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Failed to send email. Please contact with support team !!", this);
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("DisApprove Failed !!", this);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public bool SendDisApproveEmailToTenant(string sFirstName, string sLastName, string sEmail, string sLocationName, string sUnitId)
        {
            bool IsSentSuccessful = false;
            try
            {
                String strMailServer = string.Empty;
                String strMailUser = string.Empty;
                String strMailPassword = string.Empty;
                String strMailPort = System.Configuration.ConfigurationManager.AppSettings.Get("strMailPort");
                String isMailLive = System.Configuration.ConfigurationManager.AppSettings.Get("isMailLive");

                strMailUser = System.Configuration.ConfigurationManager.AppSettings.Get("strMailUser");
                strMailPassword = System.Configuration.ConfigurationManager.AppSettings.Get("strMailPassword");
                strMailServer = System.Configuration.ConfigurationManager.AppSettings.Get("strMailServer");

                if (isMailLive == "true")
                {
                    objSmtpClient = new System.Net.Mail.SmtpClient(strMailServer, Convert.ToInt32(strMailPort));
                    objSmtpClient.UseDefaultCredentials = false;
                    objSmtpClient.Credentials = new System.Net.NetworkCredential(strMailUser, strMailPassword);
                }
                else
                {
                    objSmtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    objSmtpClient.UseDefaultCredentials = false;
                    objSmtpClient.EnableSsl = true;
                    objSmtpClient.Credentials = new System.Net.NetworkCredential("info.visaformalaysia@gmail.com", "admin_321");
                    // objSmtpClient.Credentials = new System.Net.NetworkCredential("adi.email.test@gmail.com", "adiadmin123");
                }

                string from_address = "";
                string to_address = "";
                string bcc_address = "";

                from_address = System.Configuration.ConfigurationManager.AppSettings.Get("fromAddress");

                try
                {
                    to_address = sEmail;
                }
                catch (Exception e)
                {
                    to_address = System.Configuration.ConfigurationManager.AppSettings.Get("toAddress");
                }

                try
                {
                    bcc_address = System.Configuration.ConfigurationManager.AppSettings.Get("bccAddress");
                }
                catch (Exception e)
                {
                    bcc_address = "sbutcher@eproperty365.com";
                }

                objMailMessage = new System.Net.Mail.MailMessage();
                objMailMessage.From = new System.Net.Mail.MailAddress(from_address, "Support");
                objMailMessage.To.Add(new System.Net.Mail.MailAddress(to_address));
                objMailMessage.Bcc.Add(bcc_address);
                objMailMessage.Subject = "Disappoved";
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Body = this.DisApproveEmailHtml(sFirstName, sLastName, sEmail, sLocationName, sUnitId).ToString();
                objSmtpClient.Send(objMailMessage);
                IsSentSuccessful = true;

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if ((objSmtpClient == null) == false)
                {
                    objSmtpClient = null;
                }

                if ((objMailMessage == null) == false)
                {
                    objMailMessage.Dispose();
                    objMailMessage = null;
                }
            }

            return IsSentSuccessful;
        }

        public System.Text.StringBuilder DisApproveEmailHtml(string sFirstName, string sLastName, string sEmail, string sLocationName, string sUnitId)
        {
            System.Text.StringBuilder emailbody = new System.Text.StringBuilder();
            string sWeb = System.Configuration.ConfigurationManager.AppSettings.Get("WebURL");

            try
            {
                emailbody.Append("<table>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><img  alt='eProperty365' width='120' src='" + sWeb + "/Images/logo.png'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2' style='text-align:Left;font-size:14px;font-weight:bold;color:0000cc;'>Dear " + sFirstName.Trim() + ",</td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>We regret to information you that your application for  " + sLocationName + ", " + sUnitId + " has been disapproved. Thank you for applying and wish you good </p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'>Best regards</td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>Management</p> </td></tr>");
                emailbody.Append("</table>");
            }
            catch (Exception ex)
            {
            }
            return emailbody;
        }


        private void FillTenantList(string ownerId, string propertyManagerId, string locationId, string unitId, string tenantId)
        {
            try
            {
                List<usp_GetTenantApplication_Result> obj = null;
                obj = new ResidentialAddResponceTemplateDa().GetResidentTenantsBySearch(ownerId, propertyManagerId, locationId, unitId, tenantId);
                

                if(obj != null)
                {
                    if(obj.Count > 0)
                    {
                        gvLocation.DataSource = obj;
                        gvLocation.DataBind();

                        divBottom.Visible = true;
                    }
                    else
                    {
                        divBottom.Visible = false;
                    }
                }
                else
                {
                    divBottom.Visible = false;
                }

            }
            catch (Exception ex)
            {
            }

        }

        private void FillChartData(string ownerId)
        {
            try
            {
                List<GetOwnerDashboardData_Result> objList = null;
                objList = new ResidentialAddResponceTemplateDa().GetOwnerDashboardData(ownerId);

                if (objList != null)
                {
                    if (objList.Count > 0)
                    {
                        GetOwnerDashboardData_Result obj = objList[0];

                        if (obj != null)
                        {
                            if (obj.TotalUnit >= 0)
                            {
                                lblProperty.Text = obj.TotalUnit.ToString();
                                lblUnit.Text = obj.TotalUnit.ToString();
                            }
                            if (obj.YTDIncome > 0)
                            {
                                lblYTDIncome.Text = obj.YTDIncome.ToString("#.00");
                            }
                            else
                            {
                                lblYTDIncome.Text = "0";
                            }

                            if (obj.YTDExpense > 0)
                            {
                                lblYTDExpenses.Text = obj.YTDExpense.ToString("#.00");
                            }
                            else
                            {
                                lblYTDExpenses.Text = "0";
                            }

                            if (obj.MTDIncome > 0)
                            {
                                lblMTDIncome.Text = obj.MTDIncome.ToString("#.00");
                            }
                            else
                            {
                                lblMTDIncome.Text = "0";
                            }

                            if (obj.MTDExpense > 0)
                            {
                                lblMTDExpenses.Text = obj.MTDExpense.ToString("#.00");
                            }
                            else
                            {
                                lblMTDExpenses.Text = "0";
                            }


                            divChart.Visible = true;
                        }
                        else
                        {
                            divChart.Visible = false;
                        }
                    }
                    else
                    {
                        divChart.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        #endregion
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

                if (sOwnerId != "")
                {
                    FillTenantList(sOwnerId, sPropertyManagerId, sLocationId, sUnitId, sTenantId);
                }
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

                    spanAppId.InnerText = " Application ID (" + hdApplicationId.Text + ")";

                    var objTenantFee = new ResidentialAddResponceTemplateDa().GetTenantRentalFeeById(hdApplicationId.Text.ToString());

                    if (objTenantFee != null)
                    {
                        txtSecurityDeposit.Text = objTenantFee.SecurityDeposit != null ? Convert.ToDecimal(objTenantFee.SecurityDeposit).ToString("#.00") : "";
                        txtOneMonthRent.Text = objTenantFee.MonthlyRent != null ? Convert.ToDecimal(objTenantFee.MonthlyRent).ToString("#.00") : "";
                        txtProrate.Text = objTenantFee.ProrateAmount != null ? Convert.ToDecimal(objTenantFee.ProrateAmount).ToString("#.00") : "";
                        txtFirstMonthRent.Text = objTenantFee.FirstMonthRent != null ? Convert.ToDecimal(objTenantFee.FirstMonthRent).ToString("#.00") : "";
                        txtTotalRent.Text = objTenantFee.Total != null ? Convert.ToDecimal(objTenantFee.Total).ToString("#.00") : "";
                        ddlPayment.SelectedValue = objTenantFee.MonthlyPaymentDueDate != null ? objTenantFee.MonthlyPaymentDueDate : "1";
                        //txtPayment.Text = objTenantFee.MonthlyPaymentDueDate != null ? Convert.ToDateTime(objTenantFee.MonthlyPaymentDueDate).ToString("MM-dd-yyyy") : "";
                    }

                }

                Label hdOwnerId = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblOwnerId");
                if (!string.IsNullOrEmpty(hdOwnerId.Text))
                {
                    hdnOwnerId.Value = hdOwnerId.Text.ToString();
                }

                Label hdPropertyManagerId = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblPropertyManagerId");
                if (!string.IsNullOrEmpty(hdPropertyManagerId.Text))
                {
                    hdnPropertyManagerId.Value = hdPropertyManagerId.Text.ToString();
                }
                Label hdUnitSerial = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblUnitSerialId");
                if (!string.IsNullOrEmpty(hdUnitSerial.Text))
                {
                    hdnUnitSerial.Value = hdUnitSerial.Text.ToString();
                }
                Label hdLocationId = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblLocationId");
                if (!string.IsNullOrEmpty(hdLocationId.Text))
                {
                    hdnLocationId.Value = hdLocationId.Text.ToString();
                }

                Label hdLocationName = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblLocation");
                if (!string.IsNullOrEmpty(hdLocationName.Text))
                {
                    hdnLocationName.Value = hdLocationName.Text.ToString();
                }

                Label hdTenantPassword = (Label)gvLocation.Rows[row.RowIndex].FindControl("lblTenantPassword");
                if (!string.IsNullOrEmpty(hdTenantPassword.Text))
                {
                    hdnPassword.Value = hdTenantPassword.Text.ToString();
                }
            }
        }

        protected void btnViewApplication_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdnUnitSerial.Value) && !string.IsNullOrEmpty(hdnAppId.Value))
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/Resident/ResidentialTentAddResponceStep1.aspx?ResidentialUnitSerial=" + hdnUnitSerial.Value + "&&TenentId=" + hdnAppId.Value, false);
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

        protected void btnViewSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdnUnitSerial.Value) && !string.IsNullOrEmpty(hdnAppId.Value) && !string.IsNullOrEmpty(hdnPassword.Value))
                {
                    HttpContext.Current.Session["ResidentialUnitSerial"] = hdnUnitSerial.Value;
                    HttpContext.Current.Session["TenentId"] = hdnAppId.Value;
                    HttpContext.Current.Session["TenantPassword"] = hdnPassword.Value;

                    Response.Redirect(Utility.WebUrl + "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx", false);
                }
                else
                {
                    Utility.DisplayMsg("Please select approved application with valid password !! ", this);
                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string sApplicationId = (hdnAppId.Value != null ? hdnAppId.Value.ToString() : "");
                decimal nTotal = 0;
                try
                {
                    nTotal = txtTotalRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtTotalRent.Text.ToString()) : 0;
                }
                catch (Exception)
                {

                }

                if (sApplicationId.ToString().Length > 0)
                {
                    if (nTotal > 0)
                    {
                        ApproveTenant(sApplicationId);
                    }
                    else
                    {
                        Utility.DisplayMsg("Please enter security and rental fee", this);
                    }
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
        protected void btnDisapprove_Click(object sender, EventArgs e)
        {
            try
            {
                string sApplicationId = (hdnAppId.Value != null ? hdnAppId.Value.ToString() : "");
                if (sApplicationId.ToString().Length > 0)
                {
                    DisApproveTenant(sApplicationId);
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
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClearControlsTenant();
        }

        protected void txtSecurityDeposit_TextChanged(object sender, EventArgs e)
        {
            decimal aSecurity = 0;
            aSecurity = txtSecurityDeposit.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtSecurityDeposit.Text.ToString()) : 0;
            decimal aMonthly = 0;
            aMonthly = txtOneMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtOneMonthRent.Text.ToString()) : 0;
           
            decimal aFirst = 0;
            aFirst = txtFirstMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtFirstMonthRent.Text.ToString()) : aMonthly;
            decimal nTotal = aSecurity + aFirst;
            txtTotalRent.Text = nTotal.ToString("#.00");
        }

        protected void txtProrate_TextChanged(object sender, EventArgs e)
        {
            txtFirstMonthRent.Text = txtProrate.Text.ToString();
            decimal aSecurity = 0;
            aSecurity = txtSecurityDeposit.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtSecurityDeposit.Text.ToString()) : 0;
            decimal aMonthly = 0;
            aMonthly = txtOneMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtOneMonthRent.Text.ToString()) : 0;           
            decimal aFirst = 0;
            aFirst = txtFirstMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtFirstMonthRent.Text.ToString()) : aMonthly;
            decimal nTotal = aSecurity + aFirst;
            txtTotalRent.Text = nTotal.ToString("#.00");
        }

        protected void txtFirstMonthRent_TextChanged(object sender, EventArgs e)
        {
            txtProrate.Text = txtFirstMonthRent.Text.ToString();
            decimal aSecurity = 0;
            aSecurity = txtSecurityDeposit.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtSecurityDeposit.Text.ToString()) : 0;
            decimal aMonthly = 0;
            aMonthly = txtOneMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtOneMonthRent.Text.ToString()) : 0;
            decimal aFirst = 0;
            aFirst = txtFirstMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtFirstMonthRent.Text.ToString()) : aMonthly;
            decimal nTotal = aSecurity + aFirst;
            txtTotalRent.Text = nTotal.ToString("#.00");
            
        }

        protected void txtOneMonthRent_TextChanged(object sender, EventArgs e)
        {
            decimal aSecurity = 0;
            aSecurity = txtSecurityDeposit.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtSecurityDeposit.Text.ToString()) : 0;
            decimal aMonthly = 0;
            aMonthly = txtOneMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtOneMonthRent.Text.ToString()) : 0;           
            decimal aFirst = 0;
            aFirst = txtFirstMonthRent.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtFirstMonthRent.Text.ToString()) : aMonthly;
            decimal nTotal = aSecurity + aFirst;
            txtTotalRent.Text = nTotal.ToString("#.00");
        }

        #endregion
        #region XML Call
        [WebMethod]
        public static string GetStatusReport(ResidentialTenantSignIn Obj)
        {
            try
            {
                var strScreenUser = ConfigurationManager.AppSettings["ScreenUser"];
                var strScreenPassword = ConfigurationManager.AppSettings["ScreenPassword"];

                // var OrderId = new ResidentialAddResponceTemplateDa().GetOrderId(Obj.SerialId, Obj.UnitId);
                var XMLOperationStatus = new ResidentialAddResponceTemplateDa().GetTenantBackgroundStatus(Obj.SerialId, Obj.UnitId);

                if (XMLOperationStatus != null && XMLOperationStatus.OrderId > 0)
                {
                    XElement BackgroundCheck = new XElement("BackgroundCheck", new XAttribute("userId", strScreenUser),
                        new XAttribute("password", strScreenPassword));
                    // <BackgroundSearchPackage action="submit" type="demo product"> 
                    XElement BackgroundSearchPackage = new XElement("BackgroundSearchPackage",
                        new XAttribute("action", "status"));
                    //BackgroundCheck.Add(BackgroundSearchPackage);
                    XElement OrderIdBack = new XElement("OrderId", XMLOperationStatus.OrderId);
                    BackgroundSearchPackage.Add(OrderIdBack);
                    BackgroundCheck.Add(BackgroundSearchPackage);


                    string xmlMessage = BackgroundCheck.ToString();
                    string url = "https://lightning.instascreen.net/send/interchange";
                    // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); //CreateWebRequest(url);

                    byte[] requestInFormOfBytes = System.Text.Encoding.ASCII.GetBytes(xmlMessage);
                    request.Headers.Add(@"SOAP:Action");
                    request.Method = "POST";
                    request.ContentType = "text/xml;charset=utf-8";
                    request.ContentLength = requestInFormOfBytes.Length;
                    request.Accept = "text/xml";

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
                    requestStream.Close();

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader respStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                    string receivedResponse = respStream.ReadToEnd(); // You will get the responce in this line



                    var xDoc = XDocument.Parse(receivedResponse);
                    var BackgroundReportPackage = xDoc.Root.Element("BackgroundReportPackage");
                    var ScreeningStatus = BackgroundReportPackage.Element("ScreeningStatus");
                    var OrderStatus = ScreeningStatus.Element("OrderStatus");
                    var StatusVal = OrderStatus.Value;
                    var Status = StatusVal.ToString().Split(':')[1];
                    var ReportURLLink = BackgroundReportPackage.Element("ReportURL");
                    // with Descendents

                    var ReportURl = ReportURLLink.Value;

                    respStream.Close();
                    response.Close();
                    XMLStatusResponce obj = new XMLStatusResponce
                    {
                        Status = Status,
                        ReportURl = ReportURl
                    };

                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(obj);
                    return json;
                }
                else if (XMLOperationStatus != null && XMLOperationStatus.OrderId == 0)
                {
                    XMLStatusResponce obj1 = new XMLStatusResponce
                    {
                        Status = "error",
                        ReportURl = XMLOperationStatus.ErrorCode
                    };

                    var jsonSerialiser1 = new JavaScriptSerializer();
                    var json1 = jsonSerialiser1.Serialize(obj1);
                    return json1;
                }

            }
            catch (Exception ex)
            {

            }

            XMLStatusResponce obj2 = new XMLStatusResponce
            {
                Status = "error",
                ReportURl = " Report Not Found"
            };

            var jsonSerialiser2 = new JavaScriptSerializer();
            var json2 = jsonSerialiser2.Serialize(obj2);
            return json2;

        }

        //public static string GetStatusReport(ResidentialTenantSignIn Obj)
        //{
        //    try
        //    {
        //        var strScreenUser = ConfigurationManager.AppSettings["ScreenUser"];
        //        var strScreenPassword = ConfigurationManager.AppSettings["ScreenPassword"];

        //        var OrderId= new ResidentialAddResponceTemplateDa().GetOrderId(Obj.SerialId, Obj.UnitId);
        //        if(OrderId > 0)
        //        {
        //            XElement BackgroundCheck = new XElement("BackgroundCheck", new XAttribute("userId", strScreenUser),
        //      new XAttribute("password", strScreenPassword));
        //            // <BackgroundSearchPackage action="submit" type="demo product"> 
        //            XElement BackgroundSearchPackage = new XElement("BackgroundSearchPackage",
        //                new XAttribute("action", "status"));
        //            //BackgroundCheck.Add(BackgroundSearchPackage);
        //            XElement OrderIdBack = new XElement("OrderId", OrderId);
        //            BackgroundSearchPackage.Add(OrderIdBack);
        //            BackgroundCheck.Add(BackgroundSearchPackage);


        //            string xmlMessage = BackgroundCheck.ToString();
        //            string url = "https://lightning.instascreen.net/send/interchange";
        //            // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); //CreateWebRequest(url);

        //            byte[] requestInFormOfBytes = System.Text.Encoding.ASCII.GetBytes(xmlMessage);
        //            request.Headers.Add(@"SOAP:Action");
        //            request.Method = "POST";
        //            request.ContentType = "text/xml;charset=utf-8";
        //            request.ContentLength = requestInFormOfBytes.Length;
        //            request.Accept = "text/xml";

        //            Stream requestStream = request.GetRequestStream();
        //            requestStream.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
        //            requestStream.Close();

        //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //            StreamReader respStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
        //            string receivedResponse = respStream.ReadToEnd(); // You will get the responce in this line



        //            var xDoc = XDocument.Parse(receivedResponse);
        //            var BackgroundReportPackage = xDoc.Root.Element("BackgroundReportPackage");
        //            var ScreeningStatus = BackgroundReportPackage.Element("ScreeningStatus");
        //            var OrderStatus = ScreeningStatus.Element("OrderStatus");
        //            var StatusVal = OrderStatus.Value;
        //            var Status = StatusVal.ToString().Split(':')[1];
        //            var ReportURLLink = BackgroundReportPackage.Element("ReportURL");
        //            // with Descendents

        //            var ReportURl = ReportURLLink.Value;

        //            respStream.Close();
        //            response.Close();
        //            XMLStatusResponce obj = new XMLStatusResponce
        //            {
        //                Status = Status,
        //                ReportURl = ReportURl
        //            };

        //            var jsonSerialiser = new JavaScriptSerializer();
        //            var json = jsonSerialiser.Serialize(obj);
        //            return json;
        //        }

        //    }
        //    catch (Exception ex)
        //    {                    

        //    }

        //    return "";
        //}

        #endregion
    }
}