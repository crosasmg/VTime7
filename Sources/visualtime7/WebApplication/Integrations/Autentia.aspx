<%@ Page Language="VB" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web.Services" %>
<%@ Import Namespace="System.Web.Script.Services" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
    Public Shared Function GToken() As String
        Dim guid As System.Guid = New System.Guid()
        Dim guidString As String = Guid.NewGuid().ToString()

        Dim ctx As HttpContext = System.Web.HttpContext.Current
        ctx.Session("AutentiaToken") = guidString

        Return guidString
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function VToken(token As String) As Boolean
        Dim ctx As HttpContext = System.Web.HttpContext.Current
        Dim tokenSession As String = ctx.Session("AutentiaToken").ToString()

        If token.Equals(tokenSession) Then
            Return True
        Else
            Return False
        End If
    End Function

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>   
    </div>
    </form>
</body>
</html>
