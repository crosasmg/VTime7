<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsFinanceWin As eFinance.FinanceWin
'+ Objeto para el manejo generico	
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = 0
mclsSequence = New eFunctions.Sequence
mclsFinanceWin = New eFinance.FinanceWin
mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
	
	<META http-equiv="Content-Language" content="es">
    <BASE TARGET="fraFolder">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
	<%Response.Write("<SCRIPT>")
If Session("bQuery") Then
	Response.Write("var pblnQuery=true;")
Else
	Response.Write("var pblnQuery=false;")
End If
Response.Write("</SCRIPT>")
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
Response.Write(mclsFinanceWin.LoadTabs(Session("nTransaction"), Session("nContrat"), Session("dEffecdate"), Session("nUsercode"), Session("sSche_code")))


If Request.QueryString.Item("sGoToNext") = "Yes" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

mclsSequence = Nothing
mclsFinanceWin = Nothing
%>





