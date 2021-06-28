<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sil004_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sil004_k"
%>

	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->


<HTML>
<HEAD>

<SCRIPT> 

//% insStateZone: se manejan los campos de la página
//-----------------------------------------------------------------------------
function insStateZone()
//-----------------------------------------------------------------------------
{
}
//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//-----------------------------------------------------------------------------
function insPreZone(llngAction)
//-----------------------------------------------------------------------------
{
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel()
//-----------------------------------------------------------------------------
{
   return true
}
</SCRIPT>

    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("SIL004", "SIL004_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.31 $"
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIL004" ACTION="valClaimRep.aspx?sMode=1">
	<BR><BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
	<BR><BR>
	<TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><A NAME="Fecha a considerar">Fecha a considerar</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
		<TR>
			<TD WIDTH="12%"><LABEL ID=0>Inicial</LABEL></TD>
			<TD WIDTH="20%"><%=mobjValues.DateControl("tcdIniDate", CStr(Session("dIniDate")), False, "Fecha a partir de la cual se van a procesar las órdenes del profesional")%></TD>
			<TD WIDTH="8%">&nbsp;</TD>
			<TD WIDTH="12%"><LABEL ID=0>Final</LABEL></TD>
			<TD WIDTH="20%"><%=mobjValues.DateControl("tcdEndDate", CStr(Session("dEndDate")), False, "Fecha hasta la cual se van a procesar las órdenes del profesional")%></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD><LABEL ID=0>Profesional</LABEL></TD>
	          <%
mobjValues.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
%>
			<TD COLSPAN="4"><%=mobjValues.PossiblesValues("valProvider", "TabTab_provider", 2, CStr(Session("nProvider")), True,  ,  ,  ,  ,  ,  , 4, "Código del profesional  al  que  le corresponde la solicitud", 1)%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0>Estado</LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeDesc_Status", "Table215", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , "Estado de las órdenes profesionales que se desean listar")%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0>Ramo</LABEL></TD>
			<TD COLSPAN="4"><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , "Ramo al que pentenecen las órdenes profesionales")%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0>Sucursal</LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , "Sucursal a la que corresponden las órdenes profesionales")%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("sil004_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




