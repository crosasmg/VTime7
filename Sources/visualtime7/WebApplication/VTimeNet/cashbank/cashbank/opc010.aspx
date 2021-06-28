<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid.sCodisplPage = "opc010"
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddTextColumn(40148, GetLocalResourceObject("tctMovementColumnCaption"), "tctMovement", 30, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctMovementColumnToolTip"))
		Call .AddTextColumn(40149, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddDateColumn(40150, GetLocalResourceObject("tcdOperdateColumnCaption"), "tcdOperdate",  ,  , GetLocalResourceObject("tcdOperdateColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(0),  , GetLocalResourceObject("tcnPolicyColumnToolTip"), False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnProponumColumnCaption"), "tcnProponum", 10, CStr(0),  , GetLocalResourceObject("tcnProponumColumnToolTip"), False)
		Call .AddNumericColumn(40145, GetLocalResourceObject("tcnDebitColumnCaption"), "tcnDebit", 18, CStr(0),  , GetLocalResourceObject("tcnDebitColumnToolTip"), True, 6)
		Call .AddNumericColumn(40146, GetLocalResourceObject("tcnCreditColumnCaption"), "tcnCredit", 18, CStr(0),  , GetLocalResourceObject("tcnCreditColumnToolTip"), True, 6)
		Call .AddNumericColumn(40147, GetLocalResourceObject("tcnBordereauxColumnCaption"), "tcnBordereaux", 9, CStr(0),  , GetLocalResourceObject("tcnBordereauxColumnToolTip"))
		Call .AddButtonColumn(0, GetLocalResourceObject("SCA2-817ColumnCaption"), "SCA2-817", 0, True, Request.QueryString.Item("Type") <> "PopUp",  ,  ,  ,  , "btnNotenum")
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "OPC010"
		.Columns("Sel").GridVisible = False
		.DeleteButton = False
		.AddButton = False
	End With
End Sub

'% insPreOPC010: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC010()
	'--------------------------------------------------------------------------------------------
	Dim lclsMove_acc As eCashBank.Move_acc
	Dim lcolMove_accs As eCashBank.Move_accs
	Dim ldblDebit As Double
	Dim ldblCredit As Double
	Dim ldblBalance As Object
	
	ldblCredit = 0
	ldblDebit = 0
	
	With Server
		lclsMove_acc = New eCashBank.Move_acc
		lcolMove_accs = New eCashBank.Move_accs
	End With
	
	If lcolMove_accs.Find_CurrAccInq(mobjValues.StringToDate(Request.QueryString.Item("dEffecdate")), mobjValues.StringToType(Request.QueryString.Item("nTyp_acco"), eFunctions.Values.eTypeData.etdInteger), Request.QueryString.Item("sType_acc"), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger)) Then
		
		For	Each lclsMove_acc In lcolMove_accs
			With mobjGrid
				.Columns("tcnPolicy").DefValue = CStr(lclsMove_acc.nPolicy)
				.Columns("tcnProponum").DefValue = CStr(lclsMove_acc.nProponum)
				.Columns("tctMovement").DefValue = lclsMove_acc.nIdConsec & " - " & lclsMove_acc.sShort_des
				.Columns("tctDescript").DefValue = lclsMove_acc.sDescript
				.Columns("tcdOperdate").DefValue = CStr(lclsMove_acc.dOperdate)
				.Columns("tcnDebit").DefValue = CStr(lclsMove_acc.nDebit)
				.Columns("tcnCredit").DefValue = CStr(lclsMove_acc.nCredit)
				.Columns("tcnBordereaux").DefValue = mobjValues.TypeToString(lclsMove_acc.nBordereaux, eFunctions.Values.eTypeData.etdDouble)
                    .Columns("btnNotenum").nNotenum = lclsMove_acc.nNoteNum
				ldblDebit = ldblDebit + lclsMove_acc.nDebit
				ldblCredit = ldblCredit + lclsMove_acc.nCredit
				Response.Write(.DoRow)
			End With
		Next lclsMove_acc
		
		'+ Se reasignan los valores del encabezado de la forma
		With Response
			.Write("<SCRIPT>top.fraHeader.document.forms[0].tcdEffecdate.value='" & Request.QueryString.Item("dEffecdate") & "';</" & "Script>")
			.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeTypeAccount.value=" & Request.QueryString.Item("nTyp_acco") & ";</" & "Script>")
			.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeBussType.value=" & Request.QueryString.Item("sType_acc") & ";</" & "Script>")
			.Write("<SCRIPT>top.fraHeader.document.forms[0].dtcClient.value='" & Request.QueryString.Item("sClient") & "';</" & "Script>")
			.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeBranch.value=" & Request.QueryString.Item("nBranch") & ";</" & "Script>")
			.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnPolicy.value=" & Request.QueryString.Item("nPolicy") & ";</" & "Script>")
			.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnCertif.value=" & Request.QueryString.Item("nCertif") & ";</" & "Script>")
			.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCurrency.value=" & Request.QueryString.Item("nCurrency") & ";</" & "Script>")
			.Write("<SCRIPT>var nBalance = insConvertNumber('" & CStr(ldblDebit - ldblCredit) & "');</" & "Script>")
			.Write("<SCRIPT>insShowDateHeader(nBalance);</" & "Script>")
		End With
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lclsMove_acc = Nothing
	lcolMove_accs = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "opc010"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">



    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OPC010", "OPC010.aspx"))
End With
mobjMenu = Nothing%>
<SCRIPT>

 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 29/03/04 20:17 $|$$Author: Nvaplat7 $"
    
//%insShowDateHeader: Esta función permite recuperar los Datos introducidos en el 
//                    Header, igualmente muestra el saldo de la cuenta en tratamiento,
//                    así como el tipo del mismo (Deudor,Acreedor)
//------------------------------------------------------------------------------------------
function insShowDateHeader(nBal){
//------------------------------------------------------------------------------------------
    with (top.frames[1].document.forms[0]){
        if (nBal<0)        
            optTypeAmou[0].checked=true //Deudor
		if (nBal>0)		
            optTypeAmou[1].checked=true //Acreedor
	}
    nBal=Math.abs(nBal)    
    top.frames[1].document.getElementById("lblBalance").innerHTML=VTFormat(nBal, '', '', '', 2, true)
}
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCashBank.aspx?mode=2">
    <%Response.Write(mobjValues.ShowWindowsName("OPC010"))%>
    
    <TABLE WIDTH="100%">        
           <%Call insDefineHeader()
Call insPreOPC010()%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




