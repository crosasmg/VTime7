<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**+ Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("COL889_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col889_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>



<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 3 $|$$Date: 28/11/03 17:41 $|$$Author: Nvaplat56 $"

//% insStateZone: se controla el estado de los campos de la página 
//-------------------------------------------------------------------------------------------- 
function insStateZone() 
//-------------------------------------------------------------------------------------------- 
{
} 
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel()
//--------------------------------------------------------------------------------------------
{
	return true;
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish()
//--------------------------------------------------------------------------------------------
{
    return true;
}
</SCRIPT>
	<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "COL889_K.aspx", 1, ""))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" NAME="col889" ACTION="valbookrep.aspx?sMode=1">
	<BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>

	<BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateIniCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDateIni", CStr(Today),  , GetLocalResourceObject("tcdDateIniToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateEndCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDateEnd", CStr(Today),  , GetLocalResourceObject("tcdDateEndToolTip"))%></TD>
        </TR>        
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col889_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




