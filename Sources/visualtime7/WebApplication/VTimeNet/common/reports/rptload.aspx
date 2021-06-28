<%@ Page LANGUAGE="VB" explicit="true" %>
<script language="VB" runat="Server">
Dim i As Integer


</script>
<%Response.Expires = -1440


Application("nTicketCount") = 0
Application("nCurrentTicket") = 1

Response.Write("<SCRIPT>")
For i = 1 To CInt(Request.QueryString.Item("times"))
	Response.Write("window.open('/VTimeNet/Common/Reports/PDFReport.aspx?URL=/reports/cal01506.rpt&ServerName=&DataBase=&Server=0&sp=2&sp=403&sp=699&sp=0&sp=1&sp=1971&Merge=Verdadero&MergeBranch=2&MergeProduct=403&MergePolicy=" & Request.QueryString.Item("policy") & "&MergeCertif=0');")
Next 
Response.Write("</script>")

%>




