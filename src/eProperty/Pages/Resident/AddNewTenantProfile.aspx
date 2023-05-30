<%@ Page Title="EProperty365: First Time Create Existing Tenant Profile" Language="C#" MasterPageFile="~/MasterPage/DashboardManagament.Master" AutoEventWireup="true" CodeBehind="AddNewTenantProfile.aspx.cs" Inherits="eProperty.Pages.Resident.AddNewTenantProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box" style="border: 1px solid #000000;">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="row">
                    <div class="box-header with-border CommonHeader col-md-12" style="margin-top: 0px;">
                        <h3 class="box-title" id="H1" runat="server">First Time Create Tenant Profile</h3>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label for="" class="col-sm-12 control-label" style="color: red">
                                Dear Tenant,
                            </label>
                        </div>
                        <div class="col-md-12">
                            <p>The owner has decided to use Eproperty365 rental property management software to management the property to make it easier and faster for you. We you to add your basic information to the form that probably provided on your original application. This will allow us to set up your tenant account. you will be able to pay your rent via checking account online.You will also be able you to communicate via Email messenger for any issues you are having including problems with the software. We look forward to serving you.</p>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label for="" class="col-sm-3 control-label">Unit Location: </label>
                            <div class="col-sm-9">
                                <span id="txtUnitLocation"></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="" class="col-sm-3 control-label">Unit ID: </label>
                            <div class="col-sm-9">
                                <span id="txtUnitId"></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="" class="col-sm-12 control-label" style="color: red">
                                You can not change your email account that you registered with!
                                <br />
                                You must contact Owner or Property Management to change password. 
                                This could take 5 days or more to change.
                            </label>

                        </div>
                    </div>
                    <div class="row">                        
                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Email Address: </label>
                            <div class="col-sm-6">
                                <input type="text" id="txtEmailUser" class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Re-Email Address:</label>
                            <div class="col-sm-6">
                                <input type="text" id="txtReEmailUser" class="form-control" />
                            </div>
                        </div>                  
                    </div>

                    <div class="row">                        
                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Password </label>
                            <div class="col-sm-6">
                                <input type="password" id="txtPassword"  class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Re-Enter Password:</label>
                            <div class="col-sm-6">
                                <input type="password" id="txtRePassword" class="form-control" />
                            </div>
                        </div>                  
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <div style="width: 100%">
                                <span style="width: 75%; margin-left: 10px; float: left">Agreement in the Name of </span><span style="width: 23%; color: red; float: left">* denote you must fill</span>
                            </div>
                            <table cellspacing="3" cellpadding="5" style="width: 100%; float: left; border: 1px solid black; background-color: #DFE3EE">
                                <tbody style="padding: 5px;">
                                    <tr>
                                        <td style="">
                                            <span style="margin-right: 5%;">*Name; First:</span>
                                            <input type="text" id="txtFirstName" style="width: 58%" />
                                            <input type="hidden" id="hdTenantBasicId" value="0" />

                                        </td>
                                        <td style="">
                                            <span style="margin-right: 5%;">Middle:</span>
                                            <input type="text" id="txtMiddleName" style="width: 61%" />

                                        </td>
                                        <td style="">
                                            <span style="margin-right: 5%;">*Last:</span>
                                            <input type="text" id="txtLastName" style="width: 73%" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="" colspan="3">
                                            <span style="margin-right: 7%">Alias or nick name used:</span>
                                            <input type="text" id="txtAliasName" style="width: 75%" />


                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="" colspan="3">
                                            <span style="margin-right: 14%">*Address:</span>
                                            <input type="text" id="txtAddress" style="width: 75%" />


                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="" colspan="3">
                                            <span style="margin-right: 14%">Address1:</span>
                                            <input type="text" id="txtAddress1" style="width: 75%" />


                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="">
                                            <span style="margin-right: 8%">*Country:</span>
                                            <select id="ddlCountry" style="width: 66%" class="ddl22 country"></select>

                                        </td>
                                        <td class="" colspan="2">
                                            <span style="margin-right: 10%">Region:</span>
                                            <input type="text" id="txtRegion" style="width: 75%" />
                                            <%-- <asp:TextBox ID="stateapp1Txt" Width="75%"
                            runat="server" ></asp:TextBox>--%>
                        
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="">
                                            <span style="margin-right: 14%">*City:</span>
                                            <%-- <select id="ddlcityapp" style="width: 75%" class="ddl22"></select>--%>
                                            <input type="text" id="cityapp1Txt" style="width: 65%" />


                                        </td>
                                        <td class="" colspan="2">
                                            <span style="margin-right: 12%">State:</span>
                                            <select id="ddlstateapp" style="width: 74%" class="ddl state"></select>
                                            <%-- <asp:TextBox ID="stateapp1Txt" Width="75%"
                            runat="server" ></asp:TextBox>--%>
                        
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="">
                                            <span style="margin-right: 7%">*Zip code:</span>
                                            <input type="text" id="zipcodeapp1Txt" style="width: 65%" />


                                        </td>
                                        <td style="" colspan="2">
                                            <span style="margin-right: 4%">*Email Address:</span>
                                            <input type="text" id="txtEmailAddress" style="width: 75%" />


                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="">
                                            <span style="margin-right: 0%">Mobile Phone #:</span>
                                            <input type="text" id="txtMobilePhone" style="width: 63%" />


                                        </td>
                                        <td style="" colspan="2">
                                            <span style="margin-right: 4%">Home Phone #:</span>
                                            <input type="text" id="txtHomePhone" style="width: 75%" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="" >
                                            <span style="margin-right: 2%">Relationship:</span>
                                            <select id="ddlUserRelationship" style="width: 29%" class="ddl relation"></select>
                                        </td>
                                        <td style="" colspan="2">
                                            <span style="margin-right: 14%">Other</span>
                                            <input type="text" id="txtOther" style="width: 72%" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <span style="margin-right: 8%">No People living in Unit:</span>
                                            <select id="ddlNoPeopleLiving" style="width: 52%" class="people"></select>
                                        </td>
                                        <td>
                                            <span style="margin-right: 10%">No People under age of 18:</span>
                                            <select id="ddlPeopleUnderAge" style="width: 45%" class="people"></select>
                                        </td>
                                        <td>
                                            <span style="margin-right: 10%">*Birth date:</span>
                                            <input type="text" id="txtBirthday" class="tDate" style="width: 50%" />

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <%-- <div class="row">
                        <div class="col-md-12" style="text-align: center; margin-top: 10px;">
                            <input type="button" class="btn btnNewColor" value="Add only Lease Signers" id="btnAddResidence" />
                        </div>

                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="box" style="border: 1px solid #000000; background-color: #DFE3EE">
        <div class="col-md-12">
            <div class="box box-primary" style="background: none">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label for="" class="col-sm-12 control-label">In Tenant Rental Account: </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Security Deposit: </label>
                            <div class="col-sm-6">
                                <span id="txtSecurityDeposit"></span>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Lease Signed Date: </label>
                            <div class="col-sm-6">
                                <span id="txtLeaseSignerDate"></span>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">One Months Rent: </label>
                            <div class="col-sm-6">
                                <span id="txtOneMonthRent"></span>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Date of monthly payment due: </label>
                            <div class="col-sm-6">
                                <span id="txtDateOfPaymentDue"></span>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Other Amounts held: </label>
                            <div class="col-sm-6">
                                <span id="txtOtherAmountHeld"></span>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="" class="col-sm-6 control-label">Start of Eproperty365 Account: </label>
                            <div class="col-sm-6">
                                <span id="txtStartEpropertyAccount"></span>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>


    <div class="box" style="border: 1px solid #000000; background-color: #DFE3EE">
        <div class="col-md-12">
            <div class="box box-primary" style="background: none">
                <div class="row">
                    <div class="box-header with-border CommonHeader col-md-12" style="margin-top: 0px;">
                        <h3 class="box-title" id="H3" runat="server">Emergency Contacts</h3>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <table style="font-size: 11px; float: left; width: 100%; word-break: break-word;" cellspacing="2" cellpadding="5" border="1" id="tblEmergencyContact">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Address</th>
                                   <%-- <th>Address1</th>--%>
                                    <th>City</th>
                                    <th>State</th>
                                    <th>Zip Code</th>
                                   <%-- <th>Contact Person</th>
                                    <th>Contact Phone Number</th>
                                    <th>Relationship</th>--%>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                    <div class="row">
                        <%--<div class="col-md-12">--%>
                        <label for="" class="col-sm-2 control-label">*Name: </label>
                        <div class="col-sm-10">
                            <input type="text" id="txtEmpName" class="col-md-12 form-control" />
                            <input type="hidden" id="hdRowId" value="0" />
                        </div>
                        <%--</div>--%>
                    </div>
                    <div class="row">
                        <%--  <div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">*Address: </label>
                        <div class="col-sm-4">
                            <textarea id="txtEmpAddress" class="form-control" rows="3" cols="50"></textarea>
                        </div>
                        <%-- </div>--%>
                        <%--  <div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">Address1: </label>
                        <div class="col-sm-4">
                            <textarea id="txtEmpAddress1" class="form-control" rows="3" cols="50"></textarea>
                        </div>
                        <%-- </div>--%>
                    </div>
                    <div class="row">
                        <%-- <div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">*Country: </label>
                        <div class="col-sm-4">
                            <select id="ddlEmpCountry" class="form-control ddl country"></select>
                        </div>
                        <%--  </div>--%>
                        <%-- <div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">Region: </label>
                        <div class="col-sm-4">
                            <input type="text" id="txEmpRegion" class="form-control" />
                        </div>
                        <%--  </div>--%>
                    </div>
                    <div class="row">
                        <%--<div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">*City: </label>
                        <div class="col-sm-4">
                            <input type="text" id="cityEmpTxt" class="form-control" />
                        </div>
                        <%--  </div>--%>
                        <%--<div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">State: </label>
                        <div class="col-sm-4">
                            <select id="ddlEmpstateapp" class="form-control ddl state"></select>
                        </div>
                        <%-- </div>--%>
                    </div>
                    <div class="row">
                        <%-- <div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">*Zip Code: </label>
                        <div class="col-sm-4">
                            <input type="text" id="Empzipcodeapp1Txt" class="form-control" />
                        </div>
                        <%--   </div>--%>
                        <%--   <div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">Relationship: </label>
                        <div class="col-sm-4">
                            <select id="ddlRelationship" class=" form-control ddl relation"></select>
                        </div>
                        <%-- </div>--%>
                    </div>
                    <div class="row">
                        <%-- <div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">Phone: </label>
                        <div class="col-sm-4">
                            <input type="text" id="txtEmpContactPersonPhone" class="form-control" />
                        </div>
                        <%--   </div>--%>
                        <%-- <div class="col-md-6">--%>
                        <label for="" class="col-sm-2 control-label">Email Address: </label>
                        <div class="col-sm-4">
                            <input type="text" id="txtEmailEmergency" class="form-control" />
                        </div>
                        <%-- </div>--%>
                    </div>
                    <div class="row" style="text-align: center; margin-top: 10px">
                        <input type="button" class="col-sm-3 btn btnNewColor" style="margin: 0 auto" value="Add another Contact" id="btnAddanotherContact" />

                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="box" style="border: 1px solid #000000; background-color: #DFE3EE">
        <div class="box box-primary">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="box-title" id="H5" style="font-size: 14px;">Vehicles</h3>
            </div>
            <div class="box-body">
                <div class="col-md-12">
                    <table style="font-size: 11px; float: left; width: 100%; margin-bottom: 20px; background-color: white" cellspacing="2" cellpadding="5" border="1" id="tblVehicle">
                        <thead style="text-align: center">
                            <tr>
                                <th>Make</th>
                                <th>Model</th>
                                <th>Color</th>
                                <th>Year</th>
                                <th>License Plate</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="box" style="border: 1px solid #000000; background-color: #DFE3EE">
        <div class="box box-primary">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="box-title" id="H5" style="font-size: 14px;">People Staying in the Unit</h3>
            </div>
            <div class="box-body">
                <div class="col-md-12">
                    <table style="font-size: 11px; float: left; width: 100%; margin-bottom: 20px; background-color: white" cellspacing="2" cellpadding="5" border="1" id="tblPeople">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Relationship</th>
                                <th>Age</th>

                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="box-footer">
                <div class="col-md-12">
                    <table style="width: 100%; margin-top: 20px; float: left;">
                        <tbody>
                            <tr>
                                <td style="text-align: center; width: 50%;">
                                    <input type="button" class="btn btnNewColor" id="btnIn" style="background-color: #3B5998" value="Cancel" /></td>
                                <td style="width: 50%;">
                                    <input type="button" id="btnSave" class="btn" style="background-color: #66FF00" value="Save" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/TenantAllJs/FirstTimeAddTenant.js"></script>
    <style type="text/css">
        #tblPeople .select2-container {
            width: 100% !important;
        }
    </style>
</asp:Content>
