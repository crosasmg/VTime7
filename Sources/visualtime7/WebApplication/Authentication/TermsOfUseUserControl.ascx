<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TermsOfUseUserControl.ascx.vb"
    Inherits="TermsOfUseUserControl" %>
<table width="100%" align="center">
    <tr align="center">
        <td width="100%" colspan="2" align="left">
            <asp:Panel ID="TermsPanel" runat="server"  ScrollBars="Vertical"
                Height="200px" Width="100%">

            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr align="center">
        <td width="100%" colspan="2">
            <div align="center">
                <dxe:ASPxCheckBox ID="AcceptTheTermsCheckBox" runat="server" Text="I accept the terms"
                    meta:resourcekey="AcceptTheTermsCheckBoxResource">
                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="You should accept the terms of the contract" />
                    </ValidationSettings>
                </dxe:ASPxCheckBox>
            </div>
        </td>
    </tr>
</table>
  

