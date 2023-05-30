<%@ Page Title="EProperty365: Residential Tenant Step 1 Application Fee" Language="C#" AutoEventWireup="true" CodeBehind="ResidentialTentAddResponceStep1.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialTentAddResponceStep1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <%--<link href="../../css/credit.css" rel="stylesheet" />--%>
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
    <script src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script src="../../AppJs/TenantAllJs/ResidentialAddResponceStep1.js"></script>
    <%--<script type="text/javascript" src="https://sandbox.forte.net/checkout/v1/js"></script>--%>
    <script type="text/javascript" src="https://checkout.forte.net/v1/js"></script>
    <%--<script src="../../AppJs/credit.js"></script>--%>

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
                                        <td style="width: 62%; float: left; border-right: 1px solid #000000;" class="auto-style4">
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
                                                            <td><span style="width: 15%; float: left; margin-left: 5%;">User Name :</span><span style="float: left; width: 35%;" id="userName"></span></td>
                                                        </tr>
                                                        <tr>

                                                            <td style="width: 100%; text-align: center" class="auto-style5">
                                                                <span class="auto-style6">Step 1:</span>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #3B5998; text-align: center; color: white; line-height: 25px; font-family: Arial; font-weight: bold; font-size: 13px; font-style: normal;">
                                                                <span>Tenant Rental Application Fee</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color: #333333; line-height: 25px; font-family: Arial; font-size: 13px; font-style: normal; padding: 5px 10px">
                                                                <span style="width: 100%; float: left; font-size: 13px; line-height: 16px;">There is non-refundable Application and background and credit screening fee based on the number of people applying. <b>There are no refunds if you are not approved.</b> We give you 40% discount after the 1st person. You may use this reports within 30 days to any properties managed by Eproperty365.</span>
                                                                <span style="width: 50%; float: left; margin-top: 10px">Number of People Signing the Agreement :  </span><span style="width: 20%; float: left; margin-top: 10px;">
                                                                    <select id="numberofPeople" style="width: 100%" class="ddl">
                                                                    </select></span>
                                                                <span style="width: 100%; float: left">
                                                                    <div class="row">
                                                                        <div class="col-md-6" style="padding-left: 0px">
                                                                            <label for="txtManCity" class="col-sm-12 control-label">Perform Tenant background screening?:</label>
                                                                        </div>
                                                                        <div class="col-md-3" style="padding-left: 0px;">
                                                                            <div class="form-group">
                                                                                <label style="margin-right: 7px">
                                                                                    <input type="radio" id="di" name="r3" class="flat-red" value="Yes" checked="checked" />
                                                                                    Yes
                                                                                </label>
                                                                                <label style="margin-right: 7px">
                                                                                    <input type="radio" name="r3" value="No" id="Dealer" class="flat-red" />
                                                                                    No
                                                                                </label>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </span>
                                                              
                                                                <span style="width: 50%; float: left">Total Application Fee: </span>
                                                                <span style="width: 25%; float: left">$<input style="width:75%;" type="text" disabled="disabled" data_appfee="" name="total_amount" id="TotalApp" value="" /></span>
                                                                <span style="width: 25%; color: red; float: left">* denote you must fill</span>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                                <table style="width: 100%; background: #F7F7F7" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <div style="margin: 5px 10px; width: 97%; float: left;">
                                                                    <div style="width: 100%; float: left; border: 1px solid black; background-color: #DFE3EE">


                                                                        <input type="hidden" id="pg_api_login_id" value="<%=api_loginID%>" />
                                                                        <input type="hidden" id="hdnShow" value="<%=isView%>" />
                                                                        <input type="hidden" id="pg_utc_time" value="<%=utc_time%>" />
                                                                        <input type="hidden" id="hdTotalAmount" runat="server" value="" />

                                                                        <input type="hidden" id="hdnApplicationFee" value="<%=sApplicationFee%>" />
                                                                        <input type="hidden" id="hdnScreeningFee" value="<%=sScreeningFee%>" />

                                                                          <input type="hidden" id="hdnScreenType" value="<%=sScreeningType%>" />

                                                                        <table cellspacing="3" cellpadding="5" style="margin: 0 auto">
                                                                            <tbody style="padding: 5px;">
                                                                                <tr>
                                                                                    <td class="" colspan="3">Enter your Credit Card or Checking Account Information.<input type="hidden" id="hdId" value="0" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3">
                                                                                        <div class="col-md-12" style="padding-left: 3px; float: left; text-align: center;">
                                                                                            <div class="form-group">
                                                                                                <label style="margin-right: 7px">
                                                                                                    <input type="radio" id="Credit" name="card" class="flat-red" value="Credit" checked="checked" />
                                                                                                    Credit Card
                                                                                                </label>
                                                                                                <label style="margin-right: 7px">
                                                                                                    <input type="radio" name="card" value="Cash" id="Cash" class="flat-red" />
                                                                                                    Cash
                                                                                                </label>
                                                                                                <label style="margin-right: 7px">
                                                                                                    <input type="radio" name="card" value="Check" id="Checking" class="flat-red" />
                                                                                                    Checking Account
                                                                                                </label>

                                                                                            </div>
                                                                                        </div>
                                                                                    </td>

                                                                                </tr>
                                                                            </tbody>
                                                                        </table>

                                                                        <table cellspacing="3" cellpadding="5" style="width: 100%">
                                                                            <tbody style="padding: 5px;">
                                                                                <tr>
                                                                                    <td style="" colspan="3">
                                                                                        <span style="margin-right: 5%; float: left">*Name on Account:</span>
                                                                                        <input type="text" id="nameAccountapp1Txt" class="nameAccountapp1Txt" style="width: 75%" />


                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="" colspan="3">
                                                                                        <span style="margin-right: 12%; float: left">*Address1:</span>
                                                                                        <input type="text" id="addressapp1Txt1" style="width: 75%" />


                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="width:30%;">
                                                                                        <span style="float: left">*City:</span>
                                                                                        <input type="text" id="cityapp1Txt" style="width:70%;" />


                                                                                    </td>
                                                                                    <td class="width:40%;">
                                                                                        <span style="float: left">*State:</span>
                                                                                        <select id="ddlstateapp" style="width:75%;" class="ddl"></select>
                                                                                    </td>


                                                                                    <td style="width:30%;">
                                                                                        <span style="float: left">*Zip code:</span>
                                                                                        <input type="text" id="zipcodeapp1Txt" style="width:60%;" />


                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>

                                                                        <table cellspacing="3" cellpadding="5" style="width: 100%" id="tblCredit">
                                                                            <tbody style="padding: 5px;">

                                                                                <tr>
                                                                                    <td style="">
                                                                                        <span>*Credit Card Number:</span>
                                                                                        <%--<input id="creditcardapp1Txt" type="text" pattern="[3-6][0-9 ]{15,18}" data-inputmask="'mask': '9999 9999 9999 9999'" style="width: 58%" />--%>
                                                                                        <input id="creditcardapp1Txt" type="tel" name="ccnumber" placeholder="XXXX XXXX XXXX XXXX" pattern="\d{4} \d{4} \d{4} \d{4}" class="masked" style="width: 80%" />

                                                                                    </td>


                                                                                    <td style="">
                                                                                        <span style="margin-right: 2%; float: left">*CVS Number:</span>
                                                                                        <input type="text" id="cvsNumber" style="width: 64%" />

                                                                                    </td>
                                                                                    <td><span style="margin-right: 10%; float: left">*Expire Date:</span>
                                                                                        <input style="width: 64%" type="tel" placeholder="MM/YYYY" class="masked" pattern="(1[0-2]|0[1-9])\/\d\d" data-valid-example="11/18" class="tDate" id="txtExpire" />

                                                                                    </td>
                                                                                </tr>

                                                                            </tbody>
                                                                        </table>


                                                                        <table cellspacing="3" cellpadding="5" style="width: 100%; display: none" id="tblCheck">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td colspan="2">Checking Account:</td>
                                                                                </tr>
                                                                               <%-- <tr>
                                                                                    <td class="auto-style56">*Name on Account:</td>
                                                                                    <td>
                                                                                        <input type="text" id="cnameAccountapp1Txt" class="nameAccountapp1Txt" style="width: 254px" />
                                                                                      
                                                                                    </td>
                                                                                </tr>--%>
                                                                                <tr>
                                                                                    <td class="auto-style56">*Routing number (2nd # from bottom left):</td>
                                                                                    <td>
                                                                                        <input type="text" id="routingnumapp1Txt" style="width: 254px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style56">Re-Enter Routing number:</td>
                                                                                    <td>
                                                                                        <input type="text" id="rerountingnumapp1Txt" style="width: 254px" />

                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style56">* Account number (last # from bottom left):</td>
                                                                                    <td style="">
                                                                                        <input type="text" id="checkacctnumapp1Txt" style="width: 254px" />


                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style56">Re-Enter Account number:</td>
                                                                                    <td>
                                                                                        <input type="text" id="recheckacctnumapp1Txt" style="width: 254px" />

                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>

                                                                        <div style="text-align: center; margin-bottom: 20px;">
                                                                            <%--<input type="button" class="btn btnNewColor" value="Submit" id="btnSubmit" />--%>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </form>

                                            <table style="width: 100%; margin-top: 20px; float: left;">
                                                <tbody>
                                                    <tr>
                                                        <td style="text-align: center; width: 30%;">
                                                            <input type="button" class="btn btnNewColor" id="btnExit" style="background-color: #3B5998; float: left; margin-right:20px;" value="Exit" /></td>
                                                        <td style="width: 40%;">
                                                            <input type="button" class="btn btnNewColor" value="Submit Cash Payment" style="background-color: #3B5998" id="btnSubmitCash" />

                                                            <button id="btnSubmit" api_login_id="<%=api_loginID%>"
                                                                method="sale"
                                                                version_number="1.0"
                                                                utc_time="<%=utc_time%>"
                                                                card_number=""
                                                                account_number=""
                                                                expire=""
                                                                cvv=""
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
                                                                allowed_methods=""
                                                                total_amount_attr="edit"
                                                                signature="<%= pay_now_single_return_string%>"                                                               
                                                                total_amount="<%= sTotal%>"
                                                                callback="oncallback"
                                                                order_number="A1234"
                                                                button_text="Confirm Payment"
                                                                class="btnPaymentGetway">
                                                                Save & Submit </button>
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <input type="button" id="btnNext" class="btn" style="background-color: #66FF00" value="Go Tenant Application" />

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
     <!--Start of Tawk.to Script-->

    <script type="text/javascript">

        var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();

        (function () {

            var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];

            s1.async = true;

            s1.src = 'https://embed.tawk.to/5ccb0b9ad07d7e0c63919cbc/default';

            s1.charset = 'UTF-8';

            s1.setAttribute('crossorigin', '*');

            s0.parentNode.insertBefore(s1, s0);

        })();

    </script>

    <!--End of Tawk.to Script-->

</body>
</html>


