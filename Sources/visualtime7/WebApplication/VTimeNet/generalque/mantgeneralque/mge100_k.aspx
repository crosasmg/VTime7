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

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "MGE100_K"
%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
    <%=mobjMenu.MakeMenu("MGE003", "MGE003_K.aspx", 1, "")%>
 <SCRIPT>
//------------------------------------------------------------------------------------------
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
<FORM METHOD="post" ID="FORM" NAME="frmSeqFolder.aspx" ACTION="valMantGeneralQue.aspx?sZone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=100pcx><LABEL ID=0><%= GetLocalResourceObject("cbeQueryTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeQueryType", "Table418", 1, CStr(1),  ,  ,  ,  ,  ,  , True,  , ""))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
%>




