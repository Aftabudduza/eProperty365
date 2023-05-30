using PropertyService.Admin.DA;
using PropertyService.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.UserControls
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    spanAccount.InnerHtml = "<a style='color:#fff;' href='" + Utility.WebUrl + "/Pages/Admin/AddUser.aspx?IsTopMenu=1&AddUserId= " + Session["UserId"].ToString() + "'>Account Profile </a>";
                    spanReset.InnerHtml = "<a class='btn btn-default btn-flat' href='" + Utility.WebUrl + "/Pages/Admin/ResetPassword.aspx?AddUserId= " + Session["UserId"].ToString() + "'>Reset Password </a>";
                    spanSignOut.InnerHtml = "<a class='btn btn-default btn-flat' href='" + Utility.WebUrl + "/Pages/Logout.aspx'>Sign out</a>";

                    UserProfile obj = new AdminUserProfileDA().GetUserByUserID(Convert.ToInt32(Session["UserId"].ToString()));
                    if (obj != null)
                    {
                        if (!string.IsNullOrEmpty(obj.Location))
                        {
                            imgTopLogo.ImageUrl = Utility.WebUrl + "/Uploads/Files/User/" + obj.Location;
                            imgTopIcon.ImageUrl = Utility.WebUrl + "/Uploads/Files/User/" + obj.Location;
                        }
                    }
                }
            }
        }
    }
}