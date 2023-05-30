<%@ Page Title="EProperty365: Residential Ad response Site template" Language="C#" AutoEventWireup="true" CodeBehind="ResidentialAddResponseTemplateNew.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialAddResponseTemplateNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EProperty365: Residential Ad response Site template</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0)" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="SKYPE_TOOLBAR" content="SKYPE_TOOLBAR_PARSER_COMPATIBLE" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="stylesheet" href="../../Content/bootstrap.css" />
    <link rel="stylesheet" href="../../Content/jquery-ui.css" />
    <link rel="stylesheet" href="../../Content/toastr.css" />
    <link rel="stylesheet" href="../../Content/font-awesome.css" />
    <link rel="stylesheet" href="../../Content/ionicons/ionicons.css" />
    <link rel="stylesheet" href="../../AdminLTE/css/AdminLTE.css" />
    <link rel="stylesheet" href="../../AdminLTE/css/skins/_all-skins.css" />
    <link rel="stylesheet" href="../../Content/admin.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link type="text/css" href="../../scripts/notification/notification.css" rel="stylesheet" />
    <link href="../../scripts/select2/dist/css/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../scripts/select2/dist/css/select2.css" rel="stylesheet" />
    <link type="text/css" href="../../AdminLTE/iCheck/all.css" rel="stylesheet" />
    <link href="../../css/Site.css" rel="stylesheet" />
    <link href="../../AppJs/AdditionalCSS/timepicki.css" rel="stylesheet" />

    <!-- Web Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,400,700,300&amp;subset=latin,latin-ext' rel='stylesheet' type='text/css'>
    <link href='https://fonts.googleapis.com/css?family=PT+Serif' rel='stylesheet' type='text/css'>
    <script type="text/javascript">
        if (window.innerWidth <= 350) {

            google_ad_width = 320;
            google_ad_height = 50;
        } else if (window.innerWidth >= 750) {

            google_ad_width = 728;
            google_ad_height = 90;
        } else {

            google_ad_width = 468;
            google_ad_height = 60;
        }
    </script>
    <style type="text/css">
        .skin-blue .sidebar-menu .treeview-menu > li > a.active {
            color: red;
        }

        .input-validation-error {
            border: 1px solid #ff0000;
            background-color: #ffeeee !important;
        }

        .field-validation-error {
            color: #ff0000 !important;
        }

        .nav-tabs-custom {
            background: none;
        }

            .nav-tabs-custom > .nav-tabs {
                border-bottom-color: initial;
                padding: 0px 0px 0px 18px;
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

        .nav-tabs-custom > .nav-tabs > li > a, .nav-tabs-custom > .nav-tabs > li > a:hover {
            background: #ffffff;
        }

        .nav-tabs-custom > .nav-tabs > li > a {
            border-radius: 10px 10px 0px 0px;
        }

        .nav-tabs {
            border-bottom: none;
        }

        .box-header.with-border {
            border-bottom: none;
            text-align: center;
        }

        .wrapper {
            width: 100%;
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



        #addPress {
            margin-bottom: 20px;
            float: left;
        }

        .lblitemname {
            margin-top: 10px;
        }

        .bgimg {
            background-image: url(../../Images/u4_normal.png);
            background-repeat: no-repeat;
            padding: 10px 10px 10px 10px;
            background-color: #98cdfe;
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

        .col-md-12 {
            padding-right: 0px;
            padding-left: 0px;
        }

        .col-md-6 {
            padding-left: 0px;
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

        .content-wrapper {
            margin-left: 0px;
            padding-top: 0px;
            margin-top: 0px;
        }

        .fixed .content-wrapper, .fixed .right-side {
            padding-top: 0px;
        }

        .RentHeader {
            font-size: 36px;
            line-height: 36px;
            font-family: Arial;
            padding-top: 10px;
            color: #ff3300;
            text-align: center;
        }

        .divRentHeader {
            float: left;
            text-align: center;
            background: #DFE3EE;
            margin-bottom: 20px;
        }

        .appointmentHeader {
            background-color: #3B5998;
            padding: 5px;
            font-size: 15px;
            color: white;
            width: 100%;
            text-align: center;
        }

        @media (min-width: 768px) {
            .col-md-10 {
                max-width: 85%;
            }

            .col-md-2 {
                max-width: 15%;
            }

                .col-md-2 .pull-left {
                    width: 100%;
                }
        }

        @media (max-width: 420px) {
            .col-md-10 {
                max-width: 100%;
            }

            .col-md-2 {
                max-width: 100%;
            }

                .col-md-2 .pull-left {
                    width: 250px;
                }
        }
    </style>
    <style type="text/css">
        .rent_footer {
            list-style-type: none;
            text-align: center;
            margin-bottom: 0px;
            padding-bottom: 20px;
        }

            .rent_footer li {
                display: inline;
                margin: 10px 25px;
            }
        /* The Modal (background) */
        .modal {
            display: none;
            position: fixed;
            z-index: 10000;
            padding-top: 0px;
            left: 0;
            top: 0;
            width: 100% !important;
            height: 100% !important;
            overflow: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.4);
            margin: 0px !important;
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            margin: auto;
            padding: 0;
            width: 45%;
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.4s;
            top: 10%;
        }

        /* Add Animation */


        /* The Close Button */
        .close {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }

        .modal-header {
            /*padding: 2px 16px;*/
            background-color: #5cb85c;
            color: white;
        }

        .modal-body {
            padding: 2px 16px;
            min-height: 275px !important;
            overflow: hidden;
            background: #F7F7F7;
        }

        .modal-footer {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }
    </style>
</head>
<body class="hold-transition skin-blue sidebar-mini fixed">
    <!-- Site wrapper -->
    <div class="wrapper">
        <!-- =============================================== -->
        <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper">
            <!-- Main content -->
            <section class="content">
                <form id="form1" runat="server" class="form-horizontal">
                    <div class="box">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="col-md-12" style="float: left;margin: 10px 0; text-align: center;">
                                         <h3>World Best Property & Facilities Management Software</h3>
                                    </div>
                                    <div class="col-md-12" style="float: left;margin: 20px 0;">
                                        <div class="col-md-2">
                                        <img alt="" src="https://www.eproperty365.net/Images/logo.png" class="img img-responsive pull-left" />
                                         </div>
                                        <div class="col-md-10 adBoxHeader ad-code-container">
                                            <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70109"></script>
                                        </div>
                                    </div>
                                    <div class="box-header with-border CommonHeader col-md-12" style="height: 10px; margin-bottom: 10px;">
                                        <h3 class="box-title">&nbsp;</h3>
                                    </div>
                                     <div class="col-md-12" style="margin-bottom: 10px; float: left;">
                                         <div class="col-md-3" style="float: left;">
                                              <h3 class="box-title">&nbsp;</h3>
                                             </div>
                                         <div class="col-md-4" style="float: left;" id="RentimageDiv">
                                            <%--<img alt="" src="https://www.eproperty365.net/Images/For_Rent_241_81.jpg" class="img img-responsive" />--%>
                                         </div>
                                        <div class="col-md-4"  style="float: left;">                                             
                                            <h6>MONTHLY RENTAL AMOUNT <span id="lblMonthlyRentAmount"></span></h6>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="float: left; min-height: 100px;" id="imageDiv">
                                    </div>
                                    <div class="col-md-12" style="float: left">
                                        <div class="col-md-8" style="float: left">
                                            <div class="col-md-12" style="float: left; margin-bottom: 10px;">
                                                <label  class="col-sm-4 control-label">Size (Rooms / Sq / Ft)</label>
                                                <div class="col-sm-8" style="float: right">
                                                    <label  class="col-sm-2 control-label">Location</label>
                                                    <label  class="col-sm-10 control-label" id="lblLocationName"> </label>
                                                </div>

                                            </div>
                                            <div class="col-md-12" style="float: left">
                                                <textarea rows="10" cols="100" id="LongDesc" class="form-control"></textarea>
                                            </div>
                                        </div>
                                        <div class="col-md-4" style="float: left; padding-top:30px;">
                                            <div class="col-md-12" style="float: left">
                                                <input type="button"  style="width: 100%; font-size: 15px;" value="View Location" id="btnViewLocation" class="btn btnNewColor" />
                                            </div>
                                            <div class="col-md-12" style="float: left; border: 1px solid; margin: 10px 0px 5px 0px; padding: 10px;">
                                                <div class="col-md-12" style="float: left;">
                                                      <label  class="col-sm-6 control-label">Broker Name:</label>
                                                <div class="col-sm-6" style="float: left">
                                                    <label class="col-sm-12 control-label" id="BrokerName"></label>
                                                </div>
                                                </div>
                                              <div class="col-md-12" style="float: left;">
                                                   <label  class="col-sm-6 control-label">Broker Phone:</label>
                                                <div class="col-sm-6" style="float: left">
                                                    <label  class="col-sm-12 control-label" id="BrokePhone"></label>
                                                </div>
                                                  </div>
                                   
                                                 <div class="col-md-12" style="float: left;">
                                                     <label class="col-sm-6 control-label"><%--Broker Email--%></label>
                                                <div class="col-sm-6" style="float: left">
                                                    <label class="col-sm-12 control-label" id="BrokerEmail"></label>
                                                </div>
                                                  </div>
                                    
                                                 <div class="col-md-12" style="float: left;">
                                                      <label  class="col-sm-6 control-label" style="margin-top: 7px">Agent Name:</label>
                                                    <div class="col-sm-6" style="float: left">
                                                        <label class="col-sm-12 control-label" style="margin-top: 7px" id="AgentName"></label>
                                                    </div>
                                                  </div>
                                   
                                                 <div class="col-md-12" style="float: left;">
                                                      <label  class="col-sm-6 control-label">Agent Phone:</label>
                                                <div class="col-sm-6" style="float: left">
                                                    <label  class="col-sm-12 control-label" id="AgentPhone"></label>
                                                </div>
                                                  </div>
                                   
                                                 <div class="col-md-12" style="float: left;">
                                                      <label  class="col-sm-6 control-label"><%--Agent Email--%></label>
                                                <div class="col-sm-6" style="float: left">
                                                    <label  class="col-sm-12 control-label" id="AgentEmail"></label>
                                                </div>
                                                  </div>
                                   
                                            </div>
                                            <div class="col-md-12" style="float: left">
                                                <input type="button" value="Apply" style="width: 100%; font-size: 15px;" id="btnApply" class="btn btn-successNew" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="float: left; margin-top: 10px;">
                                        <div class="col-md-12" style="float: left; background-color: #DFE3EE; padding-left: 0px; padding-right: 0px;">
                                            <div class="box-header with-border CommonHeader col-md-12" style="padding: 4px; font-size: 10px; float: left; margin-top: 0px;">
                                                <h6 class="box-title" id="lblHeadline" runat="server">Residential Quick Features View</h6>
                                            </div>
                                            <div class="col-md-12" style="float: left; padding: 0px 0px 10px 0px;" id="FeatureList">
                                                <table id="FeatureName">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="float: left; margin-top: 10px;">
                                        <div class="col-md-6" style="background-color: #DFE3EE; padding-right: 0px; float: left;">
                                            <div class="col-md-12 appointmentHeader" style="">Schedule to see the property</div>
                                            <label  class="col-sm-12 control-label" style="margin-top: 20px; margin-bottom: 10px;">Enter the best day and time.</label>
                                            <div class="col-md-12">
                                                <label for="txtDate" class="col-sm-2 control-label" style="line-height: 34px;">Date</label>
                                                <input type="text" id="txtDate" class="col-sm-4 form-control mainDate" style="float: left" />
                                                <div class="bootstrap-timepicker col-sm-6" style="float: right">
                                                    <div class="form-group" style="float: left">
                                                        <div class="input-group indexpicker">
                                                              <input id="timepicker1" class="form-actions" style="border: none;padding: 5px;margin-top: 2px;float: left;margin-left: 10px;border-radius: 4px;" type="text" name="timepicker1"/>
                                                              <div class="input-group-addon" style=" position: absolute;float: right;left: 260px;right: 0;background: none;top: 9px;width: 10px">
                                                                    <i class="fa fa-clock-o" style="float: right"></i>
                                                              </div>
                                                        </div>
                                                        <!-- /.input group -->
                                                    </div>
                                       
                                                    <!-- /.form group -->
                                                </div>

                                            </div>
                                            <div class="col-md-12" style="float: left;">
                                                <label class="col-sm-5 control-label" style="margin-right: 10px;line-height: 34px;">Enter Cell Phone Number:</label>
                                                <input  type="text" id="txtPhone" class="col-sm-6 form-control" />
                                            </div>
                                            <div class="col-md-12" style="float: left; margin-top: 10px;margin-bottom: 10px">
                                                 <label class="col-sm-12 control-label"> You will receive text from the agents to confirm or set up your appointment</label>
                                            </div>
                                            <div class="col-md-12" style="float: left; text-align: center;">
                                                <input type="button" style="font-weight: bold;" value="Submit" id="btnSubmit" class="btn btnNewColor" />
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding-left: 20px; ">
                                            <%--<img src="../../Images/u87_normal.png" style="width: 100%;"/>--%>
                                            <script language="javascript" type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70110"></script>
                                        </div>
                                    </div>
                                    
                                    
                                    <div id="myModalForRegistraton" class="modal" data-backdrop="false">

           

        </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </form>
            </section>
            <!-- /.content -->
            <footer>                
                <ul class="rent_footer">
                    <li>
                        <a href="/">
                            <img src="https://www.eproperty365.net/Images/logo.png" width="100" class="img img-responsive" alt="Logo"/>
                        </a>
                    </li>
                    <li><a target="_blank" href="https://www.eproperty365.com/aboutus/">About Us</a></li>
                    <li><a target="_blank" href="https://www.eproperty365.com/elements/propertyowner/">How It Works</a></li>
                    <li><a target="_blank" href="https://www.eproperty365.com/">Get Started</a></li>
                    <li><a target="_blank" href="https://www.eproperty365.com/elements/termsandconditions/">Terms & Conditions</a></li>
                    <li><a target="_blank" href="https://www.eproperty365.com/elements/privacypolicy/">Privacy</a></li>
                    <li><a target="_blank" href="https://www.eproperty365.com/contact/">Contact</a></li>
                    <li><a target="_blank" href="https://www.eproperty365.com/">Eproperty365</a></li>
                </ul>
            </footer>
        </div>
        <!-- /.content-wrapper -->
    </div>

    <script type="text/javascript" src="../../scripts/jquery-3.1.1.js"></script>
    <script type="text/javascript" src="../../scripts/jquery-ui-1.12.1.min.js"></script>

    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>

    <script type="text/javascript" src="../../scripts/bootstrap.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="../../scripts/fastclick.js"></script>
    <script type="text/javascript" src="../../AdminLTE/js/adminlte.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.validate.unobtrusive.min.js"></script>
    <script type="text/javascript" src="../../scripts/notification/bootstrap-growl.min.js"></script>
    <script type="text/javascript" src="../../scripts/notification/notification.js"></script>
    <script type="text/javascript" src="../../scripts/select2/dist/js/select2.min.js"></script>
    <script type="text/javascript" src="../../AdminLTE/iCheck/icheck.js"></script>
    <script type='text/javascript' src="../../AppJs/AdditionalJsFile/timepicki.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $('.sidebar-menu').tree();
            $('.active').closest('li.treeview').addClass('active');
        })
    </script>
    <script type="text/javascript">
        $('#timepicker1').timepicki();

        $(".mainDate").datepicker({
            dateFormat: "mm-dd-yy",
            changeYear: true,
            changeMonth: true
        });
        var currentPagePath = window.location.pathname + "/";
        $(document).ready(function (parameters) {
            LoadWebImage();
            LoadResidentialQuickFeaturesView();
            LoadRelationalData();
            LoadRentData();
        });

        function LoadWebImage() {
            var pagePath = currentPagePath + "GetAllIamge";
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Error");
                    },
                success:
                    function (result) {

                        var lstofCountry = $.parseJSON(decodeURIComponent(result.d));
                        var content = "", c = 1;
                        $.each(lstofCountry, function (i, obj) {

                            if (c == 1) {
                                content += '<div class="col-md-12" style="float: left">';
                            }

                            content += '<div class="col-md-3" style="float:left;margin-bottom: 10px;">' +
                                '<div style="float: left;padding: 3px;margin-bottom: 8px;width:100%;">' +
                                '<img style="width: 100%;float: left;" src="' + obj.ImagePath + '"/>' +
                                '</div>' +
                                '<div  style="text-align: center">' +
                                '<label>' + obj.ShortDescription + '</label>' +
                                '</div>' +
                                '</div>';
                            if (c == 4) {
                                content += '</div>';
                                c = 0;
                            }
                            c++;


                        });
                        content += '</div>';
                        $("#imageDiv").empty();
                        $("#imageDiv").append(content);
                    }
            });
        }
        function LoadResidentialQuickFeaturesView() {
            var pagePath = window.location.pathname + "/GetResidentialQuickFeaturesView";
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {

                        notify('danger', "Error");
                    },
                success:
                    function (result) {

                        var lstofFeatureName = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (lstofFeatureName != null) {
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

                                //content += "<tr style='float:left;width: 25%;'>";
                                //content += "<td><span class='col-sm-12' form-check-inline'><input id='" + obj.Id + "' disabled name='chkFeatureName' class='chkf' type='checkbox'>" +
                                //    "<label>" + obj.FeatureName + "</label></span></td>";
                                //content += "</tr>";
                            });
                            $("#FeatureName tbody").empty();
                            $("#FeatureName tbody").append(content);
                        }

                    }
            });
            // }
        }
        function LoadRelationalData() {
            var pagePath = window.location.pathname + "/GetOtherData";
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Error");
                    },
                success:
                    function (result) {

                        var lstofFeatureName = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (lstofFeatureName != null) {

                            $("#BrokerName").text(lstofFeatureName.UnitBrokerName);
                            $("#BrokePhone").text(lstofFeatureName.UnitBrokerPhone);
                            $("#BrokerEmail").text("");
                            $("#AgentName").text(lstofFeatureName.UnitAgentName);
                            $("#AgentPhone").text(lstofFeatureName.UnitAgentPhone);
                            $("#AgentEmail").text("");
                            $("#lblLocationName").text(lstofFeatureName.UnitSpecialStatements);
                            $("#lblMonthlyRentAmount").text("$" + lstofFeatureName.UnitMonthlyRent);


                        }

                    }
            });

        }
        function LoadRentData() {
            var pagePath = window.location.pathname + "/GetRentData";
            var obj = {};
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        notify('danger', "Error");
                    },
                success:
                    function (result) {

                        var lstofFeatureName = $.parseJSON(decodeURIComponent(result.d));
                        var content = "";
                        if (lstofFeatureName != null) {
                            if (lstofFeatureName == "2")
                            {
                                content = '<img class="img img-responsive" alt="" src="https://www.eproperty365.net/Images/Rented_241_81.jpg"/>'; 
                                $("#btnApply").attr('disabled', true);
                            }
                            else
                            {
                                content = '<img class="img img-responsive" alt="" src="https://www.eproperty365.net/Images/For_Rent_241_81.jpg"/>';
                                $("#btnApply").attr('disabled', false);
                            }

                            $("#RentimageDiv").empty();
                            $("#RentimageDiv").append(content);

                        }

                    }
            });

        }

        var currentPagePath = window.location.pathname + "/";
        $("#btnSubmit").click(function (parameters) {
            var pagePath = currentPagePath + "Save";
            var obj = {
                "Showing_Phone_No": $("#txtPhone").val(),
                "Showing_Date": $("#txtDate").val(),
                "Showing_Time": $('#timepicker1').val()
            };
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ 'obj': obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error"); //../../Uploads/Images/38860545_2093714167366803_1085132197528076288_n.jpg
                    },
                success:
                    function (result) {

                        var res = $.parseJSON(decodeURIComponent(result.d));

                        if (res == true) {
                            notify('success', "You Application submitted successfully.You will receive text from the agents to confirm or set up your appointment ");
                            $("#btnSubmit").attr('disabled', true);
                        } else {
                            notify('danger', "You Application submitted Failed");
                        }

                    }
            });
        });



        $("#btnApply").click(function (parameters) {
            var pagePath = currentPagePath + "apply";
            var obj = {
                "Showing_Phone_No": $("#txtPhone").val(),
                "Showing_Date": $("#txtDate").val(),
                "Showing_Time": $('#timepicker1').val()
            };
            $.ajax({
                type: "POST",
                url: pagePath,
                data: JSON.stringify({ 'obj': obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Error");
                        notify('danger', "Error"); //../../Uploads/Images/38860545_2093714167366803_1085132197528076288_n.jpg
                    },
                success:
                    function (result) {

                        var res = $.parseJSON(decodeURIComponent(result.d));
                        if (res != null) {
                            if (res.isFirstSignIn == true) {
                                var origin = window.location.origin; // Returns base URL
                                var url = origin + "/Pages/Resident/ResidentialTentAddResponceStep1.aspx?ResidentialUnitSerial=" + res.ResidentialUnitSerialId + '&&TenentId=' + res.Serial;
                                window.location.href = url;
                            }
                        }
                    }
            });

        });


    </script>
    <style type="text/css">
        .indexpicker .time_pick {
            width: 270px;
            display: inline-block;
        }

            .indexpicker .time_pick > input {
                width: 100%;
                margin-bottom: 20px;
            }
    </style>

</body>
</html>

