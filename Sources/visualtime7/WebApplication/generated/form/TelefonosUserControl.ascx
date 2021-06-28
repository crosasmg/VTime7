<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TelefonosUserControl.ascx.vb" Inherits="TelefonosUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">



</script>

<script src="/generated/form/Telefonos.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="TelefonosUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='TelefonosTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
<dxe:ASPxButton ID="btnADDPhone" Visible="True" Enabled="True" Image-Url="~/images/generaluse/new.gif" Text="" meta:resourcekey="PhoneGridBtnResource" runat="server" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                    Phone.AddNewRow();
                }" />
            </dxe:ASPxButton>            
   
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='Phone' EnableRowsCache='False' EnableViewState='False' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='Phone' runat='server' Width='100%' KeyFieldName='KeyPhone' Caption="Phone" meta:resourcekey="PhoneResource"
>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { Phone.StartEditRow(e.visibleIndex); }" />
            <SettingsEditing Mode="Inline" />
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='KeyPhone' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='TelephoneType' FieldName='TelephoneType' ToolTip="Tipo de Teléfono" Caption="Tipo de Teléfono" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="TelephoneTypeFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NPHONE_TYPE'>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Phone" >

               </ValidationSettings>
<ClientSideEvents Validation="TelephoneTypeOnValidation" /> 

</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='CountryCode' FieldName='CountryCode' ToolTip="Código del país" Caption="Código del país" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="CountryCodeFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-99999..99999g>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='AreaCode' FieldName='AreaCode' ToolTip="Área" Caption="Área" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="AreaCodeFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-99999..99999g>' />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Phone" >

               </ValidationSettings>
<ClientSideEvents Validation="AreaCodeOnValidation" /> 

           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PhoneNumber' FieldName='PhoneNumber' ToolTip="Número telefónico" Caption="Número telefónico" GroupIndex="-1" VisibleIndex="3" meta:resourcekey="PhoneNumberFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='11' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Phone" >

               </ValidationSettings>
<ClientSideEvents Validation="PhoneNumberOnValidation" /> 

           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn Caption=" " >
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

    <dxlp:ASPxLoadingPanel ID="LoadingPanelGridView" runat="server" ClientInstanceName="LoadingPanelGridView"  Modal="True" Text="<%$ Resources:Resource, Working %>" />
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
                        <ClientSideEvents Click="function(s,e){Confirmation_Actions();}" />
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