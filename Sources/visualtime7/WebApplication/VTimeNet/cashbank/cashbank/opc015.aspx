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
	
	mobjGrid.sCodisplPage = "opc015"
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddDateColumn(40191, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(40188, GetLocalResourceObject("cbeBankextColumnCaption"), "cbeBankext", "table7", 1, "",  ,  ,  ,  ,  , CBool("True"),  , GetLocalResourceObject("cbeBankextColumnToolTip"))
		Call .AddTextColumn(40190, GetLocalResourceObject("txtChequeColumnCaption"), "txtCheque", 10, "",  , GetLocalResourceObject("txtChequeColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(40189, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Width = 300
		.Height = 400
		.Codispl = "OPC015"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreOPC015: Función que asigna valor a las columnas del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC015()
	'--------------------------------------------------------------------------------------------
	'- Se define la variable para la carga de datos del Grid de la ventana		
	Dim lclsMove_acc As eCashBank.Move_acc
	Dim lcolMove_accs As eCashBank.Move_accs
	
	With Server
		lclsMove_acc = New eCashBank.Move_acc
		lcolMove_accs = New eCashBank.Move_accs
	End With
	
	If lcolMove_accs.FindMoveAcc_OPC015(mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sBussiType"), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsMove_acc In lcolMove_accs
			With mobjGrid
				.Columns("tcdEffecdate").DefValue = CStr(lclsMove_acc.dOperdate)
				.Columns("cbeBankext").DefValue = CStr(lclsMove_acc.nBankext)
				.Columns("txtCheque").DefValue = lclsMove_acc.sCheque
				.Columns("tcnAmount").DefValue = CStr(lclsMove_acc.nAmount)
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsMove_acc
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	'+ Se reasignan los valores del ancabezado de la forma 
	
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcdEffecdate.value='" & Request.QueryString.Item("dEffecdate") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeTypeAccount.value=" & mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble) & ";</" & "Script>")
	'Response.Write "<NOTSCRIPT>top.fraHeader.document.forms[0].cbeBussiType.value='" & Request.QueryString ("sBussiType") & "';</" & "Script>"
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].dtcClient.value='" & Request.QueryString.Item("sClient") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCurrency.value=" & mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble) & ";</" & "Script>")
	
	lclsMove_acc = Nothing
	lcolMove_accs = Nothing
End Sub

</script>
<%Response.Expires = 0

'- Se crean las instancias de las variables modulares
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mobjGrid = New eFunctions.Grid
End With

mobjValues.sCodisplPage = "opc015"

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
	.Write(mobjMenu.setZone(2, "OPC015", "OPC015.aspx"))
End With
mobjMenu = Nothing
%> 
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCashBank.aspx?x=1">
<%Call insDefineHeader()
Call insPreOPC015()
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




