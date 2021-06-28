<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values


'% insShowYear: Asigna valores a parámetros del control valBudget (Presupuesto) de la página CPC003_K
'----------------------------------------------------------------------------------------------------
Sub insShowYear()
	'----------------------------------------------------------------------------------------------------
	
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

</script>
<%Response.Expires = 0
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
	Case "Year"
		Call insShowYear()
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing
%>




