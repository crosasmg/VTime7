<%@ Page Language="VB" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="InMotionGIT.Common.Proxy" %>
<%@ Import Namespace="System.Web.Services" %>
<%@ Import Namespace="System.Web.Script.Services" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ClientInformation(clientID As String) As String
        Dim result As String = String.Empty
        With New DataManagerFactory("SELECT SCLIENAME FROM CLIENT WHERE SCLIENT=@:SCLIENT", "CLIENT", "BackOfficeConnectionString")
            .Cache = InMotionGIT.Common.Enumerations.EnumCache.CacheWithFullParameters
            .AddParameter("SCLIENT", DbType.AnsiStringFixedLength, 14, False, clientID)
            result = .QueryExecuteScalarToString
        End With
        If result.IsNotEmpty Then
            result = result.Trim
        End If
        Return result
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
