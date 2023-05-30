<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonReportViewer.aspx.cs" Inherits="eProperty.Pages.Report.CommonReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Italic="True"  Font-Size="X-Large" ForeColor="#CC3300"></asp:Label>
<%--<form id="form2" runat="server" style="float: left;margin-left: 125px;">
    <rsweb:ReportViewer ID="ReportViewer2" runat="server" SizeToReportContent="True"></rsweb:ReportViewer>
    <asp:ScriptManager ID="ScriptManager2" runat="server" >            
    </asp:ScriptManager>
</form>--%>

    <form id="form1" runat="server"  style="float: left;margin-left: 125px;">
        
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server"  AsyncRendering="false" Font-Names="Verdana"  SizeToReportContent="True"></rsweb:ReportViewer>
    </div>
        
    </form>
</body>
</html>
