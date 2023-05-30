<%@ Page Title="EProperty365: Add/Edit Contact" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddContact.aspx.cs" Inherits="eProperty.Pages.Admin.AddContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager runat="server" ID="sc1">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="userpanel">
            <ContentTemplate>
                <div class="box">
                    <div class="box-header with-border CommonHeader col-md-12">
                        <h3 class="box-title" id="lblHeadline" runat="server">Create / Change Contact Information</h3>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-6">
                                <input type="text" id="search" name="search" runat="server" class="form-control search-box" placeholder="Enter Search Keywords" />
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-success " />
                            </div>
                        </div>
                    </div>
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
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box box-primary">

                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtContactName" class="col-sm-3 control-label">*Contact Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtContactName" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Contact Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtContactTitle" class="col-sm-3 control-label">Contact Title:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtContactTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="ddlType" class="col-sm-3 control-label" style="float: left;">*Type of Number:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="-1">None</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" id-="requsertype" runat="server" ControlToValidate="ddlType" InitialValue="-1" ErrorMessage="Please select Type of Contact" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtNumber" class="col-sm-3 control-label">*Phone Number:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNumber" ValidationGroup="Contact"
                                            Display="Dynamic" runat="server" ErrorMessage="Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="countrytype" runat="server" ControlToValidate="ddlCountry" InitialValue="-1" ErrorMessage="Please select Country" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
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
                                <div class="col-md-12">
                                    <div class="col-md-4 btnSubmitDiv">
                                        <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn btn-success" />
                                    </div>
                                    
                                    <div class="col-md-4 btnSubmitDiv">
                                        <asp:Button ID="btnSave" runat="server" Text="Add Contact" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-successNew" ValidationGroup="Contact" />
                                    </div>
                                    <div class="col-md-4 btnSubmitDiv">
                                        <asp:Button ID="btnCancel" runat="server" Text="Clear Form" OnClick="btnClose_Click" CssClass="btn btn-success" />
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

        $('#<%=chkEmergency.ClientID %>').change(function () {
            if (this.checked) {
                $('#<%=EmergencyDiv.ClientID %>').show();
            }
            else {
                $('#<%=EmergencyDiv.ClientID %>').hide();
            }
        });

    </script>
    <style type="text/css">
        .input-validation-error {
            border: 1px solid #ff0000;
            background-color: #ffeeee !important;
        }

        .field-validation-error {
            color: #ff0000 !important;
        }

        .box-header.with-border {
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
        }
    </style>
</asp:Content>
