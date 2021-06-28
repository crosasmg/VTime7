<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsTab_ActiveLife As eProduct.Tab_ActiveLife


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mclsSequence = New eFunctions.Sequence
mclsTab_ActiveLife = New eProduct.Tab_ActiveLife

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
<%
Response.Write("<BODY " & mclsSequence.BODYParameters() & ">")

With mobjValues
	Response.Write(mclsTab_ActiveLife.LoadTabs(Session("bQuery"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht")))
End With
If Request.QueryString.Item("sGoToNext") = "Yes" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
	
End If
mobjValues = Nothing
mclsSequence = Nothing
mclsTab_ActiveLife = Nothing
%>





