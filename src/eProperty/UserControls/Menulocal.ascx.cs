using PropertyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.UserControls
{
    public partial class Menulocal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] != null)
                {
                   
                    if (Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3")
                    {
                        if (Session["HasCompletedFullProfile"] != null)
                        {
                            if (Convert.ToBoolean(Session["HasCompletedFullProfile"]) == true)
                            {
                                lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                                   "/Pages/DashboardAdmin.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                            }
                            else
                            {
                                lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                                   "/Pages/DashboardOwner.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                            }
                        }
                    }

                    else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16) EnumUserType.Resident)
                    {
                        lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                           "/Pages/Resident/ResidentTenantDashboard.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                    }
                    else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16) EnumUserType.Condo)
                    {
                        lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                           "/Pages/DashboardCondo.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                    }
                    else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16) EnumUserType.Commercial)
                    {
                        lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                           "/Pages/DashboardCommercial.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                    }
                    else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16)EnumUserType.SalesPartner)
                    {
                        lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                          "/Pages/Account/SalesPartnerDealerDashboard.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                    }
                    else if (Convert.ToInt16(Session["UserType"].ToString()) == (Int16)EnumUserType.Dealer)
                    {
                        lihome.InnerHtml = "<a href='" + Utility.WebUrl +
                                          "/Pages/Account/SalesPartnerDealerDashboard.aspx'><i class='fa fa-circle-o'></i>Dashboard </a>";
                    }

                    liHeader.InnerHtml = "Users =: " + Enum.GetName(typeof(EnumUserType), Convert.ToInt16(Session["UserType"].ToString()));
                    spanDash.InnerHtml =  Enum.GetName(typeof(EnumUserType), Convert.ToInt16(Session["UserType"].ToString())) + " Dashboard";
                }
                else
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/Login.aspx", false);
                }

            }
        }
    }
}