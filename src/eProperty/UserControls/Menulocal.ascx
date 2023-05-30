<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menulocal.ascx.cs" Inherits="eProperty.UserControls.Menulocal" %>
<aside class="main-sidebar">
    <!-- sidebar: style can be found in sidebar.less -->
    <section class="sidebar">
        <!-- sidebar menu: : style can be found in sidebar.less -->
        <ul class="sidebar-menu" data-widget="tree">
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
                <ul class="treeview-menu" id="11">
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
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Create / Change Commercial Page </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Admin/AddResidentialUnit.aspx"><i class="fa fa-circle-o"></i>Create / Change Condo Page </a></li>
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
                     <li><a href="https://www.eproperty365.net/Pages/Resident/ApproveEPropertyFinancialList.aspx"><i class="fa fa-circle-o"></i>Approve Eproperty Transaction </a></li>
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
            <li class="treeview">
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
            </li>

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

                    <li><a href="https://www.eproperty365.net/Pages/Resident/ResidentialUnitListing.aspx"><i class="fa fa-circle-o"></i>Residential Add Template Listing </a></li>
                    <li><a href="https://www.eproperty365.net/Pages/Resident/TenantList.aspx"><i class="fa fa-circle-o"></i>Residential Tenant Listing </a></li>
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
