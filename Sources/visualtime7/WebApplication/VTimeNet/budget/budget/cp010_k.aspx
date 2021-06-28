<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues

    Dim mintAccount As Object


'----------------------------------------------------------------------------
Private Sub insLoadCP010_k()
	'----------------------------------------------------------------------------
	Response.Write(mobjValues.ButtonLedCompan("LedCompan", 6, GetLocalResourceObject("LedCompanToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=4 WIDTH=""20%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8461>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", 1,  ,  ,  ,  ,  ,  , "LoadCurrency(this)", True,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8435>" & GetLocalResourceObject("tcnYearCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnYear", 4, CStr(0),  ,  ,  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=2 WIDTH=""20%"">" & vbCrLf)
Response.Write("        <TD><LABEL ID=8427>" & GetLocalResourceObject("valBud_codeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nYear", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valBud_code", "TabBudget", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 20, GetLocalResourceObject("valBud_codeToolTip"), eFunctions.Values.eTypeCode.eString))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>		")

	
	
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "cp010_k"
%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}
//% insStateZone: Se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
    self.document.forms[0].tcnYear.disabled=false;
    self.document.forms[0].cbeCurrency.disabled=false;
    self.document.forms[0].valBud_code.disabled=false;
}
//%	LoadBudgetWork: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function LoadCurrency(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		    valBud_code.Parameters.Param1.sValue=tcnLedCompan.value
		    valBud_code.Parameters.Param2.sValue=tcnYear.value
		    valBud_code.Parameters.Param3.sValue='1'
		    valBud_code.Parameters.Param4.sValue=Field.value
    }
}

</SCRIPT>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>

<%With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.MakeMenu("CP010", "CP010_k.aspx", 1, ""))
End With
mobjMenu = Nothing

%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmBudInqUpd" ACTION="ValBudget.aspx?sTime=1">
<BR>
<%
Call insLoadCP010_k()
%>	
</BODY>
</FORM>
</HTML>





