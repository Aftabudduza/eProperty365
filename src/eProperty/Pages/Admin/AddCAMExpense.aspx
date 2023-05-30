<%@ Page Title="EProperty365: Add/Edit CAM Expense" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddCAMExpense.aspx.cs" Inherits="eProperty.Pages.Admin.AddCAMExpense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="userpanel">
            <ContentTemplate>
                <div class="box">
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-8">
                                <h3 class="box-title" id="lblPropertyLocation" runat="server"></h3>
                            </div>
                            <div class="col-md-6">
                                <label for="txtFromDate" class="col-sm-3 control-label" style="float: left;">From Date:</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control" Text=""></asp:TextBox>
                                </div>
                                <img alt="" id="Fdate" src="https://www.eproperty365.net/Images/calender.jpg"
                                    width="30px" height="30px" />
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="Fdate"
                                    TargetControlID="txtFromDate">
                                </asp:CalendarExtender>
                            </div>
                            <div class="col-md-6">
                                <label for="txtToDate" class="col-sm-3 control-label" style="float: left;">To Date:</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control" Text=""></asp:TextBox>
                                </div>
                                <img alt="" id="edate" src="https://www.eproperty365.net/Images/calender.jpg"
                                    width="30px" height="30px" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="edate"
                                    TargetControlID="txtToDate">
                                </asp:CalendarExtender>
                            </div>

                            <div class="col-md-4">
                                <asp:Button ID="btnsearchCAM" runat="server" Text="Search" OnClick="btnsearchCAM_Click" CssClass="btn btn-success " />
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <form class="vertical-form">
                            <fieldset>
                                <asp:GridView Width="100%" ID="gvCAMList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                    GridLines="None" AllowPaging="True" OnPageIndexChanging="gvCAMList_PageIndexChanging"
                                    OnSorting="gvCAMList_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                    <PagerSettings Position="TopAndBottom" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name" SortExpression="Data">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type Of Expense" SortExpression="Data">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeOfExpense" runat="server" Text='<%# Eval("TypeOfExpense") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount Paid" SortExpression="Data">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Amount", "{0:#.##}")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Paid" SortExpression="Data">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "PaidDate", "{0:MMM dd  yyyy}")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Check" SortExpression="Data">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCheckNumber" runat="server" Text='<%# Eval("CheckNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCAMId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="btnEditCAM" runat="server" Text="Edit" OnClick="btnEditCAM_Click"></asp:LinkButton>
                                                <asp:LinkButton ID="btnDeleteCAM" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDeleteCAM_Click"></asp:LinkButton>
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
                            <div class="box-header with-border">
                                <h3 class="box-title" id="lblHeadline" runat="server">Add Expense</h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="txtName" class="col-sm-6 control-label">*Vendor Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtName" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Vendor Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtExpenseType" class="col-sm-6 control-label">Type of Expenses:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtExpenseType" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtNumber" class="col-sm-6 control-label">*Amount Paid:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNumber" ValidationGroup="Contact"
                                            Display="Dynamic" runat="server" ErrorMessage="Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="ddlLedgerType" class="col-sm-6 control-label" style="float: left;">Ledger Account:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlLedgerType" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" id="ddltype" runat="server" ControlToValidate="ddlLedgerType" InitialValue="-1" ErrorMessage="Please Ledger Account" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="ddlPaidBy" class="col-sm-6 control-label" style="float: left;">Paid By:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlPaidBy" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="Dynamic" id="requsertype" runat="server" ControlToValidate="ddlPaidBy" InitialValue="-1" ErrorMessage="Please payment Type" ForeColor="Red" ValidationGroup="Contact"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtExpenseType" class="col-sm-6 control-label">*Check Number:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCheckNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCheckNo" ValidationGroup="Contact"
                                            Display="Dynamic" runat="server" ErrorMessage="Check Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                     <label class="col-sm-6 control-label">CAM Expense</label>
                                   <label class="col-sm-6 ">
                                        <asp:RadioButtonList runat="server" ID="rdoCAM" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </label>
                                </div>

                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer">
                                <div class="col-md-6">
                                    <asp:Button ID="btnSaveCAM" runat="server" Text="Add Expense Contact" OnClientClick="CheckVal()" OnClick="btnSubmitCAM_Click" CssClass="btn btn-success" ValidationGroup="Contact" />

                                    <asp:Button ID="btnCancelCAM" runat="server" Text="Cancel" OnClick="btnCloseCAM_Click" CssClass="btn btn-success " />
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
