<%@ Page Title="EProperty365: Pay ACH Fee" Language="C#" EnableEventValidation="false" Debug="true" AutoEventWireup="true" CodeBehind="PayACHFee.aspx.cs" Inherits="eProperty.Pages.Resident.PayACHFee" %>

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

    <title>EProperty365: Pay ACH Fee</title>
   

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
    <form id="form2" runat="server" class="form-horizontal">
        <%--<asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>--%>
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
                    <%--<a href="#" class="sidebar-toggle" data-toggle="push-menu" role="button">
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


            <%if (Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3") %>
            <% { %>

            <li class="treeview">
                <a href="#">
                    <img alt="" src="https://www.eproperty365.net/Images/message_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                    <span>Mesage Box</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu" id="12">
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>View communications </a></li>
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
                <ul class="treeview-menu" id="13">
                    <%-- <li><a href="https://www.eproperty365.net/Pages/Admin/AddIncome.aspx"><i class="fa fa-circle-o"></i>Add Income </a></li>
                    <li><a href="#"><i class="fa fa-circle-o"></i>Add Expense </a></li>--%>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Maintenance Manager Unit </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Create/Change Equipment Unit </a></li>
                </ul>
            </li>

            <li class="treeview">
                <a href="#">
                    <img alt="" src="https://www.eproperty365.net/Images/webpage_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                    <span>Manage Web Pages</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu" id="14">
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Create / Change Residential Page </a></li>
                  <%--  <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Create / Change Commercial Page </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Create / Change Condo Page </a></li>--%>
                </ul>
            </li>
            <li class="treeview">
                <a href="#">
                    <img alt="" src="https://www.eproperty365.net/Images/accounting_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                    <span>Accounting</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu" id="15">
                     <%if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3") %>
                    <% { %>
                     <li><a href="https://www.eproperty365.net/Pages/Account/DashboardAccount.aspx"><i class="fa fa-circle-o"></i>Accounting Main Menu </a></li>
                     <li><a href="https://www.eproperty365.net/Pages/Resident/PayACHFee.aspx"><i class="fa fa-circle-o"></i>Pay ACH/Utility Fees </a></li>
                    <% } %>                  
                   
                   
                    <%if (Session["bIsAdmin"] != null && Convert.ToBoolean(Session["bIsAdmin"]) == true) %>
                    <% { %>                    
                    <li><a href="https://www.eproperty365.net/Pages/Resident/ApproveFinancialList.aspx"><i class="fa fa-circle-o"></i>Approve Owner Transaction </a></li>
                     <li><a href="#"><i class="fa fa-circle-o"></i>Approve Eproperty Transaction </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Resident/PayCommission.aspx"><i class="fa fa-circle-o"></i>Pay Sales Commissions </a></li>
                    <% } %>
                </ul>
            </li>
            <li class="treeview">
                <a href="#">
                    <img alt="" src="https://www.eproperty365.net/Images/maintenance_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                    <span>Maintenance</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu" id="16">
                    <li><a href="https://www.eproperty365.net/Pages/Resident/ManagementDashboard.aspx"><i class="fa fa-circle-o"></i>Maintenance Dashboard</a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>View Residential Maintenance </a></li>
                </ul>
            </li>
           <%-- <li class="treeview">
                <a href="#">
                    <img alt="" src="https://www.eproperty365.net/Images/document_managment_icon.png" width="20" style="margin-right: 5px;" class="img img-responsive" />
                    <span>Document Imaging</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu" id="17">
                    <li><a href="#"><i class="fa fa-circle-o"></i>Document Imaging</a></li>
                </ul>
            </li>--%>

            <li class="treeview">
                <a href="#">
                    <img alt="" src="https://www.eproperty365.net/Images/vendormanagment_icon.png" style="margin-right: 5px;" width="20" class="img img-responsive" />

                    <span>Vendor Management</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu" id="18">
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddVendor.aspx"><i class="fa fa-circle-o"></i>Add/Change Vendor </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddVendor.aspx"><i class="fa fa-circle-o"></i>Vendor List </a></li>
                    <%--<li><a href="#"><i class="fa fa-circle-o"></i>Work Order PO </a></li>--%>
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
                <ul class="treeview-menu" id="19">
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Unit Analytics</a></li>
                </ul>
            </li>

            <li class="treeview ">
                <a href="#">
                    <img alt="" src="https://www.eproperty365.net/Images/setting_icon.png" style="margin-right: 5px;" width="20" class="img img-responsive" />

                    <span>Settings</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu" id="20">
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddOwner.aspx"><i class="fa fa-circle-o"></i>Add/Change Owner Profile</a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddPropertyManager.aspx"><i class="fa fa-circle-o"></i>Add/Change Property Manager</a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddUser.aspx"><i class="fa fa-circle-o"></i>Add/Change User (Tenant)</a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddContact.aspx"><i class="fa fa-circle-o"></i>Add/Change Contact Info</a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddLocation.aspx"><i class="fa fa-circle-o"></i>Add/Change Location </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Add/Change Unit </a></li>

                   <%-- <li><a href="https://www.eproperty365.net/Pages/Resident/ResidentialUnitListing.aspx"><i class="fa fa-circle-o"></i>Residential Add Template Listing </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Resident/TenantList.aspx"><i class="fa fa-circle-o"></i>Residential Tenant Listing </a></li>--%>

                    <%--<li><a href="https://www.eproperty365.net/Pages/Admin/UserReport.aspx"><i class="fa fa-circle-o"></i>User Search for Support </a></li>--%>

                    <%if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3") %>
                    <% { %>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddOwnerSystem.aspx"><i class="fa fa-circle-o"></i>Add/Change Owner System Info</a></li>
                    <% } %>

                    <li><a href="https://www.eproperty365.net/Pages/Resident/ImportTenantProfile.aspx"><i class="fa fa-circle-o"></i>Create Tenant Account script  of existing tenant </a></li>

                    <%if (Session["bIsAdmin"] != null && Convert.ToBoolean(Session["bIsAdmin"]) == true) %>
                    <% { %>
                    <li><a href="https://www.eproperty365.net/Pages/Account/SalesPartnerDealerDashboard.aspx"><i class="fa fa-circle-o"></i>Sales Partner & Dealer Dashboard </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddGlobalSystem.aspx"><i class="fa fa-circle-o"></i>Global Eproperty365 System Info</a></li>
                    <% } %>
                </ul>
            </li>

            <% } %>




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
                    <li><a href="#"><i class="fa fa-circle-o"></i>Document Imaging</a></li>
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
                    <div class="row">
                        <div class="col-md-12">
                            <%--<asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                <ContentTemplate>--%>
                                    <div class="box">
                                        <div class="col-md-12">
                                            <div class="box box-primary">
                                                <div class="box-header with-border CommonHeader col-md-12">
                                                    <h3 class="box-title">ACH/Utility Transaction List</h3>
                                                </div>

                                                <asp:HiddenField ID="page_pg_utc_time" runat="server" Value="" />
                                                <asp:HiddenField ID="page_pg_transaction_order_number" runat="server" Value="" />

                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            <label for="ddlType" class="col-sm-3 control-label" style="float: left;">Fee Type:</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlType" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" runat="server">
                                                                    <asp:ListItem Value="5040" Selected="True">ACH Process Fee (5040)</asp:ListItem>
                                                                    <asp:ListItem Value="6130">Unit Fee (6130)</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <label for="txtDue" class="col-sm-3 control-label" style="float: left;">Total Due:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtDue" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label for="txtPaid" class="col-sm-3 control-label" style="float: left;">Paid Amount:</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtPaid" Enabled="true" runat="server"  ValidationGroup="Pay" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                    ControlToValidate="txtPaid" Display="Dynamic"
                                                                    ErrorMessage="Amount must be in format 0.00."
                                                                    ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="txtPaid" Display="Dynamic" ValidationGroup="Pay" ForeColor="Red"
                                                                    ErrorMessage="Amount is required."></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" Text="Calculate" CssClass="btn btn-success" />
                                                            <asp:Button ID="btnPay" OnClick="btnPay_Click" runat="server" Text="Pay"  ValidationGroup="Pay" CssClass="btn btn-success" />
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="box">
                                        <div class="box-body">
                                            <form class="vertical-form">
                                                <fieldset>
                                                    <asp:GridView Width="100%" ID="gvLocation" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" CssClass="table table-bordered table-striped"
                                                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gvLocation_PageIndexChanging"
                                                        OnSorting="gvLocation_Sorting" PageSize="20" Style="float: left; margin-bottom: 10px;" AllowSorting="True">
                                                        <PagerSettings Position="TopAndBottom" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Invoice No" SortExpression="Data">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Transaction Type" SortExpression="Data">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("TransactionType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ledger Code" SortExpression="Data">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("LedgerCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Due" SortExpression="Data">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("Debit","{0:c}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paid" SortExpression="Data">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("Credit","{0:c}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Created On" SortExpression="Data">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreateDate", "{0:MM/dd/yyyy hh:mm tt}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" />
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
                                            </form>
                                        </div>
                                        <!-- left column -->

                                    </div>
                               <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
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
    </form>
</body>
</html>
