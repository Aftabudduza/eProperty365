using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using PropertyService.BO;
using PropertyService.DA.Account;
using PropertyService.ViewModel;

namespace eProperty.Pages.Account
{
    public partial class MakeBillPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserObject"] != null)
                {
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
        }
        [WebMethod(EnableSession = true)]
        public static string PrintCheck(MakeBillPaymentModel Obj)
        {
            var res = false;
            if (Obj != null)
            {
                if (new MakeBillPaymentDA().Insert(Obj.lstSaveBillPayments))
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(res);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public static string GetRecordABill(MakeBillPaymentModel Obj)
        {
            List < usp_GetRecordABillByDate_Result > lst = new List<usp_GetRecordABillByDate_Result>();
            if (!string.IsNullOrEmpty(Obj.fromDate))
            {
                lst = new MakeBillPaymentDA().GetRecord(Obj.fromDate, Obj.toDate);

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string GetRecordABillDetails(RecordABillModel Obj)
        {
            List<RecordABillDetails> lst = new List<RecordABillDetails>();
            if (Obj.Details != null)
            {
                List<RecordABillDetails> details = new List<RecordABillDetails>();
                foreach (var dtl in Obj.Details)
                {
                    RecordBillDetails obj2 = new RecordBillDetails()
                    {
                        Id = dtl.Id,
                        Amount = dtl.Amount,
                        RecordABillId = dtl.RecordABillId,
                        CreditAccountName = dtl.CreditAccountName,
                        DebitAccountName = dtl.DebitAccountName,
                        Type = dtl.Type,
                        BillId = dtl.BillId,
                        CreditLedgerCode = dtl.CreditLedgerCode,
                        DebitLedgerCode = dtl.DebitLedgerCode,
                        Description = dtl.Description,
                        DueDate = dtl.DueDate
                    };
                    details.Add(obj2);
                }
                lst = new MakeBillPaymentDA().GetRecordDetails(details);

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);
            return json;
        }
    }
}