<%@ Page Title="EProperty365: Add/Edit Owner" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddOwner.aspx.cs" Inherits="eProperty.Pages.Admin.AddOwner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="userpanel">
            <ContentTemplate>
                <div class="box">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box box-primary">
                            <div class="box-header with-border CommonHeader col-md-12">
                                <h3 class="box-title" id="lblHeadline" runat="server">Create / Change Owner Profile</h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="lblId" class="col-sm-3 control-label">Owner ID:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblId" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblMsg" ForeColor="Green" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtFirstName" class="col-sm-3 control-label">*First Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtFirstName" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="First Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtLastName" class="col-sm-3 control-label">*Last Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtLastName" ValidationGroup="Contact"
                                            Display="Dynamic" runat="server" ErrorMessage="Last Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtCompanyName" class="col-sm-3 control-label">*Company Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCompanyName" ValidationGroup="Contact"
                                            Display="Dynamic" runat="server" ErrorMessage="Company Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label class="col-sm-12 ">
                                        <asp:RadioButtonList runat="server" ID="rdoCompanyType" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">Sole Proprietorship</asp:ListItem>
                                            <asp:ListItem Value="2">LLC</asp:ListItem>
                                            <asp:ListItem Value="3">S Corp</asp:ListItem>
                                            <asp:ListItem Value="4">1120 Corp</asp:ListItem>
                                            <asp:ListItem Value="5">501C Corp</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </label>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtDate" class="col-sm-3 control-label" style="float: left;">When did you start?</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtDate" CssClass="form-control tDate" Text=""></asp:TextBox>
                                    </div>
                                    <%-- <img alt="" style="margin-left: 5px;" id="edate" src="https://www.eproperty365.net/Images/calender.jpg"
                                        width="40px" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="edate"
                                        TargetControlID="txtDate">
                                    </asp:CalendarExtender>--%>
                                </div>

                                <div class="col-md-6">
                                    <label for="ddlType" class="col-sm-3 control-label" style="float: left;">What State?</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlCompanySate" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtAddress" class="col-sm-3 control-label">*Address:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtAddress" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Address Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtAddress1" class="col-sm-3 control-label">Address1:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="ddlCountry" class="col-sm-3 control-label" style="float: left;">*Country:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlCountry" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" id-="requsertype" runat="server" ControlToValidate="ddlCountry" InitialValue="-1" ErrorMessage="Please select Country" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtRegion" class="col-sm-3 control-label">Region:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRegion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body" style="margin-top: 0px; margin-bottom: 0px;">
                                <div class="col-md-6">
                                    <label for="ddlState" class="col-sm-3 control-label" style="float: left;">State:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtCity" class="col-sm-3 control-label">*City:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ControlToValidate="txtCity" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="City Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtZip" class="col-sm-3 control-label">*Zip Code:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtZip" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Zip Code Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtEIN" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">EIN Number or Social Security Number:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtEIN" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="ddlSalesPartner" class="col-sm-3 control-label" style="float: left;">*Sales Partner:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlSalesPartner" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="SalesPartner" runat="server" ControlToValidate="ddlSalesPartner" InitialValue="-1" ErrorMessage="Please select Sales Partner" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label class="col-sm-12 ">
                                        <asp:RadioButtonList runat="server" ID="rdoEIN" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="EIN">EIN #</asp:ListItem>
                                            <asp:ListItem Value="SSN" Selected="True">Social Security #</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </label>
                                </div>
                            </div>
                            <div class="box-body" style="margin-top: 10px; text-align: center;">
                                <div class="col-md-12">
                                    <label class="col-sm-12 control-label" style="color: red;">* When creating an new Account if you do not have Property Manager, ignore the Tab. You can come back later to fill out Contacts and Vendor List Tabs.</label>
                                </div>
                            </div>
                            <div class="box-body" style="margin-top: 0px; margin-bottom: 0px; text-align: center;">
                                <div class="nav-tabs-custom">
                                    <ul id="topnav" class="nav nav-tabs">
                                        <%--<li><a href="AddOwnerSystem.aspx">Account Profile</a></li>
                                            <li><a href="AddUser.aspx">Users</a></li>
                                            <li><a href="AddPropertyManager.aspx">Property Manager</a></li>
                                            <li><a href="AddContact.aspx">Contacts</a></li>
                                            <li><a href="AddVendor.aspx">Vendor List</a></li> --%>

                                        <li>
                                            <asp:Button ID="btnAddAccount" runat="server" CssClass="btncustombox" Text="Account Profile" OnClick="btnAddAccount_Click" /></li>
                                        <li>
                                            <asp:Button ID="btnAddUser" runat="server" CssClass="btncustombox" Text="Users" OnClick="btnAddUser_Click" /></li>
                                        <li>
                                            <asp:Button ID="btnAddPropertyManager" runat="server" CssClass="btncustombox" Text="Property Manager" OnClick="btnAddPropertyManager_Click" /></li>
                                        <li>
                                            <asp:Button ID="btnAddContact" runat="server" CssClass="btncustombox" Text="Contacts" OnClick="btnAddContact_Click" /></li>
                                        <li>
                                            <asp:Button ID="btnAddVendor" runat="server" CssClass="btncustombox" Text="Vendor List" OnClick="btnAddVendor_Click" /></li>
                                    </ul>

                                </div>

                            </div>
                            <div class="box-body" style="margin-top: 0px; margin-bottom: 0px; text-align: center;">

                                <div class="col-md-12" style="margin-top: 0px; margin-bottom: 0px; text-align: left;">
                                    <asp:CheckBox ID="chkAgree" runat="server" ValidationGroup="Contact" Text="By checking this box I agree to all the terms and conditions within Terms and Condtions Agreement." /><br />
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ForeColor="Red" ValidationGroup="Contact" ErrorMessage="Please accept the terms and conditions" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                                </div>

                                <div class="col-md-12" style="text-align: center;">
                                    <div class="col-md-3">
                                        <asp:Button ID="btnAgreement" runat="server" Text="View Terms and Conditions Agreement" OnClick="btnAgreement_Click" CssClass="btn btn-success" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnSave" runat="server" Text="Create Owner Account" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="Contact" />

                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnClose_Click" CssClass="btn btn-success " />
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="box-body" style="background-color: #8B9DC3; height: 30px;">
                                &nbsp;
                            </div>
                            <div class="box-body" style="margin: 10px 0;">
                                <div class="col-md-12">
                                    <h6>Initializing Tenant Screening Report Account</h6>
                                    <p>Under the Federal Fair Trade Act Tenant Screening Reports results must go directly between the Owner / Property Manager and potential tenant so we need to you to create a direct account with the report agent. There is no charge for his. You reports will be delivered to your account with Eproperty365. Press button "Create Tenant Screening Account" it will link to their create account. This only needs to be done once. We will send you an Email when this process is complete and you can get Tenant Screening Reports. It may take 24 to 48 hours. You may continue to create your location and unit's information.</p>
                                </div>
                            </div>
                                                       
                            <div class="box-footer">
                                <div class="col-md-3" style="text-align: center;">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnClose_Click" CssClass="btn btn-success " />
                                </div>
                                <div class="col-md-6" style="text-align: center;">
                                    <asp:Button ID="btnScreening" runat="server" Text="Create Tenant Screening Account & Continue" CssClass="btn btn-success" />
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CheckVal() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
        }

    </script>
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

        .col-md-12 {
            padding-left: 0px;
            padding-right: 0px;
        }

        .btncustombox {
            border-top: 3px solid transparent;
            margin-bottom: 0px;
            margin-right: 5px;
            border-radius: 10px 10px 0px 0px;
            background: #ffffff;
            color: #444;
            padding: 6px 15px;
        }
    </style>
    <script type="text/javascript">
        $(".nav nav-tabs  li a").on("click", function () {
            $(".nav nav-tabs  li").find(".active").removeClass("active");
            $(this).addClass("active");
        });
        //$("#addOwnerSystem").click(function () {
        //    $("#ifm").attr("src", "AddOwnerSystem.aspx");
        //});
    </script>
</asp:Content>
