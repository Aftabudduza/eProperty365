<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tenantprofile.aspx.cs" Inherits="E365tenant.tenantprofile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            background-color: #F7F7F7;
        }
        .auto-style2 {
            height:28px;
            width: 100%;
            background-color: #3B5998;
        }
        .auto-style4 {
            height:25px;
            text-align: center;
        }
        .auto-style5 {
            width: 89%;
            margin-left:38px;
        }
        .auto-style6 {
            width: 116px;
        }
        .auto-style7 {
            width: 89%;
            margin-left:38px;
        }
        .auto-style8 {
            font-size: large;
            color: #FF0000;
        }
        .auto-style9 {
             width: 89%;
            margin-left:38px;
            
        }
        .auto-style10 {
            width: 115px;
        }
        .auto-style11 {
             width: 89%;
            margin-left:38px;
        }
        .auto-style12 {
            width: 227px;
        }
        .auto-style13 {
            width: 282px;
        }
        .auto-style14 {
            color: #FF0000;
        }
        .auto-style15 {
            width: 89%;
            margin-left:38px;
        }
        .auto-style16 {
            width: 89%;
            margin-left:38px;
            background-color: #DFE3EE;
        }
        .auto-style17 {
            width: 100%;
        }
        .auto-style19 {
            width: 163px;
        }
        .auto-style20 {
            width: 62px;
        }
        .auto-style21 {
            width: 143px;
        }
        .auto-style23 {
            width: 121px;
        }
        .auto-style24 {
            width: 82px;
        }
        
        .auto-style26 {
            width: 119px;
        }
        .auto-style27 {
            width: 206px;
        }
        .auto-style28 {
            width: 114px;
        }
        .auto-style29 {
            width: 225px;
        }
        .auto-style30 {
            width: 83px;
        }
        .auto-style31 {
            width: 138px;
        }
        .auto-style32 {
            width: 86px;
        }
        .auto-style33 {
            width: 118px;
        }
        .auto-style34 {
            width: 228px;
        }
        .auto-style35 {
            width: 202px;
        }
        .auto-style36 {
            width: 109px;
        }
        .auto-style38 {
            width: 94px;
        }
        .auto-style39 {
            width: 179px;
        }
        .auto-style41 {
            width: 75px;
        }
        .auto-style42 {
            width: 142px;
        }
        .auto-style44 {
            height: 360px;
        }
                
        .auto-style46 {
            color: #FFFFFF;
        }
        .auto-style47 {
            width: 85px;
        }
        .auto-style48 {
            height: 23px;
        }
        .auto-style49 {
            height: 23px;
            width: 85px;
        }
        .auto-style50 {
            height: 23px;
            width: 139px;
        }
        .auto-style51 {
            height: 23px;
            width: 63px;
        }
        .auto-style52 {
            height: 23px;
            width: 255px;
        }
        .auto-style53 {
            height: 23px;
            width: 88px;
        }
        .auto-style54 {
            width: 88px;
        }
        .auto-style55 {
            width: 221px;
        }
        .auto-style56 {
            width: 91px;
        }
        .auto-style58 {
            width: 370px;
        }
        
        .auto-style59 {
            border-style: solid;
            border-width: 1px;
        }
        .auto-style60 {
            width: 208px;
            border-style: solid;
            border-width: 1px;
        }
        .auto-style61 {
            width: 222px;
            border-style: solid;
            border-width: 1px;
        }
        .auto-style62 {
            width: 136px;
            border-style: solid;
            border-width: 1px;
        }
        
        .auto-style63 {
            border-style: solid;
            border-width: 1px;
            width: 527px;
        }
        .auto-style65 {
            width: 280px;
        }
        
        .auto-style66 {
            width: 291px;
        }
        .auto-style69 {
            width: 292px;
        }
        .auto-style72 {
            width: 288px;
        }
        .auto-style75 {
            margin-left: 0px;
        }
        .auto-style76 {
            width: 191px;
        }
        .auto-style77 {
            width: 219px;
        }
        .auto-style78 {
            width: 215px;
        }
        .auto-style79 {
            margin-left: 5px;
        }
        .auto-style80 {
            width: 201px;
        }
        .auto-style81 {
            width: 164px;
        }
        .auto-style82 {
            width: 151px;
        }
        
        .auto-style83 {
            height: 25px;
            text-align: center;
            width: 55px;
        }
        .auto-style84 {
            border-style: solid;
            border-width: 1px;
            width: 103px;
        }
        .auto-style86 {
            height: 25px;
            text-align: center;
            width: 262px;
        }
        .auto-style87 {
            width: 140px;
            border-style: solid;
            border-width: 1px;
        }
        .auto-style88 {
            width: 208px;
            border-style: solid;
            border-width: 1px;
            height: 28px;
        }
        .auto-style89 {
            width: 222px;
            border-style: solid;
            border-width: 1px;
            height: 28px;
        }
        .auto-style90 {
            width: 136px;
            border-style: solid;
            border-width: 1px;
            height: 28px;
        }
        .auto-style91 {
            border-style: solid;
            border-width: 1px;
            width: 103px;
            height: 28px;
        }
        .auto-style92 {
            border-style: solid;
            border-width: 1px;
            height: 28px;
        }
        .auto-style93 {
            border-style: solid;
            border-width: 1px;
            width: 527px;
            height: 28px;
        }
        .auto-style94 {
            width: 140px;
            border-style: solid;
            border-width: 1px;
            height: 28px;
        }
        
        .auto-style95 {
            width: 100%;
            background-color: #3B5998;
        }
        .auto-style96 {
            text-align: center;
        }
        .auto-style97 {
            width: 380px;
        }
        .auto-style98 {
            width: 306px;
        }
        
    </style>
</head>
<body style="width: 1040px">
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td>
                        <table class="auto-style95">
                            <tr>
                                <td class="auto-style46">
                                    <p class="auto-style96">
                                        Tenant Profile</p>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style5">
                            <tr>
                                <td class="auto-style6">Unit Location:</td>
                                <td>
                                    <asp:Label ID="tenp1unitlocLbl" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table class="auto-style5">
                            <tr>
                                <td class="auto-style6">Unit ID:</td>
                                <td>
                                    <asp:Label ID="tenp1unitidLbl" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table class="auto-style7">
                            <tr>
                                <td><span class="auto-style8">You can not change your email account that you registered with! </span>
                                    <br class="auto-style8" />
                                    <span class="auto-style8">You must contact Owner or Property Management to change password. This could take 5 days or more to change. </span></td>
                            </tr>
                        </table>
                        <table class="auto-style9">
                            <tr>
                                <td class="auto-style10">Email Address:</td>
                                <td>
                                    <asp:Label ID="tenp1emailLbl" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table class="auto-style11">
                            <tr>
                                <td class="auto-style12">Lease Agreement signed Name of : </td>
                                <td class="auto-style13">
                                    <asp:Label ID="tenp1Agreenamelbl" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style14">*denote you must fill</td>
                            </tr>
                        </table>
                        <table class="auto-style15">
                            <tr>
                                <td>
                                    <asp:TextBox ID="tenp1tenantnameTxtBox" runat="server" Height="172px" TextMode="MultiLine" Width="905px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="auto-style16">
                            <tr>
                                <td class="auto-style44">
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style23">&nbsp;*Name: First:&nbsp;</td>
                                            <td class="auto-style19">
                                                <asp:TextBox ID="tenp1firstnamTxtBox" runat="server" Width="157px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style20">Middle:</td>
                                            <td class="auto-style21">
                                                <asp:TextBox ID="tenp1middlenameTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style24">*Last:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1lastnameTxtBx" runat="server" Width="217px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style80">&nbsp;Alias or nick name used:&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="tenp1aliasnameTxtBx" runat="server" Width="334px" CssClass="auto-style75"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style23">&nbsp;Address&nbsp;1:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1address1TxtBx" runat="server" Width="373px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style23">&nbsp;Address 2:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1address2TxttBx" runat="server" Width="373px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style26">&nbsp;County:&nbsp;</td>
                                            <td class="auto-style27">
                                                <asp:DropDownList ID="tenp1countyDDList" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style28">Region:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1regionTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style33">&nbsp;City:&nbsp;</td>
                                            <td class="auto-style29">
                                                <asp:TextBox ID="tenp1cityTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style30">State:</td>
                                            <td class="auto-style31">
                                                <asp:DropDownList ID="tenp1stateDDist" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style32">Zip Code:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1zipcodeTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style33">&nbsp;Email Address:&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="tenp1emailadressTxtBx" runat="server" Width="447px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style33">&nbsp;Mobile Phone #:&nbsp;</td>
                                            <td class="auto-style34">
                                                <asp:TextBox ID="tenp1mobilenumTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style28">Home Phone #:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1honephoneTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style26">&nbsp;Relationship:&nbsp;</td>
                                            <td class="auto-style35">
                                                <asp:DropDownList ID="tenp1relshiDDList" runat="server">
                                                    <asp:ListItem>Single</asp:ListItem>
                                                    <asp:ListItem>Wife</asp:ListItem>
                                                    <asp:ListItem>Son</asp:ListItem>
                                                    <asp:ListItem>Daughter</asp:ListItem>
                                                    <asp:ListItem>Father</asp:ListItem>
                                                    <asp:ListItem>Friend Member</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style36">Other:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1otherTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style76">&nbsp;No People living in Unit:&nbsp;</td>
                                            <td class="auto-style38">
                                                <asp:DropDownList ID="tenp1peoplelivDDList" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">0</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style39">No People under age of 18:</td>
                                            <td class="auto-style42">
                                                <asp:DropDownList ID="tenp1nokidsDDList" runat="server">
                                                    <asp:ListItem>0</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style41">Birth Date:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1birthdateTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style17">
                            <tr>
                                <td class="auto-style97">&nbsp;</td>
                                <td>
                                    <asp:Button ID="tenp1signerBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Add only Lease Signers" Width="205px" Height="32px" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style16">
                            <tr>
                                <td>
                                    <table class="auto-style17">
                                        <tr>
                                            <td>&nbsp;In Tenant Rental Account&nbsp;:</td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style81">&nbsp; Security Deposit: &nbsp;</td>
                                            <td class="auto-style66">
                                                <asp:Label ID="tenp1sdepositLbl" runat="server"></asp:Label>
                                            </td>
                                            <td class="auto-style78">&nbsp; Lease Signed Date:</td>
                                            <td>
                                                <asp:Label ID="tenp1lesesigndateLbl" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style81">&nbsp; One Months Rent:</td>
                                            <td class="auto-style69">
                                                <asp:Label ID="tenp1monthsrentLbl" runat="server"></asp:Label>
                                            </td>
                                            <td class="auto-style78">&nbsp; Date of monthly payment due:</td>
                                            <td>
                                                <asp:Label ID="tenp1datedueLbl" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style81">&nbsp; Other Amounts held:</td>
                                            <td class="auto-style72">
                                                <asp:Label ID="tenp1otherheldLbl" runat="server"></asp:Label>
                                            </td>
                                            <td class="auto-style77">&nbsp;&nbsp; Start of Eproperty365 Account:</td>
                                            <td>
                                                <asp:Label ID="tenp1estartdateLbl" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table class="auto-style16">
                            <tr>
                                <td>
                                    <table class="auto-style2">
                                        <tr>
                                            <td class="auto-style46">
                                                <p class="auto-style4">
                                                    Emergency Contacts</p>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table class="auto-style17">
                                        <tr>
                                            <td>&nbsp;&nbsp;<asp:TextBox ID="tenp1enamesTxtBx" runat="server" Height="156px" TextMode="MultiLine" Width="871px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style32">&nbsp;Name:&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="tenp1enameTxtBx" runat="server" Width="512px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style47">&nbsp;Address 1:&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="tenp1emaddress1TxtBx" runat="server" Width="512px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style47">&nbsp;Address 2:&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="tenp1emaddress2TxtBx" runat="server" Width="512px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style49">&nbsp;County:&nbsp;</td>
                                            <td class="auto-style50">
                                                <asp:DropDownList ID="renp1ecountyTxtBx" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style51">Region:</td>
                                            <td class="auto-style52">
                                                <asp:TextBox ID="tenp1eregionTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style53">Zip Code:</td>
                                            <td class="auto-style48">
                                                <asp:TextBox ID="tenp1ezipTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style54">&nbsp;Relationship:&nbsp;</td>
                                            <td class="auto-style55">
                                                <asp:TextBox ID="tenp1erelshipTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style56">Phone No.:</td>
                                            <td>
                                                <asp:TextBox ID="tenp1ephonenumTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style82">&nbsp;Email Address:&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="tenp1emailadTxtBx" runat="server" Width="431px" CssClass="auto-style79"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style58">&nbsp;</td>
                                            <td>
                                                <asp:Button ID="tenp1addCBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Add another Contact" Height="36px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <table class="auto-style16">
                            <tr>
                                <td>
                                    <table class="auto-style2">
                                        <tr>
                                            <td class="auto-style46">
                                                <p class="auto-style4">
                                                    Vehicles</p>
                                            </td>
                                        </tr>
                                    </table>
&nbsp;<br />
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style60">
                                                <p class="auto-style4">
                                                    Make</p>
                                            </td>
                                            <td class="auto-style61">
                                                <p class="auto-style4">
                                                    Model</p>
                                            </td>
                                            <td class="auto-style62">
                                                <p class="auto-style4">
                                                    Color</p>
                                            </td>
                                            <td class="auto-style84">
                                                <p class="auto-style83">
                                                    Year</p>
                                            </td>
                                            <td class="auto-style59">
                                                <p class="auto-style4">
                                                    License Plate</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style60">
                                                <asp:TextBox ID="tenp1vmake1TxtBx" runat="server" Width="197px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style61">
                                                <asp:TextBox ID="tenp1vmodel1TxtBx" runat="server" Width="209px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style62">
                                                <asp:TextBox ID="tenp1vcolor1TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style84">
                                                <asp:TextBox ID="tenp1vyear1TxtBx" runat="server" Width="72px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style59">
                                                <asp:TextBox ID="tenp1lic1TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style88">
                                                <asp:TextBox ID="tenp1vmake2TxtBx" runat="server" Width="198px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style89">
                                                <asp:TextBox ID="tenp1vmodel2TxtBx" runat="server" Width="209px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style90">
                                                <asp:TextBox ID="tenp1vcolor2TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style91">
                                                <asp:TextBox ID="tenp1vyear2TxtBx" runat="server" Width="72px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style92">
                                                <asp:TextBox ID="tenp1lic2TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style60">
                                                <asp:TextBox ID="tenp1vmake3TxtBx" runat="server" Width="196px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style61">
                                                <asp:TextBox ID="tenp1vmodel3TxtBx" runat="server" Width="208px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style62">
                                                <asp:TextBox ID="tenp1vcolor3TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style84">
                                                <asp:TextBox ID="tenp1vyear3TxtBx" runat="server" Width="71px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style59">
                                                <asp:TextBox ID="tenp1lic3TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table class="auto-style16">
                            <tr>
                                <td>
                                    <table class="auto-style2">
                                        <tr>
                                            <td class="auto-style46">
                                                <p class="auto-style4">
                                                    People Staying in the Unit</p>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table class="auto-style17">
                                        <tr>
                                            <td class="auto-style63">
                                                <p class="auto-style4">
                                                    Name</p>
                                            </td>
                                            <td class="auto-style87">
                                                <p class="auto-style86">
                                                    Relationship</p>
                                            </td>
                                            <td class="auto-style59">
                                                <p class="auto-style4">
                                                    Age</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style93">
                                                <asp:TextBox ID="tenp1name1TxtBx" runat="server" Width="456px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style94">
                                                <asp:TextBox ID="tenp1rel1TxtBx" runat="server" Width="241px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style92">
                                                <asp:TextBox ID="tenp1age1TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style63">
                                                <asp:TextBox ID="tenp1name2TxtBx" runat="server" Width="456px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style87">
                                                <asp:TextBox ID="tenp1rel2TxtBx" runat="server" Width="239px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style59">
                                                <asp:TextBox ID="tenp1age2TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style63">
                                                <asp:TextBox ID="tenp1name3TxtBx" runat="server" Width="458px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style87">
                                                <asp:TextBox ID="tenp1rel3TxtBx" runat="server" Width="240px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style59">
                                                <asp:TextBox ID="tenp1age3TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style63">
                                                <asp:TextBox ID="tenp1name4TxtBx" runat="server" Width="457px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style87">
                                                <asp:TextBox ID="tenp1rel4TxtBx" runat="server" Width="240px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style59">
                                                <asp:TextBox ID="tenp1age4TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style63">
                                                <asp:TextBox ID="tenp1name5TxtBx" runat="server" Width="458px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style87">
                                                <asp:TextBox ID="tenp1rel5TxtBx" runat="server" Width="239px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style59">
                                                <asp:TextBox ID="tenp1age5TxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style63">
                                                <asp:TextBox ID="tenp1name6TxtBx" runat="server" Width="458px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style87">
                                                <asp:TextBox ID="tenp1rel6TxtBx" runat="server" Width="240px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style59">
                                                <asp:TextBox ID="tenp1age6TxtBx" runat="server"></asp:TextBox>
                                            </td>                                           
                                        </tr>                                    
                                    </table>
                                    <br />
                                </td>                              
                            </tr>
                        </table>
                        <br />
                        <br />
                        
                        <table class="auto-style17">
                            <tr>
                                <td class="auto-style98">&nbsp;</td>
                                <td class="auto-style65">
                                    <asp:Button ID="tenp1cancelBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Cancel" Width="109px" Height="31px" />
                                </td>
                                <td>
                                    <asp:Button ID="tenp1saveBtn" runat="server" BackColor="#66FF00" Text="Save" Width="116px" Height="33px" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <br />
                   
            </table>
        </div>
    </form>
</body>
</html>
