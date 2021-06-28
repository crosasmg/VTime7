<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>

<%  
    Response.Expires = 0
    mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = "DP018G_K"
%>
<HTML>
<HEAD>
    <%=mobjValues.StyleSheet()%>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    
	<!--META HTTP-EQUIV="CONTENT-LANGUAGE" CONTENT="ES"-->
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insStateZone: Esta función se encarga de habilitar los controles cuando se selecciona 
//%				  una acción
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		valCover.disabled = false;
		btnvalCover.disabled = valCover.disabled;
	}
}
   
//% insCancel: Se controla la acción cancelar de la ventan
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
    var lintAction  = top.frames["fraSequence"].plngMainAction;
    if (top.frames["fraSequence"].pintZone==2 && 
        top.frames["fraSequence"].plngMainAction==301)
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=DP018G_K","EndProcess",300,150)
	else
	    return (true);
}   

//% insFinish: Esta función es utilizada para realizar cambios al momento de finalizar 
//%			   la transacción
//-------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%
    
    mobjMenu = New eFunctions.Menues
    Response.Write(mobjMenu.MakeMenu("DP018G_K", "DP018G_K.aspx", 1, vbNullString))
    mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
    <BR>    
    <BR>
        <FORM METHOD="POST"  ID="FORM" NAME="DP018G_K" ACTION="ValCoverSeq.aspx?sMode=1">
	
	        <TABLE WIDTH=100%>
		        <TR>
			        <TD WIDTH=15%><LABEL><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
			        <TD><%=mobjValues.PossiblesValues("valCover", "tabTab_LifCov2", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valCoverToolTip"),  ,  ,  , True)%></TD>
		        </TR>
	        </TABLE>
        </FORM>
</BODY>
</HTML>
<%
                                                                                                                                                                           mobjValues = Nothing%>




