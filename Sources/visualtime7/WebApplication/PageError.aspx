<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PageError.aspx.vb" Inherits="generated_pageError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxpc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" 
            CloseAction="CloseButton" Modal="True" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
            EnableHotTrack="False" ShowOnPageLoad="true" 
            AllowDragging="True" AllowResize="True" ShowPageScrollbarWhenModal="True" 
            HeaderText="Ha ocurrido un error" Height="100%" Width="100%" >
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server"><%=Me.message%></dxpc:PopupControlContentControl>
</ContentCollection>
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
