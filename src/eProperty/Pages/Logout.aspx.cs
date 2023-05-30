using PropertyService.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserObject"] != null))
            {
                UserProfile obj = new UserProfile();
                obj = (UserProfile)Session["UserObject"];
                HttpContext.Current.Cache.Remove(obj.Username);
            }

            System.Web.Security.FormsAuthentication.SignOut();

            Session.Abandon();
            Session.Clear();

            Response.Redirect("Login.aspx");
        }
    }
}