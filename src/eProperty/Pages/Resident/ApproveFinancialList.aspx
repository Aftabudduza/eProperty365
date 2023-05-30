<%@ Page Title="EProperty365: Approve Transaction" Language="C#" MasterPageFile="~/MasterPage/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ApproveFinancialList.aspx.cs" Inherits="eProperty.Pages.Resident.ApproveFinancialList" %>
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
                                        <h3 class="box-title">Transaction List</h3>
                                    </div>

                                    <%-- <MyAccount:AccountControl ID="Account" runat="server" />--%>

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label for="ddlOwner" class="col-sm-3 control-label" style="float: left;">*Owner ID:</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlOwner" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlOwner_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlOwner" InitialValue="-1" ErrorMessage="Select owner" ForeColor="Red" ValidationGroup="ContactSearch"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>                                            

                                            <div class="col-md-6" style="visibility:hidden;">
                                                <label for="ddlUnit" class="col-sm-3 control-label" style="float: left;">Unit ID:</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlUnit" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                           
                                            <div class="col-md-6">
                                                <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-success " />
                                            </div>

                                            <asp:HiddenField ID="hdnAppId" runat="server" />
                                            <asp:HiddenField ID="hdnUnitSerial" runat="server" />

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
                                                <asp:TemplateField HeaderText="Ref Id" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("FromUser") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Invoice No" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit ID" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("UnitNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Amount" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("Amount","{0:c}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreateDate", "{0:MMM dd  yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Transaction Type" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("TransactionType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ledger Code" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("LedgerCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAppId" runat="server" Visible="False" Text='<%# Eval("Serial") %>' />
                                                        <asp:Label ID="lblUnitSerialId" runat="server" Visible="False" Text='<%# Eval("UnitNo") %>' />
                                                        <asp:LinkButton CssClass="btn btn-success" ID="btnApprove" OnClick="btnApprove_Click" runat="server" Text="Approve"></asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-success" ID="btnReject" OnClick="btnReject_Click" runat="server" Text="Reject"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="Black" />

                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
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
                            <!-- left column -->

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


    </form>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
