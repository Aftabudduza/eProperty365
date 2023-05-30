using eProperty.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.ViewModel;

namespace eProperty.Pages.Resident
{
    public partial class ManagementDashboard : System.Web.UI.Page
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
        public static string GetInitialData()
        {
            List<List<ComboData>> lstComboData = new List<List<ComboData>>();
            string OwnerId = "-1";
            if (HttpContext.Current.Session["OwnerId"] != null)
            {
                OwnerId = HttpContext.Current.Session["OwnerId"].ToString();
            }
            OwnerProfile lstOwnerProfile = new ManagementDashboardDA().GetOwner(OwnerId);
            List<PropertyManagerProfile> lstPropertyManagerProfile = new ManagementDashboardDA().GetPropertyManagerProfile(OwnerId);
            List<ComboData> lstCmb = new List<ComboData>();
            ComboData cmb = new ComboData()
            {
                Id2 = lstOwnerProfile.Serial,
                Data = lstOwnerProfile.FirstName + " " + lstOwnerProfile.LastName,
                //Id3 = lstPropertyManagerProfile.Find(x => x.OwnerId == lstOwnerProfile.Serial) == null ? "-1" : lstPropertyManagerProfile.Find(x => x.OwnerId == lstOwnerProfile.Serial).Serial
            };
            lstCmb.Add(cmb);
            lstComboData.Add(lstCmb);
            if (lstPropertyManagerProfile.Count > 0)
            {
                lstCmb = new List<ComboData>();
                foreach (var obj in lstPropertyManagerProfile)
                {
                    ComboData cmb2 = new ComboData()
                    {
                        Id2 = obj.Serial,
                        Data = obj.FirstName + " " + obj.LastName
                    };
                    lstCmb.Add(cmb2);
                }
                lstComboData.Add(lstCmb);
            }
            List<Location> lstLocation = new ManagementDashboardDA().GetLocationByOwnerId(OwnerId);
            if (lstLocation.Count > 0)
            {
                lstCmb = new List<ComboData>();
                foreach (var obj in lstLocation)
                {
                    ComboData cmb3 = new ComboData()
                    {
                        Id2 = obj.Serial,
                        Data = obj.LocationName
                    };
                    lstCmb.Add(cmb3);
                }
                lstComboData.Add(lstCmb);
            }
            List<ResidentialUnit> lstUnit = new ManagementDashboardDA().GetResidentialUnitByOwnerId(OwnerId);

            if (lstUnit.Count > 0)
            {
                lstCmb = new List<ComboData>();
                foreach (var obj in lstUnit)
                {
                    ResidentialTenantSignIn objTenantSignIn =
                        new ManagementDashboardDA().ResidentialTenantByUnitId(obj.Serial);
                    if (objTenantSignIn !=null)
                    {
                        ComboData cmb4 = new ComboData()
                        {
                            Id2 = obj.Serial,
                            Data = obj.UnitName,
                            Id3 = objTenantSignIn.SerialId,
                            SelectedField = objTenantSignIn.FirstName + " " + objTenantSignIn.LastName
                        };
                        lstCmb.Add(cmb4);
                    }
                   
                }
                lstComboData.Add(lstCmb);
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod]
        public static string GetLocationUnitData(EventManagement Obj)
        {
            List<List<ComboData>> lstComboData = new List<List<ComboData>>();
            List<Location> lstLocation = new ManagementDashboardDA().GetLocationByOwnerPropertyManager(Obj.OwnerId,Obj.PropertyManagerId);
            List<ComboData> lstCmb = new List<ComboData>();
            if (lstLocation.Count > 0)
            {
                lstCmb = new List<ComboData>();
                foreach (var obj in lstLocation)
                {
                    ComboData cmb = new ComboData()
                    {
                        Id2 = obj.Serial,
                        Data = obj.LocationName
                    };
                    lstCmb.Add(cmb);
                }
            }
            lstComboData.Add(lstCmb);
            List<ResidentialUnit> lstUnit = new ManagementDashboardDA().GetResidentialUnitByOwnerPropertyManager(Obj.OwnerId, Obj.PropertyManagerId);

            if (lstUnit.Count > 0)
            {
                lstCmb = new List<ComboData>();
                foreach (var obj in lstUnit)
                {
                    ResidentialTenantSignIn objTenantSignIn = new ManagementDashboardDA().ResidentialTenantByUnitId(obj.Serial);
                    if (objTenantSignIn != null)
                    {
                        ComboData cmb4 = new ComboData()
                        {
                            Id2 = obj.Serial,
                            Data = obj.UnitName,
                            Id3 = objTenantSignIn.SerialId,
                            SelectedField = objTenantSignIn.FirstName + " " + objTenantSignIn.LastName
                        };
                        lstCmb.Add(cmb4);
                    }

                }
                lstComboData.Add(lstCmb);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod]
        public static string GetUnitData(EventManagement Obj)
        {
            List<List<ComboData>> lstComboData = new List<List<ComboData>>();
            List<ResidentialUnit> lstUnit = new ManagementDashboardDA().GetResidentialUnitByOwnerPropertyManagerLocation(Obj.OwnerId,Obj.PropertyManagerId,Obj.LocationId);
            List<ComboData> lstCmbUnit = new List<ComboData>();
            if (lstUnit.Count > 0)
            {
                foreach (var obj in lstUnit)
                {
                    ResidentialTenantSignIn objTenantSignIn = new ManagementDashboardDA().ResidentialTenantByUnitId(obj.Serial);
                    if (objTenantSignIn != null)
                    {
                        ComboData cmb4 = new ComboData()
                        {
                            Id2 = obj.Serial,
                            Data = obj.UnitName,
                            Id3 = objTenantSignIn.SerialId,
                            SelectedField = objTenantSignIn.FirstName + " " + objTenantSignIn.LastName
                        };
                        lstCmbUnit.Add(cmb4);
                    }
                }
            }
            lstComboData.Add(lstCmbUnit);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lstComboData);
            return json;
        }
        [WebMethod]
        public static string GetTableDataByUnit(EventManagement Obj)
        {
            ManagementDashboardModel obj = new ManagementDashboardDA().GettableData(Obj.OwnerId, Obj.PropertyManagerId, Obj.LocationId,Obj.UnitId);
            
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string Save(EventManagement Obj)
        {
            bool res = false;
            if (Obj != null)
            {
                Obj.CreateDate = DateTime.Now;
                if (new ManagementDashboardDA().Insert(Obj))
                {
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
        [WebMethod(EnableSession = true)]
        public static string Update(EventManagement Obj)
        {
            bool res = false;
            if (Obj != null)
            {
                Obj.CreateDate = DateTime.Now;
                if (new ManagementDashboardDA().Update(Obj))
                {
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
        [WebMethod(EnableSession = true)]
        public static string Delete(EventManagement Obj)
        {
            bool res = false;
            if (Obj != null)
            {
                Obj.CreateDate = DateTime.Now;
                if (new ManagementDashboardDA().Delete(Obj))
                {
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
        [WebMethod(EnableSession = true)]
        public static string GetEventData(EventManagement Obj)
        {
            List<Get_EventManagement_Data_Result> evnList = new List<Get_EventManagement_Data_Result>();
            if (Obj != null)
            {
                string OwnerId = "-1";
                if (HttpContext.Current.Session["OwnerId"] != null)
                {
                    Obj.OwnerId = HttpContext.Current.Session["OwnerId"].ToString();
                }

                Obj.CreateDate = DateTime.Now;
                evnList = new ManagementDashboardDA().GetEventData(Obj);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(evnList);
            return json;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx", false);
        }

    }
}