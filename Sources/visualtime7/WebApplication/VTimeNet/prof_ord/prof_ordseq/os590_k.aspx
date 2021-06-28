<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "os590_k"
%>
<HTML>
<HEAD>
	<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"
	</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("OS590_K", "OS590_K.aspx", 1, vbNullString))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
	<SCRIPT LANGUAGE=JavaScript>

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false;
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
<FORM METHOD="POST" NAME="OS590_K" ACTION="valProf_ordseq.aspx?sMode=2">
<BR>
    <TABLE WIDTH="100%" ALIGN=center>
        <TR>
			<TD WIDTH=25%><LABEL ID=0><%= GetLocalResourceObject("tctserv_orderCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tctserv_order", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tctserv_orderToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>




