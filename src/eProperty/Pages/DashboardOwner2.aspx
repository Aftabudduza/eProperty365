<%@ Page Title="EProperty365: Owner Dashboard" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DashboardOwner2.aspx.cs" Inherits="eProperty.Pages.DashboardOwner2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>
        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                    <ContentTemplate>
                        <div class="box" runat="server" id="divChart">
                            <div class="box-body">
                            </div>
                        </div>
                        <div class="box" runat="server" id="divLinkMenu">
                            <div class="box-body">
                                <div class="col-md-12" style="float: left;">
                                    <p><strong>To Start:</strong> <b>Click on upper right "Help" link and then click on "Get Started" link and review the step that you need to do to to activate an account</b></p>
                                </div>
                                <div class="col-md-6" style="float: left;">
                                    <ul>
                                        <%--<li id="litop1" runat="server"><a href="../Pages/Admin/AddOwnerSystem.aspx">1) Setup Owner Account Profile</a> </li>--%>
                                        <li id="litop2" runat="server"><a href="../Pages/Admin/AddOwner.aspx">1) Setup Owners Profile</a></li>
                                        <li id="litop3" runat="server"><a href="../Pages/Admin/AddLocation.aspx">2) Setup Property Location Profile</a></li>
                                        <li id="litop4" runat="server"><a href="../Pages/Admin/AddResidentialUnit.aspx">3) Setup Property Unit Profile</a> </li>
                                    </ul>
                                </div>
                                <div class="col-md-6" style="float: left;">
                                    <ul>
                                        <li id="litop5" runat="server"><a href="../Pages/Admin/AddResidentialUnit.aspx">4) Setup Document Management System</a></li>
                                        <li id="litop6" runat="server"><a href="../Pages/Account/AddChartofAccount.aspx">5) Setup Accounting System Profile</a></li>
                                        <li id="litop7" runat="server"><a href="../Pages/Resident/ImportTenantProfile.aspx">6) Setup Existing Tenants Profile Import</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="box">
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
                        <div class="box" runat="server" id="divBottom" >
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

                                                <%--<asp:TextBox ID="txtPayment" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                <%-- <img alt="" id="payment" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                                    width="20px" height="30px" />--%>
                                                <%-- <asp:CalendarExtender ID="CalendarExtender18" runat="server"
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


    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #MainContent_divLinkMenu ul {
            list-style-type: none;
            /*width: 30%;*/
            margin-left: 0px;
            margin-right: 2px;
        }

        #MainContent_divLinkMenu colorRed {
            color: red;
        }

        #MainContent_divLinkMenu colorGreen {
            color: green;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function (parameters) {
            //$(".tDate").datepicker({
            //    dateFormat: "mm-dd-yy",
            //    changeYear: true,
            //    changeMonth: true
            //});
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



    </script>
</asp:Content>
