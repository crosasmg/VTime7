<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues

Dim mclsBal_Histor As eLedge.Bal_histor



Dim mintAccount As Object


'----------------------------------------------------------------------------
Private Sub insLoadCP003_k()
	'----------------------------------------------------------------------------
	Response.Write(mobjValues.ButtonLedCompan("LedCompan", 6, GetLocalResourceObject("LedCompanToolTip"),  , "insGetNumber(this)"))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=3 WIDTH=""20%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL><A NAME=""Comparación"">" & GetLocalResourceObject("AnchorComparaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL><A NAME=""Estilo"">" & GetLocalResourceObject("AnchorEstiloCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""2""><HR></TD>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""1""><HR></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>	" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.OptionControl(0, "optSel", GetLocalResourceObject("optSel_CStr1Caption"), CStr(1), CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr1Caption"), CStr(1), CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.OptionControl(0, "optStyle", GetLocalResourceObject("optStyle_CStr1Caption"), mclsBal_Histor.DefaultValuesCP003(CInt(Request.QueryString.Item("nMainAction")), "optStyle0", Session("nLedCompan"), Session("sAccount")), CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.OptionControl(0, "optSel", GetLocalResourceObject("optSel_CStr2Caption"), CStr(0), CStr(2)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr2Caption"), CStr(0), CStr(2)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.OptionControl(0, "optStyle", GetLocalResourceObject("optStyle_CStr2Caption"), mclsBal_Histor.DefaultValuesCP003(CInt(Request.QueryString.Item("nMainAction")), "optStyle1", Session("nLedCompan"), Session("sAccount")), CStr(2)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>&nbsp</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr3Caption"), CStr(0), CStr(3)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE  WIDTH=""100%"" COLS=4 WIDTH=""25%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11544>" & GetLocalResourceObject("cboCompareCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cboCompare", "table7031", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboCompareToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	    <TD><LABEL ID=11464>" & GetLocalResourceObject("valAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valAccount", "tabLedger_acc", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "LoadAccount(this)",  , 20, "", 2))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL ID=11465>" & GetLocalResourceObject("valAuxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sAccount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valAux", "tabLedger_accAux", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , 20, "", 2))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("     <TR>   " & vbCrLf)
Response.Write("        <TD><LABEL ID=11546>" & GetLocalResourceObject("valUnitCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valUnit", "tabTab_cost_c", eFunctions.Values.eValuesType.clngWindowType, mclsBal_Histor.DefaultValuesCP003(CInt(Request.QueryString.Item("nMainAction")), "valUnit_disabled", Session("nLedCompan"), Session("sAccount")), True,  ,  ,  ,  ,  , mclsBal_Histor.DefaultValuesCP003(CInt(Request.QueryString.Item("nMainAction")), "valUnit", Session("nLedCompan"), Session("sAccount")), 20, "", 2))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=11545>" & GetLocalResourceObject("tcnLedger_YearCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnLedger_Year", 4, CStr(0),  , GetLocalResourceObject("tcnLedger_YearToolTip"),  , 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>		")

	
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsBal_Histor = New eLedge.Bal_histor

mobjValues.sCodisplPage = "CP003_K"


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
    self.document.forms[0].valAccount.disabled=false;
    self.document.forms[0].valAux.disabled=false;
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

</SCRIPT>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>

<%With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.MakeMenu("CP003", "CP003_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmHisBalance" ACTION="ValLedGerTra.aspx?sTime=1">
<BR>
<%
Call insLoadCP003_k()
%>	
</BODY>
</FORM>
</HTML>





