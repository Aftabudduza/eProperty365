<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="eProperty.UserControls.Header" %>
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
