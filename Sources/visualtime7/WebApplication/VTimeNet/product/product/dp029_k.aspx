<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "dp029_k"
%>
<HTML>
<HEAD>
<%=mobjValues.StyleSheet()%>


<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT">
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"

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
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=DP018G_K","EndProcess",300,150)
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
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/tmenu.js"></SCRIPT>
<meta http-equiv="Content-Language" content="es">
<%
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("DP029_K", "DP029_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>
</HEAD>
	<BODY CLASS="HEADER">
		<BR>
		<BR>
		<FORM METHOD="POST" NAME="DP029_K" ACTION="ValCoverSeq.aspx?sMode=1">
			<TABLE WIDTH=100%>
				<TR>
					<TD WIDTH=45pcx><LABEL><%= GetLocalResourceObject("valCoverCaption") %><LABEL></TD>
					<TD>
					<%
mobjValues.Parameters.Add("sStatregt", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nConvergen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valCover", "tabTabGenCov", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCoverToolTip"), eFunctions.Values.eTypeCode.eNumeric,  ,  , True))
%>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>




