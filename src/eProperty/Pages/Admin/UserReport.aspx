<%@ Page Title="EProperty365: User List" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UserReport.aspx.cs" Inherits="eProperty.Pages.Admin.UserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager runat="server" ID="sc1">
        </asp:ScriptManager>

        <div class="box">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border CommonHeader col-md-12">
                        <h3 class="box-title" id="lblHeadline" runat="server">Eproperty365 Users</h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="userpanel">
                        <ContentTemplate>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <label for="txtLoginEmail" class="col-sm-4 control-label" style="float: left;">Master Password:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtLoginEmail" TextMode="Password" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                       
                                            <asp:Label ForeColor="Red" Font-Bold="true" Visible="false" Text="Incorrect Password" ID="lblValid" CssClass="col-sm-6" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Button ID="btnLoginSupport" runat="server" Text="Login" OnClick="btnLoginSupport_Click" CssClass="btn btn-success " />
                                    </div>
                                </div>
                            </div>

                            <div class="box-body" id="divSearchUser" visible="false" runat="server">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <input type="text" id="search" name="search" runat="server" class="form-control search-box" placeholder="Enter Search Keywords" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-success " />
                                    </div>
                                </div>
                            </div>
                            <div class="box-body" id="divSearchUserList" visible="false" runat="server">
                                <form class="vertical-form">
                                    <fieldset>
                                        <asp:GridView Width="100%" ID="gvContactList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                            GridLines="None" AllowPaging="True" OnPageIndexChanging="gvContactList_PageIndexChanging"
                                            OnSorting="gvContactList_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                            <PagerSettings Position="TopAndBottom" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Name" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesp1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Phone" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesp2" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Email" SortExpression="Data">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesp13" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="box-footer">
                        <div class="col-md-12">
                            <div class="col-md-4 btnSubmitDiv">
                                <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn btn-success " />
                            </div>

                            <div class="col-md-4 btnSubmitDiv">
                            </div>
                            <div class="col-md-4 btnSubmitDiv">
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
