<%@ Page Title="EProperty365: Accounting Module - About" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="AccountAbout.aspx.cs" Inherits="eProperty.Pages.Account.AccountAbout" %>

<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border CommonHeader col-md-12">
                                <h3 class="box-title">Accounting Module - About</h3>
                            </div>

                            <MyAccount:AccountControl ID="Account" runat="server" />

                            <div class="box-body">
                                 <div class="col-md-12" style="float:left;">     
                                    <label for="lblOwner" class="col-sm-6 control-label">Owner Name:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblOwner" CssClass="form-control" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12" style="float:left;">     
                                    <label for="lblLocation" class="col-sm-6 control-label">Location:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblLocation" CssClass="form-control" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                                 <div class="col-md-12" style="float:left;">     
                                    <label for="lblDatabaseLocation" class="col-sm-6 control-label">Database Location:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblDatabaseLocation" CssClass="form-control" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                                 <div class="col-md-12" style="float:left;">     
                                    <label for="lblDatabaseName" class="col-sm-6 control-label">Database Name:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblDatabaseName" CssClass="form-control" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                                 <div class="col-md-12" style="float:left;">     
                                    <label for="lblDatabasePassword" class="col-sm-6 control-label">Database Password:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblDatabasePassword" CssClass="form-control" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12" style="float:left;">     
                                    <label class="col-sm-12 control-label">Default Chart of Account Location:  www.eproperty365.net/forms/e365chartofaccounts.cvs  </label>
                                   
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
