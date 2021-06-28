<%
If CStr(Session("SessionID")) = vbNullString Then
	Response.Redirect("/VTimeNet/VisualTime/VisualTime.htm")
End If
Response.Expires = 0
%>




