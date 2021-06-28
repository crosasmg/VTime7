<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values


'% insShowAccount: Asigna valores a parámetros del control valAccount (Cuenta Contable)
'% de la página CPC002_K
'--------------------------------------------------------------------------------------
Sub insShowAccount()
	'--------------------------------------------------------------------------------------
	
	Dim lclsLed_compan As eLedge.Led_compan
	lclsLed_compan = New eLedge.Led_compan
	
	If lclsLed_compan.Find(mclsValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		'+ Se asigna el 1er. parámetro (nLed_compan) del SP que le da valor al control valBudget (Presupuesto)
		'+ de la página CPC003_K
		Response.Write("opener.document.forms[0].valBudget.Parameters.Param1.sValue = " & lclsLed_compan.nLed_compan & ";")
		
		'+ Se asigna el 3er. parámetro (nCurrency) del SP que le da valor al control valBudget (Presupuesto)
		'+ de la página CPC003_K
		
		Response.Write("opener.document.forms[0].valBudget.Parameters.Param3.sValue = " & lclsLed_compan.nCurrency & ";")
	End If
End Sub

'% insSessionCompan: Asigna valor a variable de session que contiene la compañia contable
'--------------------------------------------------------------------------------------
Sub insSessionCompan()
	'--------------------------------------------------------------------------------------
	Session("nLedCompan") = Request.QueryString.Item("nLed_compan")
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
	Case "Account"
		Call insShowAccount()
	Case "Led_Compan"
		Call insSessionCompan()
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing

%>




