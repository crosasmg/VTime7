<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionesTodosLosRamosIconosALCUserControl.ascx.vb" Inherits="NNCotizacionesTodosLosRamosIconosALCUserControl" %>
<%@ Register Src="~/Controls/ClientControl.ascx" TagName="ClientControlClientID" TagPrefix="ucClientID" %>
 
<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgbtnCotizarFinalInformationMessageResource='<asp:Localize runat="server" Text="Redirigiendo a la planilla de cotización. Por favor espere..." meta:resourcekey="btnCotizarFinalInformationMessageResource"></asp:Localize>';
    var titlebtnCotizarFinalInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebtnCotizarFinalInformationMessageResource"></asp:Localize>';
    var msgbutton13InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button13InformationMessageResource"></asp:Localize>';
    var titlebutton13InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton13InformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/NNCotizacionesTodosLosRamosIconosALC.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="NNCotizacionesTodosLosRamosIconosALCUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='NNCotizacionesTodosLosRamosIconosALCTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone5" ClientInstanceName="zone5" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:80%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone3" ClientInstanceName="zone3" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone3Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label6' EncodeHtml='false' ClientInstanceName='label6' runat='server' ClientIDMode='Static' meta:resourcekey="label6Resource"  Text="Nosotros asumimos los riesgos y garantizamos su tranquilidad"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="14"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='center'>

       <dxe:ASPxLabel ID='CompleteClientNameCTLR' EncodeHtml='false' ClientInstanceName='CompleteClientNameCTLR' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='center'>

<dxe:ASPxImage ID="image9" runat="server" ToolTip="" ClientEnabled="True" ClientVisible="False" ClientIDMode='Static' ImageUrl="" meta:resourcekey="image9Resource" > 
</dxe:ASPxImage>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone12CTLR" ClientInstanceName="zone12CTLR" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone12CTLRResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%;' align='center'>       <dxe:ASPxLabel ID='ClientIDLabel' EncodeHtml='false' ClientInstanceName='ClientIDLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClientIDLabelResource"  Text="Asegurado"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClientID'       ></dxe:ASPxLabel><br />

       <ucClientID:ClientControlClientID runat='server' ID='ClientID' Text='' ToolTip='Asegurado' NullText='' Enabled='True' Visible='True' RepositoryValue='BackOfficeConnectionString' IsAllowSearch='True' meta:resourcekey="ClientIDResource" PaddingLeft='8px' HorizontalPositionImage='left' ImageUrl='/images/generaluse/required.PNG' RepeatImage='NoRepeat' 
VerticalPositionImage='center' ErrorDisplayMode='Text' IsRequired='True' ErrorText='El campo es requerido.'
/>
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
                    <dxrp:ASPxRoundPanel ID="zone10AgenteCTLR" ClientInstanceName="zone10AgenteCTLR" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone10AgenteCTLRResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='center'>       <dxe:ASPxLabel ID='ClienteProductorLabel' EncodeHtml='false' ClientInstanceName='ClienteProductorLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClienteProductorLabelResource"  Text="Asegurado"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClienteProductor'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='ClienteProductor' runat='server' ClientInstanceName='ClienteProductor' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Asegurado" ClientVisible='true' ClientEnabled='True' meta:resourcekey="ClienteProductorResource"  Width='400px'  ValueType='System.String'    TextField='SCLIENAME' ValueField='SCLIENT'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone10AgenteCTLR" >
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
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='btnCotizarFinal' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Cotización en línea del producto seleccionado" ClientVisible='False' ClientEnabled='True' meta:resourcekey="btnCotizarFinalResource" Text="Iniciar cotización" Width='250px'  Font-Names="Arial"  Font-Bold="True"  Font-Size="14"  ForeColor="#000000"  BackColor="#BFBFBF"   OnClick='btnCotizarFinal_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnCotizarFinalClick" />
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
    <td style='width:20%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0Contacto" ClientInstanceName="zone0Contacto" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0ContactoResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='center'>

<dxe:ASPxImage ID="image4" runat="server" ToolTip="image 4" ClientEnabled="True" ClientVisible="True" ClientIDMode='Static' ImageUrl="/images/Banners/Customer Service/2.jpg" meta:resourcekey="image4Resource" Height="50px"  Width="50px" > 
</dxe:ASPxImage>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button9' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Háganos saber sus dudas... Contáctenos" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button9Resource" Text="Háganos saber sus dudas... Contáctenos"  OnClick='button9_Click' AutoPostBack='true'>
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone7" ClientInstanceName="zone7" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone7Resource"
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
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:80%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0CTLR" ClientInstanceName="zone0CTLR" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0CTLRResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone1raParte" ClientInstanceName="zone1raParte" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone1raParteResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='10'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:5%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone6" ClientInstanceName="zone6" runat="server" HeaderText="" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='button12' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Productos previos" ClientVisible='False' ClientEnabled='True' meta:resourcekey="button12Resource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/Previous.png"   AutoPostBack='false'>
<ClientSideEvents  Click="button12Click" />
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
    <td style='width:30%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone3CTLR" ClientInstanceName="zone3CTLR" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone3CTLRResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button3MVVNN' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Cotización en línea de Mi Vida Vale" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button3MVVNNResource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/2.png"  Width='50%'  Font-Names="Comic Sans MS"  Font-Bold="True"  Font-Italic="True"  Font-Size="8"  ForeColor="#002060"   OnClick='button3MVVNN_Click' AutoPostBack='true'>
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label0' EncodeHtml='false' ClientInstanceName='label0' runat='server' ClientIDMode='Static' meta:resourcekey="label0Resource"  Text="Mi Vida Vale"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:30%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone32CTLR" ClientInstanceName="zone32CTLR" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone32CTLRResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='buttonCotVI' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Cotización en línea de Mi Inversión Segura" ClientVisible='True' ClientEnabled='True' meta:resourcekey="buttonCotVIResource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/family-care.png"  Width='50%'  Font-Names="Comic Sans MS"  Font-Bold="True"  Font-Italic="True"  Font-Size="8"  ForeColor="#002060"   OnClick='buttonCotVI_Click' AutoPostBack='true'>
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label1' EncodeHtml='false' ClientInstanceName='label1' runat='server' ClientIDMode='Static' meta:resourcekey="label1Resource"  Text="Mi Inversión Segura"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:30%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone5CTLR" ClientInstanceName="zone5CTLR" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5CTLRResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='CotizaMAD' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Cotización en línea de Mi Auro aDorado" ClientVisible='True' ClientEnabled='True' meta:resourcekey="CotizaMADResource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/6.png"  Width='100%'  Font-Names="Comic Sans MS"  Font-Bold="True"  Font-Italic="True"  Font-Size="8"  ForeColor="#002060"   OnClick='CotizaMAD_Click' AutoPostBack='true'>
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label2' EncodeHtml='false' ClientInstanceName='label2' runat='server' ClientIDMode='Static' meta:resourcekey="label2Resource"  Text="Mi Auto aDorado"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:5%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone2" ClientInstanceName="zone2" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone2Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='button3' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Siguientes productos" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button3Resource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/Next.png"  Height='48px'   AutoPostBack='false'>
<ClientSideEvents  Click="button3Click" />
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
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone2daParte" ClientInstanceName="zone2daParte" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone2daParteResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='10'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:5%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone4" ClientInstanceName="zone4" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone4Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='button5' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Productos previos" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button5Resource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/Previous.png"  Height='64px'   AutoPostBack='false'>
<ClientSideEvents  Click="button5Click" />
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
    <td style='width:30%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone6CTLR" ClientInstanceName="zone6CTLR" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6CTLRResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='CotizaHV' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Cotización en línea de Póliza Hogar Seguro" ClientVisible='True' ClientEnabled='True' meta:resourcekey="CotizaHVResource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/9.png"  Width='100%'  Font-Names="Comic Sans MS"  Font-Bold="True"  Font-Italic="True"  Font-Size="8"  ForeColor="#002060"   OnClick='CotizaHV_Click' AutoPostBack='true'>
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label3' EncodeHtml='false' ClientInstanceName='label3' runat='server' ClientIDMode='Static' meta:resourcekey="label3Resource"  Text="Póliza Hogar Seguro"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:30%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone9CTLR" ClientInstanceName="zone9CTLR" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone9CTLRResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='CotizaHV2' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Cotización en línea de Fianzas" ClientVisible='True' ClientEnabled='True' meta:resourcekey="CotizaHV2Resource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/Crédito.png"  Width='100%'  Font-Names="Comic Sans MS"  Font-Bold="True"  Font-Italic="True"  Font-Size="8"  ForeColor="#002060"   OnClick='CotizaHV2_Click' AutoPostBack='true'>
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label4' EncodeHtml='false' ClientInstanceName='label4' runat='server' ClientIDMode='Static' meta:resourcekey="label4Resource"  Text="Fianzas"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:30%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone8" ClientInstanceName="zone8" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone8Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button13' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="button13" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button13Resource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/5.png"  Width='100%'  Height='64%'   OnClick='button13_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button13Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label5' EncodeHtml='false' ClientInstanceName='label5' runat='server' ClientIDMode='Static' meta:resourcekey="label5Resource"  Text="Mi Salud Vale Oro"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:5%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone9" ClientInstanceName="zone9" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone9Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='button14' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Siguientes productos" ClientVisible='False' ClientEnabled='True' meta:resourcekey="button14Resource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/WidgetIconRamos/Next.png"   AutoPostBack='false'>
<ClientSideEvents  Click="button14Click" />
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
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
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
                            SkinID="Main" Width="74px" PageSize="50">
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
                        ClientInstanceName="btnConfirm" Text="<%$ Resources:Resource, Confirm %>" >
                        <ClientSideEvents Click="function(s,e){Confirmation_Actions();}" />
                    </dxe:ASPxButton>
            </td>            
            <td width="50%">
                    <dxe:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnCancel" Text="<%$ Resources:Resource, Cancel %>">
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