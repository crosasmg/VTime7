<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "OP010_K"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("OP010", "OP010_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 11/02/04 17:25 $|$$Author: Nvaplat7 $"
    
//% insCancel: se controla la acción cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    with(document){
		for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
		    forms[0].elements[lintIndex].disabled = false
		images["btnvalAccountNum"].disabled = false
		images["btn_tcdChequeDate"].disabled = false
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR></BR>
<FORM METHOD="post" ID="FORM" NAME="frmCheqUpdate" ACTION="ValCashBank.aspx?sMode=1">
    <TABLE WIDTH="100%">
		<TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=8700><%= GetLocalResourceObject("valAccountNumCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("sStatregt", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("valAccountNum", "tabBank_acc_CurAcc", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valAccountNumToolTip")))
End With
%>
            </TD>
            <TD><LABEL ID=8708><%= GetLocalResourceObject("tcdChequeDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdChequeDate", CStr(Today),  , GetLocalResourceObject("tcdChequeDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<% mobjValues = Nothing%>




