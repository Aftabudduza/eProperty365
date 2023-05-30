<%@ Page Title="EProperty365 : Create / Change Chart of Accounts" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" CodeBehind="AddChartofAccount.aspx.cs" Inherits="eProperty.Pages.Account.AddChartofAccount" %>

<%@ Register TagPrefix="MyAccount" TagName="AccountControl" Src="~/UserControls/accounts.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border CommonHeader col-md-12">
                            <h3 class="box-title">Create / Change Chart of Accounts</h3>
                        </div>
                        <MyAccount:AccountControl ID="Account" runat="server" />
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <label for="txtAccountCode" class="col-sm-6 col-form-label">*Account Code:</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="txtAccountCode" placeholder="Account Code" maxlength="10" />
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtAccountName" class="col-sm-6 col-form-label">*Account Name: </label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="txtAccountName" placeholder="Account Name" maxlength="50" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label for="ddlAccountType" class="col-sm-6 col-form-label">*Account type:</label>
                                    <div class="col-sm-6">
                                        <select class="form-control ddl" id="ddlAccountType"></select>
                                    </div>
                                    <label style="margin-right: 7px">
                                        <input type="checkbox" name="Active" id="chkIsActive" value="trueFalse" class="flat-red" checked="checked" />
                                        Is Active
                                    </label>
                                </div>

                                <div class="col-md-6">
                                    <input type="button" class="btn" style="background-color: #3B5998" value="Cancel" id="btnCancel" />
                                    <input type="button" class="btn" style="background-color: #66FF00" value="Save" data-id="0" id="btnSave" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="box">
                <div class="col-md-12">
                    <div id="divTable" style="max-height: 250px; overflow-x: auto; text-align: center;">
                        <table id="tbl" class="table table-responsive table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>Sl#</th>
                                    <th>Account Code</th>
                                    <th>Account Name</th>
                                    <th>Account Type</th>
                                    <th>Account Description</th>
                                    <th>Is Active</th>
                                    <th>Date</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script type="text/javascript" src="../../AppJs/Account/AddChartofAccount.js"></script>
</asp:Content>
