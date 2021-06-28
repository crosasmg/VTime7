<%@ Page LANGUAGE="VB" explicit="true" %>
<%Application("nOpenSlots") = Request.QueryString.Item("avail")
Response.Write(Application("nOpenSlots"))

%>




