using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialTenantRental_App_Page_4_old : System.Web.UI.Page
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
        public static string VerifyImageUpload(string Image, string ImageName,string Description)
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
            string uploadPath = "~/Uploads/Income/"+ HttpContext.Current.Session["TenentId"].ToString();
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
            //if (!File.Exists(sfullpath))
            //{
            //    File.WriteAllBytes(sfullpath, getImageData);
            //}

            obj.FileName = ImageName;
            obj.FilePath = "../../Uploads/Income/" + HttpContext.Current.Session["TenentId"].ToString() + "/" + obj.FileName; ;
            obj.Serial = HttpContext.Current.Session["TenentId"].ToString();
            obj.DocumentDescription = Description;


            var returnObj =new List<Residential_Tenant_Add_Step2_Page4_VerityIncome>();
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
        public static string DocImageUpload(string Image, string ImageName,string DocId,string CurrentStatus,string DocumentDescription)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new ResidentalDocumentListOfRentalAdd_Step2_Page4_Tenant();
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            obj.ResidentTenantDocId = 0;// Convert.ToInt32(DocId);
            obj.DocumentDescription = DocumentDescription;
            obj.CurrentStatus = CurrentStatus;
            obj.GetWay = "ResidentialStep2";
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

            //if (!File.Exists(sfullpath))
            //{
            //    File.WriteAllBytes(sfullpath, getImageData);
            //}

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
            if (obj>0)
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
        public static string FinalSubmit(string investigateReport,string CredtReport)
        {
            var mass = "";

            if (new ResidentialAddResponceTemplateDa().UpdateTenantApproveStatus(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(),HttpContext.Current.Session["TenentId"].ToString()))
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
           
            return mass;
        }
    }
}