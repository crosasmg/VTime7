<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de la Fecha
Dim mobjDate As eGeneral.GeneralFunction


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SIL704_k")

mobjDate = New eGeneral.GeneralFunction

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "sil704_k"
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
	.Write(mobjValues.WindowsTitle("sil704", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("sil704", "sil704_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%> 
<SCRIPT LANGUAGE=JavaScript> 
//+ Variable para el control de versiones 
	document.VssVersion="$$Revision: 1 $|$$Date: 16/11/04 15:05 $|$$Author: Nvaplat11 $" 

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
<FORM METHOD="POST" NAME="SIL704" ACTION="valbookrep.aspx?sMode=2">
	<BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>

	<BR>
    <TABLE WIDTH="100%">
        <TR>
			<BR><BR>
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
mobjValues = Nothing'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("SIL704_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer




