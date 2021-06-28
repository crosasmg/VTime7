<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values


'% getName: toma el nombre del parámetro que recibe en el QueryString
'--------------------------------------------------------------------------------------------
Private Function getName(ByRef lintIndex As Double) As String
	'--------------------------------------------------------------------------------------------
	Dim lstrForm As String
	Dim lintCount As Integer
	
	lstrForm = Request.Params.Get("Query_String")
	For lintCount = 1 To lintIndex - 1
		lstrForm = Mid(lstrForm, InStr(1, lstrForm, "&") + 1)
	Next 
	getName = Mid(lstrForm, 1, InStr(1, lstrForm, "=") - 1)
End Function

'% insGetFields: toma las descripciones de los parámetros que recibe en el QueryString
'--------------------------------------------------------------------------------------------
Private Sub insGetFields()
	'--------------------------------------------------------------------------------------------
	Dim lobjValues As eFunctions.Values
	Dim lstrCommand As String
	Dim lintIndex As integer
	
	lobjValues = New eFunctions.Values
	'For lintIndex = 1 To Request.QueryString.Count
    For lintIndex = 0 To Request.QueryString.Count -1
		If getName(lintIndex) = "sFields" Then
			lstrCommand = Request.QueryString.Item(lintIndex)
		End If
	Next 
	
	Response.Write(lstrCommand)
	
	lobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
</HEAD>
<BODY  ONUNLOAD="closeWindows()">
<FORM METHOD="POST" NAME="CA026" ACTION="ValPolicySeq.aspx?sCodispl=CA026&WindowType=PopUp">
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2"><%=mobjValues.ShowWindowsName("CA026")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><HR></TD>
        </TR>
		<TR>
            <TD><LABEL ID=13241><%= GetLocalResourceObject("tcdExpDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdExpDate", Session("dEffecdate"))%></TD>
        </TR>
		<TR>
			<TD ALIGN="RIGHT" COLSPAN="2"><%=mobjValues.ButtonAcceptCancel( , "top.document.location=/VTimeNet/common/secWHeader.aspx?sCodispl=CA001_K&sProject=PolicySeq&sModule=Policy", True)%>
		</TR>
    </TABLE>
    <%Call insGetFields()%>    
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
%>





