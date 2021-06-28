<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eTarif" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsTableTarifSeq As eTarif.TableTarifSeq


</script>
<%Response.Expires = -1
mclsSequence = New eFunctions.Sequence
mclsTableTarifSeq = New eTarif.TableTarifSeq
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
If Not IsNothing(Request.QueryString.Item("nAction")) Then
	Response.Write(mclsTableTarifSeq.LoadTabsTableTarif(CInt(Request.QueryString.Item("nAction")), Session("sSche_code"), Session("nId_Table"), Session("dEffecdate")))
	
	If Request.QueryString.Item("sGoToNext") = "Yes" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
	End If
	'+ En el caso que no se encuentre secuencia asociada, se carga la imagen del FRAME principal
	'+ por defecto
Else
	%>      
	<SCRIPT>top.fraFolder.document.location = "/VTimeNet/Common/Blank.htm"</SCRIPT> 
<%	
End If
mclsSequence = Nothing
%>





