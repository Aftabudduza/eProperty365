<%@ Page Title="EProperty365: Dealer & Sales Partner Dashboard" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="DashboardSalesPartner.aspx.cs" Inherits="eProperty.Pages.DashboardSalesPartner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>

        <div class="box">
            <div class="col-md-12">
                <div class="box-header with-border CommonHeader col-md-12">
                    <h3 class="box-title">Dealer & Sales Partner Dashboard</h3>
                </div>

                <div class="nav-tabs-custom">
                    <ul id="topnav" class="nav nav-tabs">
                        <li><a href="#Commissions" data-toggle="tab" class="active show">Your Commissions</a></li>
                        <li><a href="#Accounts" data-toggle="tab">Your Accounts</a></li>
                        <li><a href="#Partner" data-toggle="tab">Sales Partner Profile</a></li>
                        <li><a href="#Dealer" data-toggle="tab">Dealer Profile</a></li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane active show" id="Commissions">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="box-body">
                                        </div>
                                        <div class="box-body">
                                            <fieldset>
                                                <asp:GridView Width="100%" ID="gvCommission" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                    GridLines="None" AllowPaging="True"
                                                    PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                                    <PagerSettings Position="TopAndBottom" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name" SortExpression="Data">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Title" SortExpression="Data">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesp1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />

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
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="tab-pane" id="Accounts">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="box-body">
                                            <div class="col-md-8">
                                                <h3 class="box-title" id="lblPropertyLocation" runat="server"></h3>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="ddlOwner" class="col-sm-3 control-label" style="float: left;">*Owner ID:</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlOwner" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlOwner" InitialValue="-1" ErrorMessage="Select owner" ForeColor="Red" ValidationGroup="ContactSearch"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="ddlLocation" class="col-sm-3 control-label" style="float: left;">Location:</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlLocation" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="col-md-4">
                                                <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success " />
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <fieldset>
                                                <asp:GridView Width="100%" ID="gvLocation" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                    GridLines="None" AllowPaging="True"
                                                    PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                                    <PagerSettings Position="TopAndBottom" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name" SortExpression="Data">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Title" SortExpression="Data">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesp1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />

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
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                        <div class="tab-pane" id="Partner">
                            <div class="box">
                                <div class="box box-primary">
                                    <div class="box-header with-border CommonHeader col-md-12">
                                        <h3 class="box-title">Sales Partner Profile</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-8">
                                                <label for="ddlOwner" class="col-sm-12 control-label">Sales Partner First Name</label>

                                            </div>
                                            <div class="col-md-4">
                                            </div>

                                            <%--<table class="auto-style3">
                                                    <tr>
                                                        <td>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style55">&nbsp;</td>
                                                                    <td class="auto-style56">
                                                                        <asp:TextBox ID="ds5spfirstnameTxtBox" runat="server" Width="439px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style57">
                                                                        <p class="auto-style6">
                                                                            E365 Only
                                                                        </p>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style59">Sales Partners First Name&nbsp;&nbsp;</td>
                                                                    <td class="auto-style123">&nbsp;Sales Partner ID</td>
                                                                    <td class="auto-style62">
                                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style59">
                                                                        <asp:TextBox ID="ds5splastnameTxtBox" runat="server" Width="441px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style123">&nbsp; Join Date:&nbsp;</td>
                                                                    <td class="auto-style62">
                                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style63">Sales Partners Last Name&nbsp;&nbsp;</td>
                                                                    <td class="auto-style124">&nbsp; Commission Rate:&nbsp;</td>
                                                                    <td class="auto-style62">
                                                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style70">
                                                                        <asp:TextBox ID="ds5address1TxtBox" runat="server" Width="439px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style67">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style69">Address 1&nbsp;&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style72">
                                                                        <asp:TextBox ID="ds5address2TxtBox" runat="server" Width="440px"></asp:TextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style73">Address 2</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style89">&nbsp;</td>
                                                                    <td class="auto-style74">
                                                                        <asp:TextBox ID="ds5cityTxtBox" runat="server" Width="223px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style87">&nbsp;</td>
                                                                    <td class="auto-style75">
                                                                        <asp:DropDownList ID="ds5stateDDList" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="auto-style75">&nbsp;</td>
                                                                    <td class="auto-style82">
                                                                        <asp:DropDownList ID="ds5countryDDList" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="auto-style81">
                                                                        <asp:TextBox ID="ds5regionTxtBox" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style77">
                                                                        <asp:TextBox ID="ds5zipcodeTxtBox" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style83">City&nbsp;</td>
                                                                    <td class="auto-style88">&nbsp;State&nbsp;</td>
                                                                    <td class="auto-style80">Country</td>
                                                                    <td class="auto-style84">&nbsp;</td>
                                                                    <td class="auto-style85">Region</td>
                                                                    <td>Zip Code</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style90">
                                                                        <asp:TextBox ID="ds5spphoneTxtBox" runat="server" Width="221px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="ds5spmphoneTxtBox" runat="server" Width="243px"></asp:TextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style91">
                                                                <tr>
                                                                    <td class="auto-style92">&nbsp;</td>
                                                                    <td class="auto-style93">Primary Phone Number</td>
                                                                    <td class="auto-style94">Mobile Phone Number</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style95">
                                                                        <asp:TextBox ID="ds5spemailTxtBox" runat="server" Width="320px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="ds5spreemaileTxtBox" runat="server" Width="355px"></asp:TextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style98">&nbsp;</td>
                                                                    <td class="auto-style125">Email Address</td>
                                                                    <td class="auto-style126">&nbsp;</td>
                                                                    <td class="auto-style127">Re-Enter Email</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table class="auto-style4">
                                                                <tr>
                                                                    <td class="auto-style128">
                                                                        <p class="auto-style129">
                                                                            Zip Lead Coverage
                                                                        </p>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <br />
                                                            <table class="auto-style130">
                                                                <tr>
                                                                    <td>
                                                                        <br />
                                                                        <table class="auto-style132">
                                                                            <tr>
                                                                                <td class="auto-style133">&nbsp;</td>
                                                                                <td class="auto-style128">By Zip Coverage:</td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                        <table class="auto-style132">
                                                                            <tr>
                                                                                <td class="auto-style84">&nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TextBox2" runat="server" Height="344px" TextMode="MultiLine" Width="768px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                        <table class="auto-style91">
                                                                            <tr>
                                                                                <td class="auto-style134">&nbsp;</td>
                                                                                <td class="auto-style135">&nbsp;</td>
                                                                                <td class="auto-style143">Zip Code:</td>
                                                                                <td class="auto-style137">
                                                                                    <asp:TextBox ID="ds5dcommrateTxtBox" runat="server" Width="79px"></asp:TextBox>
                                                                                </td>
                                                                                <td class="auto-style138">
                                                                                    <asp:Button ID="ds5addBtn" runat="server" BackColor="#66FF00" Text="Add" Width="76px" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="ds5removeBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Remove" />
                                                                                </td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style114">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>--%>
                                        </div>
                                    </div>


                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSubmitContact" runat="server" Text="Save" CssClass="btn btn-successNew" />

                                            <asp:Button ID="btnCloseContact" runat="server" Text="Cancel" CssClass="btn btn-success " />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="tab-pane" id="Dealer">
                            <div class="box">
                                <div class="box box-primary">
                                    <div class="box-header with-border CommonHeader col-md-12">
                                        <h3 class="box-title">Dealer Profile</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="auto-style3">
                                                <table class="auto-style51">
                                                    <tr>
                                                        <td>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style55">&nbsp;</td>
                                                                    <td class="auto-style56">
                                                                        <asp:TextBox ID="ds6spfirstnameTxtBox" runat="server" Width="439px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style57">
                                                                        <p class="auto-style6">
                                                                            E365 Only
                                                                        </p>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style59">Dealer First Name&nbsp;&nbsp;</td>
                                                                    <td class="auto-style123">&nbsp;Dealer Partner ID</td>
                                                                    <td class="auto-style62">
                                                                        <asp:Label ID="ds6commrateLbl" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style59">
                                                                        <asp:TextBox ID="ds6splastnameTxtBox" runat="server" Width="441px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style123">&nbsp; Join Date:&nbsp;</td>
                                                                    <td class="auto-style62">
                                                                        <asp:Label ID="ds6spjoindateLbl" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style63">Dealer Last Name&nbsp;&nbsp;</td>
                                                                    <td class="auto-style124">&nbsp; Commission Rate:&nbsp;</td>
                                                                    <td class="auto-style62">
                                                                        <asp:Label ID="ds6spcommrateLbl" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style70">
                                                                        <asp:TextBox ID="ds6address1TxtBox" runat="server" Width="439px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style67">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style69">Address 1&nbsp;&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style72">
                                                                        <asp:TextBox ID="ds6address2TxtBox" runat="server" Width="440px"></asp:TextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style73">Address 2</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style89">&nbsp;</td>
                                                                    <td class="auto-style74">
                                                                        <asp:TextBox ID="ds6cityTxtBox" runat="server" Width="223px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style87">&nbsp;</td>
                                                                    <td class="auto-style75">
                                                                        <asp:DropDownList ID="ds6stateDDList" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="auto-style75">&nbsp;</td>
                                                                    <td class="auto-style82">
                                                                        <asp:DropDownList ID="ds6countryDDList" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="auto-style81">
                                                                        <asp:TextBox ID="ds6regionTxtBox" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style77">
                                                                        <asp:TextBox ID="ds6zipcodeTxtBox" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style83">City&nbsp;</td>
                                                                    <td class="auto-style88">&nbsp;State&nbsp;</td>
                                                                    <td class="auto-style80">Country</td>
                                                                    <td class="auto-style84">&nbsp;</td>
                                                                    <td class="auto-style85">Region</td>
                                                                    <td>Zip Code</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style90">
                                                                        <asp:TextBox ID="ds6spphoneTxtBox" runat="server" Width="221px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="ds6spmphoneTxtBox" runat="server" Width="243px"></asp:TextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style91">
                                                                <tr>
                                                                    <td class="auto-style92">&nbsp;</td>
                                                                    <td class="auto-style93">Primary Phone Number</td>
                                                                    <td class="auto-style94">Mobile Phone Number</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style58">&nbsp;</td>
                                                                    <td class="auto-style95">
                                                                        <asp:TextBox ID="ds6spemailTxtBox" runat="server" Width="320px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Tds6spreemaileTxtBox" runat="server" Width="355px"></asp:TextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style98">&nbsp;</td>
                                                                    <td class="auto-style125">Email Address</td>
                                                                    <td class="auto-style126">&nbsp;</td>
                                                                    <td class="auto-style127">Re-Enter Email</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table class="auto-style4">
                                                                <tr>
                                                                    <td class="auto-style128">
                                                                        <p class="auto-style129">
                                                                            Exclusive Territory
                                                                        </p>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <br />
                                                            <table class="auto-style130">
                                                                <tr>
                                                                    <td>
                                                                        <br />
                                                                        <table class="auto-style132">
                                                                            <tr>
                                                                                <td class="auto-style133">&nbsp;</td>
                                                                                <td class="auto-style128">By State:</td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                        <table class="auto-style132">
                                                                            <tr>
                                                                                <td class="auto-style84">&nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="ds6dzipTxtBox" runat="server" Height="344px" TextMode="MultiLine" Width="768px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                        <table class="auto-style91">
                                                                            <tr>
                                                                                <td class="auto-style134">&nbsp;</td>
                                                                                <td class="auto-style135">
                                                                                    <asp:DropDownList ID="ds6dstateDDList" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="auto-style143">Commission Rate:</td>
                                                                                <td class="auto-style137">
                                                                                    <asp:TextBox ID="ds6dcommrateTxtBox" runat="server" Width="79px"></asp:TextBox>
                                                                                </td>
                                                                                <td class="auto-style138">
                                                                                    <asp:Button ID="ds6addBtn" runat="server" BackColor="#66FF00" Text="Add" Width="76px" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="ds6removeBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Remove" />
                                                                                </td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table class="auto-style53">
                                                                <tr>
                                                                    <td class="auto-style114">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <table class="auto-style91">
                                                                <tr>
                                                                    <td class="auto-style139">&nbsp;</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Height="184px" TextMode="MultiLine" Width="763px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table class="auto-style91">
                                                                <tr>
                                                                    <td class="auto-style140">&nbsp;</td>
                                                                    <td class="auto-style141">
                                                                        <asp:DropDownList ID="ds6dzipsDDList" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="auto-style143">Commission Rate:</td>
                                                                    <td class="auto-style144">
                                                                        <asp:TextBox ID="ds6dcoomratebyzipTxtBox" runat="server" Width="81px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="auto-style145">
                                                                        <asp:Button ID="ds6AddzipBtn" runat="server" BackColor="#66FF00" Text="Add" Width="76px" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="ds6removezipBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Remove" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <%--<table class="auto-style91">
                                                    <tr>
                                                        <td class="auto-style121">&nbsp;</td>
                                                        <td class="auto-style122">
                                                            <asp:Button ID="ds6cancelBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Cancel" Width="80px" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="ds6saveBtn" runat="server" BackColor="#66FF00" Text="Save" Width="80px" />
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>--%>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSaveUser" runat="server" Text="Save" OnClientClick="CheckVal()" CssClass="btn btn-successNew" ValidationGroup="user" />

                                            <asp:Button ID="btnCancelUser" runat="server" Text="Cancel" CssClass="btn btn-success " />
                                        </div>
                                    </div>
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

    <style type="text/css">
        .auto-style3 {
            border-style: solid;
            border-width: 1px;
            margin-right: 46px;
        }

        .auto-style4 {
            width: 100%;
            background-color: #3B5998;
        }

        .auto-style5 {
        }

        .auto-style6 {
            text-align: center;
            width: 321px;
        }

        .auto-style51 {
            width: 94%;
            margin-left: 31px;
            background-color: #DFE3EE;
        }

        .auto-style52 {
            width: 94%;
            margin-left: 31px;
            background-color: #8B9DC3;
        }

        .auto-style53 {
            width: 100%;
            background-color: #DFE3EE;
        }

        .auto-style54 {
            width: 181px;
        }

        .auto-style55 {
            width: 31px;
        }

        .auto-style56 {
            width: 517px;
        }

        .auto-style57 {
            width: 315px;
            background-color: #8B9DC3;
        }

        .auto-style58 {
            width: 32px;
        }

        .auto-style59 {
            width: 515px;
        }

        .auto-style62 {
            width: 186px;
            background-color: #8B9DC3;
        }

        .auto-style63 {
            width: 513px;
        }

        .auto-style67 {
            width: 324px;
            background-color: #8B9DC3;
        }

        .auto-style69 {
            width: 510px;
        }

        .auto-style70 {
            width: 512px;
        }

        .auto-style71 {
            text-align: center;
            color: #FFFFFF;
            width: 991px;
        }

        .auto-style72 {
            width: 509px;
        }

        .auto-style73 {
            width: 506px;
        }

        .auto-style74 {
            width: 238px;
        }

        .auto-style75 {
            width: 143px;
        }

        .auto-style77 {
            width: 389px;
        }

        .auto-style80 {
            width: 90px;
        }

        .auto-style81 {
            width: 227px;
        }

        .auto-style82 {
            width: 187px;
        }

        .auto-style83 {
            width: 284px;
        }

        .auto-style84 {
            width: 50px;
        }

        .auto-style85 {
            width: 153px;
        }

        .auto-style87 {
            width: 129px;
        }

        .auto-style88 {
            width: 126px;
        }

        .auto-style89 {
            width: 107px;
        }

        .auto-style90 {
            width: 322px;
        }

        .auto-style91 {
            width: 100%;
        }

        .auto-style92 {
            width: 67px;
        }

        .auto-style93 {
            width: 327px;
        }

        .auto-style94 {
            width: 156px;
        }

        .auto-style95 {
            width: 402px;
        }

        .auto-style98 {
            width: 144px;
        }

        .auto-style114 {
            width: 51px;
        }

        .auto-style121 {
            width: 259px;
        }

        .auto-style122 {
            width: 263px;
        }

        .auto-style123 {
            width: 133px;
            background-color: #8B9DC3;
        }

        .auto-style124 {
            width: 134px;
            background-color: #8B9DC3;
        }

        .auto-style125 {
            width: 148px;
        }

        .auto-style126 {
            width: 261px;
        }

        .auto-style127 {
            width: 172px;
        }

        .auto-style128 {
            color: #FFFFFF;
        }

        .auto-style129 {
            text-align: center;
        }

        .auto-style130 {
            width: 94%;
            margin-left: 32px;
            background-color: #8B9DC3;
        }

        .auto-style132 {
            width: 100%;
            background-color: #8B9DC3;
        }

        .auto-style133 {
            width: 46px;
        }

        .auto-style134 {
            width: 29px;
        }

        .auto-style135 {
            width: 95px;
        }

        .auto-style137 {
            width: 200px;
        }

        .auto-style138 {
            width: 106px;
        }

        .auto-style140 {
            width: 34px;
        }

        .auto-style141 {
            width: 114px;
        }

        .auto-style143 {
            width: 134px;
        }

        .auto-style144 {
            width: 212px;
        }

        .auto-style145 {
            width: 103px;
        }

        .auto-style146 {
            border-style: solid;
            border-width: 1px;
        }
    </style>
    <style type="text/css">
        .input-validation-error {
            border: 1px solid #ff0000;
            background-color: #ffeeee !important;
        }

        .field-validation-error {
            color: #ff0000 !important;
        }

        .control-label {
            float: left;
        }

        .col-sm-3 {
            padding-left: 0px;
        }

        .col-sm-6 {
            padding-left: 0px;
            padding-right: 0px;
        }

        .col-md-3 {
            float: left;
            padding-left: 10px;
        }

        label {
            margin-bottom: 0px;
        }
    </style>
    <style type="text/css">
        .color-palette {
            height: 35px;
            line-height: 35px;
            text-align: center;
        }

        .color-palette-set {
            margin-bottom: 15px;
        }

        .color-palette span {
            display: none;
            font-size: 12px;
        }

        .color-palette:hover span {
            display: block;
        }

        .color-palette-box h4 {
            position: absolute;
            top: 100%;
            left: 25px;
            margin-top: -40px;
            color: rgba(255, 255, 255, 0.8);
            font-size: 12px;
            display: block;
            z-index: 7;
        }

        .nav-tabs-custom {
            background-color: #DFE3EE;
        }

            .nav-tabs-custom > .nav-tabs {
                border-bottom-color: initial;
                padding: 0px 0px 0px 18px;
                background-color: #8B9DC3;
            }

                .nav-tabs-custom > .nav-tabs > li > a, .nav-tabs-custom > .nav-tabs > li > a:hover {
                    background: #ffffff;
                }

                .nav-tabs-custom > .nav-tabs > li > a {
                    border-radius: 10px 10px 0px 0px;
                }

        .nav-tabs {
            border-bottom: none;
        }

        .nav > li > a.active {
            background-color: #98cdfe;
            color: #ffffff;
        }

        .nav-tabs-custom > .tab-content {
            background: none;
            border-bottom-right-radius: 3px;
            border-bottom-left-radius: 3px;
            padding: 10px 10px 10px 5px;
        }

        .col-md-12 {
            padding-left: 0px;
            padding-right: 0px;
        }
    </style>


    <script type="text/javascript">
        $(".nav nav-tabs  li a").on("click", function () {
            $(".nav nav-tabs  li").find(".active").removeClass("active");
            $(this).addClass("active");
        });
    </script>
    <script type="text/javascript">

        function show_confirm() {
            var r = confirm("Are You Sure To Delete?");
            if (r) {
                return true;
            }
            else {
                return false;
            }
        }

        function CheckVal() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
        }



    </script>
</asp:Content>
