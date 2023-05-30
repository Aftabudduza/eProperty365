<%@ Page Title="EProperty365: Balance Sheet" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="BalanceSheet.aspx.cs" Inherits="eProperty.Pages.Account.BalanceSheet" %>

<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box">
        <div class="col-md-12" style="padding-left: 2px;">
            <div class="box-header with-border CommonHeader col-md-12" style="float: none;">
                <h3 class="box-title">Balance Sheet</h3>
            </div>
            <MyAccount:AccountControl ID="Account" runat="server" />
            <div class="box-body">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <label for="txtFromDate" class="col-sm-3 control-label">Date:</label>
                        <div class="col-sm-6">
                            <input type="text" id="txFromDate" class="form-control mainDate" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-sm-3 text-center mx-auto">
                            <input type="button" class="btn" style="background-color: #66FF00" value="Show Report" data-id="0" id="btnShowReport" />
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/ReportJs/BalanceSheet.js"></script>
</asp:Content>
