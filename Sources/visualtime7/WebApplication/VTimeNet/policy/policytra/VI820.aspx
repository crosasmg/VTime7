<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As Object
Dim mcoltmp_undo_move_accs As eBatch.tmp_undo_move_accs
Dim mclstmp_undo_move_acc As Object



'+ insDefineHeader: Definición del Grid
'-------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------
	Dim lclsCurrency As ePolicy.Curren_pol
	Dim sCurrency As String
	Dim sDate As Object
	lclsCurrency = New ePolicy.Curren_pol
	Call lclsCurrency.findCurrency("2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	sCurrency = lclsCurrency.sDescript
	
	lclsCurrency = Nothing
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	
	mobjGrid.sCodisplPage = "VI820"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid
		With .Columns
			Call .AddCheckColumn(0, GetLocalResourceObject("chkAuxSelColumnCaption"), "chkAuxSel", vbNullString, True)
			Call .AddCheckColumn(0, GetLocalResourceObject("chkAuxSel1ColumnCaption"), "chkAuxSel1", vbNullString, True)
			
			If mobjValues.StringToType(Session("sPolitype"), eFunctions.Values.eTypeData.etdDouble) = 2 And mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble) = 0 Then
				Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 5, CStr(0),  , GetLocalResourceObject("tcnCertifColumnToolTip"))
			Else
				Call .AddHiddenColumn("tcnCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
			End If
			Call .AddTextColumn(0, GetLocalResourceObject("tcdOperdateColumnCaption"), "tcdOperdate", 10, vbNullString,  , GetLocalResourceObject("tcdOperdateColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tctType_moveColumnCaption"), "tctType_move", 15, vbNullString,  , GetLocalResourceObject("tctType_moveColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 15, sCurrency,  , GetLocalResourceObject("tctCurrencyColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnOriginColumnCaption"), "tcnOrigin", "Table5633", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnOriginColumnToolTip"))
			Call .AddNumericColumn(0, GetLocalResourceObject("tctCreditColumnCaption"), "tctCredit", 18, CStr(0),  , GetLocalResourceObject("tctCreditColumnToolTip"),  , 6)
			Call .AddNumericColumn(0, GetLocalResourceObject("tctDebitColumnCaption"), "tctDebit", 18, CStr(0),  , GetLocalResourceObject("tctDebitColumnToolTip"),  , 6)
			Call .AddNumericColumn(0, GetLocalResourceObject("tctTaxColumnCaption"), "tctTax", 18, CStr(0),  , GetLocalResourceObject("tctTaxColumnToolTip"),  , 6)
			Call .AddHiddenColumn("hddManual", vbNullString)
			Call .AddHiddenColumn("hddidconsec", vbNullString)
			Call .AddHiddenColumn("hddInvested", vbNullString)
			Call .AddHiddenColumn("hddId_reverse", vbNullString)
			
		End With
		
		.Codispl = "VI818"
		.Width = 420
		.Height = 260
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		
	End With
End Sub

'%inspreVI818: Se cargan los Valores en el Grid
'-------------------------------------------------------------------------------------------
Private Sub inspreVI820()
	'-------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lstrvalue As String
	
	mcoltmp_undo_move_accs = New eBatch.tmp_undo_move_accs
	If mcoltmp_undo_move_accs.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 2) Then
		lintIndex = 0
		lstrvalue = "1"
		With mobjGrid
			For	Each mclstmp_undo_move_acc In mcoltmp_undo_move_accs
				
				.Columns("tcnCertif").DefValue = mclstmp_undo_move_acc.nCertif
				.Columns("tcdOperdate").DefValue = mclstmp_undo_move_acc.dOperdate
				.Columns("tctType_move").DefValue = mclstmp_undo_move_acc.sType_move
				.Columns("tctCurrency").DefValue = mclstmp_undo_move_acc.sCurrency
				.Columns("tcnOrigin").DefValue = mclstmp_undo_move_acc.nOrigin
				.Columns("tcnOrigin").Descript = mclstmp_undo_move_acc.sOrigin
				.Columns("tctCredit").DefValue = mclstmp_undo_move_acc.nCredit
				.Columns("tctDebit").DefValue = mclstmp_undo_move_acc.nDebit
				.Columns("tctTax").DefValue = mclstmp_undo_move_acc.nTax
				.Columns("hddManual").DefValue = mclstmp_undo_move_acc.sManual
				.Columns("hddidconsec").DefValue = mclstmp_undo_move_acc.nidconsec
				.Columns("hddInvested").DefValue = mclstmp_undo_move_acc.nInvested
				.Columns("hddId_reverse").DefValue = mclstmp_undo_move_acc.nId_reverse
				
				If mclstmp_undo_move_acc.sManual = "1" Then
					.Columns("chkAuxSel").Checked = 1
				Else
					.Columns("chkAuxSel").Checked = 2
				End If
				.Columns("chkAuxSel").Disabled = True
				If mclstmp_undo_move_acc.sManual <> "1" And mclstmp_undo_move_acc.sSel = "1" Then
					.Columns("chkAuxSel1").Checked = 1
				Else
					.Columns("chkAuxSel1").Checked = 2
				End If
				.Columns("chkAuxSel1").Disabled = True
				
				.Columns("tctType_move").HRefScript = ""
				If mclstmp_undo_move_acc.nType_move = 1 Or mclstmp_undo_move_acc.nType_move = 2 Or (mclstmp_undo_move_acc.nType_move = 5 And mclstmp_undo_move_acc.nId_reverse <= 0) Or mclstmp_undo_move_acc.nType_move = 14 Or mclstmp_undo_move_acc.nType_move = 699 Or mclstmp_undo_move_acc.nType_move = 700 Or mclstmp_undo_move_acc.nType_move = 701 Then
					.Columns("Sel").Disabled = False
					.Columns("tctType_move").EditRecord = True
				Else
					.Columns("Sel").Disabled = True
					.Columns("tctType_move").EditRecord = False
				End If
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			Next mclstmp_undo_move_acc
		End With
	End If
	
	Response.Write(mobjGrid.CloseTable)
	
End Sub

</script>
<%Response.Expires = -1441
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
			
<SCRIPT>
//% insAccept: se realizan las acciones al aceptar la ventana
//-------------------------------------------------------------------------------------------
function insAccept(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
        opener.top.fraSubmit.document.location.href = '/VTimeNet/Policy/Policytra/valpolicytra.aspx?sCodispl=VI820';
        window.close();
	}
}

</SCRIPT>

	<%
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI818"

Response.Write(mobjValues.StyleSheet())

%>

</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI820">
		<%Call insDefineHeader()
Call inspreVI820()
%>
	<TABLE WIDTH=100%>
		<TR>
			<TD CLASS="HorLine" COLSPAN="4"></TD>
		</TR>
		<TR>
			<TD COLSPAN="2" WIDTH="65%"><LABEL><B><%= GetLocalResourceObject("AnchorCaption") %></DIV></B></LABEL></TD>
			<TD ALIGN="Right"><%=mobjValues.ButtonAcceptCancel("insAccept();", "window.close();", False)%></TD>
			
		</TR>
	</TABLE>
<%
mobjGrid = Nothing
mobjValues = Nothing
mcoltmp_undo_move_accs = Nothing
mclstmp_undo_move_acc = Nothing
%>

	</FORM>
</BODY>
</HTML>






