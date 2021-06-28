<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página.

Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú.

Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página.

Dim mclsDisc_percentage As eBranches.Disc_percentage
Dim mcolDisc_percentages As eBranches.Disc_percentages


'% insDefineHeader: Se definen las propiedades del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid.
	
	
	With mobjGrid.Columns
		'+ Estructura del GRID modificada debido a cambios en el funcional de la transacción - ACM - 06/08/2003
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_iniColumnCaption"), "tcnAge_ini", 4, vbNullString,  , GetLocalResourceObject("tcnAge_iniColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_endColumnCaption"), "tcnAge_end", 4, vbNullString,  , GetLocalResourceObject("tcnAge_endColumnToolTip"))
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnQprempayedColumnCaption"), "tcnQprempayed", 5, vbNullString,  , GetLocalResourceObject("tcnQprempayedColumnToolTip"), True, 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnQprempayedColumnCaption"), "tcnQprempayed", 5, vbNullString,  , GetLocalResourceObject("tcnQprempayedColumnToolTip"), True, 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		End If
		Call .AddCheckColumn(0, GetLocalResourceObject("chkExtrapremColumnCaption"), "chkExtraprem", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkExtrapremColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDisc_percentageColumnCaption"), "tcnDisc_percentage", 5, vbNullString,  , GetLocalResourceObject("tcnDisc_percentageColumnToolTip"), True, 2)
	End With
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "MVI8001"
		.Codisp = "MVI8001"
		.sCodisplPage = "MVI8001"
		.ActionQuery = mobjValues.ActionQuery
		If Request.QueryString.Item("Action") <> "Del" Then
			.Top = 250
			.Height = 300
			.Width = 300
		End If
		.Columns("tcnAge_ini").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nRole=" & Request.QueryString.Item("nRole")
		
		.sDelRecordParam = .sEditRecordParam & "&tcnAge_ini=' + marrArray[lintIndex].tcnAge_ini + '&tcnAge_end=' + marrArray[lintIndex].tcnAge_end + ' &tcnQprempayed=' + marrArray[lintIndex].tcnQprempayed + ' &tcnDisc_percentage=' + marrArray[lintIndex].tcnDisc_percentage + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI8001: Se realiza el manejo del grid.
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8001()
	'--------------------------------------------------------------------------------------------
	If mcolDisc_percentages.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each mclsDisc_percentage In mcolDisc_percentages
			With mobjGrid
				.Columns("tcnAge_ini").DefValue = CStr(mclsDisc_percentage.nAge_ini)
				.Columns("tcnAge_end").DefValue = CStr(mclsDisc_percentage.nAge_end)
				.Columns("tcnQprempayed").DefValue = CStr(mclsDisc_percentage.nQprempayed)
				.Columns("tcnDisc_percentage").DefValue = CStr(mclsDisc_percentage.nDisc_percentage)
				
				.Columns("chkExtraprem").Checked = CShort(mclsDisc_percentage.sExtraprem)
				
				Response.Write(.DoRow)
			End With
		Next mclsDisc_percentage
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVI8001Upd: Se realiza el manejo de la ventana PopUp asociada al grid.
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8001Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			Call mclsDisc_percentage.insPostMVI8001(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("tcnAge_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnQprempayed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("tcnDisc_percentage"), eFunctions.Values.eTypeData.etdDouble, True))
			
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantNoTraLife.aspx", "MVI8001", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsDisc_percentage = New eBranches.Disc_percentage
mcolDisc_percentages = New eBranches.Disc_percentages

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI8001"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:10 $|$$Author: Nvaplat61 $"
</SCRIPT>

<%
Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI8001", "MVI8001.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI8001" ACTION="valMantNoTraLife.aspx?sMode=2">

<%Response.Write(mobjValues.ShowWindowsName("MVI8001"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI8001Upd()
Else
	Call insPreMVI8001()
End If

mobjGrid = Nothing
mobjMenu = Nothing
mobjValues = Nothing
mclsDisc_percentage = Nothing
mcolDisc_percentages = Nothing
%>
</FORM> 
</BODY>
</HTML>





