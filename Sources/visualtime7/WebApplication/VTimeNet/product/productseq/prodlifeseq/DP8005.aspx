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
	Dim lblnDisabled As Boolean
	
	lblnDisabled = Request.QueryString.Item("Action") = "Update"
	
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("nPolicy_year_iniColumnCaption"), "nPolicy_year_ini", 5,  ,  , GetLocalResourceObject("nPolicy_year_iniColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("nPolicy_year_endColumnCaption"), "nPolicy_year_end", 5,  ,  , GetLocalResourceObject("nPolicy_year_endColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("nGuarsav_yearColumnCaption"), "nGuarsav_year", 5,  ,  , GetLocalResourceObject("nGuarsav_yearColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP8005"
		.Columns("nPolicy_year_ini").EditRecord = True
		.Columns("nPolicy_year_end").EditRecord = True
		
		.Height = 170
		.Width = 300
		
		.sDelRecordParam = "nYearIni=' + marrArray[lintIndex].nPolicy_year_ini + '" & "&nYearEnd=' + marrArray[lintIndex].nPolicy_year_end + '" & "&nGuarSav=' + marrArray[lintIndex].nGuarsav_year + '"
		
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		End If
		
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("nPolicy_year_ini").Disabled = True
			.Columns("nPolicy_year_end").Disabled = True
		End If
		
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
		
	End With
End Sub

'% insPreDP607B_K: Crea campos que van antes de tabla
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8005_K()
	'--------------------------------------------------------------------------------------------
	Dim lclsGuar_saving_prod As eBranches.Guar_saving_prod
	lclsGuar_saving_prod = New eBranches.Guar_saving_prod
	
	Call lclsGuar_saving_prod.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" border=""0"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("nGuarSavMaxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	Response.Write(mobjValues.NumericControl("nGuarSavMax", 5, CStr(lclsGuar_saving_prod.nGuarSav_Max),  , GetLocalResourceObject("nGuarSavMaxToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("nLower_minCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	Response.Write(mobjValues.NumericControl("nLower_min", 18, CStr(lclsGuar_saving_prod.nLower_min),  , GetLocalResourceObject("nLower_minToolTip"),  , 6))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("nValDate_IssueCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	Response.Write(mobjValues.PossiblesValues("nValDate_Issue", "TABLE8000", eFunctions.Values.eValuesType.clngComboType, CStr(lclsGuar_saving_prod.nValDate_Issue),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nValDate_IssueToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("nValDate_LastCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	Response.Write(mobjValues.PossiblesValues("nValDate_Last", "TABLE8000", eFunctions.Values.eValuesType.clngComboType, CStr(lclsGuar_saving_prod.nValDate_Last),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nValDate_LastToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("nQmin_premCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	Response.Write(mobjValues.NumericControl("nQmin_prem", 5, CStr(lclsGuar_saving_prod.nQmin_prem),  , GetLocalResourceObject("nQmin_premToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	Response.Write(mobjValues.CheckControl("sIndRenewal", "", lclsGuar_saving_prod.sIndRenewal, lclsGuar_saving_prod.sIndRenewal, "ChangeValue(this.value);"))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("sRouReserveCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	Response.Write(mobjValues.TextControl("sRouReserve", 12, lclsGuar_saving_prod.sRouReserve,  , GetLocalResourceObject("sRouReserveToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("sRouGuarSafeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	Response.Write(mobjValues.TextControl("sRouGuarSafe", 12, lclsGuar_saving_prod.sRouGuarSave,  , GetLocalResourceObject("sRouGuarSafeToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	")

	
	lclsGuar_saving_prod = Nothing
End Sub

'% insPreDP8005: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8005()
	'--------------------------------------------------------------------------------------------
	Dim lclsGuar_saving As eBranches.Guar_saving_allow
	Dim lcolGuar_saving As eBranches.Guar_saving_allows
	
	lclsGuar_saving = New eBranches.Guar_saving_allow
	lcolGuar_saving = New eBranches.Guar_saving_allows
	
	If lcolGuar_saving.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsGuar_saving In lcolGuar_saving
			With mobjGrid
				
				.Columns("nPolicy_year_ini").DefValue = CStr(lclsGuar_saving.nPolicy_Year_Ini)
				.Columns("nPolicy_year_end").DefValue = CStr(lclsGuar_saving.nPolicy_Year__end)
				.Columns("nGuarsav_year").DefValue = CStr(lclsGuar_saving.nGuarSav_Year)
				
				Response.Write(.DoRow)
			End With
		Next lclsGuar_saving
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lclsGuar_saving = Nothing
	lcolGuar_saving = Nothing
	
	
End Sub

'% insPreDP8005Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8005Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjGuar_saving_allow As eBranches.Guar_saving_allow
	
	lobjGuar_saving_allow = New eBranches.Guar_saving_allow
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			If lobjGuar_saving_allow.insPostDP8005(3, Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.QueryString.Item("nYearIni"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nYearEnd"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nGuarSav"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("nUsercode")) Then
				
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProdLifeSeq.aspx", "DP8005", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	lobjGuar_saving_allow = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "DP8005"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP8005", "DP8005.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 3/07/06 7:49p $|$$Author: Gazuaje $"

/*% ChangeValue: Recarga la página tras  cambiar valores en combos
/*---------------------------------------------------------------------------------------------------------*/
function ChangeValue(sValue){
/*---------------------------------------------------------------------------------------------------------*/
	
	with (self.document.forms[0]) {
		if (sValue==''||sValue=='2'){
			sIndRenewal.value = '1';
		}else{
			sIndRenewal.value = '2';
		}
		
	}
	
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="DP8005" ACTION="valProdLifeSeq.aspx?sMode=2">
<%

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName("DP8005"))
End If

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP8005Upd()
Else
	mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
	
	Call insPreDP8005_K()
	Call insPreDP8005()
End If
%>
</FORM> 
</BODY>
</HTML>





