<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddDateColumn(40162, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(40161, GetLocalResourceObject("txtConceptColumnCaption"), "txtConcept", 12, "",  , GetLocalResourceObject("txtConceptColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(40154, GetLocalResourceObject("optDebCreColumnCaption"), "optDebCre", "table287", 1, "",  ,  ,  ,  ,  , True,  , GetLocalResourceObject("optDebCreColumnToolTip"))
		Call .AddNumericColumn(40157, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddPossiblesColumn(40155, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "table10", 1, "",  ,  ,  ,  ,  , CBool("True"),  , GetLocalResourceObject("cbeBranchColumnToolTip"))
		Call .AddPossiblesColumn(40156, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabProdmaster1", 1, "", True,  ,  ,  ,  , CBool("True"),  , GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddNumericColumn(40158, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 8, "",  , GetLocalResourceObject("tcnPolicyColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(40159, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 8, "",  , GetLocalResourceObject("tcnCertifColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(40160, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 9, "",  , GetLocalResourceObject("tcnReceiptColumnToolTip"),  ,  ,  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Width = 300
		.Height = 400
		.Codispl = "OPC011"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreOPC011: Función que asigna valor a las columnas del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC011()
	'--------------------------------------------------------------------------------------------
	'- Se define la variable para la carga de datos del Grid de la ventana		
	Dim lclsMove_acc As eCashBank.Move_acc
	Dim lcolMove_accs As eCashBank.Move_accs
	
	lclsMove_acc = New eCashBank.Move_acc
	lcolMove_accs = New eCashBank.Move_accs
	
	If lcolMove_accs.FindMoveAcc_OPC011(mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsMove_acc In lcolMove_accs
			With mobjGrid
				.Columns("tcdEffecdate").DefValue = mobjValues.TypeToString(lclsMove_acc.dOperdate, eFunctions.Values.eTypeData.etdDate)
				.Columns("txtConcept").DefValue = lclsMove_acc.sDescript
				If lclsMove_acc.nDebit > 0 Then
					.Columns("optDebCre").DefValue = "1"
				Else
					.Columns("optDebCre").DefValue = "2"
				End If
				.Columns("tcnAmount").DefValue = CStr(lclsMove_acc.nAmount)
				.Columns("cbeBranch").DefValue = CStr(lclsMove_acc.nBranch)
				.Columns("valProduct").Parameters.Add("nBranch", lclsMove_acc.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valProduct").DefValue = CStr(lclsMove_acc.nProduct)
				.Columns("tcnPolicy").DefValue = CStr(lclsMove_acc.nPolicy)
				.Columns("tcnCertif").DefValue = CStr(lclsMove_acc.nCertif)
				.Columns("tcnReceipt").DefValue = CStr(lclsMove_acc.nReceipt)
				
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsMove_acc
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	'+ Se reasignan los valores del ancabezado de la forma 
	
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].gmdEffecdate.value='" & Request.QueryString.Item("dEffecdate") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeTypeAccount.value=" & mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble) & ";</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valClient.value='" & Request.QueryString.Item("sClient") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCurrency.value=" & mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble) & ";</" & "Script>")
	
	lclsMove_acc = Nothing
	lcolMove_accs = Nothing
End Sub

</script>
<%Response.Expires = -1

'- Se crean las instancias de las variables modulares
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mobjGrid = New eFunctions.Grid
End With


mobjValues.sCodisplPage = "OPC011"
mobjGrid.sCodisplPage = "OPC011"


%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------

}
</SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OPC011", "OPC011.aspx"))
End With
mobjMenu = Nothing
%> 
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCashBank.aspx?x=1">
<%Call insDefineHeader()
Call insPreOPC011()
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




