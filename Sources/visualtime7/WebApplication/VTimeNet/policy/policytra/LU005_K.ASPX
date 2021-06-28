<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>

<script language="VB" runat="Server">
    Dim mobjValues As eFunctions.Values
    Dim rutserver As String
</script>

<%
    mobjValues = New eFunctions.Values
    Response.Expires = -1440
    rutserver = mobjValues.insGetSetting("BucDirectory", "http://192.168.153.46:8089", "Paths")
    'Response.Write("<SCRIPT>alert("" " & rutserver & """);</" & "Script>")
    Response.Write("<script>window.open('" & rutserver & "/lu005.aspx?sa=1','','toolbar=no,status=no,menu=no, directories=no,titlebar=no,location=no,addressbar=no,scrollbars=yes,resizable=yes' );top.window.close();</script>")

%>

