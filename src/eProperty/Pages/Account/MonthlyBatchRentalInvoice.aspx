<%@ Page Title="EProperty365: Monthly Batch Rental Invoice" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="MonthlyBatchRentalInvoice.aspx.cs" Inherits="eProperty.AppJs.Account.MonthlyBatchRentalInvoice" %>
<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="col-md-12">
            <div class="box-header with-border CommonHeader col-md-12">
                <h3 class="box-title">Monthly Batch Rental Invoice</h3>
            </div>
            <MyAccount:AccountControl ID="Account" runat="server" />
            <div class="box-body">
                <div class="col-md-6">
                    <label for="ddlOwner" class="col-sm-6 control-label">Owner:</label>
                    <div class="col-sm-6">
                        <select class="form-control ddl" id="ddlOwner"></select>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="ddlPropertyManagerId" class="col-sm-6 control-label">Property Manager Id:</label>
                    <div class="col-sm-6">
                        <select class="form-control ddl" id="ddlPropertyManagerId"></select>
                    </div>
                </div>

            </div>
            <div class="box-body">
                <div class="col-md-6">
                    <label for="ddlLocationId" class="col-sm-6 control-label">Location Id:</label>
                    <div class="col-sm-6">
                        <select class="form-control ddl" id="ddlLocationId"></select>
                    </div>
                </div>
                <div class="col-md-6">
                    <%--<button style="background-color: yellow">See WorkFlow Accouting</button>--%>
                </div>
            </div>

            <div class="box-body">
                <div class="form-group row">
                    <div id="divTable" style="max-height: 250px; width: 90%; margin: auto; overflow-x: auto;">
                        <table class="table table-bordered table-striped" style="width: 100%" id="tbl">
                            <thead>
                                <tr>
                                    <th>SendDate</th>
                                    <th>Invoice ID</th>
                                    <th>Tenant Name</th>
                                    <th>Unit ID</th>
                                    <th>Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-footer">
                <div class="col-md-12 btnSaveWebCenter" style="float: left; margin-top: 1%">
                    <div class="col-md-6" style="float: left">
                        <button type="button" id="btnRunMonthlyRentalInvoices" class="btn btnNewColor col-sm-8">Run Monthly Rental Invoices</button>
                    </div>
                    <div class="col-md-6" style="float: left">
                        <button type="button" id="btnCancel" class="btn btnNewColor col-sm-4">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/Account/MonthlyBatchRentalInvoice.js"></script>
</asp:Content>
