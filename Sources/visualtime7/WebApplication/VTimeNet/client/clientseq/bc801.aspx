<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objetos genericos de valores, menu y grilla    
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objetos genericos de cliente     
Dim mobjClient As eClient.Client


'%insPreSi007M. Esta funcion se encarga de realizar la busqueda de los datos de cliente
'------------------------------------------------------------------------------------
Private Sub insPreSi007M()
	'------------------------------------------------------------------------------------
	mobjClient = New eClient.Client
	mobjClient.Find(Session("sClient"))
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

'+Se realiza el llamado a la funcion insPreSi007M, para obtener los datos del cliente en tratamiento
insPreSi007M()
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%Response.Write(mobjMenu.setZone(2, "BC801", "BC801.aspx"))
Response.Write(mobjValues.StyleSheet())
mobjMenu = Nothing
%>
<FORM METHOD="POST" ID="FORM" NAME="frmBC007M" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
	<TABLE WIDTH="100%">
		<TR>
		    <TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("cbeDisabilityCaption") %></LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("cbeDisability", "Table5505", eFunctions.Values.eValuesType.clngComboType, CStr(mobjClient.nDisability),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDisabilityToolTip"))%></TD>
		    <TD></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeIncapacityCaption") %></LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("cbeIncapacity", "Table5549", eFunctions.Values.eValuesType.clngComboType, CStr(mobjClient.nIncapacity),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeIncapacityToolTip"))%></TD>
		    <TD></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tcdIncapacityCaption") %></LABEL></TD>
		    <TD><%=mobjValues.DateControl("tcdIncapacity", CStr(mobjClient.dIncapacity),  , GetLocalResourceObject("tcdIncapacityToolTip"))%></TD>
		    <TD></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("valIncap_codCaption") %></LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("valIncap_cod", "Table5550", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjClient.nIncap_cod),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valIncap_codToolTip"))%></TD>
		    <TD></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
'+ Si la variable de sesión "sOriginalForm" es diferente de blanco,
'+ entonces se invoca a la función "insEnabledFields" - ACM - 07/08/2001
If CStr(Session("sOriginalForm")) <> vbNullString Then
	Response.Write("<SCRIPT>insEnabledFields();</SCRIPT>")
End If

mobjValues = Nothing
mobjClient = Nothing
%>




