<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="SendEmailAdministrator.aspx.vb" Inherits="Authentication_SendEmailAdministrator" meta:resourcekey="PageResource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table width="500px" align="center">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
  <tr align="center">
        <td width="20%">
            <div align="left">
              <dxe:ASPxLabel ID="EmailAddressLabel" runat="server" Text="Email Address" AssociatedControlID="EmailAddressTextBox" meta:resourcekey="EmailAddressLabelResource">
                    </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left">
             <dxe:ASPxTextBox ID="EmailAddressTextBox" runat="server" Paddings-PaddingLeft="8px" meta:resourcekey="EmailAddressTextBoxResource"
                    Width="200px">
                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                        Repeat="NoRepeat" VerticalPosition="center" />
                    <ValidationSettings ErrorDisplayMode="Text">
                        <RequiredField IsRequired='True' ErrorText='The Email Address is required' />
                    </ValidationSettings>
                </dxe:ASPxTextBox>
        </td>
    </tr>
     <tr align="center">
        <td width="20%">
            <div align="left">
              <dxe:ASPxLabel ID="CompleteNameLabel" runat="server" Text="Complete Name" AssociatedControlID="CompleteNameTextBox"
              meta:resourcekey="CompleteNameLabelResource">
                    </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left">
             <dxe:ASPxTextBox ID="CompleteNameTextBox" runat="server" Password="True" Paddings-PaddingLeft="8px"
                    meta:resourcekey="CompleteNameTextBoxResource" Width="200px">
                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                        Repeat="NoRepeat" VerticalPosition="center" />
                    <ValidationSettings ErrorDisplayMode="Text">
                        <RequiredField IsRequired='True' ErrorText='The complete name is required' />
                    </ValidationSettings>
                </dxe:ASPxTextBox>
        </td>
    </tr>
     
      <tr align="center">
        <td width="20%">
            <div align="left">
              <dxe:ASPxLabel ID="TelephoneLabel" runat="server" Text="Telephone No."
              meta:resourcekey="TelephoneLabelResource" AssociatedControlID="TelephoneTextBox">
                    </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left">
          <dxe:ASPxTextBox ID="TelephoneTextBox" runat="server" Width="150px" Paddings-PaddingLeft="8px"
           meta:resourcekey="TelephoneTextBoxResource">
                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                        Repeat="NoRepeat" VerticalPosition="center" />
                    <ValidationSettings ErrorDisplayMode="Text">
                        <RequiredField IsRequired='True' ErrorText='The telephone number is required' />
                    </ValidationSettings>
                </dxe:ASPxTextBox>
        </td>
    </tr>
          <tr align="center">
        <td width="20%">
            <div align="left">
             <dxe:ASPxLabel ID="CommentLabel" runat="server" Text="Comment" AssociatedControlID="CommentMemo"
             meta:resourcekey="CommentLabelResource">
                    </dxe:ASPxLabel>
            </div>
        </td>
        <td align="left">
          <dxe:ASPxMemo ID="CommentMemo" runat="server" Width="100%"
           meta:resourcekey="CommentMemoResource">
                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                        Repeat="NoRepeat" VerticalPosition="center" />
                    <ValidationSettings ErrorDisplayMode="Text">
                        <RequiredField IsRequired='True' ErrorText='The comment is required' />
                    </ValidationSettings>
                </dxe:ASPxMemo>
        </td>
    </tr>  
     <tr>
        <td align="center" colspan="2">
            <table width="100%">
                <tr>
                    <td width="50%" align="right">
                         <dxe:ASPxButton ID="SendButton" runat="server" Text="Send" meta:resourcekey="SendButtonResource">
                </dxe:ASPxButton>
                    </td>
                    <td width="50%" align="left">
                        <dxe:ASPxButton ID="CancelButton" runat="server" Text="Cancel" CausesValidation="false"
                            meta:resourcekey="CancelButtonResource" AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e) {window.location.href = '/dropthings/Default.aspx';} " />
                        </dxe:ASPxButton>                       
                    </td>
                </tr>
            </table>
        </td>
    </tr>     
    </table>
   
    <dxpc:ASPxPopupControl ShowPageScrollbarWhenModal="true" ID="popupMessageControl"
        runat="server" ClientInstanceName="popupMessageControl" ShowCloseButton="False"
        EnableHotTrack="False" CloseAction="None" Modal="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" Width="238px" HeaderText="Notification"  meta:resourcekey="popupMessageControlResource">
        <SizeGripImage Height="16px" Width="16px" />
        <CloseButtonImage Height="12px" Width="13px" />
        <HeaderStyle>
            <Paddings PaddingRight="6px" />
        </HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl>                
                 <table width="300px" align="center">
                    <tr align="center">
                        <td width="100%" colspan="2">
                            <div align="center">
                               <dxe:ASPxLabel ID="MessageLabel" ClientInstanceName="MessageLabel" runat="server"  meta:resourcekey="MessageLabelResource"
                                EnableViewState="False" Text="In few minutes the administrator of portal receive this email" />
                            </div>
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
                                <dxe:ASPxButton ID="btnOk" runat="server" AutoPostBack="False" ClientInstanceName="btnOk"
                                Text="Ok">
                                <ClientSideEvents Click="function(s, e) {window.location.href = '/dropthings/Default.aspx';} " />
                            </dxe:ASPxButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</asp:Content>

