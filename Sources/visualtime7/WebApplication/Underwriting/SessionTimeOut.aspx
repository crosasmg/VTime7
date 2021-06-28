<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SessionTimeOut.aspx.vb"
    Inherits="Underwriting_SessionTimeOut" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
    
        function btnYes_Click(s, e) {
            popupDelete.Hide();
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupDelete"
            runat="server" ClientInstanceName="popupDelete" Modal="True" CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css"
            CssPostfix="Office2003_Olive" EnableHotTrack="False" 
            ImageFolder="~/App_Themes/Office2003 Olive/{0}/" 
            meta:resourcekey="popupDeleteResource1">
            <ModalBackgroundStyle>
                <BackgroundImage HorizontalPosition="center" VerticalPosition="center" />
            </ModalBackgroundStyle>
            <SizeGripImage Height="16px" Width="16px" />
            <CloseButtonImage Height="12px" Width="13px" />
            <HeaderTemplate>
                <div>
                    Sesión Expirada</div>
            </HeaderTemplate>
            <HeaderStyle>
                <Paddings PaddingRight="6px" />
            </HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server"
                    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" CssPostfix="Office2003_Olive"
                    EnableHotTrack="False" ImageFolder="~/App_Themes/Office2003 Olive/{0}/" 
                    meta:resourcekey="PopupControlContentControl2Resource1">
                    <div style="width: 350px">
                        <table>
                            <tr>
                                <td rowspan="2">
                                    <dxe:ASPxImage ID="ASPxImage1" runat="server" 
                                        ImageUrl="~/Images/exclamation.png" meta:resourcekey="ASPxImage1Resource1">
                                    </dxe:ASPxImage>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                        Text="Su sesión ha expirado, por favor ingrese nuevamente" 
                                        meta:resourcekey="ASPxLabel1Resource1">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr>
                                <td style="width: 100%">
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btnYes" runat="server" Width="50px" AutoPostBack="True" ClientInstanceName="btnYes"
                                        EnableDefaultAppearance="False" meta:resourcekey="btnYesResource1">
                                        <Image Url="~/Images/btnAcceptOff.png" UrlChecked="~/Images/btnAcceptOn.png" UrlPressed="~/Images/btnAcceptOn.png" />
                                        <ClientSideEvents Click="btnYes_Click" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
