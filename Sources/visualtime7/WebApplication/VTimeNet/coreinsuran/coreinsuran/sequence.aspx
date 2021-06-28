<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsCoReinsuran As eCoReinsuran.CoReinsuran_win


</script>
<%Response.Expires = -1
mclsSequence = New eFunctions.Sequence
mclsCoReinsuran = New eCoReinsuran.CoReinsuran_win
%>
<HTML>
<HEAD>
	
	<META HTTP-EQUIV="Content-Language" CONTENT="es">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
    <BASE TARGET="fraFolder">
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
Response.Write(mclsCoReinsuran.LoadTabs(CInt(Request.QueryString.Item("nAction")), Session("nType"), UCase(Session("sCodispl_CR")), Session("nNumber"), Session("nBranch_rei"), Session("dEffecdate")))

If Request.QueryString.Item("sGoToNext") = "Yes" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction = '" & Request.QueryString.Item("nAction") & "';</SCRIPT>")

mclsSequence = Nothing
mclsCoReinsuran = Nothing

%>





