<%@ Page Title="EProperty365: Dashboard Main Menu" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="DashboardAccount.aspx.cs" Inherits="eProperty.Pages.Account.DashboardAccount" %>

<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border CommonHeader col-md-12">
                                <h3 class="box-title">Accounting Module - Main Menu</h3>
                            </div>

                            <MyAccount:AccountControl ID="AccountControl1" runat="server" />

                            <div class="box-body">
                                <div class="col-md-6">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnClose_Click" Style="margin-top: 100px;" CssClass="btn btn-success " />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
