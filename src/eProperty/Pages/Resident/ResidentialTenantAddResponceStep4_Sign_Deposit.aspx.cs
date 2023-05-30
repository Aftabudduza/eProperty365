using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;
using PropertyService;
using System.Configuration;
using PropertyService.DA;

namespace eProperty.Pages.Resident
{
    public partial class ResidentialTenantAddResponceStep4_Sign_Deposit : System.Web.UI.Page
    {
        public bool isVisible = false;
        public bool isView = true;
        public string api_loginID;
        public string utc_time = DateTime.Now.Ticks.ToString();
        public string pay_now_single_return_string;
        Gateway.Signature cliSignature = new Gateway.Signature();     // This is the worker object that takes the values and performs functions.
        public string return_string = "Initial";
        public string source = "";
        string strLoginID = System.Configuration.ConfigurationManager.AppSettings.Get("LoginID");
        string strSecureKey = System.Configuration.ConfigurationManager.AppSettings.Get("ProcessingKey");
        public string sTotal = "1";
        public string sFeeType = "0";
        public string sPercentFee = "0";
        public string sFlatFee = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Session["ResidentialUnitSerial"] = "100000000004";
                //Session["TenentId"] = "100000000001";
                if (Session["ResidentialUnitSerial"] == null && Session["TenentId"] == null && Session["TenantPassword"] ==null)
                {
                    Response.Redirect("ResidentialTenantAddResponceStep4_Sign_Deposit_Login.aspx");
                }

                api_loginID = strLoginID;

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

                if (Session["ResidentialUnitSerial"] != null && Session["TenentId"] != null)
                {
                    var stepcount = new ResidentialAddResponceTemplateDa().GetStepCount((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    if (stepcount != null)
                    {
                        if (stepcount.Step == 1)
                        {
                            CheckBox1.Checked = true;
                        }
                        else if (stepcount.Step == 2)
                        {
                            CheckBox1.Checked = true;
                            CheckBox2.Checked = true;
                        }
                        else if (stepcount.Step == 3)
                        {
                            CheckBox1.Checked = true;
                            CheckBox2.Checked = true;
                            CheckBox3.Checked = true;
                        }
                        else if (stepcount.Step == 4)
                        {
                            CheckBox1.Checked = true;
                            CheckBox2.Checked = true;
                            CheckBox3.Checked = true;
                            CheckBox4.Checked = true;
                        }
                    }

                    var ResidentialPayment = new ResidentialAddResponceTemplateDa().GetDepositeAmount((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), (HttpContext.Current.Session["TenentId"]).ToString());
                    var get_app =  new ResidentialAddResponceTemplateDa().GetApplicationFree((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    var typeChck = get_app.FeeTypeCheck != null ? get_app.FeeTypeCheck : 0;
                    var percentOrFlatVal = 0.0;
                    var subtotal = 0.0;
                    var bIsTenant = false;
                    if (ResidentialPayment != null)
                    {
                        subtotal = ResidentialPayment.Total != null ? Convert.ToDouble(ResidentialPayment.Total) : 0;
                    }
                    if (get_app != null)
                    {
                        bIsTenant = get_app.TanentPayFees != null ? Convert.ToBoolean(get_app.TanentPayFees) : false;
                    }
                    var nTotal = 0.0;

                    if (bIsTenant == true)
                    {
                        if (typeChck == 1)
                        {
                            percentOrFlatVal = get_app.FeePercentageCheck != null ? Convert.ToDouble(get_app.FeePercentageCheck) : 0;
                            var nCharge = subtotal * percentOrFlatVal / 100;
                            nTotal = subtotal + nCharge;

                            sFeeType = typeChck.ToString();
                            sPercentFee = percentOrFlatVal.ToString();
                            sFlatFee = "0";
                        }
                        else
                        {
                            percentOrFlatVal = get_app.FeeFlatAmountCheck != null ? Convert.ToDouble(get_app.FeeFlatAmountCheck) : 0;
                            nTotal = subtotal + percentOrFlatVal;

                            sFeeType = "0";
                            sPercentFee = "0";
                            sFlatFee = percentOrFlatVal.ToString();
                        }
                    }
                    else
                    {
                        nTotal = subtotal;
                    }                   

                    sTotal = nTotal.ToString();
                }
            }

            // HttpContext.Current.Session["TenantPassword"] = "123";
            cliSignature.api_login_id = strLoginID;            //Add your API login ID.
            cliSignature.secure_trans_key = strSecureKey;    //Add your secure transaction key.    

            cliSignature.pay_now_single_payment = cliSignature.api_login_id + "|sale|1.0|" + sTotal + "|" + utc_time + "|A1234||";
            api_loginID = cliSignature.api_login_id;
            pay_now_single_return_string = Gateway.EndPoint(cliSignature, "CREATESIGNATUREPAYSINGLEAMOUNT");
        }

        [WebMethod(EnableSession = true)]
        public static string GetAdditionalDoc()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()) && HttpContext.Current.Session["TenantPassword"] == null)
                return "";
            var ResidentialDoc = new ResidentialAddResponceTemplateDa().GetRentalDocList_Step4((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), (HttpContext.Current.Session["TenentId"])?.ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ResidentialDoc);
            return json;

        }
        [WebMethod(EnableSession = true)]
        public static string GetDepositeAmount()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
             string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()) && HttpContext.Current.Session["TenantPassword"] == null)
                return "";
            var ResidentialPayment = new ResidentialAddResponceTemplateDa().GetDepositeAmount((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString(), (HttpContext.Current.Session["TenentId"]).ToString());
            var get_app =
              new ResidentialAddResponceTemplateDa().GetApplicationFree((HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
            var typeChck = get_app.FeeTypeCheck;
            var percentOrFlatVal = 0.0;
           

            if(get_app.TanentPayFees != null && Convert.ToBoolean(get_app.TanentPayFees) == true)
            {
                if (typeChck == 1)
                {
                    percentOrFlatVal = Convert.ToDouble(get_app.FeePercentageCheck);
                }
                else
                {
                    percentOrFlatVal = Convert.ToDouble(get_app.FeeFlatAmountCheck);
                }
            }

            List<dynamic> dyOldAllObject = new List<dynamic>();
            dyOldAllObject.Add(new { typeChck = typeChck, percentOrFlatVal= percentOrFlatVal, getobj = ResidentialPayment });
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(dyOldAllObject);
            return json;
        }
        [WebMethod]
        public static string DocViewd(string DocId, string CurrentStatus)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new Residential_Tenant_Add_Step4_DocumentList();
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            obj.ResidentTenantDocId_Step2_Page4 = Convert.ToInt32(DocId);
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

                obj = new Residential_Tenant_Add_Step4_DocumentList();
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
        [WebMethod]
        public static string DocImageUpload(string Image, string ImageName, string DocId, string CurrentStatus, string Getway)
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
               string.IsNullOrEmpty(HttpContext.Current.Session["ResidentialUnitSerial"]?.ToString()))
                return "";
            var obj = new Residential_Tenant_Add_Step4_DocumentList();
            obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
            obj.ResidentTenantDocId_Step2_Page4 = Convert.ToInt32(DocId);
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
                    //  File.Delete(sfullpath);
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
            obj.GetWay = Getway;
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

                obj = new Residential_Tenant_Add_Step4_DocumentList();
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string GetAllOwner()
        {
            if (HttpContext.Current.Session["ResidentialUnitSerial"] == null &&
              string.IsNullOrEmpty((HttpContext.Current.Session["TenentId"])?.ToString()) && HttpContext.Current.Session["TenantPassword"] == null)
                return "";
            var OwnerInfo = new ResidentialAddResponceTemplateDa().GetAllOwnerInfo(((HttpContext.Current.Session["TenentId"])?.ToString()), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(OwnerInfo);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string SaveOwnerSecurityInfo(Residential_Tenant_Add_Step4_Owner_Signature Obj)
        {

            var rsult = new List<Residential_Tenant_Add_Step4_Owner_Signature>();

            if (Obj != null)
            {
                Obj.Serial = (HttpContext.Current.Session["TenentId"])?.ToString();
                Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                if (Obj.Id > 0)
                {
                    var getOldObj = new ResidentialAddResponceTemplateDa().GetSignatureById_Owner(Obj.Id);
                    Obj.UpdateDate = DateTime.Now;
                    Obj.CreateDate = getOldObj.CreateDate;
                    if (new ResidentialAddResponceTemplateDa().UpdateSignature_Owner(Obj))
                    {
                        rsult = new ResidentialAddResponceTemplateDa().GetAllOwnerInfo(((HttpContext.Current.Session["TenentId"])?.ToString()), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    }

                }
                else
                {
                    Obj.CreateDate = DateTime.Now;
                    if (new ResidentialAddResponceTemplateDa().InsertSignature_Owner(Obj))
                    {
                        rsult = new ResidentialAddResponceTemplateDa().GetAllOwnerInfo(((HttpContext.Current.Session["TenentId"])?.ToString()), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
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
            var rsult = new List<Residential_Tenant_Add_Step4_Owner_Signature>();
            if (obj > 0)
            {
                if (new ResidentialAddResponceTemplateDa().DeleteById_Owner(obj))
                {
                    rsult = new ResidentialAddResponceTemplateDa().GetAllOwnerInfo(((HttpContext.Current.Session["TenentId"])?.ToString()), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(rsult);
            return json;
        }
        [WebMethod(EnableSession = true)]        
        public static string SaveAndContinue(TenantRentalFee_Residential Obj)
        {
            var res = true;           

            if (Obj !=null)
            {
                var ResidentialUnit = new ResidentialAddResponceTemplateDa().GetResidentialUnit(HttpContext.Current.Session["ResidentialUnitSerial"].ToString());
                Obj.Serial= (HttpContext.Current.Session["TenentId"]).ToString();
                Obj.ResidentialUnitSerialId = HttpContext.Current.Session["ResidentialUnitSerial"].ToString();
                Obj.OwnerId = ResidentialUnit.OwnerId;
                Obj.PropertyManagerId = ResidentialUnit.PropertyManagerSerialId;
                Obj.LocationId = ResidentialUnit.LocationSerialId;
                Obj.CreateDate = DateTime.Now;

                if (Obj.AccountNumber != string.Empty && Obj.AccountNumber.Length > 4)
                {
                    Obj.AccountNumber = Obj.AccountNumber.Substring(Obj.AccountNumber.Length - 4, 4);
                }
                List<Get_TenantInformation_Result> tenObjInformation = new ResidentialAddResponceTemplateDa().GetTenantInformation(Obj.Serial);

                string sOwnerId = tenObjInformation[0] != null ? tenObjInformation[0].OwnerId : "";
                //OwnerProfile objOwner = new AdminOwnerProfileDA().GetByUserName(sOwnerId);
                //int nOwnerId = objOwner != null ? objOwner.Id : 0;
                //string sNumber = nOwnerId.ToString().PadLeft(4, '0');

                int sPackage = sOwnerId.Length > 1 ? Convert.ToInt32(sOwnerId.Substring(1)) : 1;
                string sNumber = sPackage.ToString().PadLeft(4, '0').ToString();

                string sPrefix = "I" + sNumber;

                var paymentHistory = new PropertyService.BO.TenantPaymentHistory()
                {
                    FromUser = Obj.Serial,
                   // FromUserType = tenObjInformation[0] != null ? Convert.ToInt32(tenObjInformation[0].FromUserType) : 5,
                    ToUser = tenObjInformation[0] != null ? tenObjInformation[0].OwnerId : "",
                    // ToUserType = tenObjInformation[0] != null ? Convert.ToInt32(tenObjInformation[0].ToUserType) : 2,
                    FromUserType = 5,
                    ToUserType = 2,
                    UnitNo = Obj.ResidentialUnitSerialId,
                    Amount = Convert.ToDecimal(Obj.AccountType.Equals("Cash")? Obj.CashAmount : Obj.Total),
                    AccountName = Obj.AccountName,
                    Address = Obj.Address1,
                    Address1 = Obj.Address2,
                    City = Obj.CityName,
                    State = Obj.StateName,
                    Zip = Obj.ZipCode,
                    CardNumber = Obj.PersonLast4CreditCardNumber,
                    //CVS = Obj.C,
                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    LastFourDigitCard = Obj.PersonLast4CreditCardNumber,
                    IsCheckingAccount = Obj.AccountType.Equals("Check") ? true : false,
                    RoutingNo = Obj.RoutingNo,
                    AccountNo = Obj.AccountNumber,
                    CheckNo = "",
                    AccountType = Obj.AccountType,
                    AuthorizationCode = Obj.AuthorizationCode,
                    TransactionCode = Obj.TransactionCode,
                    TransactionDescription = Obj.TransactionDescription,
                    Getway = "Residential Tenant And Responce Step4 Sign Deposit",
                    DebitAmount = 0,
                    CreditAmount = Convert.ToDecimal(Obj.AccountType.Equals("Cash") ? Obj.CashAmount : Obj.Total),
                    CreateDate = DateTime.Now,
                    Status = "pending",
                    Serial = new ResidentialAddResponceTemplateDa().MakeAutoGenSerialForPaymentHistory(sPrefix, "Invoice"),
                    TransactionType = "SignUpFee",
                    LedgerCode = "1040",
                    Remarks = ""
                };

                int paymentHistoryId = 0;

                var ExistingHistory = new ResidentialAddResponceTemplateDa().GetTenantPaymentHistory(Obj.ResidentialUnitSerialId, Obj.Serial, "Residential Tenant And Responce Step4 Sign Deposit");

                if (ExistingHistory != null)
                {
                    //paymentHistory.Id = ExistingHistory.Id;
                    //if (new ResidentialAddResponceTemplateDa().UpdateTenantPaymentHistory(Obj.ResidentialUnitSerialId, Obj.Serial, "Residential Tenant And Responce Step4 Sign Deposit"))
                    //{
                    //    paymentHistoryId = ExistingHistory.Id;
                    //}
                    
                    if (new ResidentialAddResponceTemplateDa().DeletePaymentHistoryById(ExistingHistory.Id))
                    {
                        paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);
                    }
                }
                else
                {
                    paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);
                }

               
                if (paymentHistoryId > 0)
                {                   
                    TenantRentalFee objTenantRentalFee = new ResidentialAddResponceTemplateDa().GetTenantRentalFeeById(paymentHistory.FromUser);
                    if (objTenantRentalFee != null)
                    {
                        objTenantRentalFee.LeaseSignedDate = DateTime.Now;
                        objTenantRentalFee.SecurityDeposit = Obj.SecurityDeposit;
                        objTenantRentalFee.MonthlyRent = Obj.MonthlyRent;
                        objTenantRentalFee.FirstMonthRent = Obj.FirstMonthRent;
                        objTenantRentalFee.ProrateAmount = Obj.ProrateAmount;
                        objTenantRentalFee.Total = Obj.SubTotalCharge;

                        if (new ResidentialAddResponceTemplateDa().UpdateTenantRentalFee(objTenantRentalFee))
                        {
                        }
                    }

                    TenantRentalFee_Residential objTenantRentalFee_Residential = new ResidentialAddResponceTemplateDa().GetByApplicationId(Obj.Serial);
                    if (objTenantRentalFee_Residential != null)
                    {
                        Obj.Id = objTenantRentalFee_Residential.Id;
                        if (new ResidentialAddResponceTemplateDa().UpdateTenantRentalFee_Residential(Obj))
                        {
                            bool nextStep = new ResidentialAddResponceTemplateDa().SetUpStep(Obj.Serial, Obj.ResidentialUnitSerialId, "Residential");
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }
                    }
                    else
                    {
                        if (new ResidentialAddResponceTemplateDa().InsertTenant_Resident(Obj))
                        {
                            bool nextStep = new ResidentialAddResponceTemplateDa().SetUpStep(Obj.Serial, Obj.ResidentialUnitSerialId, "Residential");
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }
                    }

                    try
                    {
                        var get_app = new ResidentialAddResponceTemplateDa().GetApplicationFree(Obj.ResidentialUnitSerialId.ToString());
                        var typeChck = get_app.FeeTypeCheck != null ? get_app.FeeTypeCheck : 0;
                        var percentOrFlatVal = 0.0;
                        var subtotal = Obj.Total != null ? Convert.ToDouble(Obj.Total) : 0;
                        var nTotal = 0.0;

                        if (get_app.IncludeProcessFees != null && Convert.ToBoolean(get_app.IncludeProcessFees) == true && Convert.ToBoolean(get_app.TanentPayFees) == false)
                        {
                            if (typeChck == 1)
                            {
                                percentOrFlatVal = get_app.FeePercentageCheck != null ? Convert.ToDouble(get_app.FeePercentageCheck) : 0;
                                var nCharge = subtotal * percentOrFlatVal / 100;
                                nTotal = nCharge;
                            }
                            else
                            {
                                percentOrFlatVal = get_app.FeeFlatAmountCheck != null ? Convert.ToDouble(get_app.FeeFlatAmountCheck) : 0;
                                nTotal = percentOrFlatVal;
                            }
                        }

                        if (nTotal > 0)
                        {
                            string sDelBillSQL = "delete from BillPayment where InvoiceNo = '" + paymentHistory.Serial + "'";
                            Utility.RunCMD(sDelBillSQL);

                            var achFee = new BillPayment()
                            {
                                RefId = paymentHistory.ToUser,
                                InvoiceNo = paymentHistory.Serial,
                                TransactionType = "ACH Process Fee",
                                AccountType = "COG",
                                Amount = Convert.ToDecimal(nTotal),
                                LedgerCode = "5040",
                                Debit = Convert.ToDecimal(nTotal),
                                Credit = 0,
                                CreateDate = DateTime.Now,
                                Remarks = "ach",
                                Status = paymentHistory.Status,
                                UnitId = paymentHistory.UnitNo,
                                UserType = paymentHistory.ToUserType.ToString(),
                                OwnerId = paymentHistory.ToUser,
                                Month = paymentHistory.Month,
                                Year = paymentHistory.Year
                            };
                            if (new BillPaymentDA(true, false).Insert(achFee))
                            {

                            }
                        }
                    }
                    catch(Exception ex)
                    {

                    }

                    //try
                    //{
                    //    var sql = " update ResidentialTenantSignIn set ApproveStatus = 'Complete'  where SerialId = '" + HttpContext.Current.Session["TenentId"].ToString() + "'";
                    //    Utility.RunCMD(sql);
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                   

                }
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
 
        [WebMethod(EnableSession = true)]
        public static string GetApprovalCode()
        {
            var TenantList =new ResidentialAddResponceTemplateDa().GetApprovalCode(HttpContext.Current.Session["ResidentialUnitSerial"].ToString(), (HttpContext.Current.Session["TenentId"]).ToString(),HttpContext.Current.Session["TenantPassword"].ToString());
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(TenantList);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string GetTenantFee()
        {
            if (HttpContext.Current.Session["TenentId"] == null)
                return "";
            var ResidentialPayment = new ResidentialAddResponceTemplateDa().GetByApplicationId((HttpContext.Current.Session["TenentId"]).ToString());           
          
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(ResidentialPayment);
            return json;
        }

    }
}