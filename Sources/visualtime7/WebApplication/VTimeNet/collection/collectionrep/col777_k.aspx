<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'+ ---------------------------------------------------------------------------------------- 
'+ COL777: Proceso de transfecrencia de recaudaciones. 
'+ ---------------------------------------------------------------------------------------- 

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




	<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("COL777"))
	.Write(mobjMenu.MakeMenu("COL777", "COL777_K.aspx", 1, ""))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "COL777_K.aspx"))
End With
mobjMenu = Nothing
'	Response.Write "<NOTSCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>" 
%> 
<SCRIPT LANGUAGE=JavaScript> 
//+ Variable para el control de versiones 
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $" 

//% insStateZone: se controla el estado de los campos de la página 
//-------------------------------------------------------------------------------------------- 
function insStateZone(){ 
//-------------------------------------------------------------------------------------------- 
} 

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COL777" ACTION="valCollectionRep.aspx?sMode=2">
	<BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>

	<BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("tcdCollectIniCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdCollectIni", CStr(Today),  , GetLocalResourceObject("tcdCollectIniToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdCollectEndCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdCollectEnd", CStr(Today),  , GetLocalResourceObject("tcdCollectEndToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>





