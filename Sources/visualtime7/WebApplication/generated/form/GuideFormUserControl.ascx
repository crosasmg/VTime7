<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GuideFormUserControl.ascx.vb" Inherits="GuideFormUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">



</script>

<script src="/generated/form/GuideForm.js" type="text/javascript"></script>      
<asp:UpdatePanel runat="server">


  
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='GuideFormTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CodeofguidetypeDescriptionLabel' EncodeHtml='false' ClientInstanceName='CodeofguidetypeDescriptionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CodeofguidetypeDescriptionLabelResource"  Text="Code of guide type."  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CodeofguidetypeDescription'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CodeofguidetypeDescription' ClientInstanceName='CodeofguidetypeDescription' ToolTip="Code of guide type." Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="CodeofguidetypeDescriptionResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="GuideForm" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CreationDateLabel' EncodeHtml='false' ClientInstanceName='CreationDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CreationDateLabelResource"  Text="Creation Date"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CreationDate'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='CreationDate' ToolTip="Computer date when the record is created." ClientIDMode='Static' ClientVisible='True' meta:resourcekey="CreationDateResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Form1" >
<RequiredField IsRequired='true' ErrorText="The field is required." />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CreatorUserCodeLabel' EncodeHtml='false' ClientInstanceName='CreatorUserCodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CreatorUserCodeLabelResource"  Text="Creator User Code"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CreatorUserCode'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CreatorUserCode' ClientInstanceName='CreatorUserCode' ToolTip="Code of the user creating the record." Size='9' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="CreatorUserCodeResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-999999999..999999999g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="The field is required." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="The field is required." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CurrencyCodeDescriptionLabel' EncodeHtml='false' ClientInstanceName='CurrencyCodeDescriptionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CurrencyCodeDescriptionLabelResource"  Text="Currency Code"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CurrencyCodeDescription'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CurrencyCodeDescription' ClientInstanceName='CurrencyCodeDescription' ToolTip="Code of the cuerrency." Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="CurrencyCodeDescriptionResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="GuideForm" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='FaceAmountLabel' EncodeHtml='false' ClientInstanceName='FaceAmountLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FaceAmountLabelResource"  Text="Face Amount"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='FaceAmount'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='FaceAmount' ClientInstanceName='FaceAmount' ToolTip="Face Amount of the guide." Size='19' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="FaceAmountResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-9999999999999999..9999999999999999g>.<00..99>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="GuideForm" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GuideIdLabel' EncodeHtml='false' ClientInstanceName='GuideIdLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GuideIdLabelResource"  Text="Guide Id"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='GuideId'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='GuideId' ClientInstanceName='GuideId' ToolTip="Guide Id" Size='9' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="GuideIdResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-999999999..999999999g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="The field is required." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="The field is required." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GuideReceptionDateLabel' EncodeHtml='false' ClientInstanceName='GuideReceptionDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GuideReceptionDateLabelResource"  Text="Guide Reception Date"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='GuideReceptionDate'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='GuideReceptionDate' ToolTip="Guide Reception Date." ClientIDMode='Static' ClientVisible='True' meta:resourcekey="GuideReceptionDateResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="GuideForm" >
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GuideReceptionUsercodeLabel' EncodeHtml='false' ClientInstanceName='GuideReceptionUsercodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GuideReceptionUsercodeLabelResource"  Text="Guide Reception Usercode"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='GuideReceptionUsercode'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='GuideReceptionUsercode' ClientInstanceName='GuideReceptionUsercode' ToolTip="Guide Reception Usercode." Size='5' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="GuideReceptionUsercodeResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="GuideForm" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GuidestatuscodeDescriptionLabel' EncodeHtml='false' ClientInstanceName='GuidestatuscodeDescriptionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GuidestatuscodeDescriptionLabelResource"  Text="Guide status code"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='GuidestatuscodeDescription'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='GuidestatuscodeDescription' ClientInstanceName='GuidestatuscodeDescription' ToolTip="Guide status code." Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="GuidestatuscodeDescriptionResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="GuideForm" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='PremiumLabel' EncodeHtml='false' ClientInstanceName='PremiumLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PremiumLabelResource"  Text="Premium"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Premium'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Premium' ClientInstanceName='Premium' ToolTip="Premium Amount of the guide." Size='19' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="PremiumResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-9999999999999999..9999999999999999g>.<00..99>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="GuideForm" >
</ValidationSettings>
       </dxe:ASPxTextBox>
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
                           
                           if (''!=''){
                            document.getElementById(btnCancel.name).style.visibility = 'hidden';
                            document.getElementById(btnConfirm.name).style.visibility = 'hidden';
                            document.getElementById(lblMessage.name).innerHTML = '';                     
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