<%@ Page Title="EProperty365: Tenant Payment History" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="TenantPaymentHistory.aspx.cs" Inherits="eProperty.Pages.Resident.TenantPaymentHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="box" style="border: 1px solid #000000;">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="row">
                    <div class="box-header with-border CommonHeader col-md-12" style="margin-top: 0px;">
                        <h3 class="box-title" id="H1" runat="server">Tenant Payment History</h3>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                            <label for="" class="col-sm-2 control-label">*Tenant Name </label>
                            <div class="col-sm-4">
                                 <span id="txtTenantName"></span>
                            </div>
                    </div>
                    <div class="row" style="padding: 10px" id="">
                       <table  style="background-color: white; margin-bottom: 20px;width: 100%" cellspacing="2" cellpadding="5" border="1" id="tblTenantHistory" >
                            <thead>
                            <tr>
                                <th>Date</th>
                                <th>Routing No</th>
                                <th>Account No</th>
                                <th>Check No</th>
                                <th>Card No</th>  
                                <th>Transaction Type</th>                             
                                <th>Amount</th>     
                                <th>Remarks</th>                          
                            </tr>
                            </thead>
                           <tbody></tbody>
                        </table>
                         </div>
                    <div class="row">
                            <div class="col-sm-6" style="text-align: center">
                                 <input type="button" class="btn btnNewColor"  id="btnImport" style="background-color: #3B5998" value="Export" />
                            </div>
                         <div class="col-sm-6" style="text-align: center">
                                <input type="button" class="btn btnNewColor"  id="btnPrint" style="background-color: #3B5998" value="Print" />
                            </div>
                    </div>
                     <div class="row" style="text-align: center">
                            <div class="col-sm-12">
                                 <input type="button" class="btn btnNewColor"  id="btnCancel" style="background-color: #3B5998" value="Cancel" />
                            </div>
                        
                    </div>
                     </div>
                 </div>
             </div>
           </div>
     
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
  <script src="../../AppJs/CommonJS/DataBaseOperationJs.js"></script>
    <script src="../../AppJs/TenantAllJs/TenantPaymenthistory.js"></script>

</asp:Content>
