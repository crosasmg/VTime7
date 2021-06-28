<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "dp018_k"
%>
<HTML>
<HEAD>
<%=mobjValues.StyleSheet()%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">

//%insStateZone. Esta función se encarga de habilitar los controles cuando se selecciona una acción
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
	with (self.document.forms[0]){
		valCover.disabled = false;
		btnvalCover.disabled = valCover.disabled;
	}
}
   
//%insCancel.Esta función muestra la ventana de cancelación de proceso
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
    var lintAction  = top.frames["fraSequence"].plngMainAction;
    if (top.frames["fraSequence"].pintZone==2 && 
        top.frames["fraSequence"].plngMainAction==301)
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=DP018_K","EndProcess",300,150)
	else
	    return (true);
}   

//%insFinish. Esta función es utilizada para realizar cambios al momento de finalizar la transacción
//------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<meta http-equiv="Content-Language" content="es">
<%
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("DP018G_K", "DP018_k.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<BODY CLASS="HEADER">
<BR>
<BR>
<FORM METHOD="POST" NAME="DP018_K" ACTION="ValCoverSeq.aspx?sMode=1">
<TABLE WIDTH=100%>
  <TR>
	<TD WIDTH=45pcx><LABEL><%= GetLocalResourceObject("valCoverCaption") %><LABEL></TD>
	<TD><%=mobjValues.PossiblesValues("valCover", "tabTab_LifCov2", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCoverToolTip"),  ,  ,  , True)%></TD>
</TR>
</TABLE>
</FORM>
</BODY>
</HTML>




