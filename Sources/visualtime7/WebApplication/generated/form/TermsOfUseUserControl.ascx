<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TermsOfUseUserControl.ascx.vb" Inherits="TermsOfUseUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgbtnPrintInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="btnPrintInformationMessageResource"></asp:Localize>';
    var titlebtnPrintInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebtnPrintInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/TermsOfUse.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="TermsOfUseUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >


  
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='TermsOfUseTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label0' EncodeHtml='false' ClientInstanceName='label0' runat='server' ClientIDMode='Static' meta:resourcekey="label0Resource"  Text="&lt;h2&gt;T&#233;rminos y Condiciones de Uso&lt;/h2&gt;&lt;hr&gt;&lt;h3&gt;Vigente desde: 01 de Enero de 2010&lt;/h3&gt;&lt;p style=&#39;margin-left: 15px;&#39;&gt;Agradecemos que utilice los productos y servicios de TIKI Seguros (en adelante, &quot;Servicios&quot;), los cuales se proporcionan a trav&#233;s de nuestro “Portal&quot;), y podr&#225; utilizar en la forma en que TIKI SEGUROS. determine conveniente. EL Portal y los Servicios se rigen por los t&#233;rminos y condiciones establecidas en este convenio, as&#237; como por las pol&#237;ticas, normas, instrucciones y reglamentos que dicten tanto la Superintendencia de Seguros, como TIKI SEGUROS, C.A.&lt;br&gt;&lt;br&gt;Este convenio implica el cumplimiento de los &quot;T&#233;rminos y Condiciones de Uso&quot; y la &quot;Pol&#237;tica de Privacidad&quot; los cuales Usted declara expresamente conocer, entender y aceptar, desde el mismo momento en que comienza hacer uso de nuestros &quot;Servicios&quot;.&lt;/p&gt;&lt;h4 style=&#39;margin-left: 15px;&#39;&gt;1. CONDICIONES DE USO&lt;/h4&gt;&lt;p style=&#39;margin-left: 15px;&#39;&gt;&lt;b&gt;a. Art&#237;culo 1:&lt;/b&gt;Usted debe cumplir tambi&#233;n con los t&#233;rminos y condiciones espec&#237;ficas correspondientes a cualquiera de nuestros &quot;Servicios&quot;, disponibles particularmente para cada uno de ellos.&lt;br&gt;&lt;b&gt;b. Art&#237;culo 2:&lt;/b&gt;Al utilizar nuestros &quot;Servicios&quot; Usted se compromete en el cumplimiento de nuestras Pol&#237;ticas de Seguridad de la Informaci&#243;n.&lt;br&gt;&lt;b&gt;c. Art&#237;culo 3:&lt;/b&gt;Usted no debe utilizar nuestros Servicios de forma inadecuada. No debe interferir con dichos Servicios, ni intentar ingresar a ellos, empleando un m&#233;todo distinto a la interfaz o a las instrucciones proporcionadas por &quot;TIKI Seguros&quot;. Podemos suspender o cancelar nuestros Servicios si no cumple con nuestras pol&#237;ticas o si consideramos que existe una conducta malintencionada.&lt;/p&gt;&lt;h4 style=&#39;margin-left: 15px;&#39;&gt;2. CUENTA DE USUARIO EN &quot;TIKI SEGUROS&quot;&lt;/h4&gt;&lt;p style=&#39;margin-left: 15px;&#39;&gt;&lt;b&gt;a. Art&#237;culo 1:&lt;/b&gt;Es posible que necesite una cuenta de usuario de &quot;TIKI Seguros&quot; para utilizar algunos de nuestros Servicios. Puede crear su propia cuenta de usuario de &quot;TIKI Seguros&quot; o se le puede asignar una cuenta a trav&#233;s de un administrador. &lt;/p&gt;&lt;h4 style=&#39;margin-left: 15px;&#39;&gt;3. SEGURIDAD&lt;/h4&gt;&lt;p style=&#39;margin-left: 15px;&#39;&gt;&lt;b&gt;a. Art&#237;culo 1:&lt;/b&gt;Nuestro sitio est&#225; protegido con diversas medidas de seguridad tales como procedimientos de control de cambios, contrase&#241;as y controles de acceso f&#237;sicos. Asimismo, empleamos una variedad de mecanismos que garantizan que los datos que usted suministra no se pierdan, sean usados indebidamente o modificados sin autorizaci&#243;n. Estos controles incluyen pol&#237;ticas de confidencialidad de datos y frecuentes respaldos de la base de datos.&lt;/p&gt;&lt;h4 style=&#39;margin-left: 15px;&#39;&gt;4. RECOLECCI&#211;N DE DATOS &lt;/h4&gt;&lt;p style=&#39;margin-left: 15px;&#39;&gt;&lt;b&gt;a. Art&#237;culo 1:&lt;/b&gt;La recolecci&#243;n de datos se realiza para proporcionar y mejorar con efectividad, rapidez y seguridad los servicios que ofrecemos a todos nuestros&lt;br&gt;&lt;b style=&quot;margin-left:20px;&quot;&gt;i. Informaci&#243;n que nos facilita usted. &lt;/b&gt;Por ejemplo, para usar muchos servicios debe registrarse para obtener una cuenta de usuarios de &quot;TIKI Seguros&quot;. Durante este proceso le pediremos determinados datos personales como, por ejemplo: su nombre completo, direcci&#243;n de correo electr&#243;nico, n&#250;meros de tel&#233;fono, los datos de los instrumentos de cobro y pago que desea utilizar, tales como: tarjetas de cr&#233;dito o cuentas de dep&#243;sito, adem&#225;s de todos aquellos datos que consideremos relevantes y necesarios para prestar adecuadamente nuestros servicios.&lt;br&gt;&lt;b style=&quot;margin-left:20px;&quot;&gt;ii. Datos que obtenemos a trav&#233;s de la utilizaci&#243;n de nuestros servicios.&lt;/b&gt;Podremos recolectar datos acerca de qu&#233; servicios utiliza y de c&#243;mo los utiliza &lt;br&gt;&lt;/p&gt;&lt;h4 style=&#39;margin-left: 15px;&#39;&gt;5. VERIFICACI&#211;N DE DATOS &lt;/h4&gt;&lt;p style=&#39;margin-left: 15px;&#39;&gt;&lt;b&gt;a. Art&#237;culo 1:&lt;/b&gt;TIKI Seguros&quot; se reserva el derecho de solicitar y verificar cualquier informaci&#243;n necesaria para ingresar y prestar cualquier servicio. &lt;br&gt;&lt;/p&gt;&lt;h4 style=&#39;margin-left: 15px;&#39;&gt;6. C&#211;MO UTILIZAMOS LOS DATOS?&lt;/h4&gt;&lt;p style=&#39;margin-left: 15px;&#39;&gt;&lt;b&gt;a. Art&#237;culo 1:&lt;/b&gt;No compartiremos sus datos personales con empresas, organizaciones o personas naturales ajenas a &quot;TIKI Seguros&quot;&lt;br&gt;&lt;/p&gt;&lt;h4 style=&#39;margin-left: 15px;&#39;&gt;7. QU&#201; DATOS PERSONALES COMPARTIMOS?&lt;/h4&gt;&lt;p style=&#39;margin-left: 15px;&#39;&gt;&lt;b&gt;a. Art&#237;culo 1:&lt;/b&gt;No compartiremos sus datos personales con empresas, organizaciones o personas naturales ajenas a &quot;TIKI Seguros&quot;, &lt;br&gt;&lt;b&gt;b. Art&#237;culo 2: &lt;/b&gt;Nos esforzamos por proteger a &quot;TIKI Seguros&quot; y a nuestros usuarios frente a cualquier modificaci&#243;n, divulgaci&#243;n o destrucci&#243;n no autorizada de los datos que conservamos o frente al acceso no autorizado a los mismos. En particular:&lt;br&gt;&lt;br&gt;• Limitamos el acceso de los agentes y los empleados de &quot;TIKI Seguros&quot; a la informaci&#243;n personal que deben procesar para &quot;TIKI Seguros&quot; y nos aseguramos de que cumplan las estrictas obligaciones de confidencialidad contractuales y de que est&#233;n sujetos a las condiciones disciplinarias pertinentes o al despido si no cumplen dichas obligaciones.&lt;/p&gt;&lt;br&gt;&lt;br&gt;"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='btnPrint' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Imprimir Términos y condiciones de uso." ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnPrintResource" Text="Imprimir Términos y condiciones de uso." ImagePosition='Left' Image-Url="/images/16x16/Operations/Printer.png" Height='16px'   AutoPostBack='false'>
<ClientSideEvents  Click="btnPrintClick" />
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
                           
                           if (msgbtnPrintConfirmationMessageResource!=''){
                            document.getElementById(btnCancel.name).style.visibility = 'hidden';
                            document.getElementById(btnConfirm.name).style.visibility = 'hidden';
                            document.getElementById(lblMessage.name).innerHTML = msgbtnPrintConfirmationMessageResource;                     
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