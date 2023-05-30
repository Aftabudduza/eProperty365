using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PropertyService.Admin.DA;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Account;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;

namespace eProperty
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  PropertyEntities PE1 = new PropertyEntities();
            string Server = @"AFTAB-PC";
            string DB = "EPropertyDB_Init";
            string User = "sa";
            string Pass = "1234";
            string connectString = Convert.ToString(ConfigurationManager.ConnectionStrings["SQLDB"]);

            Utility.Server = Server;
            Utility.Database = DB;
            Utility.Username = User;
            Utility.Password = Pass;
            Utility.ModelName = "Model1";

         //   PropertyEntities db = PropertyEntities.Create(connectString);

            List<UserProfile> Masters = new AdminUserProfileDA().GetAllUsers();

    }
    }
}