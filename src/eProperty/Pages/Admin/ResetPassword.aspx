<%@ Page Title="EProperty365: Reset Password" Language="C#" MasterPageFile="~/MasterPage/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ResetPassword.aspx.cs" Inherits="eProperty.Pages.Admin.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager runat="server" ID="sc1">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="userpanel">
            <ContentTemplate>
                <div class="box">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border CommonHeader col-md-12">
                                <h3 class="box-title" id="lblHeadline" runat="server">Reset Password</h3>
                            </div>
                           
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="box-body">
                               
                                <div class="col-md-6">
                                    <label for="txtPassword" class="col-sm-3 control-label">*Old Password:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPassword" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Old Password Required" ForeColor="Red" Display="Dynamic">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label for="txtNewPassword" class="col-sm-3 control-label">*New Password:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNewPassword" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Password Required" ForeColor="Red" Display="Dynamic">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                
                                <div class="col-md-6">
                                    <label for="txtConfirmPassword" class="col-sm-3 control-label">*Confirm Password:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtConfirmPassword" ValidationGroup="Contact"
                                            runat="server" ErrorMessage="Confirm Password Required" ForeColor="Red" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ID="cvPassword" Display="Dynamic" ControlToValidate="txtConfirmPassword" ControlToCompare ="txtNewPassword" ForeColor="Red" ErrorMessage="New Password Does not Match" ></asp:CompareValidator>
                                    </div>
                                </div>

                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer">
                                <div class="col-md-6">
                                    <asp:Button ID="btnSave" runat="server" Text="Update" OnClientClick="CheckVal()" OnClick="btnSubmit_Click" CssClass="btn btn-successNew" ValidationGroup="Contact" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnClose_Click" CssClass="btn btn-success " />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function show_confirm() {
            var r = confirm("Are You Sure To Delete?");
            if (r) {
                return true;
            }
            else {
                return false;
            }
        }
        function CheckVal() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
        }

    </script>
    <style type="text/css">
        .input-validation-error {
            border: 1px solid #ff0000;
            background-color: #ffeeee !important;
        }

        .field-validation-error {
            color: #ff0000 !important;
        }
    </style>
</asp:Content>
