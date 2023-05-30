using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Admin.ResidentialTenent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Resident
{
    public partial class ThankYouOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int IsOwner = 0;
            try
            {
                IsOwner = Convert.ToInt32(Request.QueryString["o"].ToString());
            }
            catch (Exception ex)
            {
                IsOwner = 0;
            }
            if (IsOwner > 0)
            {
                PropertyService.BO.TenantPaymentHistory paymentHistory = new PropertyService.BO.TenantPaymentHistory();

                if (Session["paymentHistory"] != null)
                    paymentHistory = (PropertyService.BO.TenantPaymentHistory)Session["paymentHistory"];

                int paymentHistoryId = 0;

                paymentHistoryId = new ResidentialAddResponceTemplateDa().Insert(paymentHistory);

                if (paymentHistoryId > 0)
                {
                    Session["paymentHistory"] = null;
                    Utility.DisplayMsg("ACH payment successful !", this);
                }
                else
                {
                    Utility.DisplayMsg("ACH payment successful !", this);
                }

                spanBack.InnerHtml = "<a href='" + Utility.WebUrl + "/Pages/DashboardAdmin.aspx'>Back To Home</a>";
            }

            int IsAdmin = 0;
            try
            {
                IsAdmin = Convert.ToInt32(Request.QueryString["a"].ToString());
            }
            catch (Exception ex)
            {
                IsAdmin = 0;
            }

            if (IsAdmin > 0)
            {
                PaymentHistory paymentHistory = new PaymentHistory();

                if (Session["paymentHistory"] != null)
                    paymentHistory = (PaymentHistory)Session["paymentHistory"];

                int paymentHistoryId = 0;

                paymentHistoryId = new AdminPaymentHistoryDA().Insert(paymentHistory);

                if (paymentHistoryId > 0)
                {
                    Session["paymentHistory"] = null;
                    Utility.DisplayMsg("Commission payment successful !", this);
                }
                else
                {
                    Utility.DisplayMsg("Commission payment successful !", this);
                }                

                spanBack.InnerHtml = "<a href='" + Utility.WebUrl + "/Pages/Account/SalesPartnerDealerDashboard.aspx'>Back To Home</a>";
            }

        }
    }
}