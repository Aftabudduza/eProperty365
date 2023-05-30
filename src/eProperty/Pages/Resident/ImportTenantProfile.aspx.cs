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
using PropertyService.ViewModel;
using PropertyService.DA.Admin.ResidentialTenent;
using System.Data;

namespace eProperty.Pages.Resident
{
    public partial class ImportTenantProfile : System.Web.UI.Page
    {
        private System.Net.Mail.SmtpClient objSmtpClient;
        private System.Net.Mail.MailMessage objMailMessage;
        public String errStr = String.Empty;
        private Regex reEmail = new Regex("^(?:[0-9A-Z_-]+(?:\\.[0-9A-Z_-]+)*@[0-9A-Z-]+(?:\\.[0-9A-Z-]+)*(?:\\.[A-Z]{2,4}))$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["TenantImport"] = null;
                FillUsers();
                FillDropdowns();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            errStr = string.Empty;
            errStr = Validate_Control();
            DateTime dLeaseDate = txtLease.Text.ToString() != null ? Convert.ToDateTime(txtLease.Text.ToString()) : DateTime.Now;
            //DateTime dDueDate  = txtPayment.Text.ToString() != null ? Convert.ToDateTime(txtPayment.Text.ToString()) : DateTime.Now;
            // string dDueDate = txtPayment.Text.ToString() != null ? txtPayment.Text.ToString() : ""; ddlPayment
            string dDueDate = ddlPayment.SelectedValue;

            if (errStr.Length <= 0)
            {
                try
                {
                    ResidentialTenantSignIn objTenant = new ResidentialTenantSignIn();
                    objTenant = SetData(ddlUnit.SelectedValue.ToString(), txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtEmail.Text.ToString());
                    objTenant.IsActive = true;
                    if (objTenant.Id <= 0)
                    {
                        if (new ResidentialAddResponceTemplateDa().InsertTenant(objTenant))
                        {
                            if(SaveRentalFee(objTenant.SerialId, txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtEmail.Text.ToString(), ddlLocation.SelectedValue.ToString(),ddlUnit.SelectedValue.ToString(), txtSecurityDeposit.Text.ToString(), txtOneMonthRent.Text.ToString(), txtOther.Text.ToString(), dLeaseDate, dDueDate))
                            {                               
                                FillUsers();
                                ClearControls();
                                Utility.DisplayMsg("Tenant Added successfully!", this);
                            }                           
                        }
                        else
                        {
                            Utility.DisplayMsg("Tenant not Added!", this);
                        }
                    }
                    else
                    {
                        if (new ResidentialAddResponceTemplateDa().UpdateTenant(objTenant))
                        {
                            if (SaveRentalFee(objTenant.SerialId, txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtEmail.Text.ToString(), ddlLocation.SelectedValue.ToString(), ddlUnit.SelectedValue.ToString(), txtSecurityDeposit.Text.ToString(), txtOneMonthRent.Text.ToString(), txtOther.Text.ToString(), dLeaseDate, dDueDate))
                            {
                                FillUsers();
                                ClearControls();
                                Utility.DisplayMsg("Tenant Added successfully!", this);
                            }
                        }
                        else
                        {
                            Utility.DisplayMsg("Tenant not Added!", this);
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
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int nTotal = 0;          
                string username = "";
                List<TenantImportModel> objTenants = new List<TenantImportModel>();

                if (Session["TenantImport"] != null)
                {
                    objTenants = (List<TenantImportModel>)Session["TenantImport"];
                }

                if (Session["UserObject"] != null)
                {
                    username = ((UserProfile)Session["UserObject"]).Username;
                }

                string SQL = " update UserProfile set HasFinishedTenantImport = 1  where Username = '" + username + "' ";             
              

                if(objTenants != null && objTenants.Count > 0)
                {
                    foreach(TenantImportModel objViewTenant in objTenants)
                    {
                        bool bIsSent = SendEmailToTenant(objViewTenant.EmailAddress, objViewTenant.SerialId, objViewTenant.UnitId);
                        if (bIsSent)
                        {
                            nTotal = nTotal + 1;
                        }
                    }
                }

                if(nTotal > 0)
                {
                    Utility.RunCMD(SQL);
                    Utility.RunCMDMain(SQL);
                  //  Utility.DisplayMsg("Email Sent To Tenant !!", this);
                    if (Session["HasCompletedFullProfile"] != null)
                    {
                        if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == false)
                        {
                            Utility.DisplayMsgAndRedirect("Email Sent To Tenant!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                        }
                        else
                        {
                            Utility.DisplayMsgAndRedirect("Email Sent To Tenant!", this, Utility.WebUrl + "/Pages/DashboardAdmin.aspx");
                        }
                    }
                    else
                    {
                        Utility.DisplayMsgAndRedirect("Email Sent To Tenant!", this, Utility.WebUrl + "/Pages/DashboardOwner.aspx");
                    }
                }

            }
            catch (Exception ex1)
            {
                Utility.DisplayMsg(ex1.Message.ToString(), this);
            }
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            ImportFromCSV();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvContactList.Rows[row.RowIndex].FindControl("lblTenantId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                try
                {
                    List<TenantImportModel> objTenants = new List<TenantImportModel>();

                    if (Session["TenantImport"] != null)
                    {
                        objTenants = (List<TenantImportModel>)Session["TenantImport"];
                    }
                    if (objTenants != null && objTenants.Count > 0)
                    {
                        var objViewTenant = objTenants.Find(x => x.SerialId == hdId.Text.Trim());
                        if (objViewTenant != null)
                        {
                            objTenants.Remove(objViewTenant);
                            Session["TenantImport"] = objTenants;
                        }

                        FillUsers();
                    }
                }
                catch (Exception ex)
                {
                }               
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
            //txtLocation.Text = "";
            //txtUnit.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtSecurityDeposit.Text = "";
            txtOneMonthRent.Text = "";
            txtOther.Text = "";
            txtLease.Text = "";
            //txtPayment.Text = "";
        }
        public string Validate_Control()
        {
            try
            {
                if ((ddlLocation.SelectedValue.ToString().Length) <= 0)
                {
                    errStr += "Please enter Location ID" + Environment.NewLine;
                }
                if ((ddlUnit.SelectedValue.ToString().Length) <= 0)
                {
                    errStr += "Please enter Unit ID" + Environment.NewLine;
                }
                if ((txtFirstName.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter First Name" + Environment.NewLine;
                }
                if ((txtLastName.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Last Name" + Environment.NewLine;
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
                if ((txtSecurityDeposit.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Security Deposit amount" + Environment.NewLine;
                }
                if ((txtOneMonthRent.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter One Month Rent amount" + Environment.NewLine;
                }

                if ((txtOther.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Other amount" + Environment.NewLine;
                }
                if ((txtLease.Text.ToString().Length) <= 0)
                {
                    errStr += "Please enter Lease Sign Date" + Environment.NewLine;
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
        private ResidentialTenantSignIn SetData(string unitId, string firstName, string lastName, string email)
        {
            var resultobj = new ResidentialTenantSignIn();
            var obj = new ResidentialTenantSignIn();

            var serialid = "";
            var applicationCode = "";

            try
            {
               

                if (!string.IsNullOrEmpty(unitId) && unitId != string.Empty)
                {
                    obj.UnitId = unitId.Trim();
                }
                else
                {
                    obj.UnitId = "";
                }

                if (!string.IsNullOrEmpty(firstName) && firstName != string.Empty)
                {
                    obj.FirstName = firstName.Trim();
                }
                else
                {
                    obj.FirstName = "";
                }

                if (!string.IsNullOrEmpty(lastName) && lastName != string.Empty)
                {
                    obj.LastName = lastName.Trim();
                }
                else
                {
                    obj.LastName = "";
                }

                if (!string.IsNullOrEmpty(email) && email != string.Empty)
                {
                    obj.EmailId = email.ToLower().Trim();
                }
                else
                {
                    obj.EmailId = "";
                }

                obj.IsActive = true;

                resultobj = new ResidentialAddResponceTemplateDa().GetTenantbyUnitAndEmail(obj.UnitId, obj.EmailId);

                if (resultobj != null)
                {
                    obj.Id = resultobj.Id;
                    obj.SerialId = resultobj.SerialId;
                    obj.PhoneNumber = resultobj.PhoneNumber;
                    obj.ApprovalCode = resultobj.ApprovalCode;
                    obj.Password = resultobj.Password;
                    obj.CreateDate = resultobj.CreateDate;
                    obj.ApplicationCode = resultobj.ApplicationCode;
                    obj.ApproveStatus = resultobj.ApproveStatus != null ? resultobj.ApproveStatus : "";
                    obj.UpdatedDate = DateTime.Now;
                }
                else
                {
                    serialid = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialTenantSerialFromThirdParty");
                    applicationCode = new ResidentialUnitDa().MakeAutoGenLocation("1", "ResidentialTenantApproveCode");

                    obj.SerialId = serialid;
                    obj.PhoneNumber = "";
                    obj.ApprovalCode = "";
                    obj.Password = "";
                    obj.CreateDate = DateTime.Now;
                    obj.ApplicationCode = applicationCode;
                    obj.ApproveStatus = "";
                    obj.UpdatedDate = DateTime.Now;
                }

            }
            catch (Exception e)
            {
            }


            return obj;
        }

        private bool SaveRentalFee(string serialId, string firstName, string lastName, string email, string locationId, string unitId, string securityDeposit, string oneMonthRent, string otherRent, DateTime LeaseDate, string MonthlyDueDate)
        {
            TenantRentalFee objTenantFee = new TenantRentalFee();
            List<TenantImportModel> objTenants = new List<TenantImportModel>();

            if (Session["TenantImport"] != null)
            {
                objTenants = (List<TenantImportModel>)Session["TenantImport"];
            }

            try
            {               

                objTenantFee.ApplicationId = serialId;               
                objTenantFee.LocationId = locationId;
                objTenantFee.UnitId = unitId;
                objTenantFee.CreateDate = DateTime.Now;
                objTenantFee.UpdatedDate = DateTime.Now;
                objTenantFee.SecurityDeposit = securityDeposit.Trim().Length > 0 ? Convert.ToDecimal(securityDeposit.Trim()) : 0;
                objTenantFee.MonthlyRent = oneMonthRent.Trim().Length > 0 ? Convert.ToDecimal(oneMonthRent.Trim()) : 0;
                objTenantFee.ProrateAmount = otherRent.Trim().Length > 0 ? Convert.ToDecimal(otherRent.Trim()) : 0;
                objTenantFee.FirstMonthRent = Convert.ToDecimal(objTenantFee.ProrateAmount) > 0 ? Convert.ToDecimal(objTenantFee.ProrateAmount) : Convert.ToDecimal(objTenantFee.MonthlyRent);
                objTenantFee.Total = objTenantFee.SecurityDeposit +  objTenantFee.FirstMonthRent;
                objTenantFee.LeaseSignedDate = LeaseDate != null ? Convert.ToDateTime(LeaseDate) : DateTime.Now;
                objTenantFee.MonthlyPaymentDueDate = MonthlyDueDate;

                var objResidentialMasterTable = new ResidentialAddResponceTemplateDa().GetOwnerPropertyAndLocationInfo(objTenantFee.UnitId);
                if(objResidentialMasterTable != null)
                {
                    objTenantFee.OwnerId = objResidentialMasterTable.OwnerId;
                    objTenantFee.PropertyManagerId = objResidentialMasterTable.PropertyManagerSerialId;
                }

                TenantImportModel objTenantModel = new TenantImportModel();
                objTenantModel.SerialId = serialId;
                objTenantModel.FirstName = firstName;
                objTenantModel.LastName = lastName;
                objTenantModel.EmailAddress = email;
                objTenantModel.UnitId = objTenantFee.UnitId;
                objTenantModel.LocationId = objTenantFee.LocationId;
                objTenantModel.SecurityDeposit = objTenantFee.SecurityDeposit.ToString();
                objTenantModel.MonthlyRentHeld = objTenantFee.MonthlyRent.ToString();
                objTenantModel.OtherAmountHeld = objTenantFee.FirstMonthRent.ToString();
                objTenantModel.LeaseSignDate = objTenantFee.LeaseSignedDate != null ? Convert.ToDateTime(objTenantFee.LeaseSignedDate).ToString("MM-dd-yyyy") : "";
                objTenantModel.MonthlyPayDueDate = objTenantFee.MonthlyPaymentDueDate != null ? objTenantFee.MonthlyPaymentDueDate : "";

                TenantRentalFee objTenantRentalFee = new ResidentialAddResponceTemplateDa().GetTenantRentalFeeById(serialId);
                if (objTenantRentalFee != null && objTenantRentalFee.Id > 0)
                {
                    objTenantFee.Id = objTenantRentalFee.Id;
                    objTenantFee.CreateDate = objTenantRentalFee.CreateDate;

                    if (new ResidentialAddResponceTemplateDa().UpdateTenantRentalFee(objTenantFee))
                    {
                        objTenants.Add(objTenantModel);
                        Session["TenantImport"] = objTenants;
                        return true;
                    }
                }
                else
                {
                    if (new ResidentialAddResponceTemplateDa().InsertTenantRentalFee(objTenantFee))
                    {
                        objTenants.Add(objTenantModel);
                        Session["TenantImport"] = objTenants;
                        return true;
                    }
                }

            }
            catch (Exception e)
            {
            }

            return false;
        }

        private void FillUsers()
        {
            try
            {
                List<TenantImportModel> objTenants = new List<TenantImportModel>();

                if (Session["TenantImport"] != null)
                {
                    objTenants = (List<TenantImportModel>)Session["TenantImport"];
                    
                    gvContactList.DataSource = objTenants;
                    gvContactList.DataBind();

                }
            }
            catch (Exception e)
            {

            }
        }

        public bool SendEmailToTenant(string sEmail, string sTenantId, string sUnitId)
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
                objMailMessage.Subject = "Creating new tenant account";
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Body = this.ScriptEmailHtml(sTenantId, sUnitId).ToString();
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

        public System.Text.StringBuilder ScriptEmailHtml(string sTenantId, string sUnitId)
        {
            System.Text.StringBuilder emailbody = new System.Text.StringBuilder();
            string sWeb = System.Configuration.ConfigurationManager.AppSettings.Get("WebURL");
            string sSignInUrl = Utility.WebUrl + "/Pages/Resident/AddNewTenantProfile.aspx?m=1&TenentId=" + sTenantId + "&&ResidentialUnitSerial=" + sUnitId;
            try
            {
                emailbody.Append("<table>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><img  alt='eProperty365' width='120' src='" + sWeb + "/Images/logo.png'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2' style='text-align:Left;font-size:14px;font-weight:bold;color:0000cc;'>Dear Tenant,</td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>The owner has decided to use Eproperty365 rental property management software to management the property to make it easier and faster for you. We you to add your basic information to the form that probably provided on your original application. This will allow us to set up your tenant account. you will be able to pay your rent via checking account online.You will also be able you to communicate via Email messenger for any issues you are having including problems with the software. We look forward to serving you. </p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p><a style='color:red;' href='" + sSignInUrl + "' target='_blank'>Click on link to fill out the  form  </a></p> </td></tr>");
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

        private void ImportFromCSV()
        {
            try
            {
                int nTotal = 0;
                List<TenantImportModel> objTenants = new List<TenantImportModel>();

                if (Session["TenantImport"] != null)
                {
                    objTenants = (List<TenantImportModel>)Session["TenantImport"];
                }

                string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\TenantCSV\\");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = this.uplLogo.FileName;
                string csvPath = Path.Combine(filePath, fileName);

                try
                {
                    if (System.IO.File.Exists(csvPath))
                    {
                        System.IO.File.Delete(csvPath);
                    }

                }
                catch (Exception ex)
                {
                }

                uplLogo.SaveAs(csvPath);

                //Upload and save the file
                //string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(uplLogo.PostedFile.FileName);
                //uplLogo.SaveAs(csvPath);

                //Create a DataTable.
                DataTable dt = new DataTable();

                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("LocationID", typeof(string)),
                                    new DataColumn("UnitID", typeof(string)),
                                    new DataColumn("FirstName", typeof(string)),
                                    new DataColumn("LastName", typeof(string)),
                                    new DataColumn("EmailAddress", typeof(string)),
                                    new DataColumn("SecurityDeposit", typeof(string)),
                                    new DataColumn("MonthlyRentHeld",typeof(string)),
                                    new DataColumn("OtherAmountHeld", typeof(string)),
                                    new DataColumn("LeaseSignDate", typeof(string)),
                                    new DataColumn("MonthlyPayDueDate", typeof(string))});

                //Read the contents of CSV file.
                string csvData = File.ReadAllText(csvPath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        try
                        {
                            dt.Rows.Add();
                            int i = 0;

                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                        catch (Exception ex2)
                        {

                        }

                    }
                }

                if (dt != null && dt.Rows.Count > 1)
                {
                    dt.Rows.RemoveAt(0);
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            DateTime dLeaseDate = dr["LeaseSignDate"] != null ? Convert.ToDateTime(dr["LeaseSignDate"]) : DateTime.Now;
                            //DateTime dDueDate = dr["MonthlyPayDueDate"] != null ? Convert.ToDateTime(dr["MonthlyPayDueDate"]) : DateTime.Now;
                            string dDueDate = dr["MonthlyPayDueDate"] != null ? dr["MonthlyPayDueDate"].ToString() : "";
                            string sUnitID = dr["UnitID"] != null ? dr["UnitID"].ToString() : "";
                            string sLocationID = dr["LocationID"] != null ? dr["LocationID"].ToString() : "";
                            string sFirstName = dr["FirstName"] != null ? dr["FirstName"].ToString() : "";
                            string sLastName = dr["LastName"] != null ? dr["LastName"].ToString() : "";
                            string sEmailAddress = dr["EmailAddress"] != null ? dr["EmailAddress"].ToString() : "";
                            string sSecurityDeposit = dr["SecurityDeposit"] != null ? dr["SecurityDeposit"].ToString() : "";
                            string sMonthlyRentHeld = dr["MonthlyRentHeld"] != null ? dr["MonthlyRentHeld"].ToString() : "";
                            string sOtherAmountHeld = dr["OtherAmountHeld"] != null ? dr["OtherAmountHeld"].ToString() : "";


                            ResidentialTenantSignIn objTenant = new ResidentialTenantSignIn();
                            objTenant = SetData(sUnitID, sFirstName, sLastName, sEmailAddress);

                            if (objTenant.Id <= 0)
                            {
                                if (new ResidentialAddResponceTemplateDa().InsertTenant(objTenant))
                                {
                                    if (SaveRentalFee(objTenant.SerialId, sFirstName, sLastName, sEmailAddress, sLocationID, sUnitID, sSecurityDeposit, sMonthlyRentHeld, sOtherAmountHeld, dLeaseDate, dDueDate))
                                    {
                                        nTotal = nTotal + 1;
                                    }
                                }

                            }
                            else
                            {
                                if (new ResidentialAddResponceTemplateDa().UpdateTenant(objTenant))
                                {
                                    if (SaveRentalFee(objTenant.SerialId, sFirstName, sLastName, sEmailAddress, sLocationID, sUnitID, sSecurityDeposit, sMonthlyRentHeld, sOtherAmountHeld, dLeaseDate, dDueDate))
                                    {
                                        nTotal = nTotal + 1;
                                    }
                                }

                            }

                        }
                        catch (Exception ex2)
                        {

                        }
                    }
                }



                //Bind the DataTable.
                //gvContactList.DataSource = dt;
                //gvContactList.DataBind();

                FillUsers();

            }
            catch (Exception ex1)
            {
                Utility.DisplayMsg(ex1.Message.ToString(), this);
            }
        }

        private void FillDropdowns()
        {
            try
            {
                if (Session["UserObject"] != null)
                {
                    UserProfile user = (UserProfile)Session["UserObject"];
                    
                    var isAdmin = false;
                    var sOwnerId = "";
                    if (Session["UserObject"] != null)
                    {
                        isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                           ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                           : false;
                        sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null
                          ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId)
                          : "";

                    }
                       

                    if (sOwnerId != null && sOwnerId != "")
                    {
                        ddlLocation.Items.Clear();
                        ddlLocation.AppendDataBoundItems = true;
                        ddlLocation.Items.Add(new ListItem("Select Location", "-1"));  
                        List<Location> objLocations = new LocationDA().GetByOwner(sOwnerId);
                        if (objLocations != null && objLocations.Count > 0)
                        {
                            foreach (Location obj in objLocations)
                            {
                                string sName = obj.LocationName.ToString() + " (" + obj.Serial.ToString() + ")";
                                ddlLocation.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                            }
                        }

                        ddlLocation.DataBind();
                        ddlLocation.SelectedValue = "-1";

                        ddlUnit.Items.Clear();
                        ddlUnit.AppendDataBoundItems = true;
                        ddlUnit.Items.Add(new ListItem("Select Unit", "-1"));
                        List<ResidentialUnit> objResidentialUnits = new ResidentialUnitDa().GetByOwner(sOwnerId);
                        if (objLocations != null && objLocations.Count > 0)
                        {
                            foreach (ResidentialUnit obj in objResidentialUnits)
                            {
                                string sName = obj.UnitName.ToString() + " (" + obj.Serial.ToString() + ")";
                                ddlUnit.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                            }
                        }
                      
                        ddlUnit.DataBind();
                        ddlUnit.SelectedValue = "-1";


                        ddlPayment.Items.Clear();
                        ddlPayment.AppendDataBoundItems = true;
                        for(int i = 1; i <= 30; i ++ )
                        {
                            ddlPayment.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        }
                        ddlPayment.DataBind();

                    }


                }
            }
            catch (Exception ex)
            {
               
            }
        }

        #endregion

    }
}