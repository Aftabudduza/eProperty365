<%@ Page Title="EProperty365: Add/Edit Property Manager" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddPropertyManager.aspx.cs" Inherits="eProperty.Pages.Admin.AddPropertyManager" %>

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
                                <h3 class="box-title" id="lblHeadline" runat="server">Create / Change Property Manager Profile</h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="box-body">
                                <div class="col-md-8">
                                    <label for="lblId" class="col-sm-3 control-label">Property Manager ID:</label>
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
                                <div class="col-md-6">
                                    <label for="txtZip" class="col-sm-3 control-label">*Zip Code:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtZip" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Zip Code Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtRegion" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">EIN Number or Social Security Number:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtEIN" runat="server" CssClass="form-control"></asp:TextBox>
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

                            <%-- <div class="nav-tabs-custom">
                                <ul id="topnav" class="nav nav-tabs">
                                    <li><a href="#Contacts" data-toggle="tab">Contacts</a></li>
                                    <li><a href="#Users" data-toggle="tab">Users</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane" id="Contacts">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>
                                                <div class="box">
                                                    <div class="box-body">
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
                                                            </fieldset>
                                                        </form>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="box box-primary">
                                                            <div class="box-body">
                                                                <div class="col-md-6">
                                                                    <label for="txtContactName" class="col-sm-6 control-label">*Contact Name:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="txtContactName" ValidationGroup="Contact"
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
                                                                        <asp:RequiredFieldValidator Display="Dynamic" id="Contacttype" runat="server" ControlToValidate="ddlType" InitialValue="-1" ErrorMessage="Please select Type of Contact" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtNumber" class="col-sm-6 control-label">*Number:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtNumber" ValidationGroup="Contact"
                                                                            Display="Dynamic" runat="server" ErrorMessage="Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="box-body">
                                                                <div class="col-md-6">
                                                                    <label for="txtEmail" class="col-sm-6 control-label">*Email Address:</label>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtEmail" ValidationGroup="Contact"
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
                                                                    <asp:Button ID="btnSubmitContact" runat="server" Text="Add Contact" OnClientClick="CheckVal()" OnClick="btnSubmitContact_Click" CssClass="btn btn-success" ValidationGroup="Contact" />

                                                                    <asp:Button ID="btnCloseContact" runat="server" Text="Cancel" OnClick="btnCloseContact_Click" CssClass="btn btn-success " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane" id="Users">
                                        <div class="box-body">
                                            <form class="vertical-form">
                                                <fieldset>
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
                                                </fieldset>
                                            </form>
                                        </div>
                                       
                                        <div class="col-md-12">
                                           
                                            <div class="box box-primary">
                                               
                                                <div class="box-body">
                                                    <div class="col-md-6">
                                                        <label for="txtUserName" class="col-sm-3 control-label">*User Name:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" ControlToValidate="txtUserName" ValidationGroup="user"
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
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtUserEmail" ValidationGroup="user"
                                                                runat="server" ErrorMessage="Email Address Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" ValidationGroup="Contact"
                                                                runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtUserEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtUserNumber" class="col-sm-3 control-label">*Phone Number:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtUserNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtUserNumber" ValidationGroup="user"
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
                                                        <asp:Button ID="btnSaveUser" runat="server" Text="Add" OnClientClick="CheckVal()" OnClick="btnSubmitUser_Click" CssClass="btn btn-success" ValidationGroup="user" />

                                                        <asp:Button ID="btnCancelUser" runat="server" Text="Cancel" OnClick="btnCloseUser_Click" CssClass="btn btn-success " />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <!-- /.box-body -->
                            <div class="box-footer">
                                <div class="col-md-12">
                                    <div class="col-md-4 btnSubmitDiv">
                                        <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn btn-success " />
                                    </div>
                                   
                                    <div class="col-md-4 btnSubmitDiv">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-successNew" ValidationGroup="Contact" />
                                    </div>
                                     <div class="col-md-4 btnSubmitDiv">
                                        <asp:Button ID="btnCancel" runat="server" Text="Clear Form" OnClick="btnClose_Click" CssClass="btn btn-success " />
                                    </div>
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
    </style>
    <script type="text/javascript">
        $(".nav nav-tabs  li a").on("click", function () {
            $(".nav nav-tabs  li").find(".active").removeClass("active");
            $(this).addClass("active");
        });
    </script>
</asp:Content>
