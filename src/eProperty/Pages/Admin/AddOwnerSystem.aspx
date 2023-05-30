<%@ Page Title="EProperty365: Add/Edit Owner System Info" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddOwnerSystem.aspx.cs" Inherits="eProperty.Pages.Admin.AddOwnerSystem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanel8">
            <ContentTemplate>
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
                            <div class="box-body" style="display:none;">
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtComEmailUser1" ValidationGroup="System"
                                        runat="server" ErrorMessage="Email User Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4" style="float: left;">
                                    <label for="txtComEmailAddress1" class="col-sm-6 control-label">*Email Address:</label>
                                    <asp:TextBox ID="txtComEmailAddress1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtComEmailAddress1" ValidationGroup="System"
                                        runat="server" ErrorMessage="Email Address Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ValidationGroup="System" ForeColor="Red"
                                        runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtComEmailAddress1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-4" style="float: left;">
                                    <label class="col-sm-12 control-label">CC:</label>
                                </div>
                                <div class="col-md-4" style="float: left;">
                                    <label for="txtComEmailUser2" class="col-sm-6 control-label">Email User Name:</label>
                                    <asp:TextBox ID="txtComEmailUser2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-4" style="float: left;">
                                    <label for="txtComEmailAddress2" class="col-sm-6 control-label">Email Address:</label>
                                    <asp:TextBox ID="txtComEmailAddress2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" ValidationGroup="System" ForeColor="Red"
                                        runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtComEmailAddress2" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-4" style="float: left;">
                                    <label class="col-sm-12 control-label">CC:</label>
                                </div>
                                <div class="col-md-4" style="float: left;">
                                    <label for="txtComEmailUser3" class="col-sm-6 control-label">Email User Name:</label>
                                    <asp:TextBox ID="txtComEmailUser3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-4" style="float: left;">
                                    <label for="txtComEmailAddress3" class="col-sm-6 control-label">Email Address:</label>
                                    <asp:TextBox ID="txtComEmailAddress3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator3" ValidationGroup="System" ForeColor="Red"
                                        runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtComEmailAddress3" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-4" style="float: left;">
                                    <label class="col-sm-12 control-label">CC:</label>
                                </div>
                                <div class="col-md-4" style="float: left;">
                                    <label for="txtComEmailUser4" class="col-sm-6 control-label">Email User Name:</label>
                                    <asp:TextBox ID="txtComEmailUser4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-4" style="float: left;">
                                    <label for="txtComEmailAddress4" class="col-sm-6 control-label">Email Address:</label>
                                    <asp:TextBox ID="txtComEmailAddress4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator4" ValidationGroup="System" ForeColor="Red"
                                        runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtComEmailAddress4" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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

                                    <asp:TextBox ID="txtDocUserEmail" ClientIDMode="Static" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-4" style="float: left;">
                                    <asp:TextBox ID="txtDocUserPassword" ClientIDMode="Static" Enabled="False" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <div class="box-body" id="divglobal" runat="server">
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


                            <div class="col-md-6">
                                <label class="col-sm-6 control-label">Late Rent Payment Fee: $</label>
                                <asp:TextBox ID="txtLateFee" ClientIDMode="Static" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                <%--<asp:RangeValidator runat="server" Display="Dynamic" ValidationGroup="System" ID="rvLate" ControlToValidate="txtLateFee" MinimumValue="5" MaximumValue="100" Type="Integer" ErrorMessage="Minimum value should be 5%" ForeColor="Red"></asp:RangeValidator>--%>
                            </div>

                            <div class="col-md-6">
                                <label class="col-sm-6 control-label">Charge Back Fee: $</label>
                                <asp:TextBox ID="txtChargeBackFee" ClientIDMode="Static" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                <%--<asp:RangeValidator runat="server" Display="Dynamic" ID="rvChargeBack" ValidationGroup="System" ControlToValidate="txtChargeBackFee" MinimumValue="50" MaximumValue="999999" Type="Integer" ErrorMessage="Minimum value should be $50" ForeColor="Red"></asp:RangeValidator>--%>
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
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
                                            <asp:GridView Width="100%" ID="gvCard" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-responsive table-bordered table-striped"
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
                                        <label class="col-sm-4 control-label">Address:</label>
                                        <asp:TextBox ID="txtCardAddress" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <label class="col-sm-4 control-label">City:</label>
                                        <asp:TextBox ID="txtCardCity" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <label class="col-sm-4 control-label">State:</label>
                                        <asp:DropDownList ID="ddlState" CssClass="col-sm-6 form-control" runat="server">
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
                                    <div class="col-md-12 btnSubmitDiv">
                                        <asp:Button ID="btnAddCard" runat="server" Text="Add" OnClick="btnAddCard_Click" CssClass="btn btn-success " />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div class="col-md-12">
                        <div class="box-header with-border CommonHeader col-md-12">
                            <h3 class="box-title">Pull Down Information</h3>
                        </div>
                        <asp:UpdatePanel runat="server" ID="userpanel">
                            <ContentTemplate>
                                <div class="box-body">
                                    <div class="col-md-4" style="float: left; padding: 0px; padding-top: 10px; margin-right: 15px; background-color: #8B9DC3;">
                                        <div class="col-sm-12">
                                            <asp:GridView Width="100%" ID="gvContactTypeList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                GridLines="None" AllowPaging="True" PageSize="20" Style="margin-bottom: 10px;" AllowSorting="True">
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
                                        <div class="col-sm-9">
                                            <label class="col-sm-6 control-label">Type of Contact:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtTypeofContact" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" style="text-align: center;">
                                            <asp:Button ID="btnAddTypeOfContact" runat="server" Text="Add" Style="margin: 20px 0; margin-left: 80%;" OnClick="btnAddTypeOfContact_Click" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="float: left; margin-right: 15px; padding: 0px; padding-top: 10px; background-color: #8B9DC3;">
                                        <div class="col-sm-12">
                                            <asp:GridView Width="100%" ID="gvLedger" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-responsive"
                                                GridLines="None" AllowPaging="True" PageSize="20" Style="margin-bottom: 10px;" AllowSorting="True">
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
                                        <div class="col-sm-9">
                                            <label class="col-sm-6 control-label">Ledger Code:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtLedger" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-9">
                                            <label class="col-sm-6 control-label">Ledger Name:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtLedgerName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" style="text-align: center;">
                                            <asp:Button ID="btnLedger" runat="server" Text="Add" OnClick="btnLedger_Click" Style="margin: 20px 0; margin-left: 80%;" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="float: left; padding: 0px; padding-top: 10px; background-color: #8B9DC3;">
                                        <div class="col-sm-12">
                                            <asp:GridView Width="100%" ID="gvPaymentType" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-responsive"
                                                GridLines="None" AllowPaging="True" PageSize="20" Style="margin-bottom: 10px;" AllowSorting="True">
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
                                        <div class="col-sm-9">
                                            <label class="col-sm-6 control-label">Pay Type:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPaymentType" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" style="text-align: center;">
                                            <asp:Button ID="btnPaymentType" runat="server" Text="Add" Style="margin: 20px 0; margin-left: 80%;" OnClick="btnPaymentType_Click" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="box-footer">
                    <div class="col-md-12">
                        <div class="col-md-4 btnSubmitDiv">
                            <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn btn-success " />
                        </div>
                       
                        <div class="col-md-4 btnSubmitDiv">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-successNew" ValidationGroup="System" />
                        </div>
                         <div class="col-md-4 btnSubmitDiv">
                            <asp:Button ID="btnCancel" runat="server" Text="Clear Form" OnClick="btnClose_Click" CssClass="btn btn-success " />
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
        
    </style>
</asp:Content>
