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
Dim mclsGua_saving_rent As eBranches.Guar_saving_rent


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddBranchColumn(0, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", GetLocalResourceObject("tcnBranchColumnToolTip"))
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"), "tcnBranch")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGuarSav_ValIniColumnCaption"), "tcnGuarSav_ValIni", 18, "",  , GetLocalResourceObject("tcnGuarSav_ValIniColumnToolTip"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGuarSav_ValEndColumnCaption"), "tcnGuarSav_ValEnd", 18, "",  , GetLocalResourceObject("tcnGuarSav_ValEndColumnToolTip"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGuarSav_YearColumnCaption"), "tcnGuarSav_Year", 5, "",  , GetLocalResourceObject("tcnGuarSav_YearColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGuarSavColumnCaption"), "tcnGuarSav", 9, "",  , GetLocalResourceObject("tcnGuarSavColumnToolTip"),  , 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI8000"
		.sCodisplPage = "MVI8000"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 280
		.Width = 500
		.WidthDelete = 400
		.Top = 100
		.Columns("tcnGuarSav_ValIni").EditRecord = True
		.Columns("tcnGuarSav_ValEnd").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		
		Call .Splits_Renamed.AddSplit(0, vbNullString, 2)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
		
		
		.sDelRecordParam = "nValue_ini=' + marrArray[lintIndex].tcnGuarSav_ValIni + '" & "&nValue_end='+ marrArray[lintIndex].tcnGuarSav_ValEnd + '" & "&nBranch='+ marrArray[lintIndex].tcnBranch + '" & "&nProduct='+ marrArray[lintIndex].valProduct + '" & "&nYear='+ marrArray[lintIndex].tcnGuarSav_Year + '"
		
		
		
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("Sel").GridVisible = True
			
			.Columns("tcnBranch").Disabled = True
			.Columns("valProduct").Disabled = True
			.Columns("tcnGuarSav_ValIni").Disabled = True
			.Columns("tcnGuarSav_ValEnd").Disabled = True
			.Columns("tcnGuarSav_Year").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI771: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8000()
	'--------------------------------------------------------------------------------------------
	Dim mobjObject As eBranches.Guar_saving_rents
	
	mobjObject = New eBranches.Guar_saving_rents
	
	
	If mobjObject.Find(mobjValues.StringToType(Session("dEffecdate_MVI8000"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each mclsGua_saving_rent In mobjObject
			
			With mobjGrid
				
				.Columns("tcnBranch").DefValue = CStr(mclsGua_saving_rent.nBranch)
				.Columns("valProduct").DefValue = CStr(mclsGua_saving_rent.nProduct)
				.Columns("tcnGuarSav_ValIni").DefValue = CStr(mclsGua_saving_rent.nValue_ini)
				.Columns("tcnGuarSav_ValEnd").DefValue = CStr(mclsGua_saving_rent.nValue_end)
				.Columns("tcnGuarSav_Year").DefValue = CStr(mclsGua_saving_rent.nGuarant_year)
				.Columns("tcnGuarSav").DefValue = CStr(mclsGua_saving_rent.nGuarant_rent)
				
				Response.Write(.DoRow)
			End With
		Next mclsGua_saving_rent
	End If
	Response.Write(mobjGrid.closeTable())
	
	mobjObject = Nothing
End Sub
'% insPreMVI771Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8000Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			Call mclsGua_saving_rent.insPostMVI8000(3, mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nValue_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nValue_end"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate_MVI8000"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
			
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI8000", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsGua_saving_rent = New eBranches.Guar_saving_rent
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI8000"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI8000", "MVI8000.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI8000" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI8000"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI8000Upd()
Else
	Call insPreMVI8000()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>





