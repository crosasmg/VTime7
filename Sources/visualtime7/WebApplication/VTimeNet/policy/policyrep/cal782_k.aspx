<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cal782_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cal782_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>	

<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

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
	Response.Write(mobjMenu.MakeMenu("CAL782", "ValPolicyRep.aspx", 1, vbNullString))
	Response.Write(mobjMenu.setZone(CShort("1"), "CAL782", ""))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>

    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>

<FORM METHOD="POST" NAME="CAL782" ACTION="ValPolicyRep.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdStartCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdStart", "",  , GetLocalResourceObject("tcdStartToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEnd", "",  , GetLocalResourceObject("tcdEndToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML> 
<%
mobjValues = Nothing%> 
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("cal782_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%> 





