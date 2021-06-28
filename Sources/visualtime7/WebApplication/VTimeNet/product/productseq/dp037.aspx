<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las características del grid
'------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------------------------------------
	With mobjGrid.Columns
		Call .AddNumericColumn(41349, GetLocalResourceObject("tcnMonthMaxColumnCaption"), "tcnMonthMax", 2,  ,  , GetLocalResourceObject("tcnMonthMaxColumnToolTip"), False,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(41350, GetLocalResourceObject("tcnDaysMaxColumnCaption"), "tcnDaysMax", 2,  ,  , GetLocalResourceObject("tcnDaysMaxColumnToolTip"), False,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(41351, GetLocalResourceObject("tcnRatePremiumColumnCaption"), "tcnRatePremium", 5,  ,  , GetLocalResourceObject("tcnRatePremiumColumnToolTip"), False, 2)
		Call .AddNumericColumn(41352, GetLocalResourceObject("tcnRateDevolutionColumnCaption"), "tcnRateDevolution", 5,  ,  , GetLocalResourceObject("tcnRateDevolutionColumnToolTip"), False, 2)
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = "DP037"
		.Codisp = "DP037"
		.Height = 220
		.Width = 300
		.Columns("tcnMonthMax").EditRecord = True
		.DeleteButton = True
		.AddButton = True
		.Columns("Sel").OnClick = "MarkRecord(this);"
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP037: se cargan los datos de la tabla
'------------------------------------------------------------------------------------------------------------
Private Sub insPreDP037()
	'------------------------------------------------------------------------------------------------------------
	Dim lclsTab_Short As eProduct.Tab_short
	Dim lobjErrors As eFunctions.Errors
	Dim lintCount As Integer
	Dim lblnExist As Boolean
	
	lclsTab_Short = New eProduct.Tab_short
	
	lblnExist = False
	
	If lclsTab_Short.FindDP037(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
		For lintCount = 0 To lclsTab_Short.CountItemDP037 - 1
			If lclsTab_Short.ItemDP037(lintCount) Then
				With mobjGrid
					.Columns("tcnMonthMax").DefValue = CStr(lclsTab_Short.nMonthmax)
					.Columns("tcnDaysMax").DefValue = CStr(lclsTab_Short.nDaysmax)
					.Columns("tcnRatePremium").DefValue = CStr(lclsTab_Short.nRateprem)
					.Columns("tcnRateDevolution").DefValue = CStr(lclsTab_Short.nRatedevo)
					.Columns("sParam").DefValue = "nMonthMax=" & lclsTab_Short.nMonthmax & "&nDaysMax=" & lclsTab_Short.nDaysmax & "&nRateDevolution=" & lclsTab_Short.nRatedevo & "&nRatePremium=" & lclsTab_Short.nRateprem
				End With
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
		lblnExist = True
		Response.Write(mobjGrid.CloseTable())
		Response.Write(mobjValues.BeginPageButton)
	Else
		If Not lclsTab_Short.FindTab_short_g(Session("dEffecdate")) Then
			lobjErrors = New eFunctions.Errors
			Response.Write(lobjErrors.ErrorMessage("DP037", 11391,  ,  ,  , True))
		End If
	End If
	
	If Not lblnExist And Not Session("bQuery") Then
		With Response
			.Write(mobjGrid.CloseTable())
			.Write("<P ALIGN=RIGHT>")
			.Write(mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "InitialValues()"))
		End With
	End If
	
	lclsTab_Short = Nothing
	lobjErrors = Nothing
End Sub

'% insPreDP037Upd: se manejan los campos puntuales del grid
'----------------------------------------------------------------------------------------------
Private Sub insPreDP037Upd()
	'----------------------------------------------------------------------------------------------
	Dim lclsTab_Short As eProduct.Tab_short
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsTab_Short = New eProduct.Tab_short
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			If lclsTab_Short.insPostDP037("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMonthMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDaysMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nRateDevolution"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRatePremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End With
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP037", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTab_Short = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid

mobjGrid.sCodisplPage = "DP037"
mobjValues.sCodisplPage = "DP037"

mobjGrid.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>


    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"
	
//% InitialValues: se inicializa el grid de la transacción, con l a información de 
//%				   la tabla general
//--------------------------------------------------------------------------------------------
function InitialValues(){
//--------------------------------------------------------------------------------------------
	insDefValues("Tab_short")
}
</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP037"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP037", "DP037.aspx"))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP037" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP037"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP037()
Else
	Call insPreDP037Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	    
</FORM>
</BODY>
</HTML>





