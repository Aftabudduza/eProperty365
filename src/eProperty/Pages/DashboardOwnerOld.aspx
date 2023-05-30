<%@ Page Title="EProperty365: Admin Dashboard" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DashboardOwnerOld.aspx.cs" Inherits="eProperty.Pages.DashboardOwnerOld" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager runat="server" ID="sc1">
        </asp:ToolkitScriptManager>
        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                    <ContentTemplate>
                        <div class="box" runat="server" id="divChart">
                            <div class="box-body">
                            </div>
                        </div>
                        <div class="box" runat="server" id="divLinkMenu">
                            <div class="box-body">
                                <div class="col-md-6" style="float: left;">
                                    <ul>
                                        <%--<li id="litop1" runat="server"><a href="../Pages/Admin/AddOwnerSystem.aspx">1) Setup Owner Account Profile</a> </li>--%>
                                        <li id="litop2" runat="server"><a href="../Pages/Admin/AddOwner.aspx">1) Setup Owners Profile</a></li>
                                        <li id="litop3" runat="server"><a href="../Pages/Admin/AddLocation.aspx">2) Setup Property Location Profile</a></li>
                                        <li id="litop4" runat="server"><a href="../Pages/Admin/AddResidentialUnit.aspx">3) Setup Property Unit Profile</a> </li>
                                    </ul>
                                </div>
                                <div class="col-md-6" style="float: left;">
                                    <ul>
                                        <li id="litop5" runat="server"><a href="../Pages/Admin/AddResidentialUnit.aspx">4) Setup Document Management System</a></li>
                                        <li id="litop6" runat="server"><a href="../Pages/Account/AddChartofAccount.aspx">5) Setup Accounting System Profile</a></li>
                                        <li id="litop7" runat="server"> <a href="../Pages/Resident/ImportTenantProfile.aspx">6) Setup Existing Tenants Profile Import</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #MainContent_divLinkMenu ul {
            list-style-type: none;
            /*width: 30%;*/
            margin-left: 0px;
            margin-right: 2px;
        }

        #MainContent_divLinkMenu colorRed {
            color: red;
        }

        #MainContent_divLinkMenu colorGreen {
            color: green;
        }
    </style>
</asp:Content>
