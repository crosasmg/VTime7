<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mclsGeneral As eGeneral.GeneralFunction


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "FI011"
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnDraftColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", CStr(eRemoteDB.Constants.dtmNull),  , GetLocalResourceObject("tcdExpirdatColumnToolTip"),  ,  , "insChangeValues(""Expirdat"",""" & Session("dEffecdate") & """," & Request.QueryString.Item("Index") & ")")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18,  ,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  , "insChangeValues(""Expirdat"",""" & Session("dEffecdate") & """," & Request.QueryString.Item("Index") & ")")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_netColumnCaption"), "tcnAmount_net", 18,  ,  , GetLocalResourceObject("tcnAmount_netColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIntammouColumnCaption"), "tcnIntammou", 18, CStr(0),  , GetLocalResourceObject("tcnIntammouColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeWay_payColumnCaption"), "cbeWay_pay", "Table5002", eFunctions.Values.eValuesType.clngComboType, Session("nWay_Pay"),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeWay_payColumnToolTip"))
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStat_draftColumnCaption"), "cbeStat_draft", "Table253", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStat_draftColumnToolTip"))
		End If
		Call .AddHiddenColumn("tcnIndicator", CStr(eRemoteDB.Constants.intNull))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "FI011"
		.Codisp = "FI011"
		.sCodisplPage = "FI011"
		'		.Codispl = Request.QueryString("sCodispl")
		.Top = 150
		.Height = 320
		.Width = 330
		.Columns("tcdExpirdat").EditRecord = True
		.sDelRecordParam = "nDraft=' + marrArray[lintIndex].tcnDraft + '"
		.ActionQuery = Session("bQuery")
		.Columns("Sel").Disabled = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP011: se controlan los datos de la ventana
'--------------------------------------------------------------------------------------------
Private Sub insPreDP011()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinance_co As eFinance.financeCO
	Dim lcolFinanceDrafts As eFinance.FinanceDrafts
	Dim lintIndex As Integer
	Dim ldblInitial As Object
	Dim ldblInterest As Object
	Dim ldblAmount As Double
	Dim ldblRestAmount As Object
	
	lclsFinance_co = New eFinance.financeCO
	lcolFinanceDrafts = New eFinance.FinanceDrafts
	
	Call creInitialCuota()
	Call lclsFinance_co.Find(Session("nContrat"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HIGHLIGHTED""><LABEL><A NAME=""Opciones de funcionamiento"">" & GetLocalResourceObject("AnchorOpciones de funcionamientoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(0, "optCalc", GetLocalResourceObject("optCalc_CStr1Caption"), Request.QueryString.Item("sOption") & 1, CStr(1), "insChangeValues(""Calc"")",  ,  , GetLocalResourceObject("optCalc_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(0, "optCalc", GetLocalResourceObject("optCalc_CStr2Caption"), mobjValues.StringToType(Request.QueryString.Item("sOption"), eFunctions.Values.eTypeData.etdDouble) - 1, CStr(2), "insChangeValues(""Calc"")",  ,  , GetLocalResourceObject("optCalc_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	ldblAmount = 0
	ldblRestAmount = 0
	ldblInterest = 0
	If lcolFinanceDrafts.Find(Session("nContrat")) Then
		For lintIndex = 1 To lcolFinanceDrafts.Count
        ' For lintIndex = 0 To lcolFinanceDrafts.Count -1
			With mobjGrid
				.Columns("tcnDraft").DefValue = CStr(lcolFinanceDrafts.Item(lintIndex).nDraft)
				.Columns("tcdExpirdat").DefValue = CStr(lcolFinanceDrafts.Item(lintIndex).dExpirdat)
				.Columns("tcnAmount").DefValue = CStr(lcolFinanceDrafts.Item(lintIndex).nAmount)
				.Columns("tcnAmount_net").DefValue = CStr(lcolFinanceDrafts.Item(lintIndex).nAmount - lcolFinanceDrafts.Item(lintIndex).nInterest)
				.Columns("tcnIntammou").DefValue = CStr(lcolFinanceDrafts.Item(lintIndex).nIntammou)
				.Columns("cbeWay_pay").DefValue = CStr(lcolFinanceDrafts.Item(lintIndex).nWay_pay)
				.Columns("cbeWay_pay").Descript = lcolFinanceDrafts.Item(lintIndex).sDesWay_Pay
				If Request.QueryString.Item("Type") <> "PopUp" Then
					.Columns("cbeStat_draft").DefValue = CStr(lcolFinanceDrafts.Item(lintIndex).nStat_draft)
					.Columns("cbeStat_draft").Descript = lcolFinanceDrafts.Item(lintIndex).sStat_draft
				End If
				If Session("nTransaction") = 1 Then
					.Columns("tcnIndicator").DefValue = CStr(0)
				Else
					.Columns("tcnIndicator").DefValue = CStr(2)
				End If
				ldblInterest = ldblInterest + lcolFinanceDrafts.Item(lintIndex).nIntammou
				ldblAmount = ldblAmount + lcolFinanceDrafts.Item(lintIndex).nAmount
				If lintIndex = lcolFinanceDrafts.Count Then
					.Columns("Sel").Disabled = False
				End If
				Response.Write(.DoRow)
			End With
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
	
	ldblInitial = Request.QueryString.Item("nInitial")
	If Request.QueryString.Item("nInitial") = vbNullString Or Request.QueryString.Item("sOption") = vbNullString Then
		'ldblInitial = lclsFinance_co.nAmount - ldblAmount
		ldblInitial = lclsFinance_co.nInitial
	End If
	ldblRestAmount = lclsFinance_co.nAmount - ldblAmount '- ldblInitial
	If ldblRestAmount < 0 Then
		ldblRestAmount = 0
	End If
	
Response.Write("" & vbCrLf)
Response.Write("	</BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11007>" & GetLocalResourceObject("tcnInitialCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnInitial", 18, ldblInitial,  , GetLocalResourceObject("tcnInitialToolTip"), True, 6,  ,  ,  ,  , Request.QueryString.Item("sOption") = vbNullString))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11005>" & GetLocalResourceObject("tcnAmount_fiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnAmount_fi", 18, CStr(lclsFinance_co.nAmount),  , GetLocalResourceObject("tcnAmount_fiToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11006>" & GetLocalResourceObject("tcnAmount_intCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnAmount_int", 18, ldblInterest,  , GetLocalResourceObject("tcnAmount_intToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=11008>" & GetLocalResourceObject("tcnAMount_restCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnAMount_rest", 18, ldblRestAmount,  , GetLocalResourceObject("tcnAMount_restToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	With Response
		.Write(mobjValues.BeginPageButton)
		.Write(mobjValues.HiddenControl("hddMax_draft", CStr(lclsFinance_co.nQ_draft)))
		.Write(mobjValues.HiddenControl("hddInterest", CStr(lclsFinance_co.nInterest)))
		
		'+ Se deshabilita el botón de agregar si el número de cuotas ingresadas es igual al número
		'+ de cuotas permitidas para el contrato
		'+(En consulta no existe boton Agregar)
		If Not mobjValues.ActionQuery Then
			.Write("<SCRIPT>")
			.Write("var mlngMax_draft = " & lclsFinance_co.nQ_draft & ";")
			.Write("if(mlngMax_draft==marrArray.length)")
			.Write("self.document.cmdAdd.disabled=true;")
			.Write("</" & "Script>")
		End If
	End With
	
	lclsFinance_co = Nothing
	lcolFinanceDrafts = Nothing
End Sub

'% insPreFI011Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP011Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanc_dra As eFinance.FinanceDraft
	lclsFinanc_dra = New eFinance.FinanceDraft
	With Response
		If Request.QueryString.Item("Action") = "Del" Then
			If lclsFinanc_dra.insPostFI011(Request.QueryString.Item("Action"), Session("nContrat"), Session("dEffecdate"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CInt(Request.QueryString.Item("nDraft")), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Finance/Financeseq/Sequence.aspx?nAction=0" & """;</" & "Script>")
				Response.Write(mobjValues.ConfirmDelete)
			End If
		End If
		
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valFinanceSeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
		
		If Request.QueryString.Item("Action") <> "Del" Then
			.Write(mobjValues.HiddenControl("tcdPrevExpirdat", vbNullString))
			.Write(mobjValues.HiddenControl("hddLengthArray", CStr(0)))
			.Write(mobjValues.HiddenControl("hddIndex", CStr(0)))
			.Write(mobjValues.HiddenControl("tcnAuxInitial", CStr(0)))
			.Write(mobjValues.HiddenControl("tcnAuxAmount_fi", CStr(0)))
			.Write(mobjValues.HiddenControl("hddCalc", ""))
			
			.Write("<SCRIPT>")
			.Write("var lintIndex=" & Request.QueryString.Item("Index") & ";") 'top.opener.marrArray.length - 1;"
			.Write("self.document.forms[0].hddLengthArray.value=top.opener.marrArray.length;")
			.Write("self.document.forms[0].hddIndex.value=" & Request.QueryString.Item("Index") & ";")
			.Write("self.document.forms[0].tcnAuxInitial.value=top.opener.document.forms[0].tcnInitial.value;")
			.Write("self.document.forms[0].tcnAuxAmount_fi.value=top.opener.document.forms[0].tcnAmount_fi.value;")
			.Write("self.document.forms[0].hddCalc.value=(top.opener.document.forms[0].optCalc[0].checked)?'':2;")
			'+ Se toma la fecha de vencimiento de la linea anterior
			.Write("if(lintIndex>0)")
			.Write("    self.document.forms[0].tcdPrevExpirdat.value=top.opener.marrArray[lintIndex-1].tcdExpirdat;")
			If Request.QueryString.Item("Action") = "Add" Then
				.Write("insAddValues();")
			End If
			.Write("if(top.opener.document.forms[0].hddMax_draft.value==self.document.forms[0].tcnDraft.value && self.document.forms[0].hddCalc.value == 2){")
			.Write("self.document.forms[0].tcnAmount.disabled=true;")
			.Write("self.document.forms[0].tcnAmount.value=top.opener.document.forms[0].tcnAMount_rest.value;}")
			.Write("</" & "Script>")
		End If
	End With
	lclsFinanc_dra = Nothing
End Sub

'% creInitialCuota: genera el registro asociado a la cuota inicial del contrato
'--------------------------------------------------------------------------------------------
Private Sub creInitialCuota()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanc_dra As eFinance.FinanceDraft
	Dim lclsFinance_co As eFinance.financeCO
	Dim lclsDraft_hist As eFinance.DraftHist
	
	lclsFinance_co = New eFinance.financeCO
	lclsFinanc_dra = New eFinance.FinanceDraft
	
	With lclsFinanc_dra
		If Not .Find(Session("nContrat"), 1) Then
			If lclsFinance_co.Find(Session("nContrat"), Session("dEffecdate")) Then
				.nContrat = Session("nContrat")
				.nDraft = 1
				.dLimitdate = lclsFinance_co.dFirst_draf
				.nAmount = lclsFinance_co.nInitial
				.nStat_draft = 1
				.nAmount_net = lclsFinance_co.nInitial
				.nUsercode = Session("nUsercode")
				.nWay_pay = Session("nWay_pay")
				If .Add Then
					lclsDraft_hist = New eFinance.DraftHist
					'+ Se crea el movimiento inicial en la historia de la cuota
					lclsDraft_hist.nContrat = Session("nContrat")
					lclsDraft_hist.nDraft = 1
					lclsDraft_hist.nAmount = lclsFinance_co.nInitial
					lclsDraft_hist.nCurrency = Session("nCurrency")
					lclsDraft_hist.nType = 1
					lclsDraft_hist.nUsercode = Session("nUsercode")
					lclsDraft_hist.nCommit = 1
					Call lclsDraft_hist.Add()
				End If
			End If
		End If
	End With
	
	lclsFinanc_dra = Nothing
	lclsFinance_co = Nothing
	lclsDraft_hist = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues
mclsGeneral = New eGeneral.GeneralFunction

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "FI011"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("FI011"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "FI011.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 15/04/04 17:12 $|$$Author: Nvaplat7 $"

//% insAddValues: calcula el número de la cuota a generar
//--------------------------------------------------------------------------------------------
function insAddValues(){
//--------------------------------------------------------------------------------------------
//- Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 1;
        
//+ Se genera el número consecutivo del Order
	for(var llngIndex = 0;llngIndex < top.opener.marrArray.length;llngIndex++)
	    if(top.opener.marrArray[llngIndex].tcnDraft>llngMax)
	        llngMax = top.opener.marrArray[llngIndex].tcnDraft

	if(eval(++llngMax.length) > eval(self.document.forms[0].tcnDraft.maxLength))
//+ Se asignan null
		self.document.forms[0].tcnDraft.value = "";
	else
//+ Se asignan el valor por defecto de la cuota (consecutivo)
		self.document.forms[0].tcnDraft.value = ++llngMax;
}
//% insChangeValues: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function insChangeValues(sField, dEffecdate, nIndex){
//--------------------------------------------------------------------------------------------
	var lintCurrentIndex=0
	with(self.document.forms[0]){
    	switch(sField){
		    case 'Calc':
				if(confirm('Adv. 55927: <%=mclsGeneral.insLoadMessage(55927)%>'))
					insDefValues('delAllDraft')
				else
					if (optCalc[0].checked)
						optCalc[1].checked = true;
					else
						optCalc[0].checked = true;
				break;
			case 'Expirdat':
			case 'Amount':
				if(tcdExpirdat.value!='' &&
				   tcnAmount.value!=''){
					ldtmFirstDate = dEffecdate
					if(top.opener.marrArray.length>0)
						if(nIndex!=0)
							ldtmFirstDate = tcdPrevExpirdat.value
					insDefValues("calInterest","nInterest=" + top.opener.document.forms[0].hddInterest.value + "&dFirstDate=" + ldtmFirstDate + "&dLastDate=" + tcdExpirdat.value + "&nAmount=" + tcnAmount.value)
				}
		}
    }
}
</SCRIPT>	
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="FI011" ACTION="valFinanceSeq.aspx?sMode=2">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP011()
Else
	Call insPreDP011Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</HTML>





