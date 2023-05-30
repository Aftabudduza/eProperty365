<%@ Page Title="EProperty365: Residential Tenant Deshboard" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="ResidentTenantDashboard.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentTenantDashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="col-md-12">
            <div class="box-body">
                <form id="form1" runat="server" class="form-horizontal">
                    <table style="width: 100%; float: left; margin-top: 10px;" width="100%;">
                        <tbody>
                            <tr>
                                <td style="width: 70%; vertical-align: top;">
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="row">
                                                    <div class="box-header with-border CommonHeader col-md-12" style="margin-top: 0px;">
                                                        <h3 class="box-title" id="H1" runat="server">Residential Tenant Deshboard</h3>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <label for="" class="col-sm-3 control-label">Welcome Tenant Name :</label>
                                                            <div class="col-sm-9">
                                                                <span id="txtTenantName"></span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <label for="txtTypeOfEquipment" class="col-sm-12 control-label">Manage Account Options :</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="" class="col-sm-6 control-label">Type Email Request :</label>
                                                            <div class="col-sm-6">
                                                                <select id="ddlRequestType" class="select2">
                                                                    <option value="-1">All</option>
                                                                    <option value="Report Issue to the Manager">Report Issue</option>
                                                                    <option value="Maintenance Request">Request Maintenances</option>
                                                                    <option value="Schedule Exit Inspection">Schedule Exit Inspection</option>
                                                                </select>
                                                            </div>
                                                        </div>


                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <input type="checkbox" id="showMore" />Show more 30 days
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <p id="txtMessageLoad" style="width: 100%; border: 1px solid #000000; padding: 10px; background-color: #FCFEFF; height: 256px; overflow-y: scroll; overflow-x: hidden; border-radius: 6px; box-shadow: 2px 1px 1px 0px;"></p>

                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label for="" class="col-sm-12 control-label">Message :</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <textarea id="txtMessagesend" rows="5" cols="50" style="width: 100%"></textarea>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <button type="button" id="btnSendQuote" class="btn col-sm-10" style="background-color: #66FF00">Send</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="nav-tabs-custom">
                                                <ul id="topnav" class="nav nav-tabs" style="background-repeat: no-repeat;">
                                                    <li><a href="TenantProfile_DashBoard.aspx">Tenant Profile</a></li>
                                                    <li><a href="TenantDocuments.aspx">Document Vault</a></li>
                                                    <li><a href="TenantOnlinefee.aspx">Make Payment</a></li>
                                                    <li><a href="TenantPaymentHistory.aspx">Payment History</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 30%; vertical-align: text-bottom;">
                                    <div style="margin-top: 20px; text-align: center">
                                        <table class="auto-style47">
                                            <tr>
                                                <td class="auto-style48">
                                                    <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70110"></script>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style48">
                                                    <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70111"></script>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style48">
                                                    <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70112"></script>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style48">
                                                    <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70113"></script>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style48">
                                                    <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70114"></script>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style48">
                                                    <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70115"></script>
                                                </td>
                                            </tr>

                                        </table>

                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </form>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Content/js/pdfobject.js"></script>
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/TenantAllJs/ResidentialTenantDashboard.js"></script>
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
            padding: 10px 10px 10px 10px;
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
