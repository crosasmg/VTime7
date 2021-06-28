<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values


'% insSessionCompan: Asigna valor a variable de session que contiene la compañia contable
'--------------------------------------------------------------------------------------
Sub insSessionCompan()
	'--------------------------------------------------------------------------------------
	Session("nLedCompan") = Request.QueryString.Item("nLed_compan")
End Sub

'% insLockControl: Verifica si la cuenta es de ultimo nivel para habilitar campo auxiliar
'--------------------------------------------------------------------------------------
Sub insLockControl()
	'--------------------------------------------------------------------------------------
	Dim lclsLedger_acc As eLedge.LedgerAcc
	Dim lintCompany As Integer
	Dim lstrAccount As String
	Dim lstrDisabled As String
	
	lstrDisabled = "true"
	
	lclsLedger_acc = New eLedge.LedgerAcc
	lintCompany = mclsValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble)
	lstrAccount = Request.QueryString.Item("sAccount")
	
	If lclsLedger_acc.ValAccountStruc(lintCompany, lstrAccount) Then
		If lclsLedger_acc.nLast_level = lclsLedger_acc.nLevel Then
			lstrDisabled = "false"
		End If
	End If
	
	If Request.QueryString.Item("sWindow") = "Header" Then
		Response.Write("top.fraHeader.document.forms[0].valAux.disabled = " & lstrDisabled & ";")
		Response.Write("top.fraHeader.document.forms[0].btnvalAux.disabled = " & lstrDisabled & ";")
		Response.Write("if (top.fraHeader.document.forms[0].valAux.value>''){")
		Response.Write("top.fraHeader.document.forms[0].valAux.value='';")
		Response.Write("}")
	Else
		Response.Write("top.fraFolder.document.forms[0].chkDebit.disabled = " & lstrDisabled & ";")
		Response.Write("top.fraFolder.document.forms[0].chkCredit.disabled = " & lstrDisabled & ";")
		
		If Request.QueryString.Item("sAux") = " " Then
			Response.Write("top.fraFolder.document.forms[0].cbeAux.disabled = " & lstrDisabled & ";")
		End If
	End If
End Sub

'%Copy_Struct: Copia la estructura de la compañia a la que estamos instalando 
'% Copy_Struct: Verifica si la cuenta es de ultimo nivel para habilitar campo auxiliar
'--------------------------------------------------------------------------------------
Sub Copy_Struct()
	'--------------------------------------------------------------------------------------
	Dim lclsLed_compan As eLedge.Led_compan
	
	lclsLed_compan = New eLedge.Led_compan
	
	If Request.QueryString.Item("nLed_Compan") > vbNullString Then
		If lclsLed_compan.Find(CInt(Request.QueryString.Item("nLed_Compan")), True) Then
			Response.Write("top.fraFolder.document.forms[0].gmnCode1.value = " & Mid(lclsLed_compan.sStructure, 1, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnCode2.value = " & Mid(lclsLed_compan.sStructure, 2, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnCode3.value = " & Mid(lclsLed_compan.sStructure, 3, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnCode4.value = " & Mid(lclsLed_compan.sStructure, 4, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnCode5.value = " & Mid(lclsLed_compan.sStructure, 5, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnCode6.value = " & Mid(lclsLed_compan.sStructure, 6, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnCode7.value = " & Mid(lclsLed_compan.sStructure, 7, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnUnit1.value = " & Mid(lclsLed_compan.sStruct_uni, 1, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnUnit2.value = " & Mid(lclsLed_compan.sStruct_uni, 2, 1) & ";")
			Response.Write("top.fraFolder.document.forms[0].gmnUnit3.value = " & Mid(lclsLed_compan.sStruct_uni, 3, 1) & ";")
		End If
	End If
	
	lclsLed_compan = Nothing
End Sub

</script>
<%
Response.Expires = -1
mclsValues = New eFunctions.Values
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


</HEAD>

<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>

<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Led_compan"
		Call insSessionCompan()
	Case "Locked"
		Call insLockControl()
	Case "Copy_Struct"
		Call Copy_Struct()
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")
%>




