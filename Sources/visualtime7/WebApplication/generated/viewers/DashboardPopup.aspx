<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DashboardPopup.aspx.vb" Inherits="DashboardPopup" %>

<%@ Register Assembly="DevExpress.Dashboard.v13.1.Web, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxDashboardViewer ID="ASPxDashboardViewer1" runat="server" 
            AllowExportDashboardItems="True"  
             FullscreenMode="True" RegisterJQuery="True">
        </dx:ASPxDashboardViewer>
    </div>
    </form>
</body>
</html>
