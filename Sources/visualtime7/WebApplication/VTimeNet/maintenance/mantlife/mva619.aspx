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


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"),  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnWarIntColumnCaption"), "tcnWarInt", 13, vbNullString,  , GetLocalResourceObject("tcnWarIntColumnToolTip"), True, 10)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVA619"
		.sCodisplPage = "MVA619"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 200
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
		.DeleteButton = False
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nTypeInvest=" & Request.QueryString.Item("nTypeInvest")
		
		.sDelRecordParam = .sEditRecordParam & "&dEffecdate=' + marrArray[lintIndex].tcdEffecdate + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVA619: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA619()
	'--------------------------------------------------------------------------------------------
	Dim lcolTab_Interest As eBranches.Tab_Interests
	Dim lclsTab_Interest As Object
	Dim llngCount As Integer
	Dim llngIndex As Integer
	
	lcolTab_Interest = New eBranches.Tab_Interests
	
	If lcolTab_Interest.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTypeInvest"), eFunctions.Values.eTypeData.etdDouble)) Then
		llngIndex = 0
		llngCount = lcolTab_Interest.Count
		With mobjGrid
			For	Each lclsTab_Interest In lcolTab_Interest
				llngIndex = llngIndex + 1
				.Columns("tcnWarInt").DefValue = lclsTab_Interest.nWarint
				.Columns("tcdEffecdate").DefValue = lclsTab_Interest.dEffecdate
				If llngIndex = llngCount Then
					.Columns("tcdEffecdate").EditRecord = True
				End If
				Response.Write(.DoRow)
			Next lclsTab_Interest
		End With
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVA619Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA619Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_Interest As eBranches.Tab_Interest
	
	lclsTab_Interest = New eBranches.Tab_Interest
	With mobjValues
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTab_Interest.InsPostMVA619("Del", .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nTypeInvest"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, Session("nUsercode"))
		End If
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantLife.aspx", "MVA619", Request.QueryString.Item("nMainAction"), .ActionQuery, CShort(Request.QueryString.Item("Index"))))
	End With
	lclsTab_Interest = Nothing
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA619"
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
	Response.Write(mobjMenu.setZone(2, "MVA619", "MVA619.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="Nombre_de_la_página" ACTION="valMantLife.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("MVA619"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVA619Upd()
Else
	Call insPreMVA619()
End If
mobjValues = Nothing
mobjMenu = Nothing
%>
</FORM> 
</BODY>
</HTML>




