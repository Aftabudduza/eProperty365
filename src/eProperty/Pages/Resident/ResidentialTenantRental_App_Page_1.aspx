<%@ Page Title="EProperty365: Residential Tenant rental Application Page 1" Language="C#" MasterPageFile="~/MasterPage/RentalAddMaster.Master" AutoEventWireup="true" CodeBehind="ResidentialTenantRental_App_Page_1.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialTenantRental_App_Page_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://unpkg.com/axios/dist/axios.min.js"></script>
    <script src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script src="../../AppJs/TenantAllJs/ResidentialTenantAddRentalAPPPage1.js"></script>
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
                    <span>Tenant Rental Application Page 1-4</span>
                </td>
            </tr>

        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DynamicContentBodyPart" runat="server">
    <div style="margin: 5px 10px; width: 97%; float: left;">
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
                        <input type="text" id="txtMiddleName" style="width: 69%" />

                    </td>
                    <td style="">
                        <span style="margin-right: 5%;">*Last:</span>
                        <input type="text" id="txtLastName" style="width: 73%" />

                    </td>
                </tr>
                <tr>
                    <td style="" colspan="3">
                        <span style="margin-right: 11%">Alias or nick name used:</span>
                        <input type="text" id="txtAliasName" style="width: 65%" />


                    </td>
                </tr>
                <tr>
                    <td style="" colspan="3">
                        <span style="margin-right: 4%">*Address:</span>
                        <input type="text" id="txtAddress" style="width: 85%" />


                    </td>
                </tr>
                <tr>
                    <td style="" colspan="3">
                        <span style="margin-right: 4%">Address1:</span>
                        <input type="text" id="txtAddress1" style="width: 85%" />


                    </td>
                </tr>
                <tr>
                    <td class="">
                        <span style="margin-right: 11%">*Country:</span>
                        <select id="ddlCountry" style="width: 64%" class="ddl22 country"></select>

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
                        <span style="margin-right: 19%">*City:</span>
                        <%-- <select id="ddlcityapp" style="width: 75%" class="ddl22"></select>--%>
                        <input type="text" id="cityapp1Txt" style="width: 65%" />


                    </td>
                    <td class="" colspan="2">
                        <span style="margin-right: 14%">State:</span>
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
                        <span>*Email Address:</span>
                        <input type="text" id="txtEmailAddress" style="width: 71%" />


                    </td>

                </tr>
                <tr>
                    <td style="">
                        <span style="margin-right: 0%">Mobile Phone #:</span>
                        <input type="text" id="txtMobilePhone" style="width: 58%" />


                    </td>
                    <td style="" colspan="2">
                        <span>Home Phone #:</span>
                        <input type="text" id="txtHomePhone" style="width: 72%" />


                    </td>

                </tr>
                <tr>
                    <td style="">
                        <span style="margin-right: 1%">*Drivers License No:</span>
                        <input type="text" id="txtDriverLicense" style="width: 47%" />


                    </td>
                    <td style="" colspan="2">
                        <span style="margin-right: 4%">*License State:</span>
                        <select id="ddlLicenceState" style="width: 69%" class="ddl state"></select>


                    </td>

                </tr>
                <tr>
                    <td style="">
                        <span style="margin-right: 1%">*Social Security #</span>
                        <input type="text" id="txtSocial" style="width: 55%" />


                    </td>
                    <td style="" colspan="2">
                        <span style="margin-right: 4%">Relationship:</span>
                        <select id="ddlRelationship" style="width: 71%" class="relation"></select>


                    </td>

                </tr>
                <tr>
                    <td style="">
                        <span style="margin-right: 8%">Other</span>
                        <input type="text" id="txtOther" style="width: 75%" />


                    </td>
                    <td style="" colspan="2">
                        <span style="margin-right: 10%">*Birth date:</span>
                        <input type="text" id="txtBirthday" class="tDate" style="width: 70%" />

                    </td>
                </tr>

            </tbody>
        </table>

        <div style="width: 100%; background-color: #F7F7F7; margin-top: 20px; border: 1px solid black; float: left">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px;">
                <h3 class="box-title" id="H5">Residence History</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                 <table style="font-size: 11px; float: left; width: 100%;word-break: break-word;" cellspacing="2" cellpadding="5" border="1" id="tblResidenceHistory">
                <thead>
                    <tr>
                        <%--<th>Name</th>--%>
                        <th>Address</th>
                       <%-- <th>Address1</th>--%>
                        <th>City</th>
                        <th>State</th>
                        <th>Zip Code</th>
                       <%-- <th>Owner/Manager Name</th>
                        <th>Owner/Manager Phone Number</th>
                        <th>Monthly Income</th>--%>
                       <th>Action</th>
                        <%--<th>Delete</th>--%>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
            </div>
            <table cellspacing="3" cellpadding="5" style="margin-top: 10px; width: 100%;float: left">
            <tbody style="padding: 5px;">
                
               
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 14%">*Address:</span>
                        <input type="text" id="txtResAddress" style="width: 75%" />
                        <input type="hidden" id="hdResidentId" value="0" />

                    </td>
                </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 13%">Address1:</span>
                        <input type="text" id="txtResAddress1" style="width: 75%" />


                    </td>
                </tr>
                <tr>
                    <td class="">
                        <span style="margin-right: 11%">*Country:</span>
                        <select id="ddlresCountry" style="width: 63%" class="ddl22 country"></select>

                    </td>
                    <td class="" >
                        <span style="margin-right: 10%">Region:</span>
                        <input type="text" id="txRestRegion" style="width: 70%" />
                        <%-- <asp:TextBox ID="stateapp1Txt" Width="75%"
                            runat="server" ></asp:TextBox>--%>
                        
                    </td>

                </tr>
                <tr>
                    <td class="">
                        <span style="margin-right: 18%">*City:</span>
                        <%-- <select id="ddlcityapp" style="width: 75%" class="ddl22"></select>--%>
                        <input type="text" id="cityResTxt" style="width: 63%" />


                    </td>
                    <td class="" >
                        <span style="margin-right: 14%">State:</span>
                        <select id="ddlResstateapp" style="width: 71%" class="ddl state"></select>
                        <%-- <asp:TextBox ID="stateapp1Txt" Width="75%"
                            runat="server" ></asp:TextBox>--%>
                        
                    </td>

                </tr>
                <tr>
                    <td style="">
                        <span style="margin-right: 9%">*Zip code:</span>
                        <input type="text" id="Reszipcodeapp1Txt" style="width: 64%" />


                    </td>
                    <td style="">
                        <span>*Monthly Income:</span>
                        <input type="text" id="txtmonthlyAmount" style="width: 62%" />
                    </td>

                </tr>
                 <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 14%">*Owner/Manager Name:</span>
                        <input type="text" id="txtOwnerManagerName" style="width: 61%" />


                    </td>
                </tr>
                 <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 13%">*Owner/Manager Phone:</span>
                        <input type="text" id="txtOwnerManagerPhone" style="width: 61%" />


                    </td>
                </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 0%">*Reason For Leaving:</span>
                    </td>
                </tr>
                 <tr>
                    <td style="" colspan="2">
                        <textarea id="txtReasonForLeaving" style="width: 97%;margin-left: 10px" rows="10" cols="10"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 1%; margin-left: 10px;">*Is/Was rent Paid in Full?</span>
                         <div class="form-group" style="width: 22%;float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Paid" name="rentPaid" class="flat-red" value="Yes" checked="checked" />
                                     Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="rentPaid" value="No" id="Cash" class="flat-red" />
                                       No
                                    </label>
                                </div>


                    </td>
                    
                     
                     
                   

                </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 1%; margin-left: 10px;">*Did you give notice?</span>
                         <div class="form-group" style="width: 22%;float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="notice" name="didGiveNotice" class="flat-red" value="Yes" checked="checked" />
                                     Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="didGiveNotice" value="No" id="Cash" class="flat-red" />
                                       No
                                    </label>
                                </div>


                    </td>
                </tr>
                    <tr>
                        <td style="" colspan="2">
                        <span style="margin-right: 1%; margin-left: 10px;">*Were you asked to move?</span>
                         <div class="form-group" style="width: 22%;float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="move" name="askToMove" class="flat-red" value="Yes" checked="checked" />
                                     Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="askToMove" value="No" id="CreditNo" class="flat-red" />
                                       No
                                    </label>
                                </div>


                    </td>
                    </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-left: 10px">*Name(s) in which your utilities are now billed:</span>
                    </td>
                </tr>
                 <tr>
                    <td style="" colspan="2">
                        <textarea id="txtbilled" style="width: 97%;margin-left: 10px" rows="10" cols="10"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="" >
                         <span style="margin-right: 14%">*Lived From Date:</span>
                        <input type="text" id="txtLivedFrom" class="tDate" style="width: 48%" />
                    </td>
                     <td style="">
                       
                          <span style="margin-right: 14%">*Lived To:</span>
                        <input type="text" id="txtLivedTo" class="tDate" style="width: 61%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                        <input type="button" class="btn btnNewColor"  value="Add another Residence" id="btnAddResidence"/>
                    </td>
                </tr>
            </tbody>
        </table>
        </div>
        <div style="width: 100%; background-color: #F7F7F7; margin-top: 20px; border: 1px solid black; float: left">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px;">
                <h3 class="box-title" id="H5">Employer Information</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                <div style="float: left;width: 100%;margin-bottom: 10px;text-align: center;font-weight: bold;">You must report where worked for last three years</div>
                 <table style="font-size: 11px; float: left;width: 100%;word-break: break-word;" cellspacing="2" cellpadding="5" border="1" id="tblEmpInformation">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Address</th>
                       <%-- <th>Address1</th>--%>
                        <th>City</th>
                        <th>State</th>
                        <th>Zip Code</th>
                       <%-- <th>Contact Person</th>
                        <th>Contact Phone Number</th>--%>
                        <th>Monthly Income</th>
                       <th>Action</th>
                       <%-- <th>Delete</th>--%>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
            </div>
            <table cellspacing="3" cellpadding="5" style="margin-top: 10px; width: 100%;float: left">
            <tbody style="padding: 5px;">
                
                 <tr>
                    <td style="">
                        <span style="margin-right: 14%">*Name:</span>
                        <input type="text" id="txtEmpName" style="width: 70%" />
                        <input type="hidden" id="hdEmpId" value="0" />


                    </td>
                       <td style="">
                        <div class="form-group" style=" float: left;margin-left: 30%;">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="FullTime" name="JobType" class="flat-red" value="Yes" checked="checked" />
                                     Full Time
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="JobType" value="No" id="PartTime" class="flat-red" />
                                       Part Time
                                    </label>
                                </div>


                    </td>
                </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 5%">*Address:</span>
                        <input type="text" id="txtEmpAddress" style="width: 82%" />


                    </td>
                </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 5%">Address1:</span>
                        <input type="text" id="txtEmpAddress1" style="width: 82%" />


                    </td>
                </tr>
                <tr>
                    <td class="">
                        <span style="margin-right: 11%">*Country:</span>
                        <select id="ddlEmpCountry" style="width: 63%" class="ddl22 country"></select>

                    </td>
                    <td class="" >
                        <span style="margin-right: 10%">Region:</span>
                        <input type="text" id="txEmpRegion" style="width: 68%" />
                        <%-- <asp:TextBox ID="stateapp1Txt" Width="75%"
                            runat="server" ></asp:TextBox>--%>
                        
                    </td>

                </tr>
                <tr>
                    <td class="">
                        <span style="margin-right: 18%">*City:</span>
                        <%-- <select id="ddlcityapp" style="width: 75%" class="ddl22"></select>--%>
                        <input type="text" id="cityEmpTxt" style="width: 63%" />


                    </td>
                    <td class="" >
                        <span style="margin-right: 14%">State:</span>
                        <select id="ddlEmpstateapp" style="width: 68%" class="ddl state"></select>
                        <%-- <asp:TextBox ID="stateapp1Txt" Width="75%"
                            runat="server" ></asp:TextBox>--%>
                        
                    </td>

                </tr>
                <tr>
                    <td style="">
                        <span style="margin-right: 9%">*Zip code:</span>
                        <input type="text" id="Empzipcodeapp1Txt" style="width: 64%" />


                    </td>
                    <td style="">
                        <span>*Monthly Income:</span>
                        <input type="text" id="txtEmpmonthlyAmount" style="width: 62%" />
                    </td>

                </tr>
                 <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 14%">*Contact Person Name:</span>
                        <input type="text" id="txtEmpContactPersonName" style="width: 61%" />


                    </td>
                </tr>
                 <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 13%">*Contact Person Phone:</span>
                        <input type="text" id="txtEmpContactPersonPhone" style="width: 61%" />


                    </td>
                </tr>
               
               
                   
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-left: 10px">You may have supply proof of income</span>
                    </td>
                </tr>
                <tr>
                    <td style="" colspan="2">
                      <span style="margin-right: 2%">*Employment From Date:</span>
                        <input type="text" id="txtEmpFromDate" class="tDate" style="width: 50%" />
                         
                    </td>
                </tr>
                 <tr>
                    <td style="" colspan="2">
                      <span style="margin-right: 2%">*Employment From To:</span>
                        <input type="text" id="txtEmpFromTo" class="tDate" style="width: 50%" />
                         <div class="form-group" style=" float: right;">
                                    <label style="margin-right: 7px">
                                        <input type="checkbox" id="StillWork" class="chkfStillWork" name="Work"  value="FullTime"  />
                                     Still Work There
                                    </label>
                                    
                                </div>
                    </td>
                </tr>
               
                <tr>
                    <td style="text-align: center" colspan="3">
                        <input type="button" class="btn btnNewColor"  value="Add another Employer" id="btnAddEmployee"/>
                        <span style="float: left;width: 100%">You must report where worked for last three years.</span>
                    </td>
                </tr>
            </tbody>
        </table>
        </div>
        
        <div style="width: 100%; background-color: #F7F7F7; margin-top: 20px; border: 1px solid black; float: left">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px;">
                <h3 class="box-title" id="H5">References</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                 <table style="font-size: 11px; float: left;width: 100%;word-break: break-word;" cellspacing="2" cellpadding="5" border="1" id="tblReferenceList">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Address</th>
                       <%-- <th>Address1</th>--%>
                        <th>City</th>
                        <th>State</th>
                        <th>Zip Code</th>
                        <th>Person Phone</th>
                       <%-- <th>Monthly Income</th>--%>
                        <th>Relationship</th>
                        <th>Action</th>
                       <%-- <th>Delete</th>--%>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
            </div>
            <table cellspacing="3" cellpadding="5" style="margin-top: 10px; width: 100%;float: left">
            <tbody style="padding: 5px;">
                
                 <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 16%">*Name:</span>
                        <input type="text" id="txtRefName" style="width: 75%" />
                        <input type="hidden" id="hdReferenceId" value="0"/>


                    </td>
                       
                </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 14%">*Address:</span>
                        <input type="text" id="txtRefAddress" style="width: 75%" />


                    </td>
                </tr>
                <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 13%">Address1:</span>
                        <input type="text" id="txtRefAddress1" style="width: 75%" />


                    </td>
                </tr>
                <tr>
                    <td class="">
                        <span style="margin-right: 11%">*Country:</span>
                        <select id="ddlRefCountry" style="width: 63%" class="ddl22 country"></select>

                    </td>
                    <td class="" >
                        <span style="margin-right: 10%">Region:</span>
                        <input type="text" id="txRefRegion" style="width: 72%" />
                        <%-- <asp:TextBox ID="stateapp1Txt" Width="75%"
                            runat="server" ></asp:TextBox>--%>
                        
                    </td>

                </tr>
                <tr>
                    <td class="">
                        <span style="margin-right: 18%">*City:</span>
                        <%-- <select id="ddlcityapp" style="width: 75%" class="ddl22"></select>--%>
                        <input type="text" id="cityRefTxt" style="width: 63%" />


                    </td>
                    <td class="" >
                        <span style="margin-right: 14%">State:</span>
                        <select id="ddlRefstateapp" style="width: 71%" class="ddl state"></select>
                        <%-- <asp:TextBox ID="stateapp1Txt" Width="75%"
                            runat="server" ></asp:TextBox>--%>
                        
                    </td>

                </tr>
                <tr>
                    <td style="">
                        <span style="margin-right: 9%">*Zip code:</span>
                        <input type="text" id="Refzipcodeapp1Txt" style="width: 64%" />


                    </td>
                    <td style="display:none;">
                        <span>*Monthly Income:</span>
                        <input type="text" id="txtRefmonthlyAmount" style="width: 62%" />
                    </td>

                </tr>
                 <tr>
                    <td style="" colspan="2">
                        <span style="margin-right: 0%">*Relationship:</span>
                        <select id="ddlrefRelationship" style="width: 30%" class="relation"></select>
                        <%--<input type="text" id="txtrefRelationship" style="width: 61%" />--%>


                    </td>
                </tr>
                 <tr>
                    <td style="">
                        <span style="margin-right: 12%">*Phone:</span>
                        <input type="text" id="txtrefPersonPhone" style="width: 60%" />


                    </td>
                     <td style="">
                        <span style="margin-right: 3%">*Email Address:</span>
                        <input type="text" id="txtRefEmailAddress" style="width: 61%" />


                    </td>
                </tr>
               
                <tr>
                    <td style="text-align: center" colspan="2">
                        <span style="float: left; width: 100%; margin-bottom: 10px;">You must have 3 References.</span>
                        <input type="button" class="btn btnNewColor"  value="Add another Reference" id="btnAddReference"/>
                        
                    </td>
                </tr>
            </tbody>
        </table>
        </div>

        <table style="width: 100%; margin-top: 20px; float: left;">
            <tbody>
                <tr>
                      <td style="width: 30%;">
                           <input type="hidden" id="hdnShow"  value="<%=isView%>" />
                        <input type="button" class="btn btnNewColor" id="btnExit" style="background-color: #3B5998; float: left; margin-right:20px;" value="Exit" /></td>
                    <td style="text-align: center; width: 30%;">
                        <input type="button" class="btn btnNewColor" style="background-color: #3B5998" id="btnBack" value="< Back" /></td>
                    <td style="width: 40%;">
                        <input type="button" class="btn" style="background-color: #66FF00" value="Continue" id="btnContinue" /></td>
                </tr>
            </tbody>
        </table>


    </div>
</asp:Content>
