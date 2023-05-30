<%@ Page Title="EProperty365: Record A Bill" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="RecordABill.aspx.cs" Inherits="eProperty.Pages.Account.RecordABill" %>
<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <div class="col-md-12" style="padding-left: 2px;">
            <div class="row">
            <div class="box-header with-border CommonHeader col-md-12" style="float: none;">
                <h3 class="box-title">Record A Bill</h3>
            </div>
                </div>
             <div class="row">
             <MyAccount:AccountControl ID="Account" runat="server" />

           </div>
                <div class="row" style="margin-bottom: 10px;">
                    <div class="col-5">
                        <div class="form-group row" style="float: right; margin-top: 8px;">

                            <label style="margin-right: 7px">
                                <input type="radio" name="GType" id="radioNew" value="NEW" checked="checked" class="flat-red" />
                                New
                            </label>
                            <label style="margin-right: 7px">
                                <input type="radio" name="GType" id="radioEdit" value="EDIT" class="flat-red" />
                                Edit
                            </label>
                        </div>
                    </div>
                    <div class="col-7" id="divBillnumber" style="display: none;">
                        <div class="form-group row">
                            <label for="txtDate" class="col-sm-3 col-form-label">Bill Number:</label>
                            <div class="col-sm-9">
                                <select id="ddlBillNumber" class="form-control"></select>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        <div class="form-group row">
                            <label for="txtDate" class="col-sm-3 col-form-label">Date:</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control tDate" id="txtDate" />
                            </div>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group row">
                            <label for="txtBillNumber" class="col-sm-4 col-form-label">Bill Number: </label>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" disabled="disabled" id="txtBillNumber" />
                                <input type="hidden" id="hd" value="0" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-8">
                        <div class="col-12">
                            <div class="form-group row">
                                <h5>From:</h5>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="form-group row">
                                <label for="txtDate" class="col-sm-3 col-form-label">Person / Company:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="txtPersonCompany" placeholder="Person / Company" maxlength="100" />
                                </div>
                            </div>
                        </div>
                         <div class="col-12">
                            <div class="form-group row">
                                <label for="txtDate" class="col-sm-3 col-form-label">Address 1:</label>
                                <div class="col-sm-9">
                                   <textarea class="form-control" id="txtAddress1" placeholder="Address 1" rows="1" maxlength="100"></textarea>
                                </div>
                            </div>
                        </div>
                      
                          <div class="col-12">
                            <div class="form-group row">
                                <label for="txtDate" class="col-sm-3 col-form-label">Address 2:</label>
                                <div class="col-sm-9">
                                   <textarea class="form-control" id="txtAddress2" placeholder="Address 2" rows="1" maxlength="100"></textarea>
                                </div>
                            </div>
                        </div>
                       
                        <div class="col-12">
                            <div class="form-group row">
                                  <label for="txtDate" class="col-sm-2 col-form-label">City:</label>
                                <div class="col-2">
                                    <input type="text" class="form-control" id="txtCity" placeholder="City" maxlength="25" />
                                </div>
                                 <label for="txtDate" class="col-sm-2 col-form-label">State:</label>
                                <div class="col-2">
                                    <select id="ddlState" class="form-control ddl"></select>
                                </div>
                                 <label for="txtDate" class="col-sm-2 col-form-label">Zip Code:</label>
                                <div class="col-2">
                                    <input type="text" class="form-control" id="txtZipCode" placeholder="Zip Code" maxlength="20" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="form-group row">
                                 <label for="txtDate" class="col-sm-2 col-form-label">Contact Name:</label>
                                <div class="col-4">
                                    <input type="text" class="form-control" id="txtContactName" placeholder="Contact Name" maxlength="50" />
                                </div>
                                 <label for="txtDate" class="col-sm-2 col-form-label">Phone No:</label>
                                <div class="col-4">
                                    <input type="text" class="form-control" id="txtPhoneNo" placeholder="Phone No" maxlength="20" />
                                </div>
                            </div>
                        </div>
                         <div class="col-12">
                            <div class="form-group row">
                                <label for="txtDate" class="col-sm-3 col-form-label">Email Address:</label>
                                <div class="col-sm-9">
                                   <input type="text" class="form-control" id="txtEmailAddress" placeholder="Email Address" maxlength="50" />
                                </div>
                            </div>
                        </div>
                      
                    </div>
                    
                    
                    <div class="col-4">
                        <label style="margin-right: 7px">
                            <input type="checkbox" name="Active" id="chkHaveW9Information" value="trueFalse" class="flat-red" />
                            Have W9 Information
                        </label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-6"></div>
                    <div class="col-6"></div>
                </div>
                <div class="row" style="margin-top: 20px;">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div id="divTable" style="max-height: 250px; overflow-x: auto; text-align: center;">
                                    <table id="tbl" class="table table-responsive table-hover table-striped">
                                        <thead style="border: 1px solid #696969; background: #696969; color: white;">
                                            <tr>
                                                <th></th>
                                                <th>Due Date</th>
                                                <th>Bill Id</th>
                                                <th>Description</th>
                                                <th>Type</th>
                                                <th>Credit Account Name</th>
                                                <th>Ledger</th>
                                                <th>Debit Account Name</th>
                                                <th>Ledger Code</th>
                                                <th>Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="text-center mx-auto">
                                <input type="button" class="btn" style="background-color: #3B5998" value="Cancel" id="btnCancel" />
                                <input type="button" class="btn" style="background-color: #66FF00" value="Record A Bill" data-id="0" id="btnSave" />
                            </div>
                        </div>
                    </div>
                </div>
          
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/Account/RecordABill.js"></script>
    <style type="text/css">
        .table thead tr th {
            vertical-align: top;
            width: 10%;
            padding: 0px;
        }

        .table-bordered > tbody > tr > td {
            padding: 5px;
            vertical-align: middle;
        }
    </style>
</asp:Content>
