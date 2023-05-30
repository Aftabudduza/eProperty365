<%@ Page Title="EProperty365: Create Tenant Account script for existing tenant" Language="C#" MasterPageFile="~/MasterPage/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ImportTenantProfile.aspx.cs" Inherits="eProperty.Pages.Resident.ImportTenantProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>

        <div class="box">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border CommonHeader col-md-12">
                        <h3 class="box-title">Create Tenant Account script for existing tenant</h3>
                    </div>
                    <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                        <ContentTemplate>--%>
                    <div class="box-body">
                        <div class="col-md-12">
                            <label class="col-sm-12 control-label" style="float: left;">If you fill out a tenant script application and try to view it. It will not allow you to because the first page credit information was never asked because it does not apply to existing tenants. You need to figure out how to bypass.</label>
                        </div>
                    </div>
                    <div class="box-body" style="float: left; width: 100%;">
                        <div class="col-md-6">
                            <label class="col-sm-6 control-label" style="float: left; font-weight: bold;">MANUAL:</label>
                        </div>

                        <div class="col-md-6">
                            <label class="col-sm-6 control-label" style="float: left; color: red;">* denote you must fill</label>
                        </div>

                        <div class="col-md-6">
                            <label for="txtLocation" class="col-sm-3 control-label">*Location ID:</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlLocation" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlLocation" InitialValue="-1" ErrorMessage="Location ID Required" ForeColor="Red" ValidationGroup="Tenant"></asp:RequiredFieldValidator>
                                <%-- <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="txtLocation" ValidationGroup="Tenant"
                                    runat="server" ErrorMessage="Location ID Required" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtUnit" class="col-sm-3 control-label">*Unit ID:</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlUnit" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlUnit" InitialValue="-1" ErrorMessage="Unit ID Required" ForeColor="Red" ValidationGroup="Tenant"></asp:RequiredFieldValidator>
                                <%-- <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" ControlToValidate="txtUnit" ValidationGroup="Tenant"
                                    runat="server" ErrorMessage="Unit ID Required" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="txtFirstName" class="col-sm-3 control-label">*Name; First:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtFirstName" ValidationGroup="Tenant"
                                    runat="server" ErrorMessage="First Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtLastName" class="col-sm-3 control-label">*Last:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtLastName" ValidationGroup="Tenant"
                                    runat="server" ErrorMessage="Last Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtEmail" class="col-sm-3 control-label">*Email Address:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtEmail" ValidationGroup="Tenant"
                                    runat="server" ErrorMessage="Email Address Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ValidationGroup="Tenant"
                                    runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="txtSecurityDeposit" class="col-sm-3 control-label" style="float: left;">*Security deposit held:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtSecurityDeposit" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSecurityDeposit" ErrorMessage="Security Deposit Required" ForeColor="Red" ValidationGroup="Tenant"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtOneMonthRent" class="col-sm-3 control-label" style="float: left;">*One months rent held:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtOneMonthRent" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOneMonthRent" ErrorMessage="One months rent Required" ForeColor="Red" ValidationGroup="Tenant"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtOther" class="col-sm-3 control-label" style="float: left;">*Other amounts held:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtOther" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOther" ErrorMessage="Other amounts Required" ForeColor="Red" ValidationGroup="Tenant"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="txtLease" class="col-sm-3 control-label" style="float: left;">*Lease Signed Date:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtLease" runat="server" CssClass="tDate form-control"></asp:TextBox>
                                <%-- <img alt="" id="Lease" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                    width="20px" height="30px" />
                                <asp:CalendarExtender ID="CalendarExtender17" runat="server" PopupButtonID="Lease"
                                    TargetControlID="txtLease">
                                </asp:CalendarExtender>--%>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLease" ErrorMessage="Lease Signed Date Required" ForeColor="Red" ValidationGroup="Tenant"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtPayment" class="col-sm-3 control-label" style="float: left;">Date of monthly payment due:</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlPayment" CssClass="form-control" runat="server">
                                </asp:DropDownList>

                                <%--<asp:TextBox ID="txtPayment" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                <%-- <img alt="" id="payment" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                    width="20px" height="30px" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="payment"
                                    TargetControlID="txtPayment">
                                </asp:CalendarExtender>--%>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 btnSubmitDiv" style="float: left; padding-left: 0px; width: 100%;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-successNew" OnClientClick="CheckVal()" ValidationGroup="Tenant" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>

                    <div class="box-body" style="float: left; padding-left: 0px;">
                        <div class="col-md-12">
                            <label class="col-sm-12 control-label" style="float: left; font-weight: bold;">IMPORT CSV FILE:</label>
                        </div>
                        <div class="col-md-12">
                            <label class="col-sm-12 control-label" style="float: left; font-size: 16px;">Format:</label>
                        </div>
                        <div class="col-md-12">
                            <label class="col-sm-12 control-label" style="float: left;">*Unit location ID, *Unit ID, *First Name, *Last Name, *email, security deposit amount, amount of months rent held, other amounts held amounts, Lease sign date, monthly pay due date</label>
                        </div>
                        <div class="col-md-12" style="float: left; margin: 10px 0;">
                            <a style="color: deepskyblue; font-weight: bold; padding-left: 15px;" target="_blank" href="../../Uploads/Sample/SampleTenant.csv">Click here to view sample</a>
                        </div>

                        <div class="col-md-12" style="margin-top: 10px;">
                            <div class="col-md-6">
                                <div class="col-sm-12">
                                    <asp:FileUpload ID="uplLogo" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="btn btn-success" OnClick="btnImport_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <label class="col-sm-12 control-label" style="float: left; font-weight: bold;">LIST:</label>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView Width="100%" ID="gvContactList" OnPageIndexChanging="gvContactList_PageIndexChanging"
                                OnSorting="gvContactList_Sorting" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                GridLines="None" AllowPaging="True" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Unit ID" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("UnitId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location ID" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("LocationId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="First Name" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Name" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email Address" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Security Deposit" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("SecurityDeposit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="One Month Rent" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("MonthlyRentHeld") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other Amount" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("OtherAmountHeld") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Lease Sign Date" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("LeaseSignDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly Payment Due" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("MonthlyPayDueDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTenantId" runat="server" Text='<%# Eval("SerialId") %>' Visible="false"></asp:Label>
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
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="col-md-12 ">
                            <div class="col-md-4 btnSubmitDiv">
                                <asp:Button ID="btnCancel" OnClick="btnClose_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />
                            </div>
                            <div class="col-md-6 btnSubmitDiv">
                                <asp:Button ID="btnSend" OnClick="btnSend_Click" runat="server" Text="Send to Existing Tenants" CssClass="btn btn-successNew" />
                            </div>
                        </div>
                    </div>

                </div>


            </div>
        </div>

    </form>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <script type="text/javascript">
        function CheckVal() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
        }

    </script>
</asp:Content>
