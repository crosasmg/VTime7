<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values


'% FI003Upd: Actualiza los valores de la PoPup una vez que se haya colocado  el contrato
' y giro a refinanciar 
'--------------------------------------------------------------------------------------------
Private Sub Exchange()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchange As eGeneral.Exchange
	Dim nExchange As Double
	
	lclsExchange = New eGeneral.Exchange
	
	'+ Calculando el factor de cambio
	
	'Call lclsExchange.Convert(Null, nAuxAmount, nCurrency, nCurr_cont, Date, 0)
	Call lclsExchange.Convert(0, 12360000, CInt(Request.Form.Item("cbeCurr_cont")), CInt(Request.QueryString.Item("nCurrency")), Today, 0)
	
	If lclsExchange.pdblExchange = -1 Then
		nExchange = 1
	Else
		nExchange = lclsExchange.pdblExchange
	End If
	
	Response.Write("	opener.document.forms[0].tcnExchange.value='" & nExchange & "';")
	
	lclsExchange = Nothing
End Sub

</script>
<%Response.Expires = 0

mclsValues = New eFunctions.Values
%>
<HTML>
<HEAD>


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
	Case "Exchange"
		Call Exchange()
End Select

Response.Write("window.close()")
Response.Write("</SCRIPT>")

mclsValues = Nothing


%>




