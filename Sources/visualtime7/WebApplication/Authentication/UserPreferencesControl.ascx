<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserPreferencesControl.ascx.vb"
    Inherits="UserPreferencesControl" %>

<table style="width: 30%;" align="left">
    <tr>
        <td>
            <dxe:ASPxLabel ID="ThemeLabel" Text="Theme:" runat="server" meta:resourcekey="lblThemeResource1">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxComboBox ID="ThemeList" runat="server" meta:resourcekey="ddlThemeResource1">
                <Items>
                    <dxe:ListEditItem Text="None" />
                </Items>
            </dxe:ASPxComboBox>
        </td>
         <td>
            &nbsp;
        </td>
         <td>
            &nbsp;
        </td>
         <td>
            <dxe:ASPxLabel ID="LanguageLabel" runat="server" Text="Language: " meta:resourcekey="lblLanguageResource1">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxComboBox ID="Language" runat="server">
                <Items>
                    <dxe:ListEditItem Value="" meta:resourcekey="ListItemResourceDefault" Text="Browser Default" />
                    <dxe:ListEditItem Value="EN-US" meta:resourcekey="ListItemResourceEn" Text="English (United States)" />
                    <dxe:ListEditItem Value="ES-CR" meta:resourcekey="ListItemResourceEs" Text="Spanish (Costa Rica)" />
                </Items>
            </dxe:ASPxComboBox>
        </td>        
    </tr>   
</table>
