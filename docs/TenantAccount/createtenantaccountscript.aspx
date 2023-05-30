<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createtenantaccountscript.aspx.cs" Inherits="E365tenant.createtenantaccountscript" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 1041px;
        }
        .auto-style5 {
            width: 100%;
            background-color: #F7F7F7;
        }
        .auto-style6 {
            width: 100%;
            background-color: #3B5998;
        }
        .auto-style7 {
            color: #FFFFFF;
        }
        .auto-style8 {
            text-align: center;
        }
        .auto-style9 {
            margin-left:38px;
            width: 89%;
            font-weight: bold;
        }
        .auto-style10 {
            margin-left:38px;
            width: 89%;           
        }
        .auto-style11 {
            width: 610px;
        }
        .auto-style12 {
            color: #FF0000;
        }
        .auto-style13 {
             margin-left:38px;
            width: 89%; 
            background-color: #DFE3EE;
        }
        .auto-style14 {
                     
        }
        .auto-style16 {
            width: 287px;
        }
        .auto-style17 {
            width: 93px;
        }
        .auto-style19 {
            width: 290px;
        }
        .auto-style20 {
            width: 92px;
        }
        .auto-style22 {
            width: 111px;
        }
        .auto-style23 {
            width: 330px;
        }
        .auto-style25 {
            width: 160px;
        }
        .auto-style26 {
            width: 215px;
        }
        .auto-style27 {
            width: 166px;
        }
        .auto-style28 {
            width: 159px;
        }
        .auto-style29 {
            width: 211px;
        }
        .auto-style30 {
            width: 210px;
        }
        .auto-style31 {
            width: 451px;
        }
        .auto-style33 {
            margin-left:38px;
            width: 89%;
            background-color: #DFE3EE;
        }
        .auto-style34 {
            
        }
        .auto-style35 {
            width: 461px;
        }
        .auto-style36 {
            width: 180px;
        }
        .auto-style37 {
             margin-left:38px;
            width: 89%;
        }
        .auto-style38 {
            margin-left:38px;
            width: 89%;            
        }
        .auto-style39 {
            width: 278px;
        }
        .auto-style40 {
            width: 235px;
        }
        .auto-style41 {
            width: 184px;
        }
        .auto-style42 {
            width: 182px;
        }
        .auto-style43 {
            margin-left: 0px;
        }
        .auto-style44 {
            width: 107px;
        }
        .auto-style45 {
            margin-left: 3px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            LIST<table class="auto-style5">
                <tr>
                    <td>
                        <table class="auto-style6">
                            <tr>
                                <td class="auto-style7">
                                    <p class="auto-style8">
                                        Create Tenant Account Script For Existing Tenats</p>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style9">
                            <tr>
                                <td>MANUAL:</td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style10">
                            <tr>
                                <td class="auto-style11">&nbsp;</td>
                                <td class="auto-style12">*denote you must fill</td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style13">
                            <tr>
                                <td>
                                    <table class="auto-style14">
                                        <tr>
                                            <td class="auto-style44">&nbsp;*Location ID:&nbsp;</td>
                                            <td class="auto-style16">
                                                <asp:TextBox ID="tenp3locidTxtBx" runat="server" Width="162px" CssClass="auto-style45"></asp:TextBox>
                                            </td>
                                            <td class="auto-style17">Unit ID:</td>
                                            <td>
                                                <asp:TextBox ID="tenp3unitidTxtBx" runat="server" Width="172px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style14">
                                        <tr>
                                            <td class="auto-style22">&nbsp;*Name First:&nbsp;</td>
                                            <td class="auto-style19">
                                                <asp:TextBox ID="tenp3firstnameTxtBx" runat="server" Width="252px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style20">*Last:</td>
                                            <td>
                                                <asp:TextBox ID="tenp3lastnameTxtBx" runat="server" Width="353px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style14">
                                        <tr>
                                            <td class="auto-style41">&nbsp;*Email Address:&nbsp;</td>
                                            <td class="auto-style23">
                                                <asp:TextBox ID="tenp3emailadTxtBx" runat="server" Width="280px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style42">Re-Enter Email:</td>
                                            <td>
                                                <asp:TextBox ID="tenp3reemailTxtBx" runat="server" Width="300px" CssClass="auto-style43"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style14">
                                        <tr>
                                            <td class="auto-style25">&nbsp;*Security depost held:&nbsp;</td>
                                            <td class="auto-style26">
                                                <asp:TextBox ID="tenp3secdepositamtTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style27">*One months rent held:</td>
                                            <td>
                                                <asp:TextBox ID="tenp3monthsheldamtTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style14">
                                        <tr>
                                            <td class="auto-style28">&nbsp;*Other amounts held:&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="tenp3otheramtheldTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="auto-style14">
                                        <tr>
                                            <td class="auto-style28">&nbsp;&nbsp; Lease Signed Date:&nbsp;</td>
                                            <td class="auto-style29">
                                                <asp:TextBox ID="tenp3leasesigndateTxtBx" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style30">*Date of monthly payment due:</td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList1" runat="server">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>26</asp:ListItem>
                                                    <asp:ListItem>27</asp:ListItem>
                                                    <asp:ListItem>28</asp:ListItem>
                                                    <asp:ListItem>29</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>31</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style14">
                            <tr>
                                <td class="auto-style31">&nbsp;</td>
                                <td>
                                    <asp:Button ID="tenp2addBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Add" Width="96px" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style9">
                            <tr>
                                <td>IMPORT CSV FILE:</td>
                            </tr>
                        </table>
                        <BR />
                        <table class="auto-style33">
                            <tr>
                                <td>
                                    <table class="auto-style34">
                                        <tr>
                                            <td>&nbsp;FORMAT:<br />
&nbsp;&lt;*Unit Location ID,Unit ID,FirstName,Last Name,Email address,security deposit amount held, amount of months rent held, other amounts<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; held, lease sign date, monthly payment date&gt;</td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table class="auto-style34">
                                        <tr>
                                            <td class="auto-style35">&nbsp;&nbsp;<asp:TextBox ID="tenp3pathTxtBx" runat="server" Width="394px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style36">
                                                <asp:Button ID="tenp3browseBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Browse" Width="117px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="tenp3importBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Import" Width="116px" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style9">
                            <tr>
                                <td>LIST:</td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style37">
                            <tr>
                                <td>&nbsp;&nbsp;<asp:TextBox ID="tenp3listTxtBx" runat="server" Height="234px" TextMode="MultiLine" Width="893px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="auto-style38">
                            <tr>
                                <td class="auto-style39">&nbsp;</td>
                                <td class="auto-style40">
                                    <asp:Button ID="tenp3caneclBtn" runat="server" BackColor="#3B5998" ForeColor="White" Text="Cancel" Width="114px" Height="35px" />
                                </td>
                                <td>
                                    <asp:Button ID="tenp3sendtBtn" runat="server" BackColor="#66FF00" Text="Send to Exisiting Tenants" Width="196px" Height="33px" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
