<%@ Page Title="EProperty365: Accounting - Create Invoice" Language="C#" MasterPageFile="~/MasterPage/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="RecordInvoice.aspx.cs" Inherits="eProperty.Pages.Account.RecordInvoices" %>

<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager CombineScripts="true" runat="server" ID="sc1">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="userpanel">
            <ContentTemplate>
                <div class="box">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box box-primary">
                            <div class="box-header with-border CommonHeader col-md-12">
                                <h3 class="box-title" id="lblHeadline" runat="server">Accounting - Create Invoice</h3>
                            </div>
                            <MyAccount:AccountControl ID="Account" runat="server" />
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="box-body">
                                <div class="col-md-3">
                                    <label for="lblId" class="col-sm-3 control-label">Date:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="lblId" class="col-sm-3 control-label">Invoice Number:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPOId" runat="server"></asp:Label>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <label class="control-label">Due Upon Receipt</label>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <h6>From:</h6>
                                    <br />
                                    <span id="spanOwnerAddress" runat="server"></span>
                                </div>

                                <div class="col-md-6">
                                    <asp:Label ID="lblMsg" ForeColor="Green" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label>
                                </div>


                            </div>

                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="ddlBillTo" class="col-sm-3 control-label" style="float: left;">Type:</label>
                                    <label class="col-sm-6 ">
                                        <asp:RadioButtonList runat="server" ID="rdoType" AutoPostBack="true" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Tenant" Selected="True">Tenant</asp:ListItem>
                                            <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </label>
                                </div>
                                <div class="col-md-6">
                                    <label for="ddlBillTo" class="col-sm-3 control-label" style="float: left;">Bill To:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlBillTo" CssClass="form-control" OnSelectedIndexChanged="ddlBillTo_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ddlBillToValidator" runat="server" ControlToValidate="ddlBillTo" InitialValue="-1" ErrorMessage="Please select BillTo" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                    </div>
                                </div>


                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtCompanyName" class="col-sm-3 control-label">Company Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtAddress" class="col-sm-3 control-label">Address:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtAddress1" class="col-sm-3 control-label">Address:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
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
                                    <label for="ddlState" class="col-sm-3 control-label" style="float: left;">State:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
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
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtContactName" class="col-sm-3 control-label">Contact Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtPhone" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Phone Number:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEmail" class="col-sm-3 control-label">*Email Address:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Email Address Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ValidationGroup="Contact"
                                            runat="server" ForeColor="Red" ErrorMessage="Invalid Email Address" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                               
                                <div class="col-md-6">
                                     <label for="lblTotal" class="col-sm-3 control-label">Total:</label>
                                    <asp:Label ID="lblTotal" class="col-sm-3 control-label" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <asp:GridView Width="100%" ID="gvPart" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gvPart_PageIndexChanging"
                                        OnSorting="gvPart_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                        <PagerSettings Position="TopAndBottom" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Qty" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Price" SortExpression="Data">
                                                <ItemTemplate>
                                                   <%-- <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>--%>
                                                    <%# Eval("UnitPrice","{0:c}") %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" SortExpression="Data">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>--%>
                                                     <%# Eval("Amount","{0:c}") %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="btnPartEdit" runat="server" Text="Edit" OnClick="btnPartEdit_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnPartDelete" runat="server" OnClick="btnPartDelete_Click" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete"></asp:LinkButton>
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
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtPart" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Qty/Hrs:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtQty" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtItem" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Item:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtItem" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtDescription" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Description:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtPrice" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Unit Price:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPrice" AutoPostBack="true" OnTextChanged="txtPrice_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtAmount" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Amount:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12 btnSubmitDiv" style="text-align: center;">
                                    <asp:Button ID="btnPartAdd" runat="server" Text="Add" OnClick="btnPartAdd_Click" CssClass="btn btn-success " />
                                </div>
                            </div>


                            <div class="box-body" style="margin-top: 0px; margin-bottom: 0px; text-align: center;">
                                <div class="col-md-12" style="text-align: center;">
                                    <div class="col-md-6">
                                        <asp:Button ID="btnSave" runat="server" Text="Create Email Invoice" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-successnew" ValidationGroup="Contact" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnClose_Click" CssClass="btn btn-success " />
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
            b o 3px;
            px 10px 10px;
        }


        .co height ne-height: 35px; -align: center r-palette-set -bottom: 15px; . olor- a display: none; e: 12px; palette:hover span {
            play: blo k;
            .c lor-p;

        {
            absolute;
            ;
            lft: 5px;
            -top: -40p;
            r: rgba( 55, 25 e d s z inde;
        }

        col- dding-left: 0 x padding-ri ht: 0px;
        }

        ustombox {
            p: 3px solid rans margin- otto: 0p;
            right: 5px;
            dius: 0px 1 ackgr f color: #444;
            padding: 6px 15px;
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

