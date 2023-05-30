<%@ Page Title="EProperty365: Dealer & Sales Partner Dashboard" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="SalesPartnerDealerDashboard.aspx.cs" Inherits="eProperty.Pages.Account.SalesPartnerDealerDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box">
        <div class="col-md-12">
            <div class="box-header with-border CommonHeader col-md-12">
                <h3 class="box-title">Dealer & Sales Partner Dashboard</h3>
            </div>

            <div class="nav-tabs-custom">
                <ul id="nav-tab" class="nav nav-tabs">
                    <li><a id="YourCommissions" class="nav-item nav-link" data-toggle="tab" href="#nav-YourCommissions" role="tab">Your<br />
                        Commissions</a></li>
                    <li><a id="YourAccounts" class="nav-item nav-link" data-toggle="tab" href="#nav-YourAccounts" role="tab">Your Accounts</a></li>
                    <li><a id="SalesPartnerProfile" class="nav-item nav-link" data-toggle="tab" href="#nav-SalesPartnerProfile" role="tab">Sales Partner
                        <br />
                        Profile</a></li>
                    <li><a id="DealerProfile" class="nav-item nav-link" data-toggle="tab" href="#nav-DealerProfile" role="tab">Dealer Profile</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade" id="nav-YourCommissions">
                        <div class="box">
                            <div class="col-md-12">
                                <div class="box-body">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="nav-YourAccounts">
                        <div class="box">
                            <div class="col-md-12">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="form-group row">
                                                <label id="txtPartnerNameYourAccount" class="col-sm-12 col-form-label"></label>
                                            </div>
                                            <div class="form-group row">
                                                <label for="ddlLocationId" class="col-sm-2 col-form-label">Location Id:</label>
                                                <div class="col-sm-4">
                                                    <select class="form-control ddl" id="ddlLocationId"></select>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-12 col-form-label">Location Overview Search:</label>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-1 col-form-label">
                                                    <input type="checkbox" name="Active" id="chkAll" value="All" class="flat-red" />
                                                    All
                                                </label>
                                                <label for="ddlOwner" class="col-sm-1 col-form-label">Owner:</label>
                                                <div class="col-sm-4">
                                                    <select class="form-control ddl" id="ddlOwner"></select>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <input type="button"  class="btn" style="margin-right: 10px; background-color: #66FF00" value="Search" id="btnSearch" />
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-12 col-form-label">Filters:</label>
                                                <label style="margin-right: 7px">
                                                    <input type="checkbox" name="Active" id="chkAlpha" value="Alpha" class="flat-red" />
                                                    Alpha
                                                </label>
                                                <label style="margin-right: 7px">
                                                    <input type="checkbox" name="Active" id="chk20ItemPerPage" value="20ItemPerPage" class="flat-red" />
                                                    20 Item Per Page
                                                </label>
                                                <label style="margin-right: 7px">
                                                    <input type="checkbox" name="Active" id="chkAllUserPerPage" value="AllUserPerPage" class="flat-red" />
                                                    All User Per Page
                                                </label>
                                            </div>
                                            <div class="form-group row">
                                                <div id="divTableYourAccounts" style="max-height: 250px; width: 90%; margin: auto; overflow-x: auto;">
                                                    <table class="display" style="width: 100%" id="tblYourAccounts">
                                                        <thead>
                                                        <tr>
                                                            <th>Owner Name</th>
                                                            <th>Property Manager Name</th>
                                                            <th>Property Location</th>
                                                            <th>Number Units</th>
                                                            <th>Start Date</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="nav-SalesPartnerProfile">
                        <div class="box">
                            <div class="col-md-12">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-7">
                                            <div class="form-group row">
                                                <label for="txtSalesPartnerFirstName" class="col-sm-4 col-form-label">Sales Partner First Name:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" id="txtSalesPartnerFirstName" placeholder="Sales Partner First Name" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="txtSalesPartnerLastName" class="col-sm-4 col-form-label">Sales Partner Last Name:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" id="txtSalesPartnerLastName" placeholder="Sales Partner Last Name" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="txtAreaAddress1SalesPartner" class="col-sm-4 col-form-label">Address 1:</label>
                                                <div class="col-sm-8">
                                                    <textarea class="form-control" id="txtAreaAddress1SalesPartner" rows="1" maxlength="100"></textarea>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="txtAreaAddress2SalesPartner" class="col-sm-4 col-form-label">Address 2:</label>
                                                <div class="col-sm-8">
                                                    <textarea class="form-control" id="txtAreaAddress2SalesPartner" rows="1" maxlength="100"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-5 adminUse" id="divE365UseOnlySalesPartner" style="background-color: #8b9dc3;border: 1px solid #008b8b">
                                            <div class="form-group row">
                                                <label class="col-sm-12 col-form-label text-center">E365 Use Only</label>
                                            </div>
                                            <div class="form-group row">
                                                <label for="h6SalesPartnersIDSalesPartner" class="col-sm-5 col-form-label">Sales Partners ID:</label>
                                                <div class="col-sm-6">
                                                    <h6 id="h6SalesPartnersIDSalesPartner"></h6>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="txtJoinDateSalesPartner" class="col-sm-5 col-form-label">Join Date:</label>
                                                <div class="col-sm-6">
                                                    <input type="text" class="form-control tDate" id="txtJoinDateSalesPartner" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="txtCommissionRateSalesPartner" class="col-sm-5 col-form-label">Commission Rate:</label>
                                                <div class="col-sm-6">
                                                    <input type="text" class="form-control" id="txtCommissionRateSalesPartner" value="20" />
                                                </div>
                                                <span style="margin: auto">%</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="form-group row">
                                                <label for="txtCitySalesPartner" class="col-sm-1 col-form-label">City:</label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control" id="txtCitySalesPartner" placeholder="City" maxlength="30" />
                                                </div>
                                                <label for="ddlStateSalesPartner" class="col-sm-1 col-form-label">State:</label>
                                                <div class="col-sm-2">
                                                    <select id="ddlStateSalesPartner" class="form-control ddl"></select>
                                                </div>
                                                <label for="ddlCountrySalesPartner" class="col-sm-1 col-form-label">Country:</label>
                                                <div class="col-sm-2">
                                                    <select id="ddlCountrySalesPartner" class="form-control ddl"></select>
                                                </div>
                                                <label for="txtRegionSalesPartner" class="col-sm-1 col-form-label">Region:</label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control" id="txtRegionSalesPartner" placeholder="Region" maxlength="30" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="form-group row">
                                                <label for="txtZipCodeSalesPartner" class="col-sm-1 col-form-label">Zip Code:</label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control" id="txtZipCodeSalesPartner" placeholder="Zip Code" maxlength="30" />
                                                </div>
                                                <label for="txtPrimaryPhoneNumberSalesPartner" class="col-sm-2 col-form-label">Primary Phone Number:</label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control" id="txtPrimaryPhoneNumberSalesPartner" placeholder="Primary Phone Number" maxlength="20" />
                                                </div>
                                                <label for="txtMobilePhoneNumberSalesPartner" class="col-sm-2 col-form-label">Mobile Phone Number:</label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control" id="txtMobilePhoneNumberSalesPartner" placeholder="Mobile Phone Number" maxlength="20" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group row">
                                            <label for="txtRoutingNoSalesPartner" class="col-sm-2 col-form-label">Routing No:</label>
                                            <div class="col-sm-4">
                                                <input type="text" class="form-control"  id="txtRoutingNoSalesPartner" placeholder="Routing No" maxlength="25"/>
                                            </div>
                                            <label for="txtAccountNoSalesPartner" class="col-sm-2 col-form-label">Account No:</label>
                                            <div class="col-sm-4">
                                                <input type="text" class="form-control" id="txtAccountNoSalesPartner" placeholder="Account No" maxlength="25" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="form-group row">
                                                <label for="txtEmailAddressSalesPartner" class="col-sm-2 col-form-label">Email Address:</label>
                                                <div class="col-sm-4">
                                                    <input type="text" class="form-control isAdmin mailcheck" data-userProfileId="0" id="txtEmailAddressSalesPartner" placeholder="Email Address" maxlength="50" disabled="disabled" />
                                                </div>
                                                <label for="txtReEnterEmailAddressSalesPartner" class="col-sm-2 col-form-label">Re-Enter Email Address:</label>
                                                <div class="col-sm-4">
                                                    <input type="text" class="form-control isAdmin" id="txtReEnterEmailAddressSalesPartner" placeholder="Re-Enter Email Address" maxlength="50"  disabled="disabled"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="form-group row">
                                                <label for="txtPasswordSalesPartner" class="col-sm-2 col-form-label">Password:</label>
                                                <div class="col-sm-4">
                                                    <input type="text" class="form-control isAdmin" id="txtPasswordSalesPartner" placeholder="Password" maxlength="50" disabled="disabled"/>
                                                </div>
                                                <label for="txtReEnterPasswordSalesPartner" class="col-sm-2 col-form-label">Re-Enter Password:</label>
                                                <div class="col-sm-4">
                                                    <input type="text" class="form-control isAdmin" id="txtReEnterPasswordSalesPartner" placeholder="Re-Enter Password" maxlength="50"  disabled="disabled"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box adminUse">
                                        <div class="col-md-12">
                                            <div class="box-header with-border CommonHeader col-md-12" style="float: none;">
                                                <h5 class="box-title">Zip Code Coverage</h5>
                                            </div>

                                            <div class="box-body" style="background-color: #8b9dc3;">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="text-center">
                                                            <div id="divTableSalesPartner" style="max-height: 250px; width: 50%; margin: auto; overflow-x: auto; background-color: white;">
                                                                <table class="table table-responsive" id="tblSalesPartner">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Name of Zip</th>
                                                                            <th>Commission %</th>
                                                                            <th></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                            <div class="container">
                                                                <div class="form-group row">
                                                                    <label for="txtTblNameofZipSalesPartner" class="col-sm-2 col-form-label">Name of Zip:</label>
                                                                    <div class="col-sm-2">
                                                                        <input type="text" class="form-control" id="txtTblNameofZipSalesPartner" maxlength="30" />
                                                                    </div>
                                                                    <label for="txtTblCommissionSalesPartner" class="col-sm-2 col-form-label">Commission:</label>
                                                                    <div class="col-sm-2">
                                                                        <input type="text" class="form-control" id="txtTblCommissionSalesPartner" value="20" maxlength="30" />
                                                                    </div>
                                                                    <span style="margin-right: 10px; margin-top: auto; margin-bottom: auto; width: 10px;">%</span>
                                                                    <div class="col-sm-2">
                                                                        <input type="button" class="btn" style="margin-right: 10px; background-color: #66FF00" value="Add" id="btnAddSalesPartner" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div id="divTableSalesPartnerList" style="max-height: 250px; width: 90%; margin: auto; overflow-x: auto;">
                                                            <table class="table table-responsive table-bordered" id="tblSalesPartnerList">
                                                                <thead>
                                                                <tr>
                                                                    <th>Serial Code</th>
                                                                    <th>Name</th>
                                                                    <th>Address</th>
                                                                    <th>Commission Rate</th>
                                                                    <th>Email</th>
                                                                    <th>City</th>
                                                                    <th>State</th>
                                                                    <th>Zip Code</th>
                                                                </tr>
                                                                </thead>
                                                                <tbody>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="nav-DealerProfile">
                                <div class="box">
                                    <div class="col-md-12">
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-7">
                                                    <div class="form-group row">
                                                        <label for="txtDealerProfileFirstName" class="col-sm-4 col-form-label">Dealer First Name:</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtDealerProfileFirstName" placeholder="Dealer First Name" maxlength="50" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtDealerProfileLastName" class="col-sm-4 col-form-label">Dealer Last Name:</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtDealerProfileLastName" placeholder="Dealer Last Name" maxlength="50" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtAreaAddress1DealerProfile" class="col-sm-4 col-form-label">Address 1:</label>
                                                        <div class="col-sm-8">
                                                            <textarea class="form-control" id="txtAreaAddress1DealerProfile" rows="1" maxlength="100"></textarea>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtAreaAddress2DealerProfile" class="col-sm-4 col-form-label">Address 2:</label>
                                                        <div class="col-sm-8">
                                                            <textarea class="form-control" id="txtAreaAddress2DealerProfile" rows="1" maxlength="100"></textarea>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-5 adminUse" id="divE365UseOnlyDealerProfile" style="background-color: #8b9dc3;border: 1px solid #008b8b">
                                                    <div class="form-group row">
                                                        <label class="col-sm-12 col-form-label text-center">E365 Use Only</label>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="h6DealerIdDealerProfile" class="col-sm-5 col-form-label">Dealer ID:</label>
                                                        <div class="col-sm-6">
                                                            <h6 id="h6DealerIdDealerProfile"></h6>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtJoinDateDealerProfile" class="col-sm-5 col-form-label">Join Date:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" class="form-control tDate" id="txtJoinDateDealerProfile" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtCommissionRateDealerProfile" class="col-sm-5 col-form-label">Commission Rate:</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" class="form-control" id="txtCommissionRateDealerProfile" value="10" />
                                                        </div>
                                                        <span style="margin: auto">%</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="form-group row">
                                                        <label for="txtCityDealerProfile" class="col-sm-1 col-form-label">City:</label>
                                                        <div class="col-sm-2">
                                                            <input type="text" class="form-control" id="txtCityDealerProfile" placeholder="City" maxlength="30" />
                                                        </div>
                                                        <label for="ddlStateDealerProfile" class="col-sm-1 col-form-label">State:</label>
                                                        <div class="col-sm-2">
                                                            <select id="ddlStateDealerProfile" class="form-control ddl"></select>
                                                        </div>
                                                        <label for="txtZipCodeDealerProfile" class="col-sm-1 col-form-label">Zip Code:</label>
                                                        <div class="col-sm-2">
                                                            <input type="text" class="form-control" id="txtZipCodeDealerProfile" placeholder="Zip Code" maxlength="30" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="form-group row">
                                                        <label for="txtPrimaryPhoneNumberDealerProfile" class="col-sm-2 col-form-label">Primary Phone Number:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" class="form-control" id="txtPrimaryPhoneNumberDealerProfile" placeholder="Primary Phone Number" maxlength="20" />
                                                        </div>
                                                        <label for="txtMobilePhoneNumberDealerProfile" class="col-sm-2 col-form-label">Mobile Phone Number:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" class="form-control" id="txtMobilePhoneNumberDealerProfile" placeholder="Mobile Phone Number" maxlength="20" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="form-group row">
                                                        <label for="txtRoutingNoDealerProfile" class="col-sm-2 col-form-label">Routing No:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" class="form-control"  id="txtRoutingNoDealerProfile" placeholder="Routing No" maxlength="25"/>
                                                        </div>
                                                        <label for="txtAccountNoDealerProfile" class="col-sm-2 col-form-label">Account No:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" class="form-control" id="txtAccountNoDealerProfile" placeholder="Account No" maxlength="25" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="form-group row">
                                                        <label for="txtEmailAddressDealerProfile" class="col-sm-2 col-form-label">Email Address:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" class="form-control isAdmin mailcheck" data-userProfileId="0" id="txtEmailAddressDealerProfile" placeholder="Email Address" maxlength="50"  disabled="disabled"/>
                                                        </div>
                                                        <label for="txtReEnterEmailAddressDealerProfile" class="col-sm-2 col-form-label">Re-Enter Email Address:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" class="form-control isAdmin" id="txtReEnterEmailAddressDealerProfile" placeholder="Re-Enter Email Address" maxlength="50"  disabled="disabled"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="form-group row">
                                                        <label for="txtPasswordDealerProfile" class="col-sm-2 col-form-label">Password:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" class="form-control isAdmin" id="txtPasswordDealerProfile" placeholder="Password" maxlength="50" disabled="disabled"/>
                                                        </div>
                                                        <label for="txtReEnterPasswordDealerProfile" class="col-sm-2 col-form-label">Re-Enter Password:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" class="form-control isAdmin" id="txtReEnterPasswordDealerProfile" placeholder="Re-Enter Password" maxlength="50"  disabled="disabled"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="box adminUse">
                                                <div class="col-md-12">
                                                    <div class="box-header with-border CommonHeader col-md-12" style="float: none;">
                                                        <h5 class="box-title">Exclusive Territory</h5>
                                                    </div>

                                                    <div class="box-body" style="background-color: #8b9dc3;">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="text-center">
                                                                    <div id="divTableDealerProfile" style="max-height: 250px; width: 50%; margin: auto; overflow-x: auto; background-color: white;">
                                                                        <table class="table table-responsive" id="tblDealerProfile">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>Zip Code</th>
                                                                                    <th>Commission %</th>
                                                                                    <th></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <div class="container">
                                                                        <div class="form-group row">
                                                                            <label for="txtTblZipCodeDealerProfile" class="col-sm-2 col-form-label">Zip Code:</label>
                                                                            <div class="col-sm-2">
                                                                                <input type="text" class="form-control" id="txtTblZipCodeDealerProfile" maxlength="30" />
                                                                            </div>
                                                                            <label for="txtTblCommissionPaidDealerProfile" class="col-sm-2 col-form-label">Commission Paid:</label>
                                                                            <div class="col-sm-2">
                                                                                <input type="text" class="form-control" id="txtTblCommissionPaidDealerProfile" value="10" maxlength="30" />
                                                                            </div>
                                                                            <span style="margin-right: 10px; margin-top: auto; margin-bottom: auto; width: 10px;">%</span>
                                                                            <div class="col-sm-2">
                                                                                <input type="button" class="btn" style="margin-right: 10px; background-color: #66FF00" value="Add" id="btnAddDealerProfile" />
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <div id="divTableDealerProfileList" style="max-height: 250px; width: 90%; margin: auto; overflow-x: auto;">
                                                                                <table class="table table-responsive table-bordered" id="tblDealerProfileList">
                                                                                    <thead>
                                                                                    <tr>
                                                                                        <th>Serial Code</th>
                                                                                        <th>Name</th>
                                                                                        <th>Address</th>
                                                                                        <th>Commission Rate</th>
                                                                                        <th>Email</th>
                                                                                        <th>City</th>
                                                                                        <th>State</th>
                                                                                        <th>Zip Code</th>
                                                                                    </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                </div>
                <div class="box-footer" id="divSaveCancel">
                            <div class="row" style="margin-top: 30px;">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <input type="button" data-UpdateId="0" class="btn" style="margin-right: 10px; background-color: #66FF00" value="Save" id="btnSave" />
                                        <input type="button" class="btn" style="background-color: #3B5998" value="Cancel" id="btnCancel" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../../Content/DataTables/css/dataTables.bootstrap4.css" rel="stylesheet" />--%>
    <link rel="stylesheet" type="text/css" href="../../Content/DataTables/css/jquery.dataTables.css" />
    <style type="text/css">

        .nav-tabs-custom {
            background-color: #DFE3EE;
        }

        .nav-tabs-custom > .nav-tabs {
            border-bottom-color: initial;
            padding: 0px 0px 0px 18px;
            background-color: #8B9DC3;
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

        #nav-tab > li > a.active {
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
            height: 56px;
            text-align: center;
        }
    </style>
    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css">--%>
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/Account/SalesPartnerDealerDashboard.js"></script>
    <%--<script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>--%>
    <script type="text/javascript" src="../../Content/plugins/datatables/jquery.dataTables.js"></script>
    <%--<script src="../../Content/plugins/datatables/dataTables.bootstrap.js"></script>--%>
</asp:Content>
