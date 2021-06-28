<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "MGE004_K"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%=mobjValues.StyleSheet()%>
    <%=mobjMenu.MakeMenu("MGE004", "MGE004_K.aspx", 1, "")%>
 <SCRIPT>
function insStateZone()
{
	with (self.document.forms[0])
	{ 
	cbeSelFolder.disabled = false
	btncbeSelFolder.disabled = false
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmClassPropertyWin" ACTION="valMantGeneralQue.aspx?sZone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=15%><LABEL ID=100692><%= GetLocalResourceObject("cbeSelFolderCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSelFolder", "tabFolders", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True, 5, "", eFunctions.Values.eTypeCode.eNumeric)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
%>




