<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'%--------------------------------------------------------------
'% Nombre :      btc002
'% Descripcion : Permite consultar los resultados de procesos batch 
'%               asociados a una transaccion
'%
'% document.VssVersion="$$Revision: 3 $|$$Date: 9/01/04 16:14 $|$$Author: Nvaplat7 $"
'%--------------------------------------------------------------

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As Object

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


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
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 9/01/04 16:14 $|$$Author: Nvaplat7 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    var objErr;
    try{
        self.document.forms[0].valBatch.disabled = false;
        self.document.forms[0].btnvalBatch.disabled = false;
    }catch(objErr){};
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
Response.Write(mobjMenu.MakeMenu("BTC002", "BTC002.aspx", 1, vbNullString))
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<FORM METHOD="POST" NAME="BTC002" ACTION="valBatch.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="20%"><label><%= GetLocalResourceObject("valBatchCaption") %></label></td>
			<TD><%mobjValues.Parameters.Add("nStatreg", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valBatch", "TABBATCH_PROCESS", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , 30, "", True, 5, GetLocalResourceObject("valBatchToolTip")))%>
			</TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>





