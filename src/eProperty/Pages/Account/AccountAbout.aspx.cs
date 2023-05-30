using PropertyService.BO;
using PropertyService.DA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Account
{
    public partial class AccountAbout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["OwnerId"] != null)
                {
                    OwnerProfile objOwner = new OwnerProfileDA().GetBySerial(Session["OwnerId"].ToString());
                    if (objOwner != null)
                    {
                        string sName = objOwner.FirstName + " " + objOwner.LastName;
                        lblOwner.Text = sName;

                        string sAddress = objOwner.Address + ", " + objOwner.City + ", " + objOwner.State + ", " + objOwner.Zip;
                        lblLocation.Text = sAddress;
                    }

                    var conn = ConfigurationManager.ConnectionStrings["SQLDBOwner"].ConnectionString;
                    var csb = new SqlConnectionStringBuilder(conn);
                    lblDatabaseLocation.Text = csb.DataSource;
                    lblDatabasePassword.Text = csb.Password;
                    lblDatabaseName.Text = csb.InitialCatalog;
                }
            }
        }
    }
}