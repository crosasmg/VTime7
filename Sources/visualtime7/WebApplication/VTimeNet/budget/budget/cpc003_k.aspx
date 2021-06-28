<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<%@ Import namespace="eBudget" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjMenues As eFunctions.Menues
Dim mobjValues As eFunctions.Values
Dim mclsLed_compan As eLedge.Led_compan
Dim mclsBudget As eBudget.Budget
Dim mblnMonths As Boolean


</script>
<%Response.Expires = 0

mobjMenues = New eFunctions.Menues
mobjValues = New eFunctions.Values
mclsLed_compan = New eLedge.Led_compan
mclsBudget = New eBudget.Budget

mobjValues.sCodisplPage = "cpc003_k"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenues.MakeMenu("CPC003", "CPC003_K.aspx", 1, ""))
End With
mobjMenues = Nothing
%>

<SCRIPT>
//% insStateZone: 
//-----------------------
function insStateZone(){
//-----------------------

	with(self.document){
		with(forms[0]){
			optBalance.disabled = false
			tcnYear.value = ''
			tcnYear.disabled = false
			cbeCurrency.disabled = false
			valBudget.disabled = false
		}
		btnvalBudget.disabled = false
	}
}

//% insCancel: Ejecuta las rutinas necesarias para la cancelación de la transacción
//---------------------------------------------------------------------------------
function insCancel(){return true}
//---------------------------------------------------------------------------------

//% ShowChangeValues: Llama a la página ShowDefValues que ejecuta código necesario
//% para la actualización de los controles de "Header"
//--------------------------------------------------------------------------------
function ShowChangeValues(Field){
//--------------------------------------------------------------------------------

	switch(Field.name){
		case "tcnYear":
			ShowPopUp("/VTimeNet/Budget/Budget/ShowDefValues.aspx?Field=Year&nLed_compan=" + self.document.forms[0].tcnLedCompan.value, "ShowDefValuesBudget", 1, 1,"no","no",2000,2000)
			self.document.forms[0].valBudget.Parameters.Param2.sValue = Field.value
			break;
		case "valBudget":
			self.document.location.href = "/VTimeNet/Budget/Budget/CPC003_K.aspx?sCodispl=CPC003&nLed_compan=" + self.document.forms[0].tcnLedCompan.value + "&sBud_code=" + self.document.forms[0].valBudget.value + "&nYear=" + self.document.forms[0].tcnYear.value + "&nCurrency=" + self.document.forms[0].cbeCurrency.value;
	}
}

//% ShowMonths:		
//--------------------------------------------------------------------------------
function ShowMonths(){
//--------------------------------------------------------------------------------

//+ Se recarga la página para poder actualizar el control de Meses presupuestados (cbeMonth)

	with(self.document){
		location.href = "/VTimeNet/Budget/Budget/CPC003_K.aspx?sCodispl=CPC003&nLed_compan=" + forms[0].tcnLedCompan.value + "&sBud_code=" + forms[0].valBudget.value + "&nYear=" + forms[0].tcnYear.value + "&nCurrency=" + forms[0].cbeCurrency.value
	}
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CPC003_K" ACTION="valBudget.aspx?sTime=1">
	<BR>
<%Response.Write(mobjValues.HiddenControl("tctMonthList", ""))
Response.Write(mobjValues.ButtonLedCompan("LedCompan", 1, GetLocalResourceObject("LedCompanToolTip")))
%>

    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=8451><A NAME="Saldo"><%= GetLocalResourceObject("AnchorSaldoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(0, "optBalance", GetLocalResourceObject("optBalance_1Caption"), CStr(1), "1")%></TD>
            <TD><%=mobjValues.OptionControl(0, "optBalance", GetLocalResourceObject("optBalance_2Caption"),  , "2")%></TD>
        </TR>
        <TR><TD></TD></TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8450><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear", 4, Request.QueryString.Item("nYear"),  ,  ,  , 0,  ,  ,  , "ShowChangeValues(this)", True)%></TD>
            <TD><LABEL ID=8447><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCurrency"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8448><%= GetLocalResourceObject("valBudgetCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nYear", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	
	Response.Write(mobjValues.PossiblesValues("valBudget", "TabBudget", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "ShowChangeValues(this)", True, 10, GetLocalResourceObject("valBudgetToolTip"), eFunctions.Values.eTypeCode.eString))
End With
%></TD>
            <TD><LABEL ID=8449><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
            <TD><%If Request.QueryString.Item("sBud_code") = vbNullString Then '+ Si la página se carga por primera vez
	mobjValues.TypeList = 2 '+ El control valBudget (Presupuesto) no utilizará parámetros
	mblnMonths = True
Else
	mobjValues.TypeList = 1 '+ El control valBudget (Presupuesto) utilizará parámetros
	mblnMonths = False
End If

'+ Busca los meses presupuesados para el Ejercicio contable dado

mobjValues.List = mclsBudget.insMonthValues(mobjValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sBud_code"), mobjValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble))

Response.Write(mobjValues.PossiblesValues("cbeMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , CBool(mblnMonths),  , GetLocalResourceObject("cbeMonthToolTip")))
%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mclsBudget = Nothing
mclsLed_compan = Nothing
mobjValues = Nothing
%>




