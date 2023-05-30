using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.Admin.DA;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace eProperty.Pages
{
    public partial class DashboardOwnerOld : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                   
                   string sSQL = " select ISNULL(u.HasOwnerProfile,0) HasOwnerProfile, ISNULL(u.HasPropertyLocation,0) HasPropertyLocation, ISNULL(u.HasPropertyUnit,0) HasPropertyUnit, ISNULL(u.HasFinishedTenantImport,0) HasFinishedTenantImport, ISNULL(u.HasAccountSystem,0) HasAccountSystem, ISNULL(u.HasDocuments,0) HasDocuments  from UserProfile u where u.Email = '" + ((UserProfile)Session["UserObject"]).Email.Trim() + "'";

                    try
                    {
                        string constr = ConfigurationManager.ConnectionStrings["SQLDBOwner"].ConnectionString;
                        DataSet dsFaq = new DataSet();
                        DataTable dt = new DataTable();

                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand(sSQL))
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                using (DataSet ds = new DataSet())
                                {
                                    sda.Fill(ds);
                                    dsFaq = ds;
                                }
                            }
                        }

                        if (dsFaq != null && dsFaq.Tables[0].Rows.Count > 0)
                        {
                            dt = dsFaq.Tables[0]; 
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!Convert.IsDBNull(dr["HasOwnerProfile"]) && Convert.ToBoolean(dr["HasOwnerProfile"].ToString()) == true)
                                {
                                    litop2.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Admin/AddOwner.aspx'>1) Setup Owners Profile </a>";
                                }
                                else
                                {
                                    litop2.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Admin/AddOwner.aspx'>1) Setup Owners Profile </a>";
                                }

                                if (!Convert.IsDBNull(dr["HasPropertyLocation"]) && Convert.ToBoolean(dr["HasPropertyLocation"].ToString()) == true)
                                {
                                    litop3.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Admin/AddLocation.aspx'>2) Setup Property Location Profile </a>";
                                }
                                else
                                {
                                    litop3.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Admin/AddLocation.aspx'>2) Setup Property Location Profile </a>";
                                }

                                if (!Convert.IsDBNull(dr["HasPropertyUnit"]) && Convert.ToBoolean(dr["HasPropertyUnit"].ToString()) == true)
                                {
                                    litop4.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx'>3) Setup Property Unit Profile </a>";
                                }
                                else
                                {
                                    litop4.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx'>3) Setup Property Unit Profile </a>";
                                }

                                if (!Convert.IsDBNull(dr["HasDocuments"]) && Convert.ToBoolean(dr["HasDocuments"].ToString()) == true)
                                {
                                    litop5.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx'>4) Setup Document Management System </a>";
                                }
                                else
                                {
                                    litop5.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Admin/AddResidentialUnit.aspx'>4) Setup Document Management System </a>";
                                }

                                if (!Convert.IsDBNull(dr["HasAccountSystem"]) && Convert.ToBoolean(dr["HasAccountSystem"].ToString()) == true)
                                {
                                    litop6.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Account/AddChartofAccount.aspx'>5) Setup Accounting System Profile </a>";
                                }
                                else
                                {
                                    litop6.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Account/AddChartofAccount.aspx'>5) Setup Accounting System Profile</a>";
                                }

                                if (!Convert.IsDBNull(dr["HasFinishedTenantImport"]) && Convert.ToBoolean(dr["HasFinishedTenantImport"].ToString()) == true)
                                {
                                    litop7.InnerHtml = "<a style='color:green;' href='" + Utility.WebUrl + "/Pages/Resident/ImportTenantProfile.aspx'>6) Setup Existing Tenants Profile Import </a>";
                                }
                                else
                                {
                                    litop7.InnerHtml = "<a style='color:red;' href='" + Utility.WebUrl + "/Pages/Resident/ImportTenantProfile.aspx'>6) Setup Existing Tenants Profile Import</a>";
                                }

                               

                            }
                        }
                    }
                    catch(Exception ex)
                    {

                    }

                    int newUser = 0;
                    try
                    {
                        newUser = Convert.ToInt32(Request.QueryString["N"].ToString());
                    }
                    catch(Exception ex)
                    {
                        newUser = 0;
                    }
                    if (newUser > 0)
                    {
                        Utility.DisplayMsg("Registration successful !", this);
                    }

                    if (Session["HasCompletedFullProfile"] != null)
                    {
                        if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == true)
                        {
                            divLinkMenu.Visible = false;
                        }
                        else
                        {
                            divLinkMenu.Visible = true;
                        }
                    }
                }
            }
        }        
    }
}