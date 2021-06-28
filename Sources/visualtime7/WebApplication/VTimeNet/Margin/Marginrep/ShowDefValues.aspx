<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'-Variable para el manejo de funciones generales
Dim mobjValues As eFunctions.Values


'% inscalExchange: Se calcula el factor de cambio para una fecha-moneda.
'%				   Se invoca desde la MGS001
'--------------------------------------------------------------------------------------------
Sub inscalExchange()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchange As eGeneral.Exchange
	lclsExchange = New eGeneral.Exchange
	Call lclsExchange.Convert(0, 0, mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0)
	Response.Write("top.frames['fraFolder'].document.forms[0].hddExchange.value=" & lclsExchange.pdblExchange & ";")
	Response.Write("top.frames['fraFolder'].ShowChangeAmount();")
	lclsExchange = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


</HEAD>
<BODY>
    <FORM NAME="ShowDefValues">
    </FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "nExchange"
		Call inscalExchange()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

%>





