using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using System.Xml;
using System.Xml.Linq;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialTenantRental_App_Page_4 : System.Web.UI.Page
    {
        public bool isVisible = false;
        public bool isView = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Session["ResidentialUnitSerial"] = "100000000004";
                //Session["TenentId"] = "100000000001";
                if (Session["ResidentialUnitSerial"] == null && Session["TenentId"] == null)
                {
                    Response.Redirect("ResidentialAddResponceTemplate_Login.aspx");
                }

                var isAdmin = false;
                if (Session["UserObject"] != null)
                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null
                        ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin)
                        : false;

                if (Session["UserType"] != null)
                {
                    if (isAdmin == true || Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3")
                    {
                        isVisible = true;
                        isView = false;
                    }
                }
            }
        }
        [WebMethod]
        public static string GetVerityIncomeList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetVerityIncomeList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }

        [WebMethod(EnableSession = true)]
        public static string VerifyImageUpload(string Image, string ImageName, string Description)
        {

            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new Residential_Tenant_Add_Step2_Page4_VerityIncome();
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

            // obj.Id = Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitWebpageId"].ToString());

            //obj.Description = !string.IsNullOrEmpty(PicDesc) ? PicDesc : string.Empty;

            byte[] getImageData = Convert.FromBase64String(Image);
            string sfile = ImageName;
            string direc = "~/Uploads/";
            string uploadPath = "~/Uploads/Income/" + HttpContext.Current.Session["TenentId"].ToString();
            var filepath = HttpContext.Current.Server.MapPath(uploadPath);
            if (!Directory.Exists(filepath))
            {
                string spath = HttpContext.Current.Server.MapPath(direc);
                DirectorySecurity ds = Directory.GetAccessControl(spath);
                ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(spath, ds);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadPath));
            }

            string sfullpath = Path.Combine(filepath, sfile);
            try
            {
                if (!File.Exists(sfullpath))
                {
                    File.WriteAllBytes(sfullpath, getImageData);
                }
                else
                {
                    if (File.Exists(sfullpath))
                    {
                        File.Delete(sfullpath);
                        File.WriteAllBytes(sfullpath, getImageData);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            obj.FileName = ImageName;
            obj.FilePath = "../../Uploads/Income/" + HttpContext.Current.Session["TenentId"].ToString() + "/" + obj.FileName; ;
            obj.Serial = HttpContext.Current.Session["TenentId"].ToString();
            obj.DocumentDescription = Description;


            var returnObj = new List<Residential_Tenant_Add_Step2_Page4_VerityIncome>();
            try
            {
                if (new ResidentialAddResponceTemplateDa().InsertOrUpdateVerityOfIncome(obj))
                {
                    returnObj = new ResidentialAddResponceTemplateDa().GetVerityIncomeList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());


                }
                //  obj.ImageURL = "../../Uploads/Income/" + HttpContext.Current.Session["TenentId"].ToString() +"/" + obj.ImageName;

                //"..\..\Uploads\Images\images(2).jpg"
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(returnObj);
                return json;
            }
            catch (Exception ex)
            {

                return "";
            }

        }

        [WebMethod]
        public static string DocImageUpload(string Image, string ImageName, string DocId, string CurrentStatus, string DocumentDescription)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant();
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            obj.ResidentTenantDocId = 0;// Convert.ToInt32(DocId);
            obj.DocumentDescription = DocumentDescription;
            obj.CurrentStatus = CurrentStatus;

            // obj.Id = Convert.ToInt32(HttpContext.Current.Session["ResidentialUnitWebpageId"].ToString());

            //obj.Description = !string.IsNullOrEmpty(PicDesc) ? PicDesc : string.Empty;

            byte[] getImageData = Convert.FromBase64String(Image);
            string sfile = ImageName;
            string direc = "~/Uploads/";
            string uploadPath = "~/Uploads/Doc/" + HttpContext.Current.Session["TenentId"].ToString();
            var filepath = HttpContext.Current.Server.MapPath(uploadPath);
            if (!Directory.Exists(filepath))
            {
                string spath = HttpContext.Current.Server.MapPath(direc);
                DirectorySecurity ds = Directory.GetAccessControl(spath);
                ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(spath, ds);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadPath));
            }

            string sfullpath = Path.Combine(filepath, sfile);
            try
            {
                if (!File.Exists(sfullpath))
                {
                    File.WriteAllBytes(sfullpath, getImageData);
                }
                else
                {
                    if (File.Exists(sfullpath))
                    {
                        File.Delete(sfullpath);
                        File.WriteAllBytes(sfullpath, getImageData);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            obj.FileName = ImageName;
            obj.FilePath = "../../Uploads/Doc/" + HttpContext.Current.Session["TenentId"].ToString() + "/" + obj.FileName;
            obj.Serial = HttpContext.Current.Session["TenentId"].ToString();

            try
            {
                if (new ResidentialAddResponceTemplateDa().InsertResidentialDocInfo(obj))
                {
                    // result = "true";
                }
                // obj.FilePath = "../../Uploads/Doc/" + HttpContext.Current.Session["TenentId"].ToString() + "/" + obj.FileName;

                //"..\..\Uploads\Images\images(2).jpg"

            }
            catch (Exception ex)
            {

                obj = new ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant();
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
        [WebMethod]
        public static string DocViewd(string DocId, string CurrentStatus)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant();
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            obj.ResidentTenantDocId = Convert.ToInt32(DocId);
            obj.Serial = HttpContext.Current.Session["TenentId"].ToString();
            obj.CurrentStatus = CurrentStatus;

            try
            {
                if (new ResidentialAddResponceTemplateDa().InsertResidentialDocInfo(obj))
                {
                    // result = "true";
                }
                // obj.FilePath = "../../Uploads/Doc/" + HttpContext.Current.Session["TenentId"].ToString() + "/" + obj.FileName;

                //"..\..\Uploads\Images\images(2).jpg"

            }
            catch (Exception ex)
            {

                obj = new ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant();
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string GetAdditionalDoc()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var ResidentialDoc = new ResidentialAddResponceTemplateDa().GetRentalDocList((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), (HttpContext.Current.Session["TenentId"])?.ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ResidentialDoc);
            return json;

        }
        [WebMethod(EnableSession = true)]
        public static string GetAllSignature()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var ResidentialDoc = new ResidentialAddResponceTemplateDa().GetAllSignature(((HttpContext.Current.Session["TenentId"])?.ToString()), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ResidentialDoc);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string SaveSecurityInfo(Residential_Tenant_Add_Step2_Page4_Signature Obj)
        {

            var rsult = new List<Residential_Tenant_Add_Step2_Page4_Signature>();

            if (Obj != null)
            {
                Obj.Serial = (HttpContext.Current.Session["TenentId"])?.ToString();
                Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                if (Obj.Id > 0)
                {
                    var getOldObj = new ResidentialAddResponceTemplateDa().GetSignatureById(Obj.Id);
                    Obj.UpdateDate = DateTime.Now;
                    Obj.CreateDate = getOldObj.CreateDate;
                    if (new ResidentialAddResponceTemplateDa().UpdateSignature(Obj))
                    {
                        rsult = new ResidentialAddResponceTemplateDa().GetAllSignature(((HttpContext.Current.Session["TenentId"])?.ToString()), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }

                }
                else
                {
                    Obj.CreateDate = DateTime.Now;
                    if (new ResidentialAddResponceTemplateDa().InsertSignature(Obj))
                    {
                        rsult = new ResidentialAddResponceTemplateDa().GetAllSignature(((HttpContext.Current.Session["TenentId"])?.ToString()), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }


            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(rsult);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string Delete(int obj)
        {
            var rsult = new List<Residential_Tenant_Add_Step2_Page4_Signature>();
            if (obj > 0)
            {
                if (new ResidentialAddResponceTemplateDa().DeleteById(obj))
                {
                    rsult = new ResidentialAddResponceTemplateDa().GetAllSignature(((HttpContext.Current.Session["TenentId"])?.ToString()), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(rsult);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string FinalSubmit(string investigateReport, string CredtReport)
        {
            var mass = "";
            var isSave = false;
            var objStep1ApplicationFee = new ResidentialAddResponceTemplateDa().GetTenantApp1ByUnitAndTenantId(HttpContext.Current.Session["TenentId"].ToString(), HttpContext.Current.Session["ResidentialUnitSerial"].ToString());

            if (objStep1ApplicationFee != null && !string.IsNullOrEmpty(objStep1ApplicationFee.PerFormTenantBackgroundScreening))
            {
                if(objStep1ApplicationFee.PerFormTenantBackgroundScreening.ToString() == "Yes")
                {
                    try
                    {
                        XMLOperationStatus XMLResponse = GetXML();

                        if (XMLResponse != null)
                        {
                            if (XMLResponse.ErrorCode == string.Empty)
                            {
                                try
                                {
                                    var objXML = new ResidentialAddResponceTemplateDa().GetXMLOperationStatus(XMLResponse.TenantId);
                                    if (objXML != null)
                                    {
                                        var sql = " delete from XMLOperationStatus where TenantId = '" + HttpContext.Current.Session["TenentId"].ToString() + "'";
                                        Utility.RunCMD(sql);
                                    }
                                }
                                catch (Exception ex)
                                {

                                }


                                if (new ResidentialAddResponceTemplateDa().SaveXMLStatus(XMLResponse))
                                {
                                    isSave = true;
                                }
                                else
                                {
                                    isSave = false;
                                }
                            }
                            else
                            {
                                var jsonSerialiser = new JavaScriptSerializer();
                                var json = jsonSerialiser.Serialize(XMLResponse.ErrorCode);
                                return json;
                            }

                        }
                        else
                        {
                            isSave = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        isSave = false;
                    }
                }
                else if (objStep1ApplicationFee.PerFormTenantBackgroundScreening.ToString() == "No")
                {
                    isSave = true;
                }
               
            }
           

            if(isSave == true)
            {
                try
                {
                    if (new ResidentialAddResponceTemplateDa().UpdateTenantStatus(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), HttpContext.Current.Session["TenentId"].ToString(), "Completed"))
                    {
                        var toAddress = new ResidentialAddResponceTemplateDa().GetTenantEmailAddress((HttpContext.Current.Session["TenentId"])?.ToString());
                        var strMailServer = ConfigurationManager.AppSettings["strMailServer"];
                        var strMailUser = ConfigurationManager.AppSettings["strMailUser"];
                        var strMailPassword = ConfigurationManager.AppSettings["strMailPassword"];
                        var strMailPort = ConfigurationManager.AppSettings["strMailPort"];
                        var strFromAddress = ConfigurationManager.AppSettings["fromAddress"];
                        var strBccAddress = ConfigurationManager.AppSettings["bccAddress"];

                        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                        mail.From = new MailAddress(strFromAddress, "Support", System.Text.Encoding.UTF8);
                        mail.To.Add(toAddress);
                        mail.Bcc.Add(strBccAddress);
                        mail.Subject = "eProperty365 Residential Application Request";
                        mail.SubjectEncoding = System.Text.Encoding.UTF8;
                        mail.Body = "Thank You. Your Application has been received. It may take up to 5 working days to approve or denied this application. you will receive a email letting you know either way. ";
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        SmtpClient client = new SmtpClient();
                        client.Credentials = new System.Net.NetworkCredential(strMailUser, strMailPassword);
                        client.Port = Convert.ToInt32(strMailPort);
                        client.Host = strMailServer;
                        client.EnableSsl = true;
                        try
                        {
                            //Add this line to bypass the certificate validation
                            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                            {
                                return true;
                            };
                            client.Send(mail);
                            mass = "true";
                            //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
                        }
                        catch (Exception ex)
                        {
                            Exception ex2 = ex;
                            string errorMessage = string.Empty;
                            while (ex2 != null)
                            {
                                errorMessage += ex2.ToString();
                                ex2 = ex2.InnerException;
                            }
                            mass = "true";
                            //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                return "Operation failed";
            }


            if (mass == "true")
            {
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize("true");
                return json;
            }
            else
            {
                return "Mail sending failed";
            }
        }

        //public static string FinalSubmit(string investigateReport, string CredtReport)
        //{
        //    var mass = "";
        //    var isSave = false;

        //    try
        //    {
        //        if (new ResidentialAddResponceTemplateDa().UpdateTenantStatus(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), HttpContext.Current.Session["TenentId"].ToString(), "Complete"))
        //        {
        //            var toAddress = new ResidentialAddResponceTemplateDa().GetTenantEmailAddress((HttpContext.Current.Session["TenentId"])?.ToString());
        //            var strMailServer = ConfigurationManager.AppSettings["strMailServer"];
        //            var strMailUser = ConfigurationManager.AppSettings["strMailUser"];
        //            var strMailPassword = ConfigurationManager.AppSettings["strMailPassword"];
        //            var strMailPort = ConfigurationManager.AppSettings["strMailPort"];
        //            var strFromAddress = ConfigurationManager.AppSettings["fromAddress"];
        //            var strBccAddress = ConfigurationManager.AppSettings["bccAddress"];

        //            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //            mail.From = new MailAddress(strFromAddress, "Support", System.Text.Encoding.UTF8);
        //            mail.To.Add(toAddress);
        //            mail.Bcc.Add(strBccAddress);
        //            mail.Subject = "eProperty365 Residential Application Request";
        //            mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //            mail.Body = "Thank You. Your Application has been received. It may take up to 5 working days to approve or denied this application. you will receive a email letting you know either way. ";
        //            mail.BodyEncoding = System.Text.Encoding.UTF8;
        //            mail.IsBodyHtml = true;
        //            mail.Priority = MailPriority.High;
        //            SmtpClient client = new SmtpClient();
        //            client.Credentials = new System.Net.NetworkCredential(strMailUser, strMailPassword);
        //            client.Port = Convert.ToInt32(strMailPort);
        //            client.Host = strMailServer;
        //            client.EnableSsl = true;
        //            try
        //            {
        //                //Add this line to bypass the certificate validation
        //                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
        //                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
        //                        System.Security.Cryptography.X509Certificates.X509Chain chain,
        //                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
        //                {
        //                    return true;
        //                };
        //                client.Send(mail);
        //                mass = "true";
        //                //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
        //            }
        //            catch (Exception ex)
        //            {
        //                Exception ex2 = ex;
        //                string errorMessage = string.Empty;
        //                while (ex2 != null)
        //                {
        //                    errorMessage += ex2.ToString();
        //                    ex2 = ex2.InnerException;
        //                }
        //                mass = "true";
        //                //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    if (mass == "true")
        //    {
        //        try
        //        {
        //            XMLOperationStatus XMLResponse = GetXML();

        //            if (XMLResponse != null)
        //            {
        //                try
        //                {
        //                    var objXML = new ResidentialAddResponceTemplateDa().GetXMLOperationStatus(XMLResponse.TenantId);
        //                    if (objXML != null)
        //                    {
        //                        var sql = " delete from XMLOperationStatus where TenantId = '" + HttpContext.Current.Session["TenentId"].ToString() + "'";
        //                        Utility.RunCMD(sql);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {

        //                }


        //                if (new ResidentialAddResponceTemplateDa().SaveXMLStatus(XMLResponse))
        //                {
        //                    var jsonSerialiser = new JavaScriptSerializer();
        //                    var json = jsonSerialiser.Serialize("true");
        //                    return json;
        //                }
        //                else
        //                {
        //                    var jsonSerialiser = new JavaScriptSerializer();
        //                    var json = jsonSerialiser.Serialize("Background Screen Data Save failed");
        //                    return json;
        //                }
        //            }
        //            else
        //            {
        //                return "Background Screen Data Save failed";
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            return "Background Screen Data Save failed";
        //        }
        //    }
        //    else
        //    {
        //        return "Mail sending failed";
        //    }



        //}

        public static XMLOperationStatus GetXML()
        {
            try
            {
                var strScreenUser = ConfigurationManager.AppSettings["ScreenUser"];
                var strScreenPassword = ConfigurationManager.AppSettings["ScreenPassword"];

                var OrderId = 0;
                var OrderStatus = "";
                var ErrorCode = "";

                var dataSet = new ResidentialAddResponceTemplateDa().uspGetTenantList(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), HttpContext.Current.Session["TenentId"].ToString());
                AgreementNameOfXML objAgreement =
                    dataSet.Tables[0].DataTableToList<AgreementNameOfXML>().FirstOrDefault();

                // Reference
                List<ReferenceXML> objReference =
                      dataSet.Tables[1].DataTableToList<ReferenceXML>();
                // Resident
                List<ResidentialXML> objResidenceHistories =
                    dataSet.Tables[2].DataTableToList<ResidentialXML>();
                //Employeement
                List<EmployeementXML> objEmployeeInfos = dataSet.Tables[3].DataTableToList<EmployeementXML>();

                //Personal Reference
                List<PersonalRefXML> objEmergencyContacts =
                    dataSet.Tables[4].DataTableToList<PersonalRefXML>();


                //<BackgroundCheck userId="username" password="password"> 
                XElement BackgroundCheck = new XElement("BackgroundCheck", new XAttribute("userId", strScreenUser),
                    new XAttribute("password", strScreenPassword));
                // <BackgroundSearchPackage action="submit" type="demo product"> 
               // XElement BackgroundSearchPackage = new XElement("BackgroundSearchPackage", new XAttribute("action", "submit"), new XAttribute("type", "DEMO"));
                XElement BackgroundSearchPackage = new XElement("BackgroundSearchPackage", new XAttribute("action", "submit"), new XAttribute("type", "4500- US - Plus Level Nation Wide Package $25.00"));
                //BackgroundCheck.Add(BackgroundSearchPackage);
                XElement ReferenceId = new XElement("ReferenceId");
                BackgroundSearchPackage.Add(ReferenceId);

                //  <PersonalData> 
                XElement PersonalData = new XElement("PersonalData");
                XElement PersonName = new XElement("PersonName");

                try
                {
                    if (objAgreement != null)
                    {
                        XElement GivenName = new XElement("GivenName", objAgreement.GivenName);
                        XElement MiddleName = new XElement("MiddleName", objAgreement.MiddleName);
                        XElement FamilyName = new XElement("FamilyName", objAgreement.FamilyName);

                        PersonName.Add(GivenName);
                        PersonName.Add(MiddleName);
                        PersonName.Add(FamilyName);

                        PersonalData.Add(PersonName);

                        //XElement Affix = new XElement("Affix", "");
                        //  <DemographicDetail> 
                        XElement DemographicDetail = new XElement("DemographicDetail");

                        XElement GovernmentId = new XElement("GovernmentId", objAgreement.GovernmentI, new XAttribute("countryCode", "US"),
                            new XAttribute("issuingAuthority", "SSN"));
                        //XElement Gender = new XElement("Gender", "M");
                        XElement DateOfBirth = new XElement("DateOfBirth", objAgreement.DateOfBirth);
                        DemographicDetail.Add(GovernmentId);
                        //  DemographicDetail.Add(Gender);
                        DemographicDetail.Add(DateOfBirth);
                        PersonalData.Add(DemographicDetail);

                        XElement PostalAddress = new XElement("PostalAddress");
                        XElement PostalCode = new XElement("PostalCode", objAgreement.PostalCode);
                        XElement Region = new XElement("Region", objAgreement.Region);
                        XElement Municipality = new XElement("Municipality", objAgreement.Municipality);
                        XElement DeliveryAddress = new XElement("DeliveryAddress");
                        XElement AddressLine = new XElement("AddressLine", objAgreement.AddressLine);

                        PostalAddress.Add(PostalCode);
                        PostalAddress.Add(Region);
                        PostalAddress.Add(Municipality);
                        PostalAddress.Add(DeliveryAddress);
                        PersonalData.Add(PostalAddress);
                        DeliveryAddress.Add(AddressLine);

                        XElement EmailAddress = new XElement("EmailAddress", objAgreement.EmailAddress);
                        PersonalData.Add(EmailAddress);
                        XElement Telephone = new XElement("Telephone", objAgreement.Telephone);
                        PersonalData.Add(Telephone);

                    }
                }
                catch (Exception ex)
                {

                }

                XElement Screenings = new XElement("Screenings");

                try
                {

                    if (objReference != null && objReference.Count > 0)
                    {
                        foreach (ReferenceXML aReference in objReference)
                        {
                            XElement Screening = new XElement("Screening", new XAttribute("type", "reference"), new XAttribute("qualifier", "professional"));
                            XElement Contact2 = new XElement("Contact");
                            XElement PersonName2 = new XElement("PersonName");
                            XElement FormattedName2 = new XElement("FormattedName", aReference.FormattedName);
                            XElement ContactMethod2 = new XElement("ContactMethod");
                            XElement Telephone2 = new XElement("Telephone", aReference.Telephone);
                            XElement Relationship2 = new XElement("Relationship", aReference.Relationship);
                            PersonName2.Add(FormattedName2);
                            ContactMethod2.Add(Telephone2);
                            Contact2.Add(PersonName2);
                            Contact2.Add(ContactMethod2);
                            Contact2.Add(Relationship2);
                            Screening.Add(Contact2);
                            Screenings.Add(Screening);
                        }
                    }

                }
                catch (Exception ex)
                {

                }

                try
                {
                   

                    if (objEmergencyContacts != null && objEmergencyContacts.Count > 0)
                    {
                        foreach (PersonalRefXML aReference in objEmergencyContacts)
                        {
                            XElement ScreeningPersonal = new XElement("Screening", new XAttribute("type", "reference"), new XAttribute("qualifier", "personal"));
                            XElement Contact2 = new XElement("Contact");
                            XElement PersonName2 = new XElement("PersonName");
                            XElement FormattedName2 = new XElement("FormattedName", aReference.FormattedName);
                            XElement ContactMethod2 = new XElement("ContactMethod");
                            XElement Telephone2 = new XElement("Telephone", aReference.Telephone);
                            XElement Relationship2 = new XElement("Relationship", aReference.Relationship);
                            PersonName2.Add(FormattedName2);
                            ContactMethod2.Add(Telephone2);
                            Contact2.Add(PersonName2);
                            Contact2.Add(ContactMethod2);
                            Contact2.Add(Relationship2);
                            ScreeningPersonal.Add(Contact2);
                            Screenings.Add(ScreeningPersonal);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                try
                {
                   
                    if (objResidenceHistories != null && objResidenceHistories.Count > 0)
                    {
                        foreach (ResidentialXML aReference in objResidenceHistories)
                        {
                            XElement Screeningresident = new XElement("Screening", new XAttribute("type", "resident"));
                            XElement StartDate = new XElement("StartDate", aReference.StartDat);
                            XElement ContactInfo = new XElement("ContactInfo");
                            XElement PersonName2 = new XElement("PersonName");
                            XElement FormattedName2 = new XElement("FormattedName", aReference.FormattedName);
                            XElement Telephone2 = new XElement("Telephone", aReference.Telephone);
                            XElement PostalAddress2 = new XElement("PostalAddress");
                            XElement PostalCode2 = new XElement("PostalCode", aReference.PostalCode);
                            XElement Region2 = new XElement("Region", aReference.Region);
                            XElement Municipality2 = new XElement("Municipality", aReference.Municipality);
                            XElement DeliveryAddress2 = new XElement("DeliveryAddress");
                            XElement AddressLine2 = new XElement("AddressLine", aReference.AddressLine);
                            Screeningresident.Add(StartDate);
                            PostalAddress2.Add(PostalCode2);
                            PostalAddress2.Add(Region2);
                            PostalAddress2.Add(Municipality2);
                            PostalAddress2.Add(DeliveryAddress2);
                            DeliveryAddress2.Add(AddressLine2);
                            PersonName2.Add(FormattedName2);
                            ContactInfo.Add(PersonName2);
                            ContactInfo.Add(Telephone2);
                            ContactInfo.Add(PostalAddress2);
                            Screeningresident.Add(ContactInfo);
                            Screenings.Add(Screeningresident);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                try
                {
                    if (objEmployeeInfos != null && objEmployeeInfos.Count > 0)
                    {
                        foreach (EmployeementXML aReference in objEmployeeInfos)
                        {
                            XElement Screeningemployment = new XElement("Screening", new XAttribute("type", "employment"));
                            XElement OrganizationName = new XElement("OrganizationName", aReference.OrganizationName);
                            XElement Title = new XElement("Title", aReference.Title);
                            XElement JobLevelInfo = new XElement("JobLevelInfo", aReference.JobLevelInfo);
                            XElement StartDate = new XElement("StartDate", aReference.StartDat);
                            XElement EndDate = new XElement("EndDate", aReference.EndDate);
                            XElement Compensation = new XElement("Compensation", aReference.Compensation);

                            XElement ContactInfo = new XElement("ContactInfo");
                            XElement PersonName2 = new XElement("PersonName");
                            XElement FormattedName2 = new XElement("FormattedName", aReference.FormattedName);
                            XElement Telephone2 = new XElement("Telephone", aReference.Telephone);
                            XElement PostalAddress2 = new XElement("PostalAddress");
                            XElement PostalCode2 = new XElement("PostalCode", aReference.PostalCode);
                            XElement Region2 = new XElement("Region", aReference.Region);
                            XElement Municipality2 = new XElement("Municipality", aReference.Municipality);
                            XElement DeliveryAddress2 = new XElement("DeliveryAddress");
                            XElement AddressLine2 = new XElement("AddressLine", aReference.AddressLine);

                            PostalAddress2.Add(PostalCode2);
                            PostalAddress2.Add(Region2);
                            PostalAddress2.Add(Municipality2);
                            PostalAddress2.Add(DeliveryAddress2);
                            DeliveryAddress2.Add(AddressLine2);
                            PersonName2.Add(FormattedName2);
                            ContactInfo.Add(PersonName2);
                            ContactInfo.Add(Telephone2);
                            ContactInfo.Add(PostalAddress2);

                            Screeningemployment.Add(OrganizationName);
                            Screeningemployment.Add(Title);
                            Screeningemployment.Add(JobLevelInfo);
                            Screeningemployment.Add(StartDate);
                            Screeningemployment.Add(EndDate);
                            Screeningemployment.Add(Compensation);

                            Screeningemployment.Add(ContactInfo);
                            Screenings.Add(Screeningemployment);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                //try
                //{
                //    if (objAgreement != null)
                //    {
                //        XElement ScreeningLicense = new XElement("Screening", new XAttribute("type", "license"), new XAttribute("qualifier", "imvPersonal"));
                //        XElement Region = new XElement("Region", objAgreement.LicenceState);
                //        XElement SearchLicense = new XElement("SearchLicense");
                //        XElement License = new XElement("License");
                //        XElement LicenseNumber = new XElement("LicenseNumber", objAgreement.LicenseNumber);
                //        ScreeningLicense.Add(Region);
                //        License.Add(LicenseNumber);
                //        SearchLicense.Add(License);
                //        ScreeningLicense.Add(SearchLicense);
                //        Screenings.Add(ScreeningLicense);
                //    }

                //}
                //catch (Exception ex)
                //{

                //}

                                  

                try
                {
                    XElement ScreeningcriminalStatewide = new XElement("Screening", new XAttribute("type", "criminal"), new XAttribute("qualifier", "statewide"));                   
                    Screenings.Add(ScreeningcriminalStatewide);

                    XElement Screeningcriminalcounty = new XElement("Screening", new XAttribute("type", "criminal"), new XAttribute("qualifier", "county"));
                    Screenings.Add(Screeningcriminalcounty);

                    XElement ScreeningcivilCounty = new XElement("Screening", new XAttribute("type", "civil"), new XAttribute("qualifier", "county"));
                    Screenings.Add(ScreeningcivilCounty);

                    XElement Screeningcriminalfederal = new XElement("Screening", new XAttribute("type", "criminal"), new XAttribute("qualifier", "federal"));
                    Screenings.Add(Screeningcriminalfederal);

                    XElement Screeningcriminalinternational = new XElement("Screening", new XAttribute("type", "criminal"), new XAttribute("qualifier", "international"));
                    Screenings.Add(Screeningcriminalinternational);

                    XElement Screeningcriminalnational = new XElement("Screening", new XAttribute("type", "criminal"), new XAttribute("qualifier", "national"));
                    Screenings.Add(Screeningcriminalnational);

                    XElement Screeningcriminalsinglestate = new XElement("Screening", new XAttribute("type", "criminal"), new XAttribute("qualifier", "singlestate"));
                    Screenings.Add(Screeningcriminalsinglestate);

                    XElement Screeningcriminalsecurity = new XElement("Screening", new XAttribute("type", "criminal"), new XAttribute("qualifier", "security"));
                    Screenings.Add(Screeningcriminalsecurity);

                    XElement Screeningevictionsinglestate = new XElement("Screening", new XAttribute("type", "eviction"), new XAttribute("qualifier", "singlestate"));
                    Screenings.Add(Screeningevictionsinglestate);

                    XElement additionalEmbed = new XElement("AdditionalItems", new XAttribute("type", "x:embed_credentials"));
                    XElement Text = new XElement("Text", "TRUE");

                    additionalEmbed.Add(Text);
                    Screenings.Add(additionalEmbed);
                }
                catch (Exception ex)
                {
                    
                }

                BackgroundSearchPackage.Add(PersonalData);
                BackgroundSearchPackage.Add(Screenings);
                BackgroundCheck.Add(BackgroundSearchPackage);


                string xmlMessage = BackgroundCheck.ToString();
                //string url = "https://lightning.instascreen.net/send/interchange";
                string url = "https://tenantreports.instascreen.net/send/interchange";                
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

                // with Descendents
                var status = xDoc.Descendants("OrderStatus").Single();

                var status1 = Convert.ToString(status.Value);
                status1 = status1.ToString().Split(':')[1].ToString();

                if (status1 == "pending")
                {
                    var bgReport = xDoc.Root.Element("BackgroundReportPackage");
                    var optinStatus = bgReport.Element("OrderId");
                    OrderId = Convert.ToInt32(optinStatus.Value);
                    OrderStatus = status1;

                }
                else
                {
                    // OrderStatus = status.ToString().Split(':')[1];
                    OrderStatus = status1;
                    var bgReport = xDoc.Root.Element("BackgroundReportPackage");
                    var optinStatus = bgReport.Element("ErrorReport");
                    var ErrorDescription = optinStatus.Element("ErrorDescription");
                    ErrorCode = ErrorDescription.Value;
                }

                respStream.Close();
                response.Close();

                XMLOperationStatus obj = new XMLOperationStatus
                {
                    UnitId = (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(),
                    TenantId = (HttpContext.Current.Session["TenentId"]).ToString(),
                    OrderId = OrderId,
                    Status = OrderStatus,
                    ErrorCode = ErrorCode,
                    CreatedBy = null,
                    CreatedDate = DateTime.Now
                };

                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static HttpWebRequest CreateWebRequest(string URL)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"" + URL);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
        public static string ConsumeSOAP(string XML, string URL)
        {
            HttpWebRequest request = CreateWebRequest(URL);
            // request.Method = "GET";
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"" + XML);

            //using (Stream stream = request.GetRequestStream())
            //{
            //    soapEnvelopeXml.Save(stream);
            //}
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    return soapResult;
                }
            }
        }

    }
}