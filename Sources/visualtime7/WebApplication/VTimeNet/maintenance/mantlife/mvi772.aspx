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
Dim mclsTar_Sef As eBranches.Tar_sef
Dim mcolTar_Sef As eBranches.Tar_sefs


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_initColumnCaption"), "tcnAge_init", 3, vbNullString,  , GetLocalResourceObject("tcnAge_initColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_endColumnCaption"), "tcnAge_end", 3, vbNullString,  , GetLocalResourceObject("tcnAge_endColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_initColumnCaption"), "tcnCapital_init", 18, vbNullString,  , GetLocalResourceObject("tcnCapital_initColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_endColumnCaption"), "tcnCapital_end", 18, vbNullString,  , GetLocalResourceObject("tcnCapital_endColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, vbNullString,  , GetLocalResourceObject("tcnRateColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTaxColumnCaption"), "tcnTax", 9, vbNullString,  , GetLocalResourceObject("tcnTaxColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnType_tarColumnCaption"), "tcnType_tar", "Table5584", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnType_tarColumnToolTip"))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI772"
		.sCodisplPage = "MVI772"
		.ActionQuery = mobjValues.ActionQuery
		.Top = 100
		.Height = 340
		.Width = 300
		.WidthDelete = 400
		.Columns("tcnCapital_init").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nRole=" & Request.QueryString.Item("nRole")
		.sDelRecordParam = .sEditRecordParam & "&nAge_init=' + marrArray[lintIndex].tcnAge_init + '&nCapital_init=' + marrArray[lintIndex].tcnCapital_init + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("tcnType_Tar").BlankPosition = False
	End With
End Sub

'% insPreMVI772: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI772()
	'--------------------------------------------------------------------------------------------
	If mcolTar_Sef.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each mclsTar_Sef In mcolTar_Sef
			With mobjGrid
				.Columns("tcnAge_init").DefValue = CStr(mclsTar_Sef.nAge_init)
				.Columns("tcnAge_end").DefValue = CStr(mclsTar_Sef.nAge_end)
				.Columns("tcnCapital_init").DefValue = CStr(mclsTar_Sef.nCapital_init)
				.Columns("tcnCapital_end").DefValue = CStr(mclsTar_Sef.nCapital_end)
				.Columns("tcnRate").DefValue = CStr(mclsTar_Sef.nRate)
				.Columns("tcnType_tar").DefValue = CStr(mclsTar_Sef.nType_tar)
				.Columns("tcnTax").DefValue = CStr(mclsTar_Sef.nTax)
				Response.Write(.DoRow)
			End With
		Next mclsTar_Sef
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreMVI772Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI772Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())

			Call mclsTar_Sef.insPostMVI772(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAge_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital_init"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"))

		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI772", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mcolTar_Sef = New eBranches.Tar_sefs
mobjValues = New eFunctions.Values
mclsTar_Sef = New eBranches.Tar_sef
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI772"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 24/10/03 13:43 $|$$Author: Nvaplat18 $"
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI772", "MVI772.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI772" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI772"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI772Upd()
Else
	Call insPreMVI772()
End If

mobjGrid = Nothing
mcolTar_Sef = Nothing
mobjValues = Nothing
mclsTar_Sef = Nothing
mobjMenu = Nothing
%>
</FORM> 
</BODY>
</HTML>




