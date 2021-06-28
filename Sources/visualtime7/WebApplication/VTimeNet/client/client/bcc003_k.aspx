<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

With Server
	mobjMenu = New eFunctions.Menues
	mobjValues = New eFunctions.Values
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
		
//%insStateZone: Permite habilitar/deshabilitar los campos de la ventana
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
}

//%insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("BCC003"))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "BCC003_K.aspx"))
	.Write(mobjMenu.MakeMenu("BCC003", "BCC003_k.aspx", 1, ""))
	.Write("<br>")
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQPolicycli" ACTION="ValClient.aspx?Zone=1">
	<%If IsNothing(Request.Form) Then
	session("scliename") = ""
End If
%>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(40364, "optPolicy", GetLocalResourceObject("optPolicy_CStr2Caption"), CStr(1), CStr(2))%></TD>
            <TD><LABEL ID=9696><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
            <%=mobjValues.TextControl("tctClient", 20, "")%>           
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(40365, "optPolicy", GetLocalResourceObject("optPolicy_CStr1Caption"),  , CStr(1))%></TD>
            <TD><LABEL ID=9699><%= GetLocalResourceObject("cbeRoleCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeRole", "table12", 1)%></TD>
            <TD><LABEL ID=9698><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", CStr(Now),  , "")%></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(40366, "optPolicy", GetLocalResourceObject("optPolicy_CStr3Caption"),  , CStr(3))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





