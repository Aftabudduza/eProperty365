using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;
using System.Configuration;
using System.Net.Mail;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialAddResponseTemplateNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //spanYear.InnerHtml = DateTime.UtcNow.Year.ToString();
                //Session["ResidentialUnitSerial"] = "100000000004";
                // Session["TenentId"] = "100000000001";
                // if (Session["UserObject"] != null)
                //{
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
                if (residentialId != "")
                {
                    Session["ResidentialUnitSerial"] = residentialId;
                }
                if (tenantId != "")
                {
                    Session["TenentId"] = tenantId;
                }

                Utility.DisplayMsg("Registration successful !! TenentId: " + tenantId, this);

                // }
            }
        }
        [WebMethod(EnableSession = true)]
        public static string GetAllIamge()
        {

            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
                string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var imageobj = new List<ResidentialUnitWebImage>();

            try
            {
                var residentialUnitWebImage = new ResidentialUnitDa().GetAllWebpagesListbyIsTrue(
                    HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
                if (residentialUnitWebImage.Count > 0)
                {
                    foreach (ResidentialUnitWebImage aObj in residentialUnitWebImage)
                    {
                        ResidentialUnitWebImage obj = new ResidentialUnitWebImage();
                        obj.ImagePath = "../../Uploads/Images/" + aObj.ImageName;
                        obj.ShortDescription = aObj.ShortDescription;
                        imageobj.Add(obj);
                    }
                }

                //"..\..\Uploads\Images\images(2).jpg"
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(imageobj);
                return json;
            }
            catch (Exception)
            {

                return "";
            }
        }
        [WebMethod]
        public static string GetResidentialQuickFeaturesView()
        {
            List<usp_GetResidentialFeatureSpecs_Result> accObj = new List<usp_GetResidentialFeatureSpecs_Result>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] != null)
            {
                accObj = new ResidentialUnitDa().GetAllFeatureList(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
               // accObj = new ResidentialUnitDa().GetAllFeatureList(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(accObj);
            return json;

        }
        [WebMethod]
        public static string LoadResidentialGrid()
        {
            List<ResidentialUnit> obj = new List<ResidentialUnit>();
            List<ResidentialUnit> result = new List<ResidentialUnit>();
            if (HttpContext.Current.Session["OwnerId"] != null)
                obj = new ResidentialUnitDa().GetByOwnerData(HttpContext.Current.Session["OwnerId"].ToString());
            foreach (ResidentialUnit a in obj)
            {
                ResidentialUnit r = new ResidentialUnit();
                r.Serial = a.Serial;
                r.UnitName = a.UnitName;
                r.Id = a.Id;
                result.Add(r);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(result);
            return json;
        }
        [WebMethod]
        public static string GetOtherData()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var specData = new ResidentialUnitDa().GetAllResidentialUnitSpecsList(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(specData);
            return json;
        }
        [WebMethod]
        public static string GetRentData()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            string sForRent = "1";
            var imageData = new ResidentialUnitDa().GetbySerial(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            if(imageData != null)
            {
                sForRent = string.IsNullOrEmpty(imageData.ImageType) ? "1" : imageData.ImageType;
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(sForRent);
            return json;
        }
        [WebMethod]
        public static string Save(Showing_Database obj)
        {
            if (HttpContext.Current.Session["TenentId"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["TenentId"]?.ToString()))
                return "";
            var GetTheOldObject =
                 //new ResidentialAddResponceTemplateDa().GetbyId(Convert.ToInt32(HttpContext.Current.Session["TenentId"]));
                 new ResidentialAddResponceTemplateDa().GetbyId(obj.Id);

            var objResidentialMasterTable = new ResidentialAddResponceTemplateDa().GetOwnerPropertyAndLocationInfo(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
            var UnitInfo =
                new ResidentialAddResponceTemplateDa().GetResidentialUnitSpecs(
                    HttpContext.Current.Session["ResidentialUnitSerial"].ToString());

            var objTenant = new ResidentialAddResponceTemplateDa().GetTenantbyId(HttpContext.Current.Session["TenentId"].ToString());

            obj.Showing_Id = new ResidentialUnitDa().MakeAutoGenLocation("1", "ShowingDatabase");

            obj.Tenant_Id = HttpContext.Current.Session["TenentId"]?.ToString();
            obj.Showing_Owner_Id = objResidentialMasterTable.OwnerId;
            obj.Showing_Owner_Name = objResidentialMasterTable.OwnerName;
            obj.Showing_Property_Manager_Id = objResidentialMasterTable.PropertyManagerSerialId;
            obj.Showing_Property_Manager_Name = objResidentialMasterTable.PropertyManagerName;
            obj.Showing_Type_Of_Property = objResidentialMasterTable.UnitType;
            obj.Showing_Location = objResidentialMasterTable.LocationName;
            obj.Showing_Unit_Id = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            obj.Showing_First_Name = objTenant.FirstName;
            obj.Showing_Last_Name = objTenant.LastName;
            obj.Showing_Phone_No = objTenant.PhoneNumber;
            obj.Unit_Lot_Size =Convert.ToInt16(UnitInfo.UnitLotSize) ;
            obj.Unit_Application_free = UnitInfo.UnitApplicationFee;
            obj.Unit_Inclued_Credit_Card_Process_Charge = 3;
            obj.Unit_Agent_Name = UnitInfo.UnitAgentName;

            obj.Unit_Agent_Phone = UnitInfo.UnitAgentPhone;
            obj.Unit_Agent_Email = "";
            obj.Unit_Brocker_Name = UnitInfo.UnitBrokerName;
            obj.Unit_Brocker_Phone = UnitInfo.UnitBrokerPhone;
            obj.Unit_Brocker_Email = "";
            obj.Unit_Special_Statements = "";

            var specData = new ResidentialAddResponceTemplateDa().Insert(obj);

            System.Text.StringBuilder emailbody = new System.Text.StringBuilder();

            if (specData)
            {
                //new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");
               
                string sWeb = System.Configuration.ConfigurationManager.AppSettings.Get("WebURL");

                try
                {
                    emailbody.Append("<table>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'><img  alt='eProperty365' width='120' src='" + sWeb + "/Images/logo.png'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2' style='text-align:Left;font-size:14px;font-weight:bold;color:0000cc;'>Dear Owner,</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'><p>The Tenant make a schedule to see the property</p> </td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'><p>Date: " + obj.Showing_Date + "</p> </td></tr>");
                    emailbody.Append("<tr><td colspan='2'><p>Time: " + obj.Showing_Time + "</p></td></tr>");
                    emailbody.Append("<tr><td colspan='2'><p>Cell Phone Number: " + obj.Showing_Phone_No + "</p></td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'>Best regards</td></tr>");
                    emailbody.Append("<tr><td colspan='2'></td></tr>");
                    emailbody.Append("<tr><td colspan='2'><p>Management</p> </td></tr>");
                    emailbody.Append("</table>");



                }
                catch (Exception ex)
                {
                }

                try
                {
                    var strFromAddress = "";
                    var strToAddress = "";
                    if (HttpContext.Current.Session["TenentId"] == null && string.IsNullOrEmpty(HttpContext.Current.Session["TenentId"]?.ToString()))
                    {
                        strFromAddress = ConfigurationManager.AppSettings["fromAddress"];
                    }
                    else
                    {
                        strFromAddress = new ResidentialAddResponceTemplateDa().GetTenantEmailAddress((HttpContext.Current.Session["TenentId"])?.ToString());
                    }

                    SystemInformation objSystem = new SystemInformationDA().GetByOwner(obj.Showing_Owner_Id);
                    if(objSystem != null && !string.IsNullOrEmpty(objSystem.ComEmailAddress1))
                    {
                        strToAddress = objSystem.ComEmailAddress1;
                    }
                    else
                    {
                        strToAddress = ConfigurationManager.AppSettings["fromAddress"];
                    }

                    var strMailServer = ConfigurationManager.AppSettings["strMailServer"];
                    var strMailUser = ConfigurationManager.AppSettings["strMailUser"];
                    var strMailPassword = ConfigurationManager.AppSettings["strMailPassword"];
                    var strMailPort = ConfigurationManager.AppSettings["strMailPort"];
                    
                    var strBccAddress = ConfigurationManager.AppSettings["bccAddress"];

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.From = new MailAddress(strFromAddress, "Support", System.Text.Encoding.UTF8);
                    mail.To.Add(strToAddress);
                    mail.Bcc.Add(strBccAddress);
                    mail.Subject = "eProperty365 Appointment Request";
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = emailbody.ToString();
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

                    }
                }
                catch (Exception ex)
                {
                }
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(specData);
            return json;
           
        }

        [WebMethod]
        public static string apply(Showing_Database obj)
        {
            bool isSave = true;
            var tenantModel = new TenantSignInModel();

            var stepcount = new ResidentialAddResponceTemplateDa().GetStepCount((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
            if (stepcount.Step == 0 && stepcount.Sub_Step == 0)
            {
                isSave= new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");
            }
            // isSave = new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");

            tenantModel.Serial = HttpContext.Current.Session["TenentId"].ToString();
            tenantModel.isFirstSignIn = isSave;
            tenantModel.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(tenantModel);
            return json;
        }


    }
}