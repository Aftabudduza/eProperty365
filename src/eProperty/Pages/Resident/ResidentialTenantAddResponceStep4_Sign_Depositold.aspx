<%@ Page Title="EProperty365: Residential Tenant Step 4 Sign & Pay Deposit" Language="C#" AutoEventWireup="true" CodeBehind="ResidentialTenantAddResponceStep4_Sign_Depositold.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialTenantAddResponceStep4_Sign_Depositold" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EProperty365: Residential Tenant Step 4 Sign & Pay Deposit</title>
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

    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
    <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- Google Font -->

    <%--<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">--%>

    <!-- Web Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,400,700,300&amp;subset=latin,latin-ext' rel='stylesheet' type='text/css'>
    <link href='https://fonts.googleapis.com/css?family=PT+Serif' rel='stylesheet' type='text/css'>
    <link type="text/css" href="../../scripts/notification/notification.css" rel="stylesheet" />
    <link href="../../scripts/select2/dist/css/select2-bootstrap.css" rel="stylesheet" />
    <link href="../../scripts/select2/dist/css/select2.css" rel="stylesheet" />
    <link type="text/css" href="../../AdminLTE/iCheck/all.css" rel="stylesheet" />
    <link href="../../AdminLTE/iCheck/flat/_all.css" rel="stylesheet" />
    <link href="../../css/Site.css" rel="stylesheet" />
    <%-- <link href="../../css/credit.css" rel="stylesheet" />--%>
    <%--<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>
    <script type="text/javascript" src="../../scripts/jquery-3.1.1.js"></script>
    <script type="text/javascript" src="../../scripts/jquery-ui-1.12.1.min.js"></script>
    <script type="text/javascript" src="../../scripts/bootstrap.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="../../scripts/fastclick.js"></script>
    <script type="text/javascript" src="../../AdminLTE/js/adminlte.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.validate.unobtrusive.min.js"></script>
    <script type="text/javascript" src="../../scripts/notification/bootstrap-growl.min.js"></script>
    <script type="text/javascript" src="../../scripts/notification/notification.js"></script>
    <%--    <script type="text/javascript" src="https://www.eproperty365.net/scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://www.eproperty365.net/scripts/bootstrap.js"></script>
    <script type="text/javascript" src="https://www.eproperty365.net/scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="https://www.eproperty365.net/scripts/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="https://www.eproperty365.net/scripts/fastclick.js"></script>
    <script type="text/javascript" src="https://www.eproperty365.net/AdminLTE/js/adminlte.js"></script>
    <script src="https://www.eproperty365.net/Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <script src="https://www.eproperty365.net/Scripts/jquery.validate.unobtrusive.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>--%>
    <script type="text/javascript" src="../../scripts/select2/dist/js/select2.min.js"></script>
    <script type="text/javascript" src="../../AdminLTE/iCheck/icheck.js"></script>
    <script type="text/javascript" src="../../Content/js/pdfobject.js"></script>
    <script src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script src="../../AppJs/TenantAllJs/ResidentialTenantAddResponceStep4_Sign_Deposit.js"></script>
    <script type="text/javascript" src="https://sandbox.forte.net/checkout/v1/js"></script>
    <%--<script type="text/javascript" src="https://checkout.forte.net/v1/js"></script>--%>

    <script src="../../AppJs/credit.js"></script>

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
            width: 90%;
            margin-left: 5%;
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

        .content {
            padding-left: 0px;
            padding-right: 0px;
            padding-bottom: 0px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 1024px;
            height: 1050px;
        }

        .auto-style2 {
            background-color: #FFFFCC;
            border: none;
        }

        /*.auto-style4 {
            width: 60%;
            border: none;
        }*/

        .auto-style5 {
            font-size: medium;
            border: none;
        }

        .auto-style6 {
            font-size: medium;
            font-weight: bold;
            border: none;
        }

        .auto-style7 {
            text-align: center;
            border: none;
        }

        .auto-style8 {
            width: 135px;
            border: none;
        }

        .auto-style9 {
            width: 82px;
            border: none;
        }

        .auto-style10 {
            width: 118px;
            border: none;
        }

        .auto-style16 {
            width: 116px;
            border: none;
        }

        .auto-style17 {
            width: 40%;
            border: none;
            float: left;
        }

        .auto-style18 {
            width: 209px;
            border: none;
        }

        .auto-style19 {
            width: 101px;
        }

        .auto-style21 {
            width: 186px;
            border: none;
        }

        .auto-style22 {
            width: 161px;
            border: none;
        }

        .auto-style24 {
            width: 100%;
            border: none;
        }

        .auto-style25 {
            width: 119px;
            height: 23px;
            border: none;
        }

        .auto-style31 {
            width: 131px;
            border: none;
        }

        .auto-style34 {
            width: 130px;
            border: none;
        }

        .auto-style35 {
            width: 23%;
            height: 23px;
            border-style: none;
            vertical-align: top;
            float: left;
        }

        .auto-style36 {
            width: 22%;
            height: 23px;
            vertical-align: top;
            border-style: none;
            float: left;
        }

        .auto-style37 {
            width: 25%;
            height: 23px;
            vertical-align: top;
            border-style: none;
            float: left;
        }

        .auto-style39 {
            width: 134px;
            border-style: none;
            vertical-align: top;
        }

        .auto-style40 {
            width: 133px;
            border: none;
        }

        .auto-style45 {
            color: #FF0000;
            border-left-color: #C0C0C0;
            border-left-width: medium;
            border-right-color: #A0A0A0;
            border-right-width: medium;
            border-top-color: #C0C0C0;
            border-top-width: medium;
            border-bottom-color: #A0A0A0;
            border-bottom-width: medium;
            padding: 1px;
            background-color: #FFFFCC;
        }

        .auto-style46 {
            height: 23px;
            width: 23%;
            vertical-align: top;
            border-style: none;
            float: left;
        }

        .auto-style47 {
            width: 100%;
            vertical-align: top;
            border-style: none;
        }

        .auto-style48 {
            height: 23px;
            vertical-align: top;
            border-style: none;
        }

        .auto-style49 {
            width: 100%;
            vertical-align: top;
            border-style: none;
        }

        .auto-style50 {
            width: 100%;
        }

        .auto-style51 {
            width: 70px;
        }

        .auto-style52 {
            width: 159px;
        }

        .auto-style53 {
            width: 49px;
        }

        .auto-style54 {
            width: 149px;
        }

        .auto-style55 {
            width: 104px;
        }

        .auto-style56 {
            width: 319px;
        }
    </style>
</head>
<body>
    <div>
        <div style="width: 100%">
            <table style="width: 1024px; height: 30px; margin: 0 auto; background-color: #F7F7F7;" border="1">
                <tbody>
                    <tr>
                        <td>
                            <table style="width: 100%" border="0">
                                <tbody style="border-bottom: 21px solid #3B5998;">
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <h6>World Best Property & Facilities Management Software</h6>
                                        </td>
                                    </tr>
                                    <tr style="margin-bottom: 10px;">
                                        <td style="width: 178px;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="https://www.eproperty365.net/images/E-property-Logo-A_150_71_96.png" />
                                            <br />
                                        </td>
                                        <td colspan="3">
                                            <div class="adBoxHeader ad-code-container">
                                                <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70109"></script>
                                            </div>
                                        </td>

                                    </tr>

                                </tbody>
                            </table>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 62%; border-right: 1px solid #000000;" class="auto-style4">
                                            <form id="form1" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table border="0" class="" style="z-index: 100; float: left; width: 100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="" style="text-align: center">
                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="https://www.eproperty365.net/images/App_chk1.png" />
                                                                            </td>
                                                                            <td class="" style="text-align: center">
                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="https://www.eproperty365.net/images/App_chk2.png" />
                                                                            </td>
                                                                            <td class="" style="text-align: center">
                                                                                <asp:Image ID="Image4" runat="server" ImageUrl="https://www.eproperty365.net/images/App_chk3.png" />
                                                                            </td>
                                                                            <td class="" style="text-align: center">
                                                                                <asp:Image ID="Image5" runat="server" ImageUrl="https://www.eproperty365.net/images/App_chk4.png" />
                                                                            </td>

                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <table class="" style="width: 100%; z-index: 10000; margin-top: -39px; margin-bottom: 20px; float: left">
                                                                    <tr>
                                                                        <td class="" style="text-align: center">
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" BorderStyle="None" />
                                                                        </td>
                                                                        <td class="" style="text-align: center">
                                                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                        </td>
                                                                        <td class="" style="text-align: center">
                                                                            <asp:CheckBox ID="CheckBox3" runat="server" />
                                                                        </td>
                                                                        <td class="" style="text-align: center; margin-top: 10px;">
                                                                            <asp:CheckBox ID="CheckBox4" runat="server" />
                                                                        </td>

                                                                    </tr>
                                                                </table>

                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table style="width: 100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td><span style="width: 12%; float: left; margin-left: 5%;">User Name :</span><span style="float: left; width: 36%;" id="userName"></span></td>
                                                        </tr>
                                                        <tr>

                                                            <td style="width: 100%; text-align: center" class="auto-style5">
                                                                <span class="auto-style6">Step 4:</span>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #3B5998; text-align: center; color: white; line-height: 25px; font-family: Arial; font-weight: bold; font-size: 13px; font-style: normal;">
                                                                <span>Setup Tenant  Dashboard & Sign Documents</span>
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>

                                                <table style="width: 100%; background: #F7F7F7" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <div style="margin: 5px 20px; width: 93%; float: left;">
                                                                    Applicaiton Id :- <span id="ApplicationId"></span>
                                                                    <div style="width: 100%; background-color: #F7F7F7; margin-top: 20px; border: 1px solid black; float: left;">
                                                                        <div class="col-md-12" style="float: left; margin-top: 0px; padding: 4px !important; text-align: center; font-weight: bold; font-size: 14px;">
                                                                            <span>Documents</span>
                                                                        </div>
                                                                        <div style="float: left; width: 100%; padding: 0px 10px;">
                                                                            <ol style="font-size: 13px; font-weight: bold;">
                                                                                <li>Click on each documents and look at the status to tell you what to do.</li>
                                                                                <li>If the document status is "download"  you must download the document and filled them out and then upload them. All you need to do is click on the document checkbox and then press the "Download" button.  Your selected document will be downloaded to your download sub-directory. The file will be in either a Word, Text, PDF format.  Fill out information and save the file in the original format you receive it. You may also take a picture of each page of the document (be sure you can read it) and save each page in jpg format. Press the Browse Button to select the files on your computer to upload. Then press the "Upload" Button.</li>
                                                                                <li>All document statuses must say either "Uploaded" or " Viewed" when your finished and you must have type your name and put last 4 digits of your Social Security number and date it. EVEN IF YOU SIGNED THE ACTUAL FORM to complete this section.</li>

                                                                            </ol>
                                                                            <span style="width: 100%; float: left; font-weight: bold; font-size: 13px;">ALL PARTIES MUST SIGN THE DOCUMENT BELOW.</span>

                                                                            <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblDoc">
                                                                                <thead style="font-size: 13px; font-weight: bold">
                                                                                    <tr>
                                                                                        <th></th>
                                                                                        <th>Documents</th>
                                                                                        <th>Status</th>
                                                                                        <th>Action</th>
                                                                                        <th>Current Status</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                </tbody>

                                                                            </table>
                                                                            <div style="width: 100%">
                                                                                <span>
                                                                                    <input type="file" disabled="disabled" id="documentUpload" /></span>
                                                                                <span>
                                                                                    <input id="savedoc" type="button" disabled="disabled" class="btn btnNewColor" value="Upload" onclick="SavedocumentUpload();" /></span>
                                                                            </div>
                                                                            <%-- <div id="iframedis" class="col-md-12">
                                                                                <object id="objid" data="" type="application/pdf">
                                                                                    <iframe src="" id="viewfile" style="width: 618px; height: 800px"></iframe>
                                                                                </object>

                                                                            </div>--%>
                                                                            <div id="iframedis" class="col-md-12" style="display: none">
                                                                                <object id="objid" data="" type="application/pdf">
                                                                                    <iframe src="" id="viewfile" style="width: 618px; height: 800px"></iframe>
                                                                                </object>
                                                                            </div>
                                                                            <div id="iframeimage" class="col-md-12" style="display: none">
                                                                                <img src="" id="ifrmImage" width="400" height="400" />
                                                                            </div>
                                                                            <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblSignature">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>Signature</th>
                                                                                        <th>Security No</th>
                                                                                        <th>Date</th>
                                                                                        <th>Edit</th>
                                                                                        <th>Delete</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                </tbody>

                                                                            </table>
                                                                            <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px; margin-top: 20px;" cellspacing="2" cellpadding="5" border="1">

                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <input placeholder="Signature" type="text" id="txtSign" /><input type="hidden" id="hdId" value="0" /></td>
                                                                                        <td>
                                                                                            <input type="text" placeholder="Last 4 Social security No." id="txtSecurity" /></td>
                                                                                        <td>
                                                                                            <input type="text" placeholder="Date" id="txtDate" class="tDate" /></td>

                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <span style="float: left; margin-top: 10px; text-align: center; width: 100%">
                                                                                <input type="button" class="btn btnNewColor" style="background-color: #3B5998" value="Tenant Submit" id="btnTenantSubmit" /></span>
                                                                            <span style="float: left; margin-top: 10px; font-size: 13px; font-weight: bold; color: red; text-align: center; width: 100%;">All Documents Must Be Viewed or Uploaded and signed!</span>

                                                                            <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblOwnerInfo">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>Owner Signature</th>
                                                                                        <th>Security Number</th>
                                                                                        <th>Date</th>
                                                                                        <th>Edit</th>
                                                                                        <th>Delete</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                </tbody>

                                                                            </table>
                                                                            <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px; margin-top: 20px;" cellspacing="2" cellpadding="5" border="1">

                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <input type="text" placeholder="Owner Signature" id="txtOwnerSig" /><input type="hidden" id="hdOwnerId" value="0" /></td>
                                                                                        <td>
                                                                                            <input type="text" placeholder="Last 4 Social security No." id="txtOwnerSecurity" /></td>
                                                                                        <td>
                                                                                            <input type="text" placeholder="Date" id="txtOwnerDate" class="tDate" /></td>
                                                                                    </tr>

                                                                                </tbody>
                                                                            </table>
                                                                            <span style="float: left; margin-top: 10px; text-align: center; width: 100%;">
                                                                                <input type="button" class="btn btnNewColor" style="background-color: #3B5998" value="Owner Submit" id="btnSaveOwner" /></span>
                                                                            <span style="float: left; margin-top: 10px; font-weight: bold; text-align: justify;">A copy of all the signed rental documents will be placed in your tenant portal via your MyFileit virtual safety deposit box account after the Owner / Landlord signs the documents with directions of how to access and view it.</span>
                                                                        </div>
                                                                    </div>
                                                                    <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; text-align: center;">
                                                                        <div class="col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                                                                            <h3 class="" id="H5" style="font-size: 14px;">Amount Due At Signing</h3>
                                                                        </div>
                                                                        <div style="float: left; width: 100%; padding: 0px 10px;">
                                                                            <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px; margin-top: 20px;" cellspacing="2" cellpadding="5" border="1">

                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>Security Deposit:</td>
                                                                                        <td>
                                                                                            <input type="text" id="txtSecurityDeposite" /></td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>One Months Rent:</td>
                                                                                        <td>
                                                                                            <input type="text" id="txtOneMonthRent" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Prorate Amount  </td>
                                                                                        <td>
                                                                                            <input type="text" id="txtProrateAmount" /></td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td>First Months Rent:  </td>
                                                                                        <td>
                                                                                            <input type="text" id="txtFirtMonthRent" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Total Due:   </td>
                                                                                        <td>
                                                                                            <input type="text" id="txtTotalDue" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Monthly Payment Due Date:   </td>
                                                                                        <td>
                                                                                            <%--<input type="text" id="txtDueDate" />--%>
                                                                                            <select id="txtDueDate" style="width: 55%" class="ddl">
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>

                                                                    </div>
                                                                    <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; text-align: center;">
                                                                        <table cellspacing="3" cellpadding="5" style="width: 100%; float: left; background-color: #DFE3EE">
                                                                            <tbody style="padding: 5px;">
                                                                                <tr>
                                                                                    <td colspan="3">
                                                                                        <div class="col-md-12" style="padding-left: 3px; float: left; text-align: center;">
                                                                                            <div class="form-group">
                                                                                                <label style="margin-right: 7px">
                                                                                                    <input type="radio" name="CashOrCheck" value="Check" id="Checking" checked="checked" class="flat-red" />
                                                                                                    Checking Account
                                                                                                </label>
                                                                                                <label style="margin-right: 7px">
                                                                                                    <input type="radio" name="CashOrCheck" value="Cash" id="Cash" class="flat-red" />
                                                                                                    Cash
                                                                                                </label>


                                                                                            </div>
                                                                                        </div>
                                                                                    </td>

                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table cellspacing="3" cellpadding="5" style="width: 100%; float: left; background-color: #DFE3EE">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td colspan="3">Billing Information:
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="" colspan="3">
                                                                                        <span style="margin-right: 7%;">*Account Name:</span>
                                                                                        <input type="text" id="nameAccountapp1Txt" class="nameAccountapp1Txt" style="width: 75%" />


                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="" colspan="3">
                                                                                        <span style="margin-right: 13%">*Address1:</span>
                                                                                        <input type="text" id="addressapp1Txt1" style="width: 75%" />


                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="" colspan="3">
                                                                                        <span style="margin-right: 13%">Address2:</span>
                                                                                        <input type="text" id="addressapp1Txt2" style="width: 75%" />


                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="">
                                                                                        <span style="margin-right: 5%">*Country:</span>
                                                                                        <select id="ddlCountry" style="width: 60%" class="ddl country"></select>

                                                                                    </td>
                                                                                    <td class="" colspan="2">
                                                                                        <span style="margin-right: 10%">*Region:</span>
                                                                                        <%--<select id="ddlRegion" style="width: 75%" class="ddl"></select>--%>
                                                                                        <input type="text" id="ddlRegion" style="width: 60%" />

                                                                                    </td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span style="margin-right: 10%">*City:</span>
                                                                                        <input type="text" id="cityapp1Txt" style="width: 70%" class="city" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <span style="margin-right: 3%">*State:</span>
                                                                                        <select id="ddlstateapp" style="width: 68%" class="ddl state"></select>

                                                                                    </td>
                                                                                    <td style="">
                                                                                        <span style="margin-right: 1%">*Zip code:</span>
                                                                                        <input type="text" id="zipcodeapp1Txt" style="width: 60%" />
                                                                                    </td>

                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table cellspacing="3" cellpadding="5" style="width: 100%; float: left; background-color: #DFE3EE" id="tblChecking">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td colspan="3">For Checking Account Use Only::</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style56" colspan="2">*Routing number (2nd # from bottom left):</td>
                                                                                    <td>
                                                                                        <input type="text" id="routingnumapp1Txt" style="width: 254px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style56" colspan="2">Re-Enter Routing number:</td>
                                                                                    <td>
                                                                                        <input type="text" id="rerountingnumapp1Txt" style="width: 252px" />

                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style56" colspan="2">* Account number (last # from bottom left):</td>
                                                                                    <td style="">
                                                                                        <input type="text" id="checkacctnumapp1Txt" style="width: 250px" />


                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style56" colspan="2">Re-Enter Account number:</td>
                                                                                    <td>
                                                                                        <input type="text" id="recheckacctnumapp1Txt" style="width: 248px" />

                                                                                    </td>
                                                                                </tr>

                                                                            </tbody>
                                                                        </table>
                                                                        <div style="float: left; margin-top: 10px; text-align: justify; padding: 10px;" id="tblCheckingDiv">
                                                                            <span style="float: left">By clicking on the Submit Payment button I authorize the Landlord or there agents to debit my Checking account for the below amount.</span>
                                                                            <br />
                                                                            <span style="float: left; margin-top: 20px;">
                                                                                <input type="checkbox" id="RentalAmount" />Please automatically debit the above rental amount on the 1st day of each month for  the monthly rental payment plus any processing fee.. </span>
                                                                            <div style="float: left; width: 100%">
                                                                                <span>Sub-Total Charge:$</span>
                                                                                <span id="subTotalCharge"></span>
                                                                            </div>
                                                                            <div style="float: left; width: 100%">
                                                                                <span>Checking Account Processing fee <span id="percentRatio"></span>:$</span>
                                                                                <span id="Amount"></span>
                                                                            </div>
                                                                            <div style="float: left; width: 100%">
                                                                                <span>Total Amount your Account will be Charged:$</span>
                                                                                <span id="TotalAmountCharge"></span>
                                                                            </div>
                                                                            <%--<div style="float: left; width: 100%; text-align: center; margin-top: 20px; margin-bottom: 20px;">
                                                                                <input type="button" class="btn btnPayment" id="btnSubmit" style="background-color: #66FF00" value="Submit Payment & Continue" />
                                                                            </div>--%>
                                                                        </div>
                                                                    </div>
                                                                    <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; padding: 10px; display: none" id="tblCash">
                                                                        <div class="col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                                                                            <h3 class="" id="H15" style="font-size: 14px;">Cash Receipt :</h3>
                                                                        </div>
                                                                        <div style="float: left; margin-top: 10px;">
                                                                            <p>
                                                                                I
                                                                                <input type="text" placeholder="Persone Name" id="txtPersoneName" />
                                                                                <input type="text" placeholder="Last 4 Your credit card." id="PersonLastCreitCard" />
                                                                                <input type="text" id="txtCompanyName" placeholder="Company Name" />
                                                                                as authorized agent or Landlord received in cash the amount of 
                                                                                <input type="text" id="txtAmountOff" placeholder="" />
                                                                                on
                                                                                <input type="text" placeholder="date" class="tDate" id="txtdateFrom" />
                                                                                at 
                                                                                <input id="txtLocation" type="text" placeholder="Enter Location" />

                                                                            </p>
                                                                            <span>Signature :
                                                                                <input type="text" id="txtOwnerSignature" placeholder="Persone Name" />
                                                                                <input type="text" id="txtCreditCardLast4" placeholder="Last 4 Your credit card." /></span>
                                                                        </div>
                                                                        <div style="float: left; width: 100%; text-align: center; margin-top: 20px; margin-bottom: 20px;">
                                                                            <input type="hidden" id="hdnShow" value="<%=isView%>" />

                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </form>

                                            <table style="width: 100%; margin: 20px 0; float: left;">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 20%;">
                                                            <input type="button" class="btn btnNewColor" id="btnExit" style="background-color: #3B5998" value="Exit" /></td>
                                                        <td style="text-align: center; width: 30%;">
                                                            <input type="button" class="btn btnNewColor" id="btnCancel" style="background-color: #3B5998" value="Cancel" /></td>

                                                        <td style="width: 50%;">
                                                            <%--<input type="button" class="btn" style="background-color: #66FF00" value="Submit & Continue" id="btnSaveAndContinue" />--%>
                                                            <button id="btnSubmit" api_login_id="<%=api_loginID%>"
                                                                method="sale"
                                                                version_number="1.0"
                                                                utc_time="<%=utc_time%>"
                                                                account_number=""
                                                                routing_number=""
                                                                account_number2=""
                                                                account_type=""
                                                                billing_company_name=""
                                                                billing_name=""
                                                                billing_street_line1=""
                                                                billing_street_line2=""
                                                                billing_locality=""
                                                                billing_region=""
                                                                billing_postal_code=""
                                                                billing_phone_number=""
                                                                billing_email_address=""
                                                                signature="<%= pay_now_single_return_string%>"
                                                                total_amount_attr="edit"
                                                                total_amount="<%= sTotal%>"
                                                                callback="oncallback"
                                                                order_number="A1234"
                                                                button_text="Confirm Payment"
                                                                allowed_methods="echeck"
                                                                class="btnPaymentGetway">
                                                                Submit Payment & Continue</button>

                                                            <input type="button" class="btn btnPayment" style="background-color: #66FF00; display: none;" id="btnSubmitCash" value="Submit Payment & Continue" />

                                                        </td>
                                                    </tr>

                                                </tbody>
                                            </table>

                                        </td>
                                        <td style="vertical-align: top; width: 38%; float: left" class=" auto-style17">
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ul class="rent_footer">
                                <li>
                                    <a href="/">
                                        <img src="https://www.eproperty365.net/Images/logo.png" width="100" class="img img-responsive" alt="Logo" />
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
                        </td>
                    </tr>
                </tbody>
            </table>
            <p></p>
        </div>
    </div>

    <script type="text/javascript">
        var G_buttonAdd = '<svg width="40" height="40"><line x1="20" y1="10" x2="20" y2="20"/>' +
           '<line x1="30" y1="20" x2="20" y2="20"/><line x1="20" y1="30" x2="20" y2="20" /><line x1="10" y1="20" x2="20" y2="20" />' +
           '<circle cx="20" cy="20" r="18"/></svg>';
        var G_buttonRmv = '<svg width="40" height="40">' +
                      '<line x1="30" y1="20" x2="20" y2="20"/><line x1="10" y1="20" x2="20" y2="20" />' +
                      '<circle cx="20" cy="20" r="18"/></svg>';
        $(document).ready(function (parameters) {
            //LoadRentalDocumentGrid();
        });

    </script>

</body>
</html>


