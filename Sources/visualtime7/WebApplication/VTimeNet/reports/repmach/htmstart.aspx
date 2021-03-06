<script language="VB" runat="Server">
Dim qs As String

Dim tmpArray As Object
Dim index As Short
Dim brch As Object
Dim val As Object


'  This function initializes the tabArray. 
Sub InitializeFrameArray()
	'initialize the html_frame array
	'UPGRADE_NOTE: Object session() may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	session("wtabArray") = Nothing
	session("wlastBrch") = ""
	Dim tmpArray() As Object
	'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	tmpArray = New Object(){4}
	ReDim tmpArray(4)
	'Initialize the sequence number
	tmpArray(0) = "EMPTY"
	session("wtabArray") = tmpArray.Clone()
End Sub

</script>
<%
' 05/02/98
' Added the following features:
' Tab Query String Parameter
' - This is the selected tab's tabArray index value.  
' Page Expiry Time
' -  The page will expire when downloaded by browser so that user is insured that all data
' will be current.
response.Expires = 0
On Error Resume Next
qs = request.Params.Get("Query_String")
If qs <> "" Then
	qs = "&" & qs
Else
	' Need to make this call for backward compatibility.  Users may be referencing htmstart.aspx in their web pages.
	Call InitializeFrameArray()
End If
If Not IsNothing(request.QueryString.Item("TAB")) Then
	tmpArray = session("wtabArray")
	index = CShort(request.QueryString.Item("TAB"))
	
	If tmpArray(index + 1) <> "" Then
		brch = tmpArray(index + 1)
		qs = "&" & "BRCH=" & brch
	End If
	
	session("wCurrentPageNumber") = tmpArray(index + 2)
	session("wLastknownpage") = tmpArray(index + 3)
	session("wLastPageNumber") = tmpArray(index + 4)
	' clear out all of the other arrays
	If index = 0 Then
		Call InitializeFrameArray()
	Else
		ReDim Preserve tmpArray(index - 1)
		session("wtabArray") = tmpArray
	End If
	
	
Else
	session("wCurrentPageNumber") = "1"
	session("wlastknownpage") = "0"
	session("wLastPageNumber") = ""
End If


%>

<html>
<title></title>
<frameset cols="0%,*">
<frame name="CrystalViewerTree" src="../../Reports/RepMach/rptserver.aspx?cmd=get_ttl&amp;viewer=html%5Fframe&amp;vfmt=html_frame<%response.Write(qs)%>"> 
<frame name="CrystalViewerPageFrame" src="../../Reports/RepMach/rptserver.aspx?cmd=toolbar_page&amp;viewer=html%5Fframe&amp;vfmt=html_frame&amp;page=<%response.Write(session("wCurrentPageNumber"))%><%response.Write(qs)%>">
</frameset>
</html>





