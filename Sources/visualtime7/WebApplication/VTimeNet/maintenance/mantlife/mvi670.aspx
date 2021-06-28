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

'- Objeto para el manejo de la tabla que actualiza la transacción
Dim mcolLevel_param As eBranches.Level_params


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeLevelColumnCaption"), "cbeLevel", "Table5546", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeLevelColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 2, vbNullString,  , GetLocalResourceObject("tcnAgeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_FatherColumnCaption"), "tcnAge_Father", 2, vbNullString,  , GetLocalResourceObject("tcnAge_FatherColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTaxColumnCaption"), "tcnTax", 5, vbNullString,  , GetLocalResourceObject("tcnTaxColumnToolTip"), True, 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI670"
		.sCodisplPage = "MVI670"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 280
		.Width = 330
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bOnlyForQuery = mobjValues.ActionQuery
		.Columns("cbeLevel").EditRecord = True
		.Columns("Sel").GridVisible = Not .bOnlyForQuery
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&sTyplevel=' + marrArray[lintIndex].cbeTyplevel + '&nLevel=' + marrArray[lintIndex].cbeLevel + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI670: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI670()
	'--------------------------------------------------------------------------------------------
	Dim lclsLevel_param As Object
	If mcolLevel_param.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsLevel_param In mcolLevel_param
			With mobjGrid
				.Columns("cbeLevel").DefValue = lclsLevel_param.nLevel
				.Columns("tcnAge").DefValue = lclsLevel_param.nAge
				.Columns("tcnAge_Father").DefValue = lclsLevel_param.nAge_Father
				.Columns("tcnTax").DefValue = lclsLevel_param.nTax
				Response.Write(.DoRow)
			End With
		Next lclsLevel_param
	End If
	Response.Write(mobjGrid.closeTable())
	lclsLevel_param = Nothing
End Sub

'% insPreMVI670Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI670Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsLevel_param As eBranches.Level_param
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsLevel_param = New eBranches.Level_param
			If lclsLevel_param.insPostMVI670(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nLevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAge_Father"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI670", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		With Response
			.Write(mobjValues.HiddenControl("cbeBranch", Request.QueryString.Item("nBranch")))
			.Write(mobjValues.HiddenControl("valProduct", Request.QueryString.Item("nProduct")))
			.Write(mobjValues.HiddenControl("tcdEffecdate", Request.QueryString.Item("dEffecdate")))
		End With
	End With
	lclsLevel_param = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mcolLevel_param = New eBranches.Level_params

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI670"
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
	Response.Write(mobjMenu.setZone(2, "MVI670", "MVI670.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI670" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI670"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI670Upd()
Else
	Call insPreMVI670()
End If

mobjValues = Nothing
mobjMenu = Nothing
mcolLevel_param = Nothing
%>
</FORM> 
</BODY>
</HTML>





