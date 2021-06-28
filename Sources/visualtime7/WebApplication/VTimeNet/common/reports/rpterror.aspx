<%@ Page LANGUAGE="VB" explicit="true" %>
<%Response.Expires = -1440
Response.Write("***HA OCURRIDO UNA EXCEPCIÓN***<BR>" & Request.QueryString.Item("msg"))
%>




