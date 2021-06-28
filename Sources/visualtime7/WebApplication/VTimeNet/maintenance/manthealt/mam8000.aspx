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
Dim mcolTar_Health As eBranches.Tar_Healths


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIniColumnCaption"), "tcnIni", 3, vbNullString,  , GetLocalResourceObject("tcnIniColumnToolTip"),  , 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndColumnCaption"), "tcnEnd", 3, vbNullString,  , GetLocalResourceObject("tcnEndColumnToolTip"),  , 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 3, vbNullString,  , GetLocalResourceObject("tcnAgeColumnToolTip"),  , 0)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSexColumnCaption"), "cbeSex", "Table18", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSexColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, vbNullString,  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MAM8000"
		.sCodisplPage = "MAM8000"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnAge").EditRecord = True
		.Height = 380
		.Width = 320
		.Top = 100
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		
		.sEditRecordParam = "nTyperisk=" & Request.QueryString.Item("nTyperisk")
		.sDelRecordParam = "nAge=' + marrArray[lintIndex].tcnAge + '" & "&nIni=' + marrArray[lintIndex].tcnIni + '" & "&nEnd=' + marrArray[lintIndex].tcnEnd + '" & "&nSex=' + marrArray[lintIndex].cbeSex + '"
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMAM8000: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMAM8000()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_Health As Object
	
	mcolTar_Health = New eBranches.Tar_Healths
	
	If mcolTar_Health.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nAgreement"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsTar_Health In mcolTar_Health
			With mobjGrid
				.Columns("tcnAge").DefValue = lclsTar_Health.nAge
				.Columns("cbeSex").DefValue = lclsTar_Health.nSex
				.Columns("tcnIni").DefValue = lclsTar_Health.nInsu_Count_Ini
				.Columns("tcnEnd").DefValue = lclsTar_Health.nInsu_Count_End
				.Columns("tcnRate").DefValue = lclsTar_Health.nRate
				
				Response.Write(.DoRow)
			End With
		Next lclsTar_Health
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreMAM8000Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMAM8000Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjTar_Health As eBranches.Tar_Health
	
	lobjTar_Health = New eBranches.Tar_Health
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjTar_Health.insPostMAM8000(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSex"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), CDate(Nothing), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmanthealt.aspx", "MAM8000", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MAM8000"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MAM8000", "MAM8000.aspx"))
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
<FORM METHOD="POST" NAME="MAM8000.aspx" ACTION="valmanthealt.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MAM8000"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMAM8000Upd()
Else
	Call insPreMAM8000()
End If
%>
</FORM> 
</BODY>
</HTML>




