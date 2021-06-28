<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "cr766"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_reinsuColumnCaption"), "tcnAge_reinsu", 5, vbNullString,  , GetLocalResourceObject("tcnAge_reinsuColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, vbNullString,  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, vbNullString,  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CR766"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 200
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("tcnAge_reinsu").EditRecord = True
		If .nMainAction = 401 Or .nMainAction = 306 Then
			.Columns("Sel").GridVisible = False
			.AddButton = False
			.DeleteButton = False
			.Columns("tcnAge_reinsu").Disabled = True
			.Columns("tcnAge_reinsu").EditRecord = False
		End If
		
		.Columns("tcnAge_reinsu").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sEditRecordParam = "nNumber=" & Session("tcnNumber_new") & "&nBranch_rei=" & Session("cbenBranch_rei_new") & "&nType=" & Session("cbeType_new") & "&nCovergen=" & Session("valCovergen_new") & "&nDeductible=" & Session("tcnDeductible_new") & "&nQfamily=" & Session("tcnQfamily_new") & "&nCapital=" & Session("tcnCapital_new") & "&dEffecdate=" & Session("tcdEffecdate_new")
		
		.sDelRecordParam = "nNumber=" & Session("tcnNumber_new") & "&nBranch_rei=" & Session("cbenBranch_rei_new") & "&nType=" & Session("cbeType_new") & "&nCovergen=" & Session("valCovergen_new") & "&nDeductible=" & Session("tcnDeductible_new") & "&nQfamily=" & Session("tcnQfamily_new") & "&nCapital=" & Session("tcnCapital_new") & "&dEffecdate=" & Session("tcdEffecdate_new") & "&tcnAge_reinsu=' + marrArray[lintIndex].tcnAge_reinsu + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreCr766: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCr766()
	'--------------------------------------------------------------------------------------------
	Dim lclsContr_rate_III As eCoReinsuran.contr_rate_III
	Dim lcolContr_rate_IIIs As eCoReinsuran.contr_rate_IIIs
	
	lclsContr_rate_III = New eCoReinsuran.contr_rate_III
	lcolContr_rate_IIIs = New eCoReinsuran.contr_rate_IIIs
	If lcolContr_rate_IIIs.Find(mobjValues.StringToType(Session("tcnNumber_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("cbenBranch_rei_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("cbeType_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("valCovergen_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnDeductible_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnQfamily_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnCapital_new"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcdEffecdate_new"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsContr_rate_III In lcolContr_rate_IIIs
			With mobjGrid
				.Columns("tcnAge_reinsu").DefValue = CStr(lclsContr_rate_III.nAge_reinsu)
				.Columns("tcnRate").DefValue = CStr(lclsContr_rate_III.nRate)
				.Columns("tcnPremium").DefValue = CStr(lclsContr_rate_III.nPremium)
				Response.Write(.DoRow)
			End With
		Next lclsContr_rate_III
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCr766Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCr766Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjCoReinsuranTra As eCoReinsuran.contr_rate_III
	
	lobjCoReinsuranTra = New eCoReinsuran.contr_rate_III
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjCoReinsuranTra.insPostCr766(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDeductible"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nQfamily"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("tcnAge_reinsu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValCoReinsuranTra.aspx", "CR766", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "cr766"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CR766", "CR766.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16.59 $" 
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CR766" ACTION="valCoReinsuranTra.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("CR766"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCr766Upd()
Else
	Call insPreCr766()
End If
%>
</FORM> 
</BODY>
</HTML>





