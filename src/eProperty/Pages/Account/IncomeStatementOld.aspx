<%@ Page Title="EProperty365: Income Statement" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="IncomeStatementOld.aspx.cs" Inherits="eProperty.Pages.Account.IncomeStatementOld" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
       <%-- <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>--%>
        <div class="row">
            <div class="col-md-12">
                <%--<asp:UpdatePanel runat="server" ID="UpdatePanel4">
                    <ContentTemplate>--%>
                <div class="box">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border CommonHeader col-md-12">
                                <h3 class="box-title">Income Statement</h3>
                            </div>

                            <MyAccount:AccountControl ID="Account" runat="server" />

                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <label for="txtFromDate" class="col-sm-3 control-label">From Date:</label>
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
                                        <label for="txtToDate" class="col-sm-3 control-label">To Date:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="tDate form-control"></asp:TextBox>
                                            <%--<img alt="" id="to" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                                        width="20px" height="30px" />
                                                    <asp:CalendarExtender ID="CalendarExtender17" runat="server" PopupButtonID="to"
                                                        TargetControlID="txtToDate">
                                                    </asp:CalendarExtender>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="spanIncome" class="col-sm-3 control-label">Total Income:</label>
                                        <div class="col-sm-6">
                                            <span id="spanIncome" style="font-weight: bold; color: forestgreen;" runat="server">0</span>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-success " />
                                        <asp:Button ID="btnReport" runat="server" Text="Export" OnClick="btnReport_Click" CssClass="btn btn-success " />
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="box">
                    <div class="box-body">
                    </div>
                    <fieldset>
                        <asp:GridView Width="100%" ID="gvLocation" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                            GridLines="None" AllowPaging="True" OnPageIndexChanging="gvLocation_PageIndexChanging"
                            OnSorting="gvLocation_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                            <PagerSettings Position="TopAndBottom" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction No" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Invoice No" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Account Type" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("TranType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ledger Code" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("LedgerCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ledger A/C" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("LedgerName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Income" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Credit","{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expense" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Debit","{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreateDate", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                <div>
                                </div>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#d1d0d0" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle Font-Bold="True" ForeColor="Black" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </fieldset>
                </div>
                <div class="box">
                    <div class="col-md-12">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <rsweb:ReportViewer ID="rptViewerSKUStock" runat="server" AsyncRendering="false" Font-Names="Verdana" SizeToReportContent="True"></rsweb:ReportViewer>
                        <%--  <rsweb:ReportViewer ID="rptViewerSKUStock" Width="90%" runat="server" ProcessingMode="Local" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                </rsweb:ReportViewer>--%>
                    </div>
                </div>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
