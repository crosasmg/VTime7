<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values


'%insAskToLogout: Muestra la ventana para el termino de session
'--------------------------------------------------------------------------------------------
Private Sub insAskToLogout()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=40530>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT>")


Response.Write(mobjValues.ButtonAcceptCancel())


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
End Sub

'%insLogout: Recarga la pagina
'--------------------------------------------------------------------------------------------
Private Sub insLogout()
	'--------------------------------------------------------------------------------------------
	Session.Abandon()
	
Response.Write("" & vbCrLf)
Response.Write("    <SCRIPT>" & vbCrLf)
Response.Write("        if (typeof(opener)!='undefined')" & vbCrLf)
Response.Write("            opener.top.location.reload();" & vbCrLf)
Response.Write("        self.close();" & vbCrLf)
Response.Write("    </" & "SCRIPT>")

	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <TITLE><%=GetLocalResourceObject("TitleCaption")%></TITLE>
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY>
    <FORM NAME="Logout" ACTION="Logout.aspx?sLogout=1" METHOD="POST">
<%
If Request.QueryString.Item("sLogout") = "1" Then
	Call insLogout()
Else
	Call insAskToLogout()
End If
%>    
    </FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>





