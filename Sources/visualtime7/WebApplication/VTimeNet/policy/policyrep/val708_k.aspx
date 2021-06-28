<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">


'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
	
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 19/01/04 16:20 $|$$Author: Nvaplat28 $"

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
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VAL708", "VAL708_K.aspx", 1, vbNullString))
	Response.Write(mobjMenu.setZone(CShort("1"), "VAL708", ""))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>

<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))%>

<FORM METHOD="POST" NAME="VAL708" ACTION="valPolicyRep.aspx?sMode=2">
    <TABLE WIDTH="100%">
    <TR>
        <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_1Caption"), "1", "1")%></TD>
		<TD WIDTH="10%">&nbsp;</TD>
		<TD><LABEL ID="0"><%= GetLocalResourceObject("tcnMonthCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.NumericControl("tcnMonth", 2, CStr(Month(Today)), True, GetLocalResourceObject("tcnMonthToolTip"), False)%></TD>
    </TR>
    <TR>    
        <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), "2", "2")%></TD>
        <TD WIDTH="10%">&nbsp;</TD>
		<TD><LABEL ID="0"><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.NumericControl("tcnYear", 4, CStr(Year(Today)), True, GetLocalResourceObject("tcnYearToolTip"), False)%></TD>
    </TR>
        
    </TABLE>
	<%=mobjValues.HiddenControl("tcnUsercode", session("nUsercode"))%>    
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing
%>




