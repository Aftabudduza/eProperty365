<%@ Page Title="Add/Edit Owner" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddOwner.aspx.cs" Inherits="eProperty.Pages.Admin.AddOwner" %>

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
                                            <asp:ListItem Value="1">Sole Proprietorship</asp:ListItem>
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
                                    <label for="txtDate" class="col-sm-3 control-label" style="float: left;">When?</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" Text=""></asp:TextBox>
                                    </div>
                                    <img alt="" id="edate" src="../Images/calender.jpg"
                                        width="40px" height="40px" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="edate"
                                        TargetControlID="txtDate">
                                    </asp:CalendarExtender>
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
                                    <label class="col-sm-12 ">
                                        <asp:RadioButtonList runat="server" ID="rdoEIN" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="EIN">EIN #</asp:ListItem>
                                            <asp:ListItem Value="SSN">Social Security #</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </label>
                                </div>
                            </div>
                            <div class="box-body" style="margin-top: 10px;">
                                <div class="col-md-12">
                                    <label class="col-sm-12 control-label" style="color: red;">* When creating an new Account if you do not have Property Manager, ignore the Tab. You can come back later to fill out Contacts and Vendor List Tabs.</label>
                                </div>
                            </div>
                            <div class="box-body" style="margin: 10px 0;">
                                <div class="col-md-6">
                                    <asp:Button ID="btnSave" runat="server" Text="Create Owner Account" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="Contact" />
                                </div>
                            </div>
                            <div class="box-body" style="margin: 10px 0;">
                                <div class="col-md-12">
                                    <h6>Initializing Tenant Screening Report Account</h6>
                                    <p>Under the Federal Fair Trade Act Tenant Screening Reports results must go directly between the Owner / Property Manager and potential tenant so we need to you to create a direct account with the report agent. There is no charge for his. You reports will delivered to your account with Eproperty365. Press button "Create Tenant Screening Account" it will link to their create account. this only needs to be done once. We will send you an Email when this process is complete and you can get Tenant Screening Reports. It may take 24 to 48 hours. You may continue to create your location and units information.</p>
                                </div>
                            </div>

                            <div class="nav-tabs-custom">
                                <ul id="topnav" class="nav nav-tabs">
                                    <li><a href="#System" data-toggle="tab">Account Profile</a></li>
                                    <li><a href="#Users" data-toggle="tab">Users</a></li>
                                    <li><a href="#PropertyManager" data-toggle="tab">Property Manager</a></li>
                                    <li><a href="#Contacts" data-toggle="tab">Contacts</a></li>
                                    <li><a href="#VendorList" data-toggle="tab">Vendor List</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane" id="System">
                                        <div class="box">
                                            <!-- left column -->
                                            <div class="col-md-12">
                                                <!-- general form elements -->
                                                <div class="box box-primary">
                                                    <div class="box-header with-border CommonHeader col-md-12">
                                                        <h3 class="box-title">Owner Account Profile</h3>
                                                    </div>
                                                    <!-- /.box-header -->
                                                    <!-- form start -->
                                                    <div class="box-body">
                                                        <div class="col-md-12">
                                                            <label for="txtWebUrl" class="col-sm-2 control-label">Web Site Url:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtWebUrl" Enabled="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="col-md-12">
                                                            <label class="col-sm-12 control-label">Email & E365 Messenger Communication:</label>
                                                        </div>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="col-md-4" style="float: left;">
                                                            <label class="col-sm-12 control-label">Send To:</label>
                                                        </div>
                                                        <div class="col-md-4" style="float: left;">
                                                            <label for="txtComEmailUser1" class="col-sm-6 control-label">*Email User Name:</label>
                                                            <asp:TextBox ID="txtComEmailUser1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-4" style="float: left;">
                                                            <label for="txtComEmailAddress1" class="col-sm-6 control-label">*Email Address:</label>
                                                            <asp:TextBox ID="txtComEmailAddress1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="box-body">
                                                        <div class="col-md-4" style="float: left;">
                                                            <label class="col-sm-12 control-label">CC:</label>
                                                        </div>
                                                        <div class="col-md-4" style="float: left;">
                                                            <label for="txtComEmailUser2" class="col-sm-6 control-label">*Email User Name:</label>
                                                            <asp:TextBox ID="txtComEmailUser2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-4" style="float: left;">
                                                            <label for="txtComEmailAddress2" class="col-sm-6 control-label">*Email Address:</label>
                                                            <asp:TextBox ID="txtComEmailAddress2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="col-md-4" style="float: left;">
                                                            <label class="col-sm-12 control-label">CC:</label>
                                                        </div>
                                                        <div class="col-md-4" style="float: left;">
                                                            <label for="txtComEmailUser3" class="col-sm-6 control-label">*Email User Name:</label>
                                                            <asp:TextBox ID="txtComEmailUser3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-4" style="float: left;">
                                                            <label for="txtComEmailAddress3" class="col-sm-6 control-label">*Email Address:</label>
                                                            <asp:TextBox ID="txtComEmailAddress3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="col-md-4" style="float: left;">
                                                            <label class="col-sm-12 control-label">CC:</label>
                                                        </div>
                                                        <div class="col-md-4" style="float: left;">
                                                            <label for="txtComEmailUser4" class="col-sm-6 control-label">*Email User Name:</label>
                                                            <asp:TextBox ID="txtComEmailUser4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-4" style="float: left;">
                                                            <label for="txtComEmailAddress4" class="col-sm-6 control-label">*Email Address:</label>
                                                            <asp:TextBox ID="txtComEmailAddress4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="box-body">
                                                        <div class="col-md-12">
                                                            <label class="col-sm-12 control-label">Account Package ID</label>
                                                        </div>
                                                        <div class="col-md-4" style="float: left;">
                                                            <asp:TextBox ID="txtAccountId" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4" style="float: left;">

                                                            <asp:TextBox ID="txtAccountUserEmail" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-4" style="float: left;">
                                                            <asp:TextBox ID="txtAccountUserPassword" Enabled="False" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="box-body">
                                                        <div class="col-md-12">
                                                            <label class="col-sm-12 control-label">Document Management System</label>
                                                        </div>

                                                        <div class="col-md-4" style="float: left;">

                                                            <asp:TextBox ID="txtDocUserEmail" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-4" style="float: left;">
                                                            <asp:TextBox ID="txtDocUserPassword" Enabled="False" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>

                                        <div class="box">
                                            <div class="col-md-12">
                                                <div class="box-header with-border CommonHeader col-md-12">
                                                    <h3 class="box-title">Pricing</h3>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-6">
                                                        <label class="col-sm-6 control-label">
                                                            Cost Residential / Commercial Basic Plan</label>
                                                        <asp:TextBox ID="txtUnitCost" runat="server" CssClass="col-sm-2 form-control"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-sm-4 control-label">
                                                            Number of Units:
                                                        </label>
                                                        <asp:TextBox ID="txtTotalUnit" runat="server" CssClass="col-sm-4 form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <label class="col-sm-12 control-label">
                                                            Per month Per Unit plus processing fees, Includes all basic features. see www.eproperty365.com</label>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="col-sm-12 control-label">
                                                            Unit 1 to 20 will be billed annually at end of 30 days</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-sm-12 control-label">
                                                            Units 21 and up will be billed monthly at end of 30 days,
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <label class="col-sm-12 control-label">
                                                            Rent Processing Fess
                                                        </label>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <asp:CheckBox ID="chkTenantPayFee" runat="server" Text="Tenant pays processing fees" CssClass="col-sm-12 form-check-inline" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:CheckBox ID="chkIncludeFee" runat="server" Text="Owner pays rent processing fees" CssClass="col-sm-12 form-check-inline" />
                                                    </div>
                                                </div>
                                                <div class="box-body" style="visibility: hidden">
                                                    <div class="col-md-6">
                                                        <label class="col-sm-12 control-label">
                                                            Condo Plan $1.00 Per month Per Unit does not include website marketing & ad and application form</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkIncludeCondoFee" runat="server" Text="Include processing fees" CssClass="col-sm-12 form-check-inline" />
                                                    </div>
                                                    <div class="col-md-3">

                                                        <asp:CheckBox ID="chkTenantPayCondoFee" runat="server" Text="Tenant pays processing fees" CssClass="col-sm-12 form-check-inline" />
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-6">
                                                        <label class="col-sm-4 control-label">Credit Card Process Fee:</label>
                                                        <asp:RadioButtonList runat="server" ID="rdoFeeType" CssClass="col-sm-8" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Percentage</asp:ListItem>
                                                            <asp:ListItem Value="2">Flat Amount</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtFeeAmount" runat="server" CssClass="col-sm-4 form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="col-sm-4 control-label">Checking Account Process Fee:</label>
                                                        <asp:RadioButtonList runat="server" ID="rdoAccount" CssClass="col-sm-8" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Percentage</asp:ListItem>
                                                            <asp:ListItem Value="2">Flat Amount</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtCheckAmount" runat="server" CssClass="col-sm-4 form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-6">
                                                        <label class="col-sm-6 control-label">Late Rent Payment Fee: %</label>

                                                        <asp:TextBox ID="txtLateFee" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        <asp:RangeValidator runat="server" Display="Dynamic" ID="rvLate" ControlToValidate="txtLateFee" MinimumValue="5" MaximumValue="100" Type="Integer" ErrorMessage="Minimum value should be 5%" ForeColor="Red"></asp:RangeValidator>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="col-sm-6 control-label">Charge Back Fee: $</label>

                                                        <asp:TextBox ID="txtChargeBackFee" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                        <asp:RangeValidator runat="server" Display="Dynamic" ID="rvChargeBack" ControlToValidate="txtChargeBackFee" MinimumValue="50" MaximumValue="999999" Type="Integer" ErrorMessage="Minimum value should be $50" ForeColor="Red"></asp:RangeValidator>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="col-sm-6 control-label">Application Fee: $</label>

                                                        <asp:TextBox ID="txtApplicationFee" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>

                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="col-sm-6 control-label">Tenant Screening Fee: $</label>

                                                        <asp:TextBox ID="txtScreeningFee" runat="server" CssClass=" col-sm-6 form-control"></asp:TextBox>

                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="box-header with-border CommonHeader col-md-12">
                                                    <h3 class="box-title">Eproperty365 Payment Options</h3>
                                                </div>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                    <ContentTemplate>
                                                        <div class="box-body">
                                                            <div class="col-md-12">
                                                                <label class="col-sm-12 control-label">
                                                                    <h6>* Eproperty365 Monthly & Annual Software Charges:</h6>
                                                                </label>
                                                                <asp:TextBox ID="txtMonthlyCharge" Visible="False" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:CheckBox ID="chkOneTime" runat="server" Text="Bill me first" CssClass="col-sm-12 form-check-inline" />
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:CheckBox ID="chkRecurring" runat="server" Text="I authorize recurring monthly charges" CssClass="col-sm-12 form-check-inline" />
                                                            </div>
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="col-md-12">
                                                                <div class="col-sm-12">
                                                                    <asp:GridView Width="100%" ID="gvCard" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                                        GridLines="None" AllowPaging="True" PageSize="20" Style="float: left; margin-bottom: 10px;">
                                                                        <PagerSettings Position="TopAndBottom" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccountName" runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Last 4 Digit">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLastDigit" runat="server" Text='<%# Eval("LastFourDigitCard") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Account No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccountNo" runat="server" Text='<%# Eval("AccountNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Routing No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRoutingNo" runat="server" Text='<%# Eval("RoutingNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCardId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="btnEditCard" runat="server" Text="Edit" OnClick="btnEditCard_Click"></asp:LinkButton>
                                                                                    <asp:LinkButton ID="btnDeleteCard" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDeleteCard_Click"></asp:LinkButton>
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
                                                        <div class="box-body">
                                                            <div class="col-md-6">
                                                                <asp:RadioButtonList runat="server" ID="rdoCardType" CssClass="col-sm-8" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1">Credit Card</asp:ListItem>
                                                                    <asp:ListItem Value="2">Checking Account</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <label class="col-sm-4 control-label">Name on Account:</label>
                                                                <asp:TextBox ID="txtCardAccountName" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                <label class="col-sm-4 control-label">Card Address:</label>
                                                                <asp:TextBox ID="txtCardAddress" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                <label class="col-sm-4 control-label">City:</label>
                                                                <asp:TextBox ID="txtCardCity" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                <label class="col-sm-4 control-label">State:</label>
                                                                <asp:DropDownList ID="DropDownList1" CssClass="col-sm-6 form-control" runat="server">
                                                                </asp:DropDownList>
                                                                <label class="col-sm-4 control-label">Zip:</label>
                                                                <asp:TextBox ID="txtCardZip" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                <label class="col-sm-4 control-label">Credit Card Number:</label>
                                                                <asp:TextBox ID="txtCardNumber" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                <label class="col-sm-4 control-label">CVS Number:</label>
                                                                <asp:TextBox ID="txtCVS" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                <label class="col-sm-4" style="float: left;">Expiry Date:</label>
                                                                <asp:DropDownList ID="ddlMonth" CssClass="col-sm-3 form-control" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlYear" CssClass="col-sm-3 form-control" runat="server">
                                                                </asp:DropDownList>

                                                            </div>

                                                            <div class="col-md-6">
                                                                <label class="col-sm-12 control-label">Checking Account</label>
                                                                <div class="col-sm-12">
                                                                    <asp:TextBox ID="txtRoutingNo" runat="server" CssClass="col-sm-12 form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-12 control-label">Routing number (2nd from left):</label>
                                                                <div class="col-sm-12">
                                                                    <asp:TextBox ID="txtRoutingNo2" runat="server" CssClass="col-sm-12 form-control"></asp:TextBox>
                                                                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtRoutingNo2" ForeColor="Red"
                                                                        ControlToCompare="txtRoutingNo" Display="Dynamic" runat="server" ErrorMessage="Routing number Does Not Match"></asp:CompareValidator>
                                                                </div>
                                                                <label class="col-sm-12 control-label">Re-enter:</label>

                                                                <div class="col-sm-12">
                                                                    <asp:TextBox ID="txtCheckingAccount" runat="server" CssClass="col-sm-12 form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-12 control-label">Account number (last number from left):</label>
                                                                <div class="col-sm-12">
                                                                    <asp:TextBox ID="txtCheckingAccount2" runat="server" CssClass="col-sm-12 form-control"></asp:TextBox>
                                                                    <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtCheckingAccount2" ForeColor="Red"
                                                                        ControlToCompare="txtCheckingAccount" Display="Dynamic" runat="server" ErrorMessage="Account number Does Not Match"></asp:CompareValidator>
                                                                </div>
                                                                <label class="col-sm-12 control-label">Re-enter:</label>

                                                            </div>
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="col-md-12">
                                                                <asp:Button ID="btnAddCard" runat="server" Text="Add" OnClick="btnAddCard_Click" CssClass="btn btn-success " />
                                                            </div>
                                                            <div class="col-md-12">
                                                                <label class="col-sm-12 control-label">
                                                                    I am authorize signed on the above information and authorize the Landlord or their agent's
                                                                    to debit my Credit Card or Checking account for the above amount.</label>
                                                            </div>
                                                        </div>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="col-md-12">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                    <ContentTemplate>
                                                        <div class="box-header with-border CommonHeader col-md-12">
                                                            <h3 class="box-title">Pull Down Information</h3>
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="col-md-4" style="float: left; padding: 0px;">
                                                                <div class="col-sm-12">
                                                                    <asp:GridView Width="100%" ID="gvContactTypeList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                                        GridLines="None" AllowPaging="True" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                                                        <PagerSettings Position="TopAndBottom" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Type Of Contact" SortExpression="Data">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesp2" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblContactId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="btnEditContact" runat="server" Text="Edit" OnClick="btnEditContact_Click"></asp:LinkButton>
                                                                                    <asp:LinkButton ID="btnDeleteContact" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDeleteContact_Click"></asp:LinkButton>
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
                                                                <label class="col-sm-3 control-label">Type of Contact:</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtTypeofContact" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Button ID="btnAddTypeOfContact" runat="server" Text="Add" OnClick="btnAddTypeOfContact_Click" CssClass="btn btn-success col-sm-2" />
                                                            </div>
                                                            <div class="col-md-4" style="float: left; padding: 0px;">
                                                                <div class="col-sm-12">
                                                                    <asp:GridView Width="100%" ID="gvLedger" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                                        GridLines="None" AllowPaging="True" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                                                        <PagerSettings Position="TopAndBottom" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Ledger Code" SortExpression="Data">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Ledger Name" SortExpression="Data">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesp3" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLedgerId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="btnEditLedger" runat="server" Text="Edit" OnClick="btnEditLedger_Click"></asp:LinkButton>
                                                                                    <asp:LinkButton ID="btnDeleteLedger" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDeleteLedger_Click"></asp:LinkButton>
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
                                                                <label class="col-sm-6 control-label">Ledger Code:</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtLedger" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-6 control-label">Ledger Name:</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtLedgerName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <asp:Button ID="btnLedger" runat="server" Text="Add" OnClick="btnLedger_Click" Style="float: left; margin-top: 5px;" CssClass="btn btn-success col-sm-3" />
                                                            </div>
                                                            <div class="col-md-4" style="float: left; padding: 0px;">
                                                                <div class="col-sm-12">
                                                                    <asp:GridView Width="100%" ID="gvPaymentType" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                                        GridLines="None" AllowPaging="True" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                                                        <PagerSettings Position="TopAndBottom" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Pay By" SortExpression="Data">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesp1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPaymentTypeId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="btnEditPaymentType" runat="server" Text="Edit" OnClick="btnEditPaymentType_Click"></asp:LinkButton>
                                                                                    <asp:LinkButton ID="btnDeletePaymentType" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDeletePaymentType_Click"></asp:LinkButton>
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
                                                                <label class="col-sm-4 control-label">Pay Type:</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtPaymentType" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Button ID="btnPaymentType" runat="server" Text="Add" OnClick="btnPaymentType_Click" CssClass="btn btn-success col-sm-2" />
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                        </div>
                                        <div class="box-footer">
                                            <div class="col-md-6">
                                                <asp:Button ID="btnSaveSystem" runat="server" Text="Save" OnClientClick="CheckVal()" OnClick="btnSubmitSystem_Click" CssClass="btn btn-successNew" ValidationGroup="System" />

                                                <asp:Button ID="btnCancelSystem" runat="server" Text="Cancel" OnClick="btnCloseSystem_Click" CssClass="btn btn-success " />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="tab-pane" id="Users">
                                        <div class="box">
                                            <div class="col-md-12">
                                                <div class="box-header with-border CommonHeader col-md-12">
                                                    <h3 class="box-title">Create Users</h3>
                                                </div>
                                                <div class="box-body">
                                                    <asp:GridView Width="100%" ID="gvUserList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gvUserList_PageIndexChanging"
                                                        OnSorting="gvUserList_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
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


                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="btnEditUser" runat="server" Text="Edit" OnClick="btnEditUser_Click"></asp:LinkButton>
                                                                    <asp:LinkButton ID="btnDeleteUser" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDeleteUser_Click"></asp:LinkButton>
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
                                        <div class="box">
                                            <div class="col-md-12">
                                                <div class="box box-primary">
                                                    <div class="box-body">
                                                        <div class="col-md-6">
                                                            <label for="txtUserName" class="col-sm-3 control-label">*User Name:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" ControlToValidate="txtUserName" ValidationGroup="user"
                                                                    runat="server" ErrorMessage="Contact Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtUserTitle" class="col-sm-3 control-label">User Title:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtUserTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtUserEmail" class="col-sm-3 control-label">*Email Address:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtUserEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtUserEmail" ValidationGroup="user"
                                                                    runat="server" ErrorMessage="Email Address Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" ValidationGroup="Contact"
                                                                    runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtUserEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtUserNumber" class="col-sm-3 control-label">*Phone Number:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtUserNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtUserNumber" ValidationGroup="user"
                                                                    Display="Dynamic" runat="server" ErrorMessage="Phone Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="ddlType" class="col-sm-3 control-label" style="float: left;">*Security Level:</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlUserLevel" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Value="2 and Up">2 and Up</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label class="col-sm-4 ">
                                                                <asp:CheckBox runat="server" ID="chkUserLocation" Text="By Location" />
                                                            </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="col-sm-4">
                                                                <asp:CheckBox runat="server" Checked="true" ID="chkAdmin" Text="Can not to Admin" Style="float: left; padding-right: 13px;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- /.box-body -->
                                                    <div class="box-footer">
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnSaveUser" runat="server" Text="Add" OnClientClick="CheckVal()" OnClick="btnSubmitUser_Click" CssClass="btn btn-successNew" ValidationGroup="user" />

                                                            <asp:Button ID="btnCancelUser" runat="server" Text="Cancel" OnClick="btnCloseUser_Click" CssClass="btn btn-success " />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="PropertyManager">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel9">
                                            <ContentTemplate>
                                                <div class="box">
                                                    <!-- left column -->
                                                    <div class="col-md-12">
                                                        <!-- general form elements -->
                                                        <div class="box box-primary">
                                                            <div class="box-header with-border CommonHeader col-md-12">
                                                                <h3 class="box-title">Create / Change Property Manager Profile</h3>
                                                            </div>
                                                            <!-- /.box-header -->
                                                            <!-- form start -->
                                                            <div class="box-body">
                                                                <div class="col-md-6">
                                                                    <label for="txtFirstNameP" class="col-sm-3 control-label">*First Name:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtFirstNameP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="txtFirstNameP" ValidationGroup="Contact"
                                                                            runat="server" ErrorMessage="First Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="txtLastNameP" class="col-sm-3 control-label">*Last Name:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtLastNameP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="txtLastNameP" ValidationGroup="Contact"
                                                                            Display="Dynamic" runat="server" ErrorMessage="Last Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtCompanyNameP" class="col-sm-3 control-label">*Company Name:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtCompanyNameP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="txtCompanyNameP" ValidationGroup="Contact"
                                                                            Display="Dynamic" runat="server" ErrorMessage="Company Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="box-body">
                                                                <div class="col-md-6">
                                                                    <label for="txtAddressP" class="col-sm-3 control-label">*Address:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtAddressP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" Display="Dynamic" ControlToValidate="txtAddressP" ValidationGroup="Contact"
                                                                            runat="server" ErrorMessage="Address Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="txtAddress1P" class="col-sm-3 control-label">Address1:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtAddress1P" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="ddlCountryP" class="col-sm-3 control-label" style="float: left;">*Country:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlCountryP" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ID="requsertypeddlCountryP" runat="server" ControlToValidate="ddlCountryP" InitialValue="-1" ErrorMessage="Please select Country" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="txtRegionP" class="col-sm-3 control-label">Region:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtRegionP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="box-body">
                                                                <div class="col-md-6">
                                                                    <label for="ddlStateP" class="col-sm-3 control-label" style="float: left;">State:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlStateP" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtCityP" class="col-sm-3 control-label">*City:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtCityP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" Display="Dynamic" ControlToValidate="txtCityP" ValidationGroup="Contact"
                                                                            runat="server" ErrorMessage="City Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtZipP" class="col-sm-3 control-label">*Zip Code:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtZipP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" Display="Dynamic" ControlToValidate="txtZipP" ValidationGroup="Contact"
                                                                            runat="server" ErrorMessage="Zip Code Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="txtEINP" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">EIN Number or Social Security Number:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtEINP" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label class="col-sm-12 ">
                                                                        <asp:RadioButtonList runat="server" ID="rdoEINP" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="EIN">EIN #</asp:ListItem>
                                                                            <asp:ListItem Value="SSN">Social Security #</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </label>
                                                                </div>
                                                            </div>
                                                            <div class="box-footer">
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="btnSubmitPropertyManager" runat="server" Text="Save" OnClientClick="CheckVal()" OnClick="btnSubmitPropertyManager_Click" CssClass="btn btn-successNew" ValidationGroup="Contact" />

                                                                    <asp:Button ID="btnCancelPropertyManager" runat="server" Text="Cancel" OnClick="btnCancelPropertyManager_Click" CssClass="btn btn-success " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane" id="Contacts">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                            <ContentTemplate>
                                                <div class="box">
                                                    <div class="col-md-12">
                                                        <div class="box-header with-border CommonHeader col-md-12">
                                                            <h3 class="box-title">Create / Change Contact Information</h3>
                                                        </div>

                                                        <div class="box-body">
                                                            <asp:GridView Width="100%" ID="gvContactList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                                GridLines="None" AllowPaging="True" OnPageIndexChanging="gvContactList_PageIndexChanging"
                                                                OnSorting="gvContactList_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
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
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box">
                                                    <div class="col-md-12">
                                                        <!-- general form elements -->
                                                        <div class="box box-primary">
                                                            <div class="box-body">
                                                                <div class="col-md-6">
                                                                    <label for="txtContactName" class="col-sm-6 control-label">*Contact Name:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" ControlToValidate="txtContactName" ValidationGroup="Contact"
                                                                            runat="server" ErrorMessage="Contact Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtContactTitle" class="col-sm-6 control-label">Contact Title:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtContactTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="ddlType" class="col-sm-6 control-label" style="float: left;">*Type of Contact:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server">
                                                                            <asp:ListItem Value="-1">Select Type of Contact</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ID="Contacttype" runat="server" ControlToValidate="ddlType" InitialValue="-1" ErrorMessage="Please select Type of Contact" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtNumber" class="col-sm-6 control-label">*Number:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtNumber" ValidationGroup="Contact"
                                                                            Display="Dynamic" runat="server" ErrorMessage="Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="box-body">
                                                                <div class="col-md-6">
                                                                    <label for="txtEmail" class="col-sm-6 control-label">*Email Address:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtEmail" ValidationGroup="Contact"
                                                                            runat="server" ErrorMessage="Email Address Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ValidationGroup="Contact"
                                                                            runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="col-sm-6">
                                                                        <asp:CheckBox runat="server" Checked="false" ID="chkEmergency" Text="Emergency contact" Style="float: left; padding-right: 13px;" />
                                                                    </div>
                                                                    <div id="EmergencyDiv" runat="server" class="col-sm-6" style="display: none;">
                                                                        <asp:TextBox ID="txtEmergency" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label class="col-sm-4 ">
                                                                        <asp:CheckBox runat="server" ID="chkLocation" Text="By Location" />
                                                                    </label>

                                                                    <label class="col-sm-8 ">
                                                                        <asp:CheckBox runat="server" ID="chkSend" Text="Send all email communications" />
                                                                    </label>
                                                                </div>
                                                            </div>
                                                            <!-- /.box-body -->
                                                            <div class="box-footer">
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="btnSubmitContact" runat="server" Text="Add Contact" OnClientClick="CheckVal()" OnClick="btnSubmitContact_Click" CssClass="btn btn-successNew" ValidationGroup="Contact" />

                                                                    <asp:Button ID="btnCloseContact" runat="server" Text="Cancel" OnClick="btnCloseContact_Click" CssClass="btn btn-success " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane" id="VendorList">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                            <ContentTemplate>
                                                <div class="box">
                                                    <div class="col-md-12">
                                                        <div class="box-header with-border CommonHeader col-md-12">
                                                            <h3 class="box-title">Vendor List</h3>
                                                        </div>
                                                        <div class="box-body">
                                                            <asp:GridView Width="100%" ID="gvVendorList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                                GridLines="None" AllowPaging="True" OnPageIndexChanging="gvVendorList_PageIndexChanging"
                                                                OnSorting="gvVendorList_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                                                <PagerSettings Position="TopAndBottom" />
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Type Of Work" SortExpression="Data">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTypeOfWork" runat="server" Text='<%# Eval("TypeOfWork") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Contract Name" SortExpression="Data">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("ContractName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Company" SortExpression="Data">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="Email Address" SortExpression="Data">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Phone Number" SortExpression="Data">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVendorId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                            <asp:LinkButton ID="btnEditVendor" runat="server" Text="Edit" OnClick="btnEditVendor_Click"></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnDeleteVendor" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDeleteVendor_Click"></asp:LinkButton>
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
                                                <div class="box">
                                                    <!-- left column -->
                                                    <div class="col-md-12">
                                                        <!-- general form elements -->
                                                        <div class="box box-primary">
                                                            <!-- /.box-header -->
                                                            <!-- form start -->
                                                            <div class="box-body">
                                                                <div class="col-md-6">
                                                                    <label for="txtType" class="col-sm-3 control-label">Type of Work:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtType" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="txtContactNameVendor" class="col-sm-3 control-label">*Contact Name:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtContactNameVendor" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" ControlToValidate="txtContactNameVendor" ValidationGroup="Vendor"
                                                                            runat="server" ErrorMessage="Contact Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtTitle" class="col-sm-3 control-label">Title:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="txtCompanyNameV" class="col-sm-3 control-label">*Company Name:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtCompanyNameV" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtCompanyNameV" ValidationGroup="Vendor"
                                                                            Display="Dynamic" runat="server" ErrorMessage="Company Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtAddressV" class="col-sm-3 control-label">*Address:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtAddressV" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" Display="Dynamic" ControlToValidate="txtAddressV" ValidationGroup="Vendor"
                                                                            runat="server" ErrorMessage="Address Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="txtAddress1V" class="col-sm-3 control-label">Address1:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtAddress1V" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <label for="txtRegionV" class="col-sm-3 control-label">Region:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtRegionV" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="ddlStateVendor" class="col-sm-3 control-label" style="float: left;">State:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlStateVendor" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtCityV" class="col-sm-3 control-label">*City:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtCityV" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" Display="Dynamic" ControlToValidate="txtCityV" ValidationGroup="Vendor"
                                                                            runat="server" ErrorMessage="City Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtZipV" class="col-sm-3 control-label">*Zip Code:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtZipV" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" Display="Dynamic" ControlToValidate="txtZipV" ValidationGroup="Vendor"
                                                                            runat="server" ErrorMessage="Zip Code Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtEmailVendor" class="col-sm-3 control-label">*Email Address:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtEmailVendor" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtEmailVendor" ValidationGroup="Vendor"
                                                                            runat="server" ErrorMessage="Email Address Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator3" ValidationGroup="Contact"
                                                                            runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmailVendor" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtNumberVendor" class="col-sm-3 control-label">*Phone Number:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtNumberVendor" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="txtNumberVendor" ValidationGroup="Vendor"
                                                                            Display="Dynamic" runat="server" ErrorMessage="Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtBIN" class="col-sm-3 control-label">Business License Number:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtBIN" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="txtBIN" ValidationGroup="Vendor"
                                                                            Display="Dynamic" runat="server" ErrorMessage="License Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtApprovedBy" class="col-sm-3 control-label">Approved By:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtApproveDate" class="col-sm-3 control-label">Approved Date:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtApproveDate" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="approveddate1" class="col-sm-3" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="20px" height="30px" />
                                                                        <asp:CalendarExtender ID="CalendarExtender17" runat="server" PopupButtonID="approveddate1"
                                                                            TargetControlID="txtApproveDate">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:CheckBox ID="chkSendEmail" runat="server" Text="Send Email Request for W9 Form" CssClass="col-sm-6 form-check-inline" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="box-body">
                                                            <table class="table table-bordered table-responsive">
                                                                <tr>
                                                                    <td>Documents Description
                                                                    </td>
                                                                    <td>Value
                                                                    </td>
                                                                    <td>Effect Date
                                                                    </td>
                                                                    <td>End Date
                                                                    </td>
                                                                    <td>Review By
                                                                    </td>
                                                                    <td>Review Date
                                                                    </td>
                                                                    <td>Document ID
                                                                    </td>
                                                                    <td>Filed Date
                                                                    </td>
                                                                    <td>Person Filed
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDocument1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtValue1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEffectDate1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="effectdate1" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="effectdate1"
                                                                            TargetControlID="txtEffectDate1">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEndDate1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="enddate1" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="enddate1"
                                                                            TargetControlID="txtEndDate1">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReviewBy1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReviewDate1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="rdate1" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="rdate1"
                                                                            TargetControlID="txtReviewDate1">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDocumentID1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFiledDate1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="fdate1" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="fdate1"
                                                                            TargetControlID="txtFiledDate1">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPersonFiled1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDocument2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtValue2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEffectDate2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="effectdate2" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="effectdate2"
                                                                            TargetControlID="txtEffectDate2">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEndDate2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="enddate2" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="enddate2"
                                                                            TargetControlID="txtEndDate2">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReviewBy2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReviewDate2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="rdate2" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="rdate2"
                                                                            TargetControlID="txtReviewDate2">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDocumentID2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFiledDate2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="fdate2" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="fdate2"
                                                                            TargetControlID="txtFiledDate2">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPersonFiled2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDocument3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtValue3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEffectDate3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="effectdate3" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender10" runat="server" PopupButtonID="effectdate3"
                                                                            TargetControlID="txtEffectDate3">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEndDate3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="enddate3" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender11" runat="server" PopupButtonID="enddate3"
                                                                            TargetControlID="txtEndDate3">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReviewBy3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReviewDate3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="rdate3" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender12" runat="server" PopupButtonID="rdate3"
                                                                            TargetControlID="txtReviewDate3">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDocumentID3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFiledDate3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="fdate3" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender13" runat="server" PopupButtonID="fdate3"
                                                                            TargetControlID="txtFiledDate3">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPersonFiled3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDocument4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtValue4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEffectDate4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="effectdate4" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender14" runat="server" PopupButtonID="effectdate4"
                                                                            TargetControlID="txtEffectDate4">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEndDate4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="enddate4" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender15" runat="server" PopupButtonID="enddate4"
                                                                            TargetControlID="txtEndDate4">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReviewBy4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReviewDate4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="rdate4" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender16" runat="server" PopupButtonID="rdate4"
                                                                            TargetControlID="txtReviewDate4">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDocumentID4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFiledDate4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                                        <img alt="" id="fdate4" src="http://www.eproperty365.net/Images/calender.jpg"
                                                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                                                        <asp:CalendarExtender ID="CalendarExtender18" runat="server" PopupButtonID="fdate4"
                                                                            TargetControlID="txtFiledDate4">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPersonFiled4" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <!-- /.box-body -->
                                                        <div class="box-footer">
                                                            <div class="col-md-6">
                                                                <asp:Button ID="btnSaveVendor" runat="server" Text="Add" OnClientClick="CheckVal()" OnClick="btnSubmitVendor_Click" CssClass="btn btn-success" ValidationGroup="Vendor" />
                                                                <asp:Button ID="btnSaveNew" runat="server" Text="Save Vendor List" OnClientClick="CheckVal()" OnClick="btnSubmitVendor_Click" CssClass="btn btn-successNew" ValidationGroup="Vendor" />
                                                                <asp:Button ID="btnCancelVendor" runat="server" Text="Cancel" OnClick="btnCloseVendor_Click" CssClass="btn btn-success " />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                            <!-- /.box-body -->
                            <div class="box-footer">
                                <div class="col-md-6">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnClose_Click" CssClass="btn btn-success " />
                                    <asp:Button ID="btnScreening" runat="server" Text="Create Tenant Screening Account & Continue" CssClass="btn btn-success" />
                                </div>
                            </div>
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
    </style>
    <script type="text/javascript">
        $(".nav nav-tabs  li a").on("click", function () {
            $(".nav nav-tabs  li").find(".active").removeClass("active");
            $(this).addClass("active");
        });
    </script>
</asp:Content>
