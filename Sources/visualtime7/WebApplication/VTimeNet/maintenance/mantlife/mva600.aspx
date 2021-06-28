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
Dim mcolDisc_pb As eBranches.Disc_pbs


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddPossiblesColumn(0, GetLocalResourceObject("valAgreementColumnCaption"), "valAgreement", "tabAgreement_al", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valAgreementColumnToolTip"))
		mobjGrid.Columns("valAgreement").Parameters.Add("sStatRegt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.AddNumericColumn(0, GetLocalResourceObject("tcnQPBColumnCaption"), "tcnQPB", 5, vbNullString,  , GetLocalResourceObject("tcnQPBColumnCaption"), True, 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, vbNullString,  , GetLocalResourceObject("tcnPercentColumnToolTip"),  , 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVA600"
		.sCodisplPage = "MVA600"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("valAgreement").EditRecord = True
		.Columns("tcnQPB").EditRecord = True
		.Height = 230
		.Width = 320
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sEditRecordParam = "nIntertyp=" & Request.QueryString.Item("nIntertyp") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = .sEditRecordParam & "&nAgreement=' + marrArray[lintIndex].valAgreement + '" & "&nQPB=' + marrArray[lintIndex].tcnQPB + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% InsPreMVA600: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreMVA600()
	'--------------------------------------------------------------------------------------------
	Dim lclsDisc_pb As Object
	Dim llngModulec As Integer
	
	mcolDisc_pb = New eBranches.Disc_pbs
	llngModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)
	If llngModulec = eRemoteDB.Constants.intNull Then
		llngModulec = 0
	End If
	If mcolDisc_pb.Find(mobjValues.StringToType(Request.QueryString.Item("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), llngModulec, mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsDisc_pb In mcolDisc_pb
			With mobjGrid
				.Columns("valAgreement").DefValue = lclsDisc_pb.nAgreement
				.Columns("tcnQPB").DefValue = lclsDisc_pb.nQPB
				.Columns("tcnPercent").DefValue = lclsDisc_pb.nPercent
				Response.Write(.DoRow)
			End With
		Next lclsDisc_pb
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% InsPreMVA600Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreMVA600Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjDisc_pb As eBranches.Disc_pb
	
	lobjDisc_pb = New eBranches.Disc_pb
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lobjDisc_pb.insPostMVA600(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nQPB"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVA600", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA600"
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
	Response.Write(mobjMenu.setZone(2, "MVA600", "MVA600.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVA600.aspx" ACTION="valMantLife.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("MVA600"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call InsPreMVA600Upd()
Else
	Call InsPreMVA600()
End If
%>
</FORM> 
</BODY>
</HTML>




