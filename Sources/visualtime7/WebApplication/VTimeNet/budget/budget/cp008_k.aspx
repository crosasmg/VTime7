<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues

    Dim mintAccount As Object


'----------------------------------------------------------------------------
Private Sub insLoadCP008_k()
	'----------------------------------------------------------------------------
	Response.Write(mobjValues.ButtonLedCompan("LedCompan", 6, GetLocalResourceObject("LedCompanToolTip"),  , "insGetNumber(this)"))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=6 WIDTH=""15%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL><A NAME=""Saldo"">" & GetLocalResourceObject("AnchorSaldoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""2""><HR></TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.OptionControl(0, "optMonth", GetLocalResourceObject("optMonth_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.OptionControl(0, "optMonth", GetLocalResourceObject("optMonth_2Caption"),  , "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.CheckControl("chkTotAnnual", GetLocalResourceObject("chkTotAnnualCaption"),  , "1",  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=5 WIDTH=""20%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8471>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8473>" & GetLocalResourceObject("tcnYearWorkCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnYearWork", 4, CStr(0),  ,  ,  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8461>" & GetLocalResourceObject("cbeCurrencyWorkCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrencyWork", "Table11", 1,  ,  ,  ,  ,  ,  , "LoadBudgetWork(this)", True,  , GetLocalResourceObject("cbeCurrencyWorkToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8467>" & GetLocalResourceObject("valBudgetWorkCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""3"">")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nYear", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valBudgetWork", "TabBudget", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 20, GetLocalResourceObject("valBudgetWorkToolTip"), eFunctions.Values.eTypeCode.eString))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=5 WIDTH=""20%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8468>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8472>" & GetLocalResourceObject("tcnYearCompCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnYearComp", 4, CStr(0),  ,  ,  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8462>" & GetLocalResourceObject("cbeCurrencyCompCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrencyComp", "Table11", 1,  ,  ,  ,  ,  ,  , "LoadBudgetComp(this)", True,  , GetLocalResourceObject("cbeCurrencyCompToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8466>" & GetLocalResourceObject("valBudgetCompCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			 <TD COLSPAN=""3"">")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nYear", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valBudgetComp", "TabBudget", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 20, GetLocalResourceObject("valBudgetCompToolTip"), eFunctions.Values.eTypeCode.eString))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=4 WIDTH=""25%"">" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	    <TD><LABEL ID=8463>" & GetLocalResourceObject("valAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valAccount", "tabLedger_acc", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "LoadAccount(this)", True, 20, "", eFunctions.Values.eTypeCode.eString))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL ID=8465>" & GetLocalResourceObject("valAuxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sAccount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valAux", "tabLedger_accAux", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 20, "", eFunctions.Values.eTypeCode.eString))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""3"">")


Response.Write(mobjValues.HiddenControl("tctDescript", ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("     <TR>   " & vbCrLf)
Response.Write("        <TD><LABEL ID=8469>" & GetLocalResourceObject("valUnitCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valUnit", "tabTab_cost_c", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 20, "", eFunctions.Values.eTypeCode.eString))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=8464>" & GetLocalResourceObject("tcnAnnualBudgetCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnAnnualBudget", 18, CStr(0),  ,  , True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>		")

	
	
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "cp008_k"
%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}
//% insGetNumber
//------------------------------------------------------------------------------------------
function insGetNumber(Field){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		    valAccount.Parameters.Param1.sValue=tcnLedCompan.value
		    valUnit.Parameters.Param1.sValue=tcnLedCompan.value
    }
}
//% insStateZone: Se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
    self.document.forms[0].chkTotAnnual.disabled=false;
    self.document.forms[0].tcnYearWork.disabled=false;
    self.document.forms[0].cbeCurrencyWork.disabled=false;
    self.document.forms[0].valBudgetWork.disabled=false;
    self.document.forms[0].tcnYearComp.disabled=false;
    self.document.forms[0].cbeCurrencyComp.disabled=false;
    self.document.forms[0].valBudgetComp.disabled=false;
    self.document.forms[0].valAccount.disabled=false;
    self.document.forms[0].valAux.disabled=false;
    self.document.forms[0].tctDescript.disabled=false;
    self.document.forms[0].valUnit.disabled=false;
    self.document.forms[0].tcnAnnualBudget.disabled=false;
}
//%	LoadAccount: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function LoadAccount(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		    valAux.Parameters.Param1.sValue=tcnLedCompan.value
		    valAux.Parameters.Param2.sValue=Field.value
    }
}
//%	LoadBudgetWork: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function LoadBudgetWork(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		    valBudgetWork.Parameters.Param1.sValue=tcnLedCompan.value
		    valBudgetWork.Parameters.Param2.sValue=tcnYearWork.value
		    valBudgetWork.Parameters.Param3.sValue='1'
		    valBudgetWork.Parameters.Param4.sValue=Field.value
    }
}
//%	LoadBudgetComp: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function LoadBudgetComp(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		    valBudgetComp.Parameters.Param1.sValue=tcnLedCompan.value
		    valBudgetComp.Parameters.Param2.sValue=tcnYearWork.value
		    valBudgetComp.Parameters.Param3.sValue='1'
		    valBudgetComp.Parameters.Param4.sValue=Field.value
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
	.Write(mobjMenu.MakeMenu("CP008", "CP008_k.aspx", 1, ""))
End With
mobjMenu = Nothing

%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmBudInqUpd" ACTION="ValBudget.aspx?sTime=1">
<BR>
<%
Call insLoadCP008_k()
%>	
</BODY>
</FORM>
</HTML>





