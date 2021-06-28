<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mobjMove_acc As eCashBank.Move_acc
Dim mobjMove_accs As eCashBank.Move_accs


'%insPreOPC012: Esta función se encaga de obtener los datos de la consulta.
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC012()
	'--------------------------------------------------------------------------------------------
	
	'+ Se configura la estructura del grid.	
	Call ShowHeader()
	
	If mobjMove_accs.FindMoveAcc_OPC012(Session("nType_acco"), Session("sType_acc"), Session("sClient"), Session("nCurrency"), Session("dOperdate")) Then
		
		For	Each mobjMove_acc In mobjMove_accs
			With mobjGrid
				.Columns("tcdDate").DefValue = CStr(mobjMove_acc.dOperdate)
				.Columns("tctDescript").DefValue = mobjMove_acc.sDescript
				If (mobjMove_acc.nDebit <= 0) Then
					.Columns("tcnDebitCredit").DefValue = CStr(1)
				Else
					.Columns("tcnDebitCredit").DefValue = CStr(2)
				End If
				.Columns("tcnAmount").DefValue = CStr(System.Math.Abs(mobjMove_acc.nAmount))
				.Columns("tcnBranch").DefValue = CStr(mobjMove_acc.nBranch)
				.Columns("tcnProduct").DefValue = mobjMove_acc.sProductDes
				.Columns("tcnClaim").DefValue = CStr(mobjMove_acc.nClaim)
				.Columns("tcnMovement").DefValue = CStr(mobjMove_acc.nTransac)
				.Columns("tcnPolicy").DefValue = CStr(mobjMove_acc.nPolicy)
				.Columns("tcnCertif").DefValue = CStr(mobjMove_acc.nCertif)
				Response.Write(.DoRow)
			End With
		Next mobjMove_acc
	End If
	Response.Write(mobjGrid.closeTable())
	
	mobjMove_acc = Nothing
	mobjMove_accs = Nothing
End Sub

'%insPreOPC012: Configura los títulos del encabezado.
'---------------------------------------------------------------------------------------------
Private Sub ShowHeader()
	'---------------------------------------------------------------------------------------------
	
	mobjGrid.sCodisplPage = "opc012"
	
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddDateColumn(40172, GetLocalResourceObject("tcdDateColumnCaption"), "tcdDate")
		Call .AddTextColumn(40170, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "")
		Call .AddPossiblesColumn(40163, GetLocalResourceObject("tcnDebitCreditColumnCaption"), "tcnDebitCredit", "Table287", eFunctions.Values.eValuesType.clngComboType)
		Call .AddNumericColumn(40165, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  ,  , True, 6)
		Call .AddPossiblesColumn(40164, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", "Table10", eFunctions.Values.eValuesType.clngComboType)
		Call .AddTextColumn(40171, GetLocalResourceObject("tcnProductColumnCaption"), "tcnProduct", 30, "")
		Call .AddNumericColumn(40166, GetLocalResourceObject("tcnClaimColumnCaption"), "tcnClaim", 12, "")
		Call .AddNumericColumn(40167, GetLocalResourceObject("tcnMovementColumnCaption"), "tcnMovement", 12, "")
		Call .AddNumericColumn(40168, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 12, "")
		Call .AddNumericColumn(40169, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 12, "")
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "OPC012"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
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
	mobjMove_acc = New eCashBank.Move_acc
	mobjMove_accs = New eCashBank.Move_accs
End With

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "opc012"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
function insStateZone(){
}
</SCRIPT>


<HTML>
    <HEAD>
        <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
        <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OPC012", "OPC012.aspx"))
End With
%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmchequesControl" ACTION="valCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%
mobjGrid.ActionQuery = mobjValues.ActionQuery
Call insPreOPC012()
%>
        </FORM>
    </BODY>
</HTML>

<%
mobjValues = Nothing
mobjMenu = Nothing
mobjGrid = Nothing

%>





