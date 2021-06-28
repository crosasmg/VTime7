<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mstrTypeFind As String
Dim mblnVisible As Boolean
Dim mblnDisabled As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRelationColumnCaption"), "cbeRelation", "Table55", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 5, GetLocalResourceObject("cbeRelationColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 3, CStr(0),  , GetLocalResourceObject("tcnAgeColumnToolTip"))
		
		'----------------------------------------------------------------------------
		Call .AddHiddenColumn("tcdEffecdate_reg", CStr(0))
		Call .AddHiddenColumn("sAuxSel", CStr(2))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP060"
		.Width = 400
		.Height = 190
		.DeleteButton = True
		.AddButton = True
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		ElseIf mstrTypeFind = "1" Then 
			.Columns("cbeRelation").EditRecord = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.sDelRecordParam = "dEffecdate=" + mobjValues.TypeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nRelation=' + marrArray[lintIndex].cbeRelation + '&nAge=' + marrArray[lintIndex].tcnAge + '" & "&dEffecdate_reg='+ marrArray[lintIndex].tcdEffecdate_reg + '"
	End With
End Sub

'% insPreDP060Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP060()
	'--------------------------------------------------------------------------------------------
	Dim lclsLimits_age As eBranches.Limits_age
	Dim lcolLimits_ages As eBranches.Limits_ages
	Dim lintIndex As Object
	
	lclsLimits_age = New eBranches.Limits_age
	lcolLimits_ages = New eBranches.Limits_ages
	
	If lcolLimits_ages.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsLimits_age In lcolLimits_ages
			With mobjGrid
				.Columns("cbeRelation").DefValue = CStr(lclsLimits_age.nRelation)
				.Columns("tcnAge").DefValue = CStr(lclsLimits_age.nAge)
				.Columns("tcdEffecdate_reg").DefValue = CStr(lclsLimits_age.dEffecdate_Reg)
			End With
			Response.Write(mobjGrid.DoRow)
		Next lclsLimits_age
	Else
		mblnVisible = True
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lclsLimits_age = Nothing
	lcolLimits_ages = Nothing
	
End Sub

'% insPreDP060Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP060Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsLimits_age As eBranches.Limits_age
	lclsLimits_age = New eBranches.Limits_age
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		Call lclsLimits_age.insPostDP060("DP060", Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRelation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Request.QueryString.Item("dEffecdate_reg"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	
	lclsLimits_age = Nothing
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP060", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjGrid.sCodisplPage = "DP060"
mobjValues.sCodisplPage = "DP060"

If IsNothing(Request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If

mobjValues.ActionQuery = Session("bQuery")

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//- Esta línea guarda la version procedente de VSS
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"
</SCRIPT>




<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP060", "DP060.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP060" ACTION="valProductSeq.aspx?mode=2;">
<%
Response.Write(mobjValues.ShowWindowsName("DP060"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP060Upd()
Else
	Call insPreDP060()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




