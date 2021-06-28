<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
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
	Dim lblnDisabled As Boolean
	
	lblnDisabled = Request.QueryString.Item("Action") = "Update"
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "DP8006"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQSurrIniColumnCaption"), "tcnQSurrIni", 5, vbNullString,  , GetLocalResourceObject("tcnQSurrIniColumnToolTip"),  ,  ,  ,  ,  , lblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQSurrEndColumnCaption"), "tcnQSurrEnd", 5, vbNullString,  , GetLocalResourceObject("tcnQSurrEndColumnToolTip"),  ,  ,  ,  ,  , lblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, vbNullString,  , GetLocalResourceObject("tcnPercentColumnToolTip"),  , 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP8006"
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		.Columns("tcnQSurrIni").EditRecord = True
		.Height = 200
		.Width = 390
		.sDelRecordParam = "nQSurrIni=' + marrArray[lintIndex].tcnQSurrIni + '"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreDP8006: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8006()
	'--------------------------------------------------------------------------------------------
	Dim lclsSurr_Percent As eProduct.Surr_percent
	Dim lcolSurr_Percent As eProduct.Surr_percents
	
	lclsSurr_Percent = New eProduct.Surr_percent
	lcolSurr_Percent = New eProduct.Surr_percents
	
	If lcolSurr_Percent.Find(mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsSurr_Percent In lcolSurr_Percent
			With mobjGrid
				.Columns("tcnQSurrIni").DefValue = CStr(lclsSurr_Percent.nQSurrIni)
				.Columns("tcnQSurrEnd").DefValue = CStr(lclsSurr_Percent.nQSurrEnd)
				.Columns("tcnPercent").DefValue = CStr(lclsSurr_Percent.nPercent)
				Response.Write(.DoRow)
			End With
		Next lclsSurr_Percent
	End If
	
	lclsSurr_Percent = Nothing
	lcolSurr_Percent = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP8006Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8006Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjSurr_Percent As eProduct.Surr_percent
	
	lobjSurr_Percent = New eProduct.Surr_percent
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjSurr_Percent.InsPostDP8006("Del", mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nQSurrIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, mobjValues.StringToType(session("nUserCode"), eFunctions.Values.eTypeData.etdInteger)) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
				
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProdLifeSeq.aspx", "DP8006", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	lobjSurr_Percent = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "DP8006"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP8006", "DP8006.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 1-02-06 17:32 $|$$Author: Mvazquez $"

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="DP8006" ACTION="valProdLifeSeq.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("DP8006"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP8006Upd()
Else
	Call insPreDP8006()
End If
%>
</FORM> 
</BODY>
</HTML>





