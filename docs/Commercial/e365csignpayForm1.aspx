<%@ Page Language="C#" AutoEventWireup="true" CodeFile="e365csignpayForm1.aspx.cs" Inherits="E365signup1.e365csignpayForm1" %>

<!DOCTYPE html>

<html>

<head runat="server">
 <title> Setup Commerical Lease/Rental Sign & Pay Form</title>
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
                      width: 163px;
                  }
                  .auto-style55 {
                      width: 104px;
                  }
                  .auto-style57 {
                      text-align: center;
                  }
                  .auto-style60 {
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                  }
                  .auto-style61 {
                      color: #FF0000;
                  }
                  .auto-style63 {
                      width: 236px;
                      border-style: solid;
                      border-width: 1px;
                      padding: 1px 4px;
                  }
                  .auto-style66 {
                      width: 226px;
                      height: 30px;
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
                      width: 195px;
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
                  .auto-style118 {
                      margin-left: 1px;
                  }
                  .auto-style119 {
                      text-align: center;
                      font-weight: bold;
                  }
                  .auto-style120 {
                      margin-left: 6px;
                  }
                  .auto-style122 {
                      width: 116px;
                  }
                  .auto-style123 {
                      width: 95px;
                  }
                  .auto-style125 {
                      width: 190px;
                  }
                  .auto-style126 {
                      width: 80px;
                  }
                  .auto-style127 {
                      width: 84px;
                  }
                  .auto-style128 {
                      width: 143px;
                  }
                  .auto-style129 {
                      width: 308px;
                  }
                  .auto-style130 {
                      width: 151px;
                  }
                  .auto-style131 {
                      width: 100%;
                      font-weight: bold;
                  }
                  .auto-style132 {
                      width: 387px;
                  }
                  .auto-style133 {
                      height: 30px;
                  }
                  .auto-style134 {
                      width: 109px;
                  }
                  .auto-style135 {
                      height: 30px;
                      width: 132px;
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
                          <asp:Image ID="logocsign1Image" runat="server" ImageUrl="http://173.248.153.72/images/E-property-Logo-A_150_71_96.png" />
                          <br/>
                        </td>
                      <td style="width: 555.333px;"><br/>
                        <asp:image id="topBanner1csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_728_90_96.jpg"> </asp:image></td>
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
                          <asp:Image ID="checkImagecsign1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk1.jpg" />
                          <br/>
                      </td>
                      <td class="auto-style40">
                          <asp:Image ID="checkImage2csign1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk2.jpg" />
                      </td>
                      <td class="auto-style39">
                          <asp:Image ID="checkImage3csign1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk3.jpg" />
                      </td>
                      <td class="auto-style34">
                          <asp:Image ID="checkImage4csign1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk4.jpg" />
                      </td>
                      <td><br/>
                      </td>
                    </tr>
                  </tbody>
                </table>
                  <table class="auto-style24">
                      <tr>
                          <td class="auto-style36"><asp:CheckBox ID="checkcsign1Box1" runat="server" BorderStyle="None" />
                          </td>
                          <td class="auto-style35">&nbsp;&nbsp;
                              <asp:CheckBox ID="checkcsign1Box2" runat="server" />
                          </td>
                          <td class="auto-style37"><asp:CheckBox ID="checkcsign3Box3" runat="server" />
                          </td>
                          <td class="auto-style46"><asp:CheckBox ID="checkcsign1Box4" runat="server" />
                          </td>
                          <td class="auto-style25"></td>
                      </tr>
                  </table>

                <table style="width: 1024px; height: 30px;" border="0">
                  <tbody>
                    <tr>
                      <td style="width: 138.25px;">Applicant Name:   </td>
                      <td class="auto-style54"> <asp:textbox id="applicantNamecsign1Txt"

                          runat="server" Height="22px"></asp:textbox> <br/>
                      </td>
                        <td class="auto-style55">Application No.</td>
                        <td>
                            <asp:Label ID="applicationnumcsign1Lbl" runat="server" Text="Label"></asp:Label>
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
                            Commercial / Industrial Dashboard &amp; Sign documents</b></p>
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
                                  &nbsp;&nbsp;
                                  <asp:Label ID="rentagreecsign1DLbl" runat="server" Text="Rental Agreement ID:"></asp:Label>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>
                                              <p class="auto-style119">
                                                  Documents</p>
                                          </td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>1) Click on each documents and look at the status to tell you what to do.<br />
                                              <br />
                                              2) If the document status is &quot;download&quot; you must download the document and filled them out and then upload them. All you need to do is click on the document checkbox and then press the &quot;Download&quot; button. Your selected document will be downloaded to your download sub-directory. The file will be in either a Word, Text, PDF format. Fill out information and save the file in the original format you receive it. You may also take a picture of each page of the document (be sure you can read it) and save each page in jpg format. Press the &quot;Browse&quot; Button to select the files on your computer to upload. Then press the &quot;Upload&quot; Button.<br />
                                              <br />
                                              3) All document statuses must say either &quot;Uploaded&quot; or &quot; Viewed&quot; when your finished and you must have type your name and put last 4 digits of your Social Security number and date it. EVEN IF YOU SIGNED THE ACTUAL FORM to complete this section.<br />
                                              <br />
                                              ALL PARTIES MUST SIGN THE DOCUMENT BELOW.</td>
                                      </tr>
                                  </table>
                                  <br />
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>
                                              <table class="auto-style50">
                                                  <tr>
                                                      <td class="auto-style132">&nbsp; Document Description &amp; File Name&nbsp;</td>
                                                      <td>&nbsp; Status&nbsp;</td>
                                                  </tr>
                                              </table>
                                          </td>
                                      </tr>
                                  </table>
                                  <asp:TextBox ID="docgridcsign1Txt" runat="server" CssClass="auto-style120" Height="183px" TextMode="MultiLine" Width="562px"></asp:TextBox>
                                  <br />
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style126">&nbsp;</td>
                                          <td class="auto-style127">
                                              <asp:Button ID="docviewcsign1Btn" runat="server" Text="*View" />
                                          </td>
                                          <td class="auto-style122">
                                              <asp:Button ID="doccsign1downBtn" runat="server" Text="Download" />
                                          </td>
                                          <td class="auto-style123">
                                              <asp:Button ID="docbrowsecsign1Btn" runat="server" Text="Browse" />
                                          </td>
                                          <td class="auto-style125">
                                              <asp:Button ID="docuploadcsign1Btn" runat="server" Text="Upload" />
                                          </td>
                                      </tr>
                                  </table>
                                  <br />
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>&nbsp;&nbsp; *Uploaded Documents may not be able to be viewed.&nbsp;</td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>&nbsp; &nbsp;<asp:TextBox ID="docviewcsign1Txt" runat="server" Height="126px" TextMode="MultiLine" Width="532px"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <br />
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td>&nbsp;</td>
                                          <td class="auto-style128">
                                              <asp:Button ID="docviewedcsign1Btn" runat="server" Text="Viewed" />
                                          </td>
                                          <td>&nbsp;</td>
                                      </tr>
                                  </table>
                                  <br />
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style63">Signature (Type your full name)</td>
                                          <td class="auto-style67">Last 4 Social Security No.</td>
                                          <td class="auto-style68">Date</td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style63">
                                              <asp:TextBox ID="dsign1csign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style67">
                                              <asp:TextBox ID="dlast41csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style68">
                                              <asp:TextBox ID="ddate1csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style63">
                                              <asp:TextBox ID="dsign2csign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style67">
                                              <asp:TextBox ID="dlast42csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style68">
                                              <asp:TextBox ID="ddate2csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style63">
                                              <asp:TextBox ID="dsign3csign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style67">
                                              <asp:TextBox ID="dlast43csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style68">
                                              <asp:TextBox ID="ddate3csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style63">
                                              <asp:TextBox ID="dsign4csign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style67">
                                              <asp:TextBox ID="dlast44csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style68">
                                              <asp:TextBox ID="ddate4csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style66"></td>
                                          <td class="auto-style135">
                                              <asp:Button ID="tenantsubmitcsign1Btn" runat="server" Text="Tenant Submit" />
                                          </td>
                                          <td class="auto-style133"></td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style61">
                                              <p class="auto-style57">
                                                  All Documents 
                                                  Must Be Viewed or Uploaded and signed!</p>
                                          </td>
                                      </tr>
                                  </table>
                                  &nbsp;
                                  <table class="auto-style50">
                                      <tr>
                                          <td class="auto-style129">Owner / Landlord Signature</td>
                                          <td class="auto-style130">Last 4 Social<br />
                                              Security No.</td>
                                          <td>Date</td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style129">
                                              <asp:TextBox ID="ownersign1csign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style130">
                                              <asp:TextBox ID="ownss1csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="owndate1csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style129">
                                              <asp:TextBox ID="ownsign2csign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style130">
                                              <asp:TextBox ID="ownss2csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="owndate2csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style129">
                                              <asp:TextBox ID="ownsign3csign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style130">
                                              <asp:TextBox ID="ownss3csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="owndate3csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style129">
                                              <asp:TextBox ID="ownsign4csign1Txt" runat="server" Width="215px"></asp:TextBox>
                                          </td>
                                          <td class="auto-style130">
                                              <asp:TextBox ID="ownss4csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                          <td>
                                              <asp:TextBox ID="owndate4csign1Txt" runat="server" Width="75px"></asp:TextBox>
                                          </td>
                                      </tr>
                                  </table>
                                  <table class="auto-style50">
                                      <tr>
                                          <td>&nbsp;</td>
                                          <td class="auto-style134">
                                              <asp:Button ID="ownsubmitcsign1Btn" runat="server" Text="Owner Submit" />
                                          </td>
                                          <td>&nbsp;</td>
                                      </tr>
                                  </table>
                                  <br />
                                  <table class="auto-style131">
                                      <tr>
                                          <td>A copy of all the signed rental documents will be placed in your tenant portal via your MyFileit virtual safety deposit box account after the Owner / Landlord signs the documents with directions of how to access and view it.</td>
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
                                                  <asp:Label ID="amtd1csign1Lbl" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style72">One Month Rent:</td>
                                              <td class="auto-style70">
                                                  <asp:Label ID="amtd2csign1Lbl" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style71">Pro-rated Amount:</td>
                                              <td class="auto-style60">
                                                  <asp:Label ID="amtd3csigne1Ltb" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style71">First Months Rent:</td>
                                              <td class="auto-style60">
                                                  <asp:Label ID="amtd4csign1Lbl" runat="server" Text="Label"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style71">&nbsp;</td>
                                              <td class="auto-style60">&nbsp;</td>
                                          </tr>
                                          <tr>
                                              <td class="auto-style71">Total Due</td>
                                              <td class="auto-style60">
                                                  <asp:Label ID="amtd6csign1Lbl" runat="server" Text="Label"></asp:Label>
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
                                              <td class="auto-style74">&nbsp; &nbsp;<asp:RadioButton ID="ccreditcsign1RBtn" runat="server" Text="Credit Card" />
                                              </td>
                                              <td class="auto-style75">
                                                  <asp:RadioButton ID="checkacctcsign1RBtn" runat="server" Text="Checking Account" />
                                              </td>
                                              <td>
                                                  <asp:RadioButton ID="cashcsign1RBtn" runat="server" Text="Cash" />
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      Billing Information:<br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style76">*Account Name:</td>
                                              <td>
                                                  <asp:TextBox ID="signanamecsign1Txt" runat="server" Width="305px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style74">*Address1:</td>
                                              <td>
                                                  <asp:TextBox ID="signaddress1csign1Txt" runat="server" Width="305px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style74">Address2</td>
                                              <td>
                                                  <asp:TextBox ID="signaddress2csign1Txt" runat="server" Width="305px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style77">*Country:</td>
                                              <td class="auto-style78">
                                                  <asp:DropDownList ID="countrycsign1DDList" runat="server" Width="116px">
                                                  </asp:DropDownList>
                                              </td>
                                              <td class="auto-style79">Region:</td>
                                              <td>
                                                  <asp:TextBox ID="signregioncsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style80">*City:</td>
                                              <td class="auto-style81">
                                                  <asp:TextBox ID="signcitycsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                              <td class="auto-style82">State:</td>
                                              <td class="auto-style83">
                                                  <asp:DropDownList ID="signstatecsign1DDList" runat="server">
                                                  </asp:DropDownList>
                                              </td>
                                              <td class="auto-style84">Zip Code:</td>
                                              <td>
                                                  <asp:TextBox ID="signzipcodecsign1Txt" runat="server" Width="98px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <span class="auto-style92">For Credit Card Use Only:</span><br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style85">Credit Card No.:</td>
                                              <td class="auto-style86">
                                                  <asp:TextBox ID="signcccsign1Txt" runat="server" Width="123px"></asp:TextBox>
                                              </td>
                                              <td class="auto-style88">Exp. Date:</td>
                                              <td class="auto-style87">Month</td>
                                              <td class="auto-style89">
                                                  <asp:DropDownList ID="signmonthcsign1DDList" runat="server">
                                                  </asp:DropDownList>
                                              </td>
                                              <td class="auto-style90">Year:</td>
                                              <td>
                                                  <asp:DropDownList ID="signyearcsign1DDList" runat="server">
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style91">CVS No. 3-4 digit number:</td>
                                              <td>
                                                  <asp:TextBox ID="signcvscsign1Txt" runat="server" CssClass="auto-style118"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <i>For Checking Account Use Only:</i><br />
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style113">Routing Number (2nd from bottom left #)</td>
                                              <td>
                                                  <asp:TextBox ID="signchknumcsign1Txt" runat="server" CssClass="auto-style112" Width="270px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style113">Re-enter Routing Number:</td>
                                              <td>
                                                  <asp:TextBox ID="signrertnumcsign1Txt" runat="server" Width="270px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style113">Account Number: (next set of numbers left of :)</td>
                                              <td>
                                                  <asp:TextBox ID="signacctnumcsign1Txt" runat="server" Width="270px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style113">Re-Enter Account Number:</td>
                                              <td>
                                                  <asp:TextBox ID="signreacctnumcsign1Txt" runat="server" Width="270px"></asp:TextBox>
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
                                                  <asp:CheckBox ID="signautopaycsign1CheckBox" runat="server" Text=" Please automatically debit the above account on the 1st day of each month for  the monthly rental payment. " />
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td>&nbsp;</td>
                                              <td class="auto-style111">
                                                  <asp:Button ID="csign1creditsubmitBtn" runat="server" CssClass="auto-style94" Text="Submit Payment" />
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
                                                  <asp:TextBox ID="signcashpnamecsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                              <td class="auto-style114">Last 4 license-State</td>
                                              <td class="auto-style100">
                                                  <asp:TextBox ID="signcashlast4csign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style108">&lt;company name&gt;</td>
                                              <td>
                                                  <asp:TextBox ID="signcashcnamecsign1Txt" runat="server" Width="356px"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td>as an authorized agent or Landlord received in cash the amount of&nbsp;
                                                  <asp:TextBox ID="signcashamountcsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style101">on</td>
                                              <td class="auto-style102">mm/dd/cy</td>
                                              <td class="auto-style103">
                                                  <asp:TextBox ID="signcashdatecsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                              <td class="auto-style104">at &lt;location&gt;</td>
                                              <td>
                                                  <asp:TextBox ID="signcashlocationcsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <table class="auto-style50">
                                          <tr>
                                              <td class="auto-style105">Signature:</td>
                                              <td class="auto-style106">
                                                  <asp:TextBox ID="signcashsigncsign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                              <td class="auto-style98">Last 4 License-State</td>
                                              <td>
                                                  <asp:TextBox ID="signcashlic4csign1Txt" runat="server"></asp:TextBox>
                                              </td>
                                          </tr>
                                      </table>
                                      <br />
                                      <table class="auto-style116">
                                          <tr>
                                              <td class="auto-style107">Email Address to Send Receipt:</td>
                                              <td>
                                                  <asp:TextBox ID="signcashrecemailcsign1Txt" runat="server" CssClass="auto-style115" Width="317px"></asp:TextBox>
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
                                      <asp:Button ID="csign1cancelBtn" runat="server" Text="Cancel" />
                                  </td>
                                  <td>
                                      <asp:Button ID="csign1savecsign1Btn" runat="server" Text="Submit &amp; Continue &gt;" />
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
                                  <td class="auto-style48">&nbsp;&nbsp; &nbsp;&nbsp;<asp:Image ID="bannerad2csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br/>
                          <table class="auto-style49">
                              <tr>
                                  <td>&nbsp; &nbsp;<asp:Image ID="bannerad3csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad4csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad5csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad6csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad7csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="banerad8csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad9csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:Image ID="bannerad10csign1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
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