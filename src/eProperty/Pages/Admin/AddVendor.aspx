<%@ Page Title="EProperty365: Add/Edit Vendor" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddVendor.aspx.cs" Inherits="eProperty.Pages.Admin.AddVendor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="userpanel">
            <ContentTemplate>
                <div class="box">
                    <div class="box-header with-border CommonHeader col-md-12">
                        <h3 class="box-title" id="lblHeadline" runat="server">Vendor List</h3>
                    </div>
                    <div class="box-body">
                        <div class="form-group" style="display: none;">
                            <div class="col-md-8">
                                <h3 class="box-title" id="lblPropertyLocation" runat="server"></h3>
                            </div>
                            <div class="col-md-6">
                                <label for="ddlOwner" class="col-sm-3 control-label" style="float: left;">*Owner ID:</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlOwner" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlOwner_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOwner" InitialValue="-1" ErrorMessage="Select owner" ForeColor="Red" ValidationGroup="ContactSearch"></asp:RequiredFieldValidator>
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
                                <label for="ddlLocation" class="col-sm-3 control-label" style="float: left;">*Location:</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlLocation" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlLocation" InitialValue="-1" ErrorMessage="Select location" ForeColor="Red" ValidationGroup="ContactSearch"></asp:RequiredFieldValidator>
                                </div>
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
                            <%--<div class="box-header with-border">
                                <h3 class="box-title" id="lblHeadline" runat="server">Add Vendor</h3>
                            </div>--%>
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
                                    <label for="txtContactName" class="col-sm-3 control-label">*Contact Name:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtContactName" ValidationGroup="Contact"
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtAddress" ValidationGroup="Contact"
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtCity" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="City Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtZip" class="col-sm-3 control-label">*Zip Code:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="txtZip" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Zip Code Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEmail" class="col-sm-3 control-label">*Email Address:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtEmail" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Email Address Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtNumber" class="col-sm-3 control-label">*Phone Number:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtNumber" ValidationGroup="Contact"
                                            Display="Dynamic" runat="server" ErrorMessage="Number Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtBIN" class="col-sm-3 control-label">Business License Number:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtBIN" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtBIN" ValidationGroup="Contact"
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
                                        <img alt="" id="approveddate1" class="col-sm-3" src="https://www.eproperty365.net/Images/calender.jpg"
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
                                        <img alt="" id="effectdate1" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="effectdate1"
                                            TargetControlID="txtEffectDate1">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="enddate1" src="https://www.eproperty365.net/Images/calender.jpg"
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
                                        <img alt="" id="rdate1" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="rdate1"
                                            TargetControlID="txtReviewDate1">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDocumentID1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFiledDate1" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="fdate1" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="fdate1"
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
                                        <img alt="" id="effectdate2" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="effectdate2"
                                            TargetControlID="txtEffectDate2">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="enddate2" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="enddate2"
                                            TargetControlID="txtEndDate2">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReviewBy2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReviewDate2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="rdate2" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="rdate2"
                                            TargetControlID="txtReviewDate2">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDocumentID2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFiledDate2" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="fdate2" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="fdate2"
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
                                        <img alt="" id="effectdate3" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="effectdate3"
                                            TargetControlID="txtEffectDate3">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="enddate3" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender10" runat="server" PopupButtonID="enddate3"
                                            TargetControlID="txtEndDate3">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReviewBy3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReviewDate3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="rdate3" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender11" runat="server" PopupButtonID="rdate3"
                                            TargetControlID="txtReviewDate3">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDocumentID3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFiledDate3" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="fdate3" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender12" runat="server" PopupButtonID="fdate3"
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
                                        <img alt="" id="effectdate4" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender13" runat="server" PopupButtonID="effectdate4"
                                            TargetControlID="txtEffectDate4">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="enddate4" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender14" runat="server" PopupButtonID="enddate4"
                                            TargetControlID="txtEndDate4">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReviewBy4" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReviewDate4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="rdate4" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender15" runat="server" PopupButtonID="rdate4"
                                            TargetControlID="txtReviewDate4">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDocumentID4" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFiledDate4" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                        <img alt="" id="fdate4" src="https://www.eproperty365.net/Images/calender.jpg"
                                            width="25px" height="25px" style="margin-left: 5px; margin-top: 5px;" />
                                        <asp:CalendarExtender ID="CalendarExtender16" runat="server" PopupButtonID="fdate4"
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
                            <div class="col-md-12">
                                <div class="col-md-3 btnSubmitDiv">
                                    <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn btn-success " />
                                </div>
                                <div class="col-md-3 btnSubmitDiv">
                                    <asp:Button ID="btnSave" runat="server" Text="Add" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="Contact" />
                                </div>
                                <div class="col-md-3 btnSubmitDiv">
                                    <asp:Button ID="btnSaveNew" runat="server" Text="Save Vendor List" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-successNew" ValidationGroup="Contact" />
                                </div>
                                <div class="col-md-3 btnSubmitDiv">
                                    <asp:Button ID="btnCancel" runat="server" Text="Clear Form" OnClick="btnClose_Click" CssClass="btn btn-success " />
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

        .table th, .table td {
            padding: 5px 2px;
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
