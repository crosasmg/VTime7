<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MVA646_K"
%>
<html>
<head>
<%=mobjValues.StyleSheet()%>


<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>
<script LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//%insStateZone. Esta función se encarga de habilitar los controles
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
    with (self.document.forms[0]){
        valAgreement.disabled = false;
        btnvalAgreement.disabled = valAgreement.disabled;
    }
}
   
//%insCancel.Esta función muestra la ventana de cancelación de proceso
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
    var lintAction  = top.frames["fraSequence"].plngMainAction;
    if (top.frames["fraSequence"].pintZone==2 && 
        top.frames["fraSequence"].plngMainAction==301)
        ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=MVA646_K","EndProcess",300,150)
    else
        return true;
}   

//%insFinish. Esta función es utilizada para realizar cambios al momento de finalizar la transacción
//------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------
    return true;
}
</script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<%
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MVA646_K", "MVA646_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</head>
<body>
<br>
<br>
<form METHOD="POST" NAME="MVA646_K" ACTION="ValAgreementSeq.aspx?sMode=1">
<table WIDTH="100%">
    <tr>
        <td WIDTH="45pcx"><label><%= GetLocalResourceObject("valAgreementCaption") %><label></td>
        <td>
        <%
mobjValues.Parameters.Add("sStatregt", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valAgreement", "tabAgreement_al", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAgreementToolTip"),  ,  ,  , True))
%>
        </td> 
    </tr>
</table>
</form>
</body>
</html>




