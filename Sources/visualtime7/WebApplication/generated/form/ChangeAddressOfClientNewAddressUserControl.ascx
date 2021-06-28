<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChangeAddressOfClientNewAddressUserControl.ascx.vb" Inherits="ChangeAddressOfClientNewAddressUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgSendInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="SendInformationMessageResource"></asp:Localize>';
    var titleSendInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleSendInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/ChangeAddressOfClientNewAddress.js" type="text/javascript"></script>      
<asp:UpdatePanel runat="server">

<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='ChangeAddressOfClientNewAddressTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zonegeneral" ClientInstanceName="zonegeneral" runat="server" HeaderText="Cambio de la dirección de un cliente" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zonegeneralResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0" ClientInstanceName="zone0" runat="server" HeaderText="zone" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:5%;' colspan='2' align='left'>

<dxe:ASPxImage ID="image0" runat="server" ToolTip="image 0" ClientEnabled="True" ClientVisible="True" ClientIDMode='Static' ImageUrl="/images/Banners/Clients/2.jpg" meta:resourcekey="image0Resource"  Width="40px" > 
</dxe:ASPxImage>
    </td>

    <td style='width:20%;' align='right'>       <dxe:ASPxLabel ID='ProcessDateLabel' EncodeHtml='false' ClientInstanceName='ProcessDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ProcessDateLabelResource"  Text="Fecha de modificaci&#243;n"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='ProcessDate'       ></dxe:ASPxLabel><br />

       <dxe:ASPxDateEdit runat='server' ID='ProcessDate' ToolTip="datepicker0" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="ProcessDateResource" ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

<td style='width:37.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ClientIDLabel' EncodeHtml='false' ClientInstanceName='ClientIDLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClientIDLabelResource"  Text="Cliente"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClientID'       ></dxe:ASPxLabel></td>    <td style='width:37.5%;' align='left'>


<dxe:ASPxComboBox ID='ClientID' runat='server' ClientInstanceName='ClientID' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Código de Cliente" ClientVisible='true' ClientEnabled='True' meta:resourcekey="ClientIDResource"  Width='100%'  ValueType='System.String'  TextFormatString="{1}" DropDownStyle= "DropDown" EnableCallbackMode="true" IncrementalFilteringMode="Contains" CallbackPageSize="20" DropDownRows="20" AutoResizeWithContainer="false" IncrementalFilteringDelay="500" FilterMinLength = "0" OnItemsRequestedByFilterCondition="ClientID_OnItemsRequestedByFilterCondition" OnItemRequestedByValue="ClientID_OnItemRequestedByValue"  AutoPostBack='true' OnSelectedIndexChanged='ClientID_SelectedIndexChanged'  TextField='SCLIENAME' ValueField='SCLIENT'>
           <Columns>
                <dxe:ListBoxColumn FieldName="SCLIENT" Caption="Código" Visible="True"  meta:resourcekey="ClientIDColumnSCLIENTResource"/>
                <dxe:ListBoxColumn FieldName="SCLIENAME" Caption="Nombre" Visible="True"  meta:resourcekey="ClientIDColumnSCLIENAMEResource"/>
            </Columns>
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  Validation="ClientIDValidation" />
</dxe:ASPxComboBox>
    </td>

  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone2" ClientInstanceName="zone2" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone2Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
<dxe:ASPxButton ID="btnADDAddressDLI" Visible="True" Enabled="True" Image-Url="~/images/generaluse/new.gif" Text="" meta:resourcekey="AddressDLIGridBtnResource" runat="server" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                    AddressDLI.AddNewRow();
                }" />
            </dxe:ASPxButton>            
   
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='AddressDLI' EnableRowsCache='False' EnableViewState='False' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='AddressDLI' runat='server' Width='100%' KeyFieldName='InternalAddressKey' Caption="Direcciones" meta:resourcekey="AddressDLIResource"
>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { AddressDLI.StartEditRow(e.visibleIndex); }" />
            <SettingsEditing Mode="Inline" />
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn Name='InternalAddressKey' FieldName='InternalAddressKey' ToolTip="Llave interna de la dirección" Caption="Llave interna de la dirección" VisibleIndex="0" meta:resourcekey="InternalAddressKeyFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="##,###,###,##0">
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='AddressDescription' FieldName='AddressDescription' ToolTip="Descripción de la dirección" Caption="Descripción" VisibleIndex="1" meta:resourcekey="AddressDescriptionFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="">
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn>
                                    <EditButton Visible="True" Text="Editar" />
                                    <DeleteButton Visible="True" Text="Eliminar" />
<CancelButton Visible="True" Text="Cancel" />
                                    <UpdateButton Visible="True" Text="Actualizar" />
                                </dxwgv:GridViewCommandColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </td>
  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone4" ClientInstanceName="zone4" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone4Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone1" ClientInstanceName="zone1" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone1Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:33%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='Reject' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="El cliente no desea registrar la modificación. Finaliza el proceso." ClientVisible='True' ClientEnabled='True' meta:resourcekey="RejectResource" Text="Rechazar" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/delete_16x16.gif"  OnClick='Reject_Click' AutoPostBack='true'>
       </dxe:ASPxButton>
    </td>

    <td style='width:33%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='Send' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Registrar la modificación, en espera de la confirmación de la compañía de seguros." ClientVisible='True' ClientEnabled='True' meta:resourcekey="SendResource" Text="Enviar" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/mail2_(add)_16x16.gif"  OnClick='Send_Click' AutoPostBack='false'>
<ClientSideEvents  Click="SendClick" />
       </dxe:ASPxButton>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
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
                           
                           if (msgRejectConfirmationMessageResource!=''){
                            document.getElementById(btnCancel.name).style.visibility = 'hidden';
                            document.getElementById(btnConfirm.name).style.visibility = 'hidden';
                            document.getElementById(lblMessage.name).innerHTML = msgRejectConfirmationMessageResource;                     
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