<%@ Page Title="EProperty365: Create / Change Residential Web Page Specs Options Unit" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddResidentialUnit.aspx.cs" Inherits="eProperty.Pages.Admin.AddResidentialUnit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc2">
        </asp:ToolkitScriptManager>
        <div class="box">
            <asp:UpdatePanel runat="server" ID="topPanel">
                <ContentTemplate>
                    <div class="box-body">
                        <div class="box-header with-border CommonHeader col-md-12">
                            <h3 class="box-title" id="H1" runat="server">Create/Change Residential Unit</h3>
                        </div>
                    </div>
                    <%-- <div class="box-body">
                        <div class="col-md-12">
                            <asp:Label ID="lblErrorMsg" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>--%>
                    <div class="box-body">
                        <fieldset>
                            <asp:GridView Width="100%" ID="gvUnitList" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-responsive table-bordered table-striped"
                                GridLines="None" AllowPaging="True" OnPageIndexChanging="gvUnitList_PageIndexChanging"
                                OnSorting="gvUnitList_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Unit ID" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerial" runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Name" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Type" SortExpression="Data">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("UnitType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:HyperLinkField Target="_blank" DataNavigateUrlFields="Serial" DataNavigateUrlFormatString="https://www.eproperty365.net/Pages/Resident/ResidentialAddResponceTemplate_Login.aspx?ResidentialUnitSerial={0}" HeaderText="View" Text="View Unit Page" />
                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="btnUnitEdit" runat="server" Text="Edit" OnClick="btnUnitEdit_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="btnUnitDelete" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnUnitDelete_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="btnDuplicate" runat="server" OnClientClick='return confirm("Are you sure you want to Duplicate this unit Data?");' Text="Duplicate" OnClick="btnDuplicate_OnClick"></asp:LinkButton>
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
                    </div>
                    <div class="box-body">
                        <div class="col-md-6">
                            <label for="ddlOwnerIdTop" class="col-sm-6 control-label">*Owner ID:</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlOwnerIdTop" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlOwnerIdTop_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="ddlOwnerIdTopValidator" runat="server" ControlToValidate="ddlOwnerIdTop" InitialValue="-1" ErrorMessage="Please select Owner Type" ForeColor="Red" ValidationGroup="Basic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="ddlPropertyManagerID" class="col-sm-6 control-label">Property Manager ID:</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlPropertyManagerID" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="ddlPropertyManagerIDValidator" runat="server" ControlToValidate="ddlPropertyManagerID" InitialValue="-1" ErrorMessage="Please select Property Manager" ForeColor="Red" ValidationGroup="Basic"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>

                    </div>
                    <div class="box-body">
                        <div class="col-md-6">
                            <label for="ddlLocationID" class="col-sm-6 control-label">*Location:</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlLocationID" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="ddlLocationIDValidator" runat="server" ControlToValidate="ddlLocationID" InitialValue="-1" ErrorMessage="Please select Location" ForeColor="Red" ValidationGroup="Basic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="txtUnitID" class="col-sm-6 control-label">Unit ID:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtUnitID" Text="" ReadOnly="True" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="col-md-6">
                            <label for="txtUnitName" class="col-sm-6 control-label">*Unit Name:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtUnitName" Text="" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="txtUnitNameValidator" Display="Dynamic" ControlToValidate="txtUnitName" ValidationGroup="Basic"
                                    runat="server" ErrorMessage="Unit Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="txtUnitType" class="col-sm-6 control-label">*Type of Unit:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtUnitType" Text="" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="txtUnitTypeValidator" Display="Dynamic" ControlToValidate="txtUnitType" ValidationGroup="Basic"
                                    runat="server" ErrorMessage="Unit Type Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtSpecialComments" runat="server" CssClass="col-sm-24 form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="col-md-12 btnSaveWebCenter">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnClose_Click" CssClass="btn btnNewColor " />

                            <asp:Button ID="btnSaveBasicUnit" runat="server" Text="Save & Continue >" OnClientClick="CheckVal()" CssClass="btn btnNewColor" ValidationGroup="Basic" OnClick="btnSaveBasicUnit_Click" />


                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlOwnerIdTop" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btnAsyncSave"/>--%>
                    <%--background-image: url(../../Images/u2_normal.png);--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="box">
            <div class="col-md-12">
                <div class="nav-tabs-custom" id="tabs">
                    <ul id="topnav" class="nav nav-tabs" style="background-repeat: no-repeat;">
                        <li><a href="#Specs" data-toggle="tab">Specs</a></li>
                        <li><a href="#WebPageImages" data-toggle="tab">WebPage & Images</a></li>
                        <li><a href="#WebAnalytics" data-toggle="tab">Web Analytics</a></li>
                        <li><a href="#Equipment" data-toggle="tab">Equipment</a></li>
                        <li><a href="#Maintenance" data-toggle="tab">Maintenance Manager</a></li>
                        <li><a href="../Resident/ManagementDashboard.aspx">Maintenance Schedule</a></li>
                        <li><a href="#Communication" data-toggle="tab">Communication</a></li>
                        <li><a href="#Documents" data-toggle="tab">Documents</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane" id="Maintenance">
                            <asp:UpdatePanel runat="server" ID="upMaintenance">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="row">
                                                    <div class="box-header with-border CommonHeader col-md-12">
                                                        <h3 class="box-title" id="H5">Create / Change Maintenance Manager Unit</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4" style="float: left">
                                                        <div class="col-sm-12" style="margin-bottom: 17px">
                                                            <img id="imgManager" width="100%" src="" />
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <input type="file" style="width: 100%;" id="fileMaintenanceImageUpload" />
                                                        </div>
                                                        <div class="col-sm-12 col-sm-push-8">
                                                            <input type="button" class="btn btnNewColor " value="Upload" onclick="SaveMaintenanceImage();" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-8" style="padding-left: 5px; float: right">
                                                        <div class="col-md-6">
                                                            <label for="txtMainEqType" class="col-sm-6 control-label">Type of Equipment:</label>
                                                            <div class="col-sm-6" style="float: right">
                                                                <input type="text" id="txtMainEqType" class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtMainPurchaseDate" class="col-sm-6 control-label">Purchase Date:</label>
                                                            <div class="col-sm-6" style="float: right">
                                                                <input type="text" id="txtMainPurchaseDate" class="form-control mainDate" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtMainManufacturer" class="col-sm-6 control-label">Manufacturer:</label>
                                                            <div class="col-sm-6" style="float: right">
                                                                <input type="text" id="txtMainManufacturer" class="form-control" />

                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtMainManuPhone" class="col-sm-6 control-label">Manufacturer Phone #:</label>
                                                            <div class="col-sm-6" style="float: right">
                                                                <input type="text" id="txtMainManuPhone" class="form-control" />
                                                            </div>
                                                        </div>



                                                        <div class="col-md-6">
                                                            <label for="txtMainModel" class="col-sm-6 control-label">Model:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtMainModel" class="form-control" />
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <label for="txtMainCost" class="col-sm-6 control-label">Cost:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtMainCost" class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtMainVendorName" class="col-sm-6 control-label">Vendor Name:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtMainVendorName" class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtMainVendorPhone" class="col-sm-6 control-label">Vendor Phone Number:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtMainVendorPhone" class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" style="display: none;">
                                                            <label for="txtMainVendorPhone" class="col-sm-6 control-label">Reserve or order part day before schedule work:</label>
                                                            <div class="col-sm-6">
                                                                <a href="#" class="form-control">Documents / Manual</a>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="box-header with-border CommonHeader col-md-12">
                                                        <h3 class="box-title" id="H5">Maintenances Schedule</h3>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="padding-left: 5px; text-align: center">
                                                    <table id="tblMaintenanceSche" class="table table-responsive table-bordered table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>Reminder</th>
                                                                <th>Description of Job</th>
                                                                <th>Estimated Hours</th>
                                                                <th>Last Maintenance</th>
                                                                <th>Number Days</th>
                                                                <th>Next Maintenence</th>
                                                                <th>Edit </th>
                                                                <th>Delete</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>
                                                    </table>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 0px;">
                                                        <div class="col-md-6">
                                                            <label for="txtMainVendorPhone" class="col-sm-6 control-label">Description of Maintenance job:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtdescMaintainJobDate" class="form-control" />
                                                                <input type="hidden" id="hdMaintenanceId" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <div class="col-sm-12" style="padding-right: 0px;">
                                                                <input type="text" id="txtdescMaintainJob" class="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtMainVendorPhone" class="col-sm-3 control-label">Estimated Hours:</label>
                                                        <div class="col-sm-3">
                                                            <input type="text" id="ddlEstimateHoure" class="form-control" />
                                                        </div>

                                                        <div class="form-group col-sm-6">
                                                            <label>
                                                                <input type="checkbox" id="chkSubOut" class="flat-red">
                                                                Sub Out                                                               
                                                            </label>
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtlastMaintanence" class="col-sm-6 control-label">Last Maintenance:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtlastMaintanence" class="form-control mainDate" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="float: left; padding-left: 0px;">
                                                        <div class="col-md-6">
                                                            <label for="txtlastMaintanence" class="col-sm-6 control-label">Number Days:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtNumberDays" class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label for="txtlastMaintanence" class="col-sm-6 control-label">Next Maintenance date:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtNextMainDate" class="form-control mainDate" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 btnSubmitDiv" style="float: left; margin-bottom: 20px;">
                                                        <button type="button" id="btnAddSchedule" class="btn btnNewColor col-sm-4">Add Schedule</button>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center">
                                                        <table id="tblPartdata" class="table table-responsive table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th>Part Number</th>
                                                                    <th>Part Description</th>
                                                                    <th>Contact Person</th>
                                                                    <th>Email</th>
                                                                    <th>Manufacturer</th>
                                                                    <th>Edit </th>
                                                                    <th>Delete</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtPartNumber" class="col-sm-6 control-label">Part Number:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtPartNumber" class="form-control" />
                                                            <input type="hidden" id="hdPartId" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtPartdesc" class="col-sm-6 control-label">Part Description:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtPartdesc" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtPartContactPerson" class="col-sm-6 control-label">Contact Person:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtPartContactPerson" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtPartEmailAddress" class="col-sm-6 control-label">Email Address:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtPartEmailAddress" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtPartManufacturer" class="col-sm-6 control-label">Manufacturer:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtPartManufacturer" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <button type="button" id="btnAddPart" class="btn btnNewColor">Add Part</button>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 CommonHeader">Maintenance By Vendor</div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center">
                                                        <table id="tblMainVendor" class="table table-responsive table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th>Vendor Name</th>
                                                                    <th>Quote Id</th>
                                                                    <th>Vendor Contact</th>
                                                                    <th>Vendor Phone</th>
                                                                    <th>Email Address</th>
                                                                    <th>Amount of Quote</th>
                                                                    <th>When Done</th>
                                                                    <th>How Long</th>
                                                                    <th>Date Won Quote</th>
                                                                    <th>Edit </th>
                                                                    <th>Delete</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtMainVendorName" class="col-sm-6 control-label">Vendor Name:</label>
                                                        <div class="col-sm-6">
                                                            <%--<input type="text" id="txtMainVendorName" class="form-control" />--%>
                                                            <select id="ddlVendorName" class="form-control ddl"></select>
                                                            <input type="hidden" id="hdVendorId" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtPartdesc" class="col-sm-6 control-label">Quote Id:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtVendorQuoteId" readonly="readonly" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtVendorContact" class="col-sm-6 control-label">Vendor Contact:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtVendorContact" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtVendorPhone" class="col-sm-6 control-label">Vendor Phone:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtVendorPhone" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtVendorEmail" class="col-sm-6 control-label">Vendor Email Address:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtVendorEmail" class="form-control" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtAmountQuote" class="col-sm-6 control-label">Amount of quote:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtAmountQuote" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtWhenDone" class="col-sm-6 control-label">When Done:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtWhenDone" class="form-control mainDate" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtHowLong" class="col-sm-6 control-label">How Long:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtHowLong" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="txtDateWon" class="col-sm-6 control-label">Date won Quote:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtDateWon" class="form-control mainDate" />
                                                        </div>
                                                    </div>


                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="float: left; text-align: center; margin-bottom: 20px; margin-top: 30px">
                                                        <div class="col-md-6" style="float: left">
                                                            <button type="button" id="btnAddVendor" class="btn btnNewColor">Add Vendor</button>
                                                        </div>
                                                        <div class="col-md-6" style="float: left">
                                                            <button type="button" id="btnSendQuote" class="btn btnNewColor ">Send Quote</button>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" style="float: left; text-align: center; margin-bottom: 20px;">
                                                        <div class="col-md-6" style="float: left">
                                                            <button type="button" id="btnAddWinningNotice" class="btn btnNewColor ">Send winning notice to Vendor</button>
                                                        </div>
                                                        <div class="col-md-6" style="float: left;">
                                                            <button type="button" id="btnAddScheduleWork" class="btn btnNewColor ">Schedule Work</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 btnSaveWebCenter" style="float: left; margin-top: 0;">
                                            <div class="col-md-6" style="float: left">
                                                <%--<button type="button" id="btnCancelMain" class="btn btnNewColor ">Cancel</button>--%>
                                                <asp:Button ID="Button4" runat="server" Text="< Back" OnClick="btnBack_Click" CssClass="btn btnNewColor " />
                                            </div>
                                            <div class="col-md-6" style="float: left">
                                                <button type="button" id="btnAddEquipment" class="btn btn-successNew ">Add Equipment</button>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="tab-pane" id="Communication">
                            <asp:UpdatePanel runat="server" ID="UpdateCommunication">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">

                                                <div class="row">
                                                    <div class="box-header with-border CommonHeader col-md-12">
                                                        <h3 class="box-title" id="H3" runat="server">Communication</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <%-- <div class="col-md-10">
                                                        <label for="txtPicDesc" class="col-sm-3 control-label">Tenant Name:</label>
                                                        <div class="col-sm-8">
                                                            <%--<select id="ddlTenantName" class="form-control ddl loadCoun"></select>
                                                            <input type="text" id="txtTenantName" class="form-control" />
                                                        </div>
                                                    </div>--%>
                                                    <div class="col-md-12" style="padding-left: 5px; float: left">
                                                        <label for="ddlSender" class="col-sm-3 control-label">Sender</label>
                                                        <div class="col-sm-3">
                                                            <select id="ddlSender" class="form-control ddl  col-sm-8"></select>
                                                        </div>
                                                        <label for="ddlEmailType" class="col-sm-3 control-label">Email Type</label>
                                                        <div class="col-sm-3">
                                                            <select id="ddlEmailType" class="form-control ddl  col-sm-8 select2">
                                                                <option value="-1">All</option>
                                                                <option value="Report Issue to the Manager">Report Issue</option>
                                                                <option value="Maintenance Request">Maintenance Request</option>
                                                                <option value="Schedule Exit Inspection">Schedule Exit Inspection</option>
                                                            </select>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; float: left; margin: 20px 0; padding: 10px;" id="messageId">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; float: left">
                                                        <label for="txtMessage" class="col-sm-3 control-label">Message:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox TextMode="MultiLine" ID="txtMessage" Rows="7" Columns="20" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <input id="hdCommunicationId" type="hidden" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; float: left">
                                                        <label for="ddlSendMassageTo" class="col-sm-3 control-label">Send Message To:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtMessageTo" runat="server" CssClass="form-control  col-sm-12"></asp:TextBox>
                                                            <select id="ddlSendMassageTo" class="form-control ddl loadCoun col-sm-12" style="display: none;"></select>
                                                            <button type="button" id="btnAddMassage" class="btn btnNewColor col-sm-3" style="float: right; display: none;">Add</button>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="text-align: center; float: left">
                                                        <div class="form-group">
                                                            <label style="margin-right: 7px">
                                                                <input type="radio" id="wa" name="r5" class="flat-red" value="Maintenance Request" checked>
                                                                Maintenance Request
                                                            </label>
                                                            &nbsp;<label style="margin-right: 7px"><input type="radio" name="r5" value="Report Issue to the Manager" class="flat-red">
                                                                Report Issue to the Manager
                                                            </label>

                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center;">
                                                        <table id="tblMessageLst" class="table table-responsive  table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th>Date</th>
                                                                    <th>Receiver </th>
                                                                    <th>Massage</th>
                                                                    <%-- <th>Edit </th>--%>
                                                                    <th>Delete</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-12 btnSaveWebCenter">
                                            <div class="col-md-6" style="float: left">
                                                <asp:Button ID="Button1" runat="server" Text="< Back" OnClick="btnBack_Click" CssClass="btn btnNewColor " />
                                            </div>
                                            <div class="col-md-6" style="float: left">
                                                <button type="button" id="btnSend" class="btn btn-successNew ">Send</button>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="tab-pane" id="Documents">
                            <asp:UpdatePanel runat="server" ID="upDocements">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="row">
                                                    <div class="box-header with-border CommonHeader col-md-12">
                                                        <h3 class="box-title" id="H4" runat="server">Documents</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtDocumentId" class="col-sm-3 control-label">Document ID:</label>
                                                        <div class="col-sm-6">
                                                            <%--<select id="ddlTenantName" class="form-control ddl loadCoun"></select>--%>
                                                            <input type="text" id="txtDocumentId" readonly="readonly" class="form-control" />
                                                            <input type="hidden" id="hdDocumentId" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6" style="padding-left: 5px;">
                                                        <label for="txtDocumentDesc" class="col-sm-3 control-label">Document Description:</label>
                                                        <div class="col-sm-6">
                                                            <textarea id="txtDocumentDesc" rows="5" cols="10" class="form-control"></textarea>
                                                            <%--<asp:TextBox TextMode="MultiLine" ID="txtDocumentDesc" Rows="5" Columns="10" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6" style="padding-left: 5px;">
                                                        <label for="txtTypeOfDocument" class="col-sm-3 control-label">Type Of Document:</label>
                                                        <div class="col-sm-6">
                                                            <%--<select id="ddlTypeOfDocument" class="form-control ddl loadCoun col-sm-8"></select>--%>
                                                            <input type="text" id="txtTypeOfDocument" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6" style="text-align: center">
                                                        <label for="txtDateAdded" class="col-sm-3 control-label">Date Added:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtDateAdded" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center; margin-bottom: 10px; float: left">
                                                        <button type="button" id="btnAddMyFile" class="btn btnNewColor">Add From My FileIt</button>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center; float: left; font-size: 14px; font-weight: bold; margin-bottom: 28px;">
                                                        <label>
                                                            You download App from your Apple or Google Store called "myfilepe"  put in promo code "eprop365" Enter in comment
                                                    the above document ID. Eproperty365 will create a link to that document</label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center">
                                                        <table id="tblDocument" class="table table-responsive table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th>DocumentId</th>
                                                                    <th>Document Description</th>
                                                                    <th>Type Of Document</th>
                                                                    <th>Date Added</th>
                                                                    <th>Edit </th>
                                                                    <th>Delete</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="box-header with-border CommonHeader col-md-12">
                                                        <h3 class="box-title" id="Hk" runat="server">Rental / Lease Documents</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center">
                                                        <table id="tblDocumentFileList" class="table table-responsive table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th style='width: 15%'>Add/Remove</th>
                                                                    <th style='width: 20%'>Document Description</th>
                                                                    <th style='width: 15%'>File Name</th>
                                                                    <%--<th style='width: 30%'>Browse</th>--%>
                                                                    <th style='width: 15%'>Browse </th>
                                                                    <th style='width: 5%'>Action </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                            <%--<tfoot>
                                                            <tr>
                                                                <td colspan="4" style='width:5%'> <input type="button" id="btnBrownSite"  value="Browse Site Library"  class="btn btnNewColor " /></td>
                                                            </tr>
                                                            </tfoot>--%>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div style="padding-left: 5px; text-align: center; width: 90%;">
                                                        <table id="tbl_SiteLibery" class="table table-responsive table-bordered table-striped" style="max-height: 400px; overflow-x: auto; overflow-y: scroll; width: 90%;">
                                                            <thead>
                                                                <tr>
                                                                    <th style='width: 50%'>File Name</th>
                                                                    <th style='width: 50%'>File Path</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                            </tbody>
                                                        </table>
                                                        <%-- <div id="iframedis" class="col-md-12" style="display: none">
                                                        <object id="objid" data="" type="application/pdf">  BrowseSiteLibrary
                                                            <iframe src="" id="viewfile" style="width: 618px; height: 800px"></iframe>
                                                        </object>

                                                    </div>--%>
                                                    </div>
                                                    <%-- <div id="iframedis" class="col-md-12" style="display: none">
                                                        <object id="objid" data="" type="application/pdf">
                                                            <iframe src="" id="viewfile" style="width: 618px; height: 800px"></iframe>
                                                        </object>

                                                    </div>

                                                    <div id="iframeimage" class="col-md-12" style="display: none">
                                                        <img src="" id="ifrmImage" width="400" height="400" />
                                                    </div>--%>
                                                </div>



                                            </div>
                                        </div>
                                        <div class="col-md-12 btnSaveWebCenter">
                                            <%--<button type="button" id="btnBack1" class="btn btnNewColor">Back</button>--%>
                                            <asp:Button ID="btnBack" runat="server" Text="< Back" OnClick="btnBack_Click" CssClass="btn btnNewColor " />
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="tab-pane active" id="Specs">
                            <asp:UpdatePanel runat="server" ID="userpanel">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-header with-border CommonHeader col-md-12">
                                                    <h3 class="box-title" id="lblHeadline" runat="server">Residential Quick Features View</h3>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <span>To populate the default item for a New Residential Quick Features. Fill out all the above fields and Click “Save & Continue” and then click “Edit” under “action” column and the default items will appear.</span>
                                                    </div>

                                                </div>
                                                <div class="box-body bgimg">
                                                    <div>
                                                        <span id="addPress">Click on item to Edit or Delete. To add just enter new information and press "Add"</span>
                                                        <table id="FeatureName">
                                                            <tbody>
                                                            </tbody>
                                                        </table>
                                                        <label class="col-sm-3 control-label lblitemname">Item Name:</label>
                                                        <div class="col-sm-3 lblitemname">
                                                            <%--<asp:TextBox ID="txtFeatureName" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <input type="text" id="txtFeatureNameId" class="form-control" />
                                                            <input type="hidden" id="hdFeatureNameId" />
                                                        </div>
                                                        <%--<asp:Button ID="btnAddFeatureName" runat="server" Text="Add" OnClick="btnAddFeatureName_Click" CssClass="btn btnNewColor col-sm-2" />--%>
                                                        <button type="button" id="AddFeature" class="btn btnNewColor lblitemname">Add or Change</button>
                                                        <button type="button" id="deleteFeature" class="btn btnNewColor lblitemname">Delete</button>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-6">
                                                        <label for="txtTotalSize" class="col-sm-6 control-label">Total Size:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtTotalSize" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="txtTotalSizeValidator1" runat="server"
                                                                ControlToValidate="txtTotalSize" Display="Dynamic"
                                                                ErrorMessage="Total Size must be in number."
                                                                ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                                            &nbsp;<asp:RequiredFieldValidator ID="txtTotalSizeRequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtTotalSize" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Total Sizes required."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label for="txtNumberfloors" class="col-sm-6 control-label">Number floors:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtNumberfloors" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="txtNumberfloorsValidator1" runat="server"
                                                                ControlToValidate="txtNumberfloors" Display="Dynamic"
                                                                ErrorMessage="Number floors must be in number."
                                                                ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                                            &nbsp;<asp:RequiredFieldValidator ID="txtNumberfloorsRequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtNumberfloors" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Number floors required."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label for="ddlNumberofBaths" class="col-sm-6 control-label">Number of Baths:</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlNumberofBaths" CssClass="form-control" runat="server">
                                                                <asp:ListItem Value="-1">Select Number of Baths</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4 or More</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="ddlNumberofBathsValidator" runat="server" ControlToValidate="ddlNumberofBaths" InitialValue="-1" ErrorMessage="Please select Number of Baths" ForeColor="Red" ValidationGroup="rspecs"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label for="txtMonthlyRentAmount" class="col-sm-6 control-label">Monthly Rent Amount:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtMonthlyRentAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="txtMonthlyRentAmountValidator2" runat="server"
                                                                ControlToValidate="txtMonthlyRentAmount" Display="Dynamic"
                                                                ErrorMessage="Monthly Rent Amount must be in format 0.00."
                                                                ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                                            &nbsp;<asp:RequiredFieldValidator ID="txtMonthlyRentAmountRequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtMonthlyRentAmount" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Monthly Rent Amount required."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box-body" style="padding: 2px 15px;">
                                                    <div class="col-md-6">
                                                        <label for="txtLotSize" class="col-sm-6 control-label">Lot Size:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtLotSize" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="txtLotSizeRegularExpressionValidator3" runat="server"
                                                                ControlToValidate="txtLotSize" Display="Dynamic"
                                                                ErrorMessage="Lot Size must be in number."
                                                                ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                                            &nbsp;<asp:RequiredFieldValidator ID="txtLotSizeRequiredFieldValidator3" runat="server"
                                                                ControlToValidate="txtLotSize" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Lot Size required."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label for="ddlNumberBedrooms" class="col-sm-6 control-label">Number Bedrooms:</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlNumberBedrooms" CssClass="form-control" runat="server">
                                                                <asp:ListItem Value="-1">Select Number of Bedrooms</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4 or More</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="ddlNumberBedroomsValidator" runat="server" ControlToValidate="ddlNumberBedrooms" InitialValue="-1" ErrorMessage="Please select Number of Bedrooms" ForeColor="Red" ValidationGroup="rspecs"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="box-body" style="padding: 2px 15px;">
                                                    <div class="col-md-6">
                                                        <label for="txtApplicationFee" class="col-sm-6 control-label">Application Fee:</label>
                                                        <div class="col-sm-6">
                                                            <asp:HiddenField ID="hdnApplicationFee" runat="server" />
                                                            <asp:HiddenField ID="hdnScreenFee" runat="server" />
                                                            <asp:HiddenField ID="hdnTotalFee" runat="server" />

                                                            <asp:TextBox ID="txtApplicationFee" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="txtApplicationFeeValidator4" runat="server"
                                                                ControlToValidate="txtApplicationFee" Display="Dynamic"
                                                                ErrorMessage="Application Fee must be in format 0.00."
                                                                ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                                            &nbsp;<asp:RequiredFieldValidator ID="txtApplicationFeeRequiredFieldValidator4" runat="server"
                                                                ControlToValidate="txtApplicationFee" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Application Fee required."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-sm-6 control-label">Advertised Fee:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtAdvertised" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                ControlToValidate="txtAdvertised" Display="Dynamic"
                                                                ErrorMessage="Advertised Fee must be in format 0.00."
                                                                ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>

                                                   <%-- <div class="col-md-6">
                                                        <label class="col-sm-6 control-label">Shorten Web Page:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtShortWebUrl" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>--%>

                                                </div>
                                                <div class="box-body" style="padding: 0;">
                                                    <div class="col-md-3">
                                                        <label class="col-sm-12 control-label">Shorten Web Page:</label>
                                                    </div>
                                                    <div class="col-md-9" style="float: left; padding-left:0px;">
                                                        <asp:TextBox ID="txtShortWebUrl" runat="server" CssClass="col-sm-6 form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="box-body" style="padding: 0;">
                                                    <div class="col-md-3">
                                                        <label class="col-sm-12 control-label">Your Web Page:</label>
                                                    </div>
                                                    <div class="col-md-9" style="float: left;">
                                                        <span style="padding: 0;" class="col-sm-12 control-label" id="lblWebUrl" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-3" style="float: left; padding-left: 10px;">
                                                        <asp:CheckBox ID="chkIncludedStatement" runat="server" Text="Included Statement" CssClass="col-sm-12 form-check-inline" />
                                                    </div>
                                                    <div class="col-md-9" style="float: left;">
                                                        <label class="col-sm-12 control-label">
                                                            There will be  3% Extra charged each time you use Visa, Mastercard, Discover, Amex.</label>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-6">
                                                        <label for="txtAgentName" class="col-sm-6 control-label">Agent Name:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="txtAgentNameRequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtAgentName" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Agent Name required."></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtAgentPhoneNumber" class="col-sm-6 control-label">Agent's Phone Number:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtAgentPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="txtAgentPhoneNumberRequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtAgentPhoneNumber" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Agent's Phone Number required."></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label for="txtBroker" class="col-sm-6 control-label">Broker:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtBroker" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="txtBrokerRequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtBroker" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Broker required."></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtBrokerPhoneNumber" class="col-sm-6 control-label">Broker Phone Number:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtBrokerPhoneNumber" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="txtBrokerPhoneNumberRequiredFieldValidator3" runat="server"
                                                                ControlToValidate="txtBrokerPhoneNumber" Display="Dynamic" ValidationGroup="rspecs" ForeColor="Red"
                                                                ErrorMessage="Broker Phone Number required."></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-md-6" style="float: left;">
                                                        <label class="col-sm-6 control-label">Image on Web Page:</label>
                                                        <div class="col-sm-6">
                                                            <asp:RadioButtonList runat="server" ID="rdoUnitRentType" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1" Selected="True">For Rent</asp:ListItem>
                                                                <asp:ListItem Value="2">Rented</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6" style="float: left;">
                                                        <label class="col-sm-6 control-label">Perform Tenant Background Checking?</label>
                                                        <div class="col-sm-6">
                                                            <asp:RadioButtonList runat="server" ID="rdoScreening" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoScreening_SelectedIndexChanged">
                                                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="float: left; padding-left: 0px;">
                                                        <asp:TextBox ID="txtSpecialStatementBtm" placeholder="Special Statement to be included:" runat="server" CssClass="col-sm-24 form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-12 btnSaveWebCenter">
                                            <asp:Button ID="btnSaveSpecs" ValidationGroup="rspecs" runat="server" Text="Save Specs" CssClass="btn btnNewColor " OnClick="btnSaveSpecs_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane " id="WebPageImages">
                            <asp:UpdatePanel runat="server" ID="userpanelWebPageImages">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <asp:UpdatePanel ID="upnlImageVideoUpld" runat="server">
                                                    <ContentTemplate>
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <label for="txtTitleCaption" class="col-sm-2 control-label">Title Caption:</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtTitleCaption" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4 col-md-push-8">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label for="txtShortDescription" class="col-sm-2 control-label">Short Description:</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtShortDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-4 col-sm-push-8">
                                                                </div>
                                                            </div>

                                                            <div class="row" style="display: none;">
                                                                <label for="txtLongDescription" class="col-sm-2 control-label">Long Description:</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtLongDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-4 col-sm-push-8">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label for="fileUnitImageUpload" class="col-sm-2 control-label">Your Unit Image:</label>
                                                                <div class="col-sm-4">
                                                                    <%--<asp:FileUpload ID="fileUnitImageUpload" runat="server" AllowMultiple="True" />--%>
                                                                    <input type="file" id="fileUnitImageUpload" />
                                                                    <%--  <asp:RegularExpressionValidator ID="fileUnitImageUploadValidator" runat="server" ControlToValidate="fileUnitImageUpload" ValidationGroup="uplUnitImage"
                                                                        ErrorMessage="Only .gif, .jpg, .png, .tiff and .jpeg are allowed"
                                                                        ForeColor="Red" ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Jj][Ee][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])|.*\.([tT][iI][iI][fF])$)"></asp:RegularExpressionValidator>--%>
                                                                </div>
                                                                <div class="col-sm-4 col-sm-push-8">
                                                                    <input type="button" value="Upload Image" class="btn btnNewColor" onclick="SaveAllDetails();" />
                                                                    <%--<asp:Button ID="btnUnitImageUpload" runat="server" Text="Upload Image" accept="image/gif, image/jpeg, image/jpg, image/png, image/bmp, image/tiff" OnClick="btnUnitImageUpload_Click" ValidationGroup="uplUnitImage" />--%>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="display: none;">
                                                                <label class="col-sm-2 control-label">Image on Web Page:</label>
                                                                <div class="col-sm-6">
                                                                    <asp:RadioButtonList runat="server" ID="rdoRentType" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="ForRent" Selected="True">For Rent</asp:ListItem>
                                                                        <asp:ListItem Value="Rented">Rented</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="col-sm-4 col-sm-push-8">
                                                                </div>
                                                            </div>


                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table id="tblWebImage" style="margin: 10px 0px 10px 10px; border: 1px solid #373737;">
                                                                        <tbody>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label for="fileUnitImageUpload" class="col-sm-2 control-label">Video Clip:</label>
                                                                <div class="col-sm-4">
                                                                    <%--<asp:FileUpload ID="fileUnitImageUpload" runat="server" AllowMultiple="True" />--%>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                    <%--<input type="file" id="vieoClip" />--%>
                                                                    <%--  <asp:RegularExpressionValidator ID="fileUnitImageUploadValidator" runat="server" ControlToValidate="fileUnitImageUpload" ValidationGroup="uplUnitImage"
                                                                        ErrorMessage="Only .gif, .jpg, .png, .tiff and .jpeg are allowed"
                                                                        ForeColor="Red" ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Jj][Ee][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])|.*\.([tT][iI][iI][fF])$)"></asp:RegularExpressionValidator>--%>
                                                                </div>
                                                                <div class="col-sm-4 col-sm-push-8">
                                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btnNewColor" OnClientClick="return Validate()" OnClick="btnUpload_Click" />
                                                                    <video controls width="500px" id="vid" style="display: none"></video>
                                                                    <%--<input type="button" value="Upload Video" onclick="SaveVideo();" />--%>
                                                                    <%--<asp:Button ID="btnUnitImageUpload" runat="server" Text="Upload Image" accept="image/gif, image/jpeg, image/jpg, image/png, image/bmp, image/tiff" OnClick="btnUnitImageUpload_Click" ValidationGroup="
                                                                        " />--%>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label for="txtLoggDescriptionVideo" class="col-sm-2 control-label">Long Description:</label>
                                                                <div class="col-sm-10">

                                                                    <asp:TextBox TextMode="MultiLine" ID="txtLoggDescriptionVideo" runat="server" CssClass="form-control"></asp:TextBox>

                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <asp:GridView ID="gvVideo" ShowHeaderWhenEmpty="true" HeaderStyle-CssClass="bg-primary text-white" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                                        <EmptyDataTemplate>
                                                                            <div class="text-center">No Data Found <strong>Upload New Video</strong></div>
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="Unit Id" DataField="ResidentialUnitSerialId" />
                                                                            <asp:BoundField HeaderText="Video Name" DataField="VideoName" />
                                                                            <asp:TemplateField HeaderText="Videos">
                                                                                <ItemTemplate>
                                                                                    <video width="130" height="130" controls>  
                                                                                    <source src='<%#Eval("VideoPath")%>' type="video/mp4">  
                                                                                </video>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:DataList runat="server" ID="dtlImageVideo" RepeatColumns="3" RepeatDirection="Horizontal">
                                                                    <ItemTemplate>
                                                                        <div class="row">
                                                                            <div class="col-sm-6">
                                                                                <asp:Label ID="lblImageVideoId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                                <div class="col-md-2" style="float: left;">
                                                                                    <asp:CheckBox ID="chkIncludeImage" runat="server" Text="Use" CssClass="col-sm-12 form-check-inline" />
                                                                                </div>
                                                                                <div class="col-md-10" style="float: left;">
                                                                                    <label class="col-sm-12 control-label">
                                                                                    Use
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <asp:LinkButton ID="btnEditImageVideo" runat="server" Text="Edit" OnClick="btnEditImageVideo_Click"></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDeleteImageVideo" runat="server" OnClientClick='return confirm("Are you sure you want to delete?");' Text="Delete" OnClick="btnDeleteImageVideo_Click"></asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <%--<div class="row">
                                                                                <asp:Image runat="server" ID="Image1" AlternateText="<%# Eval("TitleCaption")%>" />
                                                                            </div>
                                                                            <div class="row">
                                                                                <asp:Label ID="lblTitleCaption" runat="server" Text='<%# Eval("TitleCaption") %>'></asp:Label>
                                                                            </div>--%>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-md-12 btnSaveWebCenter">
                                            <div class="col-md-6" style="float: left">
                                                <asp:Button ID="Button2" runat="server" Text="< Back" OnClick="btnBack_Click" CssClass="btn btnNewColor " />
                                            </div>
                                            <div class="col-md-6" style="float: left">
                                                <input type="button" style="display: none;" value="Create Unit Web Page" id="createWebPage" class="btn btn-successnew " />
                                                <asp:Button ID="Button5" runat="server" Text="Exit" OnClick="btnBack_Click" CssClass="btn btnNewColor " />
                                            </div>
                                        </div>
                                        <%--  <div class="col-md-12 btnSaveWebCenter">
                                            <span>You may click on the view link on the top to see what the page will look like.</span>
                                        </div>--%>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                    <%--<asp:AsyncPostBackTrigger ControlID="btnAsyncSave"/>--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="Equipment">
                            <asp:UpdatePanel runat="server" ID="UpdateEquipment">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="row">
                                                    <div class="box-header with-border CommonHeader col-md-12">
                                                        <h3 class="box-title" id="H2" runat="server">Equipment Unit</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center">
                                                        <table id="tblEquipmentAllData" class="table table-responsive table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th style='width: 15%'>Equipment Name</th>
                                                                    <th style='width: 20%'>Manufacture</th>
                                                                    <th style='width: 15%'>Purchase Date</th>
                                                                    <th style='width: 5%'>Edit </th>
                                                                    <th style='width: 5%'>Delete </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <table id="tblEqWebImage" style="margin: 10px 0px 10px 10px; border: 1px solid #373737;">
                                                            <tbody>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <label for="fileEqImageUpload" style="padding-left: 20px;" class="col-sm-2 control-label">Equipment Image:</label>
                                                    <div class="col-sm-6">
                                                        <input type="file" id="fileEqImageUpload" />
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px;">
                                                        <label for="txtPicDesc" class="col-sm-2 control-label">Picture Description:</label>
                                                        <div class="col-sm-6">
                                                            <%--<asp:TextBox ID="txtPicDesc" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <input type="text" id="txtPicDesc" class="form-control" />
                                                        </div>
                                                        <div class="col-sm-3 col-sm-push-8">
                                                            <input type="button" value="Upload" style="margin-left: 10px;" class="btn btnNewColor" onclick="SaveAll();" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="txtTypeOfEquipment" class="col-sm-6 control-label">*Type of Equipment:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtTypeOfEquipment" runat="server" onchange="TypeOfEquipmentChange(this)" CssClass="form-control"></asp:TextBox>
                                                            <input type="hidden" id="Id" />
                                                            <input type="hidden" id="Serial" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="txtModel" class="col-sm-6 control-label">*Model:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtModel" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="txtPurdate" class="col-sm-6 control-label">*Purchase Date:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtPurdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="txtManufacturer" class="col-sm-6 control-label">Manufacturer:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtManufacturer" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="txtManuPhone" class="col-sm-6 control-label">Manufacturer Phone #:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtManuPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="txtManuContactPerson" class="col-sm-6 control-label">Contact Person:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtManuContactPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtManuAddress" class="col-sm-4 control-label">Address:</label>
                                                        <div class="col-sm-8" style="float: left; padding-left: 2px">
                                                            <asp:TextBox TextMode="MultiLine" Rows="5" Columns="5" ID="txtManuAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtManuAddress1" class="col-sm-4 control-label">Address1:</label>
                                                        <div class="col-sm-8" style="float: left; padding-left: 2px">
                                                            <asp:TextBox ID="txtManuAddress1" TextMode="MultiLine" Rows="5" Columns="5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="ddlManuCountry" class="col-sm-4 control-label">*Country:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 2px">
                                                            <select id="ddlManuCountry" class="form-control ddl loadCoun"></select>
                                                            <%-- <asp:DropDownList ID="ddlManuCountry" CssClass="form-control ddl" runat="server">
                                                            </asp:DropDownList>--%>
                                                            <%--<asp:TextBox ID="txtManuCountry" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtManuRegion" class="col-sm-4 control-label">Region:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 2px">
                                                            <asp:TextBox ID="txtManuRegion" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="ddlManuCity" class="col-sm-6 control-label">*City:</label>
                                                        <div class="col-sm-6">
                                                            <%--  <select id="ddlManuCity" class="form-control ddl"></select>
                                                               <%--  <asp:DropDownList ID="ddlManuCity" CssClass="form-control ddl" runat="server">
                                                            </asp:DropDownList>--%>
                                                            <asp:TextBox ID="txtManuCity" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="ddlManuState" class="col-sm-6 control-label">State:</label>
                                                        <div class="col-sm-6">
                                                            <select id="ddlManuState" class="form-control ddl loadSt"></select>
                                                            <%-- <asp:DropDownList ID="ddlManuDtate" CssClass="form-control ddl" runat="server">
                                                            </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="txtManuZipCode" class="col-sm-6 control-label">*Zip Code:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtManuZipCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtManuEmail" class="col-sm-4 control-label">Email:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 2px">
                                                            <asp:TextBox ID="txtManuEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtManuWebSite" class="col-sm-4 control-label">Web Site:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 2px">
                                                            <asp:TextBox ID="txtManuWebSite" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2" style="padding-left: 0px">
                                                        <label for="txtManCity" class="col-sm-12 control-label">Purchase From:</label>
                                                    </div>
                                                    <div class="col-md-10" style="padding-left: 3px;">
                                                        <div class="form-group">
                                                            <label style="margin-right: 7px">
                                                                <input type="radio" id="di" name="r3" class="flat-red" value="Direct From Manf" checked>
                                                                Direct From Manf
                                                            </label>
                                                            &nbsp;<label style="margin-right: 7px"><input type="radio" name="r3" value="Dealer" id="Dealer" class="flat-red">
                                                                Dealer
                                                            </label>
                                                            &nbsp;<label style="margin-right: 7px"><input type="radio" name="r3" value="Agent" class="flat-red">
                                                                Agent
                                                            </label>
                                                            &nbsp;<label style="margin-right: 7px"><input type="radio" name="r3" value="Unknown" class="flat-red">
                                                                Unknown
                                                            </label>
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="txtPurName" class="col-sm-6 control-label">Name:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtPurName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="txtPurPhone" class="col-sm-6 control-label">Phone Number:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtPurPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="txtPurContactPerson" class="col-sm-6 control-label">Contact Person :</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtPurContactPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtPurAddress" class="col-sm-4 control-label">Address:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 2px">
                                                            <asp:TextBox TextMode="MultiLine" Rows="5" Columns="5" ID="txtPurAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtPurAddress1" class="col-sm-4 control-label">Address1:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 2px">
                                                            <asp:TextBox ID="txtPurAddress1" TextMode="MultiLine" Rows="5" Columns="5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="ddlPurCountry" class="col-sm-4 control-label">Country:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 5px">
                                                            <select id="ddlPurCountry" class="form-control ddl loadCoun"></select>
                                                            <%-- <asp:DropDownList ID="ddlPurCountry" CssClass="form-control ddl" runat="server">
                                                            </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtPurRegion" class="col-sm-4 control-label">Region:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 5px">
                                                            <asp:TextBox ID="txtPurRegion" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="txtPurCity" class="col-sm-6 control-label">City:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtPurCity" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <%--   <asp:DropDownList ID="ddlPurCity" CssClass="form-control ddl" runat="server">
                                                                </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="ddlPurState" class="col-sm-6 control-label">State:</label>
                                                        <div class="col-sm-6">
                                                            <select id="ddlPurState" class="form-control ddl loadSt"></select>
                                                            <%--  <asp:DropDownList ID="ddlPurState" CssClass="form-control ddl" runat="server">
                                                            </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="txtPurZipCode" class="col-sm-6 control-label">Zip Code:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtPurZipCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtPurEmail" class="col-sm-4 control-label">Email:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 5px">
                                                            <asp:TextBox ID="txtPurEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtPurWebSite" class="col-sm-4 control-label">Web Site:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 5px">
                                                            <asp:TextBox ID="txtPurWebSite" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label style="margin-right: 7px">
                                                                <input type="radio" id="wa" name="r4" class="flat-red" value="Warrenty" checked>
                                                                Warrenty
                                                            </label>
                                                            &nbsp;<label style="margin-right: 7px"><input type="radio" name="r4" value="No Warrenty" class="flat-red">
                                                                No Warrenty
                                                            </label>

                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">

                                                        <label for="txtPurNoYears" class="col-sm-6 control-label">No Years:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtPurNoYears" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label for="txtPurNoDays" class="col-sm-6 control-label">No Days:</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtPurNoDays" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="padding-left: 0px;">
                                                        <label for="txtWarrentyDesc" class="col-sm-2 control-label">Warrenty Description:</label>
                                                        <div class="col-sm-10" style="float: right; padding-left: 5px; padding-right: 0px;">
                                                            <asp:TextBox TextMode="MultiLine" ID="txtWarrentyDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-sm-6">
                                                            <a href="#">Get Document ID for Document systems</a>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ReadOnly="True" ID="txtDoumentId" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label for="txtWarrentyDesc" class="col-sm-4 control-label">Purchase Price:</label>
                                                        <div class="col-sm-8" style="float: right; padding-left: 5px">
                                                            <asp:TextBox ID="txtPurchasePrice" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-12 control-label" style="text-align: center !important; float: left; font-size: 16px; font-weight: bold; margin-top: 40px;">To Create Maintenance Schedule you need to the Maintenance Management Tab</label>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-md-12 btnSaveWebCenter">
                                            <div class="col-md-6" style="float: left">
                                                <asp:Button ID="Button3" runat="server" Text="< Back" OnClick="btnBack_Click" CssClass="btn btnNewColor " />
                                            </div>
                                            <div class="col-md-6" style="float: left">
                                                <button type="button" id="btnSaveEq" class="btn btn-successnew ">Add Equipment</button>
                                            </div>
                                        </div>
                                    </div>

                                    <%--  </div>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="WebAnalytics">
                            <asp:UpdatePanel runat="server" ID="UpdatePanelWebAnalytics">
                                <ContentTemplate>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <%-- <div class="row">
                                                    <div class="col-md-6" style="float: left">
                                                        <label for="ddlPostarea" class="col-sm-3 control-label">Post Area:</label>
                                                        <div class="col-sm-6">
                                                            <select id="ddlPostarea" class="form-control ddl">
                                                                <option value="-1">Select Area</option>
                                                                <option value="Local">Local</option>
                                                                <option value="Regional">Regional</option>
                                                                <option value="National">National</option>
                                                                <option value="Overseas">Overseas</option>
                                                            </select>
                                                        </div>

                                                    </div>
                                                </div>--%>

                                                <div class="row">
                                                    <div class="col-md-12" style="float: left; padding-left: 0px;">
                                                        <div class="col-md-6">
                                                            <label for="txtWebAnalyticsfFrom" class="col-sm-2 control-label">From:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtWebAnalyticsfFrom" class="form-control mainDate" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label for="txtWebAnalyticsfTo" class="col-sm-2 control-label">To:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtWebAnalyticsfTo" class="form-control mainDate" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12"></div>
                                                </div>
                                                <%-- <div class="row">
                                                    <div class="col-md-12" style="padding-left: 5px; text-align: center">
                                                        <table id="tblWebAnalytics" class="table table-responsive table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th>Owner</th>
                                                                    <th>Property Manager</th>
                                                                    <th>Property Location</th>
                                                                    <th>Unit</th>
                                                                    <th>No. of links posted.</th>
                                                                    <th>No. of views</th>
                                                                    <th>No. of Schedules</th>
                                                                    <th>No. of Applications</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>
                                                </div>--%>
                                                <%--<div class="row">
                                                    <div class="col-md-12" style="float: left; padding-left: 0px;">
                                                        <div class="col-md-6">
                                                            <label for="txtWebAnalyticslFrom" class="col-sm-2 control-label">From:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtWebAnalyticslFrom" class="form-control mainDate" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6" style="padding-right: 0px;">
                                                            <label for="txtWebAnalyticslTo" class="col-sm-2 control-label">To:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtWebAnalyticslTo" class="form-control mainDate" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div class="row">
                                                    <div class="col-md-4" style="float: left">
                                                        <label class="col-sm-6 control-label">No Listing Visitors:</label>
                                                    </div>
                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtMTDListing" class="col-sm-6 control-label">MTD Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" disabled="disabled" id="txtMTDListing" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtTotalListing" class="col-sm-6 control-label">Total Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" disabled="disabled" id="txtTotalListing" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4" style="float: left">
                                                        <label class="col-sm-6 control-label">No Email Registration's:</label>
                                                    </div>

                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtMTDViews" class="col-sm-6 control-label">MTD Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtMTDViews" disabled="disabled" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtTotalViews" class="col-sm-6 control-label">Total Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtTotalViews" disabled="disabled" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-4" style="float: left">
                                                        <label class="col-sm-6 control-label">No Request for viewing:</label>
                                                    </div>

                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtMTDSchedules" class="col-sm-6 control-label">MTD Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtMTDSchedules" disabled="disabled" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtTotalSchedules" class="col-sm-6 control-label">Total Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtTotalSchedules" disabled="disabled" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4" style="float: left">
                                                        <label class="col-sm-6 control-label">No Application Started:</label>
                                                    </div>

                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtMTDStarted" class="col-sm-6 control-label">MTD Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtMTDStarted" disabled="disabled" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtTotalStarted" class="col-sm-6 control-label">Total Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtTotalStarted" disabled="disabled" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-4" style="float: left">
                                                        <label class="col-sm-6 control-label">No Application Completed:</label>
                                                    </div>
                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtMTDCompleted" class="col-sm-6 control-label">MTD Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtMTDCompleted" disabled="disabled" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="float: left">
                                                        <label for="txtTotalCompleted" class="col-sm-6 control-label">Total Views:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" id="txtTotalCompleted" disabled="disabled" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>


                                                <%-- <div class="row">
                                                    <div class="col-md-12">
                                                        <div id="bar-chart"></div>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>
            </div>

        </div>

    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Content/js/pdfobject.js"></script>
    <%--<script type="text/javascript" src="../../Content/js/pdfobject.js"></script>--%>
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript">
        var G_buttonAdd = '<svg width="40" height="40"><line x1="20" y1="10" x2="20" y2="20"/>' +
            '<line x1="30" y1="20" x2="20" y2="20"/><line x1="20" y1="30" x2="20" y2="20" /><line x1="10" y1="20" x2="20" y2="20" />' +
            '<circle cx="20" cy="20" r="18"/></svg>';
        var G_buttonRmv = '<svg width="40" height="40">' +
                      '<line x1="30" y1="20" x2="20" y2="20"/><line x1="10" y1="20" x2="20" y2="20" />' +
                      '<circle cx="20" cy="20" r="18"/></svg>';
        var objectUrl;

        $(document).ready(function () {
            LoadAll();
            LoadCountry();
            LoadResidentialQuickFeaturesView();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            LoadAll();
            LoadResidentialQuickFeaturesView();
            LoadWebImage();
            LoadCountry();
            GetVendorQuoteId();
            LoadRentalDocumentGrid();
            GetDocumentId();
            LoadEquipmentGridData();
            //LoadSender();
            //LoadMessage();
            //LoadWebAnalyticsBarChart();
            BrowseSiteLibrary();
        });

        function LoadAll(parameters) {
            $(".mainDate").datepicker({
                dateFormat: "mm-dd-yy",
                changeYear: true,
                changeMonth: true
            });
            $("#txtDateAdded").datepicker({
                dateFormat: "mm-dd-yy",
                changeYear: true,
                changeMonth: true
            });
            $("#MainContent_txtPurdate").datepicker({
                dateFormat: "mm-dd-yy",
                changeYear: true,
                changeMonth: true
            });
            $(".ddl").select2();
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            $(document).on('change', '#MainContent_FileUpload1', function (e) {
                var file = e.currentTarget.files[0];
                objectUrl = URL.createObjectURL(file);
                $("#vid").prop("src", objectUrl);
            });
            //  LoadPropetyManagerId();
            LoadCommunicationGrid();
            LoadDocumentGrid();
            LoadMaintenanceGrid();
            LoadVendor();
            LoadSender();
            LoadMessage("", "");
        }


        function LoadCountry() {

            var pagePath = window.location.pathname + "/GetCountry";

            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error");
                    },
                success:
                    function (result) {
                        var lstofCountry = $.parseJSON(decodeURIComponent(result.d));
                        var content = setCombo(lstofCountry, '-1');
                        $("#ddlManuCountry option").empty();
                        $("#ddlManuCountry").append(content);
                        $("#ddlManuCountry").val("US").trigger('change');
                        $("#ddlPurCountry option").empty();
                        $("#ddlPurCountry").append(content);
                        $("#ddlPurCountry").val("US").trigger('change');
                        LoadState();
                    }
            });
        }
        function LoadState() {
            var pagePath = window.location.pathname + "/GetState";
            console.log(pagePath);
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error");
                    },
                success:
                    function (result) {
                        var lstofState = $.parseJSON(decodeURIComponent(result.d));
                        var content = setCombo(lstofState, '-1');
                        $("#ddlManuState option").empty();
                        $("#ddlManuState").append(content);
                        $("#ddlManuState").val("-1").trigger('change');

                        $("#ddlPurState option").empty();
                        $("#ddlPurState").append(content);
                        $("#ddlPurState").val("-1").trigger('change');
                        // LoadAllEquipmentData();
                    }
            });
        }
        function LoadCity() {
            var pagePath = window.location.pathname + "/GetCity";
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error");
                    },
                success:
                    function (result) {
                        var lstofCity = $.parseJSON(decodeURIComponent(result.d));
                        var content = setCombo(lstofCity, '-1');
                        $("#ddlManuCity option").empty();
                        $("#ddlManuCity").append(content);
                    }
            });
        }
        function LoadResidentialQuickFeaturesView() {
            var pagePath = window.location.pathname + "/GetResidentialQuickFeaturesView";
            var pManagerId = $.trim($("[id*=ddlPropertyManagerID]").val());
            var locationId = $.trim($("[id*=ddlLocationID]").val());
            var unitName = $.trim($("[id*=txtUnitName]").val());
            var typeOfUnit = $.trim($("[id*=txtUnitType]").val());
            var unitSerialId = $.trim($("[id*=txtUnitID]").val());
            //if ((pManagerId != "undefined" && pManagerId != "-1") && (locationId != "undefined" && locationId != "-1") && (unitName != "undefined" && unitName != "")
            //    && (typeOfUnit != "undefined" && typeOfUnit != "") && (unitSerialId != '' && unitSerialId != "undefined")) {
            var obj = {};
            obj.unitSerialId = unitSerialId;
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error");
                    },
                success:
                    function (result) {

                        var lstofFeatureName = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        $.each(lstofFeatureName, function (i, obj) {
                            var isChecked = "checked='checked'";
                            content += "<tr style='float:left;width:25%'>";
                            if (obj.isSelected === 'true') {
                                isChecked = "checked='checked'";
                            } else {
                                isChecked = "";
                            }

                            content += "<td><span class='col-sm-12' form-check-inline'><input " + isChecked + " id='" + obj.Id + "' name='chkFeatureName' class='chkf' type='checkbox'>" +
                                "<label>" + obj.FeatureName + "</label></span></td>";
                            content += "</tr>";
                        });
                        $("#FeatureName tbody").empty();
                        $("#FeatureName tbody").append(content);
                    }
            });
            //}
        }

        function LoadWebImage() {
            var pagePath = window.location.pathname + "/GetAllWebImageByUnitId";
            var pManagerId = $.trim($("[id*=ddlPropertyManagerID]").val());
            var locationId = $.trim($("[id*=ddlLocationID]").val());
            var unitName = $.trim($("[id*=txtUnitName]").val());
            var typeOfUnit = $.trim($("[id*=txtUnitType]").val());
            var unitSerialId = $.trim($("[id*=txtUnitID]").val());
            if (unitSerialId != '' && unitSerialId != "undefined") {
                var obj = {};
                obj.unitSerialId = unitSerialId;
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify(obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error");
                        },
                    success:
                        function (result) {

                            var lstofFeatureName = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";
                            $.each(lstofFeatureName, function (i, obj) {
                                var isChecked = "";
                                if (obj.IsUsed == true) {
                                    isChecked = "checked='checked'";
                                }
                                content += "<tr style='float:left;margin:15px;'>";
                                content += "<td><span class='col-sm-12' style='width: 50%;float: left;' form-check-inline'><input " + isChecked + " id='" + obj.Serial + "' data_unitId='" + obj.ResidentialUnitSerialId + "' name='chkisUse'    type='checkbox'>" +
                                    "<label>Use</label></span><span style='width: 45%;float: right;text-align: right;margin-right: 3%;'><input style='border: none;background: none;cursor: pointer;text-decoration: underline;' id='" + obj.Serial + "' data_unitId='" + obj.ResidentialUnitSerialId + "' type='button' value='Delete' onclick='Delete(this);' /></span><br/><div><img style='width: 285px;height: 177px;' src='" + obj.ImagePath + "' alt='" + obj.ImageName + "'></div></td>";
                                content += "</tr>";
                            });
                            $("#tblWebImage tbody").empty();
                            $("#tblWebImage tbody").append(content);
                        }

                });
            }

        }
        $(document).on('click', '#AddFeature', function (parameters) {
            var pagePath = window.location.pathname + "/test";
            if (validationADD()) {
                var itemName = $.trim($("[id*=txtFeatureNameId]").val());
                var featurehd = $("#hdFeatureNameId").val() == "" ? 0 : parseInt($("#hdFeatureNameId").val());
                if (itemName === "undefined" || itemName === "") {
                    $("#txtFeatureNameId").css({ 'border': '1px solid red' });
                }
                else {
                    $("#txtFeatureNameId").css({ 'border': '1px solid #d2d6de' });
                    var obj = {};
                    obj.value = $.trim($("[id*=txtFeatureNameId]").val());
                    obj.unitSerialId = $.trim($("[id*=txtUnitID]").val());
                    obj.hiddenfieldId = featurehd;
                    $.ajax({
                        type: "POST",
                        url: pagePath,
                        data: JSON.stringify(obj),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error:
                            function (XMLHttpRequest, textStatus, errorThrown) {
                                // alert("Error");
                                notify('danger', "Error");
                            },
                        success:
                            function (result) {
                                var lstofFeatureName = $.parseJSON(decodeURIComponent(result.d));
                                if (featurehd == "") {
                                    var content = "";
                                    //content += "<tr style='float:left'>";
                                    //content += "<td><span class='col-sm-12' form-check-inline'><input id='" + lstofFeatureName.Id + "' class='chkf'  name='chkFeatureName'type='checkbox'>" +
                                    //    "<label>" + lstofFeatureName.FeatureName + "</label></span></td>";
                                    //content += "</tr>";
                                    //$('#FeatureName tbody:last').append(content);
                                    //$.trim($("[id*=txtFeatureNameId]").val(""));
                                    notify('success', "Feature Name Added Successfully");
                                    //  $("#AddFeature").text("Add");
                                    $.trim($("[id*=txtFeatureNameId]").val(""));
                                    $("#hdFeatureNameId").val("");
                                    LoadResidentialQuickFeaturesView();
                                } else {
                                    notify('success', "Update  Successfully");
                                    $.trim($("[id*=txtFeatureNameId]").val(""));
                                    $("#hdFeatureNameId").val("");
                                    LoadResidentialQuickFeaturesView();
                                    //   $("#AddFeature").text("Add");
                                }

                                // $("#FeatureName tbody  tr").append(content);
                            }
                    });
                }

            } else {
                notify('danger', "Please fill red mark field");
                // alert("Please fill red mark field");
            }
        });
        $(document).on('click', '#deleteFeature', function (parameters) {
            var deleteFeature = "";
            if (show_confirm()) {
                //$('input[name="chkFeatureName"]:checked').each(function () {
                //    var id = $(this).attr('id');
                //    if (id>0) {
                //        deleteFeature += id.toString() + ",";
                //    }

                //});
                var del = $("#hdFeatureNameId").val();
                if (del > 0) {
                    deleteFeature = del.toString();
                }
                if (deleteFeature != "") {
                    var pagePath = window.location.pathname + "/DeleteFeatureItem";

                    $.ajax({
                        type: "POST",
                        url: pagePath,
                        data: "{ 'FeatureItem':'" + deleteFeature + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error:
                            function (XMLHttpRequest, textStatus, errorThrown) {
                                // alert("Error");
                                notify('danger', "Error");
                            },
                        success:
                            function (result) {
                                if (result.d === true) {
                                    notify('success', "Feature deleted Successfully");
                                    // alert("Delete Successfully");
                                    $("#txtFeatureNameId").val("");
                                    $("#hdFeatureNameId").val("");
                                    $("#AddFeature").text("Add or Change");
                                    LoadResidentialQuickFeaturesView();
                                } else {
                                    notify('danger', "Delete Failed !!");
                                    // alert("Delete Failed");
                                }
                            }
                    });
                } else {
                    //alert("Please select item to delete");
                    notify('danger', "Please Select Item To Delete");
                }
            }
        });
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
        $(".nav nav-tabs  li a").on("click", function () {
            $(".nav nav-tabs  li").find(".active").removeClass("active");
            $(this).addClass("active");
        });
        $(document).on('change', ".chkf", function () {
            //if ($(this).is(":checked") == true) {
            $("#txtFeatureNameId").val("");
            $("#hdFeatureNameId").val("");
            var tr = $(this).closest('tr');
            var id = $(this).attr('id');
            var text = $($($($($(tr).children('td:eq(0)').children())[0]).children())[1]).text();
            $("#txtFeatureNameId").val(text);
            $("#hdFeatureNameId").val(id);
            //  $("#AddFeature").text("Change");
            //}
            // else {
            //var id = $(this).attr('id');
            //$("#txtFeatureNameId").val("");
            //$("#hdFeatureNameId").val("");
            //$("#AddFeature").text("Add or Change");
            // }

        });

        function validationADD() {
            var isresult = true;
            //var pManagerId = $.trim($("[id*=ddlPropertyManagerID]").val());
            var locationId = $.trim($("[id*=ddlLocationID]").val());
            var unitName = $.trim($("[id*=txtUnitName]").val());
            var typeOfUnit = $.trim($("[id*=txtUnitType]").val());

            //if (pManagerId === "undefined" || pManagerId === "-1") {
            //    $("#MainContent_ddlPropertyManagerID").css({ 'border': '1px solid red' });
            //    isresult = false;
            //}
            //else {
            //    $("#MainContent_ddlPropertyManagerID").css({ 'border': '1px solid #d2d6de' });
            //}

            if (locationId === "undefined" || locationId === "-1") {
                $("#MainContent_ddlLocationID").css({ 'border': '1px solid red' });
                isresult = false;
            }
            else {
                $("#MainContent_ddlLocationID").css({ 'border': '1px solid #d2d6de' });
            }

            if (unitName === "undefined" || unitName === "") {
                $("#MainContent_txtUnitName").css({ 'border': '1px solid red' });
                isresult = false;
            }
            else {
                $("#MainContent_txtUnitName").css({ 'border': '1px solid #d2d6de' });
            }

            if (typeOfUnit === "undefined" || typeOfUnit === "") {
                $("#MainContent_txtUnitType").css({ 'border': '1px solid red' });
                isresult = false;
            }
            else {
                $("#MainContent_txtUnitType").css({ 'border': '1px solid #d2d6de' });
            }
            // });
            if (isresult)
                isresult = true;
            else {
                isresult = false;
            }
            return isresult;

        }

        var G_ImageName = "";
        //----- Web Page & Image -----//
        function SaveAllDetails() {
            if (document.getElementById("fileUnitImageUpload").value != "") {
                var file = document.getElementById('fileUnitImageUpload').files[0];
                G_ImageName = file.name;
                var fileName = document.getElementById("fileUnitImageUpload").value;
                var idxDot = fileName.lastIndexOf(".") + 1;
                var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
                if (extFile == "jpg" || extFile == "jpeg" || extFile == "png" || extFile == 'gif' || extFile == 'tiff') {
                    //TO DO
                    var reader = new FileReader();
                    reader.readAsDataURL(file);
                    reader.onload = UpdateFiles;
                } else {
                    //alert("Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
                    notify('danger', "Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
                    $("#fileUnitImageUpload").val("");
                }

            }
            else {
                //alert('Please Choose An Image');
                notify('danger', "Please Choose An Image");
            }
        }
        function SaveAll() {
            if (document.getElementById("fileEqImageUpload").value != "") {
                var file = document.getElementById('fileEqImageUpload').files[0];
                G_ImageName = file.name;
                var fileName = document.getElementById("fileEqImageUpload").value;
                var idxDot = fileName.lastIndexOf(".") + 1;
                var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
                if (extFile == "jpg" || extFile == "jpeg" || extFile == "png" || extFile == 'gif' || extFile == 'tiff') {
                    //TO DO
                    var reader = new FileReader();
                    reader.readAsDataURL(file);
                    reader.onload = UpdateFilesEq;
                } else {
                    //alert("Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
                    notify('danger', "Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
                    $("#fileUnitImageUpload").val("");
                }

            }
            else {
                //alert('Please Choose An Image');
                notify('danger', "Please Choose An Image");
            }
        }

        function UpdateFiles(evt) {
            if (validationADD()) {
                var pagePath = window.location.pathname + "/WebPageImage";
                var result = evt.target.result;
                var ImageSave = result.replace("data:image/jpeg;base64,", "");

                var TitleCap = $.trim($("[id*=txtTitleCaption]").val());
                var ShortDesc = $.trim($("[id*=txtShortDescription]").val());
                var longDesc = $.trim($("[id*=txtLongDescription]").val());
                if (TitleCap === "undefined" || TitleCap === "") {
                    $("#MainContent_txtTitleCaption").css({ 'border': '1px solid red' });
                } else {
                    $("#MainContent_txtTitleCaption").css({ 'border': '1px solid #d2d6de' });
                    if (ShortDesc === "undefined" || ShortDesc === "") {
                        $("#MainContent_txtShortDescription").css({ 'border': '1px solid red' });
                    } else {
                        $("#MainContent_txtShortDescription").css({ 'border': '1px solid #d2d6de' });
                        $.ajax({
                            type: "POST",
                            url: pagePath,
                            data: "{ 'Image':'" + ImageSave + "' , 'ImageName':'" + G_ImageName + "' ,'TitleCap':'" + TitleCap + "','ShortDesc':'" + ShortDesc + "','longDesc':'" + longDesc + "' }",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            error:
                                function (XMLHttpRequest, textStatus, errorThrown) {
                                    alert("Error");
                                    G_ImageName = "";
                                },
                            success:
                                function (result) {
                                    G_ImageName = "";
                                    var obj = $.parseJSON(decodeURIComponent(result.d));
                                    var content = "";
                                    content += "<tr style='float:left;margin:15px;'>";
                                    content += "<td><span class='col-sm-12' style='width: 50%;float: left;' form-check-inline'><input id='" + obj.Serial + "' data_unitId='" + obj.ResidentialUnitSerialId + "' name='chkisUse' type='checkbox'>" +
                                        "<label>Use</label></span><span style='width: 45%;float: right;text-align: right;margin-right: 3%;'><input style='border: none;background: none;cursor: pointer;text-decoration: underline;' id='" + obj.Serial + "' data_unitId='" + obj.ResidentialUnitSerialId + "' type='button' value='Delete' onclick='Delete(this);' /></span><br/><div><img style='width: 285px;height: 177px;' src='" + obj.ImagePath + "' alt='" + obj.ImageName + "'></div></td>";
                                    content += "</tr>";
                                    $("#tblWebImage tbody").append(content);
                                    clearWebImage();
                                    notify('success', "Web Image Uploaded Successfully");
                                }

                        });
                    }
                }

            } else {
                //  alert("please fill up red field");
                notify('danger', "Please fill red mark field");
            }

        }
        function UpdateFilesEq(evt) {
            var ImageSave = "";
            if (validationADD()) {
                var pagePath = window.location.pathname + "/WebPageImageEquipment";
                var result = evt.target.result;
                if (G_ImageName == "jpg" || G_ImageName == "jpeg" || G_ImageName == 'gif' || G_ImageName == 'tiff') {

                    ImageSave = result.replace("data:image/jpeg;base64,", "");
                    // console.log(result);
                    //ImageSave = result.replace("data:image/'" + G_ImageName + "';base64,", "");
                } else if (G_ImageName == "pdf") {

                    //console.log(result);
                    ImageSave = result.replace("data:application/pdf;base64,", "");
                } else if (G_ImageName == "png") {
                    ImageSave = result.replace("data:image/png;base64,", "");
                }
                // var ImageSave = result.replace("data:image/jpeg;base64,", "");
                var PicDesc = $.trim($("[id*=txtPicDesc]").val());
                if (PicDesc === "undefined" || PicDesc === "") {
                    $("#txtPicDesc").css({ 'border': '1px solid red' });
                } else {
                    $("#txtPicDesc").css({ 'border': '1px solid #d2d6de' });
                    $.ajax({
                        type: "POST",
                        url: pagePath,
                        data: "{ 'Image':'" + ImageSave + "' , 'ImageName':'" + G_ImageName + "' ,'PicDesc':'" + PicDesc + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error:
                            function (XMLHttpRequest, textStatus, errorThrown) {
                                alert("Error");
                                G_ImageName = "";
                            },
                        success:
                            function (result) {
                                G_ImageName = "";
                                var obj = $.parseJSON(decodeURIComponent(result.d));
                                var content = "";
                                content += "<tr style='float:left;margin:15px;'>";
                                content += "<td><div><img style='width: 285px;height: 177px;' src='" + obj.ImagePath + "' alt='" + obj.ImageName + "'></div><br/><span>" + obj.Description + "</span></td>";
                                content += "</tr>";
                                $("#tblEqWebImage tbody").append(content);
                                $("[id*=txtPicDesc]").val("");
                                $("#fileEqImageUpload").val("");
                                notify('success', "Equipment Image Uploaded Successfully");
                            }

                    });
                }

            } else {
                //  alert("please fill up red field");
                notify('danger', "Please fill red mark field");
            }

        }
        //---------- Upload Video --------------//

        function SaveVideo(parameters) {
            var pagePath = window.location.pathname + "/WebPageVideo";
            if (document.getElementById("vieoClip").value != "") {
                var file = document.getElementById('vieoClip').files[0];
                var file1 = document.getElementById('vieoClip').value;

                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = UpdateFiles2;
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: "{ 'vFile':'" + file + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error");
                            // G_ImageName = "";
                        },
                    success:
                        function (result) {
                            notify('success', "Video Uploaded Successfully");
                        }

                });
            }
            else {
                //alert('Please Choose An Image');
                notify('danger', "Please Choose An Image");
            }
        }
        function UpdateFiles2(evt) {
            var pagePath = window.location.pathname + "/WebPageImage";
            var result = evt.target.result;
            var ImageSave = result.replace("data:image/jpeg;base64,", "");
        }
        // --------- Upload video end ----------//
        function Delete(row) {
            var serial = $(row).attr('id');
            var unitid = $(row).attr('data_unitId');
            var pagePath = window.location.pathname + "/DeleteWebImage";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: "{ 'serial':'" + serial + "' , 'unitid':'" + unitid + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        //  alert("Error");
                        G_ImageName = "";
                        notify('danger', "Delete Failed");
                    },
                success:
                    function (result) {
                        G_ImageName = "";
                        var lstofFeatureName = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        $.each(lstofFeatureName, function (i, obj) {
                            var isChecked = "";
                            if (obj.IsUsed == true) {
                                isChecked = "checked='checked'";
                            }
                            //var isChecked = obj.IsUsed == true ? 'checked' : "";
                            content += "<tr style='float:left;margin:15px;'>";
                            content += "<td><span class='col-sm-12' style='width: 50%;float: left;' form-check-inline'><input " + isChecked + " id='" + obj.Serial + "' data_unitId='" + obj.ResidentialUnitSerialId + "' name='chkisUse'    type='checkbox'>" +
                                "<label>Use</label></span><span style='width: 45%;float: right;text-align: right;margin-right: 3%;'><input style='border: none;background: none;cursor: pointer;text-decoration: underline;' id='" + obj.Serial + "' data_unitId='" + obj.ResidentialUnitSerialId + "' type='button' value='Delete' onclick='Delete(this);' /></span><br/><div><img style='width: 285px;height: 177px;' src='" + obj.ImagePath + "' alt='" + obj.ImageName + "'></div></td>";
                            content += "</tr>";
                        });
                        $("#tblWebImage tbody").empty();
                        $("#tblWebImage tbody").append(content);
                        notify('success', "deleted Successfully");
                    }
            });
        }

        function clearWebImage(parameters) {
            $("[id*=txtTitleCaption]").val("");
            $("[id*=txtShortDescription]").val("");
            $("[id*=txtLongDescription]").val("");
            $("#fileUnitImageUpload").val("");
        }
        function Validate() {
            var seconds = $("#vid")[0].duration;
            if (seconds > 120) {
                alert('Video duration should be less or equal than 2 min');
            } else {

                Page_ClientValidate();
                return Page_IsValid;
            }
            return false;

        }

        $(document).on('click', '#createWebPage', function (parameters) {
            var isUse = "";
            $('input[name="chkisUse"]:checked').each(function () {
                var id = $(this).attr('id');
                isUse += id.toString() + ",";
            });

            if (isUse != "") {
                var pagePath = window.location.pathname + "/CreateWebPage";

                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: "{ 'updateIsUse':'" + isUse + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            // alert("Error");
                            notify('danger', "Web Page Creation Failed !!");
                        },
                    success:
                        function (result) {

                            if (result.d === "true") {
                                // alert("Save Successfully");
                                notify('success', "Saved Successfully");
                            } else {
                                // alert("Save Failed");
                                notify('danger', "Web Page Creation Failed !!");
                            }

                        }

                });
            }
            else {
                notify('danger', "No Image is selected to use for unit web page template !!");
            }
        });
        $(document).on('click', '#btnSaveEq', function (parameters) {

            if (validationADD()) {
                if (validateEquipment()) {
                    //isresultEq = true;
                    var obj = GetEqObject();
                    var pagePath = window.location.pathname + "/SaveEquipment";

                    $.ajax({
                        type: "POST",
                        url: pagePath,
                        data: JSON.stringify({ "obj": obj }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error:
                            function (XMLHttpRequest, textStatus, errorThrown) {
                                // alert("Error");
                                notify('danger', "Equipment Saved Failed !!");
                            },
                        success:
                            function (result) {
                                if (result.d === "true") {
                                    // alert("Save Successfully");
                                    if ($("#btnSaveEq").text() == 'Update') {
                                        notify('success', "Updated Successfully");
                                    } else {
                                        notify('success', "Saved Successfully");
                                    }
                                    ClearEqData();
                                    LoadEquipmentGridData();
                                    // LoadResidentialQuickFeaturesView();
                                    //ClearEqData();
                                    $("#btnSaveEq").text("Save Equipment");
                                } else {
                                    // alert("Save Failed");
                                    notify('danger', "Equipment Saved Failed !!");
                                }

                            }

                    });
                } else {
                    notify('danger', "Please fill out red mark field !!");
                }
            }
        });

        function GetEqObject(parameters) {
            var obj = {
                Id: $("#Id").val() == "" ? 0 : $("#Id").val(),
                Serial: $("#Serial").val() == "" ? 0 : $("#Serial").val(),
                ResidentialUnitSerialId: $.trim($("[id*=MainContent_txtUnitID]").val()) == "" ? 0 : $.trim($("[id*=MainContent_txtUnitID]").val()),//MainContent_txtUnitID
                TypeOfEquipment: $.trim($("[id*=MainContent_txtTypeOfEquipment]").val()),
                Model: $.trim($("[id*=MainContent_txtModel]").val()),
                PurDate: $.trim($("[id*=MainContent_txtPurdate]").val()),
                Manufaturer: $.trim($("[id*=MainContent_txtManufacturer]").val()),
                ManuFacturerPhone: $.trim($("[id*=MainContent_txtManuPhone]").val()),
                ContactPerson: $.trim($("[id*=MainContent_txtManuContactPerson]").val()),
                ManuAddress: $.trim($("[id*=MainContent_txtManuAddress]").val()),
                ManuAddress1: $.trim($("[id*=MainContent_txtManuAddress1]").val()),
                ManuCountryId: $("#ddlManuCountry").val(),
                ManuRegion: $.trim($("[id*=MainContent_txtManuRegion]").val()),
                ManuCity: $.trim($("[id*=MainContent_txtManuCity]").val()),
                ManuState: $("#ddlManuState").val(),
                ManuZipCode: $.trim($("[id*=MainContent_txtManuZipCode]").val()),
                ManuEmail: $.trim($("[id*=MainContent_txtManuEmail]").val()),
                ManuWebSite: $.trim($("[id*=MainContent_txtManuWebSite]").val()),
                PurchaseFrom: $('input[name=r3]:checked').val(),
                PurName: $.trim($("[id*=MainContent_txtPurName]").val()),
                PurPhone: $.trim($("[id*=MainContent_txtPurPhone]").val()),
                PurContactPerson: $.trim($("[id*=MainContent_txtPurContactPerson]").val()),
                PurAddress: $.trim($("[id*=MainContent_txtPurAddress]").val()),
                PurAddress1: $.trim($("[id*=MainContent_txtPurAddress1]").val()),
                PurCountryId: $("#ddlPurCountry").val(),
                PurRegion: $.trim($("[id*=MainContent_txtPurRegion]").val()),
                PurCity: $.trim($("[id*=txtPurCity]").val()),
                PurState: $("#ddlPurState").val(),
                PurZipCode: $.trim($("[id*=MainContent_txtPurZipCode]").val()),
                PurEmail: $.trim($("[id*=MainContent_txtPurEmail]").val()),
                PurWebSite: $.trim($("[id*=MainContent_txtPurWebSite]").val()),
                Warrenty: $('input[name=r4]:checked').val(),
                NoYears: $.trim($("[id*=MainContent_txtPurNoYears]").val()) == "" ? 0 : $.trim($("[id*=MainContent_txtPurNoYears]").val()),
                NoDays: $.trim($("[id*=MainContent_txtPurNoDays]").val()) == "" ? 0 : $.trim($("[id*=MainContent_txtPurNoDays]").val()),
                WarrentyDesc: $.trim($("[id*=MainContent_txtWarrentyDesc]").val()),
                DocumentId: $.trim($("[id*=MainContent_txtDoumentId]").val()) == "" ? 0 : $.trim($("[id*=MainContent_txtDoumentId]").val()),
                PurchasePrice: $.trim($("[id*=MainContent_txtPurchasePrice]").val())
            }
            return obj;
        }

        function validateEquipment() {
            var isresultEq = true;
            var typeofEq = $.trim($("[id*=MainContent_txtTypeOfEquipment]").val());
            var model = $.trim($("[id*=MainContent_txtModel]").val());
            var purDate = $.trim($("[id*=MainContent_txtPurdate]").val());
            var manufacturer = $.trim($("[id*=MainContent_txtManufacturer]").val());

            //MainContent_TextBox2
            var ManuPhone = $.trim($("[id*=MainContent_txtManuPhone]").val());
            var ManuContactPerson = $.trim($("[id*=MainContent_txtManuContactPerson]").val());
            var ManuAddress = $.trim($("[id*=MainContent_txtManuAddress]").val());
            var ManuCountry = $.trim($("[id*=ddlManuCountry]").val());
            var ManuRegion = $.trim($("[id*=MainContent_txtManuRegion]").val());
            var ManuCity = $.trim($("[id*=MainContent_txtManuCity]").val());
            var ManuState = $.trim($("[id*=ddlManuState]").val());
            var ManuZip = $.trim($("[id*=MainContent_txtManuZipCode]").val());
            var ManuEmail = $.trim($("[id*=MainContent_txtManuEmail]").val());
            var ManuWeb = $.trim($("[id*=MainContent_txtManuWebSite]").val());
            var PurchaseFrom = $.trim($('input[name=r3]:checked').val());
            var Purname = $.trim($("[id*=MainContent_txtPurName]").val());
            var PurPhone = $.trim($("[id*=MainContent_txtPurPhone]").val());
            var PurContPerson = $.trim($("[id*=MainContent_txtPurContactPerson]").val());
            var PurAddress = $.trim($("[id*=MainContent_txtPurAddress]").val());
            var PurCountry = $.trim($("[id*=ddlPurCountry]").val());
            var PurRegion = $.trim($("[id*=MainContent_txtPurRegion]").val());
            var PurCity = $.trim($("[id*=txtPurCity]").val());
            var PurState = $.trim($("[id*=ddlPurState]").val());
            var Purzip = $.trim($("[id*=MainContent_txtPurZipCode]").val());
            var PurEmail = $.trim($("[id*=MainContent_txtPurEmail]").val());
            var PurWebSite = $.trim($("[id*=MainContent_txtPurWebSite]").val());
            var PurYears = $.trim($("[id*=MainContent_txtPurNoYears]").val());
            var PurDays = $.trim($("[id*=MainContent_txtPurNoDays]").val());
            var DocId = $.trim($("[id*=MainContent_txtDoumentId]").val());
            var PurPrice = $.trim($("[id*=MainContent_txtPurchasePrice]").val());

            if (typeofEq === "undefined" || typeofEq === "") {
                $("#MainContent_txtTypeOfEquipment").css({ 'border': '1px solid red' });
                isresultEq = false;
            }
            else {
                $("#MainContent_txtTypeOfEquipment").css({ 'border': '1px solid #d2d6de' });
            }

            if (model === "undefined" || model === "") {
                $("#MainContent_txtModel").css({ 'border': '1px solid red' });
                isresultEq = false;
            }
            else {
                $("#MainContent_txtModel").css({ 'border': '1px solid #d2d6de' });
            }

            if (purDate === "undefined" || purDate === "") {
                $("#MainContent_txtPurdate").css({ 'border': '1px solid red' });
                isresultEq = false;
            }
            else {
                $("#MainContent_txtPurdate").css({ 'border': '1px solid #d2d6de' });
            }

            //if (manufacturer === "undefined" || manufacturer === "") {
            //    $("#MainContent_txtManufacturer").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtManufacturer").css({ 'border': '1px solid #d2d6de' });
            //}


            //if (ManuPhone === "undefined" || ManuPhone === "") {
            //    $("#MainContent_txtManuPhone").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtManuPhone").css({ 'border': '1px solid #d2d6de' });
            //}
            //if (ManuContactPerson === "undefined" || ManuContactPerson === "") {
            //    $("#MainContent_txtManuContactPerson").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtManuContactPerson").css({ 'border': '1px solid #d2d6de' });
            //}
            //if (ManuAddress === "undefined" || ManuAddress === "") {
            //    $("#MainContent_txtManuAddress").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtManuAddress").css({ 'border': '1px solid #d2d6de' });
            //}

            if (ManuCountry === "undefined" || ManuCountry === "-1") {
                $("#ddlManuCountry").css({ 'border': '1px solid red' });
                isresultEq = false;
            }
            else {
                $("#ddlManuCountry").css({ 'border': '1px solid #d2d6de' });
            }

            //if (ManuRegion === "undefined" || ManuRegion === "") {
            //    $("#MainContent_txtManuRegion").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtManuRegion").css({ 'border': '1px solid #d2d6de' });
            //}

            if (ManuCity === "undefined" || ManuCity === "") {
                $("#MainContent_txtManuCity").css({ 'border': '1px solid red' });
                isresultEq = false;
            }
            else {
                $("#MainContent_txtManuCity").css({ 'border': '1px solid #d2d6de' });
            }
            if (ManuState === "undefined" || ManuState === "-1") {
                $("#ddlManuState").css({ 'border': '1px solid red' });
                isresultEq = false;
            }
            else {
                $("#ddlManuState").css({ 'border': '1px solid #d2d6de' });
            }
            if (ManuZip === "undefined" || ManuZip === "") {
                $("#MainContent_txtManuZipCode").css({ 'border': '1px solid red' });
                isresultEq = false;
            }
            else {
                $("#MainContent_txtManuZipCode").css({ 'border': '1px solid #d2d6de' });
            }

            //if (ManuEmail === "undefined" || ManuEmail === "") {
            //    $("#MainContent_txtManuEmail").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtManuEmail").css({ 'border': '1px solid #d2d6de' });
            //}
            //if (ManuWeb === "undefined" || ManuWeb === "") {
            //    $("#MainContent_txtManuWebSite").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtManuWebSite").css({ 'border': '1px solid #d2d6de' });
            //}

            if (PurchaseFrom === "Dealer" || PurchaseFrom === "Agent") {
                if (Purname === "undefined" || Purname === "") {
                    $("#MainContent_txtPurName").css({ 'border': '1px solid red' });
                    isresultEq = false;
                }
                else {
                    $("#MainContent_txtPurName").css({ 'border': '1px solid #d2d6de' });
                }
                if (PurPhone === "undefined" || PurPhone === "") {
                    $("#MainContent_txtPurPhone").css({ 'border': '1px solid red' });
                    isresultEq = false;
                }
                else {
                    $("#MainContent_txtPurPhone").css({ 'border': '1px solid #d2d6de' });
                }

                //if (PurContPerson === "undefined" || PurContPerson === "") {
                //    $("#MainContent_txtPurContactPerson").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#MainContent_txtPurContactPerson").css({ 'border': '1px solid #d2d6de' });
                //}
                //if (PurAddress === "undefined" || PurAddress === "") {
                //    $("#MainContent_txtPurAddress").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#MainContent_txtPurAddress").css({ 'border': '1px solid #d2d6de' });
                //}
                //if (PurCountry === "undefined" || PurCountry === "-1") {
                //    $("#ddlPurCountry").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#ddlPurCountry").css({ 'border': '1px solid #d2d6de' });
                //}
                //if (PurRegion === "undefined" || PurRegion === "") {
                //    $("#MainContent_txtPurRegion").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#MainContent_txtPurRegion").css({ 'border': '1px solid #d2d6de' });
                //}
                //if (PurCity === "undefined" || PurCity === "-1") {
                //    $("#txtPurCity").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#txtPurCity").css({ 'border': '1px solid #d2d6de' });
                //}
                //if (PurState === "undefined" || PurState === "-1") {
                //    $("#ddlPurState").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#ddlPurState").css({ 'border': '1px solid #d2d6de' });
                //}
                //if (Purzip === "undefined" || Purzip === "") {
                //    $("#MainContent_txtPurZipCode").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#MainContent_txtPurZipCode").css({ 'border': '1px solid #d2d6de' });
                //}
                //if (PurEmail === "undefined" || PurEmail === "") {
                //    $("#MainContent_txtPurEmail").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#MainContent_txtPurEmail").css({ 'border': '1px solid #d2d6de' });
                //}

                //if (PurWebSite === "undefined" || PurWebSite === "") {
                //    $("#MainContent_txtPurWebSite").css({ 'border': '1px solid red' });
                //    isresultEq = false;
                //}
                //else {
                //    $("#MainContent_txtPurWebSite").css({ 'border': '1px solid #d2d6de' });
                //}

            }

            //if (PurYears === "undefined" || PurYears === "-1") {
            //    $("#MainContent_txtPurNoYears").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtPurNoYears").css({ 'border': '1px solid #d2d6de' });
            //}
            //if (PurDays === "undefined" || PurDays === "-1") {
            //    $("#MainContent_txtPurNoDays").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtPurNoDays").css({ 'border': '1px solid #d2d6de' });
            //}
            //if (DocId === "undefined" || DocId === "") {
            //    $("#MainContent_txtDoumentId").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtDoumentId").css({ 'border': '1px solid #d2d6de' });
            //}
            //if (PurPrice === "undefined" || PurPrice === "") {
            //    $("#MainContent_txtPurchasePrice").css({ 'border': '1px solid red' });
            //    isresultEq = false;
            //}
            //else {
            //    $("#MainContent_txtPurchasePrice").css({ 'border': '1px solid #d2d6de' });
            //}
            //PurPrice

            if (isresultEq)
                isresultEq = true;
            else {
                isresultEq = false;
            }
            return isresultEq;

        }

        function ClearEqData(parameters) {
            //$.trim($("[id*=Id]").val(0));
            //$.trim($("[id*=Serial]").val(0));
            //$.trim($("[id*=MainContent_txtUnitID]").val(0)); //MainContent_txtUnitID
            $("#tblEqWebImage tbody").empty();
            $.trim($("[id*=MainContent_txtTypeOfEquipment]").val(""));
            $.trim($("[id*=MainContent_txtModel]").val(""));
            $.trim($("[id*=MainContent_txtPurdate]").val(""));
            $.trim($("[id*=MainContent_txtManufacturer]").val(""));
            $.trim($("[id*=MainContent_txtManuPhone]").val(""));
            $.trim($("[id*=MainContent_txtManuContactPerson]").val(""));
            $.trim($("[id*=MainContent_txtManuAddress]").val(""));
            $.trim($("[id*=MainContent_txtManuAddress1]").val(""));
            $.trim($("[id*=ddlManuCountry]").val("US"));
            $.trim($("[id*=MainContent_txtManuRegion]").val(""));
            $.trim($("[id*=txtManuCity]").val(""));
            $.trim($("[id*=ddlManuState]").val("-1"));
            $.trim($("[id*=MainContent_txtManuZipCode]").val(""));
            $.trim($("[id*=MainContent_txtManuEmail]").val(""));
            $.trim($("[id*=MainContent_txtManuWebSite]").val(""));
            $("#di").prop('checked', true);
            $.trim($("[id*=MainContent_txtPurName]").val("")),
                $.trim($("[id*=MainContent_txtPurPhone]").val(""));
            $.trim($("[id*=MainContent_txtPurContactPerson]").val(""));
            $.trim($("[id*=MainContent_txtPurAddress]").val(""));
            $.trim($("[id*=MainContent_txtPurAddress1]").val(""));
            $.trim($("[id*=ddlPurCountry]").val("US"));
            $.trim($("[id*=MainContent_txtPurRegion]").val("")),
                $.trim($("[id*=txtPurCity]").val(""));
            $.trim($("[id*=ddlPurState]").val("-1"));
            $.trim($("[id*=MainContent_txtPurZipCode]").val(""));
            $.trim($("[id*=MainContent_txtPurEmail]").val(""));
            $.trim($("[id*=MainContent_txtPurWebSite]").val(""));
            $("#wa").prop("checked", true);
            $.trim($("[id*=MainContent_txtPurNoYears]").val(""));
            $.trim($("[id*=MainContent_txtPurNoDays]").val(""));
            $.trim($("[id*=MainContent_txtWarrentyDesc]").val(""));
            $.trim($("[id*=MainContent_txtDoumentId]").val(""));
            $.trim($("[id*=MainContent_txtPurchasePrice]").val(""));
        }

        function LoadEquipmentGridData() {
            var pagePath = window.location.pathname + "/LoadEquipmentGrid";
            //$($(this).find('td:eq(6)').find('input')).attr('id'),
            var currentObj = {
                "Id": 0,
                "OwnerId": $("#MainContent_ddlOwnerIdTop option:selected").val(),
                "PropertyManagerId": $("#MainContent_ddlPropertyManagerID option:selected").val(),
                "LocationId": $("#MainContent_ddlLocationID option:selected").val(),
                "UnitId": $.trim($("[id*=MainContent_txtUnitID]").val())
            }
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "Obj": currentObj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Loading Failed !!");
                    },
                success:
                    function (result) {

                        console.log(result);
                        var mess = $.parseJSON(decodeURIComponent(result.d));

                        console.log(mess);
                        if (mess.length > 0) {
                            BindAllEquipment(mess);
                            // notify('success', "Schedule Set Successfully !!");
                        } else {
                            // notify('danger', "Schedule Set failed !!");
                        }

                    }

            });
        }

        function BindAllEquipment(result) {
            if (result.length > 0) {
                //tblEquipmentAllData
                var content = "";
                $.each(result, function (i, obj) {

                    content += "<tr>";
                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                    content += "<td>" + obj.TypeOfEquipment + "</td>";
                    content += "<td>" + obj.Manufaturer + "</td>";
                    content += "<td>" + ParseJsonDate(obj.PurDate) + "</td>";
                    content += "<td><input type='button' value='Edit' data_Serial='" + obj.Serial + "' onclick='EditEquipmentn(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
                    content += "<td><input type='button' value='Delete' data_Serial='" + obj.Serial + "'  onclick='DeleteEquipmentn(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
                    content += "</tr>";
                });
                $("#tblEquipmentAllData tbody").empty();
                $("#tblEquipmentAllData tbody").append(content);
            }

        }


        function EditEquipmentn(curtd) {
            var pagePath = window.location.pathname + "/GetSingleEquipment";
            var id = $(curtd).attr('Id');
            //var RequestType = $(curtd).attr('data_RequestType');
            // var data_SendMassageTo = $(curtd).attr('data_SendMassageTo');
            //hdCommunicationId
            // var trRow = $(curtd).closest('tr');
            //var Message = $(trRow).find('td:eq(2)').text();
            var obj = {
                "id": id
            }
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "obj": obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in post");
                    },
                success:
                    function (result) {
                        var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {
                            $.each(lstofEquipmentData.EqIage, function (i, obj) {
                                content += "<tr style='float:left;margin:15px;'>";
                                content += "<td><div><img style='width: 285px;height: 177px;' src='" + obj.ImagePath + "' alt='" + obj.ImageName + "'></div><br/><span>" + obj.Description + "</span></td>";
                                content += "</tr>";
                            });
                            $("#tblEqWebImage tbody").append(content);
                            setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });

            //$("#hdCommunicationId").val(id);
            //$("#MainContent_txtMessage").val(Message);
            //$("#ddlSendMassageTo").val(data_SendMassageTo).trigger('change');
            //$('input[name=r5]').closest('div').removeClass('checked');
            //$('input[name=r5]').attr('checked', false);
            //$("input[name=r5][value='" + RequestType.trim() + "']").closest('div').addClass('checked');
            //$("input[name=r5][value='" + RequestType.trim() + "']").attr('checked', true);
            //$("#btnAddMassage").text("Update");
        }
        function DeleteEquipmentn(curtd) {

            // console.log(curtd);
            var obj = {
                "id": $(curtd).attr('Id')
            }
            // var id = $(curtd).attr('Id');

            var pagePath = window.location.pathname + "/DeleteEquipment";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "obj": obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in post");
                    },
                success:
                    function (result) {
                        var mass = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {
                            notify('danger', "Equipment Deleted Failed!!");
                        } else {

                            if (mass == 'true') {
                                //setEquipmentData(mass);
                            } else {
                                notify('success', "Equipment Deleted Successfully!!");
                            }

                            ClearEqData();
                            LoadEquipmentGridData();

                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        //function LoadAllEquipmentData(parameters) {
        //    var pagePath = window.location.pathname + "/LoadEquipmentData";
        //    var obj = {};
        //    var unitSerialId = $.trim($("[id*=txtUnitID]").val());
        //    obj.unitSerialId = unitSerialId;
        //    $.ajax({
        //        type: "POST",
        //        url: pagePath,
        //        data: JSON.stringify(obj),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        error:
        //            function (XMLHttpRequest, textStatus, errorThrown) {
        //                alert("Error");
        //            },
        //        success:
        //            function (result) {
        //                var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
        //                var content = "";
        //                if (result.d == '') {

        //                } else {
        //                    $.each(lstofEquipmentData.EqIage, function (i, obj) {
        //                        content += "<tr style='float:left;margin:15px;'>";
        //                        content += "<td><div><img style='width: 285px;height: 177px;' src='" + obj.ImagePath + "' alt='" + obj.ImageName + "'></div><br/><span>" + obj.Description + "</span></td>";
        //                        content += "</tr>";
        //                    });
        //                    $("#tblEqWebImage tbody").append(content);
        //                    setEquipmentData(lstofEquipmentData.RentalUnit);
        //                }

        //            }

        //    });
        //}


        function setEquipmentData(d) {
            if (d != null) {
                $("#Id").val(d.Id);
                $("#Serial").val(d.Serial);
                // $.trim($("[id*=MainContent_txtUnitID]").val(d.ResidentialUnitSerialId)); //MainContent_txtUnitID
                $.trim($("[id*=MainContent_txtTypeOfEquipment]").val(d.TypeOfEquipment));
                $.trim($("[id*=MainContent_txtModel]").val(d.Model));
                var PurDate = ParseJsonDate(d.PurDate);
                $.trim($("[id*=MainContent_txtPurdate]").val(PurDate));
                $.trim($("[id*=MainContent_txtManufacturer]").val(d.Manufaturer));
                $.trim($("[id*=MainContent_txtManuPhone]").val(d.ManuFacturerPhone));
                $.trim($("[id*=MainContent_txtManuContactPerson]").val(d.ContactPerson));
                $.trim($("[id*=MainContent_txtManuAddress]").val(d.ManuAddress));
                $.trim($("[id*=MainContent_txtManuAddress1]").val(d.ManuAddress1));
                $("#ddlManuCountry").val(d.ManuCountryId).trigger('change.select2');
                $.trim($("[id*=MainContent_txtManuRegion]").val(d.ManuRegion));
                $.trim($("[id*=txtManuCity]").val(d.ManuCity));
                $("#ddlManuState").val(d.ManuState).trigger('change.select2');
                $.trim($("[id*=MainContent_txtManuZipCode]").val(d.ManuZipCode));
                $.trim($("[id*=MainContent_txtManuEmail]").val(d.ManuEmail));
                $.trim($("[id*=MainContent_txtManuWebSite]").val(d.ManuWebSite));
                $('input[name=r3]').closest('div').removeClass('checked');
                $('input[name=r3]').attr('checked', false);
                $("input[name=r3][value='" + d.PurchaseFrom + "']").closest('div').addClass('checked');
                $("input[name=r3][value='" + d.PurchaseFrom + "']").attr('checked', true);
                $.trim($("[id*=MainContent_txtPurName]").val(d.PurName));
                //$("#txtMainVendorName").val(d.PurName);
                $.trim($("[id*=MainContent_txtPurPhone]").val(d.PurPhone));
                //  $("#txtMainVendorPhone").val(d.PurPhone);
                $.trim($("[id*=MainContent_txtPurContactPerson]").val(d.PurContactPerson));
                $.trim($("[id*=MainContent_txtPurAddress]").val(d.PurAddress));
                $.trim($("[id*=MainContent_txtPurAddress1]").val(d.PurAddress1));
                $("#ddlPurCountry").val(d.PurCountryId).trigger('change');
                $.trim($("[id*=MainContent_txtPurRegion]").val(d.PurRegion)),
                $.trim($("[id*=txtPurCity]").val(d.PurCity));
                $("#ddlPurState").val(d.PurState).trigger('change');
                $.trim($("[id*=MainContent_txtPurZipCode]").val(d.PurZipCode));
                $.trim($("[id*=MainContent_txtPurEmail]").val(d.PurEmail));
                $.trim($("[id*=MainContent_txtPurWebSite]").val(d.PurWebSite));
                $('input[name=r4]').closest('div').removeClass('checked');
                $('input[name=r4]').attr('checked', false);
                $("input[name=r4][value='" + d.Warrenty + "']").closest('div').addClass('checked');
                $("input[name=r4][value='" + d.Warrenty + "']").attr('checked', true);
                $.trim($("[id*=MainContent_txtPurNoYears]").val(d.NoYears));
                $.trim($("[id*=MainContent_txtPurNoDays]").val(d.NoDays));
                $.trim($("[id*=MainContent_txtWarrentyDesc]").val(d.WarrentyDesc));
                $.trim($("[id*=MainContent_txtDoumentId]").val(d.DocumentId));
                $.trim($("[id*=MainContent_txtPurchasePrice]").val(d.PurchasePrice));
                $("#btnSaveEq").text("Update");
            }

        }

        function TypeOfEquipmentChange(currentVal) {
            var val = $("[id*=MainContent_txtTypeOfEquipment]").val();
            $("#txtMainEqType").val(val);
        }
        //function TypeOfEquipmentChange(currentVal) {
        //    var val = $("[id*=MainContent_txtPurdate]").val();
        //    $("#txtMainPurchaseDate").val(val);
        //}
        ////MainContent_txtManufacturer
        function ManufacturerChange(currentVal) {
            var val = $("[id*=MainContent_txtManufacturer]").val();
            $("#txtMainManufacturer").val(val);
        }
        //function TypeOfEquipmentChange(currentVal) {
        //    var val = $("[id*=MainContent_txtManuPhone]").val();
        //    $("#txtMainManuPhone").val(val);
        //}
        //function TypeOfEquipmentChange(currentVal) {
        //    var val = $("[id*=MainContent_txtModel]").val();
        //    $("#txtMainModel").val(val);
        //}
        //function TypeOfEquipmentChange(currentVal) {
        //    var val = $("[id*=MainContent_txtPurchasePrice]").val();
        //    $("#txtMainEqType").val(val);
        //}
        //function TypeOfEquipmentChange(currentVal) {
        //    var val = $("[id*=MainContent_txtPurName]").val();
        //    $("#txtMainEqType").val(val);
        //}
        //function TypeOfEquipmentChange(currentVal) {
        //    var val = $("[id*=MainContent_txtPurPhone]").val();
        //    $("#txtMainEqType").val(val);
        //}
        function setCombo(data, selectedvalue) {
            var content = '<option value="-1">Select.......</option>';
            if (data == undefined || data.length == 0 || data == null) {
                return content;
            }
            else {
                if (selectedvalue == undefined || selectedvalue == null) {
                    $.each(data, function (i, obj) {
                        content += '<option data_Id="' + obj.Id + '" value="' + obj.Id2 + '">' + obj.Data + '</option>';
                    });
                }

                else {
                    $.each(data, function (i, obj) {
                        if (obj.Id2 == selectedvalue) {
                            content += '<option data_Id="' + obj.Id + '" value="' + obj.Id2 + '" selected>' + obj.Data + '</option>';
                        } else {
                            content += '<option data_Id="' + obj.Id + '" value="' + obj.Id2 + '">' + obj.Data + '</option>';
                        }
                    });
                }

            }

            return content;
        }

        function setComboWithIntValue(data, selectedvalue) {
            var content = '<option value="-1">Select.......</option>';
            if (data == undefined || data.length == 0 || data == null) {
                return content;
            }
            else {
                if (selectedvalue == undefined || selectedvalue == null) {
                    $.each(data, function (i, obj) {
                        content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id + '">' + obj.Data + '</option>';
                    });
                }

                else {
                    $.each(data, function (i, obj) {
                        if (obj.Id == selectedvalue) {
                            content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id + '" selected>' + obj.Data + '</option>';
                        } else {
                            content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id + '">' + obj.Data + '</option>';
                        }
                    });
                }

            }

            return content;
        }
        function ParseJsonDate(date) {
            if (date != null) {
                //return $.datepicker.formatDate('dd-mm-yy', new Date(parseInt(date.substr(6))));
                //return $.datepicker.formatDate('yy-mm-dd', new Date(parseInt(date.substr(6))));
                return $.datepicker.formatDate('mm-dd-yy', new Date(parseInt(date.substr(6))));
            } else {
                return "";
            }

        }

        //---  Communication ---//
        //ddlSendMassageTo
        function LoadPropetyManagerId(parameters) {
            var pagePath = window.location.pathname + "/LoadPropertyManager";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "ownerId": $("#MainContent_ddlOwnerIdTop option:selected").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error");
                    },
                success:
                    function (result) {
                        var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result == '') {

                        } else {
                            var content = setCombo(lstofEquipmentData, '-1');
                            $("#ddlSendMassageTo option").empty();
                            $("#ddlSendMassageTo").append(content);
                            $("#ddlSendMassageTo").val("-1").trigger('change');
                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }

        function LoadCommunicationGrid(parameters) {
            var pagePath = window.location.pathname + "/LoadCommunication";
            var obj = {};
            var unitSerialId = $.trim($("[id*=txtUnitID]").val());
            obj.unitSerialId = unitSerialId;

            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in Loading Communication Grid");
                    },
                success:
                    function (result) {

                        var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {
                            $.each(lstofEquipmentData, function (i, obj) {

                                content += "<tr>";
                                //content += "<td style='display:none'>" + obj.Id + "</td>";
                                content += "<td>" + obj.CreatedDate + "</td>";
                                content += "<td>" + obj.SendMassageToName + "</td>";
                                content += "<td>" + obj.Massage + "</td>";
                                // content += "<td><input type='button' value='Edit' data_RequestType='" + obj.RequestType + "' data_SendMassageTo ='" + obj.SendMassageTo + "' onclick='EditCommunication(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
                                content += "<td><input type='button' value='Delete' data_RequestType='" + obj.RequestType + "' data_SendMassageTo ='" + obj.SendMassageTo + "' onclick='DeleteCommunication(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
                                content += "</tr>";
                            });
                            $("#tblMessageLst tbody").empty();
                            $("#tblMessageLst tbody").append(content);
                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        $(document).on('click', '#btnAddMassage', function (parameters) {
            if (validationADD()) {

                var Massage = $("#MainContent_txtMessage").val();
                //var sendMassto = $("#ddlSendMassageTo").val(); 
                var sendMassto = $("#MainContent_txtMessageTo").val();
                if (Massage === "undefined" || Massage === "") {
                    $("#MainContent_txtMessage").css({ 'border': '1px solid red' });
                    notify('danger', "Please add massage !!");
                    return;
                } else {
                    $("#MainContent_txtMessage").css({ 'border': '1px solid #d2d6de' });
                }

                if (sendMassto === "undefined" || sendMassto === "") {
                    $("#MainContent_txtMessageTo").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Receiver Email !!");
                    return;
                } else {
                    $("#MainContent_txtMessageTo").css({ 'border': '1px solid #d2d6de' });
                }

                //if (sendMassto === "undefined" || sendMassto === "") {
                //    $("#s2id_ddlSendMassageTo").css({ 'border': '1px solid red' });
                //    notify('danger', "Please select Send Massage To !!");
                //    return;
                //} else {
                //    $("#s2id_ddlSendMassageTo").css({ 'border': '1px solid #d2d6de' });
                //}

                var obj = {
                    Id: $("#hdCommunicationId").val() == "" ? 0 : $("#hdCommunicationId").val(),
                    //TenantName:
                    Massage: Massage.trim(),
                    SendMassageTo: sendMassto,
                    RequestType: ($('input[name=r5]:checked').val()).trim()
                };
                var pagePath = window.location.pathname + "/SaveCommunication";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "obj": obj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error in post");
                        },
                    success:
                        function (result) {
                            var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";
                            if (result.d == '') {

                            } else {

                                var mass = $("#btnAddMassage").text();
                                notify('success', "Message " + mass + " Successfully");
                                ClearAddMassage();
                                LoadCommunicationGrid();
                                //var content = setCombo(lstofEquipmentData, '-1');
                                //$("#ddlSendMassageTo option").empty();
                                //$("#ddlSendMassageTo").append(content);
                                //$("#ddlSendMassageTo").val("-1").trigger('change');
                                //$("#btnAddMassage").text("Add");


                                //setEquipmentData(lstofEquipmentData.RentalUnit);
                            }

                        }

                });

            } else {
                notify('danger', "Please Put Required Red Field");
            }

        });

        $(document).on('click', '#btnSend', function (parameters) {
            if (validationADD()) {

                var Massage = $("#MainContent_txtMessage").val();
                //var sendMassto = $("#ddlSendMassageTo").val();
                var sendMassto = $("#MainContent_txtMessageTo").val();
                if (Massage === "undefined" || Massage === "") {
                    $("#MainContent_txtMessage").css({ 'border': '1px solid red' });
                    notify('danger', "Please add massage !!");
                    return;
                } else {
                    $("#MainContent_txtMessage").css({ 'border': '1px solid #d2d6de' });
                }

                if (sendMassto === "undefined" || sendMassto === "") {
                    $("#MainContent_txtMessageTo").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Receiver Email !!");
                    return;
                } else {
                    $("#MainContent_txtMessageTo").css({ 'border': '1px solid #d2d6de' });
                }

                //if (sendMassto === "undefined" || sendMassto === "") {
                //    $("#s2id_ddlSendMassageTo").css({ 'border': '1px solid red' });
                //    notify('danger', "Please select Send Massage To !!");
                //    return;
                //} else {
                //    $("#s2id_ddlSendMassageTo").css({ 'border': '1px solid #d2d6de' });
                //}
                var obj = {
                    Id: $("#hdCommunicationId").val() == "" ? 0 : $("#hdCommunicationId").val(),
                    //TenantName:
                    Massage: Massage.trim(),
                    SendMassageTo: sendMassto,
                    RequestType: ($('input[name=r5]:checked').val()).trim()
                };
                var pagePath = window.location.pathname + "/SaveCommunication";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "obj": obj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error in post");
                        },
                    success:
                        function (result) {
                            var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";
                            if (result.d == '') {

                            } else {
                                notify('success', "Message sent Successfully");

                                ClearAddMassage();
                                LoadCommunicationGrid();
                                //var content = setCombo(lstofEquipmentData, '-1');
                                //$("#ddlSendMassageTo option").empty();
                                //$("#ddlSendMassageTo").append(content);
                                //$("#ddlSendMassageTo").val("-1").trigger('change');


                            }

                        }

                });

            } else {
                notify('danger', "Please Put Required Red Field");
            }

        });

        function ClearAddMassage(parameters) {

            $("#hdCommunicationId").val(0);
            $("#MainContent_txtMessage").val("");
            $("#MainContent_txtMessageTo").val("");
            //$("#ddlSendMassageTo").val("-1").trigger('change');
            $('input[name=r5]').closest('div').removeClass('checked');
            $('input[name=r5]').attr('checked', false);
            $("input[name=r5][value='Maintenance Request']").closest('div').addClass('checked');
            $("input[name=r5][value='Maintenance Request']").attr('checked', true);
        }
        function EditCommunication(curtd) {

            var id = $(curtd).attr('Id');
            var RequestType = $(curtd).attr('data_RequestType');
            var data_SendMassageTo = $(curtd).attr('data_SendMassageTo');
            //hdCommunicationId
            var trRow = $(curtd).closest('tr');
            var Message = $(trRow).find('td:eq(2)').text();
            var Message = $(trRow).find('td:eq(2)').text();

            $("#hdCommunicationId").val(id);
            $("#MainContent_txtMessage").val(Message);
            $("#MainContent_txtMessageTo").val(data_SendMassageTo);
            //$("#ddlSendMassageTo").val(data_SendMassageTo).trigger('change');
            $('input[name=r5]').closest('div').removeClass('checked');
            $('input[name=r5]').attr('checked', false);
            $("input[name=r5][value='" + RequestType.trim() + "']").closest('div').addClass('checked');
            $("input[name=r5][value='" + RequestType.trim() + "']").attr('checked', true);
            $("#btnAddMassage").text("Update");
        }
        function DeleteCommunication(curtd) {

            console.log(curtd);
            var id = $(curtd).attr('Id');

            var pagePath = window.location.pathname + "/DeleteCommunication";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "obj": id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in post");
                    },
                success:
                    function (result) {
                        var mass = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {
                            notify('danger', "Message Deleted Failed!!");
                        } else {
                            if (mass == 'true') {
                                notify('success', "Message Deleted Successfully");
                            } else {
                                notify('success', "Message Deleted Successfully!!");
                            }

                            LoadCommunicationGrid();

                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        //Load Document Grid
        //tblDocumentFileList
        function LoadRentalDocumentGrid(parameters) {
            var pagePath = window.location.pathname + "/LoadRentalDocument";
            var obj = {};
            var unitSerialId = $.trim($("[id*=txtUnitID]").val());
            obj.unitSerialId = unitSerialId;

            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in Loading Document Grid");
                    },
                success:
                    function (result) {

                        var lstofRentalDocumentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {

                            if (lstofRentalDocumentData.length > 0) {
                                $.each(lstofRentalDocumentData, function (i, obj) {

                                    content += "<tr>";
                                    content += "<td style='width:5%'><button data_RowIdAdd='" + obj.Id + "' type='button' class='btnAdd'>" + G_buttonAdd + "</button><button data_RowIdRem='" + obj.Id + "' type='button' class='btnRemove'>" + G_buttonRmv + "</button></td>";
                                    content += "<td style='width:15%'> <textarea  data_RowId='" + obj.Id + "' id='txtDocumentDesc' rows='3' cols='5' class='form-control'>" + obj.DocumentDescription + "</textarea></td>";
                                    //content += "<td style='width:35%'><input  type='text' id='RentalDocFileName' class='form-control' value='" + obj.FileName.split('.')[0] + "'></td>"; //DateAdded
                                    content += "<td style='width:35%'><input  type='text' id='RentalDocFileName' class='form-control' value='" + obj.FileName + "'></td>";
                                    content += "<td style='width:30%'> <input style='float: left;'  type='file' class='form-control' id='fileRentalDocument' /><input  type='button' style='margin-left: 4px;margin-top: 5px;' value='Upload' data_RowId='" + obj.Id + "' class='btn btnNewColor' onclick='SaveRentalImage(this);' /></td>"; //DateAdded
                                    content += "<td style='width:5%;display:none;'><select class='form-control ddl browsStatus' >" + LoadBrowsStatus(obj.IsViewedOrDownloaded, obj.Id, obj.FilePath) + "</select><a href='#' style='display:none;' class='download' download></a></td>"; //
                                    content += "<td style='width:5%'><input type='button' value='Delete' onclick='DeleteRentalDocument(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                });
                            } else {
                                // $.each(lstofEquipmentData, function (i, obj) {

                                content += "<tr>";
                                content += "<td style='width:5%'><button data_RowIdAdd='0' type='button' class='btnAdd'>" + G_buttonAdd + "</button><button data_RowIdRem='0' type='button' class='btnRemove'>" + G_buttonRmv + "</button></td>";
                                content += "<td style='width:15%'> <textarea  data_RowId='0' id='txtDocumentDesc' rows='3' cols='5' class='form-control'></textarea></td>";
                                content += "<td style='width:35%'><input type='text' id='RentalDocFileName' class='form-control' value=''></td>";//DateAdded
                                content += "<td style='width:30%'> <input  type='file' style='float: left;' style='width: 230px;float: left;' class='form-control' id='fileRentalDocument' /> <input type='button' style='margin-top:2px;' value='Upload' data_RowId='0' style='width: 24%;margin-left: 4px;margin-top: 5px;' class='btn btnNewColor' onclick='SaveRentalImage(this);' /></td>";//DateAdded
                                content += "<td style='width:5%;display:none;'><select class='form-control ddl browsStatus' >" + LoadBrowsStatusWithOutValue(0) + "</select></td>";//
                                content += "<td style='width:5%'><input disabled type='button' value='Delete' onclick='DeleteRentalDocument(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                content += "</tr>";
                                // });
                            }

                            $("#tblDocumentFileList tbody").empty();
                            $("#tblDocumentFileList tbody").append(content);
                            $(".browsStatus").select2();
                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        function LoadBrowsStatus(IsViewedOrDownloaded, rowId, filePath) {
            var content = '<option data_Imagepath="' + filePath + '" data_RowId="' + rowId + '" value="-1">Select........</option>';

            var sView = "", sDownload = "";
            if (IsViewedOrDownloaded === "View") {
                sView = "selected";
            } else if (IsViewedOrDownloaded === "Download") {
                sDownload = "selected";
            } else {

            }
            //content += '<option ' + sView + ' data_Imagepath="' + filePath + '"  data_RowId="' + rowId + '" value="View">View</option>';
            content += '<option ' + sDownload + ' data_Imagepath="' + filePath + '"  data_RowId="' + rowId + '" value="Download">Download</option>';
            return content;

        }
        function LoadBrowsStatusWithOutValue(rowId) {
            var content = '<option data_RowId="' + rowId + '" value="-1">Select........</option>';

            //content += '<option data_Imagepath=""  data_RowId="' + rowId + '" value="View">View</option>';
            content += '<option data_Imagepath=""  data_RowId="' + rowId + '"  value="Download">Download</option>';
            return content;

        }

        $(document).on('click', '#btnBrownSite', function (parameters) {
            BrowseSiteLibrary();
        });
        function BrowseSiteLibrary() {
            //alert("Please Download your desire documents and upload it again");
            //window.open('https://www.eproperty365.net/leases/', '_blank');
            ////tbl_SiteLibery

            var pagePath = window.location.pathname + "/GetAllLeaseDocument";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in post");
                    },
                success:
                    function (result) {
                        var FileList = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (FileList != null) {
                            $.each(FileList, function (i, obj) {
                                content += "<tr>";
                                content += "<td style='width: 50%'>";
                                content += obj.FileName;
                                content += "</td>";
                                content += "<td style='width: 50%'>";
                                content += "<a style='color: blue;' href='" + obj.FilePath + "'>Download</a>";
                                content += "</td>";
                                content += "</tr>";
                            });

                        }
                        $("#tbl_SiteLibery tbody").empty();
                        $("#tbl_SiteLibery tbody").append(content);
                        //if (result.d == '') {
                        //    notify('danger', "Message Deleted Failed!!");
                        //} else {
                        //    if (mass == 'true') {
                        //        notify('success', "Message Deleted Successfully");
                        //    } else {
                        //        notify('success', "Message Deleted Successfully!!");
                        //    }

                        //    //LoadCommunicationGrid();

                        //    //setEquipmentData(lstofEquipmentData.RentalUnit);
                        //}

                    }

            });


            // window.location.href = "https://www.eproperty365.net/leases/";
            //window.local = 'https://www.eproperty365.net/leases/';
        }
        $(document).off('click', '.btnAdd').on('click', '.btnAdd', function () {
            //if (g_IsEdit == false) {

            var curentRow = $(this).closest('tr');
            var content = "";
            var checkitemsave = parseInt($(curentRow).find('td:eq(4)').find('select option').attr("data_RowId"));

            if (checkitemsave > 0) {
                content += "<tr>";
                content += "<td style='width:5%'><button data_RowIdAdd='0' type='button' class='btnAdd'>" + G_buttonAdd + "</button><button data_RowIdRem='0' type='button' class='btnRemove'>" + G_buttonRmv + "</button></td>";
                content += "<td style='width:15%'> <textarea  data_RowId='0' id='txtDocumentDesc' rows='3' cols='5' class='form-control'></textarea></td>";
                content += "<td style='width:35%'><input type='text' id='RentalDocFileName' class='form-control' value=''></td>";//DateAdded
                content += "<td style='width:30%'> <input  type='file' style='float: left;width: 74%;'  class='form-control fileRentalDocument' id='fileRentalDocument' /> <input type='button' style='margin-left: 4px;margin-top: 5px;' value='Upload' data_RowId='0' class='btn btnNewColor' onclick='SaveRentalImage(this);' /></td>";//DateAdded
                content += "<td style='width:5%;display:none;'><select id='browsStatus' class='form-control ddl browsStatus' ><option data_RowId='0' value=-1'>Select........</option>" +
                    //"<option data_Imagepath=''  data_RowId='0' value='View'>View</option>" +
                    "<option data_Imagepath=''  data_RowId='0'  value='Download'>Download</option>" +
                    "</select></td>";//
                //content += "<td style='width:5%'><input disabled type='button' value='Browse Site Library' onclick='BrowsSiteLiabery(this)' class='custombtn'/></td>";
                content += "<td style='width:5%'><input disabled type='button' value='Delete' onclick='DeleteRentalDocument(this)' class='custombtn'/></td>";
                content += "</tr>";
                $("#tblDocumentFileList tbody").append(content);
                $("#browsStatus").select2();
            }
            else {
                notify("danger", "Please Browse and save a file first");
            };

        });
        $(document).off('click', '.btnRemove').on('click', '.btnRemove', function () {
            var index = $(this).closest('tr').index();
            var curentRow = $(this).closest('tr');
            var content = "";
            var checkitemsave = parseInt($(curentRow).find('td:eq(4)').find('select option').attr("data_RowId"));
            if (index !== 0) {
                if (checkitemsave > 0) {
                    notify("danger", "This Document is already saved and can not removed, you can update it");
                } else {
                    $(this).closest('tr').remove();
                }

            }

        });
        var curentRow;
        var editId = 0;
        function SaveRentalImage(currrenttd) {

            //fileRentalDocument
            //
            var trRow = $(currrenttd).closest('tr');
            curentRow = trRow;
            var checkitemsave = parseInt($(curentRow).find('td:eq(4)').find('select option').attr("data_RowId"));
            editId = checkitemsave;
            if ($($($(trRow).find('td:eq(3)').children())[0]).val() != "") {
                var file = $($($(trRow).find('td:eq(3)').children())[0].files[0]);
                // console.log(file);
                //  G_ImageName = file[0].name;
                var fileName = file[0].name;
                var idxDot = fileName.lastIndexOf(".") + 1;
                var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
                G_ImageName = extFile;
                if (extFile == "jpg" || extFile == "jpeg" || extFile == "png" || extFile == 'gif' || extFile == 'tiff' || extFile == 'pdf' || extFile == 'docx' || extFile == 'doc' || extFile == 'txt') {
                    //TO DO
                    var reader = new FileReader();


                    reader.readAsDataURL(file[0]);
                    // console.log(reader);
                    reader.onload = UpdateFilesEq_Rental;
                } else {
                    //alert("Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
                    notify('danger', "Only .docx, .doc, .txt files are allowed!");
                    $($($(trRow).find('td:eq(3)').children())[0]).val("");
                }

            }
            else {
                //alert('Please Choose An Image');
                notify('danger', "Please select File To Upload");
            }
        }

        function UpdateFilesEq_Rental(evt) {

            //console.log(evt);
            // var img = G_ImageName;
            //curentRow editId
            if (validationLessDocument(curentRow)) {
                var pagePath = window.location.pathname + "/SaveRentalDocument";
                var result = evt.target.result;
                var ImageSave = "";
                if (G_ImageName == "jpg" || G_ImageName == "jpeg" || G_ImageName == "png" || G_ImageName == 'gif' || G_ImageName == 'tiff') {
                    ImageSave = result.replace("data:image/jpeg;base64,", "");
                } else if (G_ImageName == "pdf") {
                    ImageSave = result.replace("data:application/pdf;base64,", "");
                }
                else if (G_ImageName == "docx") {
                    ImageSave = result.replace(/^data:(.*;base64,)?/, '');
                } else if (G_ImageName == "doc") {
                    ImageSave = result.replace("data:application/msword;base64,", "");
                } else if (G_ImageName == "txt") {
                    ImageSave = result.replace("data:text/plain;base64,", "");
                }

                //data:application/pdf;base64,

                var documentDescription = $(curentRow).find('td:eq(1)').children().val();
                //var FileName = $(curentRow).find('td:eq(2)').children().val();
                var file = $($($(curentRow).find('td:eq(3)').children())[0].files[0]);
                // console.log(file);
                //  G_ImageName = file[0].name;
                var FileName = file[0].name;

                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: "{ 'Image':'" + ImageSave + "' , 'documentDescription':'" + documentDescription + "' ,'FileNameExtention':'" + G_ImageName + "','FileName':'" + FileName + "','Id':'" + editId + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error");
                            curentRow = "";
                            editId = 0;
                        },
                    success:
                        function (result) {
                            //curentRow = "";
                            editId = 0;
                            var obj = $.parseJSON(decodeURIComponent(result.d));

                            if (obj.Id > 0) {
                                $($(curentRow).find('td:eq(4)').find('select option').attr("data_RowId", obj.Id));
                                $($(curentRow).find('td:eq(4)').find('select option').attr("data_Imagepath", obj.FilePath));
                                //data_Imagepath
                                $(".fileRentalDocument").val("");
                                $($(curentRow).find('td:eq(5)').find('input').prop("disabled", false));
                                $(curentRow).find('td:eq(2)').children().val(obj.FileName)
                                notify('success', "File uploaded Successfully");
                            } else {
                                notify('danger', "File uploaded Failed !!");
                            }
                            // $(curentRow).find('td:eq(4)').find('select option').attr("data_RowId").val(obj.Id);

                            //var content = "";
                            //content += "<tr style='float:left;margin:15px;'>";
                            //content += "<td><span class='col-sm-12' style='width: 50%;float: left;' form-check-inline'><input id='" + obj.Serial + "' data_unitId='" + obj.ResidentialUnitSerialId + "' name='chkisUse' type='checkbox'>" +
                            //    "<label>Use</label></span><span style='width: 45%;float: right;text-align: right;margin-right: 3%;'><input style='border: none;background: none;cursor: pointer;text-decoration: underline;' id='" + obj.Serial + "' data_unitId='" + obj.ResidentialUnitSerialId + "' type='button' value='Delete' onclick='Delete(this);' /></span><br/><div><img style='width: 285px;height: 177px;' src='" + obj.ImagePath + "' alt='" + obj.ImageName + "'></div></td>";
                            //content += "</tr>";
                            //$("#tblWebImage tbody").append(content);
                            //clearWebImage();


                            curentRow = "";
                            G_ImageName = "";
                        }

                });
                //}
                // }

            } else {
                //  alert("please fill up red field");
                notify('danger', "Please fill red mark field");
            }

        }
        function DeleteRentalDocument(curtd) {
            var curentRow = $(curtd).closest('tr');
            var content = "";
            var checkitemsave = parseInt($(curentRow).find('td:eq(4)').find('select option').attr("data_RowId"));

            var id = checkitemsave;

            if (id > 0) {
                var pagePath = window.location.pathname + "/DeleteRentalDocument";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "obj": id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error in post");
                        },
                    success:
                        function (result) {

                            var mass = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";
                            if (result.d == '') {

                            } else {
                                if (mass === true) {
                                    notify('success', "Rental Document Deleted Successfully");
                                } else {
                                    notify('danger', "Rental Document Deleted Failed!!");
                                }

                                LoadRentalDocumentGrid();

                                //setEquipmentData(lstofEquipmentData.RentalUnit);
                            }

                        }

                });
            }

        }

        $(document).on('change', '.browsStatus', function (e) {

            var selectedval = $(this).val();
            var status = "";
            var id = $(this).find('option:selected').attr('data_rowid');
            var downloadFilePath = $(this).find('option:selected').attr('data_imagepath');

            var pathname = window.location.pathname; // Returns path only
            var url = window.location.href;     // Returns full URL
            var origin = window.location.origin;   // Returns base URL

            if (selectedval == 'Download') {
                status = 'Download';
                e.preventDefault();  //stop the browser from following
                //window.location.href = downloadFilePath;
                var tr = $(this).closest('tr');

                var newUrl = downloadFilePath.replace('../', origin + '/');
                //var n = origin + "/" + downloadFilePath;
                var n = downloadFilePath.replace('../', '');
                console.log(newUrl);
                $($(tr).find('td:eq(4)').find('a').attr("href", newUrl));
                $($($(tr).find('td:eq(4)').find('a'))[1]).click();
                window.open(newUrl, '_blank');
            } else if (selectedval == 'View') {
                status = 'View';

                var newUrl = downloadFilePath.replace('../', origin + '/');
                var NnewUrl = newUrl.replace('../', '');
                //alert(NnewUrl);
                var ext = NnewUrl.split('.')[1];
                //alert(ext);
                if (ext == 'pdf') {
                    // alert(newUrl);
                    $("#iframedis").show();
                    PDFObject.embed(NnewUrl, "#iframedis", { fallbackLink: false });
                } else {
                    var baseurl = newUrl.replace('../', '');

                    $("#iframeimage").show();
                    $("#ifrmImage").attr("src", baseurl);
                    //iframeimage
                }

                //var n = newUrl.replace('../', '');
                //  var n = origin + "/" + downloadFilePath;

                //iframeimage
                //iframedocx

                //<iframe id="ifDocx" src="https://docs.google.com/gview?url=http://remote.url.tld/path/to/document.doc&embedded=true"></iframe>
                // $("#iframedocx").show();
                //$("#ifDocx").attr('src', newUrl);








                //PDFObject.embed(newUrl, "#iframedocx", { fallbackLink: false });





                // PDFObject.embed('https://www.immigration-quebec.gouv.qc.ca/publications/en/divers/GRI_SelectionProgReg_TravQualif2018-EN.pdf', "#iframedis", { fallbackLink: false });

                //https://docs.google.com/viewer?url=your_url_to_pdf&embedded=true
                // $("#viewfile").attr('src', newUrl + "#view=VFit" + "&toolbar=0" + "&navpanes=0");
            }


            if (id > 0) {
                var pagePath = window.location.pathname + "/updateStatus";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "id": id, 'Status': status }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error in post");
                        },
                    success:
                        function (result) {

                            var mass = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";
                            if (result.d == '') {

                            } else {
                                if (mass === true) {
                                    //notify('success', "Rental Document Deleted Successfully");
                                } else {
                                    // notify('danger', "Rental Document Deleted Failed!!");
                                }

                                //LoadRentalDocumentGrid();


                            }

                        }

                });
            }
        });
        function validationLessDocument(trRow) {
            var isValid = true;

            var docdescription = $(trRow).find('td:eq(1)').children().val();
            var FileName = $(trRow).find('td:eq(2)').children().val();
            if (docdescription === "undefined" || docdescription === "") {
                $(trRow).find('td:eq(1)').children().css({ 'border': '1px solid red' });
                isValid = false;
            }
            else {
                $(trRow).find('td:eq(1)').children().css({ 'border': '1px solid #d2d6de' });
            }
            //if (FileName === "undefined" || FileName === "") {
            //    $(trRow).find('td:eq(2)').children().css({ 'border': '1px solid red' });
            //    isValid = false;
            //}
            //else {
            //    $(trRow).find('td:eq(2)').children().css({ 'border': '1px solid #d2d6de' });
            //}
            if (isValid == true) {
                return true;
            } else {
                return false;
            }
        }

        //Document Id

        function GetDocumentId() {
            var pagePath = window.location.pathname + "/GetDocumentId";
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "ownerId": $("#MainContent_ddlOwnerIdTop option:selected").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error");
                    },
                success:
                    function (result) {
                        var documentId = $.parseJSON(decodeURIComponent(result.d));
                        $("#txtDocumentId").val(documentId);
                    }
            });
        }
        //Document Load
        function LoadDocumentGrid(parameters) {
            var pagePath = window.location.pathname + "/LoadDocument";
            var obj = {};
            var unitSerialId = $.trim($("[id*=txtUnitID]").val());
            obj.unitSerialId = unitSerialId;

            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in Loading Document Grid");
                    },
                success:
                    function (result) {

                        var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {
                            $.each(lstofEquipmentData, function (i, obj) {

                                content += "<tr>";
                                //content += "<td style='display:none'>" + obj.Id + "</td>";
                                content += "<td>" + obj.DocumrntId + "</td>";
                                content += "<td>" + obj.DocumentDescription + "</td>";
                                content += "<td>" + obj.TypeOfDocument + "</td>";//DateAdded
                                content += "<td>" + obj.DateAdded + "</td>";//
                                content += "<td><input type='button' value='Edit' onclick='EditDocument(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
                                content += "<td><input type='button' value='Delete' onclick='DeleteDocument(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                content += "</tr>";
                            });
                            $("#tblDocument tbody").empty();
                            $("#tblDocument tbody").append(content);
                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        //add documen
        $(document).on('click', '#btnAddMyFile', function (parameters) {
            if (validationADD()) {

                var DocumrntId = $("#txtDocumentId").val();
                var DocumentDescription = $("#txtDocumentDesc").val();
                var TypeOfDocument = $("#txtTypeOfDocument").val();
                var DateAdded = $("#txtDateAdded").val();
                if (DocumentDescription === "undefined" || DocumentDescription === "") {
                    $("#txtDocumentDesc").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Document Description !!");
                    return;
                } else {
                    $("#txtDocumentDesc").css({ 'border': '1px solid #d2d6de' });
                }
                if (TypeOfDocument === "undefined" || TypeOfDocument === "") {
                    $("#txtTypeOfDocument").css({ 'border': '1px solid red' });
                    notify('danger', "Please add type of document !!");
                    return;
                } else {
                    $("#txtTypeOfDocument").css({ 'border': '1px solid #d2d6de' });
                }

                if (DateAdded === "undefined" || DateAdded === "") {
                    $("#txtDateAdded").css({ 'border': '1px solid red' });
                    notify('danger', "Please date added !!");
                    return;
                } else {
                    $("#txtDateAdded").css({ 'border': '1px solid #d2d6de' });
                }
                var obj = {
                    Id: $("#hdDocumentId").val() == "" ? 0 : $("#hdDocumentId").val(),
                    //TenantName:
                    DocumrntId: DocumrntId,
                    DocumentDescription: DocumentDescription.trim(),
                    TypeOfDocument: TypeOfDocument,
                    DateAdded: DateAdded
                };
                var pagePath = window.location.pathname + "/SaveDocument";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "obj": obj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error in document save");
                        },
                    success:
                        function (result) {
                            var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";
                            if (result.d == '') {

                            } else {

                                var mass = $("#btnAddMyFile").text();
                                notify('success', "Document " + mass + " Successfully");
                                ClearDocument();
                                LoadDocumentGrid();



                                //setEquipmentData(lstofEquipmentData.RentalUnit);
                            }

                        }

                });

            } else {
                notify('danger', "Please Put Required Red Field");
            }

        });
        // clear field
        function ClearDocument(parameters) {

            $("#hdDocumentId").val(0);
            $("#txtDocumentId").val("");
            $("#txtDocumentDesc").val("");
            $("#txtTypeOfDocument").val("");
            $("#txtDateAdded").val("");

            $("#btnAddMyFile").text("Add From My FileIt");
        }
        //edit document
        function EditDocument(curtd) {

            var id = $(curtd).attr('Id');
            //hdCommunicationId
            var trRow = $(curtd).closest('tr');
            var documentId = $(trRow).find('td:eq(0)').text();
            var documentdesc = $(trRow).find('td:eq(1)').text();
            var typeofdocument = $(trRow).find('td:eq(2)').text();
            var dateadded = $(trRow).find('td:eq(3)').text();

            $("#hdDocumentId").val(id);
            $("#txtDocumentId").val(documentId);
            $("#txtDocumentDesc").val(documentdesc);

            $("#txtTypeOfDocument").val(typeofdocument);
            $("#txtDateAdded").val(dateadded);

            $("#btnAddMyFile").text("Update");
        }

        //delete document
        function DeleteDocument(curtd) {

            console.log(curtd);
            var id = $(curtd).attr('Id');

            var pagePath = window.location.pathname + "/DeleteDocument";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "obj": id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in post");
                    },
                success:
                    function (result) {
                        var mass = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {
                            if (mass == 'true') {
                                notify('success', "Document Deleted Successfully");
                            } else {
                                notify('success', "Document Deleted Failed!!");
                            }

                            LoadDocumentGrid();

                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }

        // Maintenance Manager

        //Load All Data
        function LoadMaintenanceGrid(parameters) {
            var pagePath = window.location.pathname + "/GetMaintenanceManagerAllData";
            var obj = {};
            var unitSerialId = $.trim($("[id*=txtUnitID]").val());
            obj.unitSerialId = unitSerialId;

            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in Loading Document Grid");
                    },
                success:
                    function (result) {

                        var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {

                            var equipment = lstofEquipmentData.ResidentialUnitEquipment;
                            var ResidentialMaintainesManagerMaster = lstofEquipmentData.ResidentialMaintainesManagerMaster;
                            var ResidentialMaintainesManagerImage = lstofEquipmentData.ResidentialMaintainesManagerImage;
                            var ListOfResidentialMaintainesManagerSchedules = lstofEquipmentData.ListOfResidentialMaintainesManagerSchedules;
                            var ListOfResidentialMaintainesManagerPartDatas = lstofEquipmentData.ListOfResidentialMaintainesManagerPartDatas;
                            var ListOfResidentialMaintainesManagerVandorDatas = lstofEquipmentData.ListOfResidentialMaintainesManagerVandorDatas;
                            if (ResidentialMaintainesManagerImage != null) {
                                $("#imgManager").attr('src', ResidentialMaintainesManagerImage.ImageURL);
                            }

                            if (equipment != null) {

                                $("#txtMainEqType").val(equipment.TypeOfEquipment);
                                $("#txtMainPurchaseDate").val(ParseJsonDate(equipment.PurDate));
                                $("#txtMainManufacturer").val(equipment.Manufaturer);
                                $("#txtMainManuPhone").val(equipment.ManuFacturerPhone);
                                $("#txtMainModel").val(equipment.Model);
                                $("#txtMainCost").val(equipment.PurchasePrice);
                            }

                            //$("#txtMainVendorName").val("");
                            //$("#txtMainVendorPhone").val("");


                            if (ListOfResidentialMaintainesManagerSchedules != null) {
                                var content = "";
                                $.each(ListOfResidentialMaintainesManagerSchedules, function (i, obj) {

                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + obj.Reminder + "</td>";
                                    content += "<td>" + obj.MaintenenceJobDesc + "</td>";
                                    content += "<td>" + obj.EstimatedHours + "</td>";
                                    content += "<td>" + obj.LastmaintenanceDate + "</td>";//DateAdded
                                    content += "<td>" + obj.NumberOfDays + "</td>";//
                                    content += "<td>" + obj.NextMaintenanceDate + "</td>";//
                                    content += "<td> <input type='button' value='Edit' onclick='EditMaintainesManagerSchedules(this)' data_issubout='" + obj.isSubOut + "' id='" + obj.Id + "' class='custombtn'/></td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeleteMaintainesManagerSchedules(this)' id='" + obj.Id + "' class='custombtn'/> </td>";
                                    content += "</tr>";
                                });
                                $("#tblMaintenanceSche tbody").empty();
                                $("#tblMaintenanceSche tbody").append(content);
                            }


                            if (ListOfResidentialMaintainesManagerPartDatas != null) {
                                var content = "";
                                $.each(ListOfResidentialMaintainesManagerPartDatas, function (i, obj) {
                                    ;
                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + obj.PartNumber + "</td>";
                                    content += "<td>" + obj.PartDescription + "</td>";
                                    content += "<td>" + obj.ContactPerson + "</td>";//
                                    content += "<td>" + obj.EmailAddress + "</td>";//
                                    content += "<td>" + obj.Manufacturer + "</td>";//DateAdded
                                    content += "<td><input type='button' value='Edit' onclick='EdiPartDatas(this)' id='" + obj.Id + "' class='custombtn'/>  </td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeletePartDatas(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                });
                                $("#tblPartdata tbody").empty();
                                $("#tblPartdata tbody").append(content);
                            }


                            if (ListOfResidentialMaintainesManagerVandorDatas != null) {
                                var content = "";
                                $.each(ListOfResidentialMaintainesManagerVandorDatas, function (i, obj) {
                                    ;
                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + obj.VendorName + "</td>";
                                    content += "<td>" + obj.QuoteId + "</td>";
                                    content += "<td>" + obj.VendorContactName + "</td>";//DateAdded
                                    content += "<td>" + obj.VendorPhone + "</td>";//
                                    content += "<td>" + obj.VendorEmail + "</td>";//
                                    content += "<td>" + obj.AmountOfQuote + "</td>";//
                                    content += "<td>" + obj.WhenDone + "</td>";//
                                    content += "<td>" + obj.HowLong + "</td>";//
                                    content += "<td>" + obj.WonQuote + "</td>";//
                                    content += "<td><input type='button' value='Edit' onclick='EditVandorDatas(this)' id='" + obj.Id + "' data_VendorId='" + obj.VendorId + "' class='custombtn'/></td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeleteVandorDatas(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                });
                                $("#tblMainVendor tbody").empty();
                                $("#tblMainVendor tbody").append(content);
                            }

                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        //save image

        function SaveMaintenanceImage() {
            if (document.getElementById("fileMaintenanceImageUpload").value != "") {
                var file = document.getElementById('fileMaintenanceImageUpload').files[0];
                G_ImageName = file.name;
                var fileName = document.getElementById("fileMaintenanceImageUpload").value;
                var idxDot = fileName.lastIndexOf(".") + 1;
                var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
                if (extFile == "jpg" || extFile == "jpeg" || extFile == "png" || extFile == 'gif' || extFile == 'tiff') {
                    //TO DO
                    var reader = new FileReader();
                    reader.readAsDataURL(file);
                    reader.onload = UpdateFilesMaintenance;
                } else {
                    //alert("Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
                    notify('danger', "Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
                    $("#fileMaintenanceImageUpload").val("");
                }

            }
            else {
                //alert('Please Choose An Image');
                notify('danger', "Please Choose An Image");
            }
        }

        function UpdateFilesMaintenance(evt) {
            if (validationADD()) {
                var pagePath = window.location.pathname + "/MaintenanceImageUpload";
                var result = evt.target.result;
                var ImageSave = result.replace("data:image/jpeg;base64,", "");
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: "{ 'Image':'" + ImageSave + "' , 'ImageName':'" + G_ImageName + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error");
                            G_ImageName = "";
                        },
                    success:
                        function (result) {
                            G_ImageName = "";
                            var obj = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";

                            $("#imgManager").attr('src', obj.ImageURL);
                            //content += "<tr style='float:left;margin:15px;'>";
                            //content += "<td><div><img style='width: 285px;height: 177px;' src='" + obj.ImagePath + "' alt='" + obj.ImageName + "'></div><br/><span>" + obj.Description + "</span></td>";
                            //content += "</tr>";
                            //$("#tblEqWebImage tbody").append(content);
                            //$("[id*=txtPicDesc]").val("");
                            //$("#fileEqImageUpload").val("");
                            notify('success', "Equipment Image Upload Successfully");
                        }

                });

            } else {
                //  alert("please fill up red field");
                notify('danger', "Please fill red mark field");
            }

        }
        //save doc
        $(document).on('click', '#btnAddSchedule', function (parameters) {
            if (validationADD()) {

                var reminder = $("#txtdescMaintainJobDate").val();
                var DocumentDescription = $("#txtdescMaintainJob").val();
                var EstimateHoure = $("#ddlEstimateHoure").val();
                var chkSubOut = $("#chkSubOut").is(":checked") == true ? true : false;
                var lastMaintanence = $("#txtlastMaintanence").val();
                var NumberDays = $("#txtNumberDays").val();
                var NextMainDate = $("#txtNextMainDate").val();
                if (DocumentDescription === "undefined" || DocumentDescription === "") {
                    $("#txtdescMaintainJob").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Document Description !!");
                    return;
                } else {
                    $("#txtdescMaintainJob").css({ 'border': '1px solid #d2d6de' });
                }
                if (reminder === "undefined" || reminder === "") {
                    $("#txtdescMaintainJobDate").css({ 'border': '1px solid red' });
                    notify('danger', "Please add type of reminder !!");
                    return;
                } else {
                    $("#txtdescMaintainJobDate").css({ 'border': '1px solid #d2d6de' });
                }

                if (EstimateHoure === "undefined" || EstimateHoure == "") {
                    $("#ddlEstimateHoure").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Estimate Houre !!");
                    return;
                } else {
                    $("#ddlEstimateHoure").css({ 'border': '1px solid #d2d6de' });
                }

                if (lastMaintanence === "undefined" || lastMaintanence === "") {
                    $("#txtlastMaintanence").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Last Maintenance Date !!");
                    return;
                } else {
                    $("#txtlastMaintanence").css({ 'border': '1px solid #d2d6de' });
                }
                if (NumberDays === "undefined" || NumberDays === "") {
                    $("#txtNumberDays").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Number Of Days !!");
                    return;
                } else {
                    $("#txtNumberDays").css({ 'border': '1px solid #d2d6de' });
                }
                if (NextMainDate === "undefined" || NextMainDate === "") {
                    $("#txtNextMainDate").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Next Maintenance Date !!");
                    return;
                } else {
                    $("#txtNextMainDate").css({ 'border': '1px solid #d2d6de' });
                }
                var obj = {
                    Id: $("#hdMaintenanceId").val() == "" ? 0 : $("#hdMaintenanceId").val(),
                    //TenantName:
                    Reminder: reminder,
                    MaintenenceJobDesc: DocumentDescription.trim(),
                    EstimatedHours: EstimateHoure,
                    isSubOut: chkSubOut,
                    LastmaintenanceDate: lastMaintanence,
                    NumberOfDays: NumberDays,
                    NextMaintenanceDate: NextMainDate


                };
                var pagePath = window.location.pathname + "/SaveMaintenanceSchedule";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "obj": obj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            notify('danger', "Maintenance Schedule not saved");
                        },
                    success:
                        function (result) {
                            var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));

                            var content = "";
                            if (result.d == '') {
                                notify('danger', "Maintenance Schedule not saved");
                            } else {

                                var mass = $("#btnAddSchedule").text();
                                notify('success', "Maintenance Schedule " + mass + " Successfully");

                                if (mass == "Add Schedule") {
                                    // $.each(lstofEquipmentData, function(i, obj) {

                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + lstofEquipmentData.Reminder + "</td>";
                                    content += "<td>" + lstofEquipmentData.MaintenenceJobDesc + "</td>";
                                    content += "<td>" + lstofEquipmentData.EstimatedHours + "</td>";
                                    content += "<td>" + lstofEquipmentData.LastmaintenanceDate + "</td>"; //DateAdded
                                    content += "<td>" + lstofEquipmentData.NumberOfDays + "</td>"; //
                                    content += "<td>" + lstofEquipmentData.NextMaintenanceDate + "</td>"; //
                                    content += "<td><input type='button' value='Edit' onclick='EditMaintainesManagerSchedules(this)' id='" + lstofEquipmentData.Id + "' data_issubout='" + lstofEquipmentData.isSubOut + "' class='custombtn'/></td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeleteMaintainesManagerSchedules(this)' id='" + lstofEquipmentData.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                    // });

                                    $("#tblMaintenanceSche tbody").append(content);
                                } else {
                                    $.each(lstofEquipmentData, function (i, obj) {

                                        content += "<tr>";
                                        //content += "<td style='display:none'>" + obj.Id + "</td>";
                                        content += "<td>" + obj.Reminder + "</td>";
                                        content += "<td>" + obj.MaintenenceJobDesc + "</td>";
                                        content += "<td>" + obj.EstimatedHours + "</td>";
                                        content += "<td>" + obj.LastmaintenanceDate + "</td>";//DateAdded
                                        content += "<td>" + obj.NumberOfDays + "</td>";//
                                        content += "<td>" + obj.NextMaintenanceDate + "</td>";//
                                        content += "<td><input type='button' value='Edit' onclick='EditMaintainesManagerSchedules(this)' id='" + obj.Id + "' data_issubout='" + obj.isSubOut + "' class='custombtn'/></td>";
                                        content += "<td><input type='button' value='Delete' onclick='DeleteMaintainesManagerSchedules(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                        content += "</tr>";
                                    });
                                    $("#tblMaintenanceSche tbody").empty();
                                    $("#tblMaintenanceSche tbody").append(content);
                                }
                                ClearSchedule();
                                $("#btnAddSchedule").text("Add Schedule");

                                //setEquipmentData(lstofEquipmentData.RentalUnit);
                            }

                        }

                });

            } else {
                notify('danger', "Please Put Required Red Field");
            }

        });

        function ClearSchedule(parameters) {
            $("#hdMaintenanceId").val(0);
            $("#txtdescMaintainJobDate").val("");
            $("#txtdescMaintainJob").val("");
            $("#ddlEstimateHoure").val("");
            $("#chkSubOut").prop('checked', false);
            $("#txtlastMaintanence").val("");
            $("#txtNumberDays").val("");
            $("#txtNextMainDate").val("");
        }


        //  edit doc
        function EditMaintainesManagerSchedules(curtd) {

            var id = $(curtd).attr('Id');
            //hdCommunicationId
            var isSub = $(curtd).attr('data_issubout');
            var trRow = $(curtd).closest('tr');
            var reminder = $(trRow).find('td:eq(0)').text();
            var documentJob = $(trRow).find('td:eq(1)').text();
            var EstHour = $(trRow).find('td:eq(2)').text();
            var lstdate = $(trRow).find('td:eq(3)').text();
            var numbdate = $(trRow).find('td:eq(4)').text();
            var nxtMain = $(trRow).find('td:eq(5)').text();

            $("#hdMaintenanceId").val(id);
            $("#txtdescMaintainJobDate").val(reminder);
            $("#txtdescMaintainJob").val(documentJob);
            $("#ddlEstimateHoure").val(EstHour);
            if (isSub == true) {
                $("#chkSubOut").prop('checked', true);
            } else {
                $("#chkSubOut").prop('checked', false);
            }

            $("#txtlastMaintanence").val(lstdate);
            $("#txtNumberDays").val(numbdate);
            $("#txtNextMainDate").val(nxtMain);
            $("#btnAddSchedule").text("Update");


        }
        // delete doc
        function DeleteMaintainesManagerSchedules(curtd) {

            console.log(curtd);
            var id = $(curtd).attr('Id');

            var pagePath = window.location.pathname + "/DeleteMaintenanceSchedule";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "obj": id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error in post");
                    },
                success:
                    function (result) {
                        var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {
                            if (lstofEquipmentData != null) {
                                $.each(lstofEquipmentData, function (i, obj) {
                                    ;
                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + obj.Reminder + "</td>";
                                    content += "<td>" + obj.MaintenenceJobDesc + "</td>";
                                    content += "<td>" + obj.EstimatedHours + "</td>";
                                    content += "<td>" + obj.LastmaintenanceDate + "</td>"; //DateAdded
                                    content += "<td>" + obj.NumberOfDays + "</td>"; //
                                    content += "<td>" + obj.NextMaintenanceDate + "</td>"; //
                                    content += "<td><input type='button' value='Edit' onclick='EditMaintainesManagerSchedules(this)' id='" + obj.Id + "' data_issubout='" + obj.isSubOut + "' class='custombtn'/></td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeleteMaintainesManagerSchedules(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                });
                                $("#tblMaintenanceSche tbody").empty();
                                $("#tblMaintenanceSche tbody").append(content);
                                notify('success', "Schedule Deleted Successfully");
                            } else {
                                notify('success', "Schedule Deleted Failed!!");
                            }





                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        // save part
        $(document).on('click', '#btnAddPart', function (parameters) {
            if (validationADD()) {

                var partNumber = $("#txtPartNumber").val();
                var partdesc = $("#txtPartdesc").val();
                var contPerson = $("#txtPartContactPerson").val();
                var PartEmailAddress = $("#txtPartEmailAddress").val();
                var manufac = $("#txtPartManufacturer").val();

                if (partNumber === "undefined" || partNumber === "") {
                    $("#txtPartNumber").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Part Number !!");
                    return;
                } else {
                    $("#txtPartNumber").css({ 'border': '1px solid #d2d6de' });
                }
                if (partdesc === "undefined" || partdesc === "") {
                    $("#txtPartdesc").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Part Description !!");
                    return;
                } else {
                    $("#txtPartdesc").css({ 'border': '1px solid #d2d6de' });
                }

                if (contPerson === "undefined" || contPerson == "") {
                    $("#txtPartContactPerson").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Contact Person !!");
                    return;
                } else {
                    $("#txtPartContactPerson").css({ 'border': '1px solid #d2d6de' });
                }

                if (PartEmailAddress === "undefined" || PartEmailAddress === "") {
                    $("#txtPartEmailAddress").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Part Email Address !!");
                    return;
                } else {
                    $("#txtPartEmailAddress").css({ 'border': '1px solid #d2d6de' });
                }
                if (manufac === "undefined" || manufac === "") {
                    $("#txtPartManufacturer").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Manufacturer !!");
                    return;
                } else {
                    $("#txtPartManufacturer").css({ 'border': '1px solid #d2d6de' });
                }

                var obj = {
                    Id: $("#hdPartId").val() == "" ? 0 : $("#hdPartId").val(),
                    //TenantName:
                    PartNumber: partNumber,
                    PartDescription: partdesc.trim(),
                    ContactPerson: contPerson,
                    EmailAddress: PartEmailAddress,
                    Manufacturer: manufac
                };
                var pagePath = window.location.pathname + "/SavePartData";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "obj": obj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            notify('danger', "Maintenance Schedule not saved");
                        },
                    success:
                        function (result) {
                            var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";
                            if (result.d == '') {
                                notify('danger', "Part number not saved");
                            } else {

                                var mass = $("#btnAddPart").text();
                                notify('success', "Part Number " + mass + " Successfully");

                                if (mass == "Add Part") {
                                    //$.each(lstofEquipmentData, function (i, obj) {
                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + lstofEquipmentData.PartNumber + "</td>";
                                    content += "<td>" + lstofEquipmentData.PartDescription + "</td>";
                                    content += "<td>" + lstofEquipmentData.ContactPerson + "</td>";//
                                    content += "<td>" + lstofEquipmentData.EmailAddress + "</td>";//
                                    content += "<td>" + lstofEquipmentData.Manufacturer + "</td>";//DateAdded
                                    content += "<td><input type='button' value='Edit' onclick='EdiPartDatas(this)' id='" + lstofEquipmentData.Id + "' class='custombtn'/></td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeletePartDatas(this)' id='" + lstofEquipmentData.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                    // });
                                    //$("#tblPartdata tbody").empty();
                                    $("#tblPartdata tbody").append(content);
                                } else {
                                    $.each(lstofEquipmentData, function (i, obj) {
                                        content += "<tr>";
                                        //content += "<td style='display:none'>" + obj.Id + "</td>";
                                        content += "<td>" + obj.PartNumber + "</td>";
                                        content += "<td>" + obj.PartDescription + "</td>";
                                        content += "<td>" + obj.ContactPerson + "</td>";//
                                        content += "<td>" + obj.EmailAddress + "</td>";//
                                        content += "<td>" + obj.Manufacturer + "</td>";//DateAdded
                                        content += "<td><input type='button' value='Edit' onclick='EdiPartDatas(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                        content += "<td><input type='button' value='Delete' onclick='DeletePartDatas(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                        content += "</tr>";
                                    });
                                    $("#tblPartdata tbody").empty();
                                    $("#tblPartdata tbody").append(content);
                                }
                                ClearPart();

                                //setEquipmentData(lstofEquipmentData.RentalUnit);
                            }

                        }

                });

            } else {
                notify('danger', "Please Put Required Red Field");
            }

        });
        function ClearPart(parameters) {
            $("#hdPartId").val(0);
            $("#txtPartNumber").val("");
            $("#txtPartdesc").val("");
            $("#txtPartContactPerson").val("");
            $("#txtPartEmailAddress").val("");
            $("#txtPartManufacturer").val("");
            $("#btnAddPart").text("Add Part");
        }
        // edit part
        function EdiPartDatas(curtd) {

            var id = $(curtd).attr('Id');
            //hdCommunicationId

            var trRow = $(curtd).closest('tr');
            var partnumber = $(trRow).find('td:eq(0)').text();
            var partdesc = $(trRow).find('td:eq(1)').text();
            var contctperson = $(trRow).find('td:eq(2)').text();
            var email = $(trRow).find('td:eq(3)').text();
            var manufac = $(trRow).find('td:eq(4)').text();

            $("#hdPartId").val(id);
            $("#txtPartNumber").val(partnumber);
            $("#txtPartdesc").val(partdesc);
            $("#txtPartContactPerson").val(contctperson);
            $("#txtPartEmailAddress").val(email);
            $("#txtPartManufacturer").val(manufac);
            $("#btnAddPart").text("Update");


        }
        // delete part

        function DeletePartDatas(curtd) {

            console.log(curtd);
            var id = $(curtd).attr('Id');

            var pagePath = window.location.pathname + "/DeletePart";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "obj": id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Part Deleted Failed!!");
                    },
                success:
                    function (result) {
                        var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {
                            if (lstofEquipmentData != null) {
                                $.each(lstofEquipmentData, function (i, obj) {

                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + obj.PartNumber + "</td>";
                                    content += "<td>" + obj.PartDescription + "</td>";
                                    content += "<td>" + obj.ContactPerson + "</td>";//
                                    content += "<td>" + obj.EmailAddress + "</td>";//
                                    content += "<td>" + obj.Manufacturer + "</td>";//DateAdded
                                    content += "<td><input type='button' value='Edit' onclick='EdiPartDatas(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeletePartDatas(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                });
                                $("#tblPartdata tbody").empty();
                                $("#tblPartdata tbody").append(content);
                                notify('success', "Part Deleted Successfully");
                            } else {
                                notify('danger', "Part Deleted Failed!!");
                            }





                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        // Load Vendor QuoteId
        function GetVendorQuoteId() {
            var pagePath = window.location.pathname + "/GetVendorQuoteId";
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "ownerId": $("#MainContent_ddlOwnerIdTop option:selected").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error");
                    },
                success:
                    function (result) {
                        var VendorId = $.parseJSON(decodeURIComponent(result.d));
                        $("#txtVendorQuoteId").val(VendorId);
                    }
            });
        }

        // save vendor
        function LoadVendor() {
            var pagePath = window.location.pathname + "/GetVendor";
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "ownerId": $("#MainContent_ddlOwnerIdTop option:selected").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error");
                    },
                success:
                    function (result) {

                        var lstofVendor = $.parseJSON(decodeURIComponent(result.d));
                        var content = setComboWithIntValue(lstofVendor, '-1');
                        $("#ddlVendorName option").empty();
                        $("#ddlVendorName").append(content);
                        $("#ddlVendorName").val("-1").trigger('change');
                    }
            });
        }


        $(document).on('click', '#btnAddVendor', function (parameters) {
            if (validationADD()) {


                var VendorId = $("#ddlVendorName").val();
                var VendorName = $("#ddlVendorName option:selected").text();
                var QuoteId = $("#txtVendorQuoteId").val();
                var VendorContact = $("#txtVendorContact").val();
                var VendorPhone = $("#txtVendorPhone").val();
                var VendorEmail = $("#txtVendorEmail").val();
                var AmountQuote = $("#txtAmountQuote").val();
                var WhenDone = $("#txtWhenDone").val();
                var HowLong = $("#txtHowLong").val();
                var DateWon = $("#txtDateWon").val();

                if (VendorName === "undefined" || VendorName === "-1") {
                    $("#ddlVendorName").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Vendor Name !!");
                    return;
                } else {
                    $("#ddlVendorName").css({ 'border': '1px solid #d2d6de' });
                }
                //if (QuoteId === "undefined" || QuoteId === "") {
                //    $("#txtVendorQuoteId").css({ 'border': '1px solid red' });
                //    notify('danger', "Please add Quote Id !!");
                //    return;
                //} else {
                //    $("#txtVendorQuoteId").css({ 'border': '1px solid #d2d6de' });
                //}

                if (VendorContact === "undefined" || VendorContact == "") {
                    $("#txtVendorContact").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Vendor Contact !!");
                    return;
                } else {
                    $("#txtVendorContact").css({ 'border': '1px solid #d2d6de' });
                }

                if (VendorPhone === "undefined" || VendorPhone === "") {
                    $("#txtVendorPhone").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Vendor Phone !!");
                    return;
                } else {
                    $("#txtVendorPhone").css({ 'border': '1px solid #d2d6de' });
                }
                if (VendorEmail === "undefined" || VendorEmail === "") {
                    $("#txtVendorEmail").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Vendor Email !!");
                    return;
                } else {
                    $("#txtVendorEmail").css({ 'border': '1px solid #d2d6de' });
                }

                if (AmountQuote === "undefined" || AmountQuote === "") {
                    $("#txtAmountQuote").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Amount Quote !!");
                    return;
                } else {
                    $("#txtAmountQuote").css({ 'border': '1px solid #d2d6de' });
                }
                if (WhenDone === "undefined" || WhenDone === "") {
                    $("#txtWhenDone").css({ 'border': '1px solid red' });
                    notify('danger', "Please add When Done !!");
                    return;
                } else {
                    $("#txtWhenDone").css({ 'border': '1px solid #d2d6de' });
                }
                if (HowLong === "undefined" || HowLong === "") {
                    $("#txtHowLong").css({ 'border': '1px solid red' });
                    notify('danger', "Please add How Long !!");
                    return;
                } else {
                    $("#txtHowLong").css({ 'border': '1px solid #d2d6de' });
                }
                if (DateWon === "undefined" || DateWon === "") {
                    $("#txtDateWon").css({ 'border': '1px solid red' });
                    notify('danger', "Please add Date Won !!");
                    return;
                } else {
                    $("#txtDateWon").css({ 'border': '1px solid #d2d6de' });
                }
                var obj = {
                    Id: $("#hdVendorId").val() == "" ? 0 : $("#hdVendorId").val(),
                    //TenantName:
                    VendorId: VendorId,
                    VendorName: VendorName,
                    QuoteId: QuoteId.trim(),
                    VendorContactName: VendorContact.trim(),
                    VendorPhone: VendorPhone.trim(),
                    VendorEmail: VendorEmail.trim(),
                    AmountOfQuote: AmountQuote.trim(),
                    WhenDone: WhenDone.trim(),
                    HowLong: HowLong.trim(),
                    WonQuote: DateWon.trim()
                };
                var pagePath = window.location.pathname + "/SaveVendorData";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "obj": obj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            notify('danger', "Maintenance Schedule not saved");
                        },
                    success:
                        function (result) {
                            var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                            var content = "";
                            if (result.d == '') {
                                notify('danger', "Maintenance Schedule  not saved");
                            } else {

                                var mass = $("#btnAddPart").text();
                                notify('success', "Maintenance Schedule Added Successfully");

                                if (mass == "Add Vendor") {
                                    // $.each(lstofEquipmentData, function (i, obj) {
                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + lstofEquipmentData.VendorName + "</td>";
                                    content += "<td>" + lstofEquipmentData.QuoteId + "</td>";
                                    content += "<td>" + lstofEquipmentData.VendorContactName + "</td>";//DateAdded
                                    content += "<td>" + lstofEquipmentData.VendorPhone + "</td>";//
                                    content += "<td>" + lstofEquipmentData.VendorEmail + "</td>";//
                                    content += "<td>" + lstofEquipmentData.AmountOfQuote + "</td>";//
                                    content += "<td>" + lstofEquipmentData.WhenDone + "</td>";//
                                    content += "<td>" + lstofEquipmentData.HowLong + "</td>";//
                                    content += "<td>" + lstofEquipmentData.WonQuote + "</td>";//
                                    content += "<td><input type='button' value='Delete' onclick='EditVandorDatas(this)' id='" + lstofEquipmentData.Id + "' data_VendorId='" + lstofEquipmentData.VendorId + "' class='custombtn'/></td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeleteVandorDatas(this)' id='" + lstofEquipmentData.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                    // });
                                    //$("#tblMainVendor tbody").empty();
                                    $("#tblMainVendor tbody").append(content);
                                } else {
                                    $.each(lstofEquipmentData, function (i, obj) {
                                        content += "<tr>";
                                        //content += "<td style='display:none'>" + obj.Id + "</td>";
                                        content += "<td>" + obj.VendorName + "</td>";
                                        content += "<td>" + obj.QuoteId + "</td>";
                                        content += "<td>" + obj.VendorContactName + "</td>";//DateAdded
                                        content += "<td>" + obj.VendorPhone + "</td>";//
                                        content += "<td>" + obj.VendorEmail + "</td>";//
                                        content += "<td>" + obj.AmountOfQuote + "</td>";//
                                        content += "<td>" + obj.WhenDone + "</td>";//
                                        content += "<td>" + obj.HowLong + "</td>";//
                                        content += "<td>" + obj.WonQuote + "</td>";//
                                        content += "<td><input type='button' value='Edit' onclick='EditVandorDatas(this)' id='" + obj.Id + "' data_VendorId='" + obj.VendorId + "' class='custombtn'/></td>";
                                        content += "<td><input type='button' value='Delete' onclick='DeleteVandorDatas(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                        content += "</tr>";
                                    });
                                    $("#tblMainVendor tbody").empty();
                                    $("#tblMainVendor tbody").append(content);
                                }
                                ClearVendor();

                                //setEquipmentData(lstofEquipmentData.RentalUnit);
                            }

                        }

                });

            } else {
                notify('danger', "Please Put Required Red Field");
            }

        });
        $(document).on('click', '#btnAddScheduleWork', function (parameters) {

            var pagePath = window.location.pathname + "/SetScheduleWork";
            var mainTanenceTableLength = $("#tblMaintenanceSche tbody tr").length;
            if (mainTanenceTableLength > 0) {
                Obj = [];
                $("#tblMaintenanceSche tbody tr").each(function (parameters) {
                    var currentObj = {
                        "Id": $($(this).find('td:eq(6)').find('input')).attr('id'),
                        "OwnerId": $("#MainContent_ddlOwnerIdTop option:selected").val(),
                        "PropertyManagerId": $("#MainContent_ddlPropertyManagerID option:selected").val(),
                        "LocationId": $("#MainContent_ddlLocationID option:selected").val(),
                        "UnitId": $.trim($("[id*=MainContent_txtUnitID]").val()),
                        "Title": $(this).find('td:eq(0)').text(),
                        "Description": $(this).find('td:eq(1)').text(),
                        "FromDate": $(this).find('td:eq(5)').text(),
                        "ToDate": $(this).find('td:eq(5)').text()
                    }
                    Obj.push(currentObj);

                });
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    data: JSON.stringify({ "Obj": Obj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error:
                        function (XMLHttpRequest, textStatus, errorThrown) {
                            notify('danger', "Loading Failed !!");
                        },
                    success:
                        function (result) {

                            console.log(result);
                            var mess = $.parseJSON(decodeURIComponent(result.d));

                            console.log(mess);
                            if (mess == true) {
                                notify('success', "Schedule Set Successfully !!");
                            } else {
                                notify('danger', "Schedule Set failed !!");
                            }

                        }

                });
            } else {
                notify('danger', "There is no data in Maintenances Schedule section. please add some Schedule data first");
            }
        });
        function ClearVendor(parameters) {
            $("#hdVendorId").val(0);
            $("#ddlVendorName").val("-1").trigger('change');
            $("#txtVendorQuoteId").val("");
            $("#txtVendorContact").val("");
            $("#txtVendorPhone").val("");
            $("#txtVendorEmail").val("");
            $("#txtAmountQuote").val("");
            $("#txtWhenDone").val("");
            $("#txtHowLong").val("");
            $("#txtDateWon").val("");
            $("#btnAddVendor").text("Add Vendor");
        }
        // edit vendor
        function EditVandorDatas(curtd) {

            var id = $(curtd).attr('Id');
            var VendorId = $(curtd).attr('data_VendorId');
            //hdCommunicationId

            var trRow = $(curtd).closest('tr');


            var vendorName = $(trRow).find('td:eq(0)').text();
            var quoteId = $(trRow).find('td:eq(1)').text();
            var vendorcontc = $(trRow).find('td:eq(2)').text();
            var vendorphone = $(trRow).find('td:eq(3)').text();
            var email = $(trRow).find('td:eq(4)').text();
            var amount = $(trRow).find('td:eq(5)').text();
            var WhenDone = $(trRow).find('td:eq(6)').text();
            var HowLong = $(trRow).find('td:eq(7)').text();
            var DateWon = $(trRow).find('td:eq(8)').text();

            $("#hdVendorId").val(id);
            $("#ddlVendorName").val(VendorId).trigger('change');
            $("#txtVendorQuoteId").val(quoteId);
            $("#txtVendorContact").val(vendorcontc);
            $("#txtVendorPhone").val(vendorphone);
            $("#txtVendorEmail").val(email);
            $("#txtAmountQuote").val(amount);
            $("#txtWhenDone").val(WhenDone);
            $("#txtHowLong").val(HowLong);
            $("#txtDateWon").val(DateWon);
            $("#btnAddVendor").text("Update");


        }
        // delete vendor
        function DeleteVandorDatas(curtd) {

            console.log(curtd);
            var id = $(curtd).attr('Id');

            var pagePath = window.location.pathname + "/DeleteVendor";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "obj": id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Vendor Deleted Failed!!");
                    },
                success:
                    function (result) {
                        var lstofEquipmentData = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (result.d == '') {

                        } else {
                            if (lstofEquipmentData != null) {
                                $.each(lstofEquipmentData, function (i, obj) {
                                    content += "<tr>";
                                    //content += "<td style='display:none'>" + obj.Id + "</td>";
                                    content += "<td>" + obj.VendorName + "</td>";
                                    content += "<td>" + obj.QuoteId + "</td>";
                                    content += "<td>" + obj.VendorContactName + "</td>";//DateAdded
                                    content += "<td>" + obj.VendorPhone + "</td>";//
                                    content += "<td>" + obj.VendorEmail + "</td>";//
                                    content += "<td>" + obj.AmountOfQuote + "</td>";//
                                    content += "<td>" + obj.WhenDone + "</td>";//
                                    content += "<td>" + obj.HowLong + "</td>";//
                                    content += "<td>" + obj.WonQuote + "</td>";//
                                    content += "<td><input type='button' value='Edit' onclick='EditVandorDatas(this)' id='" + obj.Id + "' data_VendorId='" + obj.VendorId + "' class='custombtn'/></td>";
                                    content += "<td><input type='button' value='Delete' onclick='DeleteVandorDatas(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                                    content += "</tr>";
                                });
                                $("#tblMainVendor tbody").empty();
                                $("#tblMainVendor tbody").append(content);
                                notify('success', "Part Deleted Successfully");
                            } else {
                                notify('danger', "Part Deleted Failed!!");
                            }

                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }


        $(document).on('click', '#btnSendQuote', function (parameters) {
            var pagePath = window.location.pathname + "/SendCote";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "code": $("#txtVendorQuoteId").val(), "MainTo": $("#txtVendorEmail").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Loading Failed !!");
                    },
                success:
                    function (result) {
                        var mess = $.parseJSON(decodeURIComponent(result.d));

                        if (mess == 'true') {
                            notify('success', "Quote Sent Successfully !!");
                        } else {
                            notify('danger', "Quote Send failed !!");
                        }

                    }

            });
        });
        $(document).on('click', '#btnAddWinningNotice', function (parameters) {
            var pagePath = window.location.pathname + "/SendWinningNotice";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "MainTo": $("#txtVendorEmail").val(), "code": $("#txtVendorQuoteId").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Notice sendi Failed !!");
                    },
                success:
                    function (result) {
                        var mess = $.parseJSON(decodeURIComponent(result.d));

                        if (mess == 'true') {
                            notify('success', "Notice Sent Successfully !!");
                        } else {
                            notify('danger', "Notice Send failed !!");
                        }

                    }

            });
        });

        $("#MainContent_txtWhenDone").datepicker({
            dateFormat: "dd-mm-yy",
            changeYear: true,
            changeMonth: true
        });
        $("#MainContent_txtDateWon").datepicker({
            dateFormat: "dd-mm-yy",
            changeYear: true,
            changeMonth: true
        });

        function LoadSender(parameters) {
            //ddlSender
            var pagePath = window.location.pathname + "/GetSender";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Loading Failed !!");
                    },
                success:
                    function (result) {
                        var lstofSender = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";


                        if (lstofSender != '') {
                            content = setCombo_withInt(lstofSender, '-1');
                            console.log(content);
                            $("#ddlSender option").empty();
                            $("#ddlSender").append(content);
                            $("#ddlSender").val("-1").trigger('change');
                        } else {


                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }

        function LoadMessage(Request, SenderId) {
            //ddlSender
            var pagePath = window.location.pathname + "/GetOwnerMessage";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "RequestType": Request, "SenderId": SenderId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Loading Failed !!");
                    },
                success:
                    function (result) {
                        var Message = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (Message != '') {
                            BindMessage(Message);
                            //content = setCombo_withInt(lstofSender, '-1');

                        } else {


                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }
        function BindMessage(Message) {

            var content = "";
            var j = 0;
            if (Message.length > 0) {
                $.each(Message, function (i, obj) {
                    j++;

                    content += "<p style='margin-top: 0px;margin-bottom: 0px;width: 50%;float: left;'>From - <span style='font-weight:bold'>" + obj.FromUser + "</span> - <span style='font-weight:bold'>" + obj.fromDate + "  " + obj.FromTime + "</span></p>";
                    content += "<p style='width: 50%;float: right;text-align:right'>Request Type :- " + obj.RequestType + " </p>";
                    content += "<p style='margin-left:10px;'>" + obj.QustionMessage + "</p>";

                    if (obj.ToUserId != "") {
                        content += "<p style='color: #8BB7DE;margin-bottom: 0px;'>Answer - <span style='font-weight:bold'>" + obj.ToUser + "</span> - <span style='font-weight:bold'>" + obj.ToDate + "  " + obj.ToTime + "</span></p>";
                        content += "<p style='color:#8BB7DE;margin-bottom:10px;margin-left:10px;'>" + obj.AnswerFromOwner + "</p>";
                    } else {
                        content += "<p id='p" + j + "'><input type='button' class='btn btnNewColor'  value='Reply' data_Id=" + j + " data_QuestionId=" + obj.Id + " onclick='ReplyClick(this)' id='Rep'" + j + " /></p>";
                        content += "<div style='margin-bottom:10px;display:none' id='div" + j + "'>";
                        content += "<span><textarea style='width:50%;' id='txtRep" + j + "' rows='5' cols='10' class='form-control'></textarea></span>";
                        content += "<span style='margin-left:10px;margin-right:10px;'>";
                        content += "<input type='button' class='btn btnNewColor' style='margin-right:10px;' value='Send' data_rowId=" + j + " data_QuestionId=" + obj.Id + " onclick='MessageSaveClick(this)' id='btnRepSave'" + j + " /></span>";
                        content += "<span><input type='button' value='Cancel' class='btn btnNewColor' data_rowId=" + j + " onclick='MessageCloseClick(this)' id='btnRepClose'" + j + " /></span></span>";
                        content += "</div>";

                    }

                });
                $("#messageId").html("");
                $("#messageId").html(content);
            }
        }

        function ReplyClick(Rep) {
            var rowid = $(Rep).attr("data_Id");

            var pId = "p" + rowid;
            var divId = "div" + rowid;

            $("#" + pId).hide();
            $("#" + divId).show();
        }
        function MessageSaveClick(q) {

            var QuestionId = $(q).attr("data_QuestionId");
            var rowid = $(q).attr("data_rowId");
            var txtData = $("#txtRep" + rowid).val();

            var pagePath = window.location.pathname + "/SaveMessageOwner";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "QuestionId": QuestionId, "txtData": txtData }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Loading Failed !!");
                    },
                success:
                    function (result) {
                        var Message = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (Message != '') {
                            BindMessage(Message);
                            //content = setCombo_withInt(lstofSender, '-1');

                        } else {


                            //setEquipmentData(lstofEquipmentData.RentalUnit);
                        }

                    }

            });
        }

        function MessageCloseClick(Rep) {
            var rowid = $(Rep).attr("data_rowId");

            var pId = "p" + rowid;
            var divId = "div" + rowid;

            $("#" + pId).show();
            $("#" + divId).hide();
        }

        $(document).on('change', '#ddlSender', function (parameters) {

            var Sender = $(this).val() == "-1" ? "" : $(this).val();
            var Request = $("#ddlEmailType").val() == '-1' ? "" : $("#ddlEmailType").val();
            LoadMessage(Request, Sender);
        });
        $(document).on('change', '#ddlEmailType', function (parameters) {
            var Sender = $("#ddlSender").val() == "-1" ? "" : $("#ddlSender").val();
            var Request = $(this).val() == '-1' ? "" : $(this).val();
            LoadMessage(Request, Sender);
        });
        $(document).on('click', '#btnAddEquipment', function (parameters) {
            // $("#tabs").tabs("option", "active", 2);
            $('a[href="#Equipment"]').click();
        });

        /*  WebAnalytics  */

        //$(document).on('change', '#txtWebAnalyticsfTo', function (parameters) {
        //    $('#tblWebAnalytics tbody').empty();
        //    var Obj = {
        //        "OwnerId": $("#MainContent_ddlOwnerIdTop option:selected").val(),
        //        "PropertyManagerId": $("#MainContent_ddlPropertyManagerID option:selected").val(),
        //        "LocationId": $("#MainContent_ddlLocationID option:selected").val(),
        //        "UnitId": $.trim($("[id*=MainContent_txtUnitID]").val()),
        //        "from": $('#txtWebAnalyticsfFrom').val(),
        //        "to": $('#txtWebAnalyticsfTo').val(),
        //        "Postarea": $("#ddlPostarea option:selected").val()
        //    }
        //    var pagePath = window.location.pathname + "/GetWebAnalyticsData";
        //    $.ajax({
        //        type: "POST",
        //        url: pagePath,
        //        data: JSON.stringify({ "Obj": Obj }),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        error:
        //            function (XMLHttpRequest, textStatus, errorThrown) {
        //                notify('danger', "Loading Failed !!");
        //            },
        //        success:
        //            function (result) {
        //                var Message = $.parseJSON(decodeURIComponent(result.d));
        //                var content = '';
        //                $(Message).each(function (index, element) {
        //                    content += '<tr>' +
        //                        '<td>' + element.OwnerId + '</td>' +
        //                        '<td>' + element.PropertyManagerId + '</td>' +
        //                        '<td>' + element.LocationId + '</td>' +
        //                        '<td>' + element.UnitId + '</td>' +
        //                        '<td>' + element.Nolinksposted + '</td>' +
        //                        '<td>' + element.NOViews + '</td>' +
        //                        '<td>' + element.NoSchedules + '</td>' +
        //                        '<td>' + element.NOApplication + '</td>' +
        //                        '</tr>';
        //                });
        //                $('#tblWebAnalytics tbody').append(content);
        //                $('#txtTotalApplicationFees').val(Message[0].TotalApplicationFees);
        //                $('#txtMTDShowings').val(Message[0].MTDShowings);
        //                $('#txtTotalShowings').val(Message[0].TotalShowings);
        //                $('#txtMTDViews').val(Message[0].MTDViews);
        //                $('#txtTotalViews').val(Message[0].TotalViews);
        //            }
        //    });
        //});

        $(document).on('change', '#txtWebAnalyticsfTo', function (parameters) {
            // $('#tblWebAnalytics tbody').empty();
            var Obj = {
                "OwnerId": $("#MainContent_ddlOwnerIdTop option:selected").val(),
                "PropertyManagerId": $("#MainContent_ddlPropertyManagerID option:selected").val(),
                "LocationId": $("#MainContent_ddlLocationID option:selected").val(),
                "UnitId": $.trim($("[id*=MainContent_txtUnitID]").val()),
                "from": $('#txtWebAnalyticsfFrom').val(),
                "to": $('#txtWebAnalyticsfTo').val()
            }
            var pagePath = window.location.pathname + "/GetWebAnalyticsData";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "Obj": Obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Loading Failed !!");
                    },
                success:
                    function (result) {
                        var Message = $.parseJSON(decodeURIComponent(result.d));
                        var content = '';
                        //$(Message).each(function (index, element) {
                        //    content += '<tr>' +
                        //        '<td>' + element.OwnerId + '</td>' +
                        //        '<td>' + element.PropertyManagerId + '</td>' +
                        //        '<td>' + element.LocationId + '</td>' +
                        //        '<td>' + element.UnitId + '</td>' +
                        //        '<td>' + element.Nolinksposted + '</td>' +
                        //        '<td>' + element.NOViews + '</td>' +
                        //        '<td>' + element.NoSchedules + '</td>' +
                        //        '<td>' + element.NOApplication + '</td>' +
                        //        '</tr>';
                        //});

                        //$('#tblWebAnalytics tbody').append(content);

                        if (Message[0] != null) {
                            if (Message[0].MTDViews != null) {
                                $('#txtMTDListing').val(Message[0].MTDViews);
                                $('#txtMTDViews').val(Message[0].MTDViews);
                            }
                            if (Message[0].TotalViews != null) {
                                $('#txtTotalListing').val(Message[0].TotalViews);
                                $('#txtTotalViews').val(Message[0].TotalViews);
                            }
                            if (Message[0].MTDSchedules != null) {
                                $('#txtMTDSchedules').val(Message[0].MTDSchedules);
                            }
                            if (Message[0].TotalSchedules != null) {
                                $('#txtTotalSchedules').val(Message[0].TotalSchedules);
                            }
                            if (Message[0].MTDStart != null) {
                                $('#txtMTDStarted').val(Message[0].MTDStart);
                            }
                            if (Message[0].TotalStart != null) {
                                $('#txtTotalStarted').val(Message[0].TotalStart);
                            }
                            if (Message[0].MTDCompleted != null) {
                                $('#txtMTDCompleted').val(Message[0].MTDCompleted);
                            }
                            if (Message[0].TotalCompleted != null) {
                                $('#txtTotalCompleted').val(Message[0].TotalCompleted);
                            }

                        }

                    }

            });

        });

        function LoadWebAnalyticsBarChart(parameters) {

            var Obj = {
                "Id": $($(this).find('td:eq(6)').find('input')).attr('id'),
                "OwnerId": $("#MainContent_ddlOwnerIdTop option:selected").val(),
                "PropertyManagerId": $("#MainContent_ddlPropertyManagerID option:selected").val(),
                "LocationId": $("#MainContent_ddlLocationID option:selected").val(),
                "UnitId": $.trim($("[id*=MainContent_txtUnitID]").val())
            }
            var pagePath = window.location.pathname + "/GetWebAnalyticsBarChartData";
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ "Obj": Obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Loading Failed !!");
                    },
                success:
                    function (result) {
                        var Message = $.parseJSON(decodeURIComponent(result.d));

                        var data = [
                                { y: 'January', a: Message[0]["January"], b: Message[1]["January"] },
                                { y: 'February', a: Message[0]["February"], b: Message[1]["February"] },
                                { y: 'March', a: Message[0]["March"], b: Message[1]["March"] },
                                { y: 'April', a: Message[0]["April"], b: Message[1]["April"] },
                                { y: 'May', a: Message[0]["May"], b: Message[1]["May"] },
                                { y: 'June', a: Message[0]["June"], b: Message[1]["June"] },
                                { y: 'July', a: Message[0]["July"], b: Message[1]["July"] },
                                { y: 'August', a: Message[0]["August"], b: Message[1]["August"] },
                                { y: 'September', a: Message[0]["September"], b: Message[1]["September"] },
                                { y: 'October', a: Message[0]["October"], b: Message[1]["October"] },
                                { y: 'November', a: Message[0]["November"], b: Message[1]["November"] },
                                { y: 'December', a: Message[0]["December"], b: Message[1]["December"] },
                                { y: 'TOTAL', a: Message[0]["TOTAL"], b: Message[1]["TOTAL"] }
                        ],
                            config = {
                                data: data,
                                xkey: 'y',
                                ykeys: ['a', 'b'],
                                labels: ['Total ' + Message[0]["Name"], 'Total ' + Message[1]["Name"]],
                                fillOpacity: 0.6,
                                hideHover: 'auto',
                                behaveLikeLine: true,
                                resize: true,
                                xLabelAngle: 45,
                                barGap: 1,
                                //barSizeRatio: 0.4,
                                pointFillColors: ['#ffffff'],
                                pointStrokeColors: ['black'],
                                lineColors: ['gray', 'red'],
                                barColors: ['#00a65a', '#f56954']
                            };

                        config.element = 'bar-chart';
                        Morris.Bar(config);
                    }

            });

            /* web End*/
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

        .nav-tabs-custom {
            background-color: #DFE3EE;
        }

            .nav-tabs-custom > .nav-tabs {
                border-bottom-color: initial;
                /*padding: 0px 0px 0px 18px;*/
                padding: 0px 0px 0px 5px;
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
            padding: 10px 10px 60px 5px;
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

        #addPress {
            margin-bottom: 20px;
            float: left;
        }

        .lblitemname {
            margin-top: 10px;
        }

        .bgimg {
            /*background-image: url(../../Images/u4_normal.png);
            background-repeat: no-repeat;*/
            padding: 10px 10px 10px 10px;
            /*background-color: #98cdfe;*/
            border: 1px solid #373737;
            margin-bottom: 10px;
        }

        #FeatureName {
            width: 100%;
        }

        .btnSaveWebCenter {
            text-align: center;
        }

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

        .pdfobject-container {
            height: 500px;
        }

        .pdfobject {
            border: 1px solid #666;
        }
    </style>
</asp:Content>
