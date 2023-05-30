<%@ Page Title="EProperty365: Residential Ad Template Unit Listing" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="ResidentialUnitListing.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialUnitListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <div class="box">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border CommonHeader col-md-12">
                        <h3 class="box-title" id="H5">Residential Ad Template Unit Listing </h3>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12" style="padding-left: 5px; text-align: center">
                            <table id="tblResidentialManager" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>UnitId</th>
                                        <th>Unit Name</th>
                                        <th>Action</th>

                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>

                   <%-- <div class="box-body">
                        <div class="col-md-12" style="padding-left: 5px; text-align: center">
                            <table id="tblResidentialManager_Step4" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>UnitId</th>
                                        <th>Tenant Id</th>
                                        <th>Approval Code</th>
                                        <th>Action</th>

                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>--%>
                    
                </div>
            </div>

        </div>

    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var currentPagePath = window.location.pathname + "/";
        $(document).ready(function () {
            LoadResidentialGrid();
            //LoadResidentialGrid_Approval();          

        });
        function LoadResidentialGrid() {

            var pagePath = currentPagePath + "LoadResidentialGrid";
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
                        notify('danger', "Error"); //../../Uploads/Images/38860545_2093714167366803_1085132197528076288_n.jpg
                    },
                success:
                    function (result) {

                        var lstResidentialUnit = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        $.each(lstResidentialUnit, function (i, obj) {

                            content += "<tr>";
                            //content += "<td style='display:none'>" + obj.Id + "</td>";
                            content += "<td>" + obj.Serial + "</td>";
                            content += "<td>" + obj.UnitName + "</td>";
                            content += "<td> <input type='button' value='View' onclick='ViewResidentialUnit(this)'  id='" + obj.Serial + "' class='custombtn'/></td>";
                            content += "</tr>";
                        });
                        $("#tblResidentialManager tbody").empty();
                        $("#tblResidentialManager tbody").append(content);
                    }
            });
        }

        function ViewResidentialUnit(unitRow) {

            var id = $(unitRow).attr('Id');
            //hdCommunicationId
            var pathname = window.location.pathname; // Returns path only
            var url = window.location.href;     // Returns full URL
            var origin = window.location.origin;   // Returns base URL
            var url = origin + "/Pages/Resident/ResidentialAddResponceTemplate_Login.aspx?ResidentialUnitSerial=" + id;
            window.location.href = url;

        }

        //function LoadResidentialGrid_Approval() {

        //    var pagePath = currentPagePath + "LoadResidentialGrid_Approval";
        //    var obj = {};
        //    $.ajax({
        //        type: "POST",
        //        url: pagePath,
        //        data: JSON.stringify(obj),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        error:
        //            function (XMLHttpRequest, textStatus, errorThrown) {
        //                // alert("Error");
        //                notify('danger', "Error"); //../../Uploads/Images/38860545_2093714167366803_1085132197528076288_n.jpg
        //            },
        //        success:
        //            function (result) {

        //                var lstResidentialUnit = $.parseJSON(decodeURIComponent(result.d));
        //                var content = "";
        //                $.each(lstResidentialUnit, function (i, obj) {

        //                    content += "<tr>";
        //                    //content += "<td style='display:none'>" + obj.Id + "</td>";
        //                    content += "<td>" + obj.UnitId + "</td>";
        //                    content += "<td>" + obj.SerialId + "</td>";
        //                    content += "<td>" + obj.ApprovalCode + "</td>";
        //                    content += "<td> <input type='button' value='View' data_SerialId ='"+ obj.SerialId+"' onclick='ViewResidentialUnit_Approval(this)'  id='" + obj.UnitId + "' class='custombtn'/></td>";
        //                    content += "</tr>";
        //                });
        //                $("#tblResidentialManager_Step4 tbody").empty();
        //                $("#tblResidentialManager_Step4 tbody").append(content);
        //            }
        //    });
        //}


        //function ViewResidentialUnit_Approval(unitRow) {

        //    var id = $(unitRow).attr('Id');
        //    var Teant = $(unitRow).attr('data_SerialId');
        //    //hdCommunicationId
        //    var pathname = window.location.pathname; // Returns path only
        //    var url = window.location.href;     // Returns full URL
        //    var origin = window.location.origin;   // Returns base URL
        //    var url = origin + "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit_Login.aspx?ResidentialUnitSerial=" + id + "&&TenentId=" + Teant;
        //    window.location.href = url;

        //}
    </script>
    <style type="text/css">
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
            margin-top: 15px;
        }
    </style>
</asp:Content>
