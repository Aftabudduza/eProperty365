<%@ Page Title="EProperty365: Make Bill Payment" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="MakeBillPayment.aspx.cs" Inherits="eProperty.Pages.Account.MakeBillPayment" %>
<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
    <div class="col-md-12" style="padding-left: 2px;">
         <div class="row">
    <div class="box-header with-border CommonHeader col-md-12" style="float: none;">
        <h3 class="box-title">Make Bill Payment</h3>
    </div>
             </div>
         <div class="row">
         <MyAccount:AccountControl ID="Account" runat="server" />
      </div>
        <div class="row">
            <div class="col-12">
                <h6>Outstanding Invoices</h6>
            </div>
        </div>
        <div class="row">
        <div class="col-4">
            <div class="form-group row">
                <label for="txtFromDate" class="col-sm-3 col-form-label">From :</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control tDate" id="txtFromDate"/>
                </div>
            </div>
        </div>
        <div class="col-4">
            <div class="form-group row">
                <label for="txtToDate" class="col-sm-3 col-form-label">To :</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control tDate" id="txtToDate"/>
                </div>
            </div>
        </div>
        <div class="col-4">
                <div class="form-group row">
                    <input type="button" class="btn" style="background-color: #66FF00" value="Go" id="btnGo"/>
                </div>
            </div>
    </div>
    
    
    <div class="row">
        <div class="col-12">
            <div class="row">
                <div class="col-12">
                    <div id="divTable1" style="max-height: 250px; overflow-x: auto; text-align: center;">
                        <table id="tbl" class="table table-responsive table-bordered table-hover">
                            <thead>
                            <tr>
                                <th></th>
                                <th>Date</th>
                                <th>Bill Number</th>
                                <th>Name</th>
                                <th>Person/Company</th>
                                <th>Address</th>
                                <th>Total Amount</th>
                            </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
        
        <div class="row">
            <div class="col-12">
                <div class="form-group row">
                    <div class="text-center mx-auto">
                        <input type="button" class="btn" style="background-color: #3B5998" value="Select a Item" id="btnSelectAItem"/>
                    </div>
                </div>
            </div>
        </div>
    <div class="row">
         <div class="col-12">
             <div class="row">
                 <div class="col-12">
                     <div id="divTable" style="max-height: 250px; overflow-x: auto; text-align: center;">
                         <table id="tblDescription" class="table table-responsive table-bordered table-hover">
                             <thead>
                             <tr>
                                 <th>Due Date</th>
                                 <th>Bill Id</th>
                                 <th>Description</th>
                                 <th>Credit Account Name</th>
                                 <th>Debit Account Name</th>
                                 <th>Amount</th>
                             </tr>
                             </thead>
                             <tbody></tbody>
                         </table>
                     </div>
                 </div>
             </div>
             <div class="row">
                 <div class="col-6">
                     <div class="form-group row">
                         <div class="col-sm-6">
                             <select id="ddlType" class="ddl form-control"></select>
                         </div>
                         <div class="col-sm-6">
                             <input type="text" class="form-control" id="txtCheckNumber" Placeholder="Check Number" />
                         </div>
                         
                         
                     </div>
                 </div>
                 <div class="col-6">
                     <div class="form-group row">
                         <div class="text-left">
                             <input type="button" class="btn" style="background-color: #66FF00" value="Print Check" id="btnPrintCheck"/>
                             <input type="button" class="btn" style="background-color: #3B5998" value="Cancel" id="btnCancel"/>
                             
                         </div>
                     </div>
                 </div>
             </div>
             
         </div>
        </div>
   
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript"  src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/Account/MakeBillPayment.js"></script>
    <style type="text/css">
        .table thead tr th {
            vertical-align: top;
            width: 10%;
            padding: 0px;
        }
        .table-bordered > tbody > tr > td {
            padding: 5px;
            vertical-align: middle;
        }
    </style>
</asp:Content>
