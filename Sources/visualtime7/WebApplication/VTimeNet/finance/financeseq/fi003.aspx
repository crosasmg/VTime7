<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

Dim mstrTypeFind As String
Dim mblnVisible As Boolean


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
	
	mobjGrid.sCodisplPage = "fi003"
	
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnContrat_dColumnCaption"), "tcnContrat_d", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnContrat_dColumnCaption"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraft_dColumnCaption"), "tcnDraft_d", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnDraft_dColumnCaption"),  ,  ,  ,  , "ShowDefVal();")
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat",  ,  , GetLocalResourceObject("tcdExpirdatColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDraftValueColumnCaption"), "cbeDraftValue", "table252", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 30, "",  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 14, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnExchangeColumnCaption"), True, 6,  ,  ,  , True)
		Call .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", vbNullString,  , GetLocalResourceObject("tctClientColumnToolTip"),  , True, "tctCliename")
		Call .AddHiddenColumn("tcnCommission", CStr(0))
		Call .AddHiddenColumn("tcnCurrency", CStr(0))
		Call .AddHiddenColumn("sAuxSel", CStr(2))
		Call .AddHiddenColumn("nDraf", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "FI003"
		.Width = 400
		.Height = 400
		.DeleteButton = False
		.AddButton = False
		
		If Session("bQuery") Or CStr(Session("optType")) = "1" Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		ElseIf mstrTypeFind = "2" Then 
			.Columns("Sel").Title = "Sel"
		ElseIf mstrTypeFind = "1" Then 
			.Columns("Sel").Title = "Sel"
			.Columns("tcnDraft_d").EditRecord = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);"
	End With
End Sub

'% insPreFI003: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreFI003()
	'--------------------------------------------------------------------------------------------
	Dim lclsRefinanDraft As eFinance.RefinanceDraft
	Dim lcolRefinanDraft As eFinance.RefinanceDrafts
	Dim nTotAmount As Object
	Dim nTotCommission As Object
	
	
	lcolRefinanDraft = New eFinance.RefinanceDrafts
	
	If lcolRefinanDraft.Find(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		lclsRefinanDraft = New eFinance.RefinanceDraft
		mobjGrid.DeleteButton = True
		
		For	Each lclsRefinanDraft In lcolRefinanDraft
			With mobjGrid
				nTotAmount = lclsRefinanDraft.nTotAmount
				nTotCommission = lclsRefinanDraft.nTotCommission
				.Columns("tcnContrat_d").DefValue = CStr(lclsRefinanDraft.nContrat_d)
				.Columns("tcnDraft_d").DefValue = CStr(lclsRefinanDraft.nDraft_d)
				.Columns("tcdExpirdat").DefValue = CStr(lclsRefinanDraft.dExpirdat)
				.Columns("cbeDraftValue").DefValue = CStr(lclsRefinanDraft.nOpt_draft)
				.Columns("tcnPremium").DefValue = mobjValues.StringToType(CStr(lclsRefinanDraft.nPremium), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tctCurrency").DefValue = lclsRefinanDraft.sCurrency
				.Columns("tcnExchange").DefValue = CStr(lclsRefinanDraft.nExchange)
				.Columns("tcnCommission").DefValue = CStr(lclsRefinanDraft.nCommission)
				.Columns("tctClient").DefValue = lclsRefinanDraft.sClient
				.Columns("tctClient").Descript = lclsRefinanDraft.sCliename
				.Columns("tcnCurrency").DefValue = CStr(lclsRefinanDraft.nCurrency)
				
				.sDelRecordParam = "nDraft_d=' + marrArray[lintIndex].tcnDraft_d + '&nPremium=' + marrArray[lintIndex].tcnPremium + '&dExpirdat=' + marrArray[lintIndex].tcdExpirdat + '&nExchange=' + marrArray[lintIndex].tcnExchange + '&nCurrency=' + marrArray[lintIndex].tcnCurrency + '&nContrat_d=' + marrArray[lintIndex].tcnContrat_d  + '&nCommission=' + marrArray[lintIndex].tcnCommission + '"
				Response.Write(.DoRow)
			End With
		Next lclsRefinanDraft
		
Response.Write("" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD><LABEL>" & GetLocalResourceObject("lblTotRefCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")

		Response.Write(mobjValues.NumericControl("lblTotRef", 18, nTotAmount,  ,  , True, 6, True,  ,  ,  , True))
		Response.Write(mobjValues.HiddenControl("hddTotRef", nTotAmount))
		
Response.Write(" </TD>" & vbCrLf)
Response.Write("		        <TD><LABEL>" & GetLocalResourceObject("lblTotcomCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD>")

		Response.Write(mobjValues.NumericControl("lblTotcom", 18, nTotCommission,  ,  , True, 6, True,  ,  ,  , True))
		Response.Write(mobjValues.HiddenControl("hddTotcom", nTotCommission))
		
Response.Write(" </TD>" & vbCrLf)
Response.Write("		        <TD></TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("		</TABLE>")

		
	Else
		mblnVisible = True
	End If
	
	Response.Write(mobjGrid.closeTable())
	If Not mobjValues.ActionQuery Then
		
Response.Write("" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD WIDTH=""25%"">")


Response.Write(mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "InitialValues()"))


Response.Write("</TD> " & vbCrLf)
Response.Write("		        <TD COLSPAN=4>&nbsp;</TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("        </TABLE>    " & vbCrLf)
Response.Write("    ")

		
	End If
	lclsRefinanDraft = Nothing
	lcolRefinanDraft = Nothing
	
End Sub

'% insPreFI003Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreFI003Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsRefinanDraft As eFinance.RefinanceDraft
	Dim lblnPost As Boolean
	
	'+ Se elimina el registro marcado
	If Request.QueryString.Item("Action") = "Del" Then
		lclsRefinanDraft = New eFinance.RefinanceDraft
		Response.Write(mobjValues.ConfirmDelete)
		
		Call lclsRefinanDraft.inspostFI003(mobjValues.StringToType(CStr(1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(2), eFunctions.Values.eTypeData.etdDouble), vbNullString, mobjValues.StringToType(Request.QueryString.Item("nDraft_d"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nExchange"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("tcncurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nContrat_d"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCommission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		
		If lblnPost Then
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Finance/Financeseq/Sequence.aspx?nAction=0" & """;</" & "Script>")
		End If
		lclsRefinanDraft = Nothing
	End If
	'+ Se muestra la ventana PopUp para modificar o actualizar	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valFinanceseq.aspx", "FI003", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	
	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write("<SCRIPT>DisabledItems();</" & "Script>")
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

If IsNothing(Request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If

mobjValues.ActionQuery = Session("bQuery")
mobjValues.sCodisplPage = "fi003"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JAVASCRIPT">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 9/09/04 19:23 $|$$Author: Nvaplat40 $"

//% InitialValues: se inicializa el grid de la transacción, 
//% con los datos definidos en el diseñador
//--------------------------------------------------------------------------------------------
function InitialValues(Field){
//--------------------------------------------------------------------------------------------
	var lstrQuery

	with (document.forms[0]) {
		lstrQuery = ""
    	insDefValues("DraftFinanc_pre", lstrQuery)
	}
}

    
//%ShowDefVal :
//----------------------------------------------------------------------------------------------------------------    
function ShowDefVal(){
//----------------------------------------------------------------------------------------------------------------
    insDefValues("FI003Upd","ncontrat_d=" + self.document.forms[0].tcnContrat_d.value + "&ndraft_d=" + self.document.forms[0].tcnDraft_d.value)
}

//%DisabledItems:
//----------------------------------------------------------------------------------------------------------------
function DisabledItems()
//----------------------------------------------------------------------------------------------------------------
{
	with (self.document.forms[0]){
		tcnContrat_d.disabled = true;
		tcnDraft_d.disabled = 	true;
	}
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		mobjNetFrameWork.sSessionID = Session.SessionID
		mobjNetFrameWork.nUsercode = Session("nUsercode")
		Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "FI003", "FI003.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmFI003" ACTION="valFinanceSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%With Response
	.Write(mobjValues.ShowWindowsName("FI003"))
	.Write("<BR>")
End With

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreFI003Upd()
Else
	Call insPreFI003()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





