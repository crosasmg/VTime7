<%@ Page LANGUAGE="VB" explicit="true" %>
<%Response.Expires = -1440
Response.Write("***HA OCURRIDO UNA EXCEPCI�N***<BR>" & Request.QueryString.Item("msg"))
%>




