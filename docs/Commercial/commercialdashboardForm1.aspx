<%@ Page Language="C#" AutoEventWireup="true" CodeFile="commercialdashboardForm1.aspx.cs" Inherits="E365signup.commercialdashboardForm1" %>

<!DOCTYPE html>

<html>

<head runat="server">
 <title> Commercial / Industrial Tenant Dashoard</title>
</head>

  <body>&nbsp;<style type="text/css">
        .auto-style1 {
            width: 1024px;
            height: 1050px;
        }
        .auto-style4 {
            background-color: #e9e2e2; 
            border-style: solid;
            border-width: 1px;
            vertical-align: top;
            width: 606.017px;
            height: 645px;
        }
        .auto-style5 {
            font-size: medium;
            border:none;
        }
        .auto-style7 {
            text-align: center;
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
                  .auto-style74 {
                      width: 113px;
                  }
                  .auto-style109 {
                      width: 186px;
                  }
                  .auto-style111 {
                      width: 42px;
                  }
                  .auto-style112 {
                      width: 465px;
                  }
                  .auto-style113 {
                      width: 177px;
                  }
                  .auto-style114 {
                      width: 240px;
                  }
                  .auto-style115 {
                      height: 645px;
                  }
                  .auto-style116 {
                      margin-left: 26px;
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
                          <asp:Image ID="logocdash1Image" runat="server" ImageUrl="http://173.248.153.72/images/E-property-Logo-A_150_71_96.png" />
                          <br/>
                        </td>
                      <td style="width: 555.333px;"><br/>
                        <asp:image id="topBanner1cdash1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_728_90_96.jpg"> </asp:image></td>
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
                          <asp:Image ID="checkImagecdash1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk1.jpg" />
                          <br/>
                      </td>
                      <td class="auto-style40">
                          <asp:Image ID="checkImage2cdash1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk2.jpg" />
                      </td>
                      <td class="auto-style39">
                          <asp:Image ID="checkImage3cdash1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk3.jpg" />
                      </td>
                      <td class="auto-style34">
                          <asp:Image ID="checkImage4cdash1" runat="server" ImageUrl="http://173.248.153.72/images/App_chk4.jpg" />
                      </td>
                      <td><br/>
                      </td>
                    </tr>
                  </tbody>
                </table>
                  <table class="auto-style24">
                      <tr>
                          <td class="auto-style36"><asp:CheckBox ID="checkcdash1Box1" runat="server" BorderStyle="None" />
                          </td>
                          <td class="auto-style35">&nbsp;&nbsp;
                              <asp:CheckBox ID="checkcdash1Box2" runat="server" />
                          </td>
                          <td class="auto-style37"><asp:CheckBox ID="checkcdash1Box3" runat="server" />
                          </td>
                          <td class="auto-style46"><asp:CheckBox ID="checkcdash1Box4" runat="server" />
                          </td>
                          <td class="auto-style25"></td>
                      </tr>
                  </table>

                <table style="width: 1024px; height: 30px;" border="0">
                  <tbody>
                    <tr>
                      <td class="auto-style74">Tenant Name:   </td>
                        <td>
                            <asp:Label ID="usernamedash1Lbl" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                  </tbody>
                </table>
                <table style="width: 100%" border="0">
                  <tbody>
                    <tr>
                      <td style="width: 606.933px;" class="auto-style5">
                        <p class="auto-style7"> <b>Commercial / Industrial Tenant Dashboard </b></p>
                      </td>
                      <td style="width: 403.067px;"><br/>
                      </td>
                    </tr>
                  </tbody>
                </table>
                <br/>
                <table border="0" class="auto-style50">
                  <tbody>
                    <tr>
                      <td class="auto-style4"><br />
                          <table class="auto-style50">
                              <tr>
                                  <td>Manage account Options:</td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td class="auto-style113">&nbsp;&nbsp;&nbsp; &nbsp;Type Email Request:</td>
                                  <td>
                                      <asp:DropDownList ID="cdash1requestDDList" runat="server" CssClass="auto-style116">
                                          <asp:ListItem>Report Issue</asp:ListItem>
                                          <asp:ListItem>Request Maintenances</asp:ListItem>
                                          <asp:ListItem>Schedule Exit Inspection</asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                          </table>
                          <table class="auto-style50">
                              <tr>
                                  <td>&nbsp;&nbsp; &nbsp;<asp:TextBox ID="cdash1messagesTxt" runat="server" Height="278px" Width="532px"></asp:TextBox>
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td class="auto-style111">Send:</td>
                                  <td class="auto-style112">
                                      <asp:TextBox ID="cdash1sendTxt" runat="server" TextMode="MultiLine" Width="441px"></asp:TextBox>
                                  </td>
                                  <td>
                                      <asp:Button ID="sendcdash1Btn" runat="server" Text="Send" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td>Make Payment &amp; Setup Auto Pay</td>
                              </tr>
                          </table>
                          <table class="auto-style50">
                              <tr>
                                  <td>Pay History &amp; Accounting</td>
                              </tr>
                          </table>
                          <table class="auto-style50">
                              <tr>
                                  <td>Document Vault (MyFileit)</td>
                              </tr>
                          </table>
                          <br />
                          <table class="auto-style50">
                              <tr>
                                  <td class="auto-style114">&nbsp;</td>
                                  <td class="auto-style109">
                                      <asp:Button ID="cdash1cancelBtn" runat="server" Text="Cancel" />
                                  </td>
                                  <td>
                                      &nbsp;</td>
                                  <td>&nbsp;</td>
                              </tr>
                          </table>

                         </td>
                      <td style="vertical-align: top" class="auto-style115">
                          <br />
                          <table class="auto-style47">
                              <tr>
                                  <td class="auto-style48">&nbsp;&nbsp; &nbsp;&nbsp;<asp:Image ID="bannerad2csdash1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br/>
                          <table class="auto-style49">
                              <tr>
                                  <td>&nbsp; &nbsp;<asp:Image ID="bannerad3cdash1" runat="server" ImageUrl="http://173.248.153.72/images/BannerAd_PlaceHolder_335_280_96.jpg" />
                                  </td>
                              </tr>
                          </table>
                          <br />
                          <br />
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
