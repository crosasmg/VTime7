<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolTar_tralife As eBranches.Tar_tralifes


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInipercovColumnCaption"), "tcnInipercov", 3, vbNullString,  , GetLocalResourceObject("tcnInipercovColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndpercovColumnCaption"), "tcnEndpercov", 3, vbNullString,  , GetLocalResourceObject("tcnEndpercovColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInipaycovColumnCaption"), "tcnInipaycov", 3, vbNullString,  , GetLocalResourceObject("tcnInipaycovColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndpaycovColumnCaption"), "tcnEndpaycov", 3, vbNullString,  , GetLocalResourceObject("tcnEndpaycovColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 3, vbNullString,  , GetLocalResourceObject("tcnAgeColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRatewomenColumnCaption"), "tcnRatewomen", 9, vbNullString,  , GetLocalResourceObject("tcnRatewomenColumnToolTip"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremwomenColumnCaption"), "tcnPremwomen", 18, vbNullString,  , GetLocalResourceObject("tcnPremwomenColumnToolTip"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRatemenColumnCaption"), "tcnRatemen", 9, vbNullString,  , GetLocalResourceObject("tcnRatemenColumnToolTip"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremmenColumnCaption"), "tcnPremmen", 18, vbNullString,  , GetLocalResourceObject("tcnPremmenColumnToolTip"),  , 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valType_tarColumnCaption"), "valType_tar", "table5584", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valType_tarColumnToolTip"))
		Call .AddHiddenColumn("hddTyperisk", Request.QueryString.Item("nTyperisk"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI729"
		.sCodisplPage = "MVI729"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnAge").EditRecord = True
		.Height = 380
		.Width = 320
		.Top = 100
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valType_tar").BlankPosition = False
		.sEditRecordParam = "nTyperisk=" & Request.QueryString.Item("nTyperisk")
		.sDelRecordParam = "nAge=' + marrArray[lintIndex].tcnAge + '" & "&nInipercov=' + marrArray[lintIndex].tcnInipercov + '" & "&nInipaycov=' + marrArray[lintIndex].tcnInipaycov + '" & "&nTyperisk=" & Request.QueryString.Item("nTyperisk")
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI729: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI729()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_tralife As Object
	
	mcolTar_tralife = New eBranches.Tar_tralifes
	
	If mcolTar_tralife.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("sSmoking"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nTyperisk"), eFunctions.Values.eTypeData.etdLong)) Then
		For	Each lclsTar_tralife In mcolTar_tralife
			With mobjGrid
				.Columns("tcnAge").DefValue = lclsTar_tralife.nAge
				.Columns("tcnInipercov").DefValue = lclsTar_tralife.nInipercov
				.Columns("tcnInipaycov").DefValue = lclsTar_tralife.nInipaycov
				.Columns("tcnRatewomen").DefValue = lclsTar_tralife.nRatewomen
				.Columns("tcnPremwomen").DefValue = lclsTar_tralife.nPremwomen
				.Columns("tcnRatemen").DefValue = lclsTar_tralife.nRatemen
				.Columns("tcnPremmen").DefValue = lclsTar_tralife.nPremmen
				.Columns("valType_tar").DefValue = lclsTar_tralife.nType_tar
				.Columns("tcnEndpercov").DefValue = lclsTar_tralife.nEndpercov
				.Columns("tcnEndpaycov").DefValue = lclsTar_tralife.nEndpaycov
				
				Response.Write(.DoRow)
			End With
		Next lclsTar_tralife
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreMVI729Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI729Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjTar_tralife As eBranches.Tar_tralife
	
	lobjTar_tralife = New eBranches.Tar_tralife
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjTar_tralife.insPostMVI729(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("sSmoking"), mobjValues.StringToType(.QueryString.Item("nAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nInipercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nInipaycov"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("tcnRatewomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremwomen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRatemen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremmen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valType_tar"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nEndpercov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nEndpaycov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTyperisk"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI729", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI729"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI729", "MVI729.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 20/10/03 12:40 $|$$Author: Nvaplat18 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI729.aspx" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI729"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI729Upd()
Else
	Call insPreMVI729()
End If
%>
</FORM> 
</BODY>
</HTML>




