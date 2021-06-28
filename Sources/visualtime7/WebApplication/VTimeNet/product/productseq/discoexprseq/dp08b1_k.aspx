<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("DP08B1_K")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

Call mobjNetFrameWork.BeginPage("DP08B1_K")

mobjValues.ActionQuery = True

Session("nDisexprc") = Request.QueryString.Item("nDisexprc")
Session("nOrderApl") = Request.QueryString.Item("nOrderApl")

mobjValues.sCodisplPage = "dp08b1_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
	var mblnAutomatic = <%=Request.QueryString.Item("bAutomatic")%>

//% insStateZone: habilita/deshabilita los campos de la ventana
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	if(mblnAutomatic)
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=DP08B1_K","EndProcess",300,150)
	else
		top.close()
}

//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true;
}    
</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

Call mobjNetFrameWork.BeginPage("DP08B1_K")

With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP08B1_K", "DP08B1_K.aspx", 1, vbNullString))
	.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP08B1_K" ACTION="valDiscoExprSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=5%>&nbsp;</TD>
            <TD WIDTH=30%><%=mobjValues.PossiblesValues("tcnType", "Table30", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nType"),  , True)%></TD>
            <TD WIDTH=5%>&nbsp;/</TD>
            <TD><%=mobjValues.TextControl("lblDescDisco", 30, Request.QueryString.Item("sDescript"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
Response.Write("<SCRIPT>top.frames['fraSequence'].document.location.href='Sequence.aspx?sGoToNext=Yes&nDisexprc=" & Request.QueryString.Item("nDisexprc") & "'</SCRIPT>")
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("DP08B1_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




