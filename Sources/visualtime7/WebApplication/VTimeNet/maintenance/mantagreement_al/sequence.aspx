<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objeto para el manejo de las páginas que forman la secuencia    
Dim mclsAgreemet_al As eBranches.Agreement_al


</script>
<%
Response.Expires = 0
mclsSequence = New eFunctions.Sequence
mclsAgreemet_al = New eBranches.Agreement_al
%>
<HTML>
<HEAD>

<META NAME="ProgId" content="FrontPage.Editor.Document">
<BASE TARGET="fraFolder">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<%
Response.Write("<SCRIPT>")
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
'+ Se invoca el método que genera la secuencia de ventanas
Response.Write(mclsAgreemet_al.LoadTabs(CInt(Request.QueryString.Item("nMainAction")), Session("nAgreement"), Session("sSche_code")))

If Request.QueryString.Item("sGoToNext") <> "NO" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction =" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")

mclsSequence = Nothing
mclsAgreemet_al = Nothing
%>
</BODY>
</HTML>




