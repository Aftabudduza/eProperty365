<%@ Page Title="EProperty365: Maintenance Manager Unit" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="ManagementDashboard.aspx.cs" Inherits="eProperty.Pages.Resident.ManagementDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="form-group row">
                <h6>Location Overview Search:</h6>
            </div>
            <div class="form-group row">
                <label for="ddlOwnerId" class="col-sm-2">*Owner ID:</label>
                <div class="col-sm-3">
                    <input type="text" data-value="" class="form-control" id="txtOwnerId" disabled="disabled" />
                </div>
                <label for="ddlPropertyManagerId" class="col-sm-3">*Enter Property Manager ID:</label>
                <div class="col-sm-4">
                    <select class="form-control ddl" id="ddlPropertyManagerId"></select>
                </div>
            </div>
            <div class="form-group row">
                <label for="ddlLocation" class="col-sm-2 col-form-label">*Location ID:</label>
                <div class="col-sm-2">
                    <select class="form-control ddl" id="ddlLocation"></select>
                </div>
                <label for="ddlUnitID" class="col-sm-1 col-form-label">*Unit ID:</label>
                <div class="col-sm-2">
                    <select class="form-control ddl" id="ddlUnitID"></select>
                </div>
                <label class="col-sm-1 col-form-label" for="txtTenantName">*Tenant Name: </label>
                <div class="col-sm-4">
                    <input type="text" class="form-control" id="txtTenantName" data-value="" disabled="disabled" />
                </div>
            </div>

            <div class="form-group row">
                <div class="text-left">
                    Filters :
                     <label style="margin-right: 7px">
                         <input type="checkbox" id="rdoByProperty" name="FilterType" class="flat-red" value="ByProperty" />
                         By Property
                     </label>
                    <label style="margin-right: 7px">
                        <input type="checkbox" name="FilterType" value="ByTenant" id="rdoByTenant" class="flat-red" />
                        By Tenant 
                    </label>
                    <%--<div class="custom-control custom-radio custom-control-inline">
                         <input type="radio" id="rdoPayment" name="TransactionType" class="custom-control-input" value="Payment"/>
                         <label class="custom-control-label" for="rdoPayment">Payment</label>
                     </div>
                     <div class="custom-control custom-radio custom-control-inline">
                         <input type="radio" id="rdoCreditTenantAccount" name="TransactionType" class="custom-control-input"  value="CreditToTenantAccount"/>
                         <label class="custom-control-label" for="rdoCreditTenantAccount">Credit to Tenant Account</label>
                     </div>--%>
                </div>
            </div>
            <div class="form-group row">
                <div class="col-sm-12">
                    <div class="divTable" style="overflow: auto; max-height: 150px; border: 1px solid grey">
                        <table id="tbl" class="table table-bordered table-responsive">
                            <thead>
                                <tr>
                                    <th>Owner</th>
                                    <th>Properly Manager</th>
                                    <th>Property Location</th>
                                    <th>Unit</th>
                                    <th>Rental YTD</th>
                                    <th>Month Rental</th>
                                    <th>Status</th>
                                    <th>Outstanding Issues</th>
                                    <th>Emergency Contact</th>
                                    <th>Emergency Phone </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="form-group row" style="min-height: 550px; padding-top: 10px;">
                <div class="col-12">
                    <%--<div id="">
                    <div class="lnb-new-schedule">
                        <button id="btn-new-schedule" type="button" class="btn btn-default btn-block lnb-new-schedule-btn" data-toggle="modal">
                            New schedule</button>
                    </div>
                </div>--%>
                    <span id="menu-navi">
                        <button type="button" class="btn btn-default btn-sm move-today" data-action="move-today">Today</button>
                        <button type="button" class="btn btn-default btn-sm " data-action="move-prev">
                            <i class="calendar-icon ic-arrow-line-left" data-action="move-prev"></i>Prev
                        </button>
                        <button type="button" class="btn btn-default btn-sm" data-action="move-next">
                            Next
                            <i class="calendar-icon ic-arrow-line-right" data-action="move-next"></i>
                        </button>
                    </span>
                    <span id="renderRange" class="render-range"></span>
                    <div id="" style="overflow: auto;">
                        <div id="calendar" style="height: 500px; top: 50px;"></div>
                    </div>
                </div>
            </div>
            <div class="form-group row" style="padding-top: 10px;">
                <div class="col-12" style="text-align:center; margin-top:20px;">
                    <input type="button" class="btn btn-success" id="btnExit" value="< Back" />
                     <%--<asp:Button ID="btnBack" runat="server" Text="< Back" OnClick="btnBack_Click" CssClass="btn btnNewColor " />--%>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/plugins/tui.calendar-master/examples/css/tui-date-picker.css" rel="stylesheet" />
    <link href="../../Content/plugins/tui.calendar-master/examples/css/tui-time-picker.css" rel="stylesheet" />
    <%--<link rel="stylesheet" type="text/css" href="https://uicdn.toast.com/tui.time-picker/latest/tui-time-picker.css">
    <link rel="stylesheet" type="text/css" href="https://uicdn.toast.com/tui.date-picker/latest/tui-date-picker.css">--%>
    <link href="../../Content/plugins/tui.calendar-master/dist/tui-calendar.css" rel="stylesheet" />
    <link href="../../Content/plugins/tui.calendar-master/examples/css/default.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../../Content/plugins/tui.calendar-master/examples/css/icons.css" />
    <style type="text/css">
        .tui-full-calendar-weekday-grid-line .tui-full-calendar-near-month-day .tui-full-calendar-extra-date {
            width: 10% !important;
        }
        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            text-align: center;
            vertical-align: middle !important;
        }
    </style>


    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/TenantAllJs/ManagementDashboard.js"></script>
    <%--<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
            crossorigin="anonymous"></script>--%>
    <%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>--%>
    <%--<script type="text/javascript" src="https://uicdn.toast.com/tui.code-snippet/latest/tui-code-snippet.min.js"></script>--%>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/tui-code-snippet.min.js"></script>
    <%--<script type="text/javascript" src="https://uicdn.toast.com/tui.time-picker/latest/tui-time-picker.min.js"></script>--%>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/tui-time-picker.min.js"></script>
    <%--<script type="text/javascript" src="https://uicdn.toast.com/tui.date-picker/latest/tui-date-picker.min.js"></script>--%>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/tui-date-picker.min.js"></script>
    <%--<script type="text/javascript"  src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.20.1/moment.min.js"></script>--%>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/moment.min.js"></script>
    <%--<script type="text/javascript"  src="https://cdnjs.cloudflare.com/ajax/libs/chance/1.0.13/chance.min.js"></script>--%>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/chance.min.js"></script>
    <%--<script src=" ../dist/tui-calendar.js"></script>--%>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/dist/tui-calendar.js"></script>

    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/data/calendars.js"></script>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/data/schedules.js"></script>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/theme/dooray.js"></script>
    <script type="text/javascript" src="../../Content/plugins/tui.calendar-master/examples/js/default.js"></script>
</asp:Content>
