using Microsoft.Reporting.WebForms;
using PropertyService.BO;
using PropertyService.DA;
using PropertyService.DA.Account;
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
using System.Web;
using System.Web.UI.WebControls;

namespace eProperty.Pages.Account
{
    public partial class IncomeStatementOld : System.Web.UI.Page
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
        protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocation.PageIndex = e.NewPageIndex;

            string sFrom = Convert.ToDateTime(txtFromDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sTo = Convert.ToDateTime(txtToDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sOwnerId = "";
            if (Session["UserObject"] != null)
            {
                sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
            }

            FillTransactionList(sOwnerId, sFrom, sTo);

        }
        protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sFrom = Convert.ToDateTime(txtFromDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sTo = Convert.ToDateTime(txtToDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sOwnerId = "";
            if (Session["UserObject"] != null)
            {
                sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
            }

            FillTransactionList(sOwnerId, sFrom, sTo);
        }
       
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string sFrom = Convert.ToDateTime(txtFromDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sTo = Convert.ToDateTime(txtToDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sOwnerId = "";
            if (Session["UserObject"] != null)
            {
                sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
            }

            FillTransactionList(sOwnerId, sFrom, sTo);
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string sFrom = Convert.ToDateTime(txtFromDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sTo = Convert.ToDateTime(txtToDate.Text.ToString().Trim()).ToString("MM/dd/yyyy");
            string sOwnerId = "";
            if (Session["UserObject"] != null)
            {
                sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
            }
         //   ExportTransactionList(sOwnerId, sFrom, sTo);
            FillReportData(sOwnerId, sFrom, sTo);
        }

        #endregion
        #region Method Support
        private void ShowReport(List<IncomeStatementModel> data)
        {
            if (data != null && data.Count > 0)
            {
                try
                {
                    DataTable dt = ToDataTable(data);
                  
                    if (dt != null && dt.Rows.Count > 0)
                    {
                       // dsIncome.Tables.Add(dt);
                        Session["dsIncome"] = dt;
                    }

                   
                    UserProfile objUser = new UserProfile();
                    objUser = (UserProfile)Session["UserObject"];
                    string sOwnerId = objUser.OwnerId;
                    OwnerProfile objOwner = new OwnerProfileDA().GetBySerial(sOwnerId);

                    //ReportParameter rpCompanyName = new ReportParameter("Company", objOwner != null ? objOwner.CompanyName : "EProperty");
                    //ReportParameter rpReportName = new ReportParameter("ReportName", "Income Statement");
                    //ReportParameter rpPrintBy = new ReportParameter("PrintBy", objUser != null ? objUser.Username : "");
                    //ReportParameter rpCurrency = new ReportParameter("Currency", "$");                 
                    DateTime toDate = txtToDate.Text.ToString().Trim() == "" ? DateTime.Today : Convert.ToDateTime(txtToDate.Text.ToString().Trim());
                    //ReportParameter rpEndDate = new ReportParameter("EndDate", toDate.ToString("MMMM dd, yyyy"));

                    //rptViewerSKUStock.ProcessingMode = ProcessingMode.Local;
                    //rptViewerSKUStock.LocalReport.ReportPath = Server.MapPath("~/Pages/Account/rptIncome.rdlc");

                    //rptViewerSKUStock.LocalReport.SetParameters(new ReportParameter[] { rpReportName });
                    //rptViewerSKUStock.LocalReport.SetParameters(new ReportParameter[] { rpPrintBy });
                    //rptViewerSKUStock.LocalReport.SetParameters(new ReportParameter[] { rpCurrency });
                    //rptViewerSKUStock.LocalReport.SetParameters(new ReportParameter[] { rpEndDate });
                    //rptViewerSKUStock.LocalReport.SetParameters(new ReportParameter[] { rpCompanyName });

                    //ReportDataSource datasource = new ReportDataSource("dtFinancialLedger", data.ToList());
                    //rptViewerSKUStock.LocalReport.DataSources.Clear();
                    //rptViewerSKUStock.LocalReport.DataSources.Add(datasource);

                    ReportParamModel objReportModel = new ReportParamModel();
                    objReportModel.CompanyName = objOwner != null ? objOwner.CompanyName : "EProperty";
                    objReportModel.ReportName = "Income Statement";
                    objReportModel.EndDate = toDate.ToString("MMMM dd, yyyy");
                    objReportModel.PrintBy = objUser != null ? objUser.Username : "";

                    HttpContext.Current.Session.Remove("reportObj");
                  //  string content = "/Pages/ReportPage/CommonReportViewer.aspx";
                    HttpContext.Current.Session["reportObj"] = objReportModel;

                    Response.Redirect(Utility.WebUrl + "/Pages/ReportPage/CommonReportViewer.aspx", false);

                }
                catch (Exception ex)
                {
                    Utility.DisplayMsg(ex.Message, this);
                }
            }
            else
            {
                //ReportDataSource datasource = new ReportDataSource("", data.ToList());
                //rptViewerSKUStock.LocalReport.DataSources.Clear();
                Utility.DisplayMsg("No data found!", this);
            }


        }

        public void FillTransactionList(string sOwnerId, string FromDate, string ToDate)
        {
            try
            {
                List<FinancialTransaction> objFTs = null;
                if (FromDate != "" && ToDate != "")
                {
                    objFTs = new FinancialTransactionDA().GetIncomeBySearch(sOwnerId, FromDate, ToDate);
                }

                decimal nOpeningBalance = 0;
                decimal nBalance = 0;

                
                var data = new List<dynamic>();
                int i = 1;

                if (objFTs != null && objFTs.Count > 0)
                {
                    objFTs = objFTs.OrderBy(x => x.CreateDate).ToList();

                    foreach (FinancialTransaction item in objFTs)
                    {
                        try
                        {
                            string sTranType = "";
                            string sAccountName = "";
                            decimal Debit = 0;
                            decimal Credit = 0;
                            decimal Closing = 0;
                            if (i == 1)
                            {
                                nBalance = nOpeningBalance;

                                if (item.EntryType == "Credit")
                                {
                                    nBalance = nBalance + Convert.ToDecimal(item.Amount);
                                    Debit = 0;
                                    Credit = Convert.ToDecimal(item.Amount);
                                }
                                else if (item.EntryType == "Debit")
                                {
                                    nBalance = nBalance - Convert.ToDecimal(item.Amount);
                                    Credit = 0;
                                    Debit = Convert.ToDecimal(item.Amount);
                                }
                            }
                            else
                            {
                                if (item.EntryType == "Credit")
                                {
                                    nBalance = nBalance + Convert.ToDecimal(item.Amount);
                                    Debit = 0;
                                    Credit = Convert.ToDecimal(item.Amount);
                                }
                                else if (item.EntryType == "Debit")
                                {
                                    nBalance = nBalance - Convert.ToDecimal(item.Amount);
                                    Credit = 0;
                                    Debit = Convert.ToDecimal(item.Amount);
                                }
                            }

                            Closing = nBalance;
                            AccountChart objAccount = new AddChartofAccountDA().GetAccountTypeByLedgerCode(item.LedgerCode);
                            AccountType objAccountType = new AddChartofAccountDA().GetAccountTypeByCode(item.AccountType);
                            sAccountName = objAccount != null ? objAccount.accountName : "";
                            sTranType = objAccountType != null ? objAccountType.description : "";

                            data.Add(new { item.Serial, item.RefId, item.InvoiceNo, item.LedgerCode, LedgerName = sAccountName, Transide = item.EntryType, TranType = sTranType, Amount = Convert.ToDecimal(item.Amount), item.CreateDate, item.Remarks, Opening = nOpeningBalance, Balance = nBalance, Debit, Credit });

                            i++;
                        }
                        catch (Exception ex)
                        { }
                    }

                }

                spanIncome.InnerText = nBalance > 0 ?  " $" +  nBalance.ToString("#.00") : "($" + Convert.ToDecimal(nBalance * -1).ToString("#.00") + ")";

                gvLocation.DataSource = data;
                gvLocation.DataBind();

             
            }
            catch (Exception ex)
            {
            }

        }

        public void ExportTransactionList(string sOwnerId, string FromDate, string ToDate)
        {
            try
            {
                List<FinancialTransaction> objFTs = null;
                if (FromDate != "" && ToDate != "")
                {
                    objFTs = new FinancialTransactionDA().GetIncomeBySearch(sOwnerId, FromDate, ToDate);
                }

                decimal nOpeningBalance = 0;
                decimal nBalance = 0;


                var data = new List<IncomeStatementModel>();
                int i = 1;

                if (objFTs != null && objFTs.Count > 0)
                {
                    objFTs = objFTs.OrderBy(x => x.CreateDate).ToList();

                    foreach (FinancialTransaction item in objFTs)
                    {
                        try
                        {
                            string sTranType = "";
                            string sAccountName = "";
                            decimal Debit = 0;
                            decimal Credit = 0;
                            decimal Closing = 0;
                            if (i == 1)
                            {
                                nBalance = nOpeningBalance;

                                if (item.EntryType == "Credit")
                                {
                                    nBalance = nBalance + Convert.ToDecimal(item.Amount);
                                    Debit = 0;
                                    Credit = Convert.ToDecimal(item.Amount);
                                }
                                else if (item.EntryType == "Debit")
                                {
                                    nBalance = nBalance - Convert.ToDecimal(item.Amount);
                                    Credit = 0;
                                    Debit = Convert.ToDecimal(item.Amount);
                                }
                            }
                            else
                            {
                                if (item.EntryType == "Credit")
                                {
                                    nBalance = nBalance + Convert.ToDecimal(item.Amount);
                                    Debit = 0;
                                    Credit = Convert.ToDecimal(item.Amount);
                                }
                                else if (item.EntryType == "Debit")
                                {
                                    nBalance = nBalance - Convert.ToDecimal(item.Amount);
                                    Credit = 0;
                                    Debit = Convert.ToDecimal(item.Amount);
                                }
                            }

                            Closing = nBalance;
                            AccountChart objAccount = new AddChartofAccountDA().GetAccountTypeByLedgerCode(item.LedgerCode);
                            AccountType objAccountType = new AddChartofAccountDA().GetAccountTypeByCode(item.AccountType);
                            sAccountName = objAccount != null ? objAccount.accountName : "";
                            sTranType = objAccountType != null ? objAccountType.description : "";


                            data.Add(new IncomeStatementModel()
                            {
                                Serial = item.Serial,
                                RefId = item.RefId,
                                InvoiceNo = item.InvoiceNo,
                                LedgerCode = item.LedgerCode,
                                LedgerName = sAccountName,
                                Amount = Convert.ToDecimal(item.Amount),
                                CreateDate = item.CreateDate,
                                Remarks = item.Remarks,
                                Expense = Debit,
                                Income = Credit
                            });

                            i++;
                        }
                        catch (Exception ex)
                        { }
                    }

                }

               
                string sCSV = "";
                string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\IncomeStatement\\");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (Session["UserObject"] != null)
                {
                    //UserProfile objUser = new UserProfile();
                    //objUser = (UserProfile)Session["UserObject"];
                    //string sOwnerId = objUser.OwnerId;
                    sCSV = sOwnerId + "_" + DateTime.Now.ToString("MMddyyyyhhmm") + ".csv";
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

                List<IncomeStatementModel> listIncome = data.ToList();
                /*List to DataTable conversion*/
                DataTable dt = ToDataTable(listIncome);

                if (dt != null && dt.Rows.Count > 0)
                {
                    CreateCSVFile(nBalance, dt, nFile);
                    Response.Redirect(Utility.WebUrl + "/Uploads/Files/IncomeStatement/" + fileName, false);
                }
            }
            catch (Exception ex)
            {
            }

        }

        //public void AddCSVFile(string FromDate, string ToDate)
        //{
        //    string sSQL = string.Empty;
        //    string sWhere = string.Empty;           
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
        //        {
        //            sWhere = " and convert(varchar, F.CreateDate, 101) between '" + FromDate + "' and  '" + ToDate + "'";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    sSQL = " select F.Serial, F.RefId, isnull(F.InvoiceNo,'') InvoiceNo, F.LedgerCode, AC.accountName LedgerName, F.Amount, isnull(F.Debit,0) Expense, isnull(F.Credit,0) Income, F.CreateDate, F.Remarks from  FinancialTransaction F,AccountChart AC, AccountType AT where F.LedgerCode = AC.accountCode and F.Amount > 0 and F.AccountType = AT.type  and (F.AccountType = 'Inc' or F.AccountType = 'Exp' or F.AccountType = 'COG') " + sWhere;
            
        //    string sCSV = "";
        //    string filePath = Path.Combine(Request.PhysicalApplicationPath, "Uploads\\Files\\IncomeStatement\\");

        //    if (!Directory.Exists(filePath))
        //    {
        //        Directory.CreateDirectory(filePath);
        //    }

        //    if (Session["UserObject"] != null)
        //    {
        //        UserProfile objUser = new UserProfile();
        //        objUser = (UserProfile)Session["UserObject"];
        //        string sOwnerId = objUser.OwnerId;
        //        sCSV = sOwnerId + "_" + DateTime.Now.ToString("MMddyyyyhhmm") + ".csv";
        //    }

        //    string fileName = sCSV;
        //    string nFile = Path.Combine(filePath, fileName);

        //    try
        //    {
        //        if (System.IO.File.Exists(nFile))
        //        {
        //            System.IO.File.Delete(nFile);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    string constr = ConfigurationManager.ConnectionStrings["SQLDBOwner"].ConnectionString;
        //    DataSet dsFaq = new DataSet();
        //    DataTable dt = new DataTable();

        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sSQL))
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            using (DataSet ds = new DataSet())
        //            {
        //                sda.Fill(ds);
        //                dsFaq = ds;
        //            }
        //        }
        //    }

        //    if (dsFaq != null && dsFaq.Tables[0].Rows.Count > 0)
        //    {
        //        dt = dsFaq.Tables[0];
        //        CreateCSVFile(0, dt, nFile);
        //        Response.Redirect(Utility.WebUrl + "/Uploads/Files/IncomeStatement/" + fileName, false);
        //    }
        //}

        public void CreateCSVFile(decimal nBalance, DataTable dt, string strFilePath)
        {

            UserProfile objUser = new UserProfile();
            objUser = (UserProfile)Session["UserObject"];
            string sOwnerId = objUser.OwnerId;
            OwnerProfile objOwner = new OwnerProfileDA().GetBySerial(sOwnerId);        
            DateTime toDate = txtToDate.Text.ToString().Trim() == "" ? DateTime.Today : Convert.ToDateTime(txtToDate.Text.ToString().Trim());

            StreamWriter sw = new StreamWriter(strFilePath, false);

            // First we will write the report headers.

            sw.Write(objOwner != null ? objOwner.CompanyName : "EProperty");
            sw.Write(sw.NewLine);
            sw.Write("Income Statement");
            sw.Write(sw.NewLine);
            sw.Write("For the period ending " + toDate.ToString("MMMM dd yyyy"));
            sw.Write(sw.NewLine);
            sw.Write(sw.NewLine);
            sw.Write("Net Income:  " + (nBalance > 0 ? nBalance.ToString("#.00") : "($" + Convert.ToDecimal(nBalance * -1).ToString("#.00") + ")")); 
            sw.Write(sw.NewLine);
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

        public void FillReportData(string sOwnerId, string FromDate, string ToDate)
        {
            try
            {
                List<FinancialTransaction> objFTs = null;
                if (FromDate != "" && ToDate != "")
                {
                    objFTs = new FinancialTransactionDA().GetIncomeBySearch(sOwnerId, FromDate, ToDate);
                }

                decimal nOpeningBalance = 0;
                decimal nBalance = 0;


                //var data = new List<dynamic>();
                var data = new List<IncomeStatementModel>();

                int i = 1;

                if (objFTs != null && objFTs.Count > 0)
                {
                    objFTs = objFTs.OrderBy(x => x.CreateDate).ToList();

                    foreach (FinancialTransaction item in objFTs)
                    {
                        try
                        {
                            string sTranType = "";
                            string sAccountName = "";
                            decimal Debit = 0;
                            decimal Credit = 0;
                            decimal Closing = 0;
                            if (i == 1)
                            {
                                nBalance = nOpeningBalance;

                                if (item.EntryType == "Credit")
                                {
                                    nBalance = nBalance + Convert.ToDecimal(item.Amount);
                                    Debit = 0;
                                    Credit = Convert.ToDecimal(item.Amount);
                                }
                                else if (item.EntryType == "Debit")
                                {
                                    nBalance = nBalance - Convert.ToDecimal(item.Amount);
                                    Credit = 0;
                                    Debit = Convert.ToDecimal(item.Amount);
                                }
                            }
                            else
                            {
                                if (item.EntryType == "Credit")
                                {
                                    nBalance = nBalance + Convert.ToDecimal(item.Amount);
                                    Debit = 0;
                                    Credit = Convert.ToDecimal(item.Amount);
                                }
                                else if (item.EntryType == "Debit")
                                {
                                    nBalance = nBalance - Convert.ToDecimal(item.Amount);
                                    Credit = 0;
                                    Debit = Convert.ToDecimal(item.Amount);
                                }
                            }

                            Closing = nBalance;
                            AccountChart objAccount = new AddChartofAccountDA().GetAccountTypeByLedgerCode(item.LedgerCode);
                            AccountType objAccountType = new AddChartofAccountDA().GetAccountTypeByCode(item.AccountType);
                            sAccountName = objAccount != null ? objAccount.accountName : "";
                            sTranType = objAccountType != null ? objAccountType.description : "";

                            // sTranType = Enum.GetName(typeof(EnumStockTranType), item.StockTranTypeID);

                         //   data.Add(new { item.Serial, item.RefId, item.InvoiceNo, item.LedgerCode, LedgerName = sAccountName, Transide = item.EntryType, TranType = sTranType, Amount = Convert.ToDecimal(item.Amount), item.CreateDate, item.Remarks, Opening = nOpeningBalance, Balance = nBalance, Debit, Credit });

                            data.Add(new IncomeStatementModel()
                            {
                                Serial = item.Serial,
                                RefId = item.RefId,
                                InvoiceNo = item.InvoiceNo,
                                LedgerCode = item.LedgerCode,
                                LedgerName = sAccountName,
                                Amount = Convert.ToDecimal(item.Amount),
                                CreateDate = item.CreateDate,
                                Remarks = item.Remarks,
                                Debit = Debit,
                                Credit = Credit,
                                Transide = item.EntryType,
                                TranType = sTranType,
                                Opening = nOpeningBalance,
                                Balance = nBalance
                            });

                            i++;
                        }
                        catch (Exception ex)
                        { }
                    }

                }

                ShowReport(data);

            }
            catch (Exception ex)
            {
            }

        }
        #endregion
    }
}