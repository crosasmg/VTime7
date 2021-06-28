<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
    Dim mobjValues As eFunctions.Values = New eFunctions.Values
</script>
<HTML>
<HEAD>
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY><BODY OnLoad="window.defaultStatus='      VisualTIME Production Environment';"></BODY>
</HTML>
<%
    mobjValues = Nothing
%>




