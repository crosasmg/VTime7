<%@ Page LANGUAGE="VB" %>
<%Response.Expires = -1


If CStr(Session("CEReport")) = "1" Then
	Response.Redirect("CEReport.aspx?" & Request.Params.Get("Query_String"))
Else
	Response.Redirect("PDFReport.aspx?" & Request.Params.Get("Query_String"))
End If
%>
 






