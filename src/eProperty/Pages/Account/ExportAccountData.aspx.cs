using Microsoft.Reporting.WebForms;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Account;
using PropertyService.DA.Admin.ResidentialTenent;
using PropertyService.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Account
{
    public partial class ExportAccountData : System.Web.UI.Page
    {
        #region Events Support
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                   
                    DateTime now = DateTime.Now;
                    DateTime fromdate = new DateTime(now.Year, now.Month, 1);
                    txtFromDate.Text = fromdate.ToString("MM/dd/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("MM/dd/yyyy");

                }
                else
                {
                    Response.Redirect(Utility.WebUrl + "/Pages/Login.aspx", false);
                }
            }
        }
     
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string sFrom = Convert.ToDateTime(txtFromDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sTo = Convert.ToDateTime(txtToDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            ExportTransactionList(sFrom, sTo);
           
        }

        #endregion

        #region Method Support
     

      
        public void ExportTransactionList(string FromDate, string ToDate)
        {
            try
            {
                DataTable dt = new DataTable();
                string sCSV = "";
                string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\ExportData\\");
                string sOwnerId = "";
                bool isAdmin = false;
                if (Session["UserObject"] != null)
                {
                    isAdmin = ((UserProfile)Session["UserObject"]).IsAdmin != null ? Convert.ToBoolean(((UserProfile)Session["UserObject"]).IsAdmin) : false;
                    sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
                    filePath = filePath + sOwnerId + "\\";
                }

               

                sCSV = sOwnerId + "_" + DateTime.Now.ToString("MMddyyyyhhmm") + ".csv";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = sCSV;
                string nFile = Path.Combine(filePath, fileName);

                try
                {
                    if (System.IO.File.Exists(nFile))
                    {
                        System.IO.File.Delete(nFile);
                    }

                }
                catch (Exception ex)
                {
                }

                

                if(rdoType.SelectedValue != null)
                {
                    if (rdoType.Items[0].Selected == true)
                    {
                        List<usp_GetTenantApplication_Result> obj = null;
                        obj = new ResidentialAddResponceTemplateDa().GetResidentTenantsBySearch(sOwnerId, "", "", "", "");
                        if (obj != null && obj.Count > 0)
                        {
                            dt = ToDataTable(obj);
                        }
                    }
                    else if(rdoType.Items[1].Selected == true)
                    {
                        List<ContactInformation> obj = null;
                        obj = new ContactInformationDA().GetByOwner(sOwnerId);
                        if (obj != null && obj.Count > 0)
                        {
                            dt = ToDataTable(obj);
                        }
                    }
                    else if (rdoType.Items[2].Selected == true)
                    {
                        List<FinancialTransaction> objFTs = null;
                        if (FromDate != "" && ToDate != "")
                        {
                            objFTs = new FinancialTransactionDA().GetTransactionBySearch(sOwnerId, FromDate, ToDate);
                        }
                        else
                        {
                            objFTs = new FinancialTransactionDA().GetTransactionBySearch(sOwnerId, "", "");
                        }

                        if (objFTs != null && objFTs.Count > 0)
                        {
                            dt = ToDataTable(objFTs);
                        }
                        
                    }
                    else if (rdoType.Items[3].Selected == true)
                    {
                        List<MonthlyBatchRentalInvoice> obj = null;
                        
                        if (FromDate != "" && ToDate != "")
                        {
                            obj = new MonthlyBatchRentalInvoiceDA().GetMonthlyBatchRentalInvoiceByOwner(sOwnerId, FromDate, ToDate);
                        }
                        else
                        {
                            obj = new MonthlyBatchRentalInvoiceDA().GetMonthlyBatchRentalInvoiceByOwner(sOwnerId, "", "");
                        }

                        if (obj != null && obj.Count > 0)
                        {
                            dt = ToDataTable(obj);
                        }
                    }
                    else if (rdoType.Items[4].Selected == true)
                    {
                        List<usp_GetRecordABillByDate_Result> obj = null;
                       
                        if (FromDate != "" && ToDate != "")
                        {
                            obj = new MakeBillPaymentDA().GetRecordBySearch(sOwnerId, FromDate, ToDate);
                        }
                        else
                        {
                            obj = new MakeBillPaymentDA().GetRecordBySearch(sOwnerId, "", "");
                        }
                        if (obj != null && obj.Count > 0)
                        {
                            dt = ToDataTable(obj);
                        }
                    }
                }


                /*List to DataTable conversion*/
               

                if (dt != null && dt.Rows.Count > 0)
                {
                    CreateCSVFile(dt, nFile);
                    Response.Redirect(Utility.WebUrl + "/Uploads/Files/ExportData/" + sOwnerId + "/" + fileName, false);
                }
                else
                {
                    Utility.DisplayMsg("Record not found with your search. Please try again !!", this);
                }
            }
            catch (Exception ex)
            {
            }

        }

     
        public void CreateCSVFile(DataTable dt, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
       
            sw.Write(sw.NewLine);

            // we will write the table headers.


            //DataTable dt = m_dsProducts.Tables[0];


            int iColCount = dt.Columns.Count;


            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }


            sw.Write(sw.NewLine);


            // Now write all the rows.


            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        private DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }


            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }
           
 
            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

      
        #endregion
    }
}