<%@ Page Title="EProperty365: Accounting - Export Data" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="ExportAccountData.aspx.cs" Inherits="eProperty.Pages.Account.ExportAccountData" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>
        <div class="row">
            <div class="col-md-12">
                 <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                    <ContentTemplate>
                        <div class="box">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-header with-border CommonHeader col-md-12">
                                        <h3 class="box-title">Accounting Module - Export Data</h3>
                                    </div>

                                    <MyAccount:AccountControl ID="Account" runat="server" />

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label for="txtFromDate" class="col-sm-3 control-label">From:</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="tDate form-control"></asp:TextBox>
                                                    <%--<img alt="" id="from" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                                        width="20px" height="30px" />
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="from"
                                                        TargetControlID="txtFromDate">
                                                    </asp:CalendarExtender>--%>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="txtToDate" class="col-sm-3 control-label">To:</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="tDate form-control"></asp:TextBox>
                                                   <%-- <img alt="" id="to" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                                        width="20px" height="30px" />
                                                    <asp:CalendarExtender ID="CalendarExtender17" runat="server" PopupButtonID="to"
                                                        TargetControlID="txtToDate">
                                                    </asp:CalendarExtender>--%>
                                                </div>
                                            </div>
                                            <div class="col-md-12" style="float:left; margin-bottom:10px; padding-left:0px;">                                                
                                                <div class="col-sm-12">
                                                   <asp:RadioButtonList ID="rdoType" runat="server" CssClass="form-control" RepeatDirection="Horizontal" >
                                                       <asp:ListItem Value="1">Export Tenant Data </asp:ListItem>
                                                        <asp:ListItem Value="2">Export Contact Data </asp:ListItem>
                                                        <asp:ListItem Value="3">Journal Entries </asp:ListItem>
                                                        <asp:ListItem Value="4">Invoices Entries </asp:ListItem>
                                                        <asp:ListItem Value="5">Bills Entries</asp:ListItem>
                                                   </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="rdoType" ValidationGroup="Export"
                                                    runat="server" ErrorMessage="Please select Type of report" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <asp:Button ID="btnReport" runat="server" Text="Export" OnClick="btnReport_Click" ValidationGroup="Export" CssClass="btn btn-success " />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                       
                  </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
