<%@ Page Title="EProperty365: Add/Change Location" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddLocation.aspx.cs" Inherits="eProperty.Pages.Admin.AddLocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>

        <div class="box">
            <div class="box-body" style="float: left; padding-left: 0px;">
                <div class="col-md-12">
                    <label class="col-sm-12 control-label" style="float: left; font-weight: bold;">IMPORT/Export CSV FILE:</label>
                </div>
                <div class="col-md-12">
                    <label class="col-sm-12 control-label" style="float: left; font-size: 16px;">Format:</label>
                </div>
                <div class="col-md-12">
                    <label class="col-sm-12 control-label" style="float: left;">LocationType, *Address, Address1, Region, *Country, State, *City, *Zipcode, *OwnerId, *PropertyManagerId, *Location Name, *SchoolDistract, Longitude, Latitude, Units, Lots, TotalSize, DatePurchased, Cost, SchoolTax, PropertyTax, Comment, IsBackgroundCheck, IsCondoFee, IsRentMonthly, Logo</label>
                </div>
                <div class="col-md-12" style="float: left; margin: 10px 0;">
                    <a style="color:deepskyblue; font-weight:bold; padding-left:15px;" target="_blank" href="../../Uploads/Sample/SampleLocation.csv">Click here to view sample</a>
                </div>

                <div class="col-md-12" style="margin-top: 10px;">
                    <div class="col-md-6">
                        <div class="col-sm-12">
                            <asp:FileUpload ID="uplLogo" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="btn btn-success" OnClick="btnImport_Click" />
                        <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-success" OnClick="btnExport_Click" />
                    </div>
                </div>
            </div>
            
            <div class="box-body">
                <div class="form-group">
                    <div class="col-md-8">
                        <h3 class="box-title" id="lblPropertyLocation" runat="server"></h3>
                    </div>
                    <div class="col-md-6">
                        <label for="ddlOwnerTop" class="col-sm-6 control-label" style="float: left;">*Owner ID:</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlOwnerTop" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlOwnerTop_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label for="ddlPropertyManagerTop" class="col-sm-6 control-label" style="float: left;">Property Manager ID:</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlPropertyManagerTop" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-success " />
                    </div>
                </div>
            </div>
            <div class="box-body">
                <form class="vertical-form">
                    <fieldset>
                        <asp:GridView Width="100%" ID="gvLocationList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                            GridLines="None" AllowPaging="True" OnPageIndexChanging="gvLocationList_PageIndexChanging"
                            OnSorting="gvLocationList_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                            <PagerSettings Position="TopAndBottom" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="Location ID" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTypeOfWork" runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location Name" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("LocationName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" SortExpression="Data">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDelete_Click"></asp:LinkButton>
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
                </form>
            </div>

            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border CommonHeader col-md-12">
                        <h3 class="box-title" id="lblHeadline" runat="server">Create Location</h3>
                    </div>
                    <div class="box-body" style="float: left;">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <label for="lblId" class="col-sm-6 control-label">Location ID:</label>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblId" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <label class="col-sm-12 ">
                                    <asp:RadioButtonList runat="server" ID="rdoLocationType" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">Residential  Rental</asp:ListItem>
                                        <asp:ListItem Value="2">Commercial</asp:ListItem>
                                        <asp:ListItem Value="3">Condo</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="radioValidator" runat="server" ControlToValidate="rdoLocationType" Display="Dynamic" ForeColor="Red"> * </asp:RequiredFieldValidator>
                                </label>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-8" style="float: left;">
                                <div class="col-md-6">
                                    <label for="txtAddress" class="col-sm-3 control-label">*Address:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtAddress" ValidationGroup="Contact"
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
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="requsertype" runat="server" ControlToValidate="ddlCountry" InitialValue="-1" ErrorMessage="Please select Country" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtRegion" class="col-sm-3 control-label">Region:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRegion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtCity" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="City Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtZip" class="col-sm-3 control-label">*Zip Code:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="txtZip" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Zip Code Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtLocationName" class="col-sm-3 control-label">*Location Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtLocationName" ValidationGroup="Contact"
                                            Display="Dynamic" runat="server" ErrorMessage="Location Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label class="col-sm-3 control-label">Document Management System:</label>
                                    <asp:FileUpload ID="uplProduct" runat="server" CssClass="col-sm-6 form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label for="txtSchoolDistract" class="col-sm-3 control-label">*School Distract:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtSchoolDistract" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSchoolDistract" ValidationGroup="Contact"
                                            Display="Dynamic" runat="server" ErrorMessage="School Distract Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="ddlOwner" class="col-sm-3 control-label" style="float: left;">*Owner:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlOwner" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlOwner_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOwner" InitialValue="-1" ErrorMessage="Select owner" ForeColor="Red" ValidationGroup="ContactSearch"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="ddlPropertyManager" class="col-sm-3 control-label" style="float: left;">Property Manager:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlPropertyManager" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtLatitude" class="col-sm-3 control-label">Geo Lat:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtLongitute" class="col-sm-3 control-label">Geo Log:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtLongitute" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtUnits" class="col-sm-3 control-label">No. Units:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtUnits" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtLots" class="col-sm-3 control-label">No. Lots:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtLots" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtSize" class="col-sm-3 control-label">Total Size:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtSize" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtCost" class="col-sm-3 control-label">Costs:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCost" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtPurchasedDate" class="col-sm-3 control-label">Date Purchased:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPurchasedDate" runat="server" CssClass=" form-control tDate"></asp:TextBox>
                                       <%-- <img alt="" id="PurchasedDate" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="30px" height="30px" />
                                        <asp:CalendarExtender ID="CalendarExtender17" runat="server" PopupButtonID="PurchasedDate"
                                            TargetControlID="txtPurchasedDate">
                                        </asp:CalendarExtender>--%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtPropertyTax" class="col-sm-3 control-label">Property Taxes:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPropertyTax" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtSchoolTax" class="col-sm-3 control-label">School Taxes:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtSchoolTax" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4" style="float: left;">
                                <div class="col-sm-12">
                                    <asp:Image ID="imgLocation" runat="server" Width="80%" />
                                </div>
                            </div>
                        </div>

                        <div class="col-md-9" style="float: left;">
                            <label for="txtComment" class="col-sm-3 control-label">Special Comments:</label>
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-9" style="float: left;">
                            <asp:CheckBox ID="chkSecurity" runat="server" Text="Must have Background Security Check..." CssClass="col-sm-12 form-check-inline" />
                        </div>
                        <div class="col-md-9" style="float: left;">
                            <asp:CheckBox ID="chkCondo" runat="server" Text="Bill Condo Fee Monthly via Email" CssClass="col-sm-12 form-check-inline" />
                        </div>
                        <div class="col-md-9" style="float: left;">
                            <asp:CheckBox ID="chkRent" runat="server" Text="Rent Monthly via Email" CssClass="col-sm-12 form-check-inline" />
                        </div>


                    </div>
                </div>
                <div class="nav-tabs-custom">
                    <ul id="topnav" class="nav nav-tabs">
                        <%-- <li><a href="AddContact.aspx">Contacts</a></li>
                        <li><a href="AddUser.aspx">Users</a></li>
                        <li><a href="AddCAMExpense.aspx">Cam Expenses</a></li>
                        <li><a href="AddVendor.aspx">Vendor List</a></li>--%>

                        <li>
                            <asp:Button ID="btnAddContact" runat="server" CssClass="btncustombox" Text="Contacts" OnClick="btnAddContact_Click" /></li>
                        <li>
                            <asp:Button ID="btnAddUser" runat="server" CssClass="btncustombox" Text="Users" OnClick="btnAddUser_Click" /></li>
                        <%-- <li>
                            <asp:Button ID="btnAddCAMExpense" runat="server" CssClass="btncustombox" Text="Cam Expenses" OnClick="btnAddCAMExpense_Click" /></li>--%>
                        <li>
                            <asp:Button ID="btnAddVendor" runat="server" CssClass="btncustombox" Text="Vendor List" OnClick="btnAddVendor_Click" /></li>

                    </ul>

                </div>

                <!-- /.box-body -->
                <div class="box-footer">
                    <div class="col-md-12">
                        <div class="col-md-4 btnSubmitDiv">
                            <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn btn-success " />
                        </div>
                        <div class="col-md-4 btnSubmitDiv">
                            <asp:Button ID="btnSave" runat="server" Text="Add" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-successNew" ValidationGroup="Contact" />
                        </div>

                        <div class="col-md-4 btnSubmitDiv">
                            <asp:Button ID="btnCancel" runat="server" Text="Clear Form" OnClick="btnClose_Click" CssClass="btn btn-success " />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
    <style type="text/css">
        .input-validation-error {
            border: 1px solid #ff0000;
            background-color: #ffeeee !important;
        }

        .field-validation-error {
            color: #ff0000 !important;
        }

        .table th, .table td {
            padding: 5px 2px;
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

        .col-md-4 {
            position: unset !important;
            padding-right: 0px !important;
            padding-left: 0 !important;
        }

        label {
            margin-bottom: 0px;
        }
        /*.box-header.with-border {
            border-bottom: none;
            text-align: center;
        }

        .box {
            position: relative;
            background: none;
            border-top: none;
            margin-bottom: 20px;
            width: 100%;
            box-shadow: none;
        }*/

        .nav-tabs-custom {
            /*background: none;*/
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
    <style type="text/css">
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

        fieldset {
            float: left;
            width: 95%;
        }
    </style>
    <script type="text/javascript">
        $(".nav nav-tabs  li a").on("click", function () {
            $(".nav nav-tabs  li").find(".active").removeClass("active");
            $(this).addClass("active");
        });
    </script>
</asp:Content>
