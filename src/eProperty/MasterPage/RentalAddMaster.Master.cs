using PropertyService.DA.Admin.ResidentialTenent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eProperty.MasterPage
{
    public partial class RentalAddMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                string residentialId, tenantId;
                try
                {
                    residentialId = Request.QueryString["ResidentialUnitSerial"];
                }
                catch (Exception ex)
                {
                    residentialId = "";
                }
                try
                {
                    tenantId = Request.QueryString["TenentId"];
                }
                catch (Exception ex)
                {
                    tenantId = "";
                }
                if(Session["TenentId"] != null)
                {
                    tenantId = Session["TenentId"].ToString();
                }
                if (Session["ResidentialUnitSerial"] != null)
                {
                    residentialId = Session["ResidentialUnitSerial"].ToString();
                }

                if (residentialId != "" && tenantId != "" && residentialId != null && tenantId != null)
                {
                    var stepcount = new ResidentialAddResponceTemplateDa().GetStepCount((HttpContext.Current.Session["TenentId"]).ToString(), (HttpContext.Current.Session["ResidentialUnitSerial"]).ToString());
                    if (stepcount != null)
                    {
                        if(stepcount.Step == 1)
                        {
                            CheckBox1.Checked = true;
                        }
                        else if(stepcount.Step == 2)
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
                }
                
            }

            
        }
    }
}