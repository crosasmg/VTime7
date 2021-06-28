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

mobjValues.sCodisplPage = "opc013_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT>
 
 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 19/03/04 13:35 $|$$Author: Nvaplat53 $"

//%insCancel: Control la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%insStateZone: Habilita/deshabilita los campos de la ventana.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
		
		document.images["btnvalIntermed"].disabled = false
		document.images["btn_tcdEffecdate"].disabled = false
		document.images["btncboCurrency"].disabled = false
}

//%insShowChangeCurrency: Se habilita/deshabilita el campo moneda
//-------------------------------------------------------------------------------------------
function insShowChangeCurrency(){
//-------------------------------------------------------------------------------------------

    with (document.forms[0]){
        if (valIntermed.value != '' &&
		    cboTypeAccount.value != 0)       
            insDefValues('Curren','nType_acc=' + cboTypeAccount.value + '&nIntermed=' + valIntermed.value + '&sZone=fraHeader', '/VTimeNet/CashBank/CashBank');
    }
}
</SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("OPC013"))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "OPC013_K.aspx"))
	.Write(mobjMenu.MakeMenu("OPC013", "OPC013_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQInterMov" ACTION="ValCashBank.aspx?sMode=1">
<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8850><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, Today)),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD><LABEL ID=8761><%= GetLocalResourceObject("cboTypeAccountCaption") %></LABEL></TD>            
			<TD><%With mobjValues
	.TypeList = 1
	.List = "25,19,17,16,14"
	Response.Write(mobjValues.PossiblesValues("cboTypeAccount", "Table400", 1,  ,  ,  ,  ,  ,  , "insShowChangeCurrency();", True,  , GetLocalResourceObject("cboTypeAccountToolTip")))
End With%></TD>            
        </TR>
        <TR>
            <TD><LABEL ID=8851><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , "insShowChangeCurrency();", True, 10, GetLocalResourceObject("valIntermedToolTip"))%></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD><LABEL ID=8849><%= GetLocalResourceObject("cboCurrencyCaption") %></LABEL></TD>
<%mobjValues.Parameters.Add("nTyp_acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sType_acc", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nIntermed", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sClient", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
%>
                <TD><%=mobjValues.PossiblesValues("cboCurrency", "TabCurr_Cli_Inter", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboCurrencyToolTip"))%></TD>
               
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>




