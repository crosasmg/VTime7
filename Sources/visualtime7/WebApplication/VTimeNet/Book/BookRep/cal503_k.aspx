<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de la Fecha
Dim mobjDate As eGeneral.GeneralFunction


</script>
<%Response.Expires = -1441

mobjDate = New eGeneral.GeneralFunction

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "cal503_k"
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




	<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("CAL503", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("CAL503", "CAL503_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%> 
<SCRIPT LANGUAGE=JavaScript> 
//+ Variable para el control de versiones 
	document.VssVersion="$$Revision: 1 $|$$Date: 22/12/04 12:36 $|$$Author: Nvaplat11 $" 

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
<FORM METHOD="POST" NAME="CAL503" ACTION="valbookrep.aspx?sMode=2">
	<BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>

	<BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateIniCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdDateIni", CStr(Today),  , GetLocalResourceObject("tcdDateIniToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateEndCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdDateEnd", CStr(Today),  , GetLocalResourceObject("tcdDateEndToolTip"))%></TD>
        </TR>        
    </TABLE>
</FORM> 
</BODY>
</HTML>





