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
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 5, vbNullString,  , GetLocalResourceObject("tcnAgeColumnCaption"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateWomenColumnCaption"), "tcnRateWomen", 9, vbNullString,  , GetLocalResourceObject("tcnRateWomenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremWomenColumnCaption"), "tcnPremWomen", 18, vbNullString,  , GetLocalResourceObject("tcnPremWomenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateMenColumnCaption"), "tcnRateMen", 9, vbNullString,  , GetLocalResourceObject("tcnRateMenColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremMenColumnCaption"), "tcnPremMen", 18, vbNullString,  , GetLocalResourceObject("tcnPremMenColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MVA606"
		.sCodisplPage = "MVA606"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnAge").EditRecord = True
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nAge=' + marrArray[lintIndex].tcnAge + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVA606: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA606()
	'--------------------------------------------------------------------------------------------
	Dim lcolTar_Actlifes As eBranches.Tar_ActLifes
	Dim lclsTar_Actlife As eBranches.Tar_ActLife
	
	lclsTar_Actlife = New eBranches.Tar_ActLife
	lcolTar_Actlifes = New eBranches.Tar_ActLifes
	
	If lcolTar_Actlifes.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeTab"), Session("sSmoking"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsTar_Actlife In lcolTar_Actlifes
			With mobjGrid
				.Columns("tcnAge").DefValue = CStr(lclsTar_Actlife.nAge)
				.Columns("tcnRateWomen").DefValue = CStr(lclsTar_Actlife.nRatewomen)
				.Columns("tcnPremWomen").DefValue = CStr(lclsTar_Actlife.nPremwomen)
				.Columns("tcnRateMen").DefValue = CStr(lclsTar_Actlife.nRatemen)
				.Columns("tcnPremMen").DefValue = CStr(lclsTar_Actlife.nPremmen)
				Response.Write(.DoRow)
			End With
		Next lclsTar_Actlife
	End If
	
	lcolTar_Actlifes = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVA606Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA606Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjTar_Actlife As eBranches.Tar_ActLife
	
	lobjTar_Actlife = New eBranches.Tar_ActLife
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		With mobjValues
			If lobjTar_Actlife.insPostMVA606("Del", .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeTab"), Session("sSmoking"), .StringToType(Request.QueryString.Item("nAge"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),  ,  ,  ,  , .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				
			End If
		End With
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVA606", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	lobjTar_Actlife = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA606"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $"
</SCRIPT>    

<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVA606", "MVA606.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVA606" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVA606"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVA606Upd()
Else
	Call insPreMVA606()
End If
%>
</FORM> 
</BODY>
</HTML>





