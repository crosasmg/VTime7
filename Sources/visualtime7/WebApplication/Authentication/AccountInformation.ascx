<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AccountInformation.ascx.vb"
    Inherits="AccountInformationControl" %>
<table style='width: 100%;'>
    <tr valign='top'>  
        <td style='padding-top: 3px;' colspan='1' align='left'>
           <dxe:ASPxLabel ID="UserNameLabel" runat="server" Text="User Name" AssociatedControlID="UserNameTextBox"
                    meta:resourcekey="UserNameLabelResource">
                </dxe:ASPxLabel>
        </td>
        <td align='left'>
             <dxe:ASPxTextBox ID="UserNameTextBox" runat="server" Paddings-PaddingLeft="8px" AutoPostBack="true"
                Width="150px" meta:resourcekey="UserNameTextBoxResource">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The user name is required' />
                </ValidationSettings>
            </dxe:ASPxTextBox>
            <dxe:ASPxLabel ID="InvalidUserLabel" runat="server" Text="" ForeColor="Red" Visible="false" />
        </td>
       
    </tr>
    <tr valign='top'>
        <td style='padding-top: 3px;' colspan='1' align='left'>
                <dxe:ASPxLabel ID="EmailAddressLabel" runat="server" Text="Email Address" AssociatedControlID="EmailAddressTextBox"
                    meta:resourcekey="EmailAddressLabelResource">
                </dxe:ASPxLabel>
        </td>
        <td align='left'>
          <dxe:ASPxTextBox ID="EmailAddressTextBox" runat="server" Paddings-PaddingLeft="8px" AutoPostBack="true"
                meta:resourcekey="EmailAddressTextBoxResource" Width="240px">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The e-mail address is required' />
                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$"
                        ErrorText="The format is invalid" />
                </ValidationSettings>
            </dxe:ASPxTextBox>
              <dxe:ASPxLabel ID="InvalidEmailLabel" runat="server" Text="" ForeColor="Red" Visible="false" />
        </td>
        <td style='padding-top: 3px;' colspan='1' align='left'>
              <dxe:ASPxLabel ID="VerifyEmailLabel" runat="server" Text="Verify Email" AssociatedControlID="VerifyEmailTextBox"
                    meta:resourcekey="VerifyEmailLabelResource">
                </dxe:ASPxLabel>
        </td>
        <td align='left'>
         <dxe:ASPxTextBox ID="VerifyEmailTextBox" runat="server" Paddings-PaddingLeft="8px"
                Width="240px" meta:resourcekey="VerifyEmailTextBoxResource">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The e-mail address is required' />
                    <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$"
                        ErrorText="The format is invalid" />
                </ValidationSettings>
            </dxe:ASPxTextBox>
            <asp:CompareValidator ID="EmailCompareValidator" runat="server" ControlToValidate="EmailAddressTextBox"
                meta:resourcekey="EmailCompareValidatorResource" ControlToCompare="VerifyEmailTextBox"
                ErrorMessage="The e-mail address should identical to the previous." ForeColor="Red" />
        </td>
     
    </tr>
    <tr valign='top'>
      
         <td style='padding-top: 3px;' colspan='1' align='left'>
                      <dxe:ASPxLabel ID="SecurityQuestionLabel" runat="server" Text="Security Question"
                    AssociatedControlID="SecurityQuestionMemo" meta:resourcekey="SecurityQuestionLabelResource">
                </dxe:ASPxLabel>
        </td>
        <td align='left'>
           <dxe:ASPxMemo ID="SecurityQuestionMemo" runat="server" Width="350px" meta:resourcekey="SecurityQuestionMemoResource">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The security question is required' />
                </ValidationSettings>
            </dxe:ASPxMemo>
        </td>
          <td style='padding-top: 3px;' colspan='1' align='left'>
            <dxe:ASPxLabel ID="SecurityAnswerLabel" runat="server" Text="Security Answer" AssociatedControlID="SecurityAnswerTextBox"
                    meta:resourcekey="SecurityAnswerLabelResource">
                </dxe:ASPxLabel>
        </td>
        <td align='left'>
          <dxe:ASPxTextBox ID="SecurityAnswerTextBox" runat="server" Paddings-PaddingLeft="8px"
                Width="240px" meta:resourcekey="SecurityAnswerTextBoxResource">
                <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                    Repeat="NoRepeat" VerticalPosition="center" />
                <ValidationSettings ErrorDisplayMode="Text">
                    <RequiredField IsRequired='True' ErrorText='The security answer is required' />
                </ValidationSettings>
            </dxe:ASPxTextBox>
        </td>
    </tr>
</table>













