<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


    <SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:00 $|$$Author: Iusr_llanquihue $"

//% insCancel: se controla la acción Cancelar de la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false;

	document.images["btn_tcdEffecdate"].disabled = false;
    document.images["btntcnIntermed"].disabled = false;
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("AGC001"))
	.Write(mobjMenu.MakeMenu("AGC001", "AGC001_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCommisTabInq" ACTION="valAgent.aspx?sZone=1">
	<BR><BR>
	<TABLE WIDTH=100%>
		<TR>
			<TD WIDTH="18%"><LABEL ID=8023><%= GetLocalResourceObject("tcnIntermedCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.PossiblesValues("tcnIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 7, GetLocalResourceObject("tcnIntermedToolTip"))%> </TD>
		</TR>
		<TR>
			<TD><LABEL ID=8022><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD COLSPAN="3"><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>





