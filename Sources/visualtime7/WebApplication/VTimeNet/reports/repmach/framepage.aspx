<script language="VB" runat="Server">
Dim qs As String


</script>
<%
' 05/02/98
' Added the following features:
' Page Expiry Time
' -  The page will expire when downloaded by browser so that user is insured that all data
' will be current.
response.Expires = 0
%>
<html>
<title></title>
<%
qs = request.Params.Get("Query_String")
If qs <> "" Then
	qs = "&" & qs
End If
%>
<frameset rows="59,*">
<frame marginheight="0" marginwidth="0" noresize scrolling="no" name="CrystalViewerToolbar" src="../../Reports/RepMach/toolbar.aspx?<%response.Write(request.Params.Get("Query_String"))%>">
<frame name="CrystalViewerPreview" src="../../Reports/RepMach/rptserver.aspx?cmd=get_pg&amp;viewer=html_frame&amp;vfmt=html_frame&amp;page=<%response.Write(session("wCurrentPageNumber"))%><%response.Write(qs)%>">
</frameset>
</html>




