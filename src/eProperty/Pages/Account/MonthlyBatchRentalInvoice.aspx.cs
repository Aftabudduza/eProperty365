using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using eProperty.Models;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Account;
using PropertyService.ViewModel;

namespace eProperty.AppJs.Account
{
    public partial class MonthlyBatchRentalInvoice : System.Web.UI.Page
    {
        private static System.Net.Mail.SmtpClient objSmtpClient;
        private static System.Net.Mail.MailMessage objMailMessage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
        }
        [WebMethod]
        public static string GetOwner()
        {
            List<ComboData> lstComboData = new List<ComboData>();
            List<OwnerProfile> list = new MonthlyBatchRentalInvoiceDA().GetOwner();
            string owner = "-1";
            if (HttpContext.Current.Session["OwnerId"] != null)
            {
                owner = HttpContext.Current.Session["OwnerId"].ToString();
            }
            foreach (var obj in list)
            {
                ComboData cmb = new ComboData();
                cmb.Id = obj.Id;
                cmb.Id2 = obj.Serial;
                cmb.Data = obj.FirstName + " "+ obj.LastName;
                cmb.SelectedField = owner;
                lstComboData.Add(cmb);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod]
        public static string GetPropertyManager(MonthlyBatchRentalInvoiceModel Obj)
        {
            List<ComboData> lstComboData = new List<ComboData>();
            List<PropertyManagerProfile> list = new MonthlyBatchRentalInvoiceDA().GetPropertyManager(Obj.OwnerId);
            
            foreach (var obj in list)
            {
                ComboData cmb = new ComboData();
                cmb.Id = obj.Id;
                cmb.Id2 = obj.Serial;
                cmb.Data = obj.FirstName + " " + obj.LastName;
                lstComboData.Add(cmb);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }

        [WebMethod]
        public static string GetLocation(MonthlyBatchRentalInvoiceModel Obj)
        {
            List<ComboData> lstComboData = new List<ComboData>();
            List<Location> list = new MonthlyBatchRentalInvoiceDA().GetLocation(Obj.OwnerId);

            foreach (var obj in list)
            {
                ComboData cmb = new ComboData();
                cmb.Id = obj.Id;
                cmb.Id2 = obj.Serial;
                cmb.Data = obj.LocationName;
                lstComboData.Add(cmb);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod]
        public static string OwnerLocationWiseData(MonthlyBatchRentalInvoiceModel Obj)
        {
            List<MonthlyBatchRentalInvoiceTenantMail> lst = new MonthlyBatchRentalInvoiceDA().GetMonthlyBatchRentalInvoiceTenantMail(Obj);

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);
            return json;
        }
        [WebMethod]
        public static string Save(MonthlyBatchRentalInvoiceModel Obj)
        {
            var res = false;
            if (Obj != null)
            {
                BillPayment objBillPayment = new BillPayment();
                List<PropertyService.BO.MonthlyBatchRentalInvoice> lstObj = new List<PropertyService.BO.MonthlyBatchRentalInvoice>();
                decimal amount = new MonthlyBatchRentalInvoiceDA().GetUnitPrice(Obj.OwnerId) * Obj.lst.Count;
                string InvoiceNo = new FinancialTransactionDA().MakeAutoGenSerial("M", "MonthlyBillPaymentInvoice");
                foreach (var item in Obj.lst)
                {
                    PropertyService.BO.MonthlyBatchRentalInvoice objSave =
                        new PropertyService.BO.MonthlyBatchRentalInvoice()
                        {
                            RefId = item.TenantSerialId,
                            PropertyManagerId = Obj.PropertyManagerSerialId,
                            LocationId = Obj.LocationSerialId,
                            InvoiceNo = item.InvoiceID,
                            TransactionType = "Monthly Batch Rental Invoice",
                            AccountType = "Inc",
                            Amount = Convert.ToDecimal(string.IsNullOrEmpty(item.Amount) ? "0" : item.Amount),
                            Credit = Convert.ToDecimal(string.IsNullOrEmpty(item.Amount) ? "0" : item.Amount),
                            CreateDate = DateTime.Now,
                            UnitId = item.UnitSerialId,
                            OwnerId = Obj.OwnerId,
                            Month = DateTime.Now.Month.ToString(),
                            Year = DateTime.Now.Year.ToString(),
                            UserType = HttpContext.Current.Session["UserType"].ToString(),
                            Remarks = "Inc"
                        };

                    PropertyService.BO.MonthlyBatchRentalInvoice objMonthlyRent = new MonthlyBatchRentalInvoiceDA().GetMonthlyBatchRentalInvoiceByUnit(objSave.OwnerId, objSave.UnitId, objSave.Year, objSave.Month);
                    if(objMonthlyRent != null)
                    {
                       if(new MonthlyBatchRentalInvoiceDA().DeleteById(objMonthlyRent.Id))
                        lstObj.Add(objSave);
                    }
                    
                }

                objBillPayment = new BillPayment()
                {
                    RefId = Obj.OwnerId,
                    InvoiceNo = "I" + Obj.OwnerId.Substring(Obj.OwnerId.Length - 4) + InvoiceNo.Substring(InvoiceNo.Length - 7),
                    TransactionType = "Unit Fee",
                    AccountType = "Exp",
                    LedgerCode = "6130",
                    Remarks = "Exp",
                    Amount = amount,
                    Debit = amount,
                    Credit = 0,
                    CreateDate = DateTime.Now,
                    OwnerId = Obj.OwnerId,
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    UserType = HttpContext.Current.Session["UserType"].ToString()
                };

                if (new MonthlyBatchRentalInvoiceDA().Insert(lstObj, objBillPayment))
                    {
                        foreach (var element in Obj.lst)
                        {
                            var oEmailToTenant =  SendEmailToTenant(element.EmailId, element.TenantName, element.UnitName,Obj.LocationName,
                                element.Amount, element.SendDate, element.InvoiceID, element.TenantSerialId);
                        }
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }
                
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
        public static bool SendEmailToTenant(string tenantEmail, string tenantName, string unitName, string location, string amount,string date, string InvoiceNo, string TenantSerial)
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
                    to_address = tenantEmail;
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
                objMailMessage.Subject = tenantName + " " + unitName + " monthly rent invoice";
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Body = EmailHtml(location,unitName,date,amount, InvoiceNo, TenantSerial).ToString();
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
        public static System.Text.StringBuilder EmailHtml(string location, string unit, string date, string amount, string InvoiceNo, string TenantSerial)
        {
            System.Text.StringBuilder emailbody = new System.Text.StringBuilder();
            string sWeb = System.Configuration.ConfigurationManager.AppSettings.Get("WebURL");

            try
            {
                emailbody.Append("<table style='width: 100%;'>");
                emailbody.Append("<tr style='height: 25px;'><td colspan='6'><img  alt='eProperty365' width='120' src='" + sWeb + "/Images/logo.png'></td></tr>");
                emailbody.Append("<tr style='height: 35px;'><td colspan='3'> Location:" + location + "</td><td colspan='3'> Unit ID: " + unit + "</td></tr>");
                emailbody.Append("<tr style='height: 35px;font-weight: bold;'><td colspan='2'> Date </td><td colspan ='2'>Description </td><td colspan='2'> Amount </td></tr>");
                emailbody.Append("<tr style='height: 35px;'><td colspan='2'>" + date + "</td><td colspan = '2' > One Months Rent </td><td colspan ='2'>$" + Convert.ToDecimal(amount).ToString("#.00") + "</td></tr>");
                emailbody.Append("<tr style='height: 35px;'><td colspan='3'> Terms: Due upon Receipt </td><td colspan = '3' > *Total Amount: $" + Convert.ToDecimal(amount).ToString("#.00") + "</td></tr>");
                emailbody.Append("<tr style='height: 35px;'><td colspan='3'></td><td colspan = '3' ><a style='text-decoration: none; color: black; background: forestgreen;' href='" + sWeb + "/Pages/Resident/TenantOnlinefee.aspx?InvoiceId=" + InvoiceNo + "&TenentId=" + TenantSerial + "'>Click Here to Pay Online</a></td></tr>");
                emailbody.Append("</table>");
            }
            catch (Exception ex)
            {
            }
            return emailbody;
        }


    }
}