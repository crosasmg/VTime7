<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sil1002_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sil1002_k"
%>
<HTML>
	<HEAD>
		<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
		<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
		<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
		<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT>
//+Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 31-03-13 21:30 $"
</SCRIPT>
		
<SCRIPT>
//%insSetParameters: Asigna el parámetro "nBranch" al campo Producto
//------------------------------------------------------------------------------------------
function insSetParameters(Field){
//------------------------------------------------------------------------------------------
	document.forms[0].valProduct.Parameters.Param1.sValue = Field.value;
	document.forms[0].valProduct.value = ""
}

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("sil1002", "sil1002_K.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST" ID="FORM" NAME="sil1002" ACTION="valClaimRep.aspx?sMode=1">
        <BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("sil1002", Request.QueryString("sWindowDescript")))%>
		<TABLE WIDTH="100%">
		    <BR><BR>
			<TR>
				<TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40667><a NAME="Modalidad">Fecha a filtrar</a></LABEL></td>
			</TR>
			<TR>
				<TD COLSPAN="5" CLASS="HorLine"></TD>
			</TR>
			<TR>
				<TD WIDTH="50%"><%=mobjValues.OptionControl(0, "optDate", "Fecha de ocurrencia", "1", "1")%></TD>
			</TR>
			<TR>
				<TD WIDTH="50%"><%=mobjValues.OptionControl(0, "optDate", "Fecha de denuncio",  , "2")%></TD>
			</TR>
			<TR>
				<TD WIDTH="50%"><%=mobjValues.OptionControl(0, "optDate", "Fecha de estado del siniestro",  , "3")%></TD>
			</TR>						

		</TABLE>
		<BR><BR>
	
		<TABLE WIDTH="100%">
			<TR>
				<TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40667><a NAME="Parámetros para el listado">Parámetros para el listado</a></LABEL></td>
			</TR>
			<TR>
				<TD COLSPAN="5" CLASS="HorLine"></TD>
			</TR>
			<TR>
			<TR>
				<TD><LABEL ID=101041>Estado del siniestro</LABEL></TD>
				<%
mobjValues.TypeList = 1
mobjValues.List = "1,4,5,7,9"
%>
				<TD><%=mobjValues.PossiblesValues("cbeStaClaim", "Table135", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "Estado del siniestro del cual se requiere la información")%></TD>
				<TD></TD>
				<TD></TD>
				<TD></TD>
			</TR>				
			<TR>
				<TD><LABEL ID=101038>Fecha inicial</LABEL></TD>
				<TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdInitdate", CStr(Today),  , "Fecha a partir de la cual se requiere la información")%></TD>
				<TD></TD>
				<TD></TD>
				<TD></TD>				
			</TR>
			<TR>
				<TD><LABEL ID=101039>Fecha final</LABEL></TD>
				<TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdEnddate", CStr(Today),  , "Fecha hasta la cual se requiere la información")%></TD>
				<TD></TD>
				<TD></TD>
				<TD></TD>				
			</TR>
						


<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("sil1002_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




