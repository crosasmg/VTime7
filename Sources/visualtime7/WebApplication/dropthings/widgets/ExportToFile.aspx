<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExportToFile.aspx.vb" Inherits="ExportToFile" %>

  <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxwgv:ASPxGridView ID="grExport" runat="server" Width="100%" 
            ClientInstanceName="grExport">
            <SettingsPager Visible="False">
            </SettingsPager>
        </dxwgv:ASPxGridView>
    </div>
    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server" 
        GridViewID="grExport">
    </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
