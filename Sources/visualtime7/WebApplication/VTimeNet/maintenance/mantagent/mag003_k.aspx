<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG003"

%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insCancel: Ejecuta la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return(true)
}

//% insStateZone: Inhabilita determinados campos de acuerdo a la acción en tratamiento.
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]){ 
		gmtComtabge.disabled = false
		gmdEffecdate.disabled = false
		btn_gmdEffecdate.disabled = false
		btngmtComtabge.disabled = false
	}
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG003_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTabGralComm" ACTION="valMantAgent.aspx?mode=1">
<BR><BR>
    <TABLE WIDTH="100%">            
        <TR>
            <TD><LABEL ID=11742><%= GetLocalResourceObject("gmtComtabgeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("gmtComtabge", "tab_comgen", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("gmtComtabgeToolTip"))%></TD>
            <TD><LABEL ID=11741><%= GetLocalResourceObject("gmdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("gmdEffecdate", CStr(Today), True, GetLocalResourceObject("gmdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





