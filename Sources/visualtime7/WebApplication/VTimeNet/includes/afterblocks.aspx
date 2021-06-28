<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">


'**% AfterValidate:
'% AfterValidate:
'------------------------------------------------------------------------------------------------
Private Function AfterValidate(ByRef sValidate As Object) As String
	Dim mblnSkipPost As Object
	'------------------------------------------------------------------------------------------------
	Dim objAfterValidate As eFunctions.AfterProcess
	Dim objSessionItem As String
	Dim strSessionVariables As String
	Dim objArray As Object
	
	strSessionVariables = vbNullString
	
	For	Each objSessionItem In Session.Contents
		If Not IsNothing(Session.Contents.Item(objSessionItem)) Then
			strSessionVariables = strSessionVariables & (objSessionItem & "=Session object cannot be displayed.&")
		Else
			If IsArray(Session.Contents.Item(objSessionItem)) Then
				For	Each objArray In Session.Contents.Item(objSessionItem)
					strSessionVariables = strSessionVariables & "&" & Session.Contents(objSessionItem) & "(" & objSessionItem & "):" & Session.Contents.Item(objSessionItem)(objArray)
				Next objArray
			Else
				strSessionVariables = strSessionVariables & (objSessionItem & "=" & Session.Contents.Item(objSessionItem) & "&")
			End If
		End If
	Next objSessionItem
	
	objAfterValidate = New eFunctions.AfterProcess
	AfterValidate = objAfterValidate.AfterValidate(Request.Form.ToString, Request.Params.Get("Query_String"), strSessionVariables, mblnSkipPost, sValidate)
	objAfterValidate = Nothing
End Function

</script>








