<%@ Page Language="C#" AutoEventWireup="true" CodeFile="e365tenantportalForm1.aspx.cs" Inherits="E365signup.e365tenantportalForm1" %>

<!DOCTYPE html>

<html>

<head runat="server">
 <title> Setup Tenat Dashoard & Sign Documents</title>
</head>

  <body>&nbsp;<style type="text/css">
        .auto-style1 {
            width: 1024px;
            height: 1050px;
        }
        .auto-style2 {
            background-color: #FFFFCC;
            border:none;
        }
        .auto-style4 {
            background-color: #FFFFFF;
            border-style: solid;
            border-width: 1px;

        }
        .auto-style5 {
            font-size: medium;
            border:none;
        }
        .auto-style6 {
            font-size: medium;
            font-weight: bold;
            border:none;
        }
        .auto-style7 {
            text-align: center;
            border:none;
        }
        .auto-style17 {
            width: 439px;
            border:none;
        }
        .auto-style24 {
            width: 100%;
            border:none;
           
        }
        .auto-style25 {
            width: 119px;
            height: 23px;
            border:none;
        }
        .auto-style31 {
            width: 131px;
            border:none;
        }
        .auto-style34 {
            width: 130px;
            border:none;
        }
        .auto-style35 {
            width: 41px;
            height: 23px;
            border-style: none;
            vertical-align: top;
            
            }
        .auto-style36 {
            width: 85px;
            height: 23px;
            vertical-align:top;
            border-style: none;
            }
        .auto-style37 {
            width: 93px;
            height: 23px;
            vertical-align: top;
            border-style: none;
            }
        .auto-style39 {
            width: 134px;
            border-style: none;
            vertical-align: top;
        }
        .auto-style40 {
            width: 133px;
            border:none;
        }
        .auto-style46 {
            height: 23px;
            width: 50px;
            vertical-align: top;
            border-style: none;      
          
        }
        .auto-style47 {
            width: 100%;
            vertical-align: top;
            border-style: none;
        }
        .auto-style48 {
            height: 23px;
            vertical-align: top;
            border-style: none;
        }
        .auto-style49 {
            width: 100%;
            vertical-align: top;
            border-style: none;
        }
                  .auto-style50 {
                      width: 100%;
                  }
                  .auto-style54 {
                      width: 149px;
                  }
                  .auto-style55 {
                      width: 104px;
                  }
                  .auto-style57 {
                      text-align: center;
                  }
                  .auto-style58 {
                      width: 285px;
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                  }
                  .auto-style59 {
                      width: 142px;
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                  }
                  .auto-style60 {
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                  }
                  .auto-style61 {
                      color: #FF0000;
                  }
                  .auto-style62 {
                      height: 23px;
                  }
                  .auto-style63 {
                      width: 236px;
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                  }
                  .auto-style66 {
                      width: 226px;
                  }
                  .auto-style67 {
                      width: 107px;
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                  }
                  .auto-style68 {
                      width: 84px;
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                  }
                  .auto-style69 {
                      width: 100%;
                      border-style: solid;
                      border-width: 1px;
                      background-color: #CCCCFF;
                  }
                  .auto-style70 {
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                      height: 25px;
                  }
                  .auto-style71 {
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                      width: 155px;
                  }
                  .auto-style72 {
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                      height: 25px;
                      width: 155px;
                  }
                  .auto-style73 {
                      width: 100%;
                      border-style: none;
                      border-width: 1px;
                      background-color: #FFCC99;
                  }
                  .auto-style74 {
                      width: 113px;
                  }
                  .auto-style75 {
                      width: 152px;
                     
                  }
                  .auto-style76 {
                      width: 114px;
                  }
                  .auto-style77 {
                      width: 111px;
                  }
                  .auto-style78 {
                      width: 147px;
                  }
                  .auto-style79 {
                      width: 64px;
                  }
                  .auto-style80 {
                      width: 107px;
                  }
                  .auto-style81 {
                      width: 138px;
                  }
                  .auto-style82 {
                      width: 48px;
                  }
                  .auto-style83 {
                      width: 86px;
                  }
                  .auto-style84 {
                      width: 67px;
                  }
                  .auto-style85 {
                      width: 113px;
                      height: 25px;
                  }
                  .auto-style86 {
                      width: 139px;
                      height: 25px;
                  }
                  .auto-style87 {
                      height: 25px;
                      width: 47px;
                  }
                  .auto-style88 {
                      height: 25px;
                      width: 66px;
                  }
                  .auto-style89 {
                      width: 75px;
                  }
                  .auto-style90 {
                      width: 35px;
                  }
                  .auto-style91 {
                      width: 168px;
                  }
                  .auto-style92 {
                      font-style: italic;
                      border-left-color: #A0A0A0;
                      border-right-color: #C0C0C0;
                      border-top-color: #A0A0A0;
                      border-bottom-color: #C0C0C0;
                      padding: 1px;
                  }
                  .auto-style94 {
                      margin-left: 226px;
                  }
                  .auto-style95 {
                      width: 100%;
                      border-style: none;
                      border-width: 1px;
                      background-color: #CCFFCC;
                  }
                  .auto-style96 {
                      width: 19px;
                  }
                  .auto-style97 {
                      width: 103px;
                  }
                  .auto-style98 {
                      width: 132px;
                  }
                  .auto-style100 {
                      width: 125px;
                  }
                  .auto-style101 {
                      width: 30px;
                  }
                  .auto-style102 {
                      width: 69px;
                  }
                  .auto-style103 {
                      width: 134px;
                  }
                  .auto-style104 {
                      width: 88px;
                  }
                  .auto-style105 {
                      width: 62px;
                  }
                  .auto-style106 {
                      width: 140px;
                  }
                  .auto-style107 {
                      width: 220px;
                  }
                  .auto-style108 {
                      width: 118px;
                  }
                  .auto-style109 {
                      width: 186px;
                  }
                  .auto-style110 {
                      width: 97px;
                  }
                  .auto-style111 {
                      width: 586px;
                  }
                  .auto-style112 {
                      margin-left: 0px;
                  }
                  .auto-style113 {
                      width: 285px;
                  }
                  .auto-style114 {
                      width: 139px;
                  }
                  .auto-style115 {
                      margin-left: 19px;
                  }
                  .auto-style116 {
                      width: 97%;
                  }
        </style><form id="form3" runat="server">
      <div>
        <table style="width: 1024px; height: 30px;" border="1">
          <tbody>
            <tr>
              <td class="auto-style1"> 
                   
                <table style="width: 100%" border="0">
                  <tbody>
                    <tr>
                      <td style="width: 178px;">
                          <asp:Image ID="logosign1Image" runat="server" ImageUrl="http://173.248.153.72/images/E-property-Logo-A_150_71_96.png" />
                          <br/>
                        </td>
                      <td style="width: 555.333px;"><br/>
                        <asp:image id="topBanner1sign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_728_90_96.jpg"> </asp:image></td>
                      <td style="width: 263.1px;"><br/>
                      </td>
                      <td><br/>
                      </td>
                    </tr>
                  </tbody>
                </table>
                <br />
                <table border="0" class="auto-style50">
                  <tbody>
                    <tr>
                      <td class="auto-style31">
                          <asp:Image ID="checkImagesign1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk1.jpg" />
                          <br/>
                      </td>
                      <td class="auto-style40">
                          <asp:Image ID="checkImage2sign1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk2.jpg" />
                      </td>
                      <td class="auto-style39">
                          <asp:Image ID="checkImage3sign1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk3.jpg" />
                      </td>
                      <td class="auto-style34">
                          <asp:Image ID="checkImage4sign1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk4.jpg" />
                      </td>
                      <td><br/>
                      </td>
                    </tr>
                  </tbody>
                </table>
                  <table class="auto-style24">
                      <tr>
                          <td class="auto-style36"><asp:CheckBox ID="checksign1Box1" runat="server" BorderStyle="None" />
                          </td>
                          <td class="auto-style35">&nbsp;&nbsp;
                              <asp:CheckBox ID="checkapp1Box2" runat="server" />
                          </td>
                          <td class="auto-style37"><asp:CheckBox ID="checkapp1Box3" runat="server" />
                          </td>
                          <td class="auto-style46"><asp:CheckBox ID="checkapp1Box4" runat="server" />
                          </td>
                          <td class="auto-style25"></td>
                      </tr>
                  </table>

                <table style="width: 1024px; height: 30px;" border="0">
                  <tbody>
                    <tr>
                      <td style="width: 138.25px;">Applicant Name:   </td>
                      <td class="auto-style54"> <asp:textbox id="applicantNamesign1Txt"

                          runat="server" Height="22px"></asp:textbox> <br/>
                      </td>
                        <td class="auto-style55">Application No.</td>
                        <td>
                            <asp:Label ID="applicationnumsign1Lbl" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                  </tbody>
                </table>
                <table style="width: 100%" border="0">
                  <tbody>
                    <tr>
                      <td style="width: 606.933px;" class="auto-style5">
                        <p class="auto-style7"> <span class="auto-style6">Step
                            4:</span><b><br/>
                            Setup
                            Tenant Dashboard &amp; Sign documents</b></p>
                      </td>
                      <td style="width: 403.067px;"><br/>
                      </td>
                    </tr>
                  </tbody>
                </table>
                <br/>
                <table style="width: 100%" border="0">
                  <tbody>
                    <tr>
                      <td style="width: 606.017px;" class="auto-style4"><br />
                        <table style="width: 100%" border="0">
                          <tbody>
                            <tr>
                              <td class="auto-style2">
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>
                                              <p class="auto-style57">
                                                  Documents</p>
                                          </td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>Click on View Button for each Document and sign. If there is joint name documents the other parties meeds to sign them too. After there signed you will receive email how to view and get a copy of your documents.</td>
                                      </tr>
                                  </table>
                                  <br />
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style58">DocumentDecription</td>
                                          <td class="auto-style59">Status</td>
                                          <td class="auto-style60">View</td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style58">
                                              <asp:Label ID="ddoc1sign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                          <td class="auto-style59">
                                              <asp:Label ID="dstatus1sign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                          <td class="auto-style60">
                                              <asp:Button ID="dview1sign1Btn" runat="server" Text="View" />
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style58">
                                              <asp:Label ID="ddoc2sign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                          <td class="auto-style59">
                                              <asp:Label ID="dstatus2sign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                          <td class="auto-style60">
                                              <asp:Button ID="dview2sign1Btn" runat="server" Text="View" />
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style58">
                                              <asp:Label ID="ddoc3sign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                          <td class="auto-style59">
                                              <asp:Label ID="dstatus3sign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                          <td class="auto-style60">
                                              <asp:Button ID="dveiw3sign1Btn" runat="server" Text="View" />
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style58">
                                              <asp:Label ID="ddoc4sign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                          <td class="auto-style59">
                                              <asp:Label ID="dstatus4sign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                          <td class="auto-style60">
                                              <asp:Button ID="dveiw4sign1Btn" runat="server" Text="View" />
                                          </td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>&nbsp; &nbsp;<asp:TextBox ID="docviewsign1Txt" runat="server" Height="126px" TextMode="MultiLine" Width="532px"></asp:TextBox>
                                          </td>
                                      </tr>
                                  </table>
                                  <table class="auto-style50">
                                      <tr>
                                          <td>&nbsp;</td>
                                          <td>
                                              <asp:Button ID="vewedresdash1Btn" runat="server" Text="Viewed" />
                                          </td>
                                          <td>&nbsp;</td>
                                      </tr>
                                  </table>
                                  <br />
                                  <br />
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style63">Signaure</td>
                                          <td class="auto-style67">Last 4 Social Security No.</td>
                                          <td class="auto-style68">Date</td>
                                          <td class="auto-style60">Status</td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style63">
                                              <asp:TextBox ID="dsign1sign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style67">
                                              <asp:TextBox ID="dlast41sign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style68">
                                              <asp:TextBox ID="ddate1sign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style60">
                                              <asp:Label ID="dstatus1ssign1LBL" runat="server" Text="Label"></asp:Label>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style63">
                                              <asp:TextBox ID="dsign2sign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style67">
                                              <asp:TextBox ID="dlast42sign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style68">
                                              <asp:TextBox ID="ddate2sign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style60">
                                              <asp:Label ID="dstatus2ssign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style63">
                                              <asp:TextBox ID="dsign3sign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style67">
                                              <asp:TextBox ID="dlast43sign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style68">
                                              <asp:TextBox ID="ddate3sign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style60">
                                              <asp:Label ID="dstatus3ssign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style63">
                                              <asp:TextBox ID="dsign4sign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style67">
                                              <asp:TextBox ID="dlast44sign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style68">
                                              <asp:TextBox ID="ddate4sign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style62">
                                              <asp:Label ID="dstatus4ssign1Lbl" runat="server" Text="Label"></asp:Label>
                                          </td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style66">&nbsp;</td>
                                          <td>
                                              <asp:Button ID="dsignersign1Btn" runat="server" Text="Next Signer" />
                                          </td>
                                          <td>&nbsp;</td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style61">
                                              <p class="auto-style57">
                                                  All Documents must be viewed and signed!</p>
                                          </td>
                                      </tr>
                                  </table>
                                  <br />
                                <br/>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                          <br />
                          <table class="auto-style69">
                              <tr>
                                  <td>
                                      <table class="auto-style50">
                                          <tr>
                                              <td>Amount Due At Signing</td>
                                          </tr>
                                      </table>
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style71">Security Deposit</td>
                                              <td class="auto-style60">
                                                  <asp:Label ID="amtd1sign1Lbl" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style72">One Month Rent:</td>
                                              <td class="auto-style70">
                                                  <asp:Label ID="amtd2sign1Lbl" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style71">Pro-rated Amount:</td>
                                              <td class="auto-style60">
                                                  <asp:Label ID="amtd3signe1Ltb" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style71">First Months Rent:</td>
                                              <td class="auto-style60">
                                                  <asp:Label ID="amtd4sign1Lbl" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style71">&nbsp;</td>
                                              <td class="auto-style60">&nbsp;</td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style71">Total Due</td>
                                              <td class="auto-style60">
                                                  <asp:Label ID="amtd6sign1Lbl" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style73">
                              <tr>
                                  <td>
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style74">&nbsp; &nbsp;<asp:RadioButton ID="ccreditsign1RBtn" runat="server" Text="Credit Card" />
                                              </td>
                                              <td class="auto-style75">
                                                  <asp:RadioButton ID="checkacctsign1RBtn" runat="server" Text="Checking Account" />
                                              </td>
                                              <td>
                                                  <asp:RadioButton ID="cashsign1RBtn" runat="server" Text="Cash" />
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      Billing Information:<br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style76">*Account Name:</td>
                                              <td>
                                                  <asp:TextBox ID="signanamesign1Txt" runat="server" Width="305px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style74">*Address1:</td>
                                              <td>
                                                  <asp:TextBox ID="signaddress1sign1Txt" runat="server" Width="305px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style74">Address2</td>
                                              <td>
                                                  <asp:TextBox ID="signaddress2sign1Txt" runat="server" Width="305px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style77">*Country:</td>
                                              <td class="auto-style78">
                                                  <asp:DropDownList ID="countrysign1DDList" runat="server" Width="116px">
                                                  </asp:DropDownList>
                                              </td>
                                              <td class="auto-style79">Region:</td>
                                              <td>
                                                  <asp:TextBox ID="signregionsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style80">*City:</td>
                                              <td class="auto-style81">
                                                  <asp:TextBox ID="signcitysign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                              <td class="auto-style82">State:</td>
                                              <td class="auto-style83">
                                                  <asp:DropDownList ID="signstatesign1DDList" runat="server">
                                                  </asp:DropDownList>
                                              </td>
                                              <td class="auto-style84">Zip Code:</td>
                                              <td>
                                                  <asp:TextBox ID="signzipcodesign1Txt" runat="server" Width="98px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <span class="auto-style92">For Credit Card Use Only:</span><br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style85">Credit Card No.:</td>
                                              <td class="auto-style86">
                                                  <asp:TextBox ID="signccsign1Txt" runat="server" Width="123px"></asp:TextBox>
                                              </td>
                                              <td class="auto-style88">Exp. Date:</td>
                                              <td class="auto-style87">Month</td>
                                              <td class="auto-style89">
                                                  <asp:DropDownList ID="signmonthsign1DDList" runat="server">
                                                  </asp:DropDownList>
                                              </td>
                                              <td class="auto-style90">Year:</td>
                                              <td>
                                                  <asp:DropDownList ID="signyearsign1DDList" runat="server">
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style91">CVS No. 3-4 digit number:</td>
                                              <td>
                                                  <asp:TextBox ID="signcvssign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      For Checking Accounting Use Only:<br />
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style113">Routing Number (2nd from bottom left)</td>
                                              <td>
                                                  <asp:TextBox ID="signchknumsign1Txt" runat="server" CssClass="auto-style112" Width="270px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style113">Re-enter Routing Number</td>
                                              <td>
                                                  <asp:TextBox ID="signrertnumsign1Txt" runat="server" Width="270px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style113">Account Number: (last number bottom left)</td>
                                              <td>
                                                  <asp:TextBox ID="signacctnumsign1Txt" runat="server" Width="270px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style113">Re-Enter Account Number</td>
                                              <td>
                                                  <asp:TextBox ID="signreacctnumsign1Txt" runat="server" Width="270px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td>By clicking on the Submit Payment button I authorize the Landlord or there agents to debit my Credit Card or Checking account for the above amount.</td>
                                          </tr>
                                      </table>
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td>
                                                  <asp:CheckBox ID="signautopaysign1CheckBox" runat="server" Text=" Please Automatically debit the above account on the 1st day of each month for  the monthly rental payment. " />
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td>&nbsp;</td>
                                              <td class="auto-style111">
                                                  <asp:Button ID="sign1submitpayBtn" runat="server" CssClass="auto-style94" Text="Submit Payment" />
                                              </td>
                                              <td>&nbsp;</td>
                                          </tr>
                                      </table>
                                      <br />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style95">
                              <tr>
                                  <td>Cash Receipt:<br />
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style96">I</td>
                                              <td class="auto-style97">&lt;person name&gt;</td>
                                              <td class="auto-style98">
                                                  <asp:TextBox ID="signcashpnamesign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                              <td class="auto-style114">Last 4 license-State</td>
                                              <td class="auto-style100">
                                                  <asp:TextBox ID="signcashlast4sign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style108">&lt;company name&gt;</td>
                                              <td>
                                                  <asp:TextBox ID="signcashcnamesign1Txt" runat="server" Width="356px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td>as an authorized agent or Landlord received in cash the amount of&nbsp;
                                                  <asp:TextBox ID="signcashamountsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style101">on</td>
                                              <td class="auto-style102">mm/dd/cy</td>
                                              <td class="auto-style103">
                                                  <asp:TextBox ID="signcashdatesign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                              <td class="auto-style104">at &lt;location&gt;</td>
                                              <td>
                                                  <asp:TextBox ID="signcashlocationsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style105">Signature:</td>
                                              <td class="auto-style106">
                                                  <asp:TextBox ID="signcashsignsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                              <td class="auto-style98">Last 4 License-State</td>
                                              <td>
                                                  <asp:TextBox ID="signcashlic4sign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <table class="auto-style116">
                                          <tr>
                                              <td class="auto-style107">Email Address to Send Receipt:</td>
                                              <td>
                                                  <asp:TextBox ID="signcashrecemailsign1Txt" runat="server" CssClass="auto-style115" Width="317px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                  </td>
                                  <td></td>
                              </tr>
                          </table>
                          <br />
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td class="auto-style110">&nbsp;</td>
                                  <td class="auto-style109">
                                      <asp:Button ID="sign1cancelBtn" runat="server" Text="Cancel" />
                                  </td>
                                  <td>
                                      <asp:Button ID="sign1savecsign1Btn" runat="server" Text="Submit &amp; Continue &gt;" />
                                  </td>
                                  <td>&nbsp;</td>
                              </tr>
                          </table>
                          <br />
                          <br />

                         </td>
                      <td style="vertical-align: top" class=" auto-style17">
                          <br />
                          <table class="auto-style47">
                              <tr>
                                  <td class="auto-style48">&nbsp;&nbsp; &nbsp;&nbsp;<asp:Image ID="bannerad2sign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br/>
                          <table class="auto-style49">
                              <tr>
                                  <td>&nbsp; &nbsp;<asp:Image ID="bannerad3sign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad4sign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad5sign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad6sign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad7sign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="banerad8sign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <br />
                          <br />
                          <br />
                      </td>
                    </tr>
                  </tbody>
                </table>
              </td>
            </tr>
          </tbody>
        </table>
       </div>
    </form>
  </body>
  </html>