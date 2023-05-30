<%@ Page Title="EProperty365: Tenant Documents" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="TenantDocuments.aspx.cs" Inherits="eProperty.Pages.Resident.TenantDocuments" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Content/js/pdfobject.js"></script>
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/TenantAllJs/TenantDocuments.js"></script>
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

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box" style="border: 1px solid #000000;">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="row">
                    <div class="box-header with-border CommonHeader col-md-12" style="margin-top: 0px;">
                        <h3 class="box-title" id="H1" runat="server">Tenant Documents</h3>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <label for="" class="col-sm-2 control-label">*Tenant Name </label>
                        <div class="col-sm-4">
                            <span id="txtTenantName"></span>
                        </div>
                    </div>
                    <div class="row" style="padding: 10px">
                        <table class="table table-responsive table-bordered table-striped" style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblAdditionalDoc">
                            <thead>
                                <tr>
                                    <th style="width: 33%">Document Description</th>
                                    <th style="width: 33%">Document Name</th>
                                    <th style="width: 33%">File Path</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>

                    <div class="row" style="padding: 10px">
                        <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1">
                            <tbody>
                                <tr>
                                    <td style="width: 42%"><span style="width: 100%; float: left">Enter Document Description</span>
                                        <span style="width: 100%; float: left;">
                                            <input type="text" id="txtAddiDoc" class="form-control" />
                                        </span>
                                    </td>
                                    <td>
                                        <span style="width: 100%; float: left;">
                                            <input type="file" id="documentUpload" />
                                        </span>                                      
                                    </td>
                                </tr>                               
                            </tbody>
                        </table>
                    </div>

                    <div class="row">
                        <div class="col-sm-6" style="text-align: center">
                            <input id="savedoc" type="button" class="btn btnNewColor" value="Upload" onclick="SavedocumentUpload();" />
                        </div>
                        <div class="col-sm-6" style="text-align: center">
                           <input type="button" class="btn btnNewColor" id="btnCancel" style="background-color: #3B5998" value="Cancel" />
                        </div>
                    </div>                   
                </div>
            </div>
        </div>
    </div>


</asp:Content>

