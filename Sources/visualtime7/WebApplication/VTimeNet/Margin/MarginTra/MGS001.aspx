<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

Dim x_x As Object

'- Variable para controlar el estado de los campos
Dim mblnDisabled As Boolean
Dim mblOrig As Boolean


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lintTypeRec As Object
	Dim lstrTab_tables As String
	Dim lstrTab_Product As String
	
	lintTypeRec = 0
	If mblnDisabled Or mblOrig Then
		lstrTab_tables = "Table10"
	Else
		lstrTab_tables = "tabMG_Allow_branch"
		lintTypeRec = 1
	End If
	If mblOrig Then
		lstrTab_Product = "tabProdMaster1"
	Else
		lstrTab_Product = "tabMG_Allow_product"
	End If
	
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", lstrTab_tables, eFunctions.Values.eValuesType.clngComboType, vbNullString, True,  ,  ,  , "insChangeField(this, ""Branch"",""" & Request.QueryString.Item("Action") & """)", mblnDisabled, 10, GetLocalResourceObject("cbeBranchColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabMG_Allow_product", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "insChangeField(this, ""Product"",""" & Request.QueryString.Item("Action") & """)", True,  , GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTyperecColumnCaption"), "cbeTyperec", "Table5610", eFunctions.Values.eValuesType.clngComboType, lintTypeRec,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTyperecColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  , "insExchangeDate()", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdValdateColumnCaption"), "tcdValdate", CStr(Today),  , GetLocalResourceObject("tcdValdateColumnToolTip"),  ,  , "insExchangeDate()", Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabMG_Allow_modulec", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "insChangeField(this, ""Modulec"",""" & Request.QueryString.Item("Action") & """)", True,  , GetLocalResourceObject("valModulecColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "tabMG_Allow_cover", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "insChangeField(this, ""Cover"",""" & Request.QueryString.Item("Action") & """)", True,  , GetLocalResourceObject("valCoverColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSVSClassColumnCaption"), "cbeSVSClass", "Table71", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeSVSClassColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInitialAmoOriColumnCaption"), "tcnInitialAmoOri", 24, vbNullString,  , GetLocalResourceObject("tcnInitialAmoOriColumnToolTip"), True, 6,  ,  , "ShowChangeAmount()")
		Call .AddAnimatedColumn(0, GetLocalResourceObject("btnAdjustColumnCaption"), "btnAdjust", "/VTimeNet/Images/btnWONotes.png", GetLocalResourceObject("btnAdjustColumnToolTip"),  , "insAdjust(-1)", True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAdjAmoOriColumnCaption"), "tcnAdjAmoOri", 24, CStr(0),  , GetLocalResourceObject("tcnAdjAmoOriColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmoOriColumnCaption"), "tcnAmoOri", 24, vbNullString,  , GetLocalResourceObject("tcnAmoOriColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmoLocalColumnCaption"), "tcnAmoLocal", 24, vbNullString,  , GetLocalResourceObject("tcnAmoLocalColumnToolTip"), True,  ,  ,  ,  , True)
		Call .AddHiddenColumn("hddIdRec", vbNullString)
		Call .AddHiddenColumn("hddStaDet", vbNullString)
		Call .AddHiddenColumn("hddExchange", vbNullString)
		Call .AddHiddenColumn("hddCountAdjust", CStr(0))
		Call .AddHiddenColumn("nIdtable", CStr(0))
		Call .AddHiddenColumn("nAdjAmoLoc", CStr(0))
		
		
		
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MGS001"
		.sCodisplPage = "MGS001"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 490
		.Width = 420
		.Top = 20
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("cbeCurrency").BlankPosition = False
		.sDelRecordParam = "nInsur_area=" & session("nInsur_area") & "&dInitDate=" & Request.QueryString.Item("dInitDate") & "&nIdTable=" & session("nIdtable") & "&nIdRec=' + marrArray[lintIndex].hddIdRec + '" & "&nTabletyp=" & Request.QueryString.Item("nTabletyp") & "&nSource=" & Request.QueryString.Item("nSource") & "&nClaimClass=" & Request.QueryString.Item("nClaimClass") & "&dEndDate=" & Request.QueryString.Item("dEndDate")
		
		.sEditRecordParam = "nInsur_area=" & session("nInsur_area") & "&nTabletyp=" & Request.QueryString.Item("nTabletyp") & "&nSource=" & Request.QueryString.Item("nSource") & "&nClaimClass=" & Request.QueryString.Item("nClaimClass") & "&dInitDate=" & Request.QueryString.Item("dInitDate") & "&dEndDate=" & Request.QueryString.Item("dEndDate") & "&nIdTable=' + self.document.forms[0].hddIDTable.value + '"
		
		.Columns("cbeBranch").EditRecord = False
		.Columns("cbeCurrency").EditRecord = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		With .Columns("cbeBranch").Parameters
			.Add("nInsur_area", mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nTabletyp", mobjValues.StringToType(Request.QueryString.Item("nTabletyp"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nSource", mobjValues.StringToType(Request.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		With .Columns("valProduct").Parameters
			.Add("nInsur_area", mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nTabletyp", mobjValues.StringToType(Request.QueryString.Item("nTabletyp"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nSource", mobjValues.StringToType(Request.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nMainaction", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("sAction", Request.QueryString.Item("Action"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.ReturnValue("sModulec", False, vbNullString, True)
		End With
		With .Columns("valModulec").Parameters
			.Add("nInsur_area", mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nTabletyp", mobjValues.StringToType(Request.QueryString.Item("nTabletyp"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nSource", mobjValues.StringToType(Request.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nMainaction", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("sAction", Request.QueryString.Item("Action"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		With .Columns("valCover").Parameters
			.Add("nInsur_area", mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nTabletyp", mobjValues.StringToType(Request.QueryString.Item("nTabletyp"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nSource", mobjValues.StringToType(Request.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nMainaction", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("sAction", Request.QueryString.Item("Action"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.ReturnValue("nBranch_est", False, vbNullString, True)
		End With
	End With
	With Response
		.Write(mobjValues.HiddenControl("hddInsur_area", session("nInsur_area")))
		.Write(mobjValues.HiddenControl("hddTabletyp", Request.QueryString.Item("nTabletyp")))
		.Write(mobjValues.HiddenControl("hddSource", Request.QueryString.Item("nSource")))
		.Write(mobjValues.HiddenControl("hddClaimClass", Request.QueryString.Item("nClaimClass")))
		.Write(mobjValues.HiddenControl("hddInitDate", Request.QueryString.Item("dInitDate")))
		.Write(mobjValues.HiddenControl("hddEndDate", Request.QueryString.Item("dEndDate")))
		.Write(mobjValues.HiddenControl("hddIDTable", session("nIdtable")))
	End With
End Sub

'% insPreMGS001: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMGS001()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lclsMargin_detail As Object
	Dim lcolMargin_detail As eMargin.Margin_details
	lcolMargin_detail = New eMargin.Margin_details
	
	
	
	If lcolMargin_detail.Find(mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nTabletyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nClaimClass"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintIndex = 0
		For	Each lclsMargin_detail In lcolMargin_detail
			With mobjGrid
				.Columns("valProduct").Parameters.Add("nBranch", lclsMargin_detail.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				.Columns("valModulec").Parameters.Add("nBranch", lclsMargin_detail.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valModulec").Parameters.Add("nProduct", lclsMargin_detail.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				
				.Columns("valCover").Parameters.Add("nBranch", lclsMargin_detail.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valCover").Parameters.Add("nProduct", lclsMargin_detail.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valCover").Parameters.Add("nModulec", lclsMargin_detail.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				.Columns("cbeBranch").DefValue = lclsMargin_detail.nBranch
				.Columns("valProduct").DefValue = lclsMargin_detail.nProduct
				.Columns("cbeTyperec").DefValue = lclsMargin_detail.nTypeRec
				.Columns("cbeCurrency").DefValue = lclsMargin_detail.nCurrency
				.Columns("tcdValdate").DefValue = lclsMargin_detail.dValDate
				.Columns("valModulec").DefValue = lclsMargin_detail.nModulec
				.Columns("valCover").DefValue = lclsMargin_detail.nCover
				.Columns("cbeSVSClass").DefValue = lclsMargin_detail.nSVSClass
				.Columns("tcnInitialAmoOri").DefValue = lclsMargin_detail.nInitialAmoOri
				.Columns("btnAdjust").Disabled = lclsMargin_detail.sStaDet = "false"
				.Columns("btnAdjust").HRefScript = "insAdjust(" & lintIndex & ")"
				.Columns("hddStaDet").DefValue = lclsMargin_detail.sStaDet
				.Columns("tcnAdjAmoOri").DefValue = lclsMargin_detail.nAdjAmoOri
				.Columns("tcnAmoOri").DefValue = lclsMargin_detail.nAmountOri
				.Columns("tcnAmoLocal").DefValue = CStr(lclsMargin_detail.nInitialAmoLoc + lclsMargin_detail.nAdjAmoLoc)
				.Columns("hddIdRec").DefValue = lclsMargin_detail.nIdRec
				.Columns("hddExchange").DefValue = lclsMargin_detail.nExchange
				.Columns("hddCountAdjust").DefValue = lclsMargin_detail.nCountAdjust
				.Columns("Sel").OnClick = "insSel(" & lintIndex & ")"
				.Columns("nIdtable").DefValue = lclsMargin_detail.nIdtable
				.Columns("nAdjAmoLoc").DefValue = lclsMargin_detail.nAdjAmoLoc
				
				session("nIdtable") = lclsMargin_detail.nIdtable
				session("nAdjAmoLoc") = lclsMargin_detail.nAdjAmoLoc
				Response.Write(.DoRow)
				
			End With
			lintIndex = lintIndex + 1
			
		Next lclsMargin_detail
	Else
		Response.Write("<SCRIPT>self.document.forms[0].hddIDTable.value=""""</" & "Script>")
		
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lclsMargin_detail = Nothing
	lcolMargin_detail = Nothing
End Sub

'% insPreMGS001Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMGS001Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsMargin_detail As eMargin.Margin_detail
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsMargin_detail = New eMargin.Margin_detail
			If lclsMargin_detail.inspostMGS001(.QueryString.Item("Action"), mobjValues.StringToType(session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(session("nIdTable"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nIdRec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nTabletyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nClaimClass"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("dEndDate"), eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMarginTra.aspx", "MGS001", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If .QueryString.Item("Action") <> "Del" Then
			Response.Write("<SCRIPT>" & "if (self.document.forms[0].tcdValdate.value == ''){" & "self.document.forms[0].tcdValdate.value = '" & Today & "';" & "self.document.forms[0].tcdValdate.disabled = false;" & "self.document.forms[0].btn_tcdValdate.disabled = false;" & "}" & "</" & "Script>")
		End If
		If .QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>" & "with(self.document.forms[0]){" & "tcdValdate.Value = " & Today & ";" & "if(hddStaDet.value=='2'){" & "tcnInitialAmoOri.disabled=true;" & "btnAdjust.disabled=false;" & "}" & "else " & "btnAdjust.disabled=true;" & "}" & "insChangeField(self.document.forms[0].cbeBranch, ""Branch"",""" & .QueryString.Item("Action") & """)" & "</" & "Script>")
		Else
			If .QueryString.Item("Action") = "Add" Then
				Response.Write("<SCRIPT>insExchangeDate();</" & "Script>")
			End If
		End If
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MGS001"

mblnDisabled = Request.QueryString.Item("Action") = "Update" Or Request.QueryString.Item("nTabletyp") = "5"

If IsNothing(Request.QueryString.Item("Action")) And Request.QueryString.Item("nMainAction") = "302" Then
	mblOrig = True
ElseIf Request.QueryString.Item("Action") = "Update" And Request.QueryString.Item("nMainAction") = "302" Then 
	mblOrig = True
ElseIf Request.QueryString.Item("nMainAction") = "401" Then 
	mblOrig = True
Else
	mblOrig = False
End If
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>






<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 20 $|$$Date: 18/12/03 17:00 $|$$Author: Nvaplat15 $"

//% insChangeField: se controla el estado de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function insChangeField(Field, sOption, sUpdate){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(sOption){
			case "Branch":
				if(sUpdate!="Update"){
					valProduct.value='';
					UpdateDiv('valProductDesc', '');
					valProduct.disabled=(Field.value==0)?true:false;
				}
				btnvalProduct.disabled=valProduct.disabled;
				valProduct.Parameters.Param5.sValue = Field.value;
				valModulec.Parameters.Param5.sValue = Field.value;
				valCover.Parameters.Param5.sValue = Field.value;
				valCover.disabled = true;
				break;
			case "Product":
				if(sUpdate!="Update"){
					valModulec.value='';
					UpdateDiv('valModulecDesc', '');
					valCover.value='';
					UpdateDiv('valCoverDesc', '');
					valModulec.disabled=(valProduct_sModulec.value=='1')?false:true;
					btnvalCover.disabled=valCover.disabled
				}
				btnvalModulec.disabled=valModulec.disabled;
				if(!valProduct.disabled){
					valCover.disabled=(valProduct_sModulec.value=='1')?true:false;
					btnvalCover.disabled=valCover.disabled
					}
				valModulec.Parameters.Param6.sValue = Field.value;
				valCover.Parameters.Param6.sValue = Field.value;
				break;
			case "Modulec":
				if(sUpdate!="Update"){
					valCover.value='';
					UpdateDiv('valCoverDesc', '');
					valCover.disabled=(Field.value=='')?true:false;
				}
				btnvalCover.disabled=valCover.disabled;
                if (Field.value!= '')  
				valCover.Parameters.Param7.sValue = Field.value;
				break;
			case "Cover":
                if (valCover.value != '')
				cbeSVSClass.value=valCover_nBranch_est.value;
				break;
		}
	}
	
}

//% insAdjust: se realizan los ajustes de los movimientos
//%			   Se invoca la ventana MGS002 (Movimientos de ajustes del margen de solvencia
//--------------------------------------------------------------------------------------------
function insAdjust(nIndex){
//--------------------------------------------------------------------------------------------
        
	
	var lstrQueryString

	with(self.document.forms[0]){
		lstrQueryString = 'nInsur_area=' + '<%=session("nInsur_area")%>' + 
	                      '&nTableTyp=' + hddTabletyp.value + 
	                      '&nSource=' + hddSource.value + 
	                      '&dInitDate=' + hddInitDate.value + 
	                      //'&nIDTable=' + hddIDTable.value
				          '&nIDTable=' + nIdtable.value
		                  
		                  
		              if(nIndex<0)
//+ Si se invoca desde la ventana PopUp
	
			lstrQueryString += '&nMainAction=302' + 
						       '&nBranch=' + cbeBranch.value + 
							   '&nProduct=' + valProduct.value + 
							   '&nCurrency=' + cbeCurrency.value + 
							   '&nTyperec=' + cbeTyperec.value + 
							   '&nModulec=' + valModulec.value + 
							   '&nCover=' + valCover.value + 
							   '&nIdRec=' + hddIdRec.value +
							   '&dValDate=' + tcdValdate.value
		
		else
//+ Si se invoca desde la grilla, columna (Ajustes)
       
			lstrQueryString += '&nMainAction=401' + 
			                   '&nBranch=' + marrArray[nIndex].cbeBranch + 
							   '&nProduct=' + marrArray[nIndex].valProduct + 
							   '&nCurrency=' + marrArray[nIndex].cbeCurrency + 
							   '&nTyperec=' + marrArray[nIndex].cbeTyperec + 
							   '&nModulec=' + marrArray[nIndex].valModulec + 
							   '&nCover=' + marrArray[nIndex].valCover + 
							   '&nIdRec=' + marrArray[nIndex].hddIdRec + 
 							   '&dValDate=' + marrArray[nIndex].tcdValdate
	                           
	}
	
	ShowPopUp('MGS002.aspx?' + lstrQueryString, 'MGS002', 600 , 400, 'yes', 'no', 100, 50)
	
}

//% ShowChangeAmount: actualiza el monto final en moneda origen y local
//-------------------------------------------------------------------------------------------
function ShowChangeAmount(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (tcnInitialAmoOri.value!=0){
			tcnAmoOri.value = VTFormat(insConvertNumber(tcnInitialAmoOri.value) + insConvertNumber(tcnAdjAmoOri.value), '', '', '', 6, true);
			if ((hddExchange.value != '') &&
				(hddExchange.value != 0))		
			    tcnAmoLocal.value = VTFormat((insConvertNumber(tcnInitialAmoOri.value) + insConvertNumber(tcnAdjAmoOri.value)) * insConvertNumber(hddExchange.value), '', '', '', 0, true);
			else{
			    insDefValues('nExchange', 'dEffecdate=' + tcdValdate.value + '&nCurrency=' + cbeCurrency.value);
			    tcnAmoLocal.value = VTFormat((insConvertNumber(tcnInitialAmoOri.value) + insConvertNumber(tcnAdjAmoOri.value)) * insConvertNumber(hddExchange.value), '', '', '', 0, true);
			}
			    
		}			
	}
}	

//% insExchangeDate: se calcula el factor de cambio a la fecha
//-------------------------------------------------------------------------------------------
function insExchangeDate(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		insDefValues('nExchange', 'dEffecdate=' + tcdValdate.value + '&nCurrency=' + cbeCurrency.value)
	}
}

//% insSel: se valida si se puede eliminar o no el registro
//--------------------------------------------------------------------------------------------
function insSel(nIndex){
//--------------------------------------------------------------------------------------------
	var lblnError = false;
	if(marrArray[nIndex].hddCountAdjust>0){
		alert('Err. 56037: <%=eFunctions.Values.GetMessage(56037)%>');
		lblnError = true;
	}

	if(lblnError){
		marrArray[nIndex].Sel=false;
		if(marrArray.length>1)
			self.document.forms[0].Sel[nIndex].checked=false;
		else
			self.document.forms[0].Sel.checked=false;
	}
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MGS001", "MGS001.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MGS001" ACTION="valMarginTra.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MGS001"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMGS001Upd()
Else
	Call insPreMGS001()
End If
mobjMenu = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>






