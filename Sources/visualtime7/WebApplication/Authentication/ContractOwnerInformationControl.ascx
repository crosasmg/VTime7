<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ContractOwnerInformationControl.ascx.vb" Inherits="ContractOwnerInformationControl" %>



  <table width="50%" align="left">
        <tr align="center">
            <td width="30%">
             <dxe:ASPxLabel ID="ContractLabel" meta:resourcekey="ContractLabelResource"
              runat="server" Text="Client identifier code" AssociatedControlID="RegistrationCodeTextBox">
</dxe:ASPxLabel>
            </td>
            <td align="left">
              <dxe:ASPxTextBox ID="RegistrationCodeTextBox"  meta:resourcekey="RegistrationCodeTextBoxResource" 
              runat="server" Paddings-PaddingLeft="8px" width="150px" MaxLength="14" Size="14">
    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
        Repeat="NoRepeat" VerticalPosition="center" />
    <ValidationSettings ErrorDisplayMode="Text">
        <RequiredField IsRequired='True' ErrorText='The registration code is required' />
    </ValidationSettings>
</dxe:ASPxTextBox>
            </td>
        </tr>       
    </table>