<%@ Page Title="EProperty365: Residential Tenant rental Application Page 3" Language="C#" MasterPageFile="~/MasterPage/RentalAddMaster.Master" AutoEventWireup="true" CodeBehind="ResidentialTenantRental_App_Page_3.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialTenantRental_App_Page_3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script src="../../AppJs/TenantAllJs/ResidentialTenantAdd_Step2_Page3.js"></script>
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
                    <span>Tenant Rental Application Page 3-4</span>
                </td>
            </tr>

        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DynamicContentBodyPart" runat="server">
    <div style="margin: 5px 20px; width: 93%; float: left;">
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left;">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px; padding: 4px !important; text-align: center;">
                <h3 class="box-title" id="H5" style="font-size: 14px;">General Information</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                <table style="font-size: 12px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1">
                    <tbody>
                        <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px;">*Have you ever been served a late rent notice?</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="late" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="late" value="No" id="Cash" class="flat-red" />
                                        No
                                    </label>
                                </div>
                                <input type="hidden" id="hdRowId" value="0" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px;">*Do any of the people who would be living in property smoke?</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="smoke" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="smoke" value="No" id="Cash" class="flat-red" />
                                        No
                                    </label>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px;">*How long do you think you would be renting from us?</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <select id="ddlRenting"  style="width: 70%;">
                                        <option value="-1">select.......</option>
                                         <option value="1">1</option>
                                         <option value="2">2</option>
                                         <option value="3">3</option>
                                         <option value="4">4</option>

                                    </select>
                                    Year(s)
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px;">*Have you ever filed for bankruptcy?</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="bank" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="bank" value="No" id="Cash" class="flat-red" />
                                        No
                                    </label>
                                </div>

                            </td>
                        </tr>
                         <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px; float: left;">If so when?</span>
                                <div class="form-group" style="width: 22%; float: right;">
                                    <input type="text" value="" id="txtbankruptcy" class="tDate" style="width: 70%"  />
                                </div>

                            </td>
                        </tr>

                        <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px;">*When would you be able to move in?</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <%--<select id="ddlMoveIn">
                                         <option value="-1">select.......</option>
                                         <option value="1">1</option>
                                         <option value="2">2</option>
                                         <option value="3">3</option>
                                         <option value="4">4</option>
                                    </select>--%>
                                    <input type="text" value="" id="txtMoveIn" class="tDate" style="width: 70%"  />
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px;">*Have you ever been convicted of a felony?</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="felony" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="felony" value="No" id="Cash" class="flat-red" />
                                        No
                                    </label>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px;">*Have you ever been served an eviction notice?</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="eviction" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="eviction" value="No" id="Cash" class="flat-red" />
                                        No
                                    </label>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px; float: left;">If so when?</span>
                                <div class="form-group" style="width: 22%; float: right;">
                                    <input type="text" value="" id="txtwhen" class="tDate" style="width: 70%"  />
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="width: 100%; float: left">*How many pets do you have (type, breed, approx Weight & age)?</span>
                                <div class="form-group" style="width: 100%; float: left">
                                    <textarea id="txtPetsDetails" rows="7" cols="80"></textarea>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="margin-right: 1%;margin-left: 10px;width: 73%;float: left;">*Have you had any reoccurring problems with your current place or land load? If yes, please explain</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="reoccurring" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="reoccurring" value="No" id="Cash" class="flat-red" />
                                        No
                                    </label>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="form-group" style="width: 100%; float: left">
                                    <textarea id="txtreoccurring" rows="7" cols="80"></textarea>
                                </div>

                            </td>
                        </tr>
                         <tr>
                            <td>
                                <span style="width: 100%; float: left">*Why are you moving from your current address?</span>
                                <div class="form-group" style="width: 100%; float: left">
                                    <textarea id="txtMovingReason" rows="7" cols="80"></textarea>
                                </div>

                            </td>
                        </tr>
                          <tr>
                            <td>
                                <span style="width: 100%; float: left">List any verifiable source and amounts of income you wish to have considered (optional):</span>
                                <div class="form-group" style="width: 100%; float: left">
                                    <textarea id="txtAmountOfIncome" rows="7" cols="80"></textarea>
                                </div>

                            </td>
                        </tr>
                         <tr>
                            <td>
                                <span style="width: 100%; float: left">If you were to run into financial difficulty in the future and couldn’t come up with the money to pay the rent, do you know someone that would
loan you the money? If so, provide the person’s name, address, & phone # so that we can use them as a reference for you.</span>
                                <div class="form-group" style="width: 100%; float: left">
                                    <textarea id="txtFinancialProblem" rows="7" cols="80"></textarea>
                                </div>

                            </td>
                        </tr>
                         <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px;">Have you been a party to a lawsuit in the past? If yes, please explain why:</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="lawsuit" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="lawsuit" value="No" id="Cash" class="flat-red" />
                                        No
                                    </label>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="form-group" style="width: 100%; float: left">
                                    <textarea id="txtlawsuit" rows="7" cols="80"></textarea>
                                </div>

                            </td>
                        </tr>
                         <tr>
                            <td>
                                <span style="margin-right: 1%; margin-left: 10px; float: left;width: 70%">We may run a credit check and a criminal background check. Is there anything negative we will find that you want to comment on?</span>
                                <div class="form-group" style="width: 22%; float: right">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="creditcheck" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="creditcheck" value="No" id="Cash" class="flat-red" />
                                        No
                                    </label>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="form-group" style="width: 100%; float: left">
                                    <textarea id="txtcreditcheck" rows="7" cols="80"></textarea>
                                </div>

                            </td>
                        </tr>
                          <tr>
                            <td>
                                <span style="margin-right: 27%; margin-left: 10px; float: left;">How did you hear about this apartment?</span>
                                <div class="form-group" style="width: 22%; float: left;">
                                    <input type="text" value="" id="txtthisApartment" class="flat-red" />
                                </div>

                            </td>
                        </tr>
                         <tr>
                            <td>
                                <span style="width: 100%; float: left">Do you know of anybody else looking for an apartment? Please provide their name and number. If you refer a friend and you each end up renting
separate apartments from us then we will pay you a referral reward.</span>
                                <div class="form-group" style="width: 100%; float: left">
                                    <textarea id="txtOtherPeopleForAppartment" rows="7" cols="80"></textarea>
                                </div>

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <table style="width: 100%; margin-top: 20px; float: left;">
            <tbody>
                <tr>
                    <td style="width: 30%;">
                        <input type="hidden" id="hdnShow"  value="<%=isView%>" />
                        <input type="button" class="btn btnNewColor" id="btnExit" visible="<%=isVisible%>"  style="background-color: #3B5998; float: left; margin-right:20px;" value="Exit" /></td>
                    <td style="text-align: center; width: 30%;">
                        <input type="button" class="btn btnNewColor" id="btnBack" style="background-color: #3B5998" value="< Back" /></td>
                    <td style="width: 40%;">
                        <input type="button" class="btn" style="background-color: #66FF00" id="btnContinue" value="Continue" /></td>
                </tr>
            </tbody>
        </table>


    </div>
</asp:Content>
