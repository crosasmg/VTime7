<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "cr726"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_iniColumnCaption"), "tcnAge_ini", 5, vbNullString,  , GetLocalResourceObject("tcnAge_iniColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_reinsuColumnCaption"), "tcnAge_reinsu", 5, vbNullString,  , GetLocalResourceObject("tcnAge_reinsuColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateWomenColumnCaption"), "tcnRateWomen", 9, vbNullString,  , GetLocalResourceObject("tcnRateWomenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremWomenColumnCaption"), "tcnPremWomen", 18, vbNullString,  , GetLocalResourceObject("tcnPremWomenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateMenColumnCaption"), "tcnRateMen", 9, vbNullString,  , GetLocalResourceObject("tcnRateMenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremMenColumnCaption"), "tcnPremMen", 18, vbNullString,  , GetLocalResourceObject("tcnPremMenColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CR726"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 300
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("tcnAge_ini").EditRecord = True
		
		If .nMainAction = 401 Or .nMainAction = 306 Then
			mobjGrid.ActionQuery = True
			.AddButton = False
			.DeleteButton = False
			.Columns("Sel").GridVisible = False
			.Columns("tcnAge_ini").Disabled = True
			.Columns("tcnAge_ini").EditRecord = False
			.Columns("tcnAge_reinsu").Disabled = True
		End If
		
		.Columns("tcnAge_ini").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnAge_reinsu").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sEditRecordParam = "nBranch_rei=" & Request.QueryString.Item("nBranch_rei") & "&nNumber=" & Request.QueryString.Item("nNumber") & "&nType=" & Request.QueryString.Item("nType") & "&nCovergen=" & Request.QueryString.Item("nCovergen") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
		.sDelRecordParam = "nBranch_rei=" & Request.QueryString.Item("nBranch_rei") & "&nNumber=" & Request.QueryString.Item("nNumber") & "&nType=" & Request.QueryString.Item("nType") & "&nCovergen=" & Request.QueryString.Item("nCovergen") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&tcnAge_ini='+ marrArray[lintIndex].tcnAge_ini + '" & "&tcnAge_reinsu=' + marrArray[lintIndex].tcnAge_reinsu + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'%printhead : copia la cabecera en la parte superior de la grilla
'--------------------------------------------------------------------------------------------
Sub printhead()
	'--------------------------------------------------------------------------------------------
	With Request
		
Response.Write("" & vbCrLf)
Response.Write("		<DIV ID=""DivHeaderDup"" >" & vbCrLf)
Response.Write("			<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD><LABEL ID=""100601"">" & GetLocalResourceObject("cbenBranch_rei_newCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.PossiblesValues("cbenBranch_rei_new", "table5000", eFunctions.Values.eValuesType.clngComboType))


Response.Write("</TD>" & vbCrLf)
Response.Write("				    <TD><LABEL ID=""0"">" & GetLocalResourceObject("tcnNumber_newCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.NumericControl("tcnNumber_new", 10, vbNullString,  , GetLocalResourceObject("tcnNumber_newToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD><LABEL ID=""100600"">" & GetLocalResourceObject("cbeType_newCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")

		mobjValues.TypeList = 2
		mobjValues.List = "1"
		Response.Write(mobjValues.PossiblesValues("cbeType_new", "table173", eFunctions.Values.eValuesType.clngComboType))
Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD><LABEL ID=""0"">" & GetLocalResourceObject("tcdStartdate_newCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.DateControl("tcdStartdate_new", CStr(Today),  , GetLocalResourceObject("tcdStartdate_newToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD><LABEL ID=""0"">" & GetLocalResourceObject("valCovergen_newCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.PossiblesValues("valCovergen_new", "tabtab_lifcov2", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCovergen_newToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("					<TD><LABEL ID=""0"">" & GetLocalResourceObject("tcdEffecdate_newCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate_new", CStr(Today),  , GetLocalResourceObject("tcdEffecdate_newToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("            </TABLE>" & vbCrLf)
Response.Write("		</DIV>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		")

		
	End With
	Response.Write(mobjValues.HiddenControl("hddBranch_rei", mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble)))
	Response.Write(mobjValues.HiddenControl("hddNumber", mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble)))
	Response.Write(mobjValues.HiddenControl("hddType", mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble)))
	Response.Write(mobjValues.HiddenControl("hddCovergen", mobjValues.StringToType(Session("nCovergen"), eFunctions.Values.eTypeData.etdDouble)))
	Response.Write(mobjValues.HiddenControl("hddEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)))
	
	mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
End Sub

'% insPreCR726: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCR726()
	'--------------------------------------------------------------------------------------------
	Dim lclsContr_rate_I As eCoReinsuran.Contr_rate_I
	Dim lcolContr_rate_Is As eCoReinsuran.Contr_rate_Is
	Dim lintCoverGen As Integer
	Dim lblnFind As Boolean
	
	lclsContr_rate_I = New eCoReinsuran.Contr_rate_I
	lcolContr_rate_Is = New eCoReinsuran.Contr_rate_Is
	If mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble) = 306 Then
		lintCoverGen = mobjValues.StringToType(Session("nPriorCoverGen"), eFunctions.Values.eTypeData.etdDouble)
		lblnFind = lcolContr_rate_Is.Find("CR726", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), lintCoverGen, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		
	Else
		lintCoverGen = mobjValues.StringToType(Request.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble)
		lblnFind = lcolContr_rate_Is.Find("CR726", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), lintCoverGen, mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End If
	
	If lblnFind Then
		For	Each lclsContr_rate_I In lcolContr_rate_Is
			With mobjGrid
				.Columns("tcnAge_ini").DefValue = CStr(lclsContr_rate_I.nAge_ini)
				.Columns("tcnAge_reinsu").DefValue = CStr(lclsContr_rate_I.nAge_reinsu)
				.Columns("tcnRateWomen").DefValue = CStr(lclsContr_rate_I.nRatewomen)
				.Columns("tcnPremWomen").DefValue = CStr(lclsContr_rate_I.nPremwomen)
				.Columns("tcnRateMen").DefValue = CStr(lclsContr_rate_I.nRatemen)
				.Columns("tcnPremMen").DefValue = CStr(lclsContr_rate_I.nPremmen)
				Response.Write(.DoRow)
			End With
		Next lclsContr_rate_I
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCR726Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCR726Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjCoReinsuranTra As eCoReinsuran.Contr_rate_I
	
	lobjCoReinsuranTra = New eCoReinsuran.Contr_rate_I
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjCoReinsuranTra.insPostCR726("CR307", .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValCoReinsuranTra.aspx", "CR726", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "cr726"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CR726", "CR726.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CR726" ACTION="valCoReinsuranTra.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("CR726"))
Response.Write("<BR>")
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCR726Upd()
Else
	Call insPreCR726()
End If
%>
<SCRIPT LANGUAGE="JavaScript">
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16.59 $" 
</SCRIPT>
</FORM> 
</BODY>
</HTML>






