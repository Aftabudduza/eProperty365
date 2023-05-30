<%@ Page Title="EProperty365: Online Tenant Payment" Language="C#" AutoEventWireup="true" CodeBehind="TenantOnlinefee.aspx.cs" Inherits="eProperty.Pages.Resident.TenantOnlinefee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <link href="../../css/Site.css" rel="stylesheet" />
     <link rel="stylesheet" href="../plugins/SlickNav/slicknav.css" />
    <title>EProperty365: Online Tenant Payment</title>
   

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
    <script type="text/javascript" src="../../scripts/select2/dist/js/select2.min.js"></script>
    <script type="text/javascript" src="../../AdminLTE/iCheck/icheck.js"></script>
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/TenantAllJs/TenantOnlineFee.js"></script>
     <script type="text/javascript" src="../../plugins/SlickNav/jquery.slicknav.min.js"></script>
    <%--<script type="text/javascript" src="https://sandbox.forte.net/checkout/v1/js"></script>--%>
    <script type="text/javascript" src="https://checkout.forte.net/v1/js"></script>

     <style type="text/css">
        .skin-blue .sidebar-menu .treeview-menu > li > a.active {
            color: red;
        }
    </style>

    <style type="text/css">
        #ddlLocation .select2-container {
            width: 100% !important;
        }
    </style>
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

    <script type="text/javascript">
        $(function () {
            $('#menu').slicknav();
        });
        $(document).ready(function () {
            //$('.sidebar-menu').tree();
            //$('.active').closest('li.treeview').addClass('active');
        })
    </script>

    <style type="text/css">
        .main-footer {
            clear: both;
            float: left !important;
            width: 100% !important;
        }

        #navFooter {
            display: block;
        }

        .nav > li {
            float: left;
        }

        .nav > li > a {
            float: left;
        }

         .slicknav_menu {
	        display:none;
        }

        @media screen and (max-width: 767px) {
	        /* #menu is the original menu */
	        #menu {
		        display:none;
	        }
	
	        .slicknav_menu {
                background: #8B9DC3;
				display: block;
				top: 10px;
				left: 150px;
				position: absolute;
				z-index: 10000;
	        }
        }

    </style>

</head>
<body class="hold-transition skin-blue sidebar-mini fixed">
    <!-- Site wrapper -->
    <div class="wrapper">
        <header class="main-header">
            <!-- Logo -->
            <a href="/" class="logo">
                <!-- mini logo for sidebar mini 50x50 pixels -->
                <span class="logo-mini">
                    <img alt="" src="https://www.eproperty365.net/Images/logo.png" width="100" class="img img-responsive" /></span>
                <!-- logo for regular state and mobile devices -->
                <span class="logo-lg">
                    <img alt="" src="https://www.eproperty365.net/Images/logo.png" width="120" class="img pull-left" /></span>
            </a>
            <!-- Header Navbar: style can be found in header.less -->
            <nav class="navbar navbar-static-top">
                <div class="col-md-12" style="float: left; margin: 10px 0;">
                    <h6>World Best Property & Facilities Management Software</h6>
                </div>
                <div class="col-md-12" style="float: left; margin: 10px 0;">
                    <!-- Sidebar toggle button-->
                   <%-- <a href="#" class="sidebar-toggle" data-toggle="push-menu" role="button">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </a>--%>

                    <div class="col-md-10 adBoxHeader ad-code-container">
                        <script type="text/javascript" src="https://banneradvertising.adclickmedia.com/cgi-bin/bannerrotate.cgi?sbutcher::70109"></script>
                        <%-- <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
                        </script>--%>

                        <%-- <h6>Ad Banner</h6>--%>
                    </div>
                    <div style="float:left; width: 50px;">
                        <a href="https://www.eproperty365.net/e365help/e365help.html" target="_blank">Help</a>
                    </div>
                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">
                            <!-- User Account: style can be found in dropdown.less -->
                            <li class="dropdown user user-menu">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <%--<img src="https://www.eproperty365.net/AdminLTE/img/noimage.png" class="user-image" alt="User Image">--%>
                                    <asp:Image ImageUrl="https://www.eproperty365.net/AdminLTE/img/noimage.png" ID="imgTopLogo" CssClass="user-image" alt="User Image" runat="server" />
                                </a>
                                <ul class="dropdown-menu">
                                    <!-- User image -->
                                    <li class="user-header">
                                        <%--<img src="../AdminLTE/img/noimage.png" class="img-circle" alt="User Image">--%>
                                        <asp:Image ImageUrl="https://www.eproperty365.net/AdminLTE/img/noimage.png" ID="imgTopIcon" CssClass="img-circle" alt="User Image" runat="server" />

                                        <% if (Session["Username"] != null) %>
                                        <% { %>
                                        <p>
                                            <%=Session["Username"]%>
                                            <% if (Session["UserId"] != null) %>
                                            <% { %>
                                            <br />
                                            <span id="spanAccount" runat="server"></span>

                                            <% } %>
                                        </p>
                                        <% } %>

                               
                                    </li>

                                    <!-- Menu Footer-->
                                    <li class="user-footer">
                                        <div class="pull-left">
                                            <span id="spanReset" runat="server"></span>

                                        </div>
                                        <div class="pull-right">
                                            <%--<a href="https://www.eproperty365.net/Pages/Logout.aspx" class="btn btn-default btn-flat">Sign out</a>--%>
                                            <span id="spanSignOut" runat="server"></span>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>


            </nav>
        </header>
        <!-- =============================================== -->
        <!-- Left side column. contains the sidebar -->
        <aside class="main-sidebar">
            <!-- sidebar: style can be found in sidebar.less -->
            <section class="sidebar">
                <!-- sidebar menu: : style can be found in sidebar.less -->
                <ul class="sidebar-menu" data-widget="tree" id="menu">
                    <li id="liHeader" runat="server" class="header"></li>
                    <%if (Session["UserType"] != null && Convert.ToBoolean(Session["bIsLogin"]) == true) %>
                    <% { %>

                    <li class="treeview">
                        <a href="#">
                            <img alt="" src="https://www.eproperty365.net/Images/dashboard_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                            <span id="spanDash" runat="server">Dashboad</span>
                            <span class="pull-right-container">
                                <i class="fa fa-angle-left pull-right"></i>
                            </span>
                        </a>
                        <ul class="treeview-menu" id="11" style="display: block;">
                            <li id="lihome" runat="server"></li>
                        </ul>
                    </li>

                    <%if (Session["UserType"].ToString() == "5" || Session["UserType"].ToString() == "6" || Session["UserType"].ToString() == "7") %>
                    <% { %>
                     <li class="treeview">
                        <a href="#">
                            <img alt="" src="https://www.eproperty365.net/Images/message_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                            <span>Mesage Box</span>
                            <span class="pull-right-container">
                                <i class="fa fa-angle-left pull-right"></i>
                            </span>
                        </a>
                        <ul class="treeview-menu" id="31">
                            <li><a href="https://www.eproperty365.net/Pages/Resident/ResidentTenantDashboard.aspx"><i class="fa fa-circle-o"></i>View communications </a></li>
                        </ul>
                    </li>

                      <li class="treeview">
                        <a href="#">
                            <img alt="" src="https://www.eproperty365.net/Images/Unit_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                            <span>Unit</span>
                            <span class="pull-right-container">
                                <i class="fa fa-angle-left pull-right"></i>
                            </span>
                        </a>
                        <ul class="treeview-menu" id="32">
                            <li><a href="https://www.eproperty365.net/Pages/Resident/TenantOnlinefee.aspx"><i class="fa fa-circle-o"></i>Make Payment </a></li>
                        </ul>
                    </li>
                      <li class="treeview">
                        <a href="#">
                            <img alt="" src="https://www.eproperty365.net/Images/document_managment_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                            <span>Document Imaging</span>
                            <span class="pull-right-container">
                                <i class="fa fa-angle-left pull-right"></i>
                            </span>
                        </a>
                        <ul class="treeview-menu" id="33">
                            <li><a href="https://www.eproperty365.net/Pages/Resident/TenantDocuments.aspx"><i class="fa fa-circle-o"></i>Document Imaging</a></li>
                        </ul>
                    </li>
                      <li class="treeview">
                        <a href="#">
                            <img alt="" src="https://www.eproperty365.net/Images/report_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                            <span>Reports</span>
                            <span class="pull-right-container">
                                <i class="fa fa-angle-left pull-right"></i>
                            </span>
                        </a>
                        <ul class="treeview-menu" id="34">
                            <li><a href="https://www.eproperty365.net/Pages/Resident/TenantPaymentHistory.aspx"><i class="fa fa-circle-o"></i>Payment History</a></li>
                        </ul>
                    </li>

                    <li class="treeview">
                        <a href="#">
                            <img alt="" src="https://www.eproperty365.net/Images/setting_icon.png" style="margin-right: 5px;" width="20" class="img img-responsive" />

                            <span>Settings</span>
                            <span class="pull-right-container">
                                <i class="fa fa-angle-left pull-right"></i>
                            </span>
                        </a>
                        <ul class="treeview-menu" id="35">
                            <li><a href="https://www.eproperty365.net/Pages/Resident/TenantProfile_DashBoard.aspx"><i class="fa fa-circle-o"></i>Add/Change User (Tenant)</a></li>
                        </ul>
                    </li>


                    <% } %>

                    <% } %>
                </ul>
            </section>
            <!-- /.sidebar -->
        </aside>
        <!-- =============================================== -->
        <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper">
            <!-- Main content -->
            <section class="content" style="padding: 45px 15px;">
                   <form id="form1" runat="server" class="form-horizontal">
                   <div class="box">
                        <div class="col-md-12">
                            <div class="box box-primary" style="box-shadow: none;">
                                <div class="row">
                                    <div class="box-header with-border CommonHeader col-md-12" style="margin-top: 0px;">
                                        <h3 class="box-title" id="H1" runat="server">Resident / Tenant  Make Online Payment</h3>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <label for="" class="col-sm-3 control-label">*Location </label>
                                        <div class="col-sm-4">
                                            <select id="ddlLocation" class="form-control ddl"></select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label for="" class="col-sm-3 control-label">*Unit ID </label>
                                        <div class="col-sm-4">
                                            <span id="txtUnitId"></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label for="" class="col-sm-3 control-label">Resident /Tenant Name </label>
                                        <div class="col-sm-4">
                                            <span id="txtTenantName"></span>
                                        </div>

                                    </div>
                                     <div class="row">
                                        <label for="" class="col-sm-3 control-label">Rental Fee </label>
                                        <div class="col-sm-4">
                                            <%--<input type="text" readonly="readonly" id="txtRentAmount" runat="server" class="form-control" />--%>
                                            $<span id="txtRentAmount" runat="server">0</span>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <label for="" class="col-sm-3 control-label">Process Fee </label>
                                        <div class="col-sm-4">
                                          <%--  <input type="text" id="txtACHFee" class="form-control" />--%>
                                            $<span id="txtACHFee">0</span>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label for="" class="col-sm-3 control-label">*Enter Amount being Paid </label>
                                        <div class="col-sm-4">
                                            <input type="text" id="txtPaidAmount" class="form-control" />
                                        </div>
                                    </div>
                   
                                    <div class="row" style="padding: 15px;">
                                        <div class="col-md-6">

                                            <table cellspacing="3" cellpadding="5" style="width: 100%" id="tblCredit">
                                                <tbody style="padding: 5px;">
                                                    <tr>
                                                        <td style="" colspan="2">
                                                            <span style="margin-right: 5%; float: left">*Name on Account:</span>
                                                            <input type="text" id="nameAccountapp1Txt" class="nameAccountapp1Txt" style="width: 74%" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="" colspan="2">
                                                            <span style="margin-right: 10%; float: left">*Card Address:</span>
                                                            <input type="text" id="addressapp1Txt1" style="width: 74%" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%">
                                                            <span style="margin-right: 7%; float: left">*City:</span>
                                                            <input type="text" id="cityapp1Txt" style="width: 80%" />
                                                        </td>
                                                        <td style="width: 50%">
                                                            <span style="margin-right: 10%; float: left">*State:</span>
                                                            <select id="ddlstateapp" style="width: 65%" class="ddl state"></select>                                           
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="">
                                                            <span style="margin-right: 1%; float: left">*Zip code:</span>
                                                            <input type="text" id="zipcodeapp1Txt" style="width: 75%" />
                                                        </td>
                                                    </tr>
                                    
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="col-md-6">
                                            <table cellspacing="3" cellpadding="5" style="width: 100%; background-color: #8B9DC3;" id="tblCheck">
                                                <tbody>
                                                    <tr>
                                                        <td colspan="2">Checking Account:</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style56">*Routing number (2nd # from bottom
                                                    left):</td>
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
                                                        <td class="auto-style56">* Account
                                                    number (last # from bottom left):</td>
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
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-12" style="color: red">
                                            Note: Returned Check: There is a minumun of $50.00 for each return check. LateRent Fee may apply.
                                        </label>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                   </form>
                    <div style="width: 100%; text-align: center; ">
                        <table style="width: 100%; float: left;">
                            <tbody>
                                <tr>
                                    <td style="text-align: center; width: 50%;">
                                        <input type="button" class="btn btnNewColor" id="btnCancel" style="background-color: #3B5998" value="Cancel" /></td>
                                    <td style="width: 50%;">
                                        <%--<input type="button" id="btnSubmit" class="btn" style="background-color: #66FF00" value="Submit" />--%>
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
                                                                order_number="<%= sOrderNo%>""
                                                                button_text="Confirm Payment"
                                                                allowed_methods="echeck"
                                                                class="btnPaymentGetway">
                                                                Submit</button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>            
                
            </section>
            <!-- /.content -->
        </div>
        <!-- /.content-wrapper -->
        <footer class="main-footer">    
            <ul id="navFooter" class="nav">
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
        </footer>
    </div>
    <!-- ./wrapper -->

    <%--  <div class="modal fade" id="pleaseWait" tabindex="-1" role="dialog" aria-labelledby="pleaseWaitLabel" data-backdrop="static">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <span class="modal-title" id="pleaseWaitLabel"><span class="glyphicon glyphicon-time"></span>&nbsp;Please Wait</span>
                </div>
                <div class="progress">
                    <div class="progress-bar progress-bar-info progress-bar-striped active" style="width: 100%">
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

</body>
</html>


