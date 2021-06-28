<%		
		On Error Resume Next
		session("owRpt").ReadRecords()
		If Err.Number <> 0 Then
			Response.Write("An Error has occured on the server in attempting to access the data source<BR>")
			Response.Write("[" & Err.Number & "] " & Err.Description)
		Else
			If Not IsNothing(session("owPageEngine")) Then
				'UPGRADE_NOTE: Object session() may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
				session("owPageEngine") = Nothing
			End If
			session("owPageEngine") = session("owRpt").PageEngine
		End If
		
		%>




