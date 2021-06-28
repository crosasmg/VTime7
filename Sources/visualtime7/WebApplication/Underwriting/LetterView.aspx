<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LetterView.aspx.vb" Inherits="Underwriting_LetterView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="height: 100%; width: 100%">
            <tr align="center" valign='top' style="height: 1%">
                <td width="50px">
                    <div align="left">
                        <dxe:ASPxLabel ID="ToLabel" runat="server" Text="To" meta:resourcekey="ToLabelResource"
                            AssociatedControlID="ToTextBox">
                        </dxe:ASPxLabel>
                    </div>
                </td>
                <td align="justify" width="80%">
                    <dxe:ASPxTextBox ID="ToTextBox" runat="server" Paddings-PaddingLeft="20px" meta:resourcekey="ToTextBoxResource"
                        Width="100%">
                        <BackgroundImage HorizontalPosition="left" ImageUrl="../images/16x16/General/person.png"
                            Repeat="NoRepeat" VerticalPosition="center" />
                    </dxe:ASPxTextBox>                    
                </td>
                <td align="justify" width="25px">
                <dxe:ASPxImage ID="StatusImage" runat="server" ClientInstanceName="StatusImage" IsPng="True" ImageAlign="Middle" ImageUrl="~/images/empty.png" /> 
                  </td>
            </tr>
            <tr align="center" valign='top' style="height: 1%">
                <td width="50px">
                    <div align="left">
                        <dxe:ASPxLabel ID="SubjectLabel" runat="server" Text="Subject" meta:resourcekey="SubjectLabelResource"
                            AssociatedControlID="SubjectTextBox">
                        </dxe:ASPxLabel>
                    </div>
                </td>
                <td align="justify" width="80%">
                    <dxe:ASPxTextBox ID="SubjectTextBox" runat="server" Paddings-PaddingLeft="20px" Width="100%"
                        meta:resourcekey="SubjectTextBoxResource">
                        <BackgroundImage ImageUrl="~/Underwriting/Images/subject.png" Repeat="NoRepeat" VerticalPosition="center" />
                    </dxe:ASPxTextBox>
                   
                </td>
                  <td align="justify" width="25px">
                  </td>
            </tr>
            <tr align="center" valign='top' style="height: 1%">
                <td align="left" width="100%" colspan="3">                   
                        <dxhe:ASPxHtmlEditor ID="BodyHtmlEditor" runat="server" Height="395px" Width="100%" Settings-AllowDesignView="False">
<Settings AllowDesignView="False" AllowHtmlView="False"></Settings>
                        </dxhe:ASPxHtmlEditor>                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
