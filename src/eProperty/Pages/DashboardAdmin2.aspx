<%@ Page Title="EProperty365: Admin Dashboard" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DashboardAdmin2.aspx.cs" Inherits="eProperty.Pages.DashboardAdmin2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>

        <div class="box">
            <div class="col-md-12">
                <div class="box-header with-border CommonHeader col-md-12">
                    <h3 class="box-title">EProperty365 Dashboard</h3>
                </div>

                <div class="nav-tabs-custom">
                    <ul id="topnav" class="nav nav-tabs">
                        <li><a href="#Supports" data-toggle="tab" class="active show">Supports</a></li>
                        <li><a href="Admin/AddOwnerSystem.aspx?R=home">System</a></li>
                        <li><a href="Admin/AddContact.aspx?R=home">Contacts</a></li>
                        <li><a href="Admin/AddUser.aspx?R=home">Users</a></li>
                        <li><a href="Admin/AddVendor.aspx?R=home">Vendor List</a></li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane active show" id="Supports">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                <ContentTemplate>
                                    <div class="box" runat="server" id="divlogin">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-header with-border CommonHeader col-md-12">
                                                    <h3 class="box-title">Support</h3>
                                                </div>

                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            <label for="txtLoginEmail" class="col-sm-4 control-label" style="float: left;">User Email Address:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtLoginEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtLoginSS" class="col-sm-3 control-label" style="float: left;">Location ID:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtLoginSS" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnLoginSupport" runat="server" Text="Login" OnClick="btnLoginSupport_Click" CssClass="btn btn-success " />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="box" runat="server" id="divSearchLocation">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            <label for="ddlOwner" class="col-sm-3 control-label" style="float: left;">*Owner ID:</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlOwner" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlOwner_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlOwner" InitialValue="-1" ErrorMessage="Select owner" ForeColor="Red" ValidationGroup="ContactSearch"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="ddlPropertyManager" class="col-sm-3 control-label" style="float: left;">Property Manager ID:</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlPropertyManager" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlPropertyManager_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="ddlLocation" class="col-sm-3 control-label" style="float: left;">Location:</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlLocation" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <label for="ddlUnit" class="col-sm-3 control-label" style="float: left;">Unit ID:</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlUnit" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtTenantName" class="col-sm-3 control-label" style="float: left;">Tenant Id:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtTenantName" runat="server" CssClass="form-control"></asp:TextBox>
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
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-header with-border CommonHeader col-md-12">
                                                    <h3 class="box-title">Property Lease / Rental Application Portal</h3>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <asp:GridView Width="100%" ID="gvLocation" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-responsive table-bordered"
                                                            GridLines="None" AllowPaging="True" OnPageIndexChanging="gvLocation_PageIndexChanging"
                                                            OnSorting="gvLocation_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                                            <PagerSettings Position="TopAndBottom" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Application ID" SortExpression="Data">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("ApplicationCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tenant Name" SortExpression="Data">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("TenantName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Location" SortExpression="Data">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("LocationName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit ID" SortExpression="Data">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("UnitSerial") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" SortExpression="Data">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("ApproveStatus") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAppId" runat="server" Visible="False" Text='<%# Eval("ApplicationCode") %>' />
                                                                        <asp:Label ID="lblOwnerId" runat="server" Visible="False" Text='<%# Eval("OwnerId") %>' />
                                                                        <asp:Label ID="lblPropertyManagerId" runat="server" Visible="False" Text='<%# Eval("PropertyManagerSerialId") %>' />
                                                                        <asp:Label ID="lblUnitSerialId" runat="server" Visible="False" Text='<%# Eval("UnitSerial") %>' />
                                                                        <asp:Label ID="lblLocationId" runat="server" Visible="False" Text='<%# Eval("LocationSerial") %>' />
                                                                        <asp:Label ID="lblLocation" runat="server" Visible="False" Text='<%# Eval("LocationName") %>' />
                                                                        <asp:Label ID="lblTenantPassword" runat="server" Visible="False" Text='<%# Eval("Password") %>' />
                                                                        <asp:LinkButton CssClass="btn btn-success" ID="btnDetails" OnClick="btnDetails_Click" runat="server" Text="Details"></asp:LinkButton>

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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box-body">
                                                <div class="col-md-12">
                                                    <asp:HiddenField ID="hdnAppId" runat="server" />
                                                    <asp:HiddenField ID="hdnOwnerId" runat="server" />
                                                    <asp:HiddenField ID="hdnPropertyManagerId" runat="server" />
                                                    <asp:HiddenField ID="hdnUnitSerial" runat="server" />
                                                    <asp:HiddenField ID="hdnLocationId" runat="server" />
                                                    <asp:HiddenField ID="hdnLocationName" runat="server" />
                                                    <asp:HiddenField ID="hdnPassword" runat="server" />

                                                    <label class="col-sm-6 control-label" style="float: left;">If going to Approved Please fill out:</label>
                                                    <span class="col-sm-6" id="spanAppId" runat="server"></span>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 10px; float: left;">
                                                    <div class="col-md-6">
                                                        <label for="txtSecurityDeposit" class="col-sm-3 control-label" style="float: left;">Security Deposit:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtSecurityDeposit" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtSecurityDeposit_TextChanged"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSecurityDeposit" ErrorMessage="Security Deposit Required" ForeColor="Red" ValidationGroup="Rent"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtOneMonthRent" class="col-sm-3 control-label" style="float: left;">One Months Rent:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtOneMonthRent" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOneMonthRent_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtProrate" class="col-sm-3 control-label" style="float: left;">Prorate Amount:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtProrate" runat="server" CssClass="form-control" OnTextChanged="txtProrate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label for="txtFirstMonthRent" class="col-sm-3 control-label" style="float: left;">First Months Rent:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtFirstMonthRent" runat="server" CssClass="form-control" OnTextChanged="txtFirstMonthRent_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtTotalRent" class="col-sm-3 control-label" style="float: left;">Total:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtTotalRent" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtPayment" class="col-sm-3 control-label" style="float: left;">Date of monthly payment due:</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlPayment" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>

                                                            <%-- <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <%-- <img alt="" id="payment" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                                                width="20px" height="30px" />
                                                            <asp:CalendarExtender ID="CalendarExtender18" runat="server" PopupButtonID="payment"
                                                                TargetControlID="txtPayment">
                                                            </asp:CalendarExtender>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="box-footer">
                                                <div class="col-md-12">
                                                    <asp:Button ID="btnViewApplication" runat="server" Text="View Application" OnClick="btnViewApplication_Click" CssClass="btn btn-success" />

                                                    <asp:Button ID="btnViewSignIn" runat="server" Text="View Sign In & Pay Deposit" OnClick="btnViewSignIn_Click" CssClass="btn btn-success" />

                                                    <asp:Button ID="btnView" runat="server" Text="View Credit / Background Screening" CssClass="btn btn-success" />

                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-successNew" OnClick="btnApprove_Click" OnClientClick="CheckVal()" ValidationGroup="Rent" />

                                                    <asp:Button ID="btnDisapprove" runat="server" Text="Disapproved" CssClass="btn btn-success" OnClick="btnDisapprove_Click" OnClientClick='return confirm("Are you sure you want to Disappove?");' Style="background: red; border-color: white; border-color: #fff;" />

                                                    <asp:Button ID="btnCancel" OnClick="btnClose_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
        $(document).ready(function (parameters) {
            $(".tDate").datepicker({
                dateFormat: "mm-dd-yy",
                changeYear: false,
                changeMonth: true
            });
        });

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

        <%-- $('#<%=chkEmergency.ClientID %>').change(function () {
            if (this.checked) {
                $('#<%=EmergencyDiv.ClientID %>').show();
            }
            else {
                $('#<%=EmergencyDiv.ClientID %>').hide();
            }
        });--%>

    </script>
</asp:Content>
