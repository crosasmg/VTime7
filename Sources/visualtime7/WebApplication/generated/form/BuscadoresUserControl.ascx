﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BuscadoresUserControl.ascx.vb" Inherits="BuscadoresUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgIrInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="IrInformationMessageResource"></asp:Localize>';
    var titleIrInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleIrInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/Buscadores.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="BuscadoresUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >


  
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='BuscadoresTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GoogleLabel' EncodeHtml='false' ClientInstanceName='GoogleLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GoogleLabelResource"  Text="Google"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Google'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Google' ClientInstanceName='Google' ToolTip="Google" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="GoogleResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Buscadores" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YahooLabel' EncodeHtml='false' ClientInstanceName='YahooLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YahooLabelResource"  Text="Yahoo"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Yahoo'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Yahoo' ClientInstanceName='Yahoo' ToolTip="Yahoo" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="YahooResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Buscadores" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BingLabel' EncodeHtml='false' ClientInstanceName='BingLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BingLabelResource"  Text="Bing"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Bing'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Bing' ClientInstanceName='Bing' ToolTip="Bing" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="BingResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Buscadores" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AskLabel' EncodeHtml='false' ClientInstanceName='AskLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AskLabelResource"  Text="Ask"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Ask'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Ask' ClientInstanceName='Ask' ToolTip="Ask" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="AskResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Buscadores" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='Ir' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='True' ClientEnabled='True' meta:resourcekey="IrResource" Text="Ir a los cuatro"  OnClick='Ir_Click' AutoPostBack='false'>
<ClientSideEvents  Click="IrClick" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
  </tr>
</table>

    <table style="width: 100%;">
        <tr valign='top'>
            <td>
                <br />
                <asp:UpdatePanel ID="UpdatePanelErrors" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="ErrorsGridView" runat="server" AutoGenerateColumns="False" Visible="False"
                            SkinID="Main" Width="74px">
                            <Columns>
                                <asp:TemplateField HeaderText="" SortExpression="Severity">
                                    <ItemTemplate>
                                        <asp:Image ID="imgButton" runat="server" ImageUrl='<%# eval("SeverityImage") %>'></asp:Image>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# eval("Category") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ErrorButton" runat="server" Text='<%# eval("Message") %>' CommandArgument="<%# DirectCast(Container,GridViewRow).RowIndex %>" CommandName='<%# eval("ControlIdFullPath") %>'> </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:Label ID="ErrorLabel" runat="server" Font-Bold="True" ForeColor="Red" Text="Errors"></asp:Label>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>       
<dxpc:ASPxPopupControl ShowPageScrollbarWhenModal="true" ID="popControl" runat="server"  ClientInstanceName="popControl"
            ShowCloseButton="False" CloseAction="None" Modal="True" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"             
            EnableHotTrack="False" >
        <SizeGripImage Height="16px" Width="16px" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <CloseButtonImage Height="12px" Width="13px" />
        <HeaderStyle>
        <Paddings PaddingRight="6px" />
        </HeaderStyle>
        <Windows>
        <dxpc:PopupWindow Name="pwUno" Modal="true">
        <ContentCollection>
        <dxpc:PopupControlContentControl>     
        <table width="100%" >
        <tr>
        <td width="100%" colspan="2" align="center">
        <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage"> </dxe:ASPxLabel>
            <br />
            &nbsp;
        </td>  
        </tr>
        <tr>
            <td width="50%" align="right">
                    <dxe:ASPxButton ID="btnConfirm" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnConfirm" Text="Confirm" >
                        <ClientSideEvents Click="function(s,e){ 
                            var window = popControl.GetWindowByName('pwUno');                                                    
                           
                           if (msgIrConfirmationMessageResource!=''){
                            document.getElementById(btnCancel.name).style.visibility = 'hidden';
                            document.getElementById(btnConfirm.name).style.visibility = 'hidden';
                            document.getElementById(lblMessage.name).innerHTML = msgIrConfirmationMessageResource;                     
                            window.SetHeaderText('Message');                           
                            
                            window.popupControl.ShowWindow(window);
                           }
                           
                    }" />
                    </dxe:ASPxButton>
            </td>            
            <td width="50%">
                    <dxe:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnCancel" Text="Cancel">
                        <ClientSideEvents Click="function(s,e){ASPxClientPopupControl.GetPopupControlCollection().HideAllWindows();}" />
                    </dxe:ASPxButton>
            </td>
            </tr>
        </table>
        </dxpc:PopupControlContentControl>        
        </ContentCollection>        
        </dxpc:PopupWindow>        
        </Windows>        
        </dxpc:ASPxPopupControl>
  <asp:Label ID="_FormID" runat="server" Visible="False"></asp:Label>
  <table id='MessageTable' runat='server' style='width: 100%;' visible='false'>
      <tr valign='top' align='center'>
          <td>
					  <div class='FormMessage'>
		            <dxe:ASPxLabel EncodeHtml='false' ClientInstanceName='FormMessageLabel' ID='FormMessageLabel' ClientIDMode='Static' runat='server' Text=''  >
		            </dxe:ASPxLabel>
					  </div>
          </td>
      </tr>
  </table>  
  <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" SkinID="GroupBoxRoundedBorder"
                   ID="popupDelete" runat="server" ClientInstanceName="popupDelete" EnableHotTrack="False" >
                 <SizeGripImage Height="16px" Width="16px" />
                 <ClientSideEvents Init="function(s,e){
                                    popupDelete_Init(popupDelete)                        
                                    } " />
                                <ContentCollection>
                                    <dxpc:popupcontrolcontentcontrol ID="Popupcontrolcontentcontrol1" runat="server">
                                    <uc1:ConfirmDelete ID="ConfirmDelete1" runat="server" />
                                    </dxpc:popupcontrolcontentcontrol>  
                                </ContentCollection>
                 <CloseButtonImage Height="12px" Width="13px" />
                 <HeaderStyle>
                 <Paddings PaddingRight="6px" />
                 </HeaderStyle>
                </dxpc:ASPxPopupControl>
    
    <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupNotifyMessage" HeaderText=" "
        runat="server" ClientInstanceName="popupNotifyMessage" EnableHotTrack="False" Height="26px">
        <ModalBackgroundStyle>
            <BackgroundImage HorizontalPosition="center" />
        </ModalBackgroundStyle>
        <SizeGripImage Height="16px" Width="16px" />
        <ContentCollection>
            <dxpc:PopupControlContentControl>
                <table style='width: 100%;'>
                    <tr>
                        <td rowspan="2">
                            <dxe:ASPxImage ID="MessageImage" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                            </dxe:ASPxImage>
                        </td>
                        <td>
                            <asp:Literal ID='NotifyMessageLabel' Text="" runat='server' >
                            </asp:Literal>
                        </td>
                    </tr>
                </table>
                <br />
                <table style='width: 100%;'>
                    <tr>
                        <td rowspan="2" align='Center'>
                            <dxe:ASPxButton ID="btnOkNotificy" runat="server" AutoPostBack="False" Text="<%$ Resources:Resource, AcceptBtnResource %>"
                                Height="22px" Width="60px">
                                 <ClientSideEvents Click="function(s, e) {popupNotifyMessage.Hide(); }" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <CloseButtonImage Height="12px" Width="13px" />
        <HeaderStyle>
            <Paddings PaddingRight="6px" />
        </HeaderStyle>
    </dxpc:ASPxPopupControl>    
      
      
</ContentTemplate>
</asp:UpdatePanel>