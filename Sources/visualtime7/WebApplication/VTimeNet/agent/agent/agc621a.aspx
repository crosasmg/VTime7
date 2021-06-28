<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjGrid1 As eFunctions.Grid
Dim mobjGrid2 As eFunctions.Grid
Dim mnLoans As Double
Dim mnRet As Double
Dim mnTax As Double
Dim mnTotal As Double


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader1()
	'------------------------------------------------------------------------------
	mobjGrid2 = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjGrid2.sSessionID = Session.SessionID
	mobjGrid2.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid2.sCodisplPage = "agc621a"
	Call mobjGrid2.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid2.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "", False, GetLocalResourceObject("tctBranchColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 30, "", False, GetLocalResourceObject("tctProductColumnToolTip"),  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 6, "", False, GetLocalResourceObject("tcnPolicyColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcncertifColumnCaption"), "tcncertif", 6, "", False, GetLocalResourceObject("tcncertifColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctTitularcColumnCaption"), "tctTitularc", 30, "", False, GetLocalResourceObject("tctTitularcColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDoctypeColumnCaption"), "tctDoctype", 15, "", False, GetLocalResourceObject("tctDoctypeColumnCaption"),  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDocnumbeColumnCaption"), "tcnDocnumbe", 6, "", False, "", False, 0,  ,  ,  , False)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdPay_DateColumnCaption"), "tcdPay_Date",  , False, GetLocalResourceObject("tcdPay_DateColumnCaption"),  ,  ,  , False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCom_AfecColumnCaption"), "tcnCom_Afec", 18, "", False, GetLocalResourceObject("tcnCom_AfecColumnToolTip"), False, 6,  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCom_ExenColumnCaption"), "tctCom_Exen", 10, "", False, GetLocalResourceObject("tctCom_ExenColumnToolTip"),  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTotorigiColumnCaption"), "tcnTotorigi", 18, "", False, GetLocalResourceObject("tcnTotorigiColumnToolTip"), False, 6,  ,  ,  , False)
	End With
	
	With mobjGrid2
		.Codispl = "AGC621"
		.Codisp = "AGC621"
		.Top = 100
		.Columns("Sel").GridVisible = False
		.AddButton = False
		.DeleteButton = False
	End With
End Sub

'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "agc621a"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnType_MoveColumnCaption"), "tcnType_Move", "Table401", eFunctions.Values.eValuesType.clngComboType, "")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDebitColumnCaption"), "tcnDebit", 18, "", False, "", False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCreditColumnCaption"), "tcnCredit", 18, "", False, "", False, 6,  ,  ,  , False)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "AGC621A"
		.Codisp = "AGC621A"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Top = 50
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'------------------------------------------------------------------------------
Private Sub insDefineHeader2()
	'------------------------------------------------------------------------------
	mobjGrid1 = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjGrid1.sSessionID = Session.SessionID
	mobjGrid1.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid1.sCodisplPage = "AGC621A"
	Call mobjGrid1.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columns del Grid
	Response.Write("<BR>")
	
	With mobjGrid1.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommissionColumnCaption"), "tcnCommission", 18, "", False, "", False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnLoansColumnCaption"), "tcnLoans", 18, "", False, "", False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTaxColumnCaption"), "tcnTax", 18, "", False, "", False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRetColumnCaption"), "tcnRet", 18, "", False, "", False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTotalColumnCaption"), "tcnTotal", 18, "", False, "", False, 6,  ,  ,  , False)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid1
		.Codispl = "AGC621A"
		.Codisp = "AGC621A"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Top = 50
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	Call insPreAGC621A2()
End Sub

'------------------------------------------------------------------------------
Private Sub insPreAGC621A()
	'------------------------------------------------------------------------------
	Dim lcolMove_acc As eCashBank.Move_accs
	Dim lclsMove_acc As Object
	Dim nDebit As Double
	Dim nCredit As Double
	
	lcolMove_acc = New eCashBank.Move_accs
	
	nDebit = 0
	nCredit = 0
	mnLoans = 0
	If lcolMove_acc.FindIntermedia(CInt(Request.QueryString.Item("nIntermed"))) Then
		For	Each lclsMove_acc In lcolMove_acc
			With mobjGrid
				If lclsMove_acc.nType_Move <> 321 And lclsMove_acc.nType_Move <> 324 Then
					.Columns("tcnType_Move").DefValue = lclsMove_acc.nType_Move
					
					If lclsMove_acc.nDebit <> 0 Then
						.Columns("tcnDebit").DefValue = lclsMove_acc.nDebit
						nDebit = nDebit + lclsMove_acc.nDebit
					Else
						.Columns("tcnDebit").DefValue = CStr(0)
					End If
					
					If lclsMove_acc.nCredit <> 0 Then
						.Columns("tcnCredit").DefValue = lclsMove_acc.nCredit
						nCredit = nCredit + lclsMove_acc.nCredit
					Else
						.Columns("tcnCredit").DefValue = CStr(0)
					End If
					
					Response.Write(mobjGrid.DoRow())
				ElseIf lclsMove_acc.nType_Move = 321 Then  ' Corresponde a Préstamos
					mnLoans = mnLoans + lclsMove_acc.nDebit
				End If
			End With
		Next lclsMove_acc
	End If
	Response.Write(mobjGrid.CloseTable())
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" border = 0>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD width= ""50%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD width= ""10%""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD width= ""16%"" align= right><LABEL ID=0>")


Response.Write(nDebit)


Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("            <TD width= ""29%"" align= right><LABEL ID=0>")


Response.Write(nCredit)


Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("<br>")

	
	Call insDefineHeader2()
	lcolMove_acc = Nothing
End Sub
'------------------------------------------------------------------------------
Private Sub insPreAGC621A2()
	'------------------------------------------------------------------------------
	mobjGrid1.Columns("tcnLoans").DefValue = CStr(mnLoans)
	mobjGrid1.Columns("tcnCommission").DefValue = CStr(mnTotal)
	mobjGrid1.Columns("tcnRet").DefValue = CStr(mnRet)
	mobjGrid1.Columns("tcnTotal").DefValue = CStr(mnTotal - mnLoans + mnTax + mnRet)
	mobjGrid1.Columns("tcnTax").DefValue = CStr(mnTax)
	
	Response.Write(mobjGrid1.DoRow())
	Response.Write(mobjGrid1.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
End Sub

'%insPreAGC621. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreAGC621_1()
	'------------------------------------------------------------------------------
	Dim lcolpay_comms As eAgent.pay_comms
	Dim lclspay_comm As Object
	
	lcolpay_comms = New eAgent.pay_comms
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	
	If lcolpay_comms.Findagc621a(mobjValues.StringToType(Request.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dEffecdateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nPay_Comm"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclspay_comm In lcolpay_comms
			With lclspay_comm
				mobjGrid2.Columns("tctBranch").DefValue = .sDesBranch
				mobjGrid2.Columns("tctProduct").DefValue = .sDesProduct
				mobjGrid2.Columns("tcnPolicy").DefValue = .nPolicy
				mobjGrid2.Columns("tcnCertif").DefValue = .nCertif
				mobjGrid2.Columns("tctTitularc").DefValue = .sTitularc
				If .nDocType = 1 Then
					mobjGrid2.Columns("tctDoctype").DefValue = "Recibo"
				Else
					mobjGrid2.Columns("tctDoctype").DefValue = "Contrato de financiamiento"
				End If
				mobjGrid2.Columns("tcnDocnumbe").DefValue = .nDocnumbe
				mobjGrid2.Columns("tcdPay_Date").DefValue = .dPay_Date
				mobjGrid2.Columns("cbeCurrency").DefValue = .nOricurr
				mobjGrid2.Columns("tcnCom_Afec").DefValue = .nCom_Afec
				mobjGrid2.Columns("tctCom_Exen").DefValue = .nCom_Exen
				mobjGrid2.Columns("tcnTotorigi").DefValue = .nTotLocal
				mnTotal = mnTotal + .nTotLocal
				If .ntaxloc > 0 Then
					mnTax = mnTax + .ntaxloc
				Else
					mnRet = mnRet + .ntaxloc
				End If
				
				Response.Write(mobjGrid2.DoRow())
			End With
		Next lclspay_comm
	End If
	Response.Write(mobjGrid2.CloseTable())
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" border = 0>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD width= ""50%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD width= ""10%""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD width= ""16%"" align= right><LABEL ID=0>")

	
Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("            <TD width= ""29%"" align= right><LABEL ID=0>")


Response.Write(mnTotal)


Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("<br>" & vbCrLf)
Response.Write("")

	
	Response.Write("<br>")
	
	lclspay_comm = Nothing
	lcolpay_comms = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGC621A")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "AGC621A"

mnLoans = 0
mnRet = 0
mnTax = 0
mnTotal = 0

%>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"        
</SCRIPT>        

    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmProvGroup" ACTION="valAgent.aspx?sCodispl=AGC621a">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0>Intermediario
            <TD><%=mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nIntermed"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valIntermedToolTip"))%></TD>
            <TD><LABEL ID=0>Liquidación
            <TD><%=mobjValues.NumericControl("nPay_Comm", 10, Request.QueryString.Item("nPay_Comm"),  , GetLocalResourceObject("nPay_CommToolTip"), False,  ,  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
    <br>
    <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Call insDefineHeader1()
Call insPreAGC621_1()
Call insDefineHeader()
Call insPreAGC621A()

%>
    <TABLE WIDTH="100%">
        <TR>
            <TD ALIGN="Right"><%=mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel)%></TD>
		</TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>
<%
mobjGrid1 = Nothing
mobjGrid2 = Nothing

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("AGC621A")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




