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
Dim mcolCap_crelife As eBranches.Cap_crelifes


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 5, vbNullString,  , GetLocalResourceObject("tcnYearColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDurationColumnCaption"), "tcnDuration", 5, vbNullString,  , GetLocalResourceObject("tcnDurationColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, vbNullString,  , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCurrencyColumnCaption"), "valCurrency", "Table11", eFunctions.Values.eValuesType.clngWindowType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCurrencyColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI757"
		.sCodisplPage = "MVI757"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnYear").EditRecord = True
		.Height = 280
		.Width = 320
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nDuration=' + marrArray[lintIndex].tcnDuration + '&nYear=' + marrArray[lintIndex].tcnYear + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI757: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI757()
	'--------------------------------------------------------------------------------------------
	Dim lclsCap_crelife As Object
	
	mcolCap_crelife = New eBranches.Cap_crelifes
	If mcolCap_crelife.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate")) Then
		For	Each lclsCap_crelife In mcolCap_crelife
			With mobjGrid
				.Columns("tcnDuration").DefValue = lclsCap_crelife.nDuration
				.Columns("tcnYear").DefValue = lclsCap_crelife.nYear
				.Columns("tcnCapital").DefValue = lclsCap_crelife.nCapital
				.Columns("valCurrency").DefValue = lclsCap_crelife.nCurrency
				
				Response.Write(.DoRow)
			End With
		Next lclsCap_crelife
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolCap_crelife = Nothing
End Sub

'% insPreMVI757Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI757Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjCap_crelife As eBranches.Cap_crelife
	
	lobjCap_crelife = New eBranches.Cap_crelife
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			If lobjCap_crelife.InsPostMVI757(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDuration"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI757", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjCap_crelife = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI757"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "MVI757", "MVI757.aspx"))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI757.aspx" ACTION="valMantLife.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("MVI757"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI757Upd()
Else
	Call insPreMVI757()
End If

mobjValues = Nothing
mobjGrid = Nothing
mobjMenu = Nothing
%>
</FORM> 
</BODY>
</HTML>




