<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lobjColumn As eFunctions.Column
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid
		lobjColumn = .Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeIndrecdepColumnCaption"), "cbeIndrecdep", "table5600", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeIndrecdepColumnToolTip"))
		lobjColumn.TypeList = CShort("2")
		lobjColumn.List = "1,2,9"
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPrem_quotColumnCaption"), "tcnPrem_quot", 18,  ,  , GetLocalResourceObject("tcnPrem_quotColumnToolTip"), True, 6)
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnRate_discColumnCaption"), "tcnRate_disc", 4,  ,  , GetLocalResourceObject("tcnRate_discColumnCaption"),  , 2)
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnNom_valbonColumnCaption"), "tcnNom_valbon", 18,  ,  , GetLocalResourceObject("tcnNom_valbonColumnCaption"), True, 6)
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdIssuedatbonColumnCaption"), "tcdIssuedatbon",  ,  , GetLocalResourceObject("tcdIssuedatbonColumnCaption"))
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdExpirdatbonColumnCaption"), "tcdExpirdatbon",  ,  , GetLocalResourceObject("tcdExpirdatbonColumnToolTip"))
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		.Columns.AddHiddenColumn("hddnReceipt", vbNullString)
		.Columns.AddHiddenColumn("hddnId", vbNullString)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "RV778"
		.ActionQuery = Session("bQuery")
		.Height = 380
		.Width = 500
		.sDelRecordParam = "nId='+ marrArray[lintIndex].hddnId + '"
		.sEditRecordParam = "nPremiumbas=' + document.forms[0].tcnPremiumbas.value + '"
		.Columns("cbeIndrecdep").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreRV778: Esta función se encarga de cargar los datos en la forma "Folder" 
'------------------------------------------------------------------------------
Private Sub insPreRV778()
	'------------------------------------------------------------------------------
	Dim lcolPrem_annuitiess As ePolicy.Prem_annuitiess
	Dim lclsPrem_annuities As Object
	Dim lblnFound As Boolean
	Dim lintPremiumbas As String
	
	lcolPrem_annuitiess = New ePolicy.Prem_annuitiess
	lblnFound = lcolPrem_annuitiess.insPreRV778(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
	If Request.QueryString.Item("nPremiumbas") = vbNullString Then
		lintPremiumbas = lcolPrem_annuitiess.nPremiumbas
	Else
		lintPremiumbas = Request.QueryString.Item("nPremiumbas")
	End If
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremiumbasCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremiumbas", 18, lintPremiumbas,  , GetLocalResourceObject("tcnPremiumbasToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	If lblnFound Then
		mobjGrid.sEditRecordParam = mobjGrid.sEditRecordParam & "&nCount=" & lcolPrem_annuitiess.Count
		For	Each lclsPrem_annuities In lcolPrem_annuitiess
			With mobjGrid
				.Columns("cbeIndrecdep").DefValue = lclsPrem_annuities.nIndrecdep
				.Columns("tcnPrem_quot").DefValue = lclsPrem_annuities.nPrem_quot
				.Columns("tcnRate_disc").DefValue = lclsPrem_annuities.nRate_disc
				.Columns("tcnNom_valbon").DefValue = lclsPrem_annuities.nNom_valbon
				.Columns("tcdIssuedatbon").DefValue = lclsPrem_annuities.dIssuedatbon
				.Columns("tcdExpirdatbon").DefValue = lclsPrem_annuities.dExpirdatbon
				.Columns("cbeCurrency").DefValue = lclsPrem_annuities.nCurrency
				.Columns("hddnReceipt").DefValue = lclsPrem_annuities.nReceipt
				.Columns("hddnId").DefValue = lclsPrem_annuities.nId
			End With
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
		Next lclsPrem_annuities
	End If
	Response.Write(mobjGrid.closeTable())
	lcolPrem_annuitiess = Nothing
	lclsPrem_annuities = Nothing
End Sub

'% insPreRV778Upd: Se define esta función para contruir el contenido de la ventana "UPD"
'------------------------------------------------------------------------------
Private Sub insPreRV778Upd()
	'------------------------------------------------------------------------------
	Dim lclsPrem_annuities As ePolicy.Prem_annuities
	Dim lstrContent As String
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsPrem_annuities = New ePolicy.Prem_annuities
			Response.Write(mobjValues.ConfirmDelete())
			lclsPrem_annuities.InsPostRV778Upd(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
			lstrContent = lclsPrem_annuities.sContent
			lclsPrem_annuities = Nothing
			mobjGrid.UpdContent = True
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("MainAction"),  , CShort(.QueryString.Item("Index")), lstrContent))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("RV778")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:49 $"

</SCRIPT>    
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write("<SCRIPT>var nMainAction='" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
	mobjMenues.sSessionID = Session.SessionID
	mobjMenues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	Response.Write(mobjMenues.setZone(2, "RV778", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenues = Nothing
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="RV778" ACTION="valPolicySeq.aspx?mode=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreRV778()
Else
	Call insPreRV778Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
Call mobjNetFrameWork.FinishPage("RV778")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




