<%@ Page Title="EProperty365: Add/Edit/View Journal Entry" EnableEventValidation="false" Debug="true" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="AddJournalEntry.aspx.cs" Inherits="eProperty.Pages.Account.AddJournalEntry" %>

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
                                        <h3 class="box-title">Add/View Journal Entry</h3>
                                    </div>

                                    <MyAccount:AccountControl ID="Account" runat="server" />

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label for="txtFromDate" class="col-sm-6 control-label">From Date:</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="tDate form-control"></asp:TextBox>
                                                   <%-- <img alt="" id="from" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                                        width="20px" height="30px" />
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="from"
                                                        TargetControlID="txtFromDate">
                                                    </asp:CalendarExtender>--%>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="txtToDate" class="col-sm-6 control-label">To Date:</label>
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
                                                <label for="ddlType" class="col-sm-6 control-label" style="float: left;">Ledger Code:</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-success " />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box">
                            <div class="box-body">
                                <form class="vertical-form">
                                    <fieldset>
                                        <asp:GridView Width="100%" ID="gvLocation" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                            GridLines="None" AllowPaging="True" OnPageIndexChanging="gvLocation_PageIndexChanging"
                                            OnSorting="gvLocation_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                            <PagerSettings Position="TopAndBottom" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Invoice No" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Account Type" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("AccountType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ledger Code" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("LedgerCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Debit" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("Debit","{0:c}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("Credit","{0:c}") %>'></asp:Label>
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
                                </form>
                            </div>
                        </div>

                        <div class="box">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-header with-border CommonHeader col-md-12">
                                        <h3 class="box-title">Add New Journal Entry</h3>
                                    </div>

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label for="ddlDebitAccount" class="col-sm-6 control-label" style="float: left;">Debit A/C:</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlDebitAccount" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="requserdebit" runat="server" ControlToValidate="ddlDebitAccount" InitialValue="-1" ErrorMessage="Please select Debit A/C" ForeColor="Red" ValidationGroup="Pay"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="ddlCreditAccount" class="col-sm-6 control-label" style="float: left;">Credit A/C:</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlCreditAccount" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="requsercredit" runat="server" ControlToValidate="ddlCreditAccount" InitialValue="-1" ErrorMessage="Please select Credit A/C" ForeColor="Red" ValidationGroup="Pay"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="txtPaid" class="col-sm-6 control-label" style="float: left;">Amount:</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtPaid" Enabled="true" runat="server" ValidationGroup="Pay" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ControlToValidate="txtPaid" Display="Dynamic"
                                                        ErrorMessage="Amount must be in format 0.00."
                                                        ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtPaid" Display="Dynamic" ValidationGroup="Pay" ForeColor="Red"
                                                        ErrorMessage="Amount is required."></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="txtRemarks" class="col-sm-6 control-label" style="float: left;">Remarks:</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="float:left;">
                                            <div class="col-md-6 btnSubmitDiv">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add New Journal Entry" OnClick="btnAdd_Click" ValidationGroup="Pay" CssClass="btn btn-success " />
                                            </div>
                                            <div class="col-md-6 btnSubmitDiv">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-success " />
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
