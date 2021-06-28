<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Define las columnas del Grid
'-------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "CPC002"
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tcdDateColumnCaption"), "tcdDate", 10, "",  , GetLocalResourceObject("tcdDateColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctOffiNumColumnCaption"), "tctOffiNum", 10, "",  , GetLocalResourceObject("tctOffiNumColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctVoucherColumnCaption"), "tctVoucher", 10, "",  , GetLocalResourceObject("tctVoucherColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAcc_linesesColumnCaption"), "tcnAcc_lineses", 10, "",  , GetLocalResourceObject("tcnAcc_linesesColumnToolTip"),  , 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDebitColumnCaption"), "tcnDebit", 18, CStr(0),  , GetLocalResourceObject("tcnDebitColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCreditColumnCaption"), "tcnCredit", 18, CStr(0),  , GetLocalResourceObject("tcnCreditColumnToolTip"), True, 6)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CPC002"
		.AddButton = False
		.DeleteButton = False
		.Top = 80
		.Width = 330
		.Height = 300
		.Columns("Sel").GridVisible = False
		'.ActionQuery = True 
	End With
End Sub

'% insReaAcc_transaAcc_lines: Lee información de la tabla de Cuentas Contables (Ledger_acc)
'% de varias cuentas
'--------------------------------------------------------------------------------------------
Private Sub insReaAcc_transaAcc_lines()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsAcc_transa As eLedge.Acc_transa
	Dim lclsAcc_lines As eLedge.Acc_lines
	Dim nIniBalance As Double
	
	lclsAcc_transa = New eLedge.Acc_transa
	lclsAcc_lines = New eLedge.Acc_lines
	
	'- Se define la variable ldblDeb utilizada para almacenar el valor de los débitos
	
	Dim ldblDeb As Double
	
	'- Se define la variable ldblCre utilizada para almacenar el valor de los créditos
	
	Dim ldblCre As Double
	
	If lclsAcc_transa.AccReaOldBalance(mobjValues.StringToType(Session("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sAccount"), Request.QueryString.Item("sAux_accoun")) Then
		nIniBalance = lclsAcc_transa.nOldBalance
	Else
		nIniBalance = 0
	End If
	
	
	Response.Write("<SCRIPT>self.document.forms[0].lblInitBalance.value = '" & nIniBalance & "';</" & "Script>")
	
	
	If lclsAcc_transa.AccVoucherDetailByDate(mobjValues.StringToType(Session("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sAccount"), Request.QueryString.Item("sAux_accoun")) Then
		
		For	Each lclsAcc_transa In lclsAcc_transa.mcolAcc_transa
			With mobjGrid
				.Columns("tcdDate").DefValue = CStr(lclsAcc_transa.dEffecdate)
				
				If lclsAcc_transa.nOffiNum = eRemoteDB.Constants.intNull Then
					.Columns("tctOffiNum").DefValue = CStr(0)
				Else
					.Columns("tctOffiNum").DefValue = CStr(lclsAcc_transa.nOffiNum)
				End If
				
				.Columns("tctVoucher").DefValue = CStr(lclsAcc_transa.nVoucher)
				
				For	Each lclsAcc_lines In lclsAcc_transa.mcolAcc_lineses
					.Columns("tcnAcc_lineses").DefValue = CStr(lclsAcc_lines.nLine)
					If lclsAcc_lines.nDebit = eRemoteDB.Constants.intNull Then
						.Columns("tcnDebit").DefValue = CStr(0)
					Else
						.Columns("tcnDebit").DefValue = CStr(lclsAcc_lines.nDebit)
						ldblDeb = ldblDeb + lclsAcc_lines.nDebit
					End If
					
					If lclsAcc_lines.nCredit = eRemoteDB.Constants.intNull Then
						.Columns("tcnCredit").DefValue = CStr(0)
					Else
						.Columns("tcnCredit").DefValue = CStr(lclsAcc_lines.nCredit)
						ldblCre = ldblCre + lclsAcc_lines.nCredit
					End If
					
					.Columns("tctDescript").DefValue = lclsAcc_lines.sDescript
				Next lclsAcc_lines
				
				Response.Write(.DoRow)
			End With
		Next lclsAcc_transa
		
		'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
		
		Response.Write(mobjGrid.CloseTable())
	Else
		'	Response.Write "<NOTSCRIPT>alert(" & """Error.""" & ")</" & "Script>"
		'" & ldblDeb - ldblCre & "';"
	End If
	
	'Response.Write "<NOTSCRIPT>self.document.forms[0].hddEval.value = '"& Request.QueryString("nEval") &"'</" & "Script>"
	Response.Write("<SCRIPT>self.document.forms[0].lblEndBalance.value = '" & ldblDeb - ldblCre & "';</" & "Script>")
	
	lclsAcc_lines = Nothing
	lclsAcc_transa = Nothing
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CPC002"
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "CPC002", "CPC002.aspx"))

'If Request.QueryString("nMainAction") = clngActionQuery Then
'	mobjValues.ActionQuery = True
'End if

'+ Reescribe los valores en los campos del header para que no se pierdan
Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valAccount.value = '" & Request.QueryString.Item("sAccount") & "'</SCRIPT>")
Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valAux.value = '" & Request.QueryString.Item("sAux_accoun") & "'</SCRIPT>")
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="post" ID="FORM" NAME="CPC002" ACTION="ValLedgerQue.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("<BR>")
%>
		<TABLE WIDTH="100%">
			<TR> 
				<TD><LABEL><%= GetLocalResourceObject("lblInitBalanceCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("lblInitBalance", 30, "0",  , GetLocalResourceObject("lblInitBalanceToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
				<TD><LABEL><%= GetLocalResourceObject("lblEndBalanceCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("lblEndBalance", 30, "0",  , GetLocalResourceObject("lblEndBalanceToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
	        </TR>
		</TABLE>
<%
Response.Write("<BR>")

Call insDefineHeader()
Call insReaAcc_transaAcc_lines()

mobjGrid = Nothing
mobjValues = Nothing
%>     
	</FORM>
    <Script>
	//self.document.form[0].lblEndBalance.value = '3';  //ldblDeb - ldblCre
	</Script>	
	
</BODY>
</HTML>





