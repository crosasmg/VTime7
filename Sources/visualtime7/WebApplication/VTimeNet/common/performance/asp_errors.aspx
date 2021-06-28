<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

Dim lrecInsASP_Errors As eRemoteDB.Execute
Dim ObjError As System.Exception
Dim strNumber_Err As Integer
Dim strSource As Integer
Dim strPage As String
Dim strDesc As String
Dim strCode As String
Dim strLine As Integer
Dim strASPDesc As String
Dim strRemoteAddr As String
Dim strRemoteHost As String
Dim strLocalAddr As String
Dim strQuery_String As String


</script>
<%Response.Buffer = True%>
<HTML>
<HEAD><TITLE>Manejo de Errores ASP</TITLE>
<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/common/Custom.css">
</HEAD>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>


<BODY>
<%
'Referencing the error object
ObjError = Server.GetLastError()

strNumber_Err = 0 'ObjError.HResult '.AspCode
strSource = String.Empty 'ObjError.Category
strPage = ObjError.Source 'File
strDesc = ObjError.Message  'Description
strCode = Server.HtmlEncode(CStr(ObjError.Source))
If strCode = "" Then
	strCode = "Código no disponible"
End If
strLine = 0 'ObjError.Line
strASPDesc = ObjError.Message  'ASPDescription


'You get the entire context of the page that had the error.
'Review the server variables to see if you would want to store more information.
strRemoteAddr = Request.ServerVariables.Item("REMOTE_ADDR")
strRemoteHost = Request.ServerVariables.Item("REMOTE_HOST")
strLocalAddr = Request.ServerVariables.Item("LOCAL_ADDR")
strQuery_String = Request.ServerVariables.Item("QUERY_STRING")

'Conexion a la BD: aqui se hace el Insert en la tabla de Errores 5xx
lrecInsASP_Errors = New eRemoteDB.Execute

With lrecInsASP_Errors
	.StoredProcedure = "INSASP_ERRORS"
	.Parameters.Add("SNUMBER_ERR", strNumber_Err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SSOURCE", strSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SPAGE", strPage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SDESC", strDesc & ". " & strASPDesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SCODE", strCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SLINE", strLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SREMOTE_ADDR", strRemoteAddr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SREMOTE_HOST", strRemoteHost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SLOCAL_ADDR", strLocalAddr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SQUERY_STRING", strQuery_String, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Run(False)
End With

%>
   
    <P ALIGN=CENTER><FONT FACE=ARIAL SIZE=4><STRONG>Error en página <%=strPage%></STRONG></FONT></P>
   
    <FONT FACE=ARIAL SIZE=1>
    <TABLE WIDTH="50%" ALIGN="CENTER" CELLSPACING="0" CELLPADDING="0" BORDER="1">
     <TR>
       <TD WIDTH="200">Código de Error ASP:</TD><TD>&nbsp;<%=strNumber_Err%></TD></TR>
     <TR>
       <TD WIDTH="200">Categoría:</TD><TD>&nbsp;<%=strSource%></TD></TR>
     <TR>
       <TD WIDTH="200">Archivo:</TD><TD>&nbsp;<%=strPage%></TD></TR>
     <TR VALIGN="TOP">
       <TD WIDTH="200">Descripción:</TD><TD>&nbsp;<%=strDesc & ". " & strASPDesc%></TD></TR>
     <TR>
       <TD WIDTH="200">Código:</td><td>&nbsp;<%=strCode%></TD></TR>
     <TR>
       <TD WIDTH="200">Línea:</TD><TD>&nbsp;<%=strLine%></TD></TR>
      <TR>
       <TD WIDTH="200">Dirección Remota:</TD><TD>&nbsp;<%=strRemoteAddr%></TD></TR>
     <TR>
       <TD WIDTH="200">Host Remoto:</TD><TD>&nbsp;<%=strRemoteHost%></TD></TR>
     <TR>
       <TD WIDTH="200">Dirección Local:</TD><TD>&nbsp;<%=strLocalAddr%></TD></TR>
    </TABLE>
    </FONT>
        
</BODY>
</HTML>
<%

'Kill them their objects   
'UPGRADE_NOTE: Object ObjError may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
ObjError = Nothing
'UPGRADE_NOTE: Object lrecInsASP_Errors may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lrecInsASP_Errors = Nothing

Response.End()
%>    





