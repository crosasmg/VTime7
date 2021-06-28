<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objecto para el manejo de la funciones generales
Dim mobjValues As eFunctions.Values

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsProd_win As eProduct.Prod_win


</script>
<%Response.Expires = 0
mclsSequence = New eFunctions.Sequence
mobjValues = New eFunctions.Values
mclsProd_win = New eProduct.Prod_win
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
Response.Write(mclsProd_win.LoadTabs(Session("bQuery"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("nBranch"), Session("nProduct"), Session("nUsercode"), Session("sTypeCompanyUser")))

If Request.QueryString.Item("sGoToNext") = "Yes" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

mclsSequence = Nothing
mclsProd_win = Nothing
mobjValues = Nothing
%>





