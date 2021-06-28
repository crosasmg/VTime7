<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.48
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQuote As String


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si777_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mstrQuote = """"

mobjValues.sCodisplPage = "si777_k"

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT>
    document.VssVersion="$$Revision: 2 $|$$Date: 30/10/03 10:17 $"    

//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function onChangePolicy(){
//------------------------------------------------------------------------------------------
	var lintBranch  = 0;
	var lintProduct = 0;
	var lintPolicy  = 0;

	lintBranch  = self.document.forms[0].elements[<%=mstrQuote%>cbeBranch<%=mstrQuote%>].value	
	lintProduct = self.document.forms[0].elements[<%=mstrQuote%>valProduct<%=mstrQuote%>].value
	lintPolicy  = self.document.forms[0].elements[<%=mstrQuote%>tcnPolicy<%=mstrQuote%>].value
	insDefValues('onChangePolicy','nPolicy=' + lintPolicy +
								  '&nBranch='+ lintBranch +
								  '&nProduct=' + lintProduct ,'/VTimeNet/claim/claim/');
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}

//% insFinish: Ejecuta rutinas necesarias en el momento de Finalizar la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
   return true
}
</SCRIPT>

    <%Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("SI777", "SI777_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmClaimPaymentControl" ACTION="valClaim.aspx?sMode=1">
	<BR><BR>
    <TABLE WIDTH="100%" 
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0>Pagos a mostrar</LABEL></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="3"></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(1, "optPayment", "Por autorizar", CStr(1), CStr(1))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL>Ramo</LABEL></TD>				
			<TD><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenecen los siniestros a mostrar", "")%></TD>
		</TR>
        <TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(2, "optPayment", "Autorizadas",  , CStr(8))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL>Producto</LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", "Producto al que pertenecen los siniestros a mostrar", "")%></TD>
		</TR>
        <TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(3, "optPayment", "Sin órdenes de pago",  , CStr(3))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL>Póliza</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , "Número de póliza a procesar",  ,  ,  ,  ,  , "onChangePolicy()",  , 5)%></TD>
		</TR>
		<TR>
			<TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD>
			<TD WIDTH=20%><LABEL>Rut<LABEL></TD>
			<TD><%=mobjValues.ClientControl("tctClient", "", False, "Rut del cliente",  , False, "tctCliename",  ,  ,  ,  ,  ,  ,  , False)%></TD>
		</TR>
			<!--TD COLSPAN="2"><%=mobjValues.CheckControl("chkRelation", "Relación",  , CStr(1))%></TD-->
			<TD>&nbsp;</TD>
			<TD><LABEL>Monto mínimo de aprobación</LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmountApp", 10, vbNullString,  , "Monto mínimo de aprobación",  ,  ,  ,  ,  , "",  , 5)%></TD>
		</TR>
		<TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL>Fecha</LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>		
		<TR>
			<TD><LABEL>Desde</LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdInitial_date",  ,  , "Fecha desde de los movimientos de pago a mostrar")%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL>Hasta</LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdFinal_date",  ,  , "Fecha hasta de los movimientos de pago a mostrar")%></TD>
		</TR>
    </TABLE>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.48
Call mobjNetFrameWork.FinishPage("si777_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




