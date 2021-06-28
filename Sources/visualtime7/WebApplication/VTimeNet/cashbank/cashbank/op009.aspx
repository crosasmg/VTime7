<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

Dim mobjCheque As eCashBank.Cheque
Dim mobjCheques As eCashBank.Cheques


'%insPreOP090: Esta función se encaga de obtener los datos de la cuenta corriente
'--------------------------------------------------------------------------------------------
Private Sub insPreOP009()
	'--------------------------------------------------------------------------------------------
	
	'+ Contador local
	Dim lintCount As Integer
	
	'+ Se configura la estructura del grid.	
	Call ShowHeader()
	
	mobjCheque = New eCashBank.Cheque
	mobjCheques = New eCashBank.Cheques
	
	If mobjCheques.Find(Session("dStartDate"), Session("dEndDate"), Session("optChequeStat") + 1, Session("nConcept"), Session("valClient")) Then
		For lintCount = 1 To mobjCheques.Count
			mobjCheque = mobjCheques.Item(lintCount)
			With mobjGrid
				.Columns("tcnAccBank").DefValue = CStr(mobjCheque.nAcc_Bank)
				.Columns("tctDescript").DefValue = mobjCheque.sBank_name
				.Columns("tctCheck").DefValue = mobjCheque.sCheque
				.Columns("nAmount").DefValue = CStr(mobjCheque.nAmount)
				.Columns("tcnCurrency").DefValue = CStr(mobjCheque.nBank_curr)
				.Columns("tcnConcept").DefValue = mobjCheque.sDescript
				.Columns("tctClient").DefValue = mobjCheque.sClient
				.Columns("tcdIssue").DefValue = CStr(mobjCheque.dIssue_dat)
				
				.Columns("nRequest_nu").DefValue = CStr(mobjCheque.nRequest_nu)
				.Columns("nConsec").DefValue = CStr(mobjCheque.nConsec)
				.Columns("sCheck").DefValue = mobjCheque.sCheque
				.Columns("nCurrency").DefValue = CStr(mobjCheque.nBank_curr)
				Response.Write(.DoRow)
			End With
		Next 
	End If
	
	mobjCheques = Nothing
	mobjCheque = Nothing
End Sub

'---------------------------------------------------------------------------------------------
Private Sub ShowHeader()
	'---------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "op009"
	
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(40075, GetLocalResourceObject("tcnAccBankColumnCaption"), "tcnAccBank", 5, "")
		Call .AddTextColumn(40077, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "")
		Call .AddTextColumn(40078, GetLocalResourceObject("tctCheckColumnCaption"), "tctCheck", 20, "")
		Call .AddNumericColumn(40076, GetLocalResourceObject("nAmountColumnCaption"), "nAmount", 24, CStr(0),  ,  , True, 6)
		Call .AddPossiblesColumn(40074, GetLocalResourceObject("tcnCurrencyColumnCaption"), "tcnCurrency", "table11", eFunctions.Values.eValuesType.clngComboType)
		Call .AddTextColumn(0, GetLocalResourceObject("tcnConceptColumnCaption"), "tcnConcept", 50, "")
		Call .AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 20, "")
		Call .AddDateColumn(40079, GetLocalResourceObject("tcdIssueColumnCaption"), "tcdIssue", "")
		
		Call .AddHiddenColumn("nRequest_nu", "")
		Call .AddHiddenColumn("nConsec", "")
		Call .AddHiddenColumn("sCheck", "")
		Call .AddHiddenColumn("nCurrency", "")
		
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "OP009"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
		.Width = 450
		.Height = 400
	End With
	
End Sub

</script>
<%Response.Expires = 0

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mobjGrid = New eFunctions.Grid
End With

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "op009"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
function insStateZone(){
}
</SCRIPT>


<HTML>
    <HEAD>
        <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
        <%Response.Write(mobjValues.StyleSheet())
        Response.Write(mobjMenu.setZone(2, "OP009", "OP009.aspx"))
        If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	        mobjValues.ActionQuery = True
        End If
        %>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmchequesControl" ACTION="valCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%Call insPreOP009()%>
        </FORM>
    </BODY>
</HTML>

<%
mobjValues = Nothing
mobjMenu = Nothing
mobjGrid = Nothing

%>





