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


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "cr765"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_reinsuColumnCaption"), "tcnAge_reinsu", 3, vbNullString,  , GetLocalResourceObject("tcnAge_reinsuColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateWomenColumnCaption"), "tcnRateWomen", 9, vbNullString,  , GetLocalResourceObject("tcnRateWomenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremWomenColumnCaption"), "tcnPremWomen", 18, vbNullString,  , GetLocalResourceObject("tcnPremWomenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateMenColumnCaption"), "tcnRateMen", 9, vbNullString,  , GetLocalResourceObject("tcnRateMenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremMenColumnCaption"), "tcnPremMen", 18, vbNullString,  , GetLocalResourceObject("tcnPremMenColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CR765"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 270
		.Width = 400
		.WidthDelete = 475
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("tcnAge_reinsu").EditRecord = True
		
		If .nMainAction = 401 Or .nMainAction = 306 Then
			.Columns("Sel").GridVisible = False
			.AddButton = False
			.DeleteButton = False
			.Columns("tcnAge_reinsu").Disabled = True
			.Columns("tcnAge_reinsu").EditRecord = False
		End If
		
		.Columns("tcnAge_reinsu").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sDelRecordParam = "nNumber=" & mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble) & "&nBranch_rei=" & mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble) & "&nType=" & mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble) & "&nCovergen=" & mobjValues.StringToType(Session("nCovergen"), eFunctions.Values.eTypeData.etdDouble) & "&sSmoking=" & Session("sSmoking") & "&sPeriodpol=" & Session("sPeriodpol") & "&nTyperisk=" & Session("nTyperisk") & "&nCap_ini=" & mobjValues.StringToType(Session("nCap_ini"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nAge_reinsu=' + marrArray[lintIndex].tcnAge_reinsu + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreCR765: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCR765()
	'--------------------------------------------------------------------------------------------
	Dim lclsContr_rate_II As eCoReinsuran.Contr_rate_II
	Dim lcolContr_rate_IIs As eCoReinsuran.Contr_rate_IIs
	
	lclsContr_rate_II = New eCoReinsuran.Contr_rate_II
	lcolContr_rate_IIs = New eCoReinsuran.Contr_rate_IIs
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 Then
		Call lcolContr_rate_IIs.Find(mobjValues.StringToType(Session("nLastNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLastBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLastType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLastCovergen"), eFunctions.Values.eTypeData.etdDouble), Session("sLastSmoking"), Session("sLastPeriodPol"), Session("nLastTypeRisk"), mobjValues.StringToType(Session("nLastCapIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dLastEffecdate"), eFunctions.Values.eTypeData.etdDate))
	Else
		Call lcolContr_rate_IIs.Find(mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCovergen"), eFunctions.Values.eTypeData.etdDouble), Session("sSmoking"), Session("sPeriodPol"), Session("nTypeRisk"), mobjValues.StringToType(Session("nCapIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End If
	For	Each lclsContr_rate_II In lcolContr_rate_IIs
		With mobjGrid
			.Columns("tcnAge_reinsu").DefValue = CStr(lclsContr_rate_II.nAge_reinsu)
			.Columns("tcnRateWomen").DefValue = CStr(lclsContr_rate_II.nRatewomen)
			.Columns("tcnPremWomen").DefValue = CStr(lclsContr_rate_II.nPremwomen)
			.Columns("tcnRateMen").DefValue = CStr(lclsContr_rate_II.nRatemen)
			.Columns("tcnPremMen").DefValue = CStr(lclsContr_rate_II.nPremmen)
			Response.Write(.DoRow)
		End With
	Next lclsContr_rate_II
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCR765Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCR765Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjCoReinsuranTra As eCoReinsuran.Contr_rate_II
	
	lobjCoReinsuranTra = New eCoReinsuran.Contr_rate_II
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call lobjCoReinsuranTra.insPostCR765(.QueryString.Item("Action"), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCoverGen"), eFunctions.Values.eTypeData.etdDouble), Session("sSmoking"), Session("sPeriodPol"), Session("nTypeRisk"), mobjValues.StringToType(Session("nCapIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nCapEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremWomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRateMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremMen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValCoReinsuranTra.aspx", "CR765", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "cr765"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CR765", "CR765.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CR765" ACTION="valCoReinsuranTra.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("CR765"))
Response.Write("<BR>")
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCR765Upd()
Else
	Call insPreCR765()
End If
%>
<SCRIPT>
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16.59 $" 
</SCRIPT>
</FORM> 
</BODY>
</HTML>






