using System;

namespace PropertyService.ViewModel
{
  
    public class AccountModel
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountDescription { get; set; }
        public bool IsActive { get; set; }
        public bool EditAble { get; set; }
        public string CreateDate { get; set; }

    }

    public class IncomeStatementModel
    {
      
        public string Serial { get; set; }
        public string RefId { get; set; }
        public string InvoiceNo { get; set; }       
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Expense { get; set; }
        public Nullable<decimal> Income { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Remarks { get; set; }

        public string Transide { get; set; }
        public string TranType { get; set; }
        public Nullable<decimal> Opening { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }

    }
}
