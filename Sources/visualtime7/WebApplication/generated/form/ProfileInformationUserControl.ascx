<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProfileInformationUserControl.ascx.vb" Inherits="ProfileInformationUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgbtnSaveQuestionInformationMessageResource='<asp:Localize runat="server" Text="Please wait..." meta:resourcekey="btnSaveQuestionInformationMessageResource"></asp:Localize>';
    var titlebtnSaveQuestionInformationMessageResource='<asp:Localize runat="server" Text="Information Message" meta:resourcekey="titlebtnSaveQuestionInformationMessageResource"></asp:Localize>';
    var msgbtnCanInformationMessageResource='<asp:Localize runat="server" Text="Please wait..." meta:resourcekey="btnCanInformationMessageResource"></asp:Localize>';
    var titlebtnCanInformationMessageResource='<asp:Localize runat="server" Text="Information Message" meta:resourcekey="titlebtnCanInformationMessageResource"></asp:Localize>';
    var msgbtnSaInformationMessageResource='<asp:Localize runat="server" Text="Please wait..." meta:resourcekey="btnSaInformationMessageResource"></asp:Localize>';
    var titlebtnSaInformationMessageResource='<asp:Localize runat="server" Text="Information Message" meta:resourcekey="titlebtnSaInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/ProfileInformation.js" type="text/javascript"></script>      
<asp:UpdatePanel runat="server">

<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='ProfileInformationTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
    <dxtc:ASPxPageControl ID="tabcontainer0" runat="server" ClientIDMode='Static' ClientVisible="True" ActiveTabIndex="0" EnableHierarchyRecreation="True" Width="100%" TabAlign="Left" TabPosition="Top" >
          <TabPages>

              <dxtc:TabPage Name="tabInformationGeneral" Text="General information" ClientVisible="True" ClientEnabled="True" ToolTip="" meta:resourcekey="tabInformationGeneralResource">
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone1" ClientInstanceName="zone1" runat="server" HeaderText="Account Information" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone1Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='UserNameLabel' EncodeHtml='false' ClientInstanceName='UserNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="UserNameLabelResource"  Text="Username"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='UserName'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxLabel ID='UserName' EncodeHtml='false' ClientInstanceName='UserName' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='EmailLabel' EncodeHtml='false' ClientInstanceName='EmailLabel' runat='server' ClientIDMode='Static' meta:resourcekey="EmailLabelResource"  Text="Email"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Email'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxLabel ID='Email' EncodeHtml='false' ClientInstanceName='Email' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='userNameTemporalLabel' EncodeHtml='false' ClientInstanceName='userNameTemporalLabel' runat='server' ClientIDMode='Static' meta:resourcekey="userNameTemporalLabelResource"  Text="userNameTemporal"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='userNameTemporal'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='userNameTemporal' ClientInstanceName='userNameTemporal' ToolTip="text0" Size='15' NullText="" ClientVisible='False' MaxLength='15' ClientIDMode='Static' meta:resourcekey="userNameTemporalResource" Width='270px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone1" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='userEmailLabel' EncodeHtml='false' ClientInstanceName='userEmailLabel' runat='server' ClientIDMode='Static' meta:resourcekey="userEmailLabelResource"  Text="userEmailTemporal"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='userEmail'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='userEmail' ClientInstanceName='userEmail' ToolTip="text1" Size='10' NullText="" ClientVisible='False' MaxLength='10' ClientIDMode='Static' meta:resourcekey="userEmailResource" Width='90px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone1" >
</ValidationSettings>
       </dxe:ASPxTextBox>
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
                    <dxrp:ASPxRoundPanel ID="zone0" ClientInstanceName="zone0" runat="server" HeaderText="Personal Information" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='FirstNameLabel' EncodeHtml='false' ClientInstanceName='FirstNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FirstNameLabelResource"  Text="Firstname"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='FirstName'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='FirstName' ClientInstanceName='FirstName' ToolTip="Firstname" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="FirstNameResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SurNameLabel' EncodeHtml='false' ClientInstanceName='SurNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SurNameLabelResource"  Text="Surname"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SurName'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='SurName' ClientInstanceName='SurName' ToolTip="Surname" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="SurNameResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:17%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LastNameLabel' EncodeHtml='false' ClientInstanceName='LastNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastNameLabelResource"  Text="Lastname"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LastName'       ></dxe:ASPxLabel></td>    <td style='width:17%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='LastName' ClientInstanceName='LastName' ToolTip="Lastname" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="LastNameResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SecondLastNameLabel' EncodeHtml='false' ClientInstanceName='SecondLastNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SecondLastNameLabelResource"  Text="Second Lastname"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SecondLastName'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='SecondLastName' ClientInstanceName='SecondLastName' ToolTip="text7" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="SecondLastNameResource" Width='135px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateofBirthLabel' EncodeHtml='false' ClientInstanceName='DateofBirthLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateofBirthLabelResource"  Text="Date of birth"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateofBirth'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DateofBirth' ClientInstanceName='DateofBirth' ToolTip="User date of birth" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DateofBirthResource" Width='135px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:17%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GenderLabel' EncodeHtml='false' ClientInstanceName='GenderLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GenderLabelResource"  Text="Gender"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Gender'       ></dxe:ASPxLabel></td>    <td style='width:17%;' align='left'>


<dxe:ASPxComboBox ID='Gender' runat='server' ClientInstanceName='Gender' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="User gender" ClientVisible='true' ClientEnabled='True' meta:resourcekey="GenderResource"  ValueType='System.String'   >
            <Items>
                <dxe:ListEditItem Value='Male' Text='Male' meta:resourcekey="GenderListItemValue1Resource"/>
                <dxe:ListEditItem Value='Female' Text='Female' meta:resourcekey="GenderListItemValue2Resource"/>
            </Items>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AddressLabel' EncodeHtml='false' ClientInstanceName='AddressLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AddressLabelResource"  Text="Address"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Address'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='Address' ToolTip="User address" meta:resourcekey="AddressResource" Columns='50' Rows='0' Size='0' NullText="" ClientVisible='True' Width='100px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxMemo>
    </td>

<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CityLabel' EncodeHtml='false' ClientInstanceName='CityLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CityLabelResource"  Text="City"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='City'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='City' ClientInstanceName='City' ToolTip="User town" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="CityResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:17%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='StateLabel' EncodeHtml='false' ClientInstanceName='StateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="StateLabelResource"  Text="State"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='State'       ></dxe:ASPxLabel></td>    <td style='width:17%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='State' ClientInstanceName='State' ToolTip="User state" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="StateResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CountryLabel' EncodeHtml='false' ClientInstanceName='CountryLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CountryLabelResource"  Text="Country"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Country'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>


<dxe:ASPxComboBox ID='Country' runat='server' ClientInstanceName='Country' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="User country" ClientVisible='true' ClientEnabled='True' meta:resourcekey="CountryResource"  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NCOUNTRY'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TelephoneNumberLabel' EncodeHtml='false' ClientInstanceName='TelephoneNumberLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TelephoneNumberLabelResource"  Text="Telephone number"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='TelephoneNumber'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TelephoneNumber' ClientInstanceName='TelephoneNumber' ToolTip="User phone number" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="TelephoneNumberResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone2" ClientInstanceName="zone2" runat="server" HeaderText="Preferences" ToolTip="User preferences" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone2Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ThemeLabel' EncodeHtml='false' ClientInstanceName='ThemeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ThemeLabelResource"  Text="Theme"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Theme'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='Theme' runat='server' ClientInstanceName='Theme' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="List of available themes" ClientVisible='true' ClientEnabled='True' meta:resourcekey="ThemeResource"  ValueType='System.String'    TextField='DESCRIPTION' ValueField='CODE'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LanguageIDLabel' EncodeHtml='false' ClientInstanceName='LanguageIDLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LanguageIDLabelResource"  Text="Language"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LanguageID'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='LanguageID' runat='server' ClientInstanceName='LanguageID' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="List of available languages" ClientVisible='true' ClientEnabled='True' meta:resourcekey="LanguageIDResource"  ValueType='System.String'    TextField='DESCRIPTION' ValueField='CODE'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
</ValidationSettings>
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
                    <dxrp:ASPxRoundPanel ID="SecurityRoundPanel" ClientInstanceName="SecurityRoundPanel" runat="server" HeaderText="Security" ToolTip="Section to change security aspects" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="SecurityRoundPanelResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='PasswordQuestionLabel' EncodeHtml='false' ClientInstanceName='PasswordQuestionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PasswordQuestionLabelResource"  Text="PasswordQuestion"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PasswordQuestion'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='PasswordQuestion' ClientInstanceName='PasswordQuestion' ToolTip="Assigns secret question" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="PasswordQuestionResource" Width='270px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="SecurityRoundPanel" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='PasswordAnswerLabel' EncodeHtml='false' ClientInstanceName='PasswordAnswerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PasswordAnswerLabelResource"  Text="PasswordAnswer"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PasswordAnswer'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='PasswordAnswer' ClientInstanceName='PasswordAnswer' ToolTip="Assigns secret answer" Size='25' NullText="" ClientVisible='True' MaxLength='25' ClientIDMode='Static' meta:resourcekey="PasswordAnswerResource" Width='225px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="SecurityRoundPanel" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style='width:50%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='btnSaveQuestion' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Save the secret question and answer" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnSaveQuestionResource" Text="Save"  OnClick='btnSaveQuestion_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnSaveQuestionClick" />
       </dxe:ASPxButton>
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
                    <dxrp:ASPxRoundPanel ID="zone3" ClientInstanceName="zone3" runat="server" HeaderText="zone" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone3Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='btnCan' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="It redirects to start" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnCanResource" Text="Cancel"  OnClick='btnCan_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnCanClick" />
       </dxe:ASPxButton>
    </td>

    <td style='width:50%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='btnSa' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Makes the save profile changes" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnSaResource" Text="Save changes"  OnClick='btnSa_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnSaClick" />
       </dxe:ASPxButton>
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tabInformationAccesLog" Text="User login information" ClientVisible="True" ClientEnabled="True" ToolTip="" meta:resourcekey="tabInformationAccesLogResource">
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone4" ClientInstanceName="zone4" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone4Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LastLoginDateLabel' EncodeHtml='false' ClientInstanceName='LastLoginDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastLoginDateLabelResource"  Text="Last login date"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LastLoginDate'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxLabel ID='LastLoginDate' EncodeHtml='false' ClientInstanceName='LastLoginDate' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LastActivityDateLabel' EncodeHtml='false' ClientInstanceName='LastActivityDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastActivityDateLabelResource"  Text="Last activity date"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LastActivityDate'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxLabel ID='LastActivityDate' EncodeHtml='false' ClientInstanceName='LastActivityDate' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

<td style='width:17%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LastLockoutDateLabel' EncodeHtml='false' ClientInstanceName='LastLockoutDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastLockoutDateLabelResource"  Text="Last lockout date"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LastLockoutDate'       ></dxe:ASPxLabel></td>    <td style='width:17%;' align='left'>

       <dxe:ASPxLabel ID='LastLockoutDate' EncodeHtml='false' ClientInstanceName='LastLockoutDate' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

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
                    <dxrp:ASPxRoundPanel ID="zone5" ClientInstanceName="zone5" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='UserSecurityTrace' EnableRowsCache='False' EnableViewState='False' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='UserSecurityTrace' runat='server' Width='100%' KeyFieldName='ID' Caption="Record" meta:resourcekey="UserSecurityTraceResource"
>
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='ID' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='EffectDate' FieldName='EffectDate' ToolTip="Effect date of the action" Caption="Effect date" VisibleIndex="0" meta:resourcekey="EffectDateFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="">
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Host' FieldName='Host' ToolTip="Host" Caption="Host" VisibleIndex="1" meta:resourcekey="HostFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="">
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='State' FieldName='State' ToolTip="State of the action" Caption="State" VisibleIndex="2" meta:resourcekey="StateFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit NullText="">
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Result' FieldName='Result' ToolTip="Result of the action" Caption="Result" VisibleIndex="3" meta:resourcekey="ResultFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesTextEdit NullText="">
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn>
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
  </tr>
</table>
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
          </TabPages>
      </dxtc:ASPxPageControl>
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
                           
                           if (msgbtnSaveQuestionConfirmationMessageResource!=''){
                            document.getElementById(btnCancel.name).style.visibility = 'hidden';
                            document.getElementById(btnConfirm.name).style.visibility = 'hidden';
                            document.getElementById(lblMessage.name).innerHTML = msgbtnSaveQuestionConfirmationMessageResource;                     
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
                            <dxe:ASPxButton ID="btnOkNotificy" runat="server" AutoPostBack="False" Text="Aceptar"
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