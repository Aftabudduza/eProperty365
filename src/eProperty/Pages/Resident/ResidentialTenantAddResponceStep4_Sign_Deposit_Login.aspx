<%@ Page Title="EProperty365: Residential Tenant Step 4 Create Password" Language="C#" MasterPageFile="~/MasterPage/RentalAddMaster.Master" AutoEventWireup="true" CodeBehind="ResidentialTenantAddResponceStep4_Sign_Deposit_Login.aspx.cs" Inherits="eProperty.Pages.Resident.ResidentialTenantAddResponceStep4_Sign_Deposit_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Content/js/pdfobject.js"></script>
    <script src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script src="../../AppJs/TenantAllJs/ResidentialTenantAddResponceStep4_Sign_Deposit_Login.js"></script>
   

    <style type="text/css">
        /* The Modal (background) */
        .modal {
            display: none;
            position: fixed;
            z-index: 10000;
            padding-top: 0px;
            left: 0;
            top: 0;
            width: 100% !important;
            height: 100% !important;
            overflow: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.4);
            margin: 0px !important;
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            margin: auto;
            padding: 0;
            width: 45%;
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.4s;
            top: 10%;
        }

        /* Add Animation */


        /* The Close Button */
        .close {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }

        .modal-header {
            /*padding: 2px 16px;*/
            background-color: #5cb85c;
            color: white;
        }

        .modal-body {
            padding: 2px 16px;
            min-height: 360px !important;
            overflow: hidden;
            background: #F7F7F7;
        }

        .modal-footer {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DynamicContentHeaderPart" runat="server">
    <table style="width: 100%" border="0">
        <tbody>
            <tr>
                <td><span style="width: 25%; float: left; margin-left: 5%;">User Name :</span><span style="float: left; width: 60%; margin-left: 10%;"></span></td>
            </tr>
            <tr>

                <td style="width: 100%; text-align: center" class="auto-style5">
                    <span class="auto-style6">Step
                            4:</span><b>
                            </b>
                </td>

            </tr>
            <tr>
                <td style="background-color: #3B5998; text-align: center; color: white; line-height: 25px; font-family: Arial; font-weight: bold; font-size: 13px; font-style: normal;">
                    <span>Setup Tenant  Dashboard & Sign Documents</span>
                </td>
            </tr>

        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DynamicContentBodyPart" runat="server">
    <div style="margin: 5px 20px; width: 93%; float: left;">
        <span id="ApplicationCode"></span>
        <div style="width: 100%; background-color: #F7F7F7; margin-top: 20px; border: 1px solid black; float: left;">
            <div class="col-md-12" style="float: left; margin-top: 0px; padding: 4px !important; text-align: center; font-weight: bold; font-size: 14px;">
                <span>Documents</span>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                <ol style="font-size: 13px; font-weight: bold;">
                    <li>Click on each documents and look at the status to tell you what to do.</li>
                    <li>If the document status is "download"  you must download the document and filled them out and then upload them. All you need to do is click on the document checkbox and then press the "Download" button.  Your selected document will be downloaded to your download sub-directory. The file will be in either a Word, Text, PDF format.  Fill out information and save the file in the original format you receive it. You may also take a picture of each page of the document (be sure you can read it) and save each page in jpg format. Press the Browse Button to select the files on your computer to upload. Then press the "Upload" Button.</li>
                    <li>All document statuses must say either "Uploaded" or " Viewed" when your finished and you must have type your name and put last 4 digits of your Social Security number and date it. EVEN IF YOU SIGNED THE ACTUAL FORM to complete this section.</li>

                </ol>
                <span style="width: 100%; float: left; font-weight: bold; font-size: 13px;">ALL PARTIES MUST SIGN THE DOCUMENT BELOW.</span>
              
                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblDoc">
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

                </table>
                <div style="width: 100%">
                    <span>
                        <input type="file" disabled="disabled" id="documentUpload" /></span>
                    <span>
                        <input id="savedoc" type="button" disabled="disabled" class="btn btnNewColor" value="Upload" onclick="SavedocumentUpload();" /></span>
                </div>
                <div id="iframedis" class="col-md-12">
                    <object id="objid" data="" type="application/pdf">
                        <iframe src="" id="viewfile" style="width: 618px; height: 800px"></iframe>
                    </object>

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
                                <input type="text" placeholder="Date" id="txtDate" /></td>

                        </tr>
                    </tbody>
                </table>
                <span style="float: left; margin-top: 10px; text-align: center; width: 100%">
                    <input type="button" class="btn btnNewColor" style="background-color: #3B5998" value="Tenant Submit" id="btnTenantSubmit" /></span>
                <span style="float: left; margin-top: 10px; font-size: 13px; font-weight: bold; color: red; text-align: center; width: 100%;">All Documents Must Be Viewed or Uploaded and signed!</span>

                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px;" cellspacing="2" cellpadding="5" border="1" id="tblOwnerInfo">
                    <thead>
                        <tr>
                            <th>Owner Signature</th>
                            <th>Security Number</th>
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
                                <input type="text" placeholder="Owner Signature" id="txtOwnerSig" /><input type="hidden" id="hdOwnerId" value="0" /></td>
                            <td>
                                <input type="text" placeholder="Last 4 Social security No." id="txtOwnerSecurity" /></td>
                            <td>
                                <input type="text" placeholder="Date" id="txtOwnerDate" /></td>
                        </tr>
                        
                    </tbody>
                </table>
                <span style="float: left; margin-top: 10px; text-align: center; width: 100%;">
                    <input type="button" class="btn btnNewColor" style="background-color: #3B5998" value="Owner Submit" id="btnSaveOwner" /></span>
                <span style="float: left; margin-top: 10px; font-weight: bold; text-align: justify;">A copy of all the signed rental documents will be placed in your tenant portal via your MyFileit virtual safety deposit box account after the Owner / Landlord signs the documents with directions of how to access and view it.</span>
            </div>
        </div>
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; text-align: center;">
            <div class="col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="" id="H5" style="font-size: 14px;">Amount Due At Signing</h3>
            </div>
            <div style="float: left; width: 100%; padding: 0px 10px;">
                <table style="font-size: 11px; float: left; width: 100%; background-color: white; margin-bottom: 20px; margin-top: 20px;" cellspacing="2" cellpadding="5" border="1">

                    <tbody>
                        <tr>
                            <td>Security Deposit:</td>
                            <td>
                                <input type="text" id="txtSecurityDeposite" /></td>

                        </tr>
                        <tr>
                            <td>One Months Rent:</td>
                            <td>
                                <input type="text" id="txtOneMonthRent" /></td>
                        </tr>
                        <tr>
                            <td>Prorate Amount  </td>
                            <td>
                                <input type="text" id="txtProrateAmount" /></td>
                        </tr>

                        <tr>
                            <td>First Months Rent:  </td>
                            <td>
                                <input type="text" id="txtFirtMonthRent" /></td>
                        </tr>
                        <tr>
                            <td>Total Due:   </td>
                            <td>
                                <input type="text" id="txtTotalDue" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>

        </div>
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; text-align: center;">
            <table cellspacing="3" cellpadding="5" style="width: 100%; float: left; background-color: #DFE3EE">
                <tbody style="padding: 5px;">
                    <tr>
                        <td colspan="3">
                            <div class="col-md-12" style="padding-left: 3px; float: left; text-align: center;">
                                <div class="form-group">
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="CashOrCheck" value="Check" id="Checking" checked="checked" class="flat-red" />
                                        Checking Account
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="CashOrCheck" value="Cash" id="Cash" class="flat-red" />
                                        Cash
                                    </label>


                                </div>
                            </div>
                        </td>

                    </tr>
                </tbody>
            </table>
            <table cellspacing="3" cellpadding="5" style="width: 100%; float: left; background-color: #DFE3EE">
                <tbody>
                    <tr>
                        <td colspan="3">Billing Information:
                        </td>
                    </tr>
                    <tr>
                        <td style="" colspan="3">
                            <span style="margin-right: 7%;">*Account Name:</span>
                            <input type="text" id="nameAccountapp1Txt" style="width: 75%" />


                        </td>
                    </tr>
                    <tr>
                        <td style="" colspan="3">
                            <span style="margin-right: 13%">*Address1:</span>
                            <input type="text" id="addressapp1Txt1" style="width: 75%" />


                        </td>
                    </tr>
                    <tr>
                        <td style="" colspan="3">
                            <span style="margin-right: 13%">*Address2:</span>
                            <input type="text" id="addressapp1Txt2" style="width: 75%" />


                        </td>
                    </tr>
                    <tr>
                        <td class="">
                            <span style="margin-right: 3%">*Country:</span>
                            <select id="ddlCountry" style="width: 75%" class="ddl country"></select>

                        </td>
                        <td class="" colspan="2">
                            <span style="margin-right: 3%">*Region:</span>
                            <%--<select id="ddlRegion" style="width: 75%" class="ddl"></select>--%>
                            <input type="text" id="ddlRegion" style="width: 75%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="">
                            <span style="margin-right: 10%">*City:</span>
                            <input type="text" id="cityapp1Txt" style="width: 75%" class="city" />
                        </td>
                        <td class="" colspan="2">
                            <span style="margin-right: 3%">*State:</span>
                            <select id="ddlstateapp" style="width: 75%" class="ddl state"></select>

                        </td>

                    </tr>
                    <tr>
                        <td colspan="2" style="">
                            <span style="margin-right: 1%">*Zip code:</span>
                            <input type="text" id="zipcodeapp1Txt" style="width: 75%" />


                        </td>
                        
                    </tr>
                </tbody>
            </table>
            <table cellspacing="3" cellpadding="5" style="width: 100%; float: left; background-color: #DFE3EE" id="tblChecking">
                <tbody>
                    <tr>
                        <td colspan="3">For Checking Account Use Only::</td>
                    </tr>
                    <tr>
                        <td class="auto-style56" colspan="2">*Routing number (2nd # from bottom
                                        left):</td>
                        <td>
                            <input type="text" id="routingnumapp1Txt" style="width: 254px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style56" colspan="2">Re-Enter Routing number:</td>
                        <td>
                            <input type="text" id="rerountingnumapp1Txt" style="width: 252px" />

                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style56" colspan="2">* Account
                                        number (last # from bottom left):</td>
                        <td style="">
                            <input type="text" id="checkacctnumapp1Txt" style="width: 250px" />


                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style56" colspan="2">Re-Enter Account number:</td>
                        <td>
                            <input type="text" id="recheckacctnumapp1Txt" style="width: 248px" />

                        </td>
                    </tr>

                </tbody>
            </table>
            <div style="float: left; margin-top: 10px; text-align: justify; padding: 10px;" id="tblCheckingDiv">
                <span style="float: left">By clicking on the Submit Payment button I authorize the Landlord or there agents to debit my Checking account for the below amount.</span>
                <br />
                <span style="float: left; margin-top: 20px;">
                    <input type="checkbox" id="RentalAmount" />Please automatically debit the above rental amount on the 1st day of each month for  the monthly rental payment plus any processing fee.. </span>
                <div style="float: left; width: 100%">
                    <span>Sub-Total Charge:</span>
                    <span id="subTotalCharge"></span>
                </div>
                <div style="float: left; width: 100%">
                    <span>Checking Account Processing fee <span id="percentRatio"></span>:</span>
                    <span id="Amount"></span>
                </div>
                <div style="float: left; width: 100%">
                    <span>Total Amount your Account will be Charged:</span>
                    <span id="TotalAmountCharge"></span>
                </div>
                <div style="float: left; width: 100%; text-align: center; margin-top: 20px; margin-bottom: 20px;">
                    <input type="button" class="btn btnPayment" style="background-color: #66FF00" value="Submit Payment $ Continue" />
                </div>
            </div>
        </div>
        <div style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left; padding: 10px; display: none" id="tblCash">
            <div class="col-md-12" style="float: left; margin-top: 0px; padding: 4px !important">
                <h3 class="" id="H5" style="font-size: 14px;">Cash Receipt :</h3>
            </div>
            <div style="float: left; margin-top: 10px;">
                <p>
                    I
                    <input type="text" placeholder="Persone Name" id="txtPersoneName" />
                    <input type="text" placeholder="Last 4 Your credit card." id="PersonLastCreitCard" />
                    <input type="text" id="txtCompanyName" placeholder="Company Name" />
                    as authorized agent or Landlord received in cash the amount of 
                    <input type="text" id="txtAmountOff" placeholder="" />
                    on
                    <input type="text" placeholder="date" id="txtdateFrom" />
                    at 
                    <input id="txtLocation" type="text" placeholder="Enter Location" />

                </p>
                <span>Signature :
                    <input type="text" id="txtOwnerSignature" placeholder="Persone Name" />
                    <input type="text" id="txtCreditCardLast4" placeholder="Last 4 Your credit card." /></span>
            </div>
            <div style="float: left; width: 100%; text-align: center; margin-top: 20px; margin-bottom: 20px;">
                <input type="button" class="btn btnPayment" style="background-color: #66FF00" value="Submit Payment $ Continue" />
            </div>
        </div>



        <table style="width: 100%; margin-top: 20px; float: left;">
            <tbody>
                <tr>
                    <td style="text-align: center; width: 50%;">
                        <input type="button" class="btn btnNewColor" style="background-color: #3B5998" value="Cancel" /></td>
                    <td style="width: 50%;">
                        <input type="button" class="btn" style="background-color: #66FF00" value="Submit & Continue" /></td>
                </tr>
            </tbody>
        </table>
    </div>


    <div id="myModalForRegistraton" class="modal" data-backdrop="false">
        <!-- Modal content -->
        <div class="modal-content">

            <div class="modal-body">
                <div class="page-content RegistratonModel" id="RegistratonModel" style="margin-top: 6%">
                    <div class="row-fluid">
                        <div class="span12 widget">
                            <div class="widget-content form-container">
                                <div class="col-md-12">
                                    <div class="col-md-10" style="margin: 0 auto">
                                        <div class="col-md-6" style="float: left">
                                            <input type="text" id="txtTenantEmail" class="col-sm-12 form-control" placeholder="Enter Email Address" />
                                        </div>

                                        <div class="col-md-6" style="float: left">
                                            <input type="text" id="txtApprovalCode" placeholder="Enter Approval Code" class="col-sm-12 form-control" />

                                        </div>
                                        <div class="col-md-6" style="float: left">
                                            <input type="password" id="txtEnterPass" placeholder="Create Password"  class="col-sm-12 form-control" />
                                        </div>
                                        <div class="col-md-6" style="float: left">
                                            <input type="password" id="txtRePassword" placeholder="Re-Enter Password" class="col-sm-12 form-control" />
                                        </div>
                                        <div class="col-md-8" style="margin-top: 6px; float: left" id="divGenerateRandomValues">
                                            <%--<input type="button" class="btn btnNewColor col-sm-12" id="btnFirstTimeSignIn" style="background-color: #3B5998" value="First Time Sign In" />--%>
                                        </div>
                                        <div class="col-md-4" style="margin-top: 11px; float: left">
                                            <input type="button" class="btn btnNewColor col-sm-12" id="btnRefress" style="background-image: url(../../Images/refresh.jpg);width: 48px;height: 47px;border: none;" />
                                        </div>
                                        <div class="col-md-12" style="margin-top: 6px; float: left">
                                            <input type="text" id="txtCap" placeholder="Enter Character from image" class="col-sm-12 form-control" />
                                        </div>
                                        <div class="col-md-12" style="margin-top: 6px; float: left">
                                            <input type="button" class="btn btnNewColor col-sm-12" id="btnLogin" style="background-color: #3B5998" value="Login" />
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
    
</asp:Content>

