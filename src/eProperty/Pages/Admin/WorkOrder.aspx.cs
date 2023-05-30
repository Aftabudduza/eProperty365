using PropertyService.Admin.DA;
using PropertyService.BO;
using PropertyService.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Admin
{
    public partial class WorkOrderPO : System.Web.UI.Page
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
                Session["POId"] = null;
                Session["WorkOrderParts"] = null;
                Session["PartId"] = null;
                FillDropdowns();
                FillWorkOrder();
                lblPOId.Text = new WorkOrderDA().MakeAutoGenLocation("W", "WorkOrder");
                Session["POSerial"] = lblPOId.Text.ToString();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                WorkOrder obj = new WorkOrder();
                obj = SetData(obj);
               
                if (Session["POId"] == null || Session["POId"] == "0")
                {
                    if (new WorkOrderDA().Insert(obj))
                    {                       
                        Session["POId"] = null;
                        ClearControls();
                        FillWorkOrder();
                        lblMsg.Text = "WorkOrder saved successfully !";
                        Utility.DisplayMsg("WorkOrder saved successfully!", this);
                    }
                    else
                    {
                        lblMsg.Text = "WorkOrder not saved !";
                        Utility.DisplayMsg("WorkOrder not saved!", this);
                    }
                }
                else
                {
                    if (new WorkOrderDA().Update(obj))
                    {
                        ClearControls();
                        FillWorkOrder();
                        lblMsg.Text = "WorkOrder updated successfully !";
                        Utility.DisplayMsg("WorkOrder updated successfully!", this);
                    }
                    else
                    {
                        lblMsg.Text = "WorkOrder not updated !";
                        Utility.DisplayMsg("WorkOrder not updated!", this);
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
        protected void btnPODelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvPOList.Rows[row.RowIndex].FindControl("lblPOSerial");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                if (new WorkOrderDA().DeleteBySerial(hdId.Text))
                {
                    FillWorkOrder();
                    Utility.DisplayMsg("WorkOrder deleted successfully!", this);
                }
            }
        }
        protected void btnPOEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            Label hdId = (Label)gvPOList.Rows[row.RowIndex].FindControl("lblPOSerial");

            if (!String.IsNullOrEmpty(hdId.Text))
            {
                FillControls(Convert.ToString(hdId.Text));
            }
        }
        protected void gvPOList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPOList.PageIndex = e.NewPageIndex;
            FillWorkOrder();
        }
        protected void gvPOList_Sorting(object sender, GridViewSortEventArgs e)
        {
            FillWorkOrder();
        }
        protected void ddlBillTo_SelectedIndexChanged(object sender, EventArgs e)
        {         
            if(ddlBillTo.SelectedValue != "-1")
            {
                try
                {
                    ddlShipTo.Items.Clear();
                    ddlShipTo.AppendDataBoundItems = true;
                    ddlShipTo.Items.Add(new ListItem("Select Location", "-1"));
                    ddlShipTo.DataSource = new LocationDA().GetByOwner(ddlBillTo.SelectedValue);
                    ddlShipTo.DataTextField = "LocationName";
                    ddlShipTo.DataValueField = "Serial";
                    ddlShipTo.DataBind();
                    ddlShipTo.SelectedValue = "-1";
                }
                catch (Exception ex)
                {

                }
            }  
            
        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVendor.SelectedValue != "-1")
            {
                try
                {
                    VendorProfile obj = null;
                    obj = new VendorDA().GetbyID(Convert.ToInt32(ddlVendor.SelectedValue.ToString()));

                    if (obj != null)
                    {
                        
                        if (obj.ContractName != null && obj.ContractName.ToString() != string.Empty)
                        {
                            txtContactName.Text = obj.ContractName;
                        }
                       
                       
                        if (obj.CompanyName != null && obj.CompanyName.ToString() != string.Empty)
                        {
                            txtCompanyName.Text = obj.CompanyName;
                        }
                       
                        if (obj.Address != null && obj.Address.ToString() != string.Empty)
                        {
                            txtAddress.Text = obj.Address;
                        }
                       
                        if (obj.Address1 != null && obj.Address1.ToString() != string.Empty)
                        {
                            txtAddress1.Text = obj.Address1;
                        }

                        if (obj.Country != null && obj.Country.ToString() != string.Empty)
                        {
                            ddlCountry.SelectedValue = obj.Country;
                        }
                        if (obj.Region != null && obj.Region.ToString() != string.Empty)
                        {
                            txtRegion.Text = obj.Region;
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
        protected void btnSend_Click(object sender, EventArgs e)
        {
            _errStr = Validate_Control();
            if (_errStr.Length <= 0)
            {
                try
                {
                    string sBillTo = "";
                    string sVendorName = "";
                    if (ddlVendor.SelectedValue != "-1")
                    {
                        VendorProfile objV = new VendorDA().GetbyID(Convert.ToInt32(ddlVendor.SelectedValue.Trim()));
                        sVendorName = objV != null ? objV.Title : "";
                    }

                    if (ddlBillTo.SelectedValue != "-1")
                    {
                        UserProfile objUser = new AdminUserProfileDA().GetUserByOwnerSerial(ddlBillTo.SelectedValue.Trim());
                        sBillTo = objUser != null ? objUser.Email : "";
                    }

                    if (!string.IsNullOrEmpty(sBillTo))
                    {
                        if (SendApproveEmail(sBillTo, sVendorName, txtCompanyName.Text, txtContactName.Text, txtEmail.Text, lblPOId.Text, txtPhone.Text))
                        {
                            Utility.DisplayMsg("Mail sent successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Mail not sent !", this);
                        }
                    }
                    else
                    {
                        Utility.DisplayMsg("Receiver Billing email not found !", this);
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

        #endregion

        #region Method      
        private void FillDropdowns()
        {
            try
            {
                ddlBillTo.Items.Clear();
                ddlBillTo.AppendDataBoundItems = true;
                ddlBillTo.Items.Add(new ListItem("Select Owner/Property Manager", "-1"));
                List<OwnerProfile> objOwners = new AdminOwnerProfileDA().GetAllOwnersInfo();
                if (objOwners != null && objOwners.Count > 0)
                {
                    foreach (OwnerProfile obj in objOwners)
                    {
                        string sName = obj.FirstName.ToString() + " " + obj.LastName.ToString();
                        ddlBillTo.Items.Add(new ListItem(sName, obj.Serial.ToString()));
                    }
                }
             
                ddlBillTo.DataBind();
                ddlBillTo.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
            }

            try
            {
                ddlVendor.Items.Clear();
                ddlVendor.AppendDataBoundItems = true;
                ddlVendor.Items.Add(new ListItem("Select Vendor", "-1"));
                List<VendorProfile> objVendors = null;
                if (Session["OwnerId"] != null)
                {
                    objVendors = new VendorDA().GetByOwner(Session["OwnerId"].ToString());
                }

                if (objVendors != null && objVendors.Count > 0)
                {
                    foreach (VendorProfile obj in objVendors)
                    {
                        string sName = obj.Title.ToString();
                        ddlVendor.Items.Add(new ListItem(sName, obj.Id.ToString()));
                    }
                }

                ddlVendor.DataBind();
                ddlVendor.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
            }

            try
            {
                ddlCountry.Items.Clear();
                ddlCountry.AppendDataBoundItems = true;
                ddlCountry.Items.Add(new ListItem("Select Country", "-1"));
                ddlCountry.DataSource = new CountryDA().GetAllRefCountries();
                ddlCountry.DataTextField = "COUNTRYNAME";
                ddlCountry.DataValueField = "COUNTRY";
                ddlCountry.DataBind();
                ddlCountry.SelectedValue = "US";
            }
            catch (Exception ex)
            {

            }

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
            ddlCountry.SelectedValue = "-1";
            txtRegion.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            ddlBillTo.SelectedValue = "-1";
            ddlShipTo.SelectedValue = "-1";
            rdoType.SelectedValue = null;
            btnSave.Text = "Submit";
            lblPOId.Text = new WorkOrderDA().MakeAutoGenLocation("W", "WorkOrder");
        }
        private WorkOrder SetData(WorkOrder obj)
        {
            try
            {
                obj = new WorkOrder();

                if (Session["POId"] != null && Convert.ToInt32(Session["POId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["POId"].ToString());
                    obj = new WorkOrderDA().GetbyID(obj.Id);
                }
               
                if ((!string.IsNullOrEmpty(txtCompanyName.Text.ToString())) && (txtCompanyName.Text.ToString() != string.Empty))
                {
                    obj.CompanyName = txtCompanyName.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.CompanyName = "";
                }

                if ((!string.IsNullOrEmpty(txtContactName.Text.ToString())) && (txtContactName.Text.ToString() != string.Empty))
                {
                    obj.ContractName = txtContactName.Text.ToString();
                }
                else
                {
                    obj.ContractName = "";
                }

                if ((!string.IsNullOrEmpty(txtAddress.Text.ToString())) && (txtAddress.Text.ToString() != string.Empty))
                {
                    obj.Address = txtAddress.Text.ToString();
                }
                else
                {
                    obj.Address = "";
                }
                if ((!string.IsNullOrEmpty(txtAddress1.Text.ToString())) && (txtAddress1.Text.ToString() != string.Empty))
                {
                    obj.Address1 = txtAddress1.Text.ToString().ToLower().Trim();
                }
                else
                {
                    obj.Address1 = "";
                }
                if (ddlCountry.SelectedValue != "-1")
                {
                    obj.Country = ddlCountry.SelectedValue.Trim();
                }
                else
                {
                    obj.Country = "";
                }
                if ((!string.IsNullOrEmpty(txtRegion.Text.ToString())) && (txtRegion.Text.ToString() != string.Empty))
                {
                    obj.Region = txtRegion.Text.ToString();
                }
                else
                {
                    obj.Region = "";
                }
                if (ddlState.SelectedValue != "-1")
                {
                    obj.State = ddlState.SelectedValue.Trim();
                }
                else
                {
                    obj.State = "";
                }

                obj.City = txtCity.Text.ToString().Trim();
                obj.Zip = txtZip.Text.ToString().Trim();
                obj.Email = txtEmail.Text.ToString().Trim();
                obj.Phone = txtPhone.Text.ToString().Trim();
               
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

                if (Session["POSerial"] != null)
                {
                    obj.Serial = !string.IsNullOrEmpty(lblPOId.Text.ToString()) ? lblPOId.Text.ToString() : Session["POSerial"].ToString();
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                }
                else
                {
                    obj.Serial = !string.IsNullOrEmpty(lblPOId.Text.ToString()) ? lblPOId.Text.ToString() : new WorkOrderDA().MakeAutoGenLocation("W", "WorkOrder");
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
                }

                if (ddlBillTo.SelectedValue != "-1")
                {
                    obj.BillTo = ddlBillTo.SelectedValue.Trim();
                }
                else
                {
                    obj.BillTo = "";
                }
                if (ddlShipTo.SelectedValue != "-1")
                {
                    obj.LocationId = ddlShipTo.SelectedValue.Trim();
                }
                else
                {
                    obj.LocationId = "";
                }

                if (rdoType.Items[0].Selected == true)
                {
                    obj.ShippingType = "CheapestFright";
                }
                else if (rdoType.Items[1].Selected == true)
                {
                    obj.ShippingType = "Overnight";
                }
                else if (rdoType.Items[2].Selected == true)
                {
                    obj.ShippingType = "2ndDay";
                }

                if (ddlVendor.SelectedValue != "-1")
                {
                    obj.VendorId = Convert.ToInt32(ddlVendor.SelectedValue.Trim());
                    VendorProfile objV = new VendorDA().GetbyID(Convert.ToInt32(ddlVendor.SelectedValue.Trim()));
                    obj.VendorName = objV != null ? objV.Title : "";
                }
                else
                {
                    obj.VendorId = 0;
                    obj.VendorName = "";
                }



            }
            catch (Exception ex)
            {
            }

            return obj;
        }
        private void FillControls(string serial)
        {
            try
            {
                if (!string.IsNullOrEmpty(serial))
                {
                    WorkOrder obj = new WorkOrderDA().GetBySerial(serial);
                    if (obj != null)
                    {
                        Session["POId"] = obj.Id;

                        if (obj.Serial != null && obj.Serial.ToString() != string.Empty)
                        {
                            lblPOId.Text = obj.Serial;
                        }
                        else
                        {
                            lblPOId.Text = "";
                        }

                        if (obj.ContractName != null && obj.ContractName.ToString() != string.Empty)
                        {
                            txtContactName.Text = obj.ContractName;
                        }
                        else
                        {
                            txtContactName.Text = "";
                        }
                       
                        if (obj.CompanyName != null && obj.CompanyName.ToString() != string.Empty)
                        {
                            txtCompanyName.Text = obj.CompanyName;
                        }
                        else
                        {
                            txtCompanyName.Text = "";
                        }
                        if (obj.Address != null && obj.Address.ToString() != string.Empty)
                        {
                            txtAddress.Text = obj.Address;
                        }
                        else
                        {
                            txtAddress.Text = "";
                        }
                        if (obj.Address1 != null && obj.Address1.ToString() != string.Empty)
                        {
                            txtAddress1.Text = obj.Address1;
                        }
                        else
                        {
                            txtAddress1.Text = "";
                        }
                        if (obj.Country != null && obj.Country.ToString() != string.Empty)
                        {
                            ddlCountry.SelectedValue = obj.Country;
                        }
                        else
                        {
                            ddlCountry.SelectedValue = "";
                        }
                        if (obj.Region != null && obj.Region.ToString() != string.Empty)
                        {
                            txtRegion.Text = obj.Region;
                        }
                        else
                        {
                            txtRegion.Text = "";
                        }
                        if (obj.State != null && obj.State.ToString() != string.Empty)
                        {
                            ddlState.SelectedValue = obj.State;
                        }
                        else
                        {
                            ddlState.SelectedValue = "";
                        }
                        if (obj.City != null && obj.City.ToString() != string.Empty)
                        {
                            txtCity.Text = obj.City;
                        }
                        else
                        {
                            txtCity.Text = "";
                        }
                        if (obj.Zip != null && obj.Zip.ToString() != string.Empty)
                        {
                            txtZip.Text = obj.Zip;
                        }
                        else
                        {
                            txtZip.Text = "";
                        }
                        if (obj.Email != null && obj.Email.ToString() != string.Empty)
                        {
                            txtEmail.Text = obj.Email;
                        }
                        else
                        {
                            txtEmail.Text = "";
                        }
                        if (obj.Phone != null && obj.Phone.ToString() != string.Empty)
                        {
                            txtPhone.Text = obj.Phone;
                        }
                        else
                        {
                            txtPhone.Text = "";
                        }

                        if (obj.VendorId != null && obj.VendorId.ToString() != string.Empty)
                        {
                            ddlVendor.SelectedValue = obj.VendorId.ToString();
                        }
                        if (obj.BillTo != null && obj.BillTo.ToString() != string.Empty)
                        {
                            ddlBillTo.SelectedValue = obj.BillTo.ToString();
                        }
                        if (obj.LocationId != null && obj.LocationId.ToString() != string.Empty)
                        {
                            ddlShipTo.SelectedValue = obj.LocationId.ToString();
                        }

                        if (obj.ShippingType != null)
                        {
                            if (obj.ShippingType == "CheapestFright")
                            {
                                rdoType.Items[0].Selected = true;
                            }
                            else if (obj.ShippingType == "Overnight")
                            {
                                rdoType.Items[1].Selected = true;
                            }
                            else if (obj.ShippingType == "2ndDay")
                            {
                                rdoType.Items[2].Selected = true;
                            }                            
                        }


                        btnSave.Text = "Update";

                        RefreshPartsGrid(obj.Serial);

                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        public void RefreshPartsGrid(string serial)
        {
            try
            {
                List<WorkOrderPartData> objParts = null;
                objParts = new WorkOrderDA().GetPartDataByWorkOrder(serial);

                if (objParts != null && objParts.Count > 0)
                {
                    Session["WorkOrderParts"] = objParts;
                }

                gvPart.DataSource = objParts;
                gvPart.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        private void FillWorkOrder()
        {
            try
            {
                List<WorkOrder> obj = null;
                if (Session["OwnerId"] != null)
                {
                    obj = new WorkOrderDA().GetByOwner(Session["OwnerId"].ToString());
                }

                gvPOList.DataSource = obj;
                gvPOList.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public string Validate_Control()
        {
            if (ddlVendor.SelectedValue == "-1")
                _errStr += "Please select Vendor" + Environment.NewLine;
          
            if (ddlBillTo.SelectedValue == "-1")
                _errStr += "Please select Bill To Owner/Property Manager" + Environment.NewLine;

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
                _errStr += "PO Number can not be empty" + Environment.NewLine;
            }

            return _errStr;
        }
        public bool SendApproveEmail(string sBillTo, string sVendorName, string sCompanyName, string sContract, string sEmail, string sSerial, string sPhone)
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
                    to_address = sBillTo;
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
              //  objMailMessage.Bcc.Add(bcc_address);
                objMailMessage.Subject = "P.O/Work Order approval ";
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Body = this.ApproveEmailHtml(sVendorName, sCompanyName, sContract, sEmail, sSerial, sPhone).ToString();
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

        public System.Text.StringBuilder ApproveEmailHtml(string sVendorName, string sCompanyName, string sContract, string sEmail, string sSerial, string sPhone)
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
                emailbody.Append("<tr><td colspan='2' style='text-align:Left;font-size:14px;font-weight:bold;color:0000cc;'>Dear Customer,</td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>Congratulations !! Your P.O/Work order application is approved. </p> </td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'><p>YOUR PO Number IS: <b>" + sSerial + " </b></p></td></tr>");
                emailbody.Append("<tr><td colspan='2'></td></tr>");
                emailbody.Append("<tr><td colspan='2'>Vendor Name: " + sVendorName + "</td></tr>");
                emailbody.Append("<tr><td colspan='2'>Company Name: " + sCompanyName + "</td></tr>");
                emailbody.Append("<tr><td colspan='2'>Contact: " + sContract + "</td></tr>");
                emailbody.Append("<tr><td colspan='2'>Phone: " + sPhone + "</td></tr>");
                emailbody.Append("<tr><td colspan='2'>Email: " + sEmail + "</td></tr>");

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
                WorkOrderPartData obj = new WorkOrderPartData();
                obj = SetDataPart(obj);

                if (obj.WorkOrderSerialId == null || obj.WorkOrderSerialId == "")
                {
                    Utility.DisplayMsg("PO Number is invalid !!", this);
                }
                else
                {
                    if (Session["PartId"] == null || Session["PartId"] == "0")
                    {
                        if (new WorkOrderDA().InsertPart(obj))
                        {
                            Session["PartId"] = null;
                            ClearControlsPart();
                            RefreshPartsGrid(lblPOId.Text.ToString());
                            Utility.DisplayMsg("Part saved successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Part not saved!", this);
                        }
                    }
                    else
                    {
                        if (new WorkOrderDA().UpdatePart(obj))
                        {
                            ClearControlsPart();
                            RefreshPartsGrid(lblPOId.Text.ToString());
                            Utility.DisplayMsg("Part updated successfully!", this);
                        }
                        else
                        {
                            Utility.DisplayMsg("Part not updated!", this);
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
                if (new WorkOrderDA().DeletePartByID(Convert.ToInt32(hdId.Text)))
                {
                    RefreshPartsGrid(lblPOId.Text.ToString());
                    Utility.DisplayMsg("Part deleted successfully!", this);
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
        #endregion

        #region PartMethod  
        private void ClearControlsPart()
        {
            txtPart.Text = "";
            txtModel.Text = "";
            txtManufacture.Text = "";
            txtCost.Text = "";
            txtApproved.Text = "";
            txtDate.Text = "";
            btnPartAdd.Text = "Add";
        }
        private WorkOrderPartData SetDataPart(WorkOrderPartData obj)
        {
            try
            {
                obj = new WorkOrderPartData();

                if (Session["PartId"] != null && Convert.ToInt32(Session["PartId"]) > 0)
                {
                    obj.Id = Convert.ToInt32(Session["PartId"].ToString());
                    obj = new WorkOrderDA().GetWorkOrderPartById(obj.Id);
                }

                if (!string.IsNullOrEmpty(lblPOId.Text.ToString()) && lblPOId.Text.ToString() != string.Empty)
                {
                    obj.WorkOrderSerialId = lblPOId.Text.ToString().Trim();
                }
                else
                {
                    obj.WorkOrderSerialId = new WorkOrderDA().MakeAutoGenLocation("W", "WorkOrder");
                }

                if (!string.IsNullOrEmpty(txtPart.Text.ToString()) && txtPart.Text.ToString() != string.Empty)
                {
                    obj.PartNumber = txtPart.Text.ToString().Trim();
                }
                else
                {
                    obj.PartNumber = "";
                }

                if (!string.IsNullOrEmpty(txtManufacture.Text.ToString()) && txtManufacture.Text.ToString() != string.Empty)
                {
                    obj.Manufacturer = txtManufacture.Text.ToString();
                }
                else
                {
                    obj.Manufacturer = "";
                }

                if (!string.IsNullOrEmpty(txtModel.Text.ToString()) && txtModel.Text.ToString() != string.Empty)
                {
                    obj.Model = txtModel.Text.ToString();
                }
                else
                {
                    obj.Model = "";
                }

                if (!string.IsNullOrEmpty(txtCost.Text.ToString()) && txtCost.Text.ToString() != string.Empty)
                {
                    obj.Cost = txtCost.Text.ToString().Trim();
                }
                else
                {
                    obj.Cost = "";
                }

                if (!string.IsNullOrEmpty(txtApproved.Text.ToString()) && txtApproved.Text.ToString() != string.Empty)
                {
                    obj.ApprovedBy = txtApproved.Text.ToString();
                }
                else
                {
                    obj.ApprovedBy = "";
                }

                if (!string.IsNullOrEmpty(txtDate.Text.ToString()) && txtDate.Text.ToString() != string.Empty)
                {
                    obj.PurchaseDate = txtDate.Text.ToString().Trim().Length > 0 ? Convert.ToDateTime(txtDate.Text.ToString().Trim()) : DateTime.Now; 
                }
                else
                {
                    obj.PurchaseDate = DateTime.Now;
                }

                if (Session["PartId"] != null)
                {
                    obj.UpdatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.UpdatedDate = DateTime.Now;
                }
                else
                {
                    obj.CreatedBy = Convert.ToInt16(((UserProfile)Session["UserObject"]).Id);
                    obj.CreatedDate = DateTime.Now;
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
                    WorkOrderPartData obj = new WorkOrderDA().GetWorkOrderPartById(Id);
                    if (obj != null)
                    {
                        Session["PartId"]  = obj.Id;                       

                        if (obj.PartNumber != null && obj.PartNumber.ToString() != string.Empty)
                        {
                            txtPart.Text = obj.PartNumber;
                        }
                        else
                        {
                            txtPart.Text = "";
                        }

                        if (obj.Model != null && obj.Model.ToString() != string.Empty)
                        {
                            txtModel.Text = obj.Model;
                        }
                        else
                        {
                            txtModel.Text = "";
                        }
                        if (obj.Manufacturer != null && obj.Manufacturer.ToString() != string.Empty)
                        {
                            txtManufacture.Text = obj.Manufacturer;
                        }
                        else
                        {
                            txtManufacture.Text = "";
                        }

                        if (obj.Cost != null && obj.Cost.ToString() != string.Empty)
                        {
                            txtCost.Text = obj.Cost;
                        }
                        else
                        {
                            txtCost.Text = "";
                        }

                        if (obj.ApprovedBy != null && obj.ApprovedBy.ToString() != string.Empty)
                        {
                            txtApproved.Text = obj.ApprovedBy;
                        }
                        else
                        {
                            txtApproved.Text = "";
                        }

                        if (obj.PurchaseDate != null && obj.PurchaseDate.ToString() != string.Empty)
                        {
                            txtDate.Text = Convert.ToDateTime(obj.PurchaseDate).ToString("MM-dd-yyyy"); 
                        }
                        else
                        {
                            txtDate.Text = "";
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