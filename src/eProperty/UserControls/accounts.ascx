<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="accounts.ascx.cs" Inherits="eProperty.UserControls.accounts" %>
<style type="text/css">
    .third-level-menu {
        position: absolute;
        top: 0;
        right: -150px;
        width: 150px;
        list-style: none;
        padding: 0;
        margin: 0;
        display: none;
    }

    .third-level-menu > li {
        height: 30px;
        background: #999999;
    }

    .third-level-menu > li:hover {
        background: #CCCCCC;
    }

    .second-level-menu {
        position: absolute;
        top: 30px;
        left: 0;
        width: 300px;
        list-style: none;
        padding: 0;
        margin: 0;
        display: none;
        background:#8B9DC3;
        z-index:10000;
    }

    .second-level-menu > li {
        position: relative;
        height: 30px;
        /*background: #999999;*/
    }

    .second-level-menu > li:hover {
        background: #CCCCCC;
    }

    .top-level-menu {
        list-style: none;
        padding: 0;
        margin: 0;
    }

    .top-level-menu > li {
        position: relative;
        float: left;
        height: 30px;
        width: 150px;
        /*background: #999999;*/
        text-align:left;
    }

    .top-level-menu > li:hover {
        background: #CCCCCC;
    }

    .top-level-menu li:hover > ul {
        /* On hover, display the next level's menu */
        display: inline;
    }


    /* Menu Link Styles */

    .top-level-menu a /* Apply to all links inside the multi-level menu */ {
        font: 14px Arial, Helvetica, sans-serif;
        color: #FFFFFF;
        text-decoration: none;
        padding: 0 0 0 10px;
        /* Make the link cover the entire list item-container */
        display: block;
        line-height: 30px;
    }

    .top-level-menu a:hover {
        color: #000000;
    }
</style>

<div class="box-header with-border CommonHeader col-md-12" style="margin-top:0px;">
    <ul class="top-level-menu">
       
        <li>
            <a href="#">General</a>
            <ul class="second-level-menu">
                <li><a href="http://localhost:1303/Pages/Account/MonthlyBatchRentalInvoice.aspx">Create Monthly Rental Invoice</a></li>
                <li><a href="http://localhost:1303/Pages/Account/RecordInvoice.aspx">Create Invoice</a></li>             
               <%-- <li><a href="#">Invoice Payment Received</a></li>--%>
                 <li><a href="http://localhost:1303/Pages/Account/RecordABill.aspx">Record A Bill </a></li>
                <li><a href="http://localhost:1303/Pages/Account/MakeBillPayment.aspx">Make Bill Payment</a></li>             
                <li><a href="http://localhost:1303/Pages/Account/AddChartofAccount.aspx">Add/Change Chart of Account</a></li>
                <li><a href="http://localhost:1303/Pages/Account/AddJournalEntry.aspx">Add/View Journal Entry</a></li>
                <li><a href="http://localhost:1303/Pages/Account/ExportAccountData.aspx">Export Data</a></li>
                <li><a href="http://localhost:1303/Pages/Account/AccountAbout.aspx">About</a></li>             
                <li><a href="http://localhost:1303/Pages/DashboardAdmin.aspx">Exit</a></li>
            </ul>
        </li>

         <li>
            <a href="#">Customer</a>
            <ul class="second-level-menu">
                <li><a href="http://localhost:1303/Pages/Resident/TenantList.aspx">Tenant Add/Change/View Information</a></li>
                <li><a href="http://localhost:1303/Pages/Admin/AddContact.aspx">Contact Add/Change/View Information</a></li>             
              
            </ul>
        </li>
         <li>
            <a href="#">Vendor</a>
            <ul class="second-level-menu">
                <li><a href="http://localhost:1303/Pages/Admin/AddVendor.aspx">Add/Change/View Information</a></li>
            </ul>
        </li>
         <li>
            <a href="#">Reports</a>
            <ul class="second-level-menu">
                <li><a href="http://localhost:1303/Pages/Account/BalanceSheet.aspx">Balance Sheet</a></li>
                <li><a href="http://localhost:1303/Pages/Account/IncomeStatement.aspx">Profit & Loss</a></li>             
               
            </ul>
        </li>
    </ul>
</div>
