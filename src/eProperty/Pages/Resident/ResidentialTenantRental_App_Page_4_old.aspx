<%@ Page Title="EProperty365: Residential Tenant rental Application Page 4" Language="C#" MasterPageFile="~/MasterPage/RentalAddMaster.Master" AutoEventWireup="true" CodeBehind="ResidentialTenantRental_App_Page_4_old.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialTenantRental_App_Page_4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Content/js/pdfobject.js"></script>
    <script src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script src="../../AppJs/TenantAllJs/ResidentialTenantAdd_Step2_Page4.js"></script>
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
                    <span>Tenant Rental Application Page 4-4</span>
                </td>
            </tr>

        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DynamicContentBodyPart" runat="server">
    <div style="margin: 5px 20px; width: 93%; float: left;">
        <div style="width: 100%; background-color: #F7F7F7; margin-top: 20px; border: 1px solid black; float: left;">
            <div class="col-md-12" style="float: left; margin-top: 0px; padding: 4px !important; text-align: center; font-weight: bold; font-size: 14px;">
                <span>Verify Income</span>
                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblVerityDoc">
                    <thead>
                        <tr>
                            <th>Document Description</th>
                            <th>Document Name</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1">
                    <tbody>
                        <tr>
                            <td style="width: 42%"><span style="width: 100%; float: left">Enter Document Description</span>
                                <span style="width: 100%; float: left;">
                                    <input type="text" id="txtsavingBankDoc" class="from-control" /></span>
                            </td>
                            <td>
                                <span>
                                    <input type="file" id="fileImageUpload" /></span>
                                <span>
                                    <input type="button" class="btn btnNewColor" value="Upload" onclick="SaveVerityIncome();" /></span>

                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">Enter Description Browse your computer for bank statement file or types of file the can prove your income. Be sure name and date shows.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; text-align: center;">
            <div class="box-header with-border CommonHeader col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="box-title" id="H5" style="font-size: 14px;">Tenant Additional Documents</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">

                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblAdditionalDoc">
                    <thead>
                        <tr>
                            <th>Document Description</th>
                            <th>Document Name</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>


                <%-- <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblDoc">
                    <thead style="font-size: 13px; font-weight: bold">
                        <tr>
                            <th></th>
                            <th>Documents</th>
                            <th>Status</th>
                            <th>Action</th>
                            <th>Current Status</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>

                </table>--%>
                <div style="width: 100%">

                    <div style="float: left; width: 100%; padding: 0px 10px;">
                        <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1">
                            <tbody>
                                <tr>
                                    <td style="width: 42%"><span style="width: 100%; float: left">Enter Document Description</span>
                                        <span style="width: 100%; float: left;">
                                            <input type="text" id="txtAddiDoc" class="from-control" /></span>
                                    </td>
                                    <td>
                                        <span>
                                            <input type="file" id="documentUpload" /></span>
                                        <span>
                                            <input id="savedoc" type="button" class="btn btnNewColor" value="Upload" onclick="SavedocumentUpload();" /></span>

                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2">Enter Description Browse your computer for additional documents file.</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <%-- <span>
                        <input type="file" disabled="disabled" id="documentUpload" /></span>
                    <span>
                        <input id="savedoc" type="button" disabled="disabled" class="btn btnNewColor" value="Upload" onclick="SavedocumentUpload();" /></span>--%>
                </div>
                <%--<div id="iframedis" class="col-md-12" style="display: none">
                    <object id="objid" data="" type="application/pdf">
                        <iframe src="" id="viewfile" style="width: 618px; height: 800px"></iframe>
                    </object>

                </div>--%>
                <%-- <div id="iframedis" class="col-md-12" style="display: none">
                    <object id="objid" data="" type="application/pdf">
                        <iframe src="" id="viewfile" style="width: 618px; height: 800px"></iframe>
                    </object>
                </div>
                <div id="iframeimage" class="col-md-12" style="display: none">
                    <img src="" id="ifrmImage" width="400" height="400" />
                </div>

                <div style="width: 100%">
                    <textarea id="txtviewed" rows="20" cols="80"></textarea>
                    <span style="width: 100%; float: left; margin-top: 20px">
                        <input type="button" class="btn btnNewColor" id="btnviewed" value="Viewed" /></span>
                   <%-- <span style="width: 100%; float: left; margin-top: 10px; color: red">All Documents Must Be Viewed</span>-
                </div>--%>
                <div style="width: 100%; float: left; text-align: justify; border: 1px solid #000000; padding: 8px;">
                    <p>
                        I/we, the undersigned, authorize Eproperty365.com. Landlords and its agents to obtain an investigative consumer credit report including but not limited to credit history, OFAC search, landlord/tenant court record search, criminal record search and registered sex offender search. I authorize their lease of information from previous or current landlords, employers, and bank representatives. This investigation is for resident screening purposes only, and is strictly confidential. This report contains information compiled from sources believed to be reliable, but the accuracy of which cannot be guaranteed. I hereby hold Eproperty365.com, Landlord and its agents free and harmless of any liability for any damages a rising out of any improper use of this information. Important information about your rights under the Fair Credit reporting Act:
                    </p>
                    <ul>
                        <li>You have a right to request disclosure of the nature and scope of the investigation.</li>
                        <li>You must be told if information in your file has been used against you.</li>
                        <li>You have a right to know what is in your file, and this disclosure maybe free.</li>
                        <li>You have the right to ask for a credit score (there maybe a fee for this service). </li>
                        <li>You have the right to dispute incomplete or inaccurate information. Consumer reporting agencies must correct inaccurate, incomplete, or unverifiable information. These reports are being processed by Eproperty365.com, 809 N Bethlehem Pk. Lower Gwynedd, PA 19002, info@eproperty365.com. A summary of your rights under the Fair Credit Reporting Act is available by visiting or writing (Para information en espanol, visite oe scribe):http://www.ftc.gov/credit Consumer Response Center, Room 130-A, Federal Trade Commission, 600 Pennsylvania Avenue N.W., Washington D.C. 20580</li>
                    </ul>

                    <p>
                        Consumer Report Disclosure and Authorization 
In connection with my application for housing, I understand that the property owner/agent may obtain one or more consumer reports, which may contain public information, for the purposes of evaluating my application. These consumer reports will be obtained from one or more of the following consumer reporting agencies:
                    </p>
                    <ul>
                        <li>Equifax , E.C.I.F. ,P.O. Box 740241, Atlanta, GA, 30374-0241,(800)685-1111</li>
                        <li>Trans Union, Regional Disclosure Center, 1561 Orangethorpe Ave., Fullerton, CA, 92631, (714)738-3800 </li>
                        <li>Experian(TRW), Consumer Assistance, P.O. Box 949, Allen, TX, 75002, (888)397-3742 </li>
                        <li>Eproperty365, Inc. ,809 N Bethlehem Pike, Lower Gwynedd, PA 19002, info@eproperty365.com</li>

                        <li>Under California law, these consumer reports are defined as investigative consumer reports. These reports may contain information on my character, general reputation, personal characteristics and mode of living. In connection with my application for housing, I authorize owner/agent to obtain a consumer report from the consumer reporting agencies listed above.</li>

                    </ul>
                    <p>
                        PLEASENOTE:<br />
                        Under Section 1786.22 of the California Civil Code, if you wish to dispute the accuracy or completeness of any item in the consumer report, you may contact the consumer reporting agency named above and request an investigation. You also may view the file maintained on you by the above credit reporting agency during normal business hours. You can receive a copy of your file by providing proper identification and paying any related-copy costs. You may also receive a summary of the file by telephone. The agency is required to have employees available to explain your file to you, and they must explain any coded information in your file. You can bring some one with you to view the file, so long as they have identification.
                    </p>
                    <p style="width: 100%; font-weight: bold; font-size: 12px; text-align: center">FAIR HOUSING DISCLOSURE</p>
                    <p>The Fair Housing Act of 1968, as amended by the Fair Housing Act Amendments of 1988, prohibits discrimination in housing based on race, color, national origin, religion, sexual orientation, handicap, or familial status. Except as permitted by the Housing for Older Persons Act of 1955, we are committed to complying with the letter and spirit of the laws which administers compliance with the fair housing laws in the United States Department of Housing and Urban Development.</p>
                    <span style="width: 100%; float: left; margin-top: 10px;">If you would like to receive a copy of any investigative consumer report at no cost to you, please initial here:
                        <input type="text" id="investigateReport" /></span>
                    <span style="width: 100%; float: left; margin-top: 10px;">If you would like to receive a copy of any credit report at no cost to you, 
please initial here:
                        <input type="text" id="CredtReport" /></span>
                    <span style="width: 100%; float: left; margin-top: 20px;">
                        <input style="float: left" type="checkbox" id="chkaggrement" />
                        By checking this box and entering your name and last four of your Social Security Number and the Date you agreeing to all the terms and consideration within application I believe that the statements I have made are true and correct. I hereby authorize a credit and/or criminal check to be made, verification of information I provided and communication with any and all names listed on this application. I understand that any discrepancy or lack of information may result in the rejection of this application. I understand that this is an application does not constitute a rental or lease agreement in whole or part. I further understand that there is a non-refundable fee to cover the cost of processing my application and I am not entitled to a refund even if I don’t get the rental. Any questions regarding rejected applications must be submitted in writing and accompanied by a self-addressed stamped envelope.
                    </span>
                </div>
                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblSignature">
                    <thead>
                        <tr>
                            <th>Signature</th>
                            <th>Security No</th>
                            <th>Date</th>
                            <th>Edit</th>
                            <th>Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>

                </table>
                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px; margin-top: 20px;" cellspacing="2" cellpadding="5" border="1">
                    <tbody>
                        <tr>
                            <td>
                                <input placeholder="Signature" type="text" id="txtSign" /><input type="hidden" id="hdId" value="0" /></td>
                            <td>
                                <input type="text" placeholder="Last 4 Social security No." id="txtSecurity" /></td>
                            <td>
                                <input type="text" placeholder="Date" id="txtDate" />
                            </td>
                        </tr>
                    </tbody>
                </table>

            </div>

        </div>



        <table style="width: 100%; margin-top: 20px; float: left;">
            <tbody>
                <tr>
                    <td style="width: 50%;">
                        <input type="hidden" id="hdnShow" value="<%=isView%>" />

                        <input type="button" class="btn btnNewColor" id="btnExit" style="background-color: #3B5998" value="Exit" />

                        <input type="button" id="btnBack" class="btn btnNewColor" style="background-color: #3B5998" value="< Back" />

                    </td>
                    <td style="width: 50%;">
                        <input type="button" id="btnAddSignature" class="btn btnNewColor" style="background-color: #3B5998" value="Add Agreement Signer" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="btnSubmitDiv" style="margin-top: 30px; margin-left: 70%;">
                        <input type="button" id="btnContinue" class="btn" style="background-color: rgb(102, 255, 0); font-weight: bold;" value="Submit Total Application" />
                    </td>
                </tr>
            </tbody>
        </table>

        <div style="width: 100%; float: left">
            <p style="text-align: justify; margin-top: 20px; float: left">It may take up to 5 working days  to approve or denied this application. you will receive a email letting you know either way.</p>
            <p style="color: red; text-decoration: underline; margin-top: 10px; font-weight: bold; text-align: justify">You will receive an email shortly confirming that your application has been submitted for processing. You do not receive that email, please let us know by sending an email to info@eproperty365.com letting us know you did not receive an email.</p>
        </div>
    </div>
</asp:Content>
