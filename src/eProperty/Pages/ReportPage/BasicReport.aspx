<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="BasicReport.aspx.cs" Inherits="eProperty.Pages.ReportPage.BasicReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="col-md-12" style="padding-left: 2px;">
            <div class="box-header with-border CommonHeader col-md-12" style="float: none;">
                <h3 class="box-title">Basic Report Page</h3>
            </div>
            <div class="box-body" style="padding: 5px;">
                <div class="row">
                        <div class="col-6">
                            <div class="form-group row">
                                 <label for="ddlReportName" class="col-sm-4 col-form-label">Report Name:</label>
                                <div class="col-8">
                                    <select id="ddlReportName" class="form-control ddl"></select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-6"></div>
                    <div class="col-6"></div>
                </div>
                <div class="row" style="margin-top: 20px;">
                    <div class="col-12">
                        <div class="form-group row">
                            <div class="text-center mx-auto">
                                <input type="button" class="btn" style="background-color: #3B5998" value="Cancel" id="btnCancel" />
                                <input type="button" class="btn" style="background-color: #66FF00" value="Show Report" data-id="0" id="btnShowReport" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/ReportJs/BasicReport.js"></script>
</asp:Content>
