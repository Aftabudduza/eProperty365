using PropertyService.Admin.DA;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Account
{
    public partial class RecordInvoices : System.Web.UI.Page
    {
        public string sUrl = string.Empty;
        public string _errStr = string.Empty;
        private System.Net.Mail.SmtpClient objSmtpClient;
        private System.Net.Mail.MailMessage objMailMessage;

        #region Events     

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["RId"] = null;
                Session["RecordInvoiceParts"] = null;
                Session["PartId"] = null;
                FillDropdowns();               
                lblPOId.Text = new RecordInvoiceDA().MakeAutoGenLocation("R", "RentalInvoice");
                lblDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MM-dd-yyyy");
                Session["RISerial"] = lblPOId.Text.ToString();
                RefreshPartsGrid(lblPOId.Text.ToString());

                if (Session["OwnerId"] != null)
                {
                    OwnerProfile  objOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
                    string sAddress = string.Empty;
                    if(objOwner != null)
                    {
                        sAddress += (objOwner.CompanyName != null ? objOwner.CompanyName : ((objOwner.FirstName != null ? objOwner.FirstName : "") + " " + (objOwner.LastName != null ? objOwner.LastName : "")))  + " <br />";
                        sAddress += (objOwner.Address != null ? objOwner.Address : "") + " <br />";
                        sAddress += (objOwner.City != null ? objOwner.City : "") + " " + (objOwner.State != null ? objOwner.State : "") + " " + (objOwner.Zip != null ? objOwner.Zip : "") + Environment.NewLine;
                    }

                    spanOwnerAddress.InnerHtml = "<p>" +  sAddress + "</p>";

                    ddlBillTo.Items.Clear();
                    ddlBillTo.AppendDataBoundItems = true;
                    ddlBillTo.Items.Add(new ListItem("Select BillTo", "-1"));

                    List<usp_GetTenantApplication_Result> objTenants = null;
                    objTenants = new ResidentialAddResponceTemplateDa().GetResidentTenantsBySearch(Session["OwnerId"].ToString(), "", "", "", "");

                    if (objTenants != null && objTenants.Count > 0)
                    {
                        foreach (usp_GetTenantApplication_Result obj in objTenants)
                        {
                            string sName = obj.TenantName;
                            ddlBillTo.Items.Add(new ListItem(sName, obj.ApplicationCode.ToString()));
                        }
                    }

                    ddlBillTo.DataBind();
                    ddlBillTo.SelectedValue = "-1";

                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                _errStr = Validate_Control();
                if (_errStr.Length <= 0)
                {
                    try
                    {
                        RecordInvoice obj = new RecordInvoice();
                        obj = SetData(obj);

                        if (SendApproveEmail(spanOwnerAddress.InnerText.ToString(), lblPOId.Text.ToString(), obj))
                        {
                            if (Session["RId"] == null || Session["RId"] == "0")
                            {
                                if (new RecordInvoiceDA(true, false).Insert(obj))
                                {
                                    Session["RId"] = null;
                                    ClearControls();
                                }
                            }
                            else
                            {
                                if (new RecordInvoiceDA().Update(obj))
                                {
                                    ClearControls();
                                }
                            }

                            lblMsg.Text = "Mail sent successfully !";
                            Utility.DisplayMsg("Mail sent successfully!", this);
                        }
                        else
                        {
                            lblMsg.Text = "Mail not sent !";
                            Utility.DisplayMsg("Mail not sent !", this);
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
            catch (Exception ex1)
            {

            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClearControls();           
        }
        protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoType.SelectedItem.Value != null)
            {
                ddlBillTo.Items.Clear();
                ddlBillTo.AppendDataBoundItems = true;
                ddlBillTo.Items.Add(new ListItem("Select BillTo", "-1"));  
                
                if (rdoType.SelectedItem.Value == "Tenant")
                {
                    if (Session["OwnerId"] != null)
                    {
                        List<usp_GetTenantApplication_Result> objTenants = null;
                        objTenants = new ResidentialAddResponceTemplateDa().GetResidentTenantsBySearch(Session["OwnerId"].ToString(), "", "", "", "");
                        if (objTenants != null && objTenants.Count > 0)
                        {
                            foreach (usp_GetTenantApplication_Result obj in objTenants)
                            {
                                string sName = obj.TenantName;
                                ddlBillTo.Items.Add(new ListItem(sName, obj.ApplicationCode.ToString()));
                            }
                        }
                    }
                }
                else if (rdoType.SelectedItem.Value == "Contact")
                {
                    if (Session["OwnerId"] != null)
                    {
                        List<ContactInformation> objContacts = null;
                        objContacts = new ContactInformationDA().GetByOwner(Session["OwnerId"].ToString());
                        if (objContacts != null && objContacts.Count > 0)
                        {
                            foreach (ContactInformation obj in objContacts)
                            {
                                string sName = obj.Name;
                                ddlBillTo.Items.Add(new ListItem(sName, obj.Id.ToString()));
                            }
                        }
                    }
                }

                ddlBillTo.DataBind();
                ddlBillTo.SelectedValue = "-1";
            }
        }
        protected void ddlBillTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoType.SelectedItem.Value == "Tenant")
            {
                try
                {
                    ResidentialTenantSignIn objTenant = null;
                    Residential_Tenant_App_Step2_AgreementNameOf obj = null;

                    objTenant = new ResidentialAddResponceTemplateDa().GetTenantbyId(ddlBillTo.SelectedValue);
                    if (objTenant != null)
                    {
                        obj = new ResidentialAddResponceTemplateDa().GetTenantAgrementNameOf(objTenant.SerialId, objTenant.UnitId);
                    }

                    if (obj != null)
                    {
                        string sName = (obj.FirstName != null ? obj.FirstName : "") + " " + (obj.LastName != null ? obj.LastName : ""); 


                        txtContactName.Text = sName;

                        if (obj.Address != null && obj.Address.ToString() != string.Empty)
                        {
                            txtAddress.Text = obj.Address;
                        }
                       
                        if (obj.Address1 != null && obj.Address1.ToString() != string.Empty)
                        {
                            txtAddress1.Text = obj.Address1;
                        }                       
                        
                        if (obj.State != null && obj.State.ToString() != string.Empty)
                        {
                            ddlState.SelectedValue = obj.State;
                        }
                     
                        if (obj.City != null && obj.City.ToString() != string.Empty)
                        {
                            txtCity.Text = obj.City;
                        }
                       
                        if (obj.ZipCode != null && obj.ZipCode.ToString() != string.Empty)
                        {
                            txtZip.Text = obj.ZipCode;
                        }
                       
                        if (obj.EmailAddress != null && obj.EmailAddress.ToString() != string.Empty)
                        {
                            txtEmail.Text = obj.EmailAddress;
                        }
                       
                        if (obj.MobilePhone != null && obj.MobilePhone.ToString() != string.Empty)
                        {
                            txtPhone.Text = obj.MobilePhone;
                        }
                       
                    }
                }
                catch (Exception ex)
                {

                }
            }
           else if (rdoType.SelectedItem.Value == "Contact")
            {
                try
                {
                    ContactInformation obj = null;
                    obj = new ContactInformationDA().GetbyID(Convert.ToInt32(ddlBillTo.SelectedValue));

                    if (obj != null)
                    {
                        string sName = obj.Name != null ? obj.Name : "";


                        txtContactName.Text = sName;

                        if (obj.Address != null && obj.Address.ToString() != string.Empty)
                        {
                            txtAddress.Text = obj.Address;
                        }

                        if (obj.Address1 != null && obj.Address1.ToString() != string.Empty)
                        {
                            txtAddress1.Text = obj.Address1;
                        }

                        if (obj.State != null && obj.State.ToString() != string.Empty)
                        {
                            ddlState.SelectedValue = obj.State;
                        }

                        if (obj.City != null && obj.City.ToString() != string.Empty)
                        {
                            txtCity.Text = obj.City;
                        }

                        if (obj.Zip != null && obj.Zip.ToString() != string.Empty)
                        {
                            txtZip.Text = obj.Zip;
                        }

                        if (obj.Email != null && obj.Email.ToString() != string.Empty)
                        {
                            txtEmail.Text = obj.Email;
                        }

                        if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
                        {
                            txtPhone.Text = obj.Phone;
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }

        }      

        #endregion

        #region Method      
        private void FillDropdowns()
        {         

            try
            {
                ddlState.Items.Clear();
                ddlState.AppendDataBoundItems = true;
                ddlState.DataSource = new StateDA().GetAllRefStates();
                ddlState.DataTextField = "STATENAME";
                ddlState.DataValueField = "STATE";
                ddlState.DataBind();

            }
            catch (Exception ex)
            {
            }
        }
        private void ClearControls()
        {
            txtContactName.Text = "";
            txtCompanyName.Text = "";
            txtAddress.Text = "";
            txtAddress1.Text = "";          
            txtCity.Text = "";
            txtZip.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            ddlBillTo.SelectedValue = "-1";         
            rdoType.SelectedValue = null;
            gvPart.DataSource = null;
            gvPart.DataBind();
            lblTotal.Text = "";
            lblPOId.Text = new RecordInvoiceDA().MakeAutoGenLocation("R", "RentalInvoice");
        }
        private RecordInvoice SetData(RecordInvoice obj)
        {
            try
            {
                obj = new RecordInvoice();

                if (Session["RId"] != null && Convert.ToInt32(Session["RId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["RId"].ToString());
                    obj = new RecordInvoiceDA().GetbyID(obj.Id);
                }

                if ((!string.IsNullOrEmpty(txtCompanyName.Text.ToString())) && (txtCompanyName.Text.ToString() != string.Empty))
                {
                    obj.PersonCompany = txtCompanyName.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.PersonCompany = "";
                }

                

                if ((!string.IsNullOrEmpty(txtAddress.Text.ToString())) && (txtAddress.Text.ToString() != string.Empty))
                {
                    obj.Address1 = txtAddress.Text.ToString();
                }
                else
                {
                    obj.Address1 = "";
                }
                if ((!string.IsNullOrEmpty(txtAddress1.Text.ToString())) && (txtAddress1.Text.ToString() != string.Empty))
                {
                    obj.Address2 = txtAddress1.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Address2 = "";
                }
               
                if (ddlState.SelectedValue != "-1")
                {
                    obj.State = ddlState.SelectedValue.Trim();
                }
                else
                {
                    obj.State = "";
                }

                if ((!string.IsNullOrEmpty(txtContactName.Text.ToString())) && (txtContactName.Text.ToString() != string.Empty))
                {
                    obj.ContactName = txtContactName.Text.ToString();
                }
                else
                {
                    obj.ContactName = "";
                }

                obj.City = txtCity.Text.ToString().Trim();
                obj.ZipCode = txtZip.Text.ToString().Trim();
                obj.EmailAddress = txtEmail.Text.ToString().Trim();
                obj.PhoneNo = txtPhone.Text.ToString().Trim();

                if (Session["OwnerId"] != null)
                {
                    if (Session["OwnerId"].ToString() != string.Empty)
                    {
                        obj.OwnerId = Session["OwnerId"].ToString();
                    }
                    else
                    {
                        obj.OwnerId = "";
                    }
                }
                else
                {
                    obj.OwnerId = "";
                }

                if (Session["RISerial"] != null)
                {
                    obj.BillNumber = !string.IsNullOrEmpty(lblPOId.Text.ToString()) ? lblPOId.Text.ToString() : Session["RISerial"].ToString();

                }
                else
                {
                    obj.BillNumber = !string.IsNullOrEmpty(lblPOId.Text.ToString()) ? lblPOId.Text.ToString() : new RecordInvoiceDA().MakeAutoGenLocation("R", "RentalInvoice");
                    
                }

                obj.Date = lblDate.Text.ToString();
                obj.CreateDate = DateTime.Now;
                if (ddlBillTo.SelectedValue != "-1")
                {
                    obj.BillTo = ddlBillTo.SelectedValue.Trim();
                }
                else
                {
                    obj.BillTo = "";
                }


                if (rdoType.Items[0].Selected == true)
                {
                    obj.PersonType = "Tenant";
                }
                else if (rdoType.Items[1].Selected == true)
                {
                    obj.PersonType = "Contact";
                }



            }
            catch (Exception ex)
            {
            }

            return obj;
        }      
        public void RefreshPartsGrid(string serial)
        {
            try
            {
                List<RecordInvoiceDetails> objParts = null;
                objParts = new RecordInvoiceDA().GetInvoiceDetailRecordByInvoiceNumber(serial);

                if (objParts != null && objParts.Count > 0)
                {
                    Session["RecordInvoiceParts"] = objParts;
                    gvPart.DataSource = objParts;
                    gvPart.DataBind();

                    lblTotal.Text = "$" + new RecordInvoiceDA().GetTotalAmountByInvoiceNumber(serial).ToString("#.00");
                }
                else
                {
                    gvPart.DataSource = null;
                    gvPart.DataBind();

                    lblTotal.Text = "";
                }

              
            }
            catch (Exception ex)
            {

            }
        }      
        public string Validate_Control()
        {          
           
            if (txtCompanyName.Text.Trim().Length <= 0)
            {
                _errStr += "Please enter company name" + Environment.NewLine;
            }

            if (txtContactName.Text.Trim().Length <= 0)
            {
                _errStr += "Please enter Contact Name" + Environment.NewLine;
            }

            if (txtEmail.Text.Trim().Length <= 0)
            {
                _errStr += "Please enter email address" + Environment.NewLine;
            }
            if (lblPOId.Text.Trim().Length <= 0)
            {
                _errStr += "Invoice Number can not be empty" + Environment.NewLine;
            }

            return _errStr;
        }
        public bool SendApproveEmail(string sBillFrom, string sBillNumber , RecordInvoice objRecordInvoice)
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
                    to_address = objRecordInvoice.EmailAddress;
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
               // objMailMessage.Bcc.Add(bcc_address);
                objMailMessage.Subject = "Email Invoice sent : " + sBillNumber;
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Body = this.ApproveEmailHtml(sBillFrom, sBillNumber, objRecordInvoice).ToString();
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
        public System.Text.StringBuilder ApproveEmailHtml(string sBillFrom, string sBillNumber, RecordInvoice obj)
        {
            System.Text.StringBuilder emailbody = new System.Text.StringBuilder();
            string sWeb = System.Configuration.ConfigurationManager.AppSettings.Get("WebURL");
           // RecordInvoice obj = new RecordInvoiceDA().GetBySerial(sBillNumber);
            List<RecordInvoiceDetails> objDetails = new RecordInvoiceDA().GetInvoiceDetailRecordByInvoiceNumber(sBillNumber);

            try
            {
                emailbody.Append("<table>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><img  alt='eProperty365' width='120' src='" + sWeb + "/Images/logo.png'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2' style='text-align:Left;font-size:14px;font-weight:bold;color:0000cc;'>Dear Customer,</td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p> Your Invoice is below. </p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>Invoice Number IS: <b>" + sBillNumber + " </b></p></td></tr>");
                emailbody.Append("<tr><td colspan='2'>Date:" + obj.Date + "</td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>From: <br /> " + sBillFrom + " </p></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                if(obj != null)
                {
                    emailbody.Append("<tr><td colspan='2'>Person Type: " + obj.PersonType + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>Person/Company: " + obj.PersonCompany + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>Address: " + obj.Address1 + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>Address: " + obj.Address2 + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>City: " + obj.City + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>State: " + obj.State + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>ZipCode: " + obj.ZipCode + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");                   
                    emailbody.Append("<tr><td colspan='2'>Contact Name: " + obj.ContactName + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>Phone Number: " + obj.PhoneNo + "</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>Email Address: " + obj.EmailAddress + "</td></tr>");
                }
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p> Your Invoice Items are: </p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");

                if (objDetails != null && objDetails.Count > 0)
                {
                    emailbody.Append("<tr><td>Qty</td><td>Item</td><td>Description</td><td>Unit Price</td><td>Amount</td></tr>");

                    foreach (RecordInvoiceDetails detail in objDetails)
                    {
                        try
                        {
                            emailbody.Append("<tr><td>" + detail.Qty + "</td><td>" + detail.Title + "</td><td>" + detail.Description + "</td><td>$" + (detail.UnitPrice != null ? Convert.ToDecimal(detail.UnitPrice).ToString("#.00") : "") + "</td><td>$" + (detail.Amount != null ? Convert.ToDecimal(detail.Amount).ToString("#.00") : "") + "</td></tr>");
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    emailbody.Append("<tr><td></td><td></td><td></td><td>Total</td><td>" + lblTotal.Text.ToString() + "</td></tr>");
                }

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


        #endregion

        #region PartEvents  
        protected void btnPartAdd_Click(object sender, EventArgs e)
        {
            try
            {
                RecordInvoiceDetails obj = new RecordInvoiceDetails();
                obj = SetDataPart(obj);

                if (obj.BillNumber == null || obj.BillNumber == "")
                {
                    Utility.DisplayMsg("Invoice Number is invalid !!", this);
                }
                else
                {
                    if (Session["PartId"] == null || Session["PartId"] == "0")
                    {
                        if (new RecordInvoiceDA().InsertPart(obj))
                        {
                            Session["PartId"] = null;
                            ClearControlsPart();
                            RefreshPartsGrid(lblPOId.Text.ToString());
                            Utility.DisplayMsg("Item saved successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Item not saved!", this);
                        }
                    }
                    else
                    {
                        if (new RecordInvoiceDA().UpdatePart(obj))
                        {
                            ClearControlsPart();
                            RefreshPartsGrid(lblPOId.Text.ToString());
                            Utility.DisplayMsg("Item updated successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Item not updated!", this);
                        }
                    }
                }
                
            }
            catch (Exception ex1)
            {

            }
        }       
        protected void btnPartDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvPart.Rows[row.RowIndex].FindControl("lblPartId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new RecordInvoiceDA().DeletePartByID(Convert.ToInt32(hdId.Text)))
                {
                    RefreshPartsGrid(lblPOId.Text.ToString());
                    Utility.DisplayMsg("Item deleted successfully!", this);
                }
            }
        }
        protected void btnPartEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvPart.Rows[row.RowIndex].FindControl("lblPartId");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                Session["PartId"] = hdId.Text;
                FillControlsPart(Convert.ToInt32(hdId.Text));
            }
        }
        protected void gvPart_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPart.PageIndex = e.NewPageIndex;
            RefreshPartsGrid(lblPOId.Text.ToString());
        }
        protected void gvPart_Sorting(object sender, GridViewSortEventArgs e)
        {
            RefreshPartsGrid(lblPOId.Text.ToString());
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            decimal aQty = 0;
            aQty = txtQty.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtQty.Text.ToString()) : 0;
            decimal aPrice = 0;
            aPrice = txtPrice.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtPrice.Text.ToString()) : 0;
                       
            decimal nTotal = aQty * aPrice;
            txtAmount.Text = nTotal.ToString("#.00");
        }

        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            decimal aQty = 0;
            aQty = txtQty.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtQty.Text.ToString()) : 0;
            decimal aPrice = 0;
            aPrice = txtPrice.Text.ToString().Trim().Length > 0 ? Convert.ToDecimal(txtPrice.Text.ToString()) : 0;

            decimal nTotal = aQty * aPrice;
            txtAmount.Text = nTotal.ToString("#.00");
        }

        #endregion

        #region PartMethod  
        private void ClearControlsPart()
        {
            txtQty.Text = "";
            txtItem.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtAmount.Text = "";
            btnPartAdd.Text = "Add";
        }
        private RecordInvoiceDetails SetDataPart(RecordInvoiceDetails obj)
        {
            try
            {
                obj = new RecordInvoiceDetails();

                if (Session["PartId"] != null && Convert.ToInt32(Session["PartId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["PartId"].ToString());
                    obj = new RecordInvoiceDA().GetRecordInvoicePartById(obj.Id);
                }

                if (!string.IsNullOrEmpty(lblPOId.Text.ToString()) && lblPOId.Text.ToString() != string.Empty)
                {
                    obj.BillNumber = lblPOId.Text.ToString().Trim();
                }
                else
                {
                    obj.BillNumber = new RecordInvoiceDA().MakeAutoGenLocation("R", "RentalInvoice");
                }

                if (!string.IsNullOrEmpty(txtQty.Text.ToString()) && txtQty.Text.ToString() != string.Empty)
                {
                    obj.Qty = txtQty.Text.ToString().Trim();
                }
                else
                {
                    obj.Qty = "";
                }

                if (!string.IsNullOrEmpty(txtItem.Text.ToString()) && txtItem.Text.ToString() != string.Empty)
                {
                    obj.Title = txtItem.Text.ToString();
                }
                else
                {
                    obj.Title = "";
                }

                if (!string.IsNullOrEmpty(txtDescription.Text.ToString()) && txtDescription.Text.ToString() != string.Empty)
                {
                    obj.Description = txtDescription.Text.ToString();
                }
                else
                {
                    obj.Description = "";
                }

                if (!string.IsNullOrEmpty(txtPrice.Text.ToString()) && txtPrice.Text.ToString() != string.Empty)
                {
                    obj.UnitPrice = Convert.ToDecimal(txtPrice.Text.ToString().Trim());
                }
                else
                {
                    obj.UnitPrice = 0;
                }

                if (!string.IsNullOrEmpty(txtAmount.Text.ToString()) && txtAmount.Text.ToString() != string.Empty)
                {
                    obj.Amount = Convert.ToDecimal(txtAmount.Text.ToString().Trim());
                }
                else
                {
                    obj.Amount = 0;
                }

               

            }
            catch (Exception ex)
            {
            }

            return obj;
        }
        private void FillControlsPart(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    RecordInvoiceDetails obj = new RecordInvoiceDA().GetRecordInvoicePartById(Id);
                    if (obj != null)
                    {
                        Session["PartId"]  = obj.Id;

                        if (obj.Qty != null && obj.Qty.ToString() != string.Empty)
                        {
                            txtQty.Text = obj.Qty;
                        }
                        else
                        {
                            txtQty.Text = "";
                        }

                        if (obj.Title != null && obj.Title.ToString() != string.Empty)
                        {
                            txtItem.Text = obj.Title;
                        }
                        else
                        {
                            txtItem.Text = "";
                        }
                        if (obj.Description != null && obj.Description.ToString() != string.Empty)
                        {
                            txtDescription.Text = obj.Description;
                        }
                        else
                        {
                            txtDescription.Text = "";
                        }

                        if (obj.UnitPrice != null && obj.UnitPrice.ToString() != string.Empty)
                        {
                            txtPrice.Text = obj.UnitPrice.ToString();
                        }
                        else
                        {
                            txtPrice.Text = "";
                        }

                        if (obj.Amount != null && obj.Amount.ToString() != string.Empty)
                        {
                            txtAmount.Text = obj.Amount.ToString();
                        }
                        else
                        {
                            txtAmount.Text = "";
                        }

                       

                        btnPartAdd.Text = "Update";

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