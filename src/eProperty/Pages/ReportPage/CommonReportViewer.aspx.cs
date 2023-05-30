using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Hosting;
using PropertyService.DA.Report;
using Microsoft.Reporting.WebForms;
using PropertyService.ViewModel;
using PropertyService.BO;
using System.Reflection;
using PropertyService.DA.Account;
using PropertyService.DA;
using System.Linq;

namespace eProperty.Pages.Report
{
    public partial class CommonReportViewer : System.Web.UI.Page
    {
        List<ReportParameter> parameters = new List<ReportParameter>();
        ReportDA _reportDAL = new ReportDA();
        protected void Page_Load(object sender, EventArgs e)
        {


            lblMsg.Text = "";
            
            ReportParamModel obj = (ReportParamModel)Session["reportObj"];
            //LoadReport(obj);
            if (obj != null)
            {
                LoadReport(obj);
            }
            else
            {
                lblMsg.Text = "No Data Found!!!";
            }
        }
        private void LoadReport(ReportParamModel obj)
        {
            ReportParameter[] param = new ReportParameter[20];
            DataTable datatable = new DataTable();
            DataTable datatable1 = new DataTable();
            System.Data.DataSet ds = new System.Data.DataSet();
            parameters = new List<ReportParameter>();
            string sOwnerId = "";

            switch (obj.ReportName)
            {

                case "Dealer Sales":
                    datatable = _reportDAL.GetAllSalesPartnerDealer();
                    
                    if (datatable.Rows.Count > 0)
                    {
                        param = new ReportParameter[1];
                        param[0] = new ReportParameter("ReportName", obj.ReportName);
                        ShowReport(datatable, obj, param);

                    }
                    else
                    {
                        lblMsg.Text = "No Data Found!!!";
                    }
                    break;
                case "Balance Sheet":
                   
                    if (Session["UserObject"] != null)
                    {
                        sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
                    }

                    ds = _reportDAL.GetBalanceSheet(obj.createdate, sOwnerId);

                    if (ds.Tables.Count > 0)
                    {
                        param = new ReportParameter[3];
                        param[0] = new ReportParameter("ReportName", obj.ReportName);
                        param[1] = new ReportParameter("CompanyName", obj.CompanyName);
                        param[2] = new ReportParameter("createdate", Convert.ToDateTime(obj.createdate).ToString("MMMM dd, yyyy"));
                        ShowReportDS(ds, obj, param);

                    }
                    else
                    {
                        lblMsg.Text = "No Data Found!!!";
                    }
                    break;
                case "Income Statement":
                  
                    if (Session["UserObject"] != null)
                    {
                        sOwnerId = ((UserProfile)Session["UserObject"]).OwnerId != null ? Convert.ToString(((UserProfile)Session["UserObject"]).OwnerId) : "";
                    }
                  
                    datatable1 = FillReportData(sOwnerId, obj.createdate, obj.EndDate);

                    if (datatable1 != null && datatable1.Rows.Count > 0)
                    {
                        ds.Tables.Add(datatable1);
                    }

                    if (ds != null && ds.Tables.Count > 0)
                    {                        
                        param = new ReportParameter[5];
                        param[0] = new ReportParameter("ReportName", obj.ReportName);
                        param[1] = new ReportParameter("Company", obj.CompanyName);
                        param[2] = new ReportParameter("EndDate", Convert.ToDateTime(obj.EndDate).ToString("MMMM dd, yyyy"));
                        param[3] = new ReportParameter("Currency", "$");
                        param[4] = new ReportParameter("PrintBy", obj.PrintBy);
                        ShowReportDS(ds, obj, param);

                    }
                    else
                    {
                        lblMsg.Text = "No Data Found!!!";
                    }
                    break;
            }

        }
        private void ShowReport(DataTable dtTable, ReportParamModel obj, ReportParameter[] param)
        {
            try
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                if (obj.ReportName.Equals("Dealer Sales"))
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Pages/Report/DealerSales.rdlc");
                }
                else if (obj.ReportName.Equals("Balance Sheet"))
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Pages/Report/BalanceSheet.rdlc");
                }
              

                dtTable.TableName = "MySampleTable";
              
                for (int i = 0; i < param.Length; i++)
                {
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter(param[i].Name, param[i].Values[0]));
                }
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource datasource = new ReportDataSource("DataSet1", dtTable);
                ReportViewer1.LocalReport.DataSources.Add(datasource);

            }
            catch (Exception ex)
            {
                Utility.DisplayMsg(ex.Message, this);
            }


        }
        private void ShowReportDS(System.Data.DataSet dsReport, ReportParamModel obj, ReportParameter[] param)
        {
            try
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                if (obj.ReportName.Equals("Dealer Sales"))
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Pages/Report/DealerSales.rdlc");
                }
                else if (obj.ReportName.Equals("Balance Sheet"))
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Pages/Report/BalanceSheet.rdlc");
                }
                else if (obj.ReportName.Equals("Income Statement"))
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Pages/Report/IncomeStatement.rdlc");
                }
                

                for (int i = 0; i < param.Length; i++)
                {
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter(param[i].Name, param[i].Values[0]));
                }
                ReportViewer1.LocalReport.DataSources.Clear();

                if (obj.ReportName.Equals("Income Statement"))
                {
                    for (int i = 0; i < dsReport.Tables.Count; i++)
                    {
                        ReportDataSource datasource = new ReportDataSource("DataSet" + (i + 1), dsReport.Tables[i]);
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }
                }
                else
                {
                    for (int i = 0; i < dsReport.Tables.Count; i++)
                    {
                        ReportDataSource datasource = new ReportDataSource("DataSet" + (i + 1), dsReport.Tables[i]);
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                Utility.DisplayMsg(ex.Message, this);
            }


        }

        public DataTable FillReportData(string sOwnerId, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
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

                dt = ToDataTable(data);

            }
            catch (Exception ex)
            {
            }

            return dt;
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

    }
}