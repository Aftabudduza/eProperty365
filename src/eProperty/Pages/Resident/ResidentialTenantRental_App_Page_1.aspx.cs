using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using eProperty.Models;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;
using System.Xml.Linq;
using System.Net;
using System.IO;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialTenantRental_App_Page_1 : System.Web.UI.Page
    {
        public bool isVisible = false;
        public bool isView = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ResidentialUnitSerial"] == null && Session["TenentId"] ==null)
                {
                    Response.Redirect("ResidentialAddResponceTemplate_Login.aspx");
                }
                //Session["ResidentialUnitSerial"] = "100000000004";
                //Session["TenentId"] = "100000000001";

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

            //Session["NextPageUrl"] = "/ResidentialTenantRental_App_Page_1.aspx";
            //Session["PreviousPageUrl"] = "/ResidentialAddResponceTemplate_Login.aspx";
          
        }

        [WebMethod]
        public static string GetCountry()
        {
            var lstOfComboData = new List<ComboData>();
            var lstOfCountry = new ResidentialUnitDa().GetCountrlList();
            foreach (RefCountries aCountry in lstOfCountry)
            {
                ComboData c = new ComboData();
                c.Data = aCountry.COUNTRYNAME;
                c.Id2 = aCountry.COUNTRY;
                lstOfComboData.Add(c);

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstOfComboData);
            return json;
        }

        [WebMethod]
        public static string GetAggrementTenantBasicInformation()
        {
            if(HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantAgrementNameOf((HttpContext.Current.Session["TenentId"]).ToString(),(HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
            
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }

        [WebMethod]
        public static string GetTenantResidentHistoryList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentHistoryList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }
        [WebMethod]
        public static string GetTenantResidentEmployeeInformationList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentEmployeeInformationList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }
        [WebMethod]
        public static string GetTenantResidentReferenceList()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantBasicData);
            return json;

        }
        //SaveResidentHistory
        [WebMethod]
        public static string SaveResidentHistory(Residential_Tenant_Add_Step2_ResidenceHistory Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_Add_Step2_ResidenceHistory>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            if (Obj != null)
            {
                if (Obj.Id >0)
                {
                    var NewRentalTenantResidenceHistory = new ResidentialAddResponceTemplateDa().GetTenantResidenceHistoryById(Obj.Id);

                    Obj.Serial = NewRentalTenantResidenceHistory.Serial;
                    Obj.ResidentialUnitSerialId = NewRentalTenantResidenceHistory.ResidentialUnitSerialId;
                    //obj.IsViewedOrDownloaded = NewRentalDocumentObject.IsViewedOrDownloaded;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantResidenceHistory(Obj))
                    {
                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantResidentHistoryList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

                    }
                }
                else
                {
                    Obj.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
                    Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                  
                    if (new ResidentialAddResponceTemplateDa().InsertResidentialTenantResidenceHistory(Obj))
                    {
                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantResidentHistoryList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
            }
          //  var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;

        }
        [WebMethod(EnableSession = true)]
        public static string DeleteTenantResidenceHistory(Residential_Tenant_Add_Step2_ResidenceHistory Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_Add_Step2_ResidenceHistory>();
            if (Obj.Id > 0)
            {
                try
                {
                    Residential_Tenant_Add_Step2_ResidenceHistory newObj = new ResidentialAddResponceTemplateDa().GetTenantResidenceHistoryById(Obj.Id);
                    newObj.DeletedBy = (HttpContext.Current.Session["TenentId"]).ToString();
                    newObj.Deleteddate = DateTime.Now;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantResidenceHistory(newObj))
                    {

                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantResidentHistoryList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
                catch (Exception)
                {


                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string EditTenantResidenceHistory(Residential_Tenant_Add_Step2_ResidenceHistory Obj)
        {
            var insertedOrUpdatedRecord = new Residential_Tenant_Add_Step2_ResidenceHistory();
            if (Obj.Id > 0)
            {
                try
                {
                    insertedOrUpdatedRecord  = new ResidentialAddResponceTemplateDa().GetTenantResidenceHistoryById(Obj.Id);
                   
                }
                catch (Exception)
                {


                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;
        }




        [WebMethod(EnableSession = true)]
        public static string GetAllRelationship()
        {
            var allRelation = new ResidentialAddResponceTemplateDa().GetAllRelationshipData();
            var lstOfComboData = new List<ComboData>();
          
            foreach (RelationShip aRelation in allRelation)
            {
                ComboData c = new ComboData();
                c.Data = aRelation.RelationShipName;
                c.Id = aRelation.Id;
                lstOfComboData.Add(c);

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstOfComboData);
            return json;
           
        }


        [WebMethod]
        public static string SaveEmployeeInformation(Residential_Tenant_App_Step2_EmployeeInfo Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_App_Step2_EmployeeInfo>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            if (Obj != null)
            {
                if (Obj.Id > 0)
                {
                    var NewRentalTenantResidenceHistory = new ResidentialAddResponceTemplateDa().GetTenantResidenceEmployeeById(Obj.Id);

                    Obj.Serial = NewRentalTenantResidenceHistory.Serial;
                    Obj.ResidentialUnitSerialId = NewRentalTenantResidenceHistory.ResidentialUnitSerialId;
                    //obj.IsViewedOrDownloaded = NewRentalDocumentObject.IsViewedOrDownloaded;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantEmployee(Obj))
                    {
                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantEmployeeList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

                    }
                }
                else
                {
                    Obj.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
                    Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

                    if (new ResidentialAddResponceTemplateDa().InsertResidentialEmployee(Obj))
                    {
                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantEmployeeList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
            }
            //  var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;

        }
        [WebMethod(EnableSession = true)]
        public static string DeleteTenantEmployeeInfo(Residential_Tenant_App_Step2_EmployeeInfo Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_App_Step2_EmployeeInfo>();
            if (Obj.Id > 0)
            {
                try
                {
                    Residential_Tenant_App_Step2_EmployeeInfo newObj = new ResidentialAddResponceTemplateDa().GetTenantResidenceEmployeeById(Obj.Id);
                    newObj.DeletedBy = (HttpContext.Current.Session["TenentId"]).ToString();
                    newObj.Deleteddate = DateTime.Now;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantEmployee(newObj))
                    {

                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantEmployeeList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
                catch (Exception)
                {


                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string EditTenantEmployee(Residential_Tenant_App_Step2_EmployeeInfo Obj)
        {
            var insertedOrUpdatedRecord = new Residential_Tenant_App_Step2_EmployeeInfo();
            if (Obj.Id > 0)
            {
                try
                {
                    insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantResidenceEmployeeById(Obj.Id);

                }
                catch (Exception)
                {


                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;
        }







        [WebMethod]
        public static string SaveReference(Residential_Tenant_App_Step2_Reference Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_App_Step2_Reference>();
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            if (Obj != null)
            {
                if (Obj.Id > 0)
                {
                    var NewRentalTenantResidenceHistory = new ResidentialAddResponceTemplateDa().GetTenantReferenceById(Obj.Id);

                    Obj.Serial = NewRentalTenantResidenceHistory.Serial;
                    Obj.ResidentialUnitSerialId = NewRentalTenantResidenceHistory.ResidentialUnitSerialId;
                    //obj.IsViewedOrDownloaded = NewRentalDocumentObject.IsViewedOrDownloaded;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantReference(Obj))
                    {
                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantRefenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

                    }
                }
                else
                {
                    Obj.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
                    Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

                    if (new ResidentialAddResponceTemplateDa().InsertResidentialReference(Obj))
                    {
                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantRefenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
            }
            //  var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;

        }
        [WebMethod(EnableSession = true)]
        public static string DeleteTenantRefenceInfo(Residential_Tenant_App_Step2_Reference Obj)
        {
            var insertedOrUpdatedRecord = new List<Residential_Tenant_App_Step2_Reference>();
            if (Obj.Id > 0)
            {
                try
                {
                    Residential_Tenant_App_Step2_Reference newObj = new ResidentialAddResponceTemplateDa().GetTenantReferenceById(Obj.Id);
                    newObj.DeletedBy = (HttpContext.Current.Session["TenentId"]).ToString();
                    newObj.Deleteddate = DateTime.Now;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantReference(newObj))
                    {

                        insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantRefenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
                catch (Exception)
                {


                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string EditTenantReference(Residential_Tenant_App_Step2_Reference Obj)
        {
            var insertedOrUpdatedRecord = new Residential_Tenant_App_Step2_Reference();
            if (Obj.Id > 0)
            {
                try
                {
                    insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantReferenceById(Obj.Id);

                }
                catch (Exception)
                {


                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(insertedOrUpdatedRecord);
            return json;
        }

        [WebMethod]
        public static string SaveAndCotinue(Residential_Tenant_App_Step2_AgreementNameOf Obj)
        {
            var nextStep =true ;
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()))
                return "";
            if (Obj != null)
            {
                if (Obj.Id > 0)
                {
                    var NewRentalTenantResidenceHistory = new ResidentialAddResponceTemplateDa().GetTenantBasicInfoById(Obj.Id);

                    Obj.Serial = NewRentalTenantResidenceHistory.Serial;
                    Obj.ResidentialUnitSerialId = NewRentalTenantResidenceHistory.ResidentialUnitSerialId;
                    //obj.IsViewedOrDownloaded = NewRentalDocumentObject.IsViewedOrDownloaded;
                    if (new ResidentialAddResponceTemplateDa().UpdateResidentialTenantBasicInfo(Obj))
                    {
                       // nextStep = new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");
                        // insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantRefenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

                    }
                }
                else
                {
                    Obj.Serial = (HttpContext.Current.Session["TenentId"]).ToString();
                    Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

                    if (new ResidentialAddResponceTemplateDa().InsertResidentialBasic(Obj))
                    {
                        nextStep = new ResidentialAddResponceTemplateDa().SetUpStep((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), "Residential");
                       // insertedOrUpdatedRecord = new ResidentialAddResponceTemplateDa().GetTenantRefenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }
                }
            }
            //  var TenantBasicData = new ResidentialAddResponceTemplateDa().GetTenantResidentReferenceList((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(nextStep);
            return json;

        }

        [WebMethod]
        public static string apply()
        {
            bool isSave = true;
            var tenantModel = new TenantSignInModel();
           
            tenantModel.Serial = HttpContext.Current.Session["TenentId"].ToString();
            tenantModel.isFirstSignIn = isSave;
            tenantModel.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();

            //GetXML();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(tenantModel);
            return json;
        }

        public static void GetXML()
        {

            XElement BackgroundCheck = new XElement("BackgroundCheck", new XAttribute("userId", "butcher_xml"),
                new XAttribute("password", "butcherPass1"));
            XElement BackgroundSearchPackage = new XElement("BackgroundSearchPackage",
                new XAttribute("action", "submit"), new XAttribute("type", "DEMO"));
            XElement ReferenceId = new XElement("ReferenceId");
            XElement PersonalData = new XElement("PersonalData");

            XElement PersonName = new XElement("PersonName");

            XElement GivenName = new XElement("GivenName", "Dwight");
            XElement MiddleName = new XElement("MiddleName", "Kurt");
            XElement FamilyName = new XElement("FamilyName", "Schrute");
            XElement Affix = new XElement("Affix", "III");

            XElement DemographicDetail = new XElement("DemographicDetail");

            XElement GovernmentId = new XElement("GovernmentId", "111 - 22 - 3333", new XAttribute("countryCode", "US"),
                new XAttribute("issuingAuthority", "SSN"));
            XElement Gender = new XElement("Gender", "M");
            XElement DateOfBirth = new XElement("DateOfBirth", "1974-01-20");
            XElement PostalAddress = new XElement("PostalAddress");
            XElement PostalCode = new XElement("PostalCode", "18505");
            XElement Region = new XElement("Region", "PA");
            XElement Municipality = new XElement("Municipality", "Scranton");
            XElement DeliveryAddress = new XElement("DeliveryAddress");
            XElement AddressLine = new XElement("AddressLine", "1725 Slough Avenue");

            XElement EmailAddress = new XElement("EmailAddress", "test@noemail.com");
            XElement Telephone = new XElement("Telephone", "717-555-0177");


            XElement Screenings = new XElement("Screenings", new XAttribute("useConfigurationDefaults", "yes"));
            XElement AdditionalItems = new XElement("AdditionalItems", new XAttribute("type", "x:postback_url"));
            XElement Text = new XElement("Text", "https://www.webhook.site");
            XElement UserArea = new XElement("UserArea");
            XElement PositionDetail = new XElement("PositionDetail");

            BackgroundCheck.Add(BackgroundSearchPackage);
            BackgroundSearchPackage.Add(ReferenceId);
            BackgroundSearchPackage.Add(PersonalData);
            BackgroundSearchPackage.Add(Screenings);
            BackgroundSearchPackage.Add(UserArea);
            PersonalData.Add(PersonName);
            PersonalData.Add(DemographicDetail);
            PersonalData.Add(PostalAddress);
            PersonalData.Add(EmailAddress);
            PersonalData.Add(Telephone);

            PersonName.Add(GivenName);
            PersonName.Add(MiddleName);
            PersonName.Add(FamilyName);
            PersonName.Add(Affix);

            DemographicDetail.Add(GovernmentId);
            DemographicDetail.Add(Gender);
            DemographicDetail.Add(DateOfBirth);

            PostalAddress.Add(PostalCode);
            PostalAddress.Add(Region);
            PostalAddress.Add(Municipality);
            PostalAddress.Add(DeliveryAddress);
            DeliveryAddress.Add(AddressLine);
            Screenings.Add(AdditionalItems);
            AdditionalItems.Add(Text);
            UserArea.Add(PositionDetail);
            

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
            Console.WriteLine(receivedResponse);
            respStream.Close();
            response.Close();

        }

    }
}