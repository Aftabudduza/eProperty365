<%@ Page Title="EProperty365: Add/Edit User" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddUser.aspx.cs" Inherits="eProperty.Pages.Admin.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <%-- <asp:ScriptManager runat="server" ID="sc1">
        </asp:ScriptManager>--%>

        <div class="box">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border CommonHeader col-md-12">
                        <h3 class="box-title" id="lblHeadline" runat="server">Create Users</h3>
                    </div>
                    <%--    <asp:UpdatePanel runat="server" ID="userpanel">
                        <ContentTemplate>--%>
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
                    <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <!-- /.box-header -->
                    <!-- form start -->
                    <div class="box-body">
                        <div class="col-md-6">
                            <label for="txtContactName" class="col-sm-3 control-label">*User Name:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtContactName" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtContactName" ValidationGroup="Contact"
                                    runat="server" ErrorMessage="Contact Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtContactTitle" class="col-sm-3 control-label">User Title:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtContactTitle" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtEmail" class="col-sm-3 control-label">*Email Address:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail" ValidationGroup="Contact"
                                    runat="server" ErrorMessage="Email Address Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ValidationGroup="Contact"
                                    runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtPassword" class="col-sm-3 control-label">*Password:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPassword" ValidationGroup="Contact"
                                    runat="server" ErrorMessage="Password Required" ForeColor="Red" Display="Dynamic">
                                </asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="txtNumber" class="col-sm-3 control-label">*Phone Number:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNumber" ValidationGroup="Contact"
                                    Display="Dynamic" runat="server" ErrorMessage="Phone Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="ddlType" class="col-sm-3 control-label" style="float: left;">*Security Level:</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="2 and Up">2 and Up</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="uplLogo" class="col-sm-3 control-label" style="float: left;">Image</label>
                            <div class="col-sm-6">
                                <asp:FileUpload ID="uplLogo" runat="server" CssClass="form-control" />
                                <asp:Image ID="imgLogo" runat="server" Width="30" />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label class="col-sm-4 ">
                                <asp:CheckBox runat="server" ID="chkLocation" Text="By Location" />
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
    </style>
</asp:Content>
