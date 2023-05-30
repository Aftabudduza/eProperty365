<%@ Page Title="Commercial Tenant Step 1 Application Fee" Language="C#" MasterPageFile="~/MasterPage/Application.Master" AutoEventWireup="true" CodeBehind="CommercialTenantApplicationFee.aspx.cs" Inherits="eProperty.Pages.Commercial.CommercialTenantApplicationFee1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*.table-responsive {
            overflow: hidden;
        }*/
        /*table{background-color: #F7F7F7;}*/
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="InnerContent" runat="server">
    <table style="width: 100%; background-color: #F7F7F7; overflow: hidden;" border="0" class="table table-responsive">
        <tbody>
            <tr>
                <td><span style="width: 25%; float: left; margin-left: 5%;">User Name :</span><span style="float: left; width: 60%; margin-left: 10%;">John</span></td>
            </tr>
            <tr>

                <td style="width: 100%; text-align: center" class="auto-style5">
                    <span class="auto-style6">Step
                            1:</span><b>
                            </b>
                </td>

            </tr>
            <tr>
                <td style="background-color: #3B5998; text-align: center; color: white; font-weight: bold; font-size: 13px; font-style: normal;">
                    <span>Tenant Rental Application Fee</span>
                </td>
            </tr>
            <tr>
                <td style="color: #333333; line-height: 25px; font-family: Arial; font-size: 13px; font-style: normal; padding: 5px 10px">
                    <span style="width: 100%; float: left; font-size: 13px; line-height: 16px;">There is nonrefundable Application Fee based on the number 
                                people needing background and credit screening. We give you 40% discount after 1st person. You use this reports within 30 days to 
                                any properties managed by Eproperty365.</span>
                    <span style="width: 50%; float: left; margin-top: 10px">Number of People Signing the Agreement :  </span><span style="width: 34%; float: left; margin-top: 10px;">
                        <select id="numberofPeople" style="width: 100%">
                            <option value="1">Select....</option>
                        </select></span>
                    <span style="width: 100%; float: left">
                        <div class="row">
                            <div class="col-md-6" style="padding-left: 3px; width: 50%;">
                                <label for="txtManCity" class="col-sm-12 ">Perform Tenant background screening?:</label>
                            </div>
                            <div class="col-md-3" style="padding-left: 3px; width: 50%;">
                                 <div class="form-group">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="di" name="r3" class="flat-red" value="Yes" checked="checked" />
                                        Yes
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="r3" value="No" id="Dealer" class="flat-red" />
                                        No
                                    </label>

                                </div>
                            </div>
                        </div>
                    </span>
                    <span style="width: 23%; float: left">Total Application Fee </span><span style="width: 52%; float: left">$ 200</span><span style="width: 23%; color: red; float: left">* denote you must fill</span>
                </td>
            </tr>
        </tbody>
    </table>

    <div style="margin: 5px 10px; width: 97%; float: left;">
        <table class="table table-responsive" cellspacing="3" cellpadding="5" style="width: 100%; float: left; border: 1px solid black; background-color: #DFE3EE">
            <tbody style="padding: 5px;">
                <tr>
                    <td colspan="4">Enter your Credit Card or
                                Checking Account Information.</td>
                </tr>
                <tr>
                    <td colspan="4">
                         <div class="col-md-12" style="padding-left: 3px;float: left;text-align: center;">
                                <div class="form-group">
                                    <label style="margin-right: 7px">
                                        <input type="radio" id="Credit" name="card" class="flat-red" value="Credit" checked="checked" />
                                      Credit Card
                                    </label>
                                    <label style="margin-right: 7px">
                                        <input type="radio" name="card" value="Cash" id="Cash" class="flat-red" />
                                       Cash
                                    </label>
                                      <label style="margin-right: 7px">
                                        <input type="radio" name="card" value="Check" id="Checking" class="flat-red" />
                                       Checking Account
                                    </label>

                                </div>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"><span>*Name on Account:</span>
                    </td>
                    <td colspan="2">
                        <asp:TextBox Width="100%"
                            ID="nameAccountapp1Txt" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td colspan="2"><span>*Address1:</span></td>
                    <td colspan="2">
                        <asp:TextBox Width="100%"
                            ID="addressapp1Txt1" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td><span>*City:</span></td>
                    <td>

                        <asp:TextBox ID="cityapp1Txt"
                            runat="server"></asp:TextBox>

                    </td>
                    <td><span>*State:</span></td>
                    <td>

                        <asp:TextBox ID="stateapp1Txt"
                            runat="server"></asp:TextBox>

                    </td>

                </tr>
                <tr>
                    <td>
                        <span>*Zip code:</span>


                    </td>

                    <td>
                        <asp:TextBox
                            ID="zipcodeapp1Txt" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <span>*Credit Card Number:</span>


                    </td>
                    <td>
                        <asp:TextBox
                            ID="creditcardapp1Txt" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <span>*CVS Number:</span>
                    </td>
                    <td>
                        <asp:TextBox ID="Textbox1" runat="server"></asp:TextBox></td>
                    <td><span>*Expire Date:</span></td>
                    <td>
                        <input type="text" id="txtExpire" /></td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="4">
                        <input type="button" class="btn btn-success" value="Submit" />
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="table table-responsive" cellspacing="3" cellpadding="5" style="width: 100%; background-color: #DFE3EE; margin-top: 20px; border: 1px solid black; float: left">
            <tbody>
                <tr>
                    <td colspan="2">Checking Account:</td>
                </tr>
                <tr>
                    <td class="auto-style56">*Routing number (2nd # from bottom
                                        left):</td>
                    <td>
                        <asp:TextBox ID="routingnumapp1Txt"
                            runat="server" Width="250px"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style56">Re-Enter Routing number:</td>
                    <td>
                        <asp:TextBox ID="rerountingnumapp1Txt"
                            runat="server" Width="250px"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style56">* Account
                                        number (last # from bottom left):</td>
                    <td style="">
                        <asp:TextBox
                            ID="checkacctnumapp1Txt" runat="server"
                            Width="250px"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style56">Re-Enter Account number:</td>
                    <td>
                        <asp:TextBox ID="recheckacctnumapp1Txt"
                            runat="server" Width="250px"></asp:TextBox>
                        <br />
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="table table-responsive" style="width: 100%; margin-top: 20px; float: left; background-color: #F7F7F7;">
            <tbody>
                <tr>
                    <td style="text-align: center; width: 50%;">
                        <input type="button" class="btn btn-success" value="Exit" /></td>
                    <td style="width: 50%;">
                        <input type="button" class="btn btn-successNew" value="Go Application" /></td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
