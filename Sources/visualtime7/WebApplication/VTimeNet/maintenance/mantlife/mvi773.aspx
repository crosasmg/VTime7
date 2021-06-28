<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mclsTab_Capital As eBranches.Tab_Capital
Dim mcolTab_Capital As eBranches.Tab_Capitals


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_initColumnCaption"), "tcnAge_init", 5, vbNullString,  , GetLocalResourceObject("tcnAge_initColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_endColumnCaption"), "tcnAge_end", 5, vbNullString,  , GetLocalResourceObject("tcnAge_endColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInipercovColumnCaption"), "tcnInipercov", 5, vbNullString,  , GetLocalResourceObject("tcnInipercovColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndpercovColumnCaption"), "tcnEndpercov", 5, vbNullString,  , GetLocalResourceObject("tcnEndpercovColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInipaycovColumnCaption"), "tcnInipaycov", 5, vbNullString,  , GetLocalResourceObject("tcnInipaycovColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndpaycovColumnCaption"), "tcnEndpaycov", 5, vbNullString,  , GetLocalResourceObject("tcnEndpaycovColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSexclienColumnCaption"), "cbeSexclien", "Table18", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSexclienColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkSmokingColumnCaption"), "chkSmoking", "", True, "1",  ,  , GetLocalResourceObject("chkSmokingColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremanualColumnCaption"), "tcnPremanual", 18, vbNullString,  , GetLocalResourceObject("tcnPremanualColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, vbNullString,  , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		
		Call .AddHiddenColumn("hddId", "")
		
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI773"
		.sCodisplPage = "MVI773"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 420
		.Width = 350
		.Columns("tcnAge_init").EditRecord = True
		
		.Columns("cbeSexclien").BlankPosition = False
		.Columns("cbeCurrency").BlankPosition = False
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nRole=" & Request.QueryString.Item("nRole")
		.sDelRecordParam = .sEditRecordParam & "&nId=' + marrArray[lintIndex].hddId + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
        .Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 2)
        .Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 2)
		
	End With
End Sub

'% insPreMVI773: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI773()
	'--------------------------------------------------------------------------------------------
	If mcolTab_Capital.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each mclsTab_Capital In mcolTab_Capital
			With mobjGrid
				
				.Columns("tcnAge_init").DefValue = CStr(mclsTab_Capital.nAge_init)
				.Columns("tcnAge_end").DefValue = CStr(mclsTab_Capital.nAge_end)
				.Columns("tcnInipercov").DefValue = CStr(mclsTab_Capital.nInipercov)
				.Columns("tcnEndpercov").DefValue = CStr(mclsTab_Capital.nEndpercov)
				.Columns("tcnInipaycov").DefValue = CStr(mclsTab_Capital.nInipaycov)
				.Columns("tcnEndpaycov").DefValue = CStr(mclsTab_Capital.nEndpaycov)
				.Columns("cbeSexclien").DefValue = CStr(mclsTab_Capital.sSexclien)
				.Columns("chkSmoking").DefValue = CStr(mclsTab_Capital.sSmoking)
				
				.Columns("chkSmoking").Checked = mclsTab_Capital.sSmoking
				
				.Columns("tcnPremanual").DefValue = CStr(mclsTab_Capital.nPremanual)
				.Columns("tcnCapital").DefValue = CStr(mclsTab_Capital.nCapital)
				.Columns("cbeCurrency").DefValue = CStr(mclsTab_Capital.nCurrency)
				.Columns("hddId").DefValue = CStr(mclsTab_Capital.nId)
				
				
				Response.Write(.DoRow)
			End With
		Next mclsTab_Capital
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreMVI773Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI773Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			Call mclsTab_Capital.insPostMVI773(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), "1", "1", eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI773", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mcolTab_Capital = New eBranches.Tab_Capitals
mobjValues = New eFunctions.Values
mclsTab_Capital = New eBranches.Tab_Capital
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI773"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI773", "MVI773.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI773" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI773"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI773Upd()
Else
	Call insPreMVI773()
End If

mobjGrid = Nothing
mcolTab_Capital = Nothing
mobjValues = Nothing
mclsTab_Capital = Nothing
mobjMenu = Nothing
%>
</FORM> 
</BODY>
</HTML>




