<%@ Page Title="EProperty365: Residential Tenant rental Application Page 2" Language="C#" MasterPageFile="~/MasterPage/RentalAddMaster.Master" AutoEventWireup="true" CodeBehind="ResidentialTenantRental_App_Page_2.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialTenantRental_App_Page_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script src="../../AppJs/TenantAllJs/ResidentialTenantAdd_Step2_Page2.js"></script>
    <style type="text/css">
        .rel {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DynamicContentHeaderPart" runat="server">
    <table style="width: 100%" border="0">
        <tbody>
            <tr>
                <td><span style="width: 25%; float: left; margin-left: 5%;">User Name :</span><span style="float: left; width: 60%; margin-left: 10%;" id="userName"></span></td>
            </tr>
            <tr>

                <td style="width: 100%; text-align: center" class="auto-style5">
                    <span class="auto-style6">Step
                            2:</span><b>
                            </b>
                </td>

            </tr>
            <tr>
                <td style="background-color: #3B5998; text-align: center; color: white; line-height: 25px; font-family: Arial; font-weight: bold; font-size: 13px; font-style: normal;">
                    <span>Tenant Rental Application Page 2-4</span>
                </td>
            </tr>

        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DynamicContentBodyPart" runat="server">
    <div style="margin: 5px 20px; width: 93%; float: left;">
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; text-align: center;">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="box-title" id="H5" style="font-size: 14px;">Credit History Information</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1">
                    <thead>
                        <tr>
                            <th>Description</th>
                            <th>Bank/Institution Name</th>
                            <th>Balance on Deposit or Balance Owed</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Saving Account</td>
                            <td>
                                <input type="text" id="txtsavingBank1" class="from-control" /><input type="hidden" id="hdsavingId" value="0" /></td>
                            <td>
                                <input type="text" id="txtsavingBalance1" class="from-control" /></td>

                        </tr>
                        <tr>
                            <td>Saving Account</td>
                            <td>
                                <input type="text" id="txtsavingBank2" class="from-control" /><input type="hidden" id="hdsavingId2" value="0" /></td>
                            <td>
                                <input type="text" id="txtsavingBalance2" class="from-control" /></td>
                        </tr>
                        <tr>
                            <td>Checking Account</td>
                            <td>
                                <input type="text" id="txtCheckingBank1" class="from-control" /><input type="hidden" id="hdCheckId" value="0" /></td>
                            <td>
                                <input type="text" id="txtCheckingBalance1" class="from-control" /></td>
                        </tr>
                        <tr>
                            <td>Checking Account</td>
                            <td>
                                <input type="text" id="txtCheckingBank2" class="from-control" /><input type="hidden" id="hdCheckId2" value="0" /></td>
                            <td>
                                <input type="text" id="txtCheckingBalance2" class="from-control" /></td>
                        </tr>
                        <tr>
                            <td>All Credit cards</td>
                            <td>
                                <input type="text" id="txtAllCreditBank" class="from-control" /><input type="hidden" id="hdAllCredit" value="0" /></td>
                            <td>
                                <input type="text" id="txtAllCreditBalance" class="from-control" /></td>
                        </tr>
                        <tr>
                            <td>Auto Loan</td>
                            <td>
                                <input type="text" id="txtAutoLoanBank" class="from-control" /><input type="hidden" id="hdAutoLoan" value="0" /></td>
                            <td>
                                <input type="text" id="txtAutoLoanBalance" class="from-control" /></td>
                        </tr>
                        <tr>
                            <td>Other</td>
                            <td>
                                <input type="text" id="txtOthrBank" class="from-control" /><input type="hidden" id="hdOther" value="0" /></td>
                            <td>
                                <input type="text" id="txtotherBalance" class="from-control" /></td>
                        </tr>

                    </tbody>
                </table>
            </div>
        </div>
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left;">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="box-title" id="H5" style="font-size: 14px;">Emergency Contacts</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                <table style="font-size: 11px; float: left; width: 100%; word-break: break-word;" cellspacing="2" cellpadding="5" border="1" id="tblEmergencyContact">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Address</th>
                          <%--  <th>Address1</th>--%>
                            <th>City</th>
                            <th>State</th>
                            <th>Zip Code</th>
                            <%--<th>Contact Person</th>
                            <th>Contact Phone Number</th>
                            <th>Relationship</th>--%>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
            <table cellspacing="3" cellpadding="5" style="margin-top: 10px; width: 100%; float: left">
                <tbody style="padding: 5px;">

                    <tr>
                        <td colspan="2">
                            <span style="margin-right: 7%">*Name:</span>
                            <input type="text" id="txtEmpName" style="width: 84%" />
                            <input id="hdRowId" value="0" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td style="" colspan="2">
                            <span style="margin-right: 5%">*Address:</span>
                            <input type="text" id="txtEmpAddress" style="width: 84%" />
                        </td>
                    </tr>
                    <tr>
                        <td style="" colspan="2">
                            <span style="margin-right: 5%">Address1:</span>
                            <input type="text" id="txtEmpAddress1" style="width: 84%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="">
                            <span style="margin-right: 11%">*Country:</span>
                            <select id="ddlEmpCountry" style="width: 63%" class="country"></select>

                        </td>
                        <td class="">
                            <span style="margin-right: 10%">Region:</span>
                            <input type="text" id="txEmpRegion" style="width: 72%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="">
                            <span style="margin-right: 18%">*City:</span>
                            <input type="text" id="cityEmpTxt" style="width: 63%" />


                        </td>
                        <td class="">
                            <span style="margin-right: 14%">State:</span>
                            <select id="ddlEmpstateapp" style="width: 71%" class="state"></select>
                        </td>

                    </tr>
                    <tr>
                        <td style="">
                            <span style="margin-right: 9%">*Zip code:</span>
                            <input type="text" id="Empzipcodeapp1Txt" style="width: 64%" />


                        </td>
                        <td style="">
                            <span style="margin-right: 2%">Relationship:</span>
                            <%--<input type="text" id="txtrefRelationship" style="/*width: 61%*/" />--%>
                            <select id="ddlRelationship" style="width: 68%" class="relation"></select>
                            <%-- <span>*Monthly Income:</span>
                        <input type="text" id="txtEmpmonthlyAmount" style="width: 65%" />--%>
                        </td>

                    </tr>
                    <%--  <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 14%">*Contact Person Name:</span>
                        <input type="text" id="txtEmpContactPersonName" style="width: 61%" />


                    </td>
                </tr>--%>


                   
                    <tr>
                        <td style="" colspan="2">
                            <span style="margin-right: 5%">Phone No:</span>
                            <input type="text" id="txtEmpContactPersonPhone" style="width: 82%" />


                        </td>
                    </tr>
                    <tr>
                        <td style="" colspan="2">
                            <span style="margin-right: 0%">Email Address:</span>
                            <input type="text" id="txtRefEmailAddress" style="width: 82%" />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: center" colspan="2">
                            <%--<span style="float: left; width: 100%; margin-bottom: 10px;">You must have 3 References.</span>--%>
                            <input type="button" class="btn btnNewColor" value="Add another Contact" id="btnAddAnotherContact" />

                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left;">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="box-title" id="H5" style="font-size: 14px;">Vehicles</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
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
                        <%-- <tr>
                    <td style="width: 50px"><input style="width: 90px" id="txtMake1" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtModel1" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtColor1" /></td>
                     <td  style="width: 50px"><input style="width: 90px" id="txtYear1" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtLicensePlate1" /></td>
                </tr>
                     <tr>
                    <td  style="width: 50px"><input style="width: 90px" id="txtMake2" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtModel2" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtColor2" /></td>
                          <td  style="width: 50px"><input style="width: 90px" id="txtYear2" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtLicensePlate2" /></td>
                          
                </tr>
                     <tr>
                    <td  style="width: 50px"><input style="width: 90px" id="txtMake3" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtModel3" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtColor3" /></td>
                          <td  style="width: 50px"><input style="width: 90px" id="txtYear3" /></td>
                    <td  style="width: 50px"><input style="width: 90px" id="txtLicensePlate3" /></td>
                         
                </tr>--%>
                    </tbody>
                </table>
            </div>
        </div>
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; text-align: center;">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="box-title" id="H5" style="font-size: 14px;">People Staying in the Unit</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                <table style="font-size: 11px; float: left; width: 100%; margin-bottom: 20px; background-color: white" cellspacing="2" cellpadding="5" border="1" id="tblPeople">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Relationship</th>
                            <th>Age</th>

                        </tr>
                    </thead>
                    <tbody>
                        <%-- <tr>
                    <td><input type="text" id="txtName1" class="from-control"/></td>
                    <td><input type="text" id="txtRelationship1" class="from-control"/></td>
                    <td><input type="text" id="txtAge1" class="from-control"/></td>
                    
                </tr>
                     <tr>
                    <td><input type="text" id="txtName2" class="from-control"/></td>
                    <td><input type="text" id="txtRelationship2" class="from-control"/></td>
                    <td><input type="text" id="txtAge2" class="from-control"/></td>
                         
                </tr>
                     <tr>
                    <td><input type="text" id="txtName3" class="from-control"/></td>
                    <td><input type="text" id="txtRelationship3" class="from-control"/></td>
                    <td><input type="text" id="txtAge3" class="from-control"/></td>
                         
                </tr>
                     <tr>
                    <td><input type="text" id="txtName4" class="from-control"/></td>
                    <td><input type="text" id="txtRelationship4" class="from-control"/></td>
                    <td><input type="text" id="txtAge4" class="from-control"/></td>
                         
                </tr>
                     <tr>
                    <td><input type="text" id="txtName5" class="from-control"/></td>
                    <td><input type="text" id="txtRelationship5" class="from-control"/></td>
                    <td><input type="text" id="txtAge5" class="from-control"/></td>
                          
                </tr>
                     <tr>
                    <td><input type="text" id="txtName6" class="from-control"/></td>
                    <td><input type="text" id="txtRelationship6" class="from-control"/></td>
                    <td><input type="text" id="txtAge6" class="from-control"/></td>--%>
                    </tbody>
                </table>
            </div>
        </div>


        <table style="width: 100%; margin-top: 20px; float: left;">
            <tbody>
                <tr>
                    <td style="width: 30%;">
                        <input type="hidden" id="hdnShow"  value="<%=isView%>" />
                        <input type="button" class="btn btnNewColor" id="btnExit" style="background-color: #3B5998; float: left; margin-right:20px;" value="Exit" /></td>
                    <td style="text-align: center; width: 30%;">
                        <input type="button" class="btn btnNewColor" id="btnBack" style="background-color: #3B5998" value="< Back" /></td>
                    <td style="width: 40%;">
                        <input type="button" id="btnContinue" class="btn" style="background-color: #66FF00" value="Continue" /></td>
                </tr>
            </tbody>
        </table>


    </div>
</asp:Content>
