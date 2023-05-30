<%@ Page Title="EProperty365: Add Income" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="AddIncome.aspx.cs" Inherits="eProperty.Pages.Resident.AddIncome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border CommonHeader col-md-12">
                    <h3 class="box-title">Resident /Tenant  Make Payment / Refund Credit Dashboard</h3>
                </div>

                <div class="box-body">
                    <div class="col-md-12">
                        <div class="row" style="float: left;">
                            <div class="col-12">
                                <div class="form-group row">
                                    <label for="txtName" class="col-sm-2">*Owner ID:</label>
                                    <div class="col-sm-4">
                                        <span id="spanOwnerId"></span>
                                    </div>
                                    <label for="txtName" class="col-sm-2">*Property Manager ID:</label>
                                    <div class="col-sm-4">
                                        <span id="txtPropertyManagerId" data_id=""></span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="txtName" class="col-sm-2 col-form-label">*Location:</label>
                                    <div class="col-sm-10">
                                        <select class="form-control ddl" id="ddlLocation"></select>
                                        <%--</div>--%>
                                    </div>
                                    <div class="form-group row">
                                        <label for="txtName" class="col-sm-2 col-form-label">*Unit ID:</label>
                                        <div class="col-sm-4">
                                            <select class="form-control ddl" id="ddlUnitID"></select>
                                        </div>
                                        <label for="txtName" class="col-sm-2 col-form-label">*Resident /Tenant Name: </label>
                                        <div class="col-sm-4">
                                            <select class="form-control ddl" id="ddlResidentTenantName"></select>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="text-center mx-auto">
                                            <label style="margin-right: 7px">
                                                <input type="radio" id="rdoPayment" name="TransactionType" class="flat-red" value="Payment" checked="checked" />
                                                Payment
                                            </label>
                                            <label style="margin-right: 7px">
                                                <input type="radio" name="TransactionType" value="CreditTenantAccount" id="rdoCreditToTenantAccount" class="flat-red" />
                                                Credit to Tenant Account
                                            </label>
                                            <%--<div class="custom-control custom-radio custom-control-inline">
                         <input type="radio" id="rdoPayment" name="TransactionType" class="custom-control-input" value="Payment"/>
                         <label class="custom-control-label" for="rdoPayment">Payment</label>
                     </div>
                     <div class="custom-control custom-radio custom-control-inline">
                         <input type="radio" id="rdoCreditTenantAccount" name="TransactionType" class="custom-control-input"  value="CreditToTenantAccount"/>
                         <label class="custom-control-label" for="rdoCreditTenantAccount">Credit to Tenant Account</label>
                     </div>--%>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="txtEnterAmountBeingPaid" class="col-sm-2 col-form-label">Enter Amount being Paid:</label>
                                        <div class="col-sm-10">
                                            <input type="text" class="form-control" id="txtEnterAmountBeingPaid" placeholder="Enter Amount Being Paid" />
                                        </div>
                                    </div>

                                    <form style="background: #DFE3EE; border: 1px solid black; width: 100%">
                                        <div class="form-group row">
                                            <div class="text-center mx-auto">
                                                <label style="margin-right: 7px">
                                                    <input type="radio" id="rdoCheckingAccount" name="AccountType" class="flat-red" value="Check" checked="checked" />
                                                    Checking Account
                                                </label>
                                                <label style="margin-right: 7px">
                                                    <input type="radio" name="AccountType" value="Cash" id="rdoCash" class="flat-red" />
                                                    Cash
                                                </label>
                                                <%-- <div class="custom-control custom-radio custom-control-inline">
                             <input type="radio" id="rdoCheckingAccount" name="AccountType" class="flat-red"  value="chkAccount" checked="checked"/>
                             <label class="custom-control-label" for="rdoCheckingAccount">Checking Account</label>
                         </div>--%>
                                                <%--<div class="custom-control custom-radio custom-control-inline">
                             <input type="radio" id="rdoCash" name="AccountType" class="custom-control-input flat-red" value="Cash"/>
                             <label class="custom-control-label" for="rdoCash">Cash</label>
                         </div>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-6">
                                                <div class="form-group row">
                                                    <label for="txtName" class="col-sm-2 col-form-label">*Name:</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control" id="txtName" placeholder="Name" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtAddress" class="col-sm-2 col-form-label">Address:</label>
                                                    <div class="col-sm-10">
                                                        <input type="text" class="form-control" id="txtAddress" placeholder="Address" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="ddlCountry" class="col-sm-2 col-form-label">*Country:</label>
                                                    <div class="col-sm-4">
                                                        <select id="ddlCountry" class="form-control ddl"></select>
                                                    </div>
                                                    <label for="txtRegion" class="col-sm-2 col-form-label">*Region:</label>
                                                    <div class="col-sm-4">
                                                        <input type="text" id="txtRegion" class="form-control" />
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtAddress" class="col-sm-1 col-form-label">*City:</label>
                                                        <div class="col-sm-3">
                                                            <input type="text" class="form-control" id="txtCity" placeholder="City" />
                                                        </div>
                                                        <label for="ddlState" class="col-sm-1 col-form-label">State:</label>
                                                        <div class="col-sm-3">
                                                            <select id="ddlState" class="form-control ddl"></select>
                                                        </div>
                                                        <label for="txtAddress" class="col-sm-1 col-form-label">*Zip Code:</label>
                                                        <div class="col-sm-3">
                                                            <input type="text" class="form-control" id="txtZipCode" placeholder="Zip Code" />
                                                        </div>
                                                    </div>
                                                    <p class="p-1">I am authorize signed on the above information and authorize the Landlord or their agent's to debit my Checking account for the above amount.</p>
                                                </div>
                                            </div>
                                            <div class="col-6" id="divCheckingAccount">
                                                <h6>Checking account</h6>
                                                <div class="form-group row">
                                                    <label for="txtAddress" class="col-sm-6 col-form-label">Routing number (2nd from left):</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" class="form-control" id="txtRoutingNumber2ndFromLeft" placeholder="Routing number (2nd from left)" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtReEnter" class="col-sm-6 col-form-label">Re-enter: </label>
                                                    <div class="col-sm-6">
                                                        <input type="text" class="form-control" id="txtReEnterRoutingNumber2ndFromLeft" placeholder="Re-enter" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtAccountNumberLastNumberFromLeft" class="col-sm-6 col-form-label">Account number (last number from left):</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" class="form-control" id="txtAccountNumberLastNumberFromLeft" placeholder="Account number (last number from left)" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtReEnterAccountNumberLastNumberFromLeft" class="col-sm-6 col-form-label">Re-enter:</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" class="form-control" id="txtReEnterAccountNumberLastNumberFromLeft" placeholder="Re-enter" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6" id="divCashReceipt" style="display: none">
                                                <h6>Cash Receipt :</h6>
                                                <div class="form-group row">
                                                    <label for="txtAddress" class="col-sm-7 col-form-label">As authorized agent or Landlord received in cash the amount of</label>
                                                    <div class="col-sm-2">
                                                        <input type="text" class="form-control" id="txtAmountDisable" disabled="disabled" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <input type="text" class="form-control tDate" id="txtDate" placeholder="mm/dd/yy" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtFrom" class="col-sm-4 col-form-label">From: </label>
                                                    <div class="col-sm-8">
                                                        <input type="text" class="form-control" id="txtFrom" placeholder="Person Name" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtFor" class="col-sm-4 col-form-label">For: </label>
                                                    <div class="col-sm-8">
                                                        <input type="text" class="form-control" id="txtFor" placeholder="Enter location & unit ID" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtSignature" class="col-sm-4 col-form-label">Signature: </label>
                                                    <div class="col-sm-8">
                                                        <input type="text" class="form-control" id="txtSignature" placeholder="Person Name" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </form>

                                    <div class="form-group row">
                                        <div class="text-center mx-auto">
                                            <input type="button" class="btn" style="background-color: #3B5998" value="Cancel" id="btnCancel" />
                                            <input type="button" class="btn" style="background-color: #66FF00" value="Save" id="btnSave" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/TenantAllJs/AddIncome.js"></script>
</asp:Content>
