<%@ Page Title="EProperty365: P.O./Work Order" Language="C#" MasterPageFile="~/MasterPage/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="WorkOrder.aspx.cs" Inherits="eProperty.Pages.Admin.WorkOrderPO" %>

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
                                <h3 class="box-title" id="lblHeadline" runat="server">P.O / Work Order</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="col-md-12">
                                    <asp:GridView Width="100%" ID="gvPOList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gvPOList_PageIndexChanging"
                                        OnSorting="gvPOList_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                        <PagerSettings Position="TopAndBottom" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="PO ID" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vendor Name" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("VendorName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contract Name" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContractName" runat="server" Text='<%# Eval("ContractName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblPOSerial" runat="server" Text='<%# Eval("Serial") %>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="btnPOEdit" runat="server" Text="Edit" OnClick="btnPOEdit_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnPODelete" runat="server" OnClick="btnPODelete_Click" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete"></asp:LinkButton>
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
                                    <h6>To be Purchased:</h6>
                                </div>
                            </div>
                            <!-- form start -->
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="lblId" class="col-sm-3 control-label">P.O. ID:</label>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPOId" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblMsg" ForeColor="Green" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="ddlVendor" class="col-sm-3 control-label" style="float: left;">Vendor Name:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlVendor" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
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
                                    <label for="txtCompanyName" class="col-sm-3 control-label">Company Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtAddress" class="col-sm-3 control-label">Address1:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtAddress1" class="col-sm-3 control-label">Address2:</label>
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
                                    <label for="txtPhone" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Phone #:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEmail" class="col-sm-3 control-label">*Email Address:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <h6>Description of Purchase</h6>
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
                                            <asp:TemplateField HeaderText="Part Number" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartNumber" runat="server" Text='<%# Eval("PartNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Manufacturer" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblManufacturer" runat="server" Text='<%# Eval("Manufacturer") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cost" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCost" runat="server" Text='<%# Eval("Cost") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Approved By" SortExpression="Data">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Eval("ApprovedBy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PurchaseDate" SortExpression="Data">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblPurchaseDate" runat="server" Text='<%# Eval("PurchaseDate") %>'></asp:Label>--%>
                                                     <%#DataBinder.Eval(Container.DataItem, "PurchaseDate", "{0:MM/dd/yyyy}")%>
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
                                    <label for="txtPart" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Part #:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPart" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtManufacture" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Manufacture:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtManufacture" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtModel" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Model #:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtModel" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtCost" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Cost:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCost" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtApproved" class="col-sm-3 control-label" style="float: left; padding-left: 5px;">Approved By:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtApproved" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtDate" class="col-sm-3 control-label" style="float: left;">Date:</label>
                                    <div class="col-sm-6">
                                       <%-- <asp:TextBox runat="server" ID="txtDate" CssClass="form-control tDate" Text=""></asp:TextBox>--%>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="approveddate1" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="20px" height="30px" />
                                        <asp:CalendarExtender ID="CalendarExtender17" runat="server" PopupButtonID="approveddate1"
                                            TargetControlID="txtDate">
                                        </asp:CalendarExtender>
                                    </div>

                                </div>
                                <div class="col-md-12 btnSubmitDiv" style="text-align: center;">
                                    <asp:Button ID="btnPartAdd" runat="server" Text="Add" OnClick="btnPartAdd_Click" CssClass="btn btn-success " />
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-6">
                                    <label for="ddlBillTo" class="col-sm-3 control-label" style="float: left;">Bill To:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlBillTo" CssClass="form-control" OnSelectedIndexChanged="ddlBillTo_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="ddlShipTo" class="col-sm-3 control-label" style="float: left;">Shipped To:</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlShipTo" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label class="col-sm-12 ">
                                        <asp:RadioButtonList runat="server" ID="rdoType" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="CheapestFright" Selected="True">Cheapest Freight</asp:ListItem>
                                            <asp:ListItem Value="Overnight">Overnight</asp:ListItem>
                                            <asp:ListItem Value="2ndDay">2nd day</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </label>
                                </div>
                                <div class="col-md-12 btnSubmitDiv" style="text-align: center;">
                                    <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send Work Order PO" CssClass="btn btn-success " />
                                </div>
                            </div>
                            <div class="box-body" style="margin-top: 0px; margin-bottom: 0px; text-align: center;">
                                <div class="col-md-12" style="text-align: center;">
                                    <div class="col-md-6">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnClose_Click" CssClass="btn btn-success " />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-successnew" ValidationGroup="Contact" />
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
    <script type="text/javascript">
        function CheckVal() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
        }
    </script>
</asp:Content>

