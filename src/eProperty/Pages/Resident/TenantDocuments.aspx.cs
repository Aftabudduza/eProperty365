using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA.Admin.ResidentialTenent;
using System.IO;
using System.Security.AccessControl;

namespace eProperty.Pages.Resident
{
    public partial class TenantDocuments : System.Web.UI.Page
    {
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
                    
                }
               
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
    }
}