<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">
'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsClass As eInterface.MasterSheet


</script>
<%Response.Expires = -1
mclsSequence = New eFunctions.Sequence
mclsClass = New eInterface.MasterSheet
%>
<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
        document.VssVersion="$$Revision: 5 $|$$Date: 10/03/03 12:04 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
<BASE TARGET="fraFolder">
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>

<%
'+ Si la acción pasada como parámetro posee algún valor, se carga la secuencia del cliente seleccionado
If Not IsNothing(Request.QueryString.Item("nAction")) Then
	
	'+ Se invoca el método que genera la secuencia de ventanas
	Response.Write(mclsClass.LoadTabsMasterSheet(CInt(Request.QueryString.Item("nAction")), Session("sSche_code"), Session("nSheet")))
	
	If Request.QueryString.Item("sGoToNext") <> "NO" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
	End If
	mclsSequence = Nothing
	mclsClass = Nothing
Else
	'+ En el caso que no se encuentre secuencia asociada, se carga la imagen del FRAME principal
	'+ por defecto
	
	%>      
	<SCRIPT>top.fraFolder.document.location = "/VTimeNet/Common/Blank.htm"</SCRIPT> 
<%	
End If
Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction =" & Request.QueryString.Item("nAction") & "</SCRIPT>")
If CDbl(Request.QueryString.Item("nAction")) = 401 Then
	Session("bQuery") = True
Else
	Session("bQuery") = False
End If
%>
</BODY>
</HTML>





